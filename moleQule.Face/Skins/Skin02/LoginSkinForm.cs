using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Face.Resources;
using moleQule.Library;

namespace moleQule.Face.Skin02
{
    /// <summary>
    /// Skin de formulario de login de usuario.
    /// Su aplicación debe incluir un LoginForm hijo de este formulario
    /// </summary>
	public partial class LoginSkinForm : LoginBaseForm
    {
        #region Factory Methods

        public LoginSkinForm()
			: this(MainBaseForm.Instance) {}

		public LoginSkinForm(Form parent)
			: base(parent)
		{
			InitializeComponent();

			Server_TB.Text = SettingsMng.Instance.GetActiveServer();
		}

        #endregion

		#region Layout

		public override void FormatControls()
		{
			base.FormatControls();

			switch (System.Windows.Forms.Application.CurrentCulture.TwoLetterISOLanguageName)
			{
				case "es": Spanish_RB.Checked = true; break;
				case "en": English_RB.Checked = true; break;
				default: English_RB.Checked = true; break;
			}
		}

		#endregion

		#region Buttons

		private void OK_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.Submit); }

		private void Cancel_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.Cancel); }

        #endregion

        #region Events

        private void LoginForm_Load(object sender, EventArgs e)
		{
		    this.UserName_TB.Focus();
        }

		private void Server_CkB_CheckedChanged(object sender, EventArgs e)
		{
			Server_TB.ReadOnly = !Server_CkB.Checked;
			Server_TB.Enabled = Server_CkB.Checked;
		}

		private void Spanish_RB_CheckedChanged(object sender, EventArgs e)
		{
			if (Spanish_RB.Checked)
				PrincipalBase.SetLocale("es-ES");
		}
		
		private void English_RB_CheckedChanged(object sender, EventArgs e)
		{
			if (English_RB.Checked)
				PrincipalBase.SetLocale("en");
		}

        #endregion
    }
}