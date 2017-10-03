using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face.Skin01
{
	/// <summary>
	/// Clase Base que define la interfaz de un ManagerEntityForm
	/// </summary>
    public partial class ItemMngSkinForm : moleQule.Face.ItemMngBaseForm
	{
		#region Bussiness Methods

        #endregion
        
        #region Factory Methods
		
		/// <summary>
		/// Constructor para formularios de insercion (AddForms)
		/// No se le especifica Oid asociado al formulario
		/// </summary>
		public ItemMngSkinForm() 
			: this(-1, true) {}

		/// <summary>
		/// Constructor para formularios de inserción (AddFoms) modales
		/// No se le especifica Oid asociado al formulario
		/// </summary>
		/// <param name="is_modal"></param>
		public ItemMngSkinForm(bool is_modal) 
			: this(-1, is_modal) {}

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms)
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
		public ItemMngSkinForm(long oid) 
			: this(oid, true) {}

        /// <summary>
        /// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
        /// </summary>
        /// <param name="oid">Oid del objeto que se va a editar</param>
        public ItemMngSkinForm(long oid, bool is_modal) 
			: this(oid, is_modal, null) {}

        /// <summary>
        /// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
        /// </summary>
        /// <param name="oid">Oid del objeto que se va a editar</param>
        public ItemMngSkinForm(long oid, Form parent) 
			: this(oid, true, parent) {}

        /// <summary>
        /// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
        /// </summary>
        /// <param name="oid">Oid del objeto que se va a editar</param>
        public ItemMngSkinForm(long oid, bool is_modal, Form parent)
            : this(oid, null, is_modal, parent) {}

		/// <summary>
		/// Constructor para formularios asociados a un objeto (ViewForms & EditForms) modales
		/// </summary>
		/// <param name="oid">Oid del objeto que se va a editar</param>
		public ItemMngSkinForm(long oid, object[] parameters, bool is_modal, Form parent)
			: base(oid, parameters, is_modal, parent)
		{
			InitializeComponent();
		}

		#endregion

		#region Layout

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

				case molAction.ShowDocuments:
					Docs_BT.Visible = state;
					break;
			}
		}	

        public override void FormatControls()
        {
			base.FormatControls();

			ControlsMng.CenterButtons(Paneles2.Panel1);
        }

		protected void ShowStatusBar(string message)
		{
			PanelesV.FixedPanel = FixedPanel.Panel1;
			Paneles2.FixedPanel = FixedPanel.Panel2;
			Paneles2.Panel2Collapsed = false;
			PanelesV.Panel2MinSize += Paneles2.Panel2MinSize;
			Paneles2.Panel1MinSize = PanelesV.Panel2MinSize - Paneles2.Panel2.Height - Paneles2.SplitterWidth;
			PanelesV.FixedPanel = FixedPanel.Panel2;
			Paneles2.FixedPanel = FixedPanel.None;		

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

		private void Save_BT_Click(object sender, EventArgs e)  { ExecuteAction(molAction.Save); }

		private void Cancel_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.Cancel); }

        private void Print_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

        private void Docs_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.ShowDocuments); }

        #endregion

        #region Events

		private void ManagerEntitySkinForm_Resize(object sender, EventArgs e)
		{
			/*if (this.WindowState != FormWindowState.Minimized)
			{
				PanelesV.SplitterDistance = PanelesV.Height - PanelesV.Panel2MinSize - 1 - Paneles2.Panel2MinSize - 1;
				Paneles2.SplitterDistance = Paneles2.Height - Paneles2.Panel2MinSize - 1;
			}*/
		}

		#endregion
	}
}