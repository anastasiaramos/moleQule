using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Net;
using System.ComponentModel;
using System.Configuration;
using System.Security.Principal;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Win32;

using Infralution.Localization;
using NHibernate;
using Npgsql;
using moleQule.Library.CslaEx; 
using moleQule.Library.Resources;
using moleQule.Library.Properties;

namespace moleQule.Library
{
    /// <summary>
    /// Clase base controladora principal. 
    /// El controlador de su aplicación debe heredar de esta clase
    /// </summary>
    public class AppControllerBase
    {
        #region Atributes

		Dictionary<Type, IModuleDef> _active_modules = new Dictionary<Type, IModuleDef>();
		Dictionary<Type, Type> _record_entities = new Dictionary<Type, Type>();

		public Dictionary<Type, IModuleDef> Modules { get { return _active_modules; } }
		public Dictionary<Type, Type> RecordEntities { get { return _record_entities; } }

		public static IController AppControler;

        private ProgressBar _bar = null;
#if TRACE
        private moleQule.Library.Timer _timer = null;
#endif
        public ProgressBar ProgressBar
		{
            get { return _bar; }
            set { _bar = value; }
        }

#if TRACE
        public Timer Timer 
        { 
            get 
            {
                if (_timer == null)
                    throw new iQImplementationException("No se ha inicializado el Timer del Controler. Debe inicializarlo en el constructor del MainForm");
                else
                    return _timer;
            } 

            set { _timer = value; } 
        }
#endif
        #endregion

		#region ErrorHandling

		public static iQExceptionHandler.ErrorEventHandler ErrorHandler = null;

		protected static void HandleError(string message, string sysMessage)
		{ 
			iQExceptionHandler.HandleError(ErrorHandler, message, sysMessage);
		}
		protected static void HandleError(iQException ex)
		{
			iQExceptionHandler.HandleError(ErrorHandler, ex);
		}

		#endregion

		#region Paths

		/// <summary>
        /// Ruta de salida para las copias de seguridad
        /// </summary>
        public static string BACKUP_PATH { get { return Paths.BACKUPS; } }
        /// <summary>
        /// Ruta de los ficheros de configuración de nHibernate
        /// </summary>
        public static string NH_CONFIG_FILE_PATH { get { return Paths.NH_MAIN_CONFIG_FILE; } }

        #endregion

		#region Settings

		public static string GetBackupExtensionFilter() { return Settings.Default.BACKUP_EXTENSION_FILTER; }
		public static string GetDBServerHost() { return nHManager.Instance.Host; }
		public static string GetDBName() { return nHManager.Instance.Database; }
		public static string GetDBUser() { return nHManager.Instance.User; }
        
		#endregion

		#region Factory Methods
		
		public virtual void Close()
		{
			AppControler = null;
			ErrorHandler = null;

			ClearModules();
		}

		protected void ClearModules() 
		{
			if (_active_modules != null)
				_active_modules.Clear();

			if (_record_entities != null)
				_record_entities.Clear();
		}

		public virtual void Init(string appAlias, string appVersion)
		{
			PrincipalBase.UpgradeSettings(appVersion);
			CheckModulesVersion();
			CheckCrystalReportsVersion();
			CheckServerPath();
		}
		public virtual void InitFromService() { InitFromService(null); }
		public virtual void InitFromService(iQExceptionHandler.ErrorEventHandler errorHandler)
		{
			CheckModulesVersion();
			ErrorHandler = errorHandler;
            ActivateModules();
		}

		public void ActivateEntities()
		{
			ActivateAcreedores();
			ActivateEntidades();
		}

		protected void ActivateModule(IModuleDef module) 
		{
			if (_active_modules == null) _active_modules = new Dictionary<Type, IModuleDef>();
			_active_modules.Add(module.Type, module);

			module.GetEntities(_record_entities);
		}

		public virtual void ActivateModules() { throw new iQImplementationException("ControlerBase::ActivateModules()"); }
		public virtual void ActivateAcreedores() { throw new iQImplementationException("ControlerBase::ActivateAcreeedores()"); }
		public virtual void ActivateEntidades() { throw new iQImplementationException("ControlerBase::ActivateEntidades()"); }

        public static void CheckDBVersion()
        {
			// Version de la bd de moleQule

			ApplicationSettingInfo dbVersion = ApplicationSettingInfo.Get(Settings.Default.SETTING_NAME_MOLEQULE_DB_VERSION);

			//Version de base de datos equivalente o no existe la variable
			if ((dbVersion.Value == string.Empty) ||
				(String.CompareOrdinal(dbVersion.Value, SettingsMng.Instance.GetMoleQuleDBVersion()) == 0))
			{
				return;
			}
			//Version de base de datos superior
			else if (String.CompareOrdinal(dbVersion.Value, SettingsMng.Instance.GetMoleQuleDBVersion()) > 0)
			{
				throw new iQException(String.Format(Resources.Messages.DB_VERSION_HIGHER,
													dbVersion.Value,
													SettingsMng.Instance.GetMoleQuleDBVersion(),
													Settings.Default.NAME),
													iQExceptionCode.DB_VERSION_MISSMATCH);
			}
			//Version de base de datos inferior
			else if (String.CompareOrdinal(dbVersion.Value, SettingsMng.Instance.GetMoleQuleDBVersion()) < 0)
			{
				throw new iQException(String.Format(Resources.Messages.DB_VERSION_LOWER,
													dbVersion.Value,
													SettingsMng.Instance.GetMoleQuleDBVersion(),
													Settings.Default.NAME),
													iQExceptionCode.DB_VERSION_MISSMATCH);
			}

			// Version de la bd de la Aplicacion

            dbVersion = ApplicationSettingInfo.Get(Settings.Default.DB_VERSION_VARIABLE);

            //Version de base de datos equivalente o no existe la variable
            if ((dbVersion.Value == string.Empty) ||
				(String.CompareOrdinal(dbVersion.Value, SettingsMng.Instance.GetDBVersion()) == 0))
            {
                return;
            }
            //Version de base de datos superior
            else if (String.CompareOrdinal(dbVersion.Value, SettingsMng.Instance.GetDBVersion()) > 0)
            {
                throw new iQException(String.Format(Resources.Messages.DB_VERSION_HIGHER, 
                                                    dbVersion.Value, 
                                                    SettingsMng.Instance.GetDBVersion(),
													SettingsMng.Instance.GetApplicationTitle()),
													iQExceptionCode.DB_VERSION_MISSMATCH);
            }
            //Version de base de datos inferior
            else if (String.CompareOrdinal(dbVersion.Value, SettingsMng.Instance.GetDBVersion()) < 0)
            {
				throw new iQException(String.Format(Resources.Messages.DB_VERSION_LOWER,
                                                    dbVersion.Value,
                                                    SettingsMng.Instance.GetDBVersion(),
                                                    SettingsMng.Instance.GetApplicationTitle()),
													iQExceptionCode.DB_VERSION_MISSMATCH);
            }
        }

		public void CheckModulesDBVersion()
		{
			foreach (KeyValuePair<Type, IModuleDef> module in _active_modules)
				module.Value.Type.InvokeMember("CheckDBVersion", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);
		}

		public void CheckModulesVersion()
		{
			foreach (KeyValuePair<Type, IModuleDef> module in _active_modules)
				module.Value.Type.InvokeMember("UpgradeSettings", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);
		}

		public static void CheckCrystalReportsVersion()
		{
			try
			{
				string entryName = Paths.APP_REG32 + Paths.CRYSTAL_REPORTS_KEY;

				RegistryKey rk = Registry.LocalMachine.OpenSubKey(entryName, false);

				if (rk == null)
					throw new iQException(Resources.Errors.NO_CRYSTAL_REPORTS);
			}
			catch (Exception)
			{
				throw new iQException(Resources.Errors.NO_CRYSTAL_REPORTS);
			}
		}

		public static void CheckServerPath()
		{
			try
			{
				Reg32GetServerPath();
			}
			catch (Exception)
			{
				throw new iQException(String.Format(Resources.Errors.NO_SERVER_PATH, GetAppKeyName(), Application.ProductName));
			}
		}

		public static CultureInfo Culture
		{
			get { return CultureManager.ApplicationUICulture; }
			set 
			{
				MyLogger.LogText("CURRENT CULTURE IS " + Culture.Name + " | NEW CULTURE IS " + value.Name, "AppControllerBase::Culture:set");
				CultureManager.ApplicationUICulture = value;
				MyLogger.LogText("CULTURE SET TO " + Culture.Name, "AppControllerBase::Culture:set");
			}
		}

        public static void UpgradeSettings() { }

        #endregion

		#region Business Methods
		
		public static string GetCryptFileName(long oid, string fileName)
		{
			string blowfish_key = "f)y$JQM+o!s]jk9#t,ST#,c" + oid.ToString("000000000");
			string cryptname = BlowFishEncryptor.Encryption(fileName, blowfish_key);
			char[] invalidChars = Path.GetInvalidFileNameChars();
			//char[] invalidChars = new char[invalidFileChars.Length + 1];

			invalidChars = (invalidChars ?? Enumerable.Empty<char>()).Concat(new[] { '+' }).ToArray();

			string regex = String.Format("[{0}]", new string(invalidChars));
			Regex removeInvalidChars = new Regex(regex, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
			return removeInvalidChars.Replace(cryptname, "X");
		}

		#endregion

		#region Updates

		/// <summary>
        /// Busca actualizaciones disponibles en el FTP
        /// </summary>
        public static string LookForUpdates(string host,
                                            string user,
                                            string pwd,
                                            string remote_path,
                                            string remote_file,
                                            string exe_file)
        {
            FtpClient ftp = new FtpClient(host, user, pwd, remote_path);

            /*try
            {
                ftp.Login();
                string[] fileList = ftp.GetFileList(remote_path + "//*.*");
                remote_file = Path.GetFileNameWithoutExtension(remote_file);

                String version = string.Empty;

                if (exe_file != string.Empty)
                {
                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\" + exe_file);
                    version = fvi.FileVersion;
                }
                else
                {
                    version = Application.ProductVersion;
                }

                foreach (string ftpFile in fileList)
                {
                    string file = Path.GetFileName(ftpFile);

                    if (file.IndexOf(remote_file) != -1)
                    {
                        String ftpVersion = Path.GetFileNameWithoutExtension(file.Substring(remote_file.Length + 1));

                        //Si el fichero del ftp es una version posterior al ejecutable actual
                        string sub_version = version;
                        string sub_ftpVersion = ftpVersion;
                        while (sub_version.Length > 0 && sub_ftpVersion.Length > 0)
                        {
                            int index_version = sub_version.IndexOf(".");
                            int index_ftpVersion = sub_ftpVersion.IndexOf(".");
                            if (index_version != -1 && index_ftpVersion != -1)
                            {
                                //La version del update es posterior
                                if (Convert.ToInt32(sub_version.Substring(0, index_version)) <
                                    Convert.ToInt32(sub_ftpVersion.Substring(0, index_ftpVersion)))
                                {
                                    return Path.GetFileName(ftpFile);
                                }
                                //La version del update es anterior
                                else if (Convert.ToInt32(sub_version.Substring(0, index_version)) >
                                         Convert.ToInt32(sub_ftpVersion.Substring(0, index_ftpVersion)))
                                {
                                    sub_version = string.Empty;
                                }
                                //La version del update es igual
                                else
                                {
                                    sub_version = sub_version.Substring(index_version + 1);
                                    sub_ftpVersion = sub_ftpVersion.Substring(index_ftpVersion + 1);
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(sub_version) < Convert.ToInt32(sub_ftpVersion))
                                    return Path.GetFileName(ftpFile);
                                else
                                    sub_version = string.Empty;
                            }
                        }
                        if (String.Compare(version, ftpVersion) < 0)
                            return Path.GetFileName(ftpFile);
                        //else
                        //La actualizacion disponible es anterior a la que tenemos instalada
                        //throw new iQException(Errors.FTP_550);
                    }
                }

                //No hay ninguna actualizacion disponible
                throw new iQException(Errors.FTP_550);
            }
            catch (iQException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(Errors.FTP_GENERICO + Environment.NewLine + ex.Message);
            }*/

			return string.Empty;
        }

        // Sergio Rosas
        private static FtpClient _ftp;

        public static FtpClient Ftp { get { return _ftp; } }

        /// <summary>
        /// Descarga los ficheros para actualizar y actualiza
        /// </summary>
        public static string DownloadUpdate(string host,
                                            string user,
                                            string pwd,
                                            string remote_path,
                                            string local_path,
                                            BackgroundWorker bk)
        {

            _ftp = new FtpClient(host, user, pwd, remote_path);

            /*try
            {
                _ftp.Login();

                if (File.Exists(local_path + "\\" + Path.GetFileName(remote_path)))
                    File.Delete(local_path + "\\" + Path.GetFileName(remote_path));

                long file_size = _ftp.GetFileSize(remote_path);
                _ftp.Download(remote_path, Path.Combine(local_path, Path.GetFileName(remote_path)), bk);
            }
            catch (Exception ex)
            {
                switch (ex.Message)
                {
                    case "550": return Errors.FTP_550;
                    default: return Errors.FTP_GENERICO + Environment.NewLine +
                                    ex.Message;
                }
            }*/

            return Errors.FTP_200;
        }

        /// <summary>
        /// Ejecuta el fichero de actualización sin cambiar el usuario.
        /// </summary>
        public static void Update(string file_path)
        {

            /*if (!File.Exists(file_path))
                throw new iQException(Errors.UPDATE_FILE_NOT_FOUND);

            //Crea un nuevo proceso
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            //Ejecución del fichero de actualizacion
            process.StartInfo.FileName = file_path;

            AppContext.Principal.Updating = true;
            process.Start();
            Application.Exit();*/
        }

        /// <summary>
        /// Ejecuta el fichero de actualización
        /// </summary>
        public static void Update(string file_path, string user_name, string password, string domain)
        {
            if (!File.Exists(file_path))
                throw new iQException(Errors.UPDATE_FILE_NOT_FOUND);
            try
            {
                //Crea un nuevo proceso
                System.Diagnostics.Process process = new System.Diagnostics.Process();

                //Ejecución del fichero de actualizacion
                process.StartInfo.FileName = file_path;
                process.StartInfo.UserName = user_name;
                process.StartInfo.Domain = domain;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.WorkingDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);

                if (password.Length > 0)
                {
                    process.StartInfo.Password = new System.Security.SecureString();
                    foreach (char passChar in password.ToCharArray())
                    {
                        process.StartInfo.Password.AppendChar(passChar);
                    }
                }

                process.Start();
                Application.Exit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool IsCurrentUserAdministrator()
        {
            WindowsIdentity id = WindowsIdentity.GetCurrent();
            WindowsPrincipal pri = new WindowsPrincipal(id);
            return pri.IsInRole("BUILTIN\\Administradores") || pri.IsInRole("BUILTIN\\Administrators");
        }

        public static bool IsUserAdministrator(string user)
        {
            WindowsIdentity id = new WindowsIdentity(user);
            WindowsPrincipal pri = new WindowsPrincipal(id);
            return pri.IsInRole("BUILTIN\\Administradores") || pri.IsInRole("BUILTIN\\Administrators");
        }

        #endregion

        #region Autopilot

        /// <summary>
        /// Revisa acciones y escribe el resultado de las mismas para la agenda
        /// </summary>
        /// <returns></returns>
        public static List<string> FindNews()
        {
            List<string> news = new List<string>();

            //news.Add("");

            return news;
        }

        /// <summary>
        /// Realiza acciones automáticas al principio de la ejecución
        /// </summary>
        public virtual List<string> Autopilot(bool log)
        {
            List<string> results = new List<string>();

            //results.Add("");

            return results;
        }

        #endregion

		#region WinReg

		public static RegistryKey LocalMachineKeyWin32 { get { return RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32); } }
		public static RegistryKey LocalMachineKeyWin64 { get { return RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64); } }

		public static string GetAppKeyName() 
		{
			return Paths.APP_REG32
					+ Application.CompanyName + "\\"
					+ Application.ProductName;
		}

		/// <summary>
        /// Busca en el Registro de Windows el nombre de la base de datos
        /// </summary>
        /// <returns></returns>
		public static RegistryKey Reg32GetAppKey()
        {
            try
            {
				string entryName = GetAppKeyName();

                // Create a reference to a valid key.  In order for this code to
                // work, the indicated key must have been created previously.
                // The key name is not case-sensitive.
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(entryName, false);

                if (rk == null)
                {
                    throw new iQException(Errors.REG32_ENTRY_NOT_FOUND + " \"" + entryName + "\"");
                }

                return rk;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca en el Registro de Windows el nombre del servidor de bases de datos
        /// Si no lo encuentra lo busca en el fichero de configuración de hibernate
        /// </summary>
        /// <returns></returns>
		public static string Reg32GetDBServerHost()
        {
            try
            {
				RegistryKey rk = AppControllerBase.Reg32GetAppKey();
                if (rk.GetValue("DBServerHost") != null)
                    return rk.GetValue("DBServerHost").ToString();
                else
                    //Si no está en el registro Lo buscamos en la cadena de conexion 
                    return nHManager.Instance.GetConnectionParam("Server");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca en el Registro de Windows el nombre de la base de datos
        /// Si no lo encuentra lo busca en el fichero de configuración de hibernate
        /// </summary>
        /// <returns></returns>
		public static string Reg32GetDBName()
        {
            try
            {
				RegistryKey rk = AppControllerBase.Reg32GetAppKey();
                if (rk.GetValue("DBName") != null)
                    return rk.GetValue("DBName").ToString();
                else
                    //Si no está en el registro Lo buscamos en la cadena de conexion 
                    return nHManager.Instance.GetConnectionParam("Database");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca en el Registro de Windows el nombre del usuario de la base de datos
        /// Si no lo encuentra lo busca en el fichero de configuración de hibernate
        /// </summary>
        /// <returns></returns>
		public static string Reg32GetDBUser()
        {
            try
            {
				RegistryKey rk = AppControllerBase.Reg32GetAppKey();
                if (rk.GetValue("DBUser") != null)
                    return rk.GetValue("DBUser").ToString();
                else
                    //Si no está en el registro Lo buscamos en la cadena de conexion 
                    return nHManager.Instance.GetConnectionParam("User Id");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca en el Registro de Windows el nombre del recurso compartido
        /// donde se encuentra la aplicacion.
        /// </summary>
        /// <returns></returns>
		public static string Reg32GetServerPath()
        {
            try
            {
				RegistryKey rk = AppControllerBase.Reg32GetAppKey();
                string value = rk.GetValue("ServerPath", "").ToString();
                return (value.EndsWith("\\")) ? value : value + "\\";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca en el Registro de Windows el servidor donde se encuentra
        /// instalada la aplicacion
        /// </summary>
        /// <returns></returns>
		public static string Reg32GetServerHost()
        {
            try
            {
				RegistryKey rk = AppControllerBase.Reg32GetAppKey();
				if (rk == null) return string.Empty;
				return rk.GetValue("ServerHost", "").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Busca en el Registro de Windows la ruta de la versión de la aplicacion
        /// </summary>
        /// <returns></returns>
        public static string Reg32GetVersion()
        {
            try
            {
				RegistryKey rk = AppControllerBase.Reg32GetAppKey();
                return rk.GetValue("Version", "").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
        /// Busca en el Registro de Windows el nombre del directorio bin de PostgreSQL
        /// </summary>
        /// <returns></returns>
		public static string Reg32GetPGBinPath()
        {
			string debug = string.Empty;
			string debug_line = string.Empty;

			try
			{
                List<string> entryNames = new List<string>()
                {
                    Paths.APP_REG32 + Paths.POSTGRESQL_WINREG_6432,
                    Paths.APP_REG32 + Paths.POSTGRESQL_WINREG_32                    
                };

                List<RegistryKey> keys = new List<RegistryKey>()
                {
                    LocalMachineKeyWin64,
				    LocalMachineKeyWin32
                };

				foreach (RegistryKey key in keys)
				{
					if (key == null) continue;

                    RegistryKey reg_key = null;
                    string entryName = string.Empty;

                    foreach (string entry in entryNames)
                    {
                        entryName = entry;

                        debug_line = string.Format("LOOKING IN REGISTRY KEY: '{0}'", key.Name + "\\" + entry);
                        Console.WriteLine(debug_line);
                        debug += Environment.NewLine + debug_line;

                        // Create a reference to a valid key.  In order for this code to work, the indicated key must have been created previously.
                        reg_key = key.OpenSubKey(entry, false);

                        if (reg_key == null)
                        {
                            debug_line = string.Format(Errors.REG32_KEY_NOT_FOUND, key.Name + "\\" + entry);
                            Console.WriteLine(debug_line);
                            debug += Environment.NewLine + debug_line;
                        }
                        else
                            break;
                    }

                    if (reg_key == null) continue;

					foreach (string subKey in reg_key.GetSubKeyNames())
					{
						debug_line = string.Format(Errors.REG32_KEY_FOUND, key.Name + "\\" + entryName + subKey);
						Console.WriteLine(debug_line);
						debug += Environment.NewLine + debug_line;

						RegistryKey rk_subkey = key.OpenSubKey(entryName + subKey, false);

                        string version = (rk_subkey.GetValue("Version") != null) ? rk_subkey.GetValue("Version").ToString() : "0.0.0.0";
                        
                        if (version == SettingsMng.Instance.GetPostgreSQLVersion())
                        {
                            if (rk_subkey.GetValue("Base Directory") != null)
                                return rk_subkey.GetValue("Base Directory").ToString();
                            else
                            {
                                HandleError(String.Format(Errors.REG32_ENTRY_NOT_FOUND, key.Name + "\\" + entryName + subKey + "\\Base Directory\""), debug);
                                return string.Empty;
                            }
                        }
                        else
                        {
                            debug_line = string.Format("PostgreSQL installed version {0} mismatchs required version {1}"
                                                        ,version
                                                        ,SettingsMng.Instance.GetPostgreSQLVersion());
                            debug += Environment.NewLine + debug_line;
                        }
					}
				}
				
				HandleError(string.Format(Errors.REG32_POSTGRE_VERSION_ENTRY_NOT_FOUND, SettingsMng.Instance.GetPostgreSQLVersion()), debug);
			}
			catch (Exception ex)
			{
				HandleError(iQExceptionHandler.GetAllMessages(ex), debug);
			}

			return string.Empty;
		}

        #endregion

        #region Backups

		public static bool CanDoBackup()
		{
			DateTime nextBackup = SettingsMng.Instance.GetBackupsLastDate().AddDays(SettingsMng.Instance.GetBackupsDays());
			DateTime hour = SettingsMng.Instance.GetBackupsHour();
			nextBackup = new DateTime(nextBackup.Year, nextBackup.Month, nextBackup.Day, hour.Hour, hour.Minute, 0);

			return (DateTime.Now >= nextBackup);
		}

        /// <summary>
        /// Hace una copia automatica y con los valores por defecto del 
        /// fichero de configuración
        /// </summary>
        public static void AutoBackup(string dbHost, string dbName, string dbUser, List<ISchemaInfo> schemas = null, bool forceBackup = false)
		{
			dbUser = Settings.Default.ADMIN_USER;

			try
			{
				if ((forceBackup) || CanDoBackup())
                    CreateBackup(dbHost, dbName, dbUser, schemas, forceBackup);
				else
					throw new iQInfoException(String.Format(Resources.Messages.NO_BACKUP_POLICY, SettingsMng.Instance.GetBackupsLastDate().ToString("g")));
			}
			catch (iQInfoException ex)
			{
				HandleError(ex);
			}
			catch (Exception ex)
			{
				HandleError(ex.Message, iQExceptionHandler.GetAllMessages(ex));
			}
		}

        private static void CreateBackup(string dbHost, string dbName, string dbUser, List<ISchemaInfo> schemas = null, bool forceBackup = false)
		{
			//Obtención del nombre del fichero de salida
			DateTime fecha = DateTime.Now;
            string base_path = (SettingsMng.Instance.GetBackupsPath() != string.Empty)
                ? SettingsMng.Instance.GetBackupsPath()
                : Application.StartupPath + Paths.BACKUPS;
            
            string filename = fecha.Year.ToString() +
                                "_" + fecha.Month.ToString("00") +
                                "_" + fecha.Day.ToString("00") +
                                "_" + fecha.Hour.ToString("00") +
                                "_" + fecha.Minute.ToString("00") +
                                "_" + dbName + "_{0}" +
								".backup";

            CreateBackup(dbHost, dbName, dbUser, Path.Combine(base_path, filename), schemas, forceBackup);
		}
        private static void CreateBackup(string dbHost, string dbName, string dbUser, string outputFile, List<ISchemaInfo> schemas = null, bool forceBackup = false)
        {
			if ((forceBackup) || CanDoBackup())
                DoCreateBackup(dbHost, dbName, dbUser, schemas, outputFile);
			else
				throw new iQInfoException(String.Format(Resources.Messages.NO_BACKUP_POLICY, SettingsMng.Instance.GetBackupsLastDate().ToString("g")));
        }

        protected static void DoCreateBackup(string dbHost, string dbName, string dbUser, List<ISchemaInfo> schemas, string outputFile)
        {
            System.Diagnostics.Process process = null;

            try
            {
                //Obtención de la ruta de pg_dump
                string path_pg_dump = Reg32GetPGBinPath() + Paths.PG_DUMP;

                if (path_pg_dump == string.Empty) return;

                if (!File.Exists(path_pg_dump))
                    throw new iQException(Errors.BACKUP_APP_NOT_FOUND);

                string msg = "Creating db credentials file " + outputFile;
                MyLogger.LogText(msg, "AppControllerBase::DoCreateBackup");
                Console.WriteLine(msg);

                string db_pass = SettingsMng.Instance.GetDBPassword();

                //Creamos el fichero pgpass.conf para que no salte el prompt pidiendo password
                string pgpassPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                pgpassPath += Paths.PG_PASS;
                if (!Directory.Exists(Path.GetPathRoot(pgpassPath)))
                    Directory.CreateDirectory(Path.GetPathRoot(pgpassPath));
                StreamWriter sw = File.CreateText(pgpassPath);
                sw.WriteLine(dbHost + ":*:*:" + dbUser + ":" + db_pass);
                sw.Close();

                if (!Directory.Exists(Path.GetDirectoryName(outputFile))) Directory.CreateDirectory(Path.GetDirectoryName(outputFile));

                //Crea un nuevo proceso
                process = new System.Diagnostics.Process();

                if (schemas == null)
                {
                    //Ejecución del pg_dump
                    process.StartInfo.FileName = path_pg_dump;
                    process.StartInfo.Arguments =
                         string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8}"
                             , new string[] {
                                        "--format=c"
                                        ,"--ignore-version" 
                                        , "--oids"
                                        ,"--blobs"
                                        ,"--verbose"
                                        ,"--host=" + dbHost
                                        ,"-U " + dbUser
                                        ,@"--file=""" + string.Format(outputFile, "FULL") + @""""
                                        ,@"""" + dbName + @""""
                                    }
                         );
                    process.Start();
                    process.WaitForExit();
                }
                else
                {
                    //COMMON SCHEMA
                    try
                    {
                        //Ejecución del pg_dump
                        process.StartInfo.FileName = path_pg_dump;
                        process.StartInfo.Arguments =
                            string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}"
                                , new string[] {
                                        "--format=c"
                                        ,"--ignore-version" 
                                        , "--oids"
                                        ,"--blobs"
                                        ,"--verbose"
                                        ,"--host=" + dbHost
                                        ,"-U " + dbUser
                                        ,@"--file=""" + string.Format(outputFile, AppContext.CommonSchema) + @""""
                                        ,"--schema=" + AppContext.CommonSchema
                                        ,@"""" + dbName + @""""
                                    }
                            );

                        process.Start();
                        process.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        MyLogger.LogException(ex, "AppControllerBase::DoCreateBackup");
                    }

                    //DETAILED SCHEMAS
                    foreach (ISchemaInfo schema in schemas)
                    {
                        try
                        {
                            //Ejecución del pg_dump
                            process.StartInfo.FileName = path_pg_dump;
                            process.StartInfo.Arguments = 
                                string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}"
                                    , new string[] {
                                        "--format=c"
                                        ,"--ignore-version" 
                                        , "--oids"
                                        ,"--blobs"
                                        ,"--verbose"
                                        ,"--host=" + dbHost
                                        ,"-U " + dbUser
                                        ,@"--file=""" + string.Format(outputFile, schema.SchemaCode) + @""""
                                        ,"--schema=" + schema.SchemaCode
                                        ,@"""" + dbName + @""""
                                    }
                                );

                            process.Start();
                            process.WaitForExit();
                        }
                        catch (Exception ex)
                        {
                            MyLogger.LogException(ex, "AppControllerBase::DoCreateBackup");
                        }
                    }
                }

                //Borramos el fichero pgpass.conf
                File.Delete(pgpassPath);

                SettingsMng.Instance.SetBackupsLastDate(DateTime.Now);
                SettingsMng.Instance.SaveSettings();
            }
            catch (Exception ex)
            {
                MyLogger.LogException(ex, "AppControllerBase::DoCreateBackup");
                if (process != null)
                    MyLogger.LogText("process.StartInfo.Arguments = " + process.StartInfo.Arguments);
            }
        }

        public static void RestoreBackup(string db_host, string db_name, string db_user, string filename)
        {
			string db_pass = SettingsMng.Instance.GetDBPassword();

			//Obtención de la ruta de pg_restore
			string path_pg_restore = Reg32GetPGBinPath() + Paths.PG_RESTORE;

			if (path_pg_restore == string.Empty) return;

	        //Obtención de la ruta de pg_restore
            string path_pg_ctl = Application.StartupPath + Paths.PG_CTL;

            if (!File.Exists(path_pg_restore) || !File.Exists(path_pg_ctl))
                throw new iQException(Errors.RESTORE_APP_NOT_FOUND);

            //Creamos el fichero pgpass.conf para que no salte el prompt pidiendo password
            string pgpassPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            pgpassPath += Paths.PG_PASS;
            if (!Directory.Exists(Path.GetPathRoot(pgpassPath)))
                Directory.CreateDirectory(Path.GetPathRoot(pgpassPath));
            StreamWriter sw = File.CreateText(pgpassPath);
            sw.WriteLine(db_host + ":*:*:" + db_user + ":" + db_pass);
			sw.Close();

            string query = 
				@"	SELECT DISTINCT client_addr 
					FROM pg_stat_activity 
					WHERE datname = '" + db_name + "';";

			System.Data.IDataReader reader = nHManager.Instance.SQLNativeSelect(query);

            string lista_ip = string.Empty;
            long interfaces = 0;

            while (reader.Read())
            {
                lista_ip += Environment.NewLine + reader["client_addr"].ToString();
                interfaces++;
            }

            DialogResult result = DialogResult.No;

            if (interfaces > 1)
                result = MessageBox.Show(Resources.Messages.CLOSE_SESSIONS_1 +
                lista_ip + Environment.NewLine + Resources.Messages.CLOSE_SESSIONS_2,
                Resources.Messages.AVISO_CAPTION, MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                //cierra todas las conexiones abiertas con la base de datos 
                query = 
					@"	SELECT pg_terminate_backend(procpid) 
						FROM pg_stat_activity WHERE datname = '" + db_name + "';";
                
				ExecuteSQL(db_host, db_name, db_user, query);
                //elimina la base de datos
                query = "DROP DATABASE \"" + db_name + "\";";
                ExecuteSQL(db_host, db_name, db_user, query);
                //vuelve a crear la base de datos
                query = "CREATE DATABASE \"" + db_name + "\";";
                ExecuteSQL(db_host, db_name, db_user, query);

				//Crea un nuevo proceso
				System.Diagnostics.Process p = new System.Diagnostics.Process();

                //Ejecución del pg_restore
                p.StartInfo.FileName = path_pg_restore;
                p.StartInfo.Arguments = "--verbose " +
                                        "--host=" + db_host + " " +
                                        "-d \"" + db_name + "\" " +
                                        "-U " + db_user + " " +
                                        "\"" + filename + "\"";
                p.Start();
                p.WaitForExit();

                //Borramos el fichero pgpass.conf
                File.Delete(pgpassPath);
            }
        }

        private static bool ExecuteSQL(string db_host, string db_name, string db_user, string query)
        {
			string db_pass = SettingsMng.Instance.GetDBPassword();

            string scon = "Server=" + db_host +
                             ";User Id=" + db_user +
                             ";Password=" + db_pass +
                             ";Database = template1;";

            NpgsqlConnection conn = new NpgsqlConnection(scon);
            conn.Open();

            NpgsqlCommand command = new NpgsqlCommand(query, conn);
            command.ExecuteNonQuery();

            conn.Close();
            return true;
        }

        public static void CreateDB(string db_host, string db_name, string db_user, string filename)
        {
            string db_pass = SettingsMng.Instance.GetDBPassword();

            //Crea un nuevo proceso
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            //Obtención de la ruta de pg_restore
            string path_pg_restore = Application.StartupPath + Paths.PG_RESTORE;

            if (!File.Exists(path_pg_restore))
                throw new iQException(Errors.RESTORE_APP_NOT_FOUND);

            //Creamos el fichero pgpass.conf para que no salte el prompt pidiendo password
            string pgpassPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            pgpassPath += Paths.PG_PASS;
            if (!Directory.Exists(Path.GetPathRoot(pgpassPath)))
                Directory.CreateDirectory(Path.GetPathRoot(pgpassPath));
            StreamWriter sw = File.CreateText(pgpassPath);
            sw.WriteLine(db_host + ":*:*:" + db_user + ":" + db_pass);
            sw.Close();

            //Ejecución del pg_restore
            p.StartInfo.FileName = path_pg_restore;
            p.StartInfo.Arguments = "--create " +
                                    "--dbname=template0 " +
                                    "--ignore-version " +
                                    "--list " +
                                    "--verbose " +
                                    "--host= " + db_host + " " +
                                    "--superuser= " + db_user + " " +
                                    "-U " + db_user + " " +
                                    "--file=\"" + filename + "\"";
            p.Start();
            p.WaitForExit();

            //Borramos el fichero pgpass.conf
            File.Delete(pgpassPath);
        }

        #endregion
	}

	public class ModuleDef : IModuleDef
	{
		public string Name { get { return "moleQule"; } }
		public Type Type { get { return typeof(AppControllerBase); } }
		public Type[] Mappings
		{
			get
			{
				return new Type[] 
                {   
                    typeof(ApplicationSettingMap),
					typeof(ItemMapMap),
					typeof(PrivilegeMap),
                    typeof(SchemaSettingMap),
                    typeof(SchemaUserMap),
                    typeof(SecureItemMap),
                    typeof(SettingItemMap),
                    typeof(UserMap),
                    typeof(UserSettingMap),
                };
			}
		}

		public void GetEntities(Dictionary<Type, Type> recordEntities)
		{
			if (recordEntities.ContainsKey(typeof(ApplicationSetting))) return;

			recordEntities.Add(typeof(ApplicationSetting), typeof(ApplicationSettingRecord));
			recordEntities.Add(typeof(ItemMap), typeof(ItemMapRecord));
			recordEntities.Add(typeof(Privilege), typeof(PrivilegeRecord));
			recordEntities.Add(typeof(SchemaSetting), typeof(SchemaSettingRecord));
			recordEntities.Add(typeof(SchemaUser), typeof(SchemaUserRecord));
			recordEntities.Add(typeof(SecureItem), typeof(SecureItemRecord));
			recordEntities.Add(typeof(User), typeof(UserRecord));
			recordEntities.Add(typeof(UserSetting), typeof(UserSettingRecord));
		}
	}
}
