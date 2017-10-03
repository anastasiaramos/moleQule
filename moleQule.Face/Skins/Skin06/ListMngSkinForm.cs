using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face.Skin06
{
	/// <summary>
	/// Clase Base para Formularios de Consulta, Edición y Borrado de Elementos de una Entidad
	/// </summary>
	public partial class ListMngSkinForm : moleQule.Face.ListMngBaseForm
	{
		#region Bussiness Methods

        #endregion
        
        #region Factory Methods
		
		/// <summary>
		/// Constructor para formularios de insercion (AddForms)
		/// No se le especifica Oid asociado al formulario
		/// </summary>
		public ListMngSkinForm() 
			: this(false, null) {}

        /// <summary>
        /// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
        /// </summary>
        /// <param name="oid">Oid del objeto que se va a editar</param>
		public ListMngSkinForm(bool is_modal) 
			: this(is_modal, null) { }

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
		public ListMngSkinForm(bool is_modal, Form parent)
			: this(null, is_modal, parent) { }

		public ListMngSkinForm(object[] parameters, bool is_modal, Form parent)
			: base(parameters, is_modal, parent)
		{
			InitializeComponent();
		}

		#endregion

		#region Layout

		public override void FormatControls()
		{
			base.FormatControls();

			Status_SC.SplitterDistance = Status_SC.Height - Status_SC.Panel2MinSize - Status_SC.SplitterWidth;
			ControlsMng.CenterButtons(Status_SC.Panel1);
		}

		protected void ShowStatusBar(string message)
		{
			this.Height = this.Height + BarraEstado_ST.Height;
            Status_SC.Height += BarraEstado_ST.Height;
			Status_SC.Panel2Collapsed = false;
            //Paneles2.Panel2MinSize = BarraEstado_ST.Height;
            Info_SL.Text = message;
		}

        protected override void SetReadOnlyControls(Control.ControlCollection controls)
        {
            //Se llama a la función genérica
            base.SetReadOnlyControls(controls);

            //Se activan los botones Guardar e Imprimir, que son los únicos disponibles
            Submit_BT.Enabled = true;
            Submit_BT.TabStop = true;
            Print_BT.Enabled = true;
            Print_BT.TabStop = true;

            return;
        }

		protected override void SetView(molView view)
        {
            switch (view)
            {
                case molView.Select:
                    HideAction(molAction.Add);
                    HideAction(molAction.Delete);
                    HideAction(molAction.Print);
                    ShowAction(molAction.Select);

					MaximizeForm(new Size(Width, Height));
                    break;

                case molView.Normal:
                    HideAction(molAction.Select);

					MaximizeForm(new Size(Width, Height));
                    break;
            }

        }

        protected override void ActivateAction(molAction action, bool state)
        {
            switch (action)
            {
                case molAction.Add:
                    Add_TI.Visible = state;
                    break;

                case molAction.Delete:
                    Delete_TI.Visible = state;
                    break;

                case molAction.Print:
                    Print_TI.Visible = state;
                    Print_BT.Visible = state;
                    Separator2_TI.Visible = state;
                    break;

                case molAction.Select:
                    Select_TI.Visible = state;
                    break;

                case molAction.Close:
                    Close_TI.Visible = state;
                    break;
            }
        }	
		
        #endregion

		#region Buttons

        private void Guardar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Save); }

        private void Cancelar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Cancel); }

        private void Imprimir_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

        private void Add_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Add); }

        private void Delete_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Delete); }

        private void Select_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Select); }

        private void Print_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

        private void Close_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Close); }

        #endregion

        #region Events

        private void ListMngSkinForm_Resize(object sender, EventArgs e)
		{
			if (this.WindowState != FormWindowState.Minimized)
			{
				Buttons_SC.SplitterDistance = Buttons_SC.Height - Buttons_SC.Panel2MinSize - 1 - Status_SC.Panel2MinSize - 1;
				Status_SC.SplitterDistance = Status_SC.Height - Status_SC.Panel2MinSize - 1;
			}
		}

		#endregion

	}
}