using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using moleQule.Face;
using moleQule.Library;

namespace moleQule.Face
{
    /// <summary>
    /// Formulario base para login de usuarios.
    /// Su aplicación debe incluir un LoginForm hijo del skin de este formulario
    /// </summary>
	public partial class LoginBaseForm : ChildForm, IBackGroundLauncher
    {
        #region Factory Methods

		public LoginBaseForm()
			: this(MainBaseForm.Instance) {}

        public LoginBaseForm(Form parent)
            : base(true, parent)
		{
            InitializeComponent();
        }

        #endregion

		#region IBackGroundLauncher

		protected new enum BackJob { Login }
		protected new BackJob _back_job = BackJob.Login;

		/// <summary>
		/// La llama el backgroundworker para ejecutar codigo en segundo plano
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public new void BackGroundJob(BackgroundWorker bk)
		{
			try
			{
				switch (_back_job)
				{
					case BackJob.Login:
						BkLogin(bk);
						break;

					default:
						base.BackGroundJob(bk);
						break;
				}
			}
			catch (Exception ex)
			{
				CancelBackGroundJob();
				PgMng.ShowInfoException(ex);
			}
		}

		private void BkLogin(BackgroundWorker bk)
		{
			try
			{
				LoginAction();
			}
			catch (Exception ex)
			{
				PgMng.ShowInfoException(ex);
			}
		}

		#endregion

        #region Actions

        protected override void SubmitAction() 
		{
			_back_job = BackJob.Login;

			PgMng.Reset(4, 1, Library.Resources.Messages.VALIDATING_USER, this);

			//PgMng.StartBackJob(this);

			//Thread ActionThread = new Thread(LoginAction);
			//ActionThread.Start();

			LoginAction();
		}

        /// <summary>
        /// Función que implementa el login. Es llamada por el botón OK
        /// </summary>
        /// <remarks>
        /// Debe realizar las tareas de login de usuario mediante el objeto Principal que gestiona el usuario activo.
        /// Dicho objeto obtiene una lista de schemas accesibles para el usuario indicado
        /// en el formulario.
        /// </remarks>
        protected virtual void LoginAction() { throw new iQImplementationException("LoginBaseForm::LoginAction()"); }

		protected override void CancelAction()
		{
			MainBaseForm.Instance.Dispose();
			base.CancelAction();
		}

        #endregion
    }
}