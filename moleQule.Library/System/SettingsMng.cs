using System;
using System.Reflection;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Globalization;

using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using Infralution.Localization;

using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	public class SettingsMng
	{
		#region Application Settings

		protected ApplicationSettings _app_settings;
		
		public ApplicationSettings AppSettings { get { return _app_settings; } }

		public void SetFilesServer(string server)
		{
			_app_settings.SetValue(Properties.Settings.Default.SETTING_NAME_FILES_HOST, server);
		}
		public string GetFilesServer()
		{
			string value = _app_settings.GetValue(Properties.Settings.Default.SETTING_NAME_FILES_HOST);
			return (value != string.Empty) ? value : nHManager.Instance.Host;
		}

		public static string GetAdminUser() { return Properties.Settings.Default.ADMIN_USER; }
		public static string GetDefaultUser() { return Properties.Settings.Default.DB_USER; }
		public static string GetServicesUser() { return Properties.Settings.Default.SERVICES_USER; }
		public static string GetSuperUser() { return Properties.Settings.Default.SUPERUSER; }

		public static string GetServicesPassword() { return Properties.Settings.Default.SERVICES_PASSWORD; }

		public string GetPostgreSQLVersion()
		{
			try { return _app_settings.GetValue(Properties.Settings.Default.SETTING_NAME_POSTGRESQL_VERSION); }
			catch { return string.Empty; }
		}
		public void SetPostgreSQLVersion(string value)
		{
			_app_settings.SetValue(Properties.Settings.Default.SETTING_NAME_POSTGRESQL_VERSION, value);
			SaveSettings();
		}

		#endregion

		#region Temp Application Settings

		const int LAST_USER = 1;
		const int DB_PASSWORD = 2;
		const int FTP_USER_VARIABLE = 3;
		const int FTP_PWD_VARIABLE = 4;
		const int FTP_HOST_VARIABLE = 5;
		const int DB_VERSION = 6;
		const int DB_NAME = 7;
		const int APPLICATION_TITLE = 8;
		const int ACTIVE_HOST = 9;
		const int LAN_HOST = 10;
		const int WAN_HOST = 11;
		const int DB_USER = 12;
		const int APPLICATION_NAME = 13;
		const int APPLICATION_VERSION = 14;
		const int MOLEQULE_DB_VERSION = 15;
		const int APPLICATION_TYPE = 16;
		const int DATA_PATH = 17;
		const int NH_CONFIG_FILES_PATH = 18;
		const int NH_SERVICES_CONFIG_FILES_PATH = 19;
		const int DLL_FILES_PATH = 20;
		const int SERVICES_DLL_FILES_PATH = 21;
		const int API_URL_MONITOR = 22;
		const int API_SERVICE_MONITOR = 23;
		const int API_ACTION_MONITOR = 24;
        const int CURRENT_APP_VERSION = 25;
        const int BACKUPS_PATH = 26;

		protected static Dictionary<int, string> _app_temp_settings = new Dictionary<int, String>();

		public string GetActiveServer() { return (_app_temp_settings[ACTIVE_HOST] != string.Empty) ? _app_temp_settings[ACTIVE_HOST] : nHManager.Instance.Host;	}
		public string GetAPIActionMonitor() { return _app_temp_settings[API_ACTION_MONITOR]; }
		public string GetAPIServiceMonitor() { return _app_temp_settings[API_SERVICE_MONITOR]; }
		public string GetAPIUrlMonitor() { return _app_temp_settings[API_URL_MONITOR]; }
		public string GetApplicationName() { return _app_temp_settings[APPLICATION_NAME]; }
		public string GetApplicationTitle() { return _app_temp_settings[APPLICATION_TITLE]; }
		public EAppType GetApplicationType() { return (EAppType)Convert.ToInt32(_app_temp_settings[APPLICATION_TYPE]); }
		public string GetApplicationVersion() { return _app_temp_settings[APPLICATION_VERSION]; }
        public string GetBackupsPath() { return _app_temp_settings[BACKUPS_PATH]; }
        public string GetCurrentApplicationVersion() { return _app_temp_settings[CURRENT_APP_VERSION]; }
		public string GetDataPath() { return _app_temp_settings[DATA_PATH]; }
		public string GetDBName() { return _app_temp_settings[DB_NAME]; }
		public string GetDBUser() { return _app_temp_settings[DB_USER]; }
		public string GetDBPassword() { return _app_temp_settings[DB_PASSWORD]; }
		public string GetDBVersion() { return _app_temp_settings[DB_VERSION]; }
		public string GetDLLRelativePath()
		{
			switch (Library.SettingsMng.Instance.GetApplicationType())
			{
				case EAppType.Web:
					return Path.Combine(AppContext.StartUpPath, _app_temp_settings[DLL_FILES_PATH]);

				case EAppType.Desktop:
					return Path.Combine(AppContext.StartUpPath, _app_temp_settings[DLL_FILES_PATH]);

				case EAppType.Service:
					return Path.Combine(AppContext.StartUpPath, _app_temp_settings[SERVICES_DLL_FILES_PATH]);

				default: return string.Empty;
			}
		}
		public string GetMoleQuleDBVersion() { return _app_temp_settings[MOLEQULE_DB_VERSION]; }
		public string GetLANServer() { return (_app_temp_settings[LAN_HOST] != string.Empty) ? _app_temp_settings[LAN_HOST] : nHManager.Instance.Host; }
		public string GetLastUser() { return _app_temp_settings[LAST_USER]; }
		public string GetNHConfigFileRelativePath(string app = null)
		{
			string nHpath = (string.IsNullOrEmpty(app))
								? Resources.Paths.NH_MAIN_CONFIG_FILE
								: String.Format(Resources.Paths.NH_MAIN_CONFIG_FILE, app);

			switch (Library.SettingsMng.Instance.GetApplicationType())
			{
				case EAppType.Web:
					return Path.Combine(AppContext.StartUpPath, _app_temp_settings[NH_CONFIG_FILES_PATH], nHpath);

				case EAppType.Desktop:
					return Path.Combine(AppContext.StartUpPath, _app_temp_settings[NH_CONFIG_FILES_PATH], nHpath);

				case EAppType.Service:
					return Path.Combine(AppContext.StartUpPath, _app_temp_settings[NH_SERVICES_CONFIG_FILES_PATH], nHpath);

				default: return string.Empty;
			}
		}
		public string GetVersion()
		{
			Assembly ensamblado = System.Reflection.Assembly.GetExecutingAssembly();
			Version ver = ensamblado.GetName().Version;
			return ver.ToString();
		}
		public string GetWANServer() { return (_app_temp_settings[WAN_HOST] != string.Empty) ? _app_temp_settings[WAN_HOST] : nHManager.Instance.Host; }		

		public void SetActiveServer(string server) { _app_temp_settings[ACTIVE_HOST] = server; }
		public void SetAPIActionMonitor(string value) { _app_temp_settings[API_ACTION_MONITOR] = value; }
		public void SetAPIServiceMonitor(string value) { _app_temp_settings[API_SERVICE_MONITOR] = value; }
		public void SetAPIUrlMonitor(string value) { _app_temp_settings[API_URL_MONITOR] = value; }
		public void SetApplicationName(string value) { _app_temp_settings[APPLICATION_NAME] = value; }
		public void SetApplicationTitle(string value) { _app_temp_settings[APPLICATION_TITLE] = value; }
		public void SetApplicationType(EAppType value) { _app_temp_settings[APPLICATION_TYPE] = ((int)value).ToString(); }
		public void SetApplicationVersion(string value) { _app_temp_settings[APPLICATION_VERSION] = value; }
        public void SetBackupsPath(string value) { _app_temp_settings[BACKUPS_PATH] = value; }
        public void SetCurrentApplicationVersion(string value) { _app_temp_settings[CURRENT_APP_VERSION] = value; }
		public void SetDataPath(string value) { _app_temp_settings[DATA_PATH] = value; }
		public void SetDBName(string value) { _app_temp_settings[DB_NAME] = value; }
		public void SetDBPassword(string value) { _app_temp_settings[DB_PASSWORD] = value; }		
		public void SetDBUser(string value) { _app_temp_settings[DB_USER] = value; }
		public void SetDBVersion(string value) { _app_temp_settings[DB_VERSION] = value; }
		public void SetDLLFilesApplicationRelativePath(string value) { _app_temp_settings[DLL_FILES_PATH] = value; }
		public void SetDLLFilesServicesRelativePath(string value) { _app_temp_settings[SERVICES_DLL_FILES_PATH] = value; }
		public void SetLANServer(string value) { _app_temp_settings[LAN_HOST] = value; }
		public void SetLastUser(string value) { _app_temp_settings[LAST_USER] = value; }
		public void SetNHConfigApplicationRelativePath(string value) { _app_temp_settings[NH_CONFIG_FILES_PATH] = value; }
		public void SetNHConfigServicesRelativePath(string value) { _app_temp_settings[NH_SERVICES_CONFIG_FILES_PATH] = value; }
		public void SetWANServer(string server) { _app_temp_settings[WAN_HOST] = server; }

		#endregion

		#region Schema Settings

		protected SchemaSettings _schema_settings;

		public SchemaSettings SchemaSettings { get { return _schema_settings; } }

		public void SetContact(EContact contact, string value)
		{
			switch (contact)
			{
				case EContact.Admin:
					_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_ADMIN_CONTACT, value.ToString());
					break;

				case EContact.CustomerService:
					_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_CLIENTS_CONTACT, value.ToString());
					break;

				case EContact.Support:
					_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SUPPORT_CONTACT, value.ToString());
					break;
			}
		}
		public string GetContact(EContact contact)
		{
			switch (contact)
			{
				case EContact.Admin:
					return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_ADMIN_CONTACT);

				case EContact.CustomerService:
					return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_CLIENTS_CONTACT);

				case EContact.Support:
					return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SUPPORT_CONTACT);

				default: return string.Empty;
			}
		}

		public void SetEmail(EContact contact, string value)
		{
			switch (contact)
			{
				case EContact.Admin:
					_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_ADMIN_EMAIL, value.ToString());
					break;

				case EContact.CustomerService:
					_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_CLIENTS_EMAIL, value.ToString());
					break;

				case EContact.Support:
					_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SUPPORT_EMAIL, value.ToString());
					break;
			}
		}
		public string GetEmail(EContact contact)
		{
			switch (contact)
			{
				case EContact.Admin:
					return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_ADMIN_EMAIL);

				case EContact.CustomerService:
					return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_CLIENTS_EMAIL);

				case EContact.Support:
					return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SUPPORT_EMAIL);

				default: return string.Empty;
			}
		}

		public void SetMobilePhoneNumber(EContact contact, string value)
		{
			switch (contact)
			{
				case EContact.Admin:
					_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_ADMIN_MOBILE_PHONE, value.ToString());
					break;

				case EContact.CustomerService:
					_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_CLIENTS_MOBILE_PHONE, value.ToString());
					break;

				case EContact.Support:
					_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SUPPORT_MOBILE_PHONE, value.ToString());
					break;
			}
		}
		public string GetMobilePhoneNumber(EContact contact)
		{
			switch (contact)
			{
				case EContact.Admin:
					return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_ADMIN_MOBILE_PHONE);

				case EContact.CustomerService:
					return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_CLIENTS_MOBILE_PHONE);

				case EContact.Support:
					return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SUPPORT_MOBILE_PHONE);

				default: return string.Empty;
			}
		}

		public string GetLinkSeed() { return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_LINK_SEED); }

		//BACKUPS
		public void SetBackupsHour(DateTime value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_BACKUPS_HOUR, value.ToString("dd/MM/yyyy HH:mm"));
		}
		public DateTime GetBackupsHour()
		{
			DateTime value = DateTime.Parse(_schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_BACKUPS_HOUR), CultureInfo.CreateSpecificCulture("es-ES"));
			return (value != DateTime.MinValue) ? value : DateTime.Today;
		}

		public void SetBackupsDays(int value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_BACKUPS_DAYS, value.ToString());
		}
		public int GetBackupsDays()
		{
			return Convert.ToInt32(_schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_BACKUPS_DAYS));
		}

		public void SetBackupsLastDate(DateTime value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_BACKUPS_LAST_DATE, value.ToString("dd/MM/yyyy HH:mm"));
		}
		public DateTime GetBackupsLastDate()
		{
			DateTime value = DateTime.Parse(_schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_BACKUPS_LAST_DATE), CultureInfo.CreateSpecificCulture("es-ES"));
			return (value != DateTime.MinValue) ? value : DateTime.Today;
		}

		//SMS GATEWAY
		public void SetSMSGatewayAccount(string value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_ACCOUNT, value);
		}
		public string GetSMSGatewayAccount()
		{
			return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_ACCOUNT);
		}

		public void SetSMSGatewayCode(long value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_CODE, value.ToString());
		}
		public long GetSMSGatewayCode()
		{
			return Convert.ToInt64(_schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_CODE));
		}

		public void SetSMSGatewayName(string value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_NAME, value);
		}
		public string GetSMSGatewayName()
		{
			return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_NAME);
		}

		public void SetSMSGatewayPwd(string value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_PWD, value);
		}
		public string GetSMSGatewayPwd()
		{
			return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_PWD);
		}

		public void SetSMSGatewayUser(string value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_USER, value);
		}
		public string GetSMSGatewayUser()
		{
			return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_USER);
		}

		//SMTP & EMAIL
		public void SetSchemaSMTPHost(string value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_HOST, value);
		}
		public string GetSchemaSMTPHost()
		{
			return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_HOST);
		}

		public void SetSchemaSMTPPort(int value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_PORT, value.ToString());
		}
		public int GetSchemaSMTPPort()
		{
			try { return Convert.ToInt32(_schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_PORT)); }
			catch { return 0; }
		}

		public void SetSchemaSMTPEnableSSL(bool value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_ENABLE_SSL, value.ToString());
		}
		public bool GetSchemaSMTPEnableSSL()
		{
			try { return Convert.ToBoolean(_schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_ENABLE_SSL)); }
			catch { return false; }
		}

		public void SetSchemaSMTPUser(string value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_USER, value);
		}
		public string GetSchemaSMTPUser()
		{
			return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_USER);
		}

		public void SetSchemaSMTPPwd(string value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_PWD, value);
		}
		public string GetSchemaSMTPPwd()
		{
			return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_PWD);
		}

		public void SetSchemaSMTPMail(string value)
		{
			_schema_settings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_EMAIL, value);
		}
		public string GetSchemaSMTPMail()
		{
			return _schema_settings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_EMAIL);
		}

		#endregion

		#region User Settings

		protected static Dictionary<string, UserSettings> _user_settings = new Dictionary<string, UserSettings>();

		public UserSettings UserSettings 
		{ 
			get 
			{
				try
				{
					return _user_settings[AppContext.User.Name];
				}
				catch
				{
					return null;
				}
			} 
		}

		public long GetDefaultSchema()
		{
			try { return Convert.ToInt64(UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_DEFAULT_SCHEMA)); }
			catch { return 0; }
		}
		public void SetDefaultSchema(long oid)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_DEFAULT_SCHEMA, oid.ToString());
			SaveSettings();
		}

		public void SetPDFPrintsFolder(string value)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_PDF_PRINTS_FOLDER, value);
		}
		public string GetPDFPrintsFolder()
		{
			string folder = UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_PDF_PRINTS_FOLDER);
			return (((folder == string.Empty) ? false : Directory.Exists(folder)) ? folder : System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop));
		}

		public void SetShowAutopilot(bool show)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_SHOW_AUTOPILOT, show.ToString());
		}
		public bool GetShowAutopilot()
		{
			try { return Convert.ToBoolean(UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_SHOW_AUTOPILOT)); }
			catch { return false; }
		}

		public void SetUserCulture(string cultureCode)
		{
			string value = (cultureCode != string.Empty) ? cultureCode : CultureInfo.InstalledUICulture.Name;
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_USER_CULTURE, value);
		}
		public string GetUserCulture()
		{
			return UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_USER_CULTURE);
		}

		public void SetFormatGridsSetting(bool activate)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_FORMAT_GRIDS, activate.ToString());
		}
		public bool GetFormatGridsSetting()
		{
			try { return Convert.ToBoolean(UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_FORMAT_GRIDS)); }
			catch { return false; }
		}

		public void SetShowNullRecordsSetting(bool activate)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_SHOW_NULL_RECORDS, activate.ToString());
		}
		public bool GetShowNullRecordsSetting()
		{
			try { return Convert.ToBoolean(UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_SHOW_NULL_RECORDS)); }
			catch { return false; }
		}

		public void SetSMTPHost(string server)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_HOST, server);
		}
		public string GetSMTPHost()
		{
			return UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_HOST);
		}

		public void SetSMTPPort(int server)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_PORT, server.ToString());
		}
		public int GetSMTPPort()
		{
			try { return Convert.ToInt32(UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_PORT)); }
			catch { return 0; }
		}

		public void SetSMTPEnableSSL(bool value)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_ENABLE_SSL, value.ToString());
		}
		public bool GetSMTPEnableSSL()
		{
			try { return Convert.ToBoolean(UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_ENABLE_SSL)); }
			catch { return false; }
		}

		public void SetSMTPUser(string server)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_USER, server);
		}
		public string GetSMTPUser()
		{
			return UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_USER);
		}

		public void SetSMTPPwd(string server)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_PWD, server);
		}
		public string GetSMTPPwd()
		{
			return UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_PWD);
		}

		public void SetSMTPMail(string server)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_SMTP_EMAIL, server);
		}
		public string GetSMTPMail()
		{
			return UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_SMTP_EMAIL);
		}

		public bool GetUseDefaultPrinter()
		{
			return Convert.ToBoolean(UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_USE_DEFAULT_PRINTER));
		}
		public void SetUseDefaultPrinter(bool value)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_USE_DEFAULT_PRINTER, value.ToString());
		}

		public string GetDefaultPrinter()
		{
			return UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_DEFAULT_PRINTER);
		}
		public void SetDefaultPrinter(string value)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_DEFAULT_PRINTER, value.ToString());
		}

		public int GetDefaultNCopies()
		{
			return Convert.ToInt32(UserSettings.GetValue(Properties.Settings.Default.SETTING_NAME_DEFAULT_N_COPIES));
		}
		public void SetDefaultNCopies(int value)
		{
			UserSettings.SetValue(Properties.Settings.Default.SETTING_NAME_DEFAULT_N_COPIES, value.ToString());
		}

		#endregion

		#region Factory Methods

		protected static SettingsMng _main;

		public static SettingsMng Instance { get { return (_main != null) ? _main : new SettingsMng(); } }

		public SettingsMng()
		{
			_main = this;

			_app_temp_settings.Add(APPLICATION_TITLE, "moleQule 2.0");
			_app_temp_settings.Add(APPLICATION_NAME, "moleQule 2.0");
			_app_temp_settings.Add(APPLICATION_VERSION, "0.0.0.0");
			_app_temp_settings.Add(ACTIVE_HOST, "localhost");
            _app_temp_settings.Add(BACKUPS_PATH, string.Empty);
			_app_temp_settings.Add(DB_VERSION, "4.6.0.0");
			_app_temp_settings.Add(DB_NAME, Properties.Settings.Default.DB_NAME);
			_app_temp_settings.Add(DB_PASSWORD, Properties.Settings.Default.DB_PWD);
			_app_temp_settings.Add(DB_USER, Properties.Settings.Default.DB_USER);
			_app_temp_settings.Add(LAST_USER, "Admin");
            _app_temp_settings.Add(MOLEQULE_DB_VERSION, "6.3.4.2");
			_app_temp_settings.Add(APPLICATION_TYPE, ((int)EAppType.Desktop).ToString());
            _app_temp_settings.Add(LAN_HOST, "localhost");
            _app_temp_settings.Add(WAN_HOST, "localhost");
			_app_temp_settings.Add(DATA_PATH, Resources.Paths.DATA_PATH);
			_app_temp_settings.Add(NH_CONFIG_FILES_PATH, Resources.Paths.NH_CONFIG_FILES);
			_app_temp_settings.Add(NH_SERVICES_CONFIG_FILES_PATH, Resources.Paths.NH_SERVICES_CONFIG_FILES);
			_app_temp_settings.Add(DLL_FILES_PATH, Resources.Paths.DLL_FILES_PATH);
			_app_temp_settings.Add(SERVICES_DLL_FILES_PATH, Resources.Paths.SERVICES_DLL_FILES_PATH);
			_app_temp_settings.Add(API_URL_MONITOR, Properties.Settings.Default.API_URL_MONITOR);
			_app_temp_settings.Add(API_ACTION_MONITOR, Properties.Settings.Default.API_ACTION_MONITOR);
			_app_temp_settings.Add(API_SERVICE_MONITOR, Properties.Settings.Default.API_SERVICE_MONITOR);
            _app_temp_settings.Add(CURRENT_APP_VERSION, "0.0.0.0");
		}

		public void CloseSettings(User user)
		{
			if (_app_settings != null) { _app_settings = null; }
			if (_schema_settings != null) { _schema_settings = null; }
			if (_user_settings.ContainsKey(user.Name))  {  _user_settings.Remove(user.Name); }	
		}

		protected virtual void LoadDefaultSettings()
		{
			if ((GetPDFPrintsFolder() == string.Empty) || !Directory.Exists(GetPDFPrintsFolder()))
				SetPDFPrintsFolder(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
		}

		public void LoadSettings(User user)
		{
			try
			{
				CloseSettings(user);

				_app_settings = ApplicationSettings.GetList();
				_app_settings.CloseSession();

				_schema_settings = Library.SchemaSettings.GetList();
				_schema_settings.CloseSession();

				if (!_user_settings.ContainsKey(user.Name))
				{
					_user_settings.Add(user.Name, UserSettings.GetListByUser(user));
					UserSettings.CloseSession();
				}

				LoadDefaultSettings();
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		public void LoadSchemaSettings()
		{
			try
			{
				if (_schema_settings != null) return;

				_schema_settings = Library.SchemaSettings.GetList();
				_schema_settings.CloseSession();
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		public virtual void SaveSettings()
		{
			if (_app_settings != null)
			{
				_app_settings.OpenNewSession();
				_app_settings.BeginTransaction();
				_app_settings.Save();
				_app_settings.CloseSession();
			}

			if (_schema_settings != null)
			{
				_schema_settings.OpenNewSession();
				_schema_settings.BeginTransaction();
				_schema_settings.Save();
				_schema_settings.CloseSession();
			}

			if (_user_settings != null)
			{
				UserSettings.OpenNewSession();
				UserSettings.BeginTransaction();
				UserSettings.Save();
				UserSettings.CloseSession();
			}

			Properties.Settings.Default.Save();

			EMailClient.Instance.LoadSMTPConfig();
		}

		public void SaveSchemaSettings()
		{
			try
			{
				if (_schema_settings != null)
				{
					_schema_settings.OpenNewSession();
					_schema_settings.BeginTransaction();
					_schema_settings.Save();
					_schema_settings.CloseSession();
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

		#region Business Methods

		/*internal static void SetEnvironment()
		{
			/*EEnvironment environment = 0;
#if DEBUG
			environment = EEnvironment.Development;			
#endif
#if DEVELOP
			environment = EEnvironment.Test;
#endif
#if STAGING
			environment = EEnvironment.Staging;
#endif
#if RELEASE
			environment = EEnvironment.Release;
#endif
			SetEnvironment(environment);
		}

		public static void SetEnvironment(EEnvironment environment)
		{
			Properties.Settings.Default.ENVIRONMENT = (long)environment;
			Properties.Settings.Default.Save();
		}*/

		#endregion
	}
}
