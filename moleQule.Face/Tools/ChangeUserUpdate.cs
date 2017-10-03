using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using System.Security;
using System.Security.AccessControl;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Security.Policy;

using moleQule.Library;

namespace moleQule.Face
{
    public partial class ChangeUserUpdate : Form
    {
        private string _user = string.Empty;
        private string _pass = string.Empty;
        private string _domain = string.Empty;

        public string Usuario { get { return _user; } }
        public string Password { get { return _pass; } }
        public string Dominio { get { return _domain; } }

        //Valores por defecto
        public ChangeUserUpdate(string usuario, string pass, string dominio)
        {
            InitializeComponent();
            _user = usuario;
            _pass = pass;
            _domain = dominio;
            Usuario_TB.Text = _user;
            Password_TB.Text = _pass;
            Dominio_TB.Text = _domain;
        }

        private bool ValidateUser()
        {
            try
            {
                /*PrincipalContext adContext = new PrincipalContext(ContextType.Machine);

                using (adContext)
                {
                    if (!adContext.ValidateCredentials(_user, _pass))
                        return false;
                }
                
                if (!PrincipalBase.IsUserAdministrator(_user))
                    return false;
                */
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void Aceptar_BT_Click(object sender, EventArgs e)
        {
            _user = Usuario_TB.Text;
            _pass = Password_TB.Text;
            _domain = Dominio_TB.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancelar_BT_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}