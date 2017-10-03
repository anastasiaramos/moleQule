using System;
using System.Collections.Generic;
using System.Management;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;
using moleQule.Library;
using moleQule.Library.Reports;

namespace moleQule.Face
{
	public partial class MainBaseForm : BaseForm
	{
		#region Attributes

		// This delegate enables asynchronous calls for setting
		// the text property on a TextBox control.
		delegate void Callback();

		/// <summary>
		/// Única instancia de la clase MainBaseForm (Singleton)
		/// </summary>
		protected static MainBaseForm _main;

		protected Type _schema_type = null;

		public Type ActiveSchemaType { get { return _schema_type; } set { _schema_type = value; } }

		CRViewer _report_viewer;

		/// <summary>
		/// Visor para informes
		/// </summary>
		public CRViewer ReportViewer
		{
			get
			{
				if (_report_viewer == null)
					_report_viewer = new CRViewer();

				return _report_viewer;
			}
		}

		#endregion

		#region Business Methods

		public virtual void AutoPilot(bool log)
        {
            //IDE Compatibility
            if (AppContext.User == null) return;

            throw new iQImplementationException("MainBaseForm::AutoPilot"); 
        }

        public virtual void SetFormSkin() 
		{
			WindowState = FormWindowState.Maximized;
			ResumeLayout(true);
		}

        public virtual void LookForUpdate(bool message) { /*throw new iQImplementationException("MainBaseForm::LookForUpdate");*/ }

		public virtual void CreateBackup() { throw new iQImplementationException("MainBaseForm::CreateBackup"); }

		public virtual void RestoreBackup() { throw new iQImplementationException("MainBaseForm::RestoreBackup"); }

		public virtual bool ShowNotifications(bool log) { throw new iQImplementationException("MainBaseForm::ShowNotificacions"); }

		#endregion

		#region Factory Methods

		/// <summary>
		/// Unique MainBaseForm Class Instance
		/// </summary>
        public static MainBaseForm Instance { get { return _main != null ? _main : new MainBaseForm(); } }

		public MainBaseForm()
		{
            //AppControllerBase.InitCulture();

			InitializeComponent();

			// Singleton
			_main = this;

            Globals.Instance.ProgressInfoForm = ProgressInfoForm.Instance;
		}

		#endregion

		#region ApplyAuthorizationRules

		/// <summary>
		/// Recarga la interfaz en función de los permisos
		/// </summary>
		public virtual void Reload() 
		{ 
			ApplyAuthorizationRules();
		}

		#endregion

		#region Login/Logout

		/// <summary>
		/// Función que realiza el login de usuario y carga la clase principal del
		/// programa
		/// </summary>
		/// <remarks>
		/// Esta función utiliza el usuario y password por defecto.
		/// Para utilizar un formulario personalizado de login es necesario sobrecargar
		/// esta función en el MainForm de la aplicación.
		/// </remarks>
		protected virtual void DoLogin()
		{
			if (AppContext.Principal != null) 
				AppContext.Principal.Logout();
			
			SetFormSkin();
#if !DEBUG
			// Cableado para que no pida usuario
			try
            {
				PrincipalBase.Login();
			}
			catch (Exception ex)
			{
				ProgressInfoMng.ShowException(ex, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!AppContext.User.IsAuthenticated)
			{
				ProgressInfoMng.ShowWarning(Resources.Messages.LOGIN_ERROR);
				return;
			}
#endif
		}

        /// <summary>
        /// Carga el schema de datos por defecto. 
        /// </summary>
        /// <remarks>
        /// Esta función carga el schema por defecto.
        /// Para utilizar un formulario personalizado de selección de schema es necesario sobrecargar 
        /// esta función en el MainForm de la aplicación. En caso contrario moleQule asume que va a 
        /// trabajar con un único schema de datos.
        /// Si lo que se quiere es personalizar el schema debemos crear una nueva clase heredera de <see cref="moleQule.Library.Schema"/>.
        /// </remarks>
        public virtual void LoadSchema() { throw new iQImplementationException("LoadSchema"); }

		#endregion

		#region Print Methods

		public void ShowReport(ReportClass report)
		{
			if (report != null)
			{
				ReportViewer.SetReport(report);
				ReportViewer.ShowDialog();
			}
			else
			{
				MessageBox.Show(Resources.Messages.NO_DATA_REPORTS,
								Resources.Messages.EMPTY_REPORT,
								MessageBoxButtons.OK,
								MessageBoxIcon.Exclamation);
			}
		}

		#endregion

		#region Refresh

		public static void RefreshStatusBar()
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
            if (Globals.Instance.StatusBar.InvokeRequired)
			{
				Callback d = new Callback(RefreshStatusBar);
				MainBaseForm.Instance.Invoke(d);
			}
			else
			{
				Globals.Instance.StatusBar.Refresh();
				Application.DoEvents();
			}
		}

		#endregion
        
        #region Events

        /// <summary>
		/// Función que realiza el Login y aplica las reglas de autorización
		/// para el menú superior
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainBaseForm_Load(object sender, EventArgs e)
        {
            try
            {
				if (Csla.ApplicationContext.AuthenticationType == "Windows")
				{
					AppDomain.CurrentDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal);
					SetFormSkin();
				}
				else
				{
					DoLogin();
				}                
#if !DEBUG
				//Ejecutamos el piloto automático con las acciones periodicas
				AutoPilot(true);

				if (SettingsMng.Instance.GetShowAutopilot())
					ShowNotifications(true);
#endif
            }
            catch (Exception ex)
            {
                ProgressInfoMng.Instance.FillUp();
                ProgressInfoMng.Instance.ShowWarningException(ex);
            }
		}

		private void MainBaseForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			AppContext.Principal.CloseSettings();
		}

        #endregion
	}
}