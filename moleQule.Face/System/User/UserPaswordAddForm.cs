using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using moleQule.Library;

namespace moleQule.Face
{
    public partial class UserPasswordAddForm : moleQule.Face.UserPasswordForm
    {
        #region Attributes & Properties

        public new const string ID = "UserPasswordAddForm";
        public new static Type Type { get { return typeof(UserPasswordAddForm); } }

        #endregion

        #region Factory Methods

        public UserPasswordAddForm(bool isModal)
			: this(string.Empty, isModal) {}

		public UserPasswordAddForm(string nombre, bool isModal)
			: base(nombre, isModal)
        {
			InitializeComponent();
        }

        #endregion
    }
}

