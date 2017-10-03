using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using moleQule.Library;

namespace moleQule.Face.Skin04
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

			SetView(molView.Normal);
		}

		#endregion

		#region Layout

		protected override void ActivateAction(molAction action, bool state)
		{
			if (EntityType == null) return;

			bool allow;

			switch (action)
			{
				case molAction.CustomAction1:
					Action1_TI.Visible = state;
					break;

				case molAction.CustomAction2:
					Action2_TI.Visible = state;
					break;

				case molAction.CustomAction3:
					Action3_TI.Visible = state;
					break;

				case molAction.CustomAction4:
					Action4_TI.Visible = state;
					break;

				case molAction.Cancel:
					Cancel_TI.Visible = state;
					Cancel_BT.Visible = state;
					break;

				case molAction.Unlock:

					ChangeState_TI.Visible = (ChangeState_TI.Visible) ? true : state;
					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					Abierto_TI.Enabled = state;
					Abierto_TI.Visible = state;

					if ((AppContext.User != null) && (state))
					{
						Abierto_TI.Enabled = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
					}

					break;

				case molAction.ChangeStateAnulado:

					allow = (bool)EntityType.InvokeMember("CanEditObject", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

					ChangeState_TI.Visible = (ChangeState_TI.Visible) ? true : state;
					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					Anulado_TI.Visible = (allow) ? state : false;

					break;

				case molAction.ChangeStateContabilizado:

					ChangeState_TI.Visible = (ChangeState_TI.Visible) ? true : state;
					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					Contabilizado_TI.Visible = state;

					if ((AppContext.User != null) && (state))
					{
						Contabilizado_TI.Enabled = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
					}

					break;

				case molAction.ChangeStateEmitido:

					ChangeState_TI.Visible = (ChangeState_TI.Visible) ? true : state;
					ChangeState_TI.Enabled = (ChangeState_TI.Visible) ? true : state;

					Emitido_TI.Visible = state;

					if ((AppContext.User != null) && (state))
					{
						Emitido_TI.Enabled = Library.AutorizationRulesControler.CanEditObject(Library.Resources.SecureItems.ESTADO);
					}

					break;

				case molAction.Print:
					Print_TI.Visible = state;
					Separator02_TI.Visible = state;
					Print_TI.Enabled = state;
					Separator02_TI.Enabled = state;
					break;

				case molAction.Refresh:
					Refresh_TI.Visible = state;
					Separator03_TI.Visible = state;
					break;

				case molAction.ShowDocuments:
					Docs_TI.Visible = state;
					break;

				case molAction.Submit:
					Submit_TI.Enabled = state;
					Submit_BT.Enabled = state;
					break;
			}
		}	

		public override void FormatControls()
		{
			if (_mf_type == ManagerFormType.MFView)
				SetReadOnlyControls(this.Controls);

			base.FormatControls();

			Pie_Panel.SplitterDistance = Pie_Panel.Height - Pie_Panel.Panel2MinSize - Pie_Panel.SplitterWidth;
			ControlsMng.CenterButtons(Pie_Panel.Panel1);
		}

		protected override void FormatControl(Control ctl)
		{
			if ((ctl.Tag != null) && (ctl.Tag.ToString().ToUpper() == Resources.Consts.NO_FORMAT)) return;

			if ((ctl is ToolStrip))
			{
				((ToolStrip)ctl).GripStyle = ToolStripGripStyle.Hidden;
				((ToolStrip)ctl).BackColor = System.Drawing.Color.Gainsboro;
				((ToolStrip)ctl).RenderMode = ToolStripRenderMode.System;

				foreach (ToolStripItem item in ((ToolStrip)ctl).Items)
				{
					if (((ToolStrip)ctl).Items.IndexOf(item) == 0)
						item.Margin = new Padding(10, 1, 0, 2);
					else
						item.Margin = new Padding(0, 1, 0, 2);
				}
			}
			else if ((ctl is SplitContainer))
			{ 
				SplitterPanel panel = ((SplitContainer)ctl).Panel1;
				if ((panel.Controls.Count > 0) && (panel.Controls[0] is ToolStrip))
				{
					((SplitContainer)ctl).BackColor = System.Drawing.Color.Gainsboro;
					((SplitContainer)ctl).FixedPanel = FixedPanel.None;
					((SplitContainer)ctl).IsSplitterFixed = false;
					((SplitContainer)ctl).Panel1MinSize = 36;
					((SplitContainer)ctl).SplitterDistance = 36;
					((SplitContainer)ctl).SplitterWidth = 1;
					((SplitContainer)ctl).FixedPanel = FixedPanel.Panel1;
					((SplitContainer)ctl).IsSplitterFixed = true;
				}
				else
					base.FormatControl(ctl);
			}
			else
				base.FormatControl(ctl);
		}

		protected void SetActionStyle(molAction action, string title, Bitmap image)
		{
			switch (action)
			{
				case molAction.CustomAction1:
					{
						Action1_TI.Image = image;
						Action1_TI.Text = title;
					}
					break;

				case molAction.CustomAction2:
					{
						Action2_TI.Image = image;
						Action2_TI.Text = title;
					}
					break;

				case molAction.CustomAction3:
					{
						Action3_TI.Image = image;
						Action3_TI.Text = title;
					}
					break;

				case molAction.CustomAction4:
					{
						Action4_TI.Image = image;
						Action4_TI.Text = title;
					}
					break;

			}
		}

		protected override void SetReadOnlyControls(Control.ControlCollection controls)
		{
			//Se llama a la función genérica
			base.SetReadOnlyControls(controls);

			Top_TS.Enabled = true;

			SetView(molView.ReadOnly);

			Cancel_BT.Visible = false;

			return;
		}

		protected override void SetView(molView view)
		{
			ViewMode = view;

			switch (ViewMode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
					HideAction(molAction.CustomAction4);
					HideAction(molAction.ShowDocuments);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateAnulado);
					HideAction(molAction.ChangeStateContabilizado);
					HideAction(molAction.ChangeStateEmitido);
					HideAction(molAction.Refresh);
					ShowAction(molAction.Print);
					ShowAction(molAction.Submit);
					ShowAction(molAction.Cancel);

					break;

				case molView.ReadOnly:

					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
					HideAction(molAction.CustomAction4);
					ShowAction(molAction.ShowDocuments);
					HideAction(molAction.Unlock);
					HideAction(molAction.ChangeStateAnulado);
					HideAction(molAction.ChangeStateContabilizado);
					HideAction(molAction.ChangeStateEmitido);
					HideAction(molAction.Refresh);
					ShowAction(molAction.Print);
					ShowAction(molAction.Submit);
					ShowAction(molAction.Cancel);

					break;
			}
		}

		protected void ShowStatusBar(string message)
		{
			this.Height = this.Height + BarraEstado_ST.Height;
			Pie_Panel.Panel2Collapsed = false;
			PanelesV.Panel2MinSize = PanelesV.Panel2.Height + BarraEstado_ST.Height;
			//Paneles2.Height += BarraEstado_ST.Height;
			//Paneles2.Panel2MinSize = BarraEstado_ST.Height;
			Info_SL.Text = message;
		}

		#endregion
		
		#region Buttons

		private void Action1_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction1); }

		private void Action2_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction2); }
		
		private void Action3_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction3); }

		private void Action4_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction4); }

		private void Docs_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ShowDocuments); }

        private void Print_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

		private void Refresh_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Refresh); }

		private void StateAnulado_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ChangeStateAnulado); }

		private void StateContabilizado_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ChangeStateContabilizado); }

		private void StateEmitido_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.ChangeStateEmitido); }

		private void StateUnlock_TI_Click(object sender, EventArgs e) { ExecuteAction(molAction.Unlock); }

		private void Submit_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.Save); }

		private void Cancel_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.Cancel); }

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