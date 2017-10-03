using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face.Resources;
using moleQule.Library;

namespace moleQule.Face
{
    /// <summary>
    /// Formulario Base para Consulta, Edición y Borrado de UN ELEMENTO de un Tipo de Entidad
    /// </summary>
    public partial class ItemMngBaseForm : ManagerForm
    {
        #region Attributes & Properties

        /// <summary>
        /// Oid que hace referencia al objeto que maneja la clase
        /// </summary>
        protected long _oid = -1;

        /// <summary>
        /// Oid que hace referencia al objeto que maneja la clase.
        /// -1 para formularios *AddForm
        /// </summary>
        public long Oid { get { return _oid; } set { _oid = value; } }

		protected bool IsChild { get; set; }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Constructor para formularios de insercion (AddForms)
        /// No se le especifica Oid asociado al formulario
        /// No es modal por defecto
        /// </summary>
        public ItemMngBaseForm() 
			: this(true) { }

        /// <summary>
        /// Constructor para formularios de inserción (AddFoms) modales
        /// No se le especifica Oid asociado al formulario
        /// </summary>
        /// <param name="isModal">Formulario modal</param>
        public ItemMngBaseForm(bool is_modal) 
			: this(-1, null, is_modal, null) { }

        /// <summary>
        /// Constructor para formularios asociados a un objeto (ViewForms & EditForms)
        /// </summary>
        /// <param name="oid">Oid del objeto que se va a editar</param>
        public ItemMngBaseForm(long oid) 
			: this(oid, null, true, null) { }

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
		/// <param name="isModal">Formulario modal</param>
		public ItemMngBaseForm(long oid, bool isModal, Form parent)
			: this(oid, null, isModal, parent) {}

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
		/// <param name="isModal">Formulario modal</param>
		public ItemMngBaseForm(long oid, object[] parameters, bool isModal, Form parent)
			: base(isModal, parent)
		{
			InitializeComponent();

			_oid = oid;
			IsChild = false;

			GetFormData(oid, parameters);
		}

        #endregion

		#region Actions

		public override void DoExecuteAction(molAction action)
		{
			switch (action)
			{
				case molAction.CustomAction1:

					if (this.Datos.Count == 0) return;
					CustomAction1();

					break;

				case molAction.CustomAction2:

					if (this.Datos.Count == 0) return;
					CustomAction2();
					
					break;

				case molAction.CustomAction3:

					if (this.Datos.Count == 0) return;
					CustomAction3();
					
					break;

				case molAction.CustomAction4:

					if (this.Datos.Count == 0) return;
					CustomAction4();
					
					break;

				case molAction.Refresh:

					RefreshAction();
					break;

				default:
					base.DoExecuteAction(action);
					break;
			}
		}

		public override void ExecuteAction(molAction action, bool nested)
		{
			try
			{
#if TRACE
				PgMng.Record(String.Format("ItemMngBaseForm::ExecuteAction {0} INI", action.ToString()));
#endif
				if (!nested)
				{
					if (Status == EStatus.Working) return;

					Status = EStatus.Working;

					_current_action = action;

					//Se usa un atributo porque si uso el DialogResult el ShowDialog entiende que quiero cerrar el formulario
					_action_result = DialogResult.Ignore;
				}

				DoExecuteAction(action);
#if TRACE
				PgMng.Record(String.Format("ItemMngBaseForm::ExecuteAction {0} END", action.ToString()));
#endif
			}
			catch (iQValidationException ex)
			{
				Control control = ControlsMng.GetControlByProperty(Controls, ex.Field);
				
				if (control != null)
					MarkError(control, ex.Message);
				else
					PgMng.ShowWarningException(ex);
			}
			catch (Exception ex)
			{
				PgMng.ShowErrorException(ex);
			}
			finally
			{
				Status = EStatus.OK;
				if (PgMng != null) PgMng.FillUp();
			}
		}

		protected virtual void CustomAction1() { throw new iQImplementationException("CustomAction1"); }
		protected virtual void CustomAction2() { throw new iQImplementationException("CustomAction2"); }
		protected virtual void CustomAction3() { throw new iQImplementationException("CustomAction3"); }
		protected virtual void CustomAction4() { throw new iQImplementationException("CustomAction4"); }

		protected virtual void RefreshAction() { throw new iQImplementationException("RefreshAction"); }

		#endregion
	}
}

