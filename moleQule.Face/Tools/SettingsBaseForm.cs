using System;
using System.Text;
using System.Windows.Forms;

using moleQule.Library;
using moleQule.Face.Resources;

namespace moleQule.Face
{
	public partial class SettingsBaseForm : Skin01.InputSkinForm
	{
        #region Attributes & Properties

		protected override int BarSteps { get { return base.BarSteps + 3; } }

        protected SchemaSettings SchemaSettings { get { return SettingsMng.Instance.SchemaSettings; } }
        protected UserSettings UserSettings { get { return SettingsMng.Instance.UserSettings; } }

        #endregion

        #region Factory Methods

        public SettingsBaseForm()
            : this(null) {}

        public SettingsBaseForm(Form parent)
            : base(true, parent)
        {
            InitializeComponent();
        }

        #endregion

        #region Layout & Source

        protected override void RefreshMainData()
        {
			LoadSettings();
			PgMng.Grow();
            
			ShowActiveCulture();
			PgMng.Grow();
			
			ShowServers();
			PgMng.Grow();
        }

        #endregion

		#region Business Methods

		public const string ID = "SettingsBaseForm";
		public static Type Type { get { return typeof(SettingsBaseForm); } }

		/// <summary>
		/// Cambia el Culture de la aplicación al especificado
		/// </summary>
		/// <param name="culture_code">Codigo del Culture (es-EN, en, fr, ...)</param>
		public void SetCulture()
		{
			try
			{
				string culture;

				for (int i = 0; i < Idioma_GB.Controls.Count; i++)
					if (((RadioButton)Idioma_GB.Controls[i]).Checked)
					{
						culture = Idioma_GB.Controls[i].Tag.ToString();
						SettingsMng.Instance.SetUserCulture(culture);
						break;
					}
			}
			catch (Exception ex)
			{
				ProgressInfoMng.ShowError(ex);
			}
		}

		private void ShowActiveCulture()
		{
			if (SettingsMng.Instance.GetUserCulture() == System.Globalization.CultureInfo.InstalledUICulture.Name)
			{
				Predeterminado_RB.Checked = true;
			}
			else
			{
				for (int i = 0; i < Idioma_GB.Controls.Count; i++)
					((RadioButton)Idioma_GB.Controls[i]).Checked = (Idioma_GB.Controls[i].Tag.ToString() == SettingsMng.Instance.GetUserCulture());
			}
		}

		protected virtual void SetServers()
		{
			SettingsMng.Instance.SetLANServer(LANHost_TB.Text);
			SettingsMng.Instance.SetWANServer(WANHost_TB.Text);
			SettingsMng.Instance.SetFilesServer(FilesHost_TB.Text);

			SettingsMng.Instance.SetSMTPHost(SMTPHost_TB.Text);
			try { SettingsMng.Instance.SetSMTPPort(Convert.ToInt32(SMTPPort_TB.Text)); } catch { }
			SettingsMng.Instance.SetSMTPEnableSSL(SMTPEnableSSL_CkB.Checked);
			SettingsMng.Instance.SetSMTPUser(SMTPUser_TB.Text);
			SettingsMng.Instance.SetSMTPPwd(SMTPPwd_TB.Text);
			SettingsMng.Instance.SetSMTPMail(SMTPMail_TB.Text);

			SettingsMng.Instance.SetPDFPrintsFolder(PDFPrintsFolder_TB.Text);
		}

		protected virtual void ShowServers()
		{
			LANHost_TB.Text = SettingsMng.Instance.GetLANServer();
			WANHost_TB.Text = SettingsMng.Instance.GetWANServer();
			FilesHost_TB.Text = SettingsMng.Instance.GetFilesServer();

			SMTPHost_TB.Text = SettingsMng.Instance.GetSMTPHost();
			SMTPPort_TB.Text = (SettingsMng.Instance.GetSMTPPort() != 0) ? SettingsMng.Instance.GetSMTPPort().ToString() : string.Empty;
			SMTPEnableSSL_CkB.Checked = SettingsMng.Instance.GetSMTPEnableSSL();
			SMTPUser_TB.Text = SettingsMng.Instance.GetSMTPUser();
			SMTPPwd_TB.Text = SettingsMng.Instance.GetSMTPPwd();
			SMTPMail_TB.Text = SettingsMng.Instance.GetSMTPMail();

			PDFPrintsFolder_TB.Text = SettingsMng.Instance.GetPDFPrintsFolder();
		}

		protected virtual void LoadSettings() 
		{
			//BACKUPS
			try { BackupsDays_TB.Text = SettingsMng.Instance.GetBackupsDays().ToString(); }
			catch { }
			try { BackupsHour_DTP.Value = SettingsMng.Instance.GetBackupsHour(); }
			catch { }
			try { BackupsLastDate_DTP.Value = SettingsMng.Instance.GetBackupsLastDate(); }
			catch { }

			//LAYOUT
			try { FormatGrids_CkB.Checked = SettingsMng.Instance.GetFormatGridsSetting(); } catch { }
			try { ShowNullRecords_CkB.Checked = SettingsMng.Instance.GetShowNullRecordsSetting(); } catch { }
		}

		protected virtual void SaveSettings() 
		{
			//BAKUPS
			try { SettingsMng.Instance.SetBackupsDays(Convert.ToInt32(BackupsDays_TB.Text)); }
			catch { }
			try { SettingsMng.Instance.SetBackupsHour(BackupsHour_DTP.Value); }
			catch { }
			try { SettingsMng.Instance.SetBackupsLastDate(BackupsLastDate_DTP.Value); }
			catch { }

			//LAYOUT
			SettingsMng.Instance.SetFormatGridsSetting(FormatGrids_CkB.Checked);
			SettingsMng.Instance.SetShowNullRecordsSetting(ShowNullRecords_CkB.Checked);
		}

		#endregion

		#region Actions
        
        protected override void SubmitAction()
        {
			SaveSettings();
			SetServers();

            SettingsMng.Instance.SaveSettings();
            _action_result = DialogResult.OK;
        }

        #endregion

		#region Buttons

		private void PDFPrinstFolder_BT_Click(object sender, EventArgs e)
		{
			Browser.ShowDialog();
			PDFPrintsFolder_TB.Text = Browser.SelectedPath;
		}

		#endregion

		#region Events

        private void Idioma_GB_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetCulture();
        }

		private void MostrarPassword_CkB_CheckedChanged(object sender, EventArgs e)
		{
			SMTPPwd_TB.UseSystemPasswordChar = !MostrarPassword_CkB.Checked;
			SMTPPwd_TB.UseSystemPasswordChar = !MostrarPassword_CkB.Checked;
		}

		private void Predeterminado_RB_CheckedChanged(object sender, EventArgs e)
		{
			if (Predeterminado_RB.Checked)
				PrincipalBase.SetLocale("system");
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

