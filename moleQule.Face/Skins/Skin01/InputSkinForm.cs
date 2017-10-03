using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace moleQule.Face.Skin01
{
    public partial class InputSkinForm : moleQule.Face.InputBaseForm
    {
        #region Factory Methods

        public InputSkinForm()
            : this(true) {}

        public InputSkinForm(bool isModal)
            : this(isModal, null) {}

        public InputSkinForm(bool isModal, Form parent)
            : base(isModal, parent)
        {
            InitializeComponent();
			
			SetView(molView.Normal);
		}

        #endregion

        #region Business Methods

        #endregion

        #region Layout

		protected override void ActivateAction(molAction action, bool state)
		{
			if (EntityType == null) return;

			switch (action)
			{
				case molAction.CustomAction1:
					CustomAction1_MI.Visible = state;
					break;

				case molAction.CustomAction2:
					CustomAction1_MI.Visible = state;
					break;

				case molAction.CustomAction3:
					CustomAction1_MI.Visible = state;
					break;

				case molAction.CustomAction4:
					CustomAction1_MI.Visible = state;
					break;

				case molAction.Refresh:
					Refresh_MI.Visible = state;
					Separator1_MI.Visible = state;
					break;
			}
		}	

        public override void FormatControls()
        {
            base.FormatControls();

			PanelesV.SplitterDistance = PanelesV.Height - PanelesV.Panel2MinSize - PanelesV.SplitterWidth;
			ControlsMng.CenterButtons(PanelesV.Panel2);
        }

		protected void SetActionStyle(molAction action, string title, Bitmap image)
		{
			switch (action)
			{
				case molAction.CustomAction1:
					{
						CustomAction1_MI.Image = (image != null) ? image : CustomAction1_MI.Image;
						CustomAction1_MI.Text = title;
					}
					break;

				case molAction.CustomAction2:
					{
						CustomAction2_MI.Image = (image != null) ? image : CustomAction2_MI.Image; 
						CustomAction2_MI.Text = title;
					}
					break;

				case molAction.CustomAction3:
					{
						CustomAction3_MI.Image = (image != null) ? image : CustomAction3_MI.Image; 
						CustomAction3_MI.Text = title;
					}
					break;

				case molAction.CustomAction4:
					{
						CustomAction4_MI.Image = (image != null) ? image : CustomAction4_MI.Image;
						CustomAction4_MI.Text = title;
					}
					break;
			}
		}
		
		protected override void SetView(molView view)
		{
			ViewMode = view;

			switch (ViewMode)
			{
				case molView.Select:
				case molView.Normal:
				case molView.Enbebbed:

					HideAction(molAction.Refresh);
					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
					HideAction(molAction.CustomAction4);

					break;

				case molView.ReadOnly:

					HideAction(molAction.Refresh);
					HideAction(molAction.CustomAction1);
					HideAction(molAction.CustomAction2);
					HideAction(molAction.CustomAction3);
					HideAction(molAction.CustomAction4);

					break;
			}
		}

        #endregion

        #region Buttons

        private void Aceptar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Submit); }

        private void Cancelar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Cancel); }

		private void CustomAction1_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction1); }
		private void CustomAction2_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction2); }
		private void CustomAction3_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction3); }
		private void CustomAction4_MI_Click(object sender, EventArgs e) { ExecuteAction(molAction.CustomAction4); }

        #endregion

        #region Events

        #endregion
    }
}

