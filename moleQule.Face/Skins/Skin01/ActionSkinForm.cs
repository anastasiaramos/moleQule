using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace moleQule.Face.Skin01
{
    public partial class ActionSkinForm : moleQule.Face.ActionBaseForm
    {        
        #region Business Methods

        #endregion

        #region Factory Methods

        public ActionSkinForm()
            : this(true) {}

        public ActionSkinForm(bool isModal)
            : this(isModal, null) {}

        public ActionSkinForm(bool isModal, Form parent)
            : base(isModal, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout & Source

        public override void FormatControls()
        {
			base.FormatControls();

			Source_GB.SendToBack();

			PanelesV.SplitterDistance = PanelesV.Height - PanelesV.Panel2MinSize - PanelesV.SplitterWidth;
			ControlsMng.CenterButtons(PanelesV.Panel2);
        }

        #endregion

        #region Buttons

        private void Aceptar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Submit); }

        private void Cancelar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Cancel); }

        private void Print_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

        #endregion
                
        #region Events

        #endregion
    }
}

