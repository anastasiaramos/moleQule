using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using moleQule.Library;

namespace moleQule.Face
{
    public partial class UserPasswordForm : moleQule.Face.ChildForm
    {
        #region Attributes & Properties

		public const string ID = "UserPasswordForm";
		public static Type Type { get { return typeof(UserPasswordForm); } }

        protected string _nombre = string.Empty;
		protected string _pass = string.Empty;
		protected string _pass2 = string.Empty;

        public string Nombre { get { return _nombre; } }
        public string Pass { get { return _pass; } }

        #endregion

        #region Factory Methods

		public UserPasswordForm()
			: this(true) { }

        public UserPasswordForm(bool isModal)
			: this(string.Empty, isModal) {}

		public UserPasswordForm(string nombre, bool isModal)
			: base(isModal, null)
        {
            InitializeComponent();
            _nombre = nombre;
            Nombre_TB.Text = nombre;
        }

        #endregion

		#region Actions

		protected virtual bool SaveAction() { return true; }

		#endregion

		#region Buttons

		private void Aceptar_Button_Click(object sender, EventArgs e)
        {
            string pattern = "[A-Z]+";

            if (!Regex.IsMatch(_pass, pattern))
            {
                PgMng.ShowInfoException(Resources.Messages.NOT_UCASE_PASS);
                return;
            }

            pattern = "[a-z]+";

            if (!Regex.IsMatch(_pass, pattern))
            {
				PgMng.ShowInfoException(Resources.Messages.NOT_LCASE_PASS);
                return;
            }

            pattern = "[0-9]+";

            if (!Regex.IsMatch(_pass, pattern))
            {
                PgMng.ShowInfoException(Resources.Messages.NOT_NUMBER_PASS);
                return;
            }

            if (_pass == _pass2)
            {
                if ((_pass != string.Empty) || 
					((_pass == string.Empty) && (DialogResult.Yes == ProgressInfoMng.ShowQuestion(Resources.Messages.PASSWORD_NULL))))
                {
                    DialogResult = DialogResult.OK;
                    //_pass = ClassMD5.getMd5Hash(_pass);
					if (SaveAction()) Close();
                }
            }
            else
            {
                 PgMng.ShowInfoException(Resources.Messages.NOT_EQUAL_PASS);
            }
        }

        private void Cancelar_Button_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region Events

		private void MostrarPassword_CkB_CheckedChanged(object sender, EventArgs e)
		{
			Pass_TB.UseSystemPasswordChar = !MostrarPassword_CkB.Checked;
			Pass2_TB.UseSystemPasswordChar = !MostrarPassword_CkB.Checked;
		}

        private void Nombre_TB_TextChanged(object sender, EventArgs e)
        {
            _nombre = Nombre_TB.Text;
        }

        private void Pass_TB_TextChanged(object sender, EventArgs e)
        {
            _pass = Pass_TB.Text;
        }

        private void Pass2_TB_TextChanged(object sender, EventArgs e)
        {
            _pass2 = Pass2_TB.Text;
        }

        #endregion
    }
}

