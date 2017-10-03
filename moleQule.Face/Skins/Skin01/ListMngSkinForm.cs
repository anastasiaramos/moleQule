using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face.Skin01
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
			: this(null, is_modal, parent) {}

		public ListMngSkinForm(object[] parameters, bool is_modal, Form parent)
			: base(parameters, is_modal, parent)
		{
			InitializeComponent();
		}
		 

		#endregion

		#region Layout & Source

		protected override void ActivateAction(molAction action, bool state)
		{
			switch (action)
			{
				case molAction.Submit:
					Submit_BT.Visible = state;
					break;

				case molAction.Print:
					Imprimir_Button.Visible = state;
					break;

				case molAction.Close:
					Cancel_BT.Visible = state;
					break;
			}
		}	

        public override void FormatControls()
        {
			base.FormatControls();

			int botones = 0, espacio = 3, tab, pos = 0;
            int formWidth = Paneles2.Panel1.Width;
            int formHeight = Paneles2.Panel1.Height;
            int buttonWidth = Submit_BT.Size.Width;
            int buttonHeight = Submit_BT.Size.Height;

            foreach (Control ctl in Paneles2.Panel1.Controls)
            {
               if ((ctl.GetType().Name == "Button") && ctl.Visible)
                   botones++;
            }

            tab = (formWidth - espacio * (botones - 1) - buttonWidth * botones) / 2;

            foreach (Control ctl in Paneles2.Panel1.Controls)
            {
                if ((ctl.GetType().Name == "Button") && ctl.Visible)
                {
                    int x = tab + (espacio + buttonWidth) * pos;
                    int y = (formHeight - buttonHeight) / 2;

                    ctl.SetBounds(x, y, buttonWidth, buttonHeight);
                    pos++;
                }
            }
        }

		protected void ShowStatusBar(string message)
		{
			this.Height = this.Height + BarraEstado_ST.Height;
			Paneles2.Height += BarraEstado_ST.Height;
			Paneles2.Panel2Collapsed = false;
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
			Imprimir_Button.Enabled = true;
			Imprimir_Button.TabStop = true;

			return;
		}

		#endregion

		#region Buttons

        private void Guardar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Save); }

        private void Cancelar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Cancel); }

        private void Imprimir_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

        #endregion

        #region Events


		private void ManagerSkin01Form_Resize(object sender, EventArgs e)
		{
			if (this.WindowState != FormWindowState.Minimized)
			{
				PanelesV.SplitterDistance = PanelesV.Height - PanelesV.Panel2MinSize - 1 - Paneles2.Panel2MinSize - 1;
				Paneles2.SplitterDistance = Paneles2.Height - Paneles2.Panel2MinSize - 1;
			}
		}

		#endregion


	}
}