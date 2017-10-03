using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using moleQule.Library;

namespace moleQule.Face
{
    public partial class UserPasswordEditForm : moleQule.Face.UserPasswordForm
    {
        #region Attributes & Properties

		public new const string ID = "UserPasswordEditForm";
        public new static Type Type { get { return typeof(UserPasswordEditForm); } }

		protected User _entity; 

        #endregion

        #region Factory Methods

        public UserPasswordEditForm(bool isModal)
			: this(null, isModal) {}

        public UserPasswordEditForm(User user, bool isModal)
			: base((user != null) ? user.Name : string.Empty, isModal)
        {
			InitializeComponent();

			_entity = user;

			Nombre_TB.Enabled = false;
			Nombre_TB.ReadOnly = true;
        }

		/// <summary>
		/// Guarda en la bd el objeto actual
		/// </summary>
		protected bool SaveObject()
		{
			PgMng.Reset(3, 1, Resources.Messages.SAVING);

            User temp = _entity.Clone();
            temp.SessionCode = User.OpenSession();
			temp.BeginTransaction();
			PgMng.Grow();

			// do the save
			try
			{
				_entity = temp.Save();
				PgMng.Grow(string.Empty, "temp.Save()");

				return true;
			}
			catch (iQValidationException ex)
			{
				PgMng.ShowInfoException(ex);
				return false;
			}
			catch (Exception ex)
			{
				PgMng.ShowErrorException(ex);
				return false;
			}
			finally
			{
				PgMng.FillUp();
				temp.CloseSession();
			}
		}

        #endregion

        #region Actions

		protected override bool SaveAction()
		{
			_entity.Name = Nombre;
			_entity.PlainPwd = Pass;

			return SaveObject();
		}

        #endregion

        #region Events

        #endregion
    }
}

