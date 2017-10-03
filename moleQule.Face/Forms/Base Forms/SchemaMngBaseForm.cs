using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face
{
    /// <summary>
    /// Form Base para Gestión del Tipo de Entidad Principal (Schema). 
    /// Consulta, Creación, Edición, Borrado, Filtrado y Localización.
    /// Se gestiona mediante una Lista de Elementos de ese Tipo
    /// </summary>
    public partial class SchemaMngBaseForm : moleQule.Face.EntityMngBaseForm, IBackGroundLauncher
    {
        #region Properties

		ISchemaInfo _schema;

        /// <summary>
        /// Devuelve el ISchemaInfo seleccionado
        /// </summary>
        /// <returns></returns>
        public virtual ISchemaInfo ActiveISchema { get { return SchemaInfo.GetISchemaInfo(ActiveOID); } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Definido solo por compatibilidad con el IDE
        /// </summary>
        protected SchemaMngBaseForm()
			:this(false, null, null, null) {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        /// El constructor del SchemaMngForm debe inicializar el tipo de schema que va 
        /// a utilizarse como clase principal de la aplicacion. 
        /// </remarks>
        /// <param name="type">Tipo del formulario que maneja la entidad principal</param>
        protected SchemaMngBaseForm(bool isModal, Form parent, Type type, object list)
            : base(isModal, parent)
		{
			InitializeComponent();

			//IDE Compatibility
			if (type != null)
			{
				//Establecemos el tipo del mng actual como activo
				//AppContext.ActiveSchemaType = type;
			}

			_item_list = list;
        }

		#endregion

        #region Layout & Source

        /// <summary>
        /// Indica visualmente en el formulario cual es el esquema por defecto
        /// </summary>
        public virtual void MarkDefaultSchema() {}

        #endregion

        #region Business Methods

        /// <summary>
		/// Establece el objeto ActiveISchema actual como esquema principal de datos.
        /// Esto indica al sistema que trabaje con los datos de este esquema
		/// </summary>
		public virtual void LoadSchema() { LoadSchema(ActiveISchema); }
        
        /// <summary>
		/// Establece un objeto como esquema principal de datos.
        /// Esto indica al sistema que trabaje con los datos de este esquema
		/// </summary>
		public virtual void LoadSchema(ISchemaInfo schema)
        {
			_schema = schema;
			PgMng.Reset(4, 1, Resources.Messages.LOADING_SCHEMA, (_parent != null) ? _parent : this);
			_back_job = BackJob.LoadSchema;
			//PgMng.StartBackJob(this);
			DoLoadSchema();
        }

		public virtual void DoLoadSchema()
		{
			// Cargar los datos de la empresa
			if ((_list_active_form.Count == 0) || (ProgressInfoMng.ShowQuestion(Resources.Messages.CURRENT_EDITION_CLOSE) == DialogResult.Yes))
			{
				//Cerrar todas las ventanas abiertas de la empresa actual
				FormMngBase.Instance.CloseAllChilds();

				PgMng.Grow();

				// Verificamos que el usuario actual tiene acceso a la empresa
				if (AppContext.User.CanAccessSchema(_schema.Oid))
				{
					try
					{
						AppContext.Principal.ChangeUserSchema(_schema);
						PgMng.Grow();

						MainBaseForm.Instance.Reload();
						PgMng.Grow();

						MainBaseForm.Instance.SetFormSkin();
						PgMng.FillUp();

						this.Close();
					}
					catch (Exception ex)
					{
						PgMng.FillUp();
						PgMng.ShowErrorException(ex);						
						MainBaseForm.Instance.Dispose();
						Application.Exit();
					}
				}
				else
				{
					PgMng.ShowInfoException(Resources.Messages.SCHEMA_NOT_ALLOWED);
				}
			}
		}

        /// <summary>
        /// Establece el objeto ActiveISchema actual como esquema por defecto
        /// Esto indica al sistema que cargue automáticamente este esquema al iniciar la aplicación
        /// </summary>
        public virtual void SetDefault() 
        {
            SettingsMng.Instance.SetDefaultSchema(ActiveISchema.Oid);
            MarkDefaultSchema();
        }
        
		#endregion

		#region IBackGroundLauncher

		/// <summary>
		/// La llama el backgroundworker para ejecutar codigo en segundo plano
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public new void BackGroundJob(BackgroundWorker bk)
		{
			try
			{
				switch (_back_job)
				{
					case BackJob.LoadSchema:
						BkLoadSchema(bk);
						break;

					default:
						base.BackGroundJob(bk);
						break;
				}
			}
			catch (Exception ex)
			{
				CancelBackGroundJob();
				PgMng.ShowInfoException(ex.Message);
				PgMng.FillUp();
			}
		}

		private void BkLoadSchema(BackgroundWorker bk)
		{
			DoLoadSchema();
		}

		#endregion
    }
}

