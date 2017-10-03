using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Face.Resources;
using moleQule.Library;

namespace moleQule.Face
{
	/// <summary>
	/// Clase Base para Formularios de Consulta, Edición y Borrado de una LISTA RAIZ de Entidades de un tipo
	/// </summary>
	public partial class ListMngBaseForm : moleQule.Face.ManagerForm
    {
        #region Attributes & Properties

        /// <summary>
        /// Objeto seleccionado
        /// </summary>
        protected object _selected;
		
        /// <summary>
        /// Objeto seleccionado
        /// </summary>
        public object Selected { get { return _selected; } }

        #endregion

        #region Factory Methods

        /// <summary>
		/// Constructor para formularios de insercion (AddForms)
		/// No se le especifica Oid asociado al formulario
		/// </summary>
		public ListMngBaseForm() 
			: this(true) {}

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
        public ListMngBaseForm(bool is_modal) 
			: this(is_modal, null) {}

        /// <summary>
        /// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
        /// </summary>
        /// <param name="oid">Oid del objeto que se va a editar</param>
        public ListMngBaseForm(bool is_modal, Form parent)
            : this(null, is_modal, parent) {}

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
		public ListMngBaseForm(object[] parameters, bool is_modal, Form parent)
			: base(is_modal, parent)
		{
			InitializeComponent();
			ViewMode = molView.Normal;

			GetFormData(-1, parameters);
		}

		#endregion

		#region Layout & Source

		protected override void FormatForm()
		{
			SetView();
			FormatControls();
		}

        protected virtual void SetSelectView() { SetView(molView.Select); }

        #endregion

		#region Validation & Format

		#endregion

        #region Actions

		public override void DoExecuteAction(molAction action)
		{
			switch (_current_action)
			{
				case molAction.Add:
					
					//EnableForm(false);
					AddAction();
					this.Enabled = true;
					break;

				case molAction.Delete:

					if (this.Datos.Count > 0)
					{
						//EnableForm(false);
						DeleteAction();
					}
					break;

				case molAction.Select:
					
					if (this.Datos.Count > 0)
					{
						_action_result = DialogResult.OK;
						SelectAction();
						DialogResult = _action_result;
					}
					break;

				default:
					base.DoExecuteAction(action);
					break;
			}
		}

        protected virtual void AddAction() { throw new iQImplementationException("AddAction"); }

        protected virtual void DeleteAction() { throw new iQImplementationException("DeleteAction"); }

        protected virtual void SelectAction()
        {
            _selected = Datos.Current;
        }

        protected override void DefaultAction() {}

        #endregion
    }
}