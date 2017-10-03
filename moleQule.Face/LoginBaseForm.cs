using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face
{
	public partial class LoginBaseForm : ChildForm
    {

        #region Factory Methods

        public LoginBaseForm()
		{
		  InitializeComponent();
        }

        #endregion

        #region Buttons

        protected virtual void LoginAction() { throw new iQImplementationException("LoginAction"); }

        private void OK_Click(object sender, EventArgs e)
		{
            LoginAction();
		}

		private void Cancel_Click(object sender, EventArgs e)
		{
            MainBaseForm.Instance.Dispose();
        }

        #endregion

        #region Events

        private void LoginForm_Load(object sender, EventArgs e)
		{
		    this.UsernameTextBox.Focus();
        }

        #endregion

  }
}