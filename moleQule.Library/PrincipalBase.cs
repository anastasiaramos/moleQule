using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Configuration;
using System.Security.Principal;
using System.Globalization;
using System.Threading;
using System.Web.Security;
using Microsoft.Win32;
using Infralution.Localization;

using Csla;
using Csla.Security;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using moleQule.Library.CslaEx;
using moleQule.Library.Resources;

namespace moleQule.Library
{
    /// <summary>
    /// Clase principal de gestión del usuario. Contiene Login() y Logout()
    /// </summary>
    [Serializable()]
	public class PrincipalBase : BusinessPrincipalBaseEx, IPrincipalEx
    {
		#region Attributes

		private ISchemaInfo _schema = null;

        private Type _schema_type = null;
        private bool _updating = false;

		private HashOidList _branches;
        private SecureItemList _secure_items;

		#endregion

		#region Properties

		/// <summary>
		/// Esquema activo.
		/// </summary>
		public ISchemaInfo ActiveSchema { get { return _schema; } set { _schema = value; } }
		public static string DefaultSchemaSettingName { get { return Properties.Settings.Default.SETTING_NAME_DEFAULT_SCHEMA; } }

        /// <summary>
        /// Tipo del Schema Activo
        /// </summary>
        public Type ActiveSchemaType { get { return _schema_type; } set { _schema_type = value; } }

        //public ProgressBar ProgressBar { get { return _bar; } set { _bar = value; } }

        public bool Updating { get { return _updating; } set { _updating = value; } }

		public HashOidList Branches { get { return _branches; } set { _branches = value; } }
        public SecureItemList SecureItems { get { return _secure_items; } set { _secure_items = value; } }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Contructor 
        /// </summary>
        /// <param name="identity"></param>
        public PrincipalBase(IIdentityEx identity)
            : base(identity ?? User.UnauthenticatedIdentity())
        {			
			_branches = new HashOidList();
            _secure_items = SecureItemList.GetList();
        }

		public virtual void Close()
		{
			Logout();
			AppContext.Principal = null;
		}

		public void CloseSettings() { SettingsMng.Instance.CloseSettings(AppContext.User);	}

        /// <summary>
        /// Carga el fichero de configuracion
        /// </summary>
        /// <param name="mapFilesPath"></param>
        /// <returns></returns>
        public static nHManager InitnHManager(string appAlias = null)
        {
			string nhfile = Path.Combine(AppContext.StartUpPath, SettingsMng.Instance.GetNHConfigFileRelativePath(null));
			return InitnHManager(appAlias, nhfile, SettingsMng.GetSuperUser(), SettingsMng.Instance.GetDBPassword(), string.Empty);
        }
        /*public static nHManager InitnHManager(string mapFilesPath) { return InitnHManager(mapFilesPath, string.Empty); }
		public static nHManager InitnHManager(string mapFilesPath, string server) { return InitnHManager(mapFilesPath, string.Empty, string.Empty, server); }*/
		public static nHManager InitnHManager(string appAlias, string mapFilesPath, string user, string pwd, string server)
		{
			if (String.IsNullOrEmpty(mapFilesPath))
				mapFilesPath = Path.Combine(AppContext.StartUpPath, SettingsMng.Instance.GetNHConfigFileRelativePath(appAlias));

			MyLogger.LogText("Application Type is " + SettingsMng.Instance.GetApplicationType().ToString(), "PrincipalBase::InitnHManager");
			MyLogger.LogText("Application config file is " + mapFilesPath ?? string.Empty, "PrincipalBase::InitnHManager");

			if (String.IsNullOrEmpty(user))
				user = SettingsMng.GetSuperUser();

			if (String.IsNullOrEmpty(pwd))
				pwd = SettingsMng.Instance.GetDBPassword();

			Type[] mappings = new Type[0];

			foreach (KeyValuePair<Type, IModuleDef> module in AppControllerBase.AppControler.Modules)
				mappings = MergeMapping(mappings, module.Value.Mappings);

			nHManager.Instance.Configure(mapFilesPath, mappings, pwd, User.MapToDBUsername(user), string.Empty, server);

			SettingsMng.Instance.SetActiveServer(nHManager.Instance.Host);
			SettingsMng.Instance.SetDBName(nHManager.Instance.Database);
			SettingsMng.Instance.SetDBUser(user);
			SettingsMng.Instance.SetDBPassword(pwd);

			return nHManager.Instance;
		}
		
		public static Type[] MergeMapping(Type[] map1, Type[] map2)
		{
			Type[] map_dest = new Type[map1.Length + map2.Length];
			map1.CopyTo(map_dest, 0);
			map2.CopyTo(map_dest, map1.Length);

			return map_dest;
		}

		public void LoadSettings(User user) { SettingsMng.Instance.LoadSettings(user); }

		public virtual void ClearUserContext()
		{
			CloseSettings();
		}
        
		public virtual void LoadUserContext()
		{
			if (AppContext.Principal == null) return;
           

			LoadSettings(AppContext.User);
			SetLocale(SettingsMng.Instance.GetUserCulture());
		}

		public virtual void SaveSettings() { SettingsMng.Instance.SaveSettings(); }

		public static void SetLocale(string locale)
		{
			try
			{
				if (AppControllerBase.Culture != AppContext.GetCultureInfo(locale))
					AppControllerBase.Culture = AppContext.GetCultureInfo(locale);
			}
			catch {}
		}

		public static void UpgradeSettings(string appVersion)
		{
			if (SettingsMng.Instance.GetApplicationVersion() != appVersion.ToString())
			{
				Properties.Settings.Default.Upgrade();
				SettingsMng.Instance.SetApplicationVersion(appVersion);
			}
		}

        #endregion

		#region Authorization Rules

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

        public override bool CanReadObject(long tipo_elemento)
        {
            User identity = (User)this.Identity;
            return identity.IsReadable(tipo_elemento);
        }

        public override bool CanCreateObject(long tipo_elemento)
        {
            User identity = (User)this.Identity;
            return identity.IsCreable(tipo_elemento);
        }

        public override bool CanModifyObject(long tipo_elemento)
        {
            User identity = (User)this.Identity;
            return identity.IsModifiable(tipo_elemento);
        }

        public override bool CanRemoveObject(long tipo_elemento)
        {
            User identity = (User)this.Identity;
            return identity.IsRemovable(tipo_elemento);
        }

        #endregion

		#region Schemas

		/// <summary>
		/// Elimina todos los datos asociados al esquema activo
		/// tanto los propios como los de las entidades que contiene
		/// </summary>
		/// <param name="schema"></param>
		public virtual void DeleteSchema(ISchema schema) { schema.IDelete(schema.Oid); }

		/// <summary>
		/// Crea un nuevo esquema
		/// </summary>
		/// <param name="oid"></param>
		public static void NewSchema(long oid_schema)
		{
			//Damos acceso a la nueva empresa al usuario Admin
			User admin = User.GetSuperUser();
			admin.Schemas.NewItem(admin, oid_schema);
			admin.Save();
			admin.CloseSession();

			AppContext.User.Reload();
		}

		#endregion

		#region Login / Logout

		public virtual void ChangeUserSchema(ISchemaInfo schema) { ChangeUserSchema(schema, false); }
		public virtual void ChangeUserSchema(ISchemaInfo schema, bool forceChangeSessionFactory)
		{
			try
			{
				if (schema == null) return;

				// Esquema activo
				if ((ActiveSchema == null) || (ActiveSchema.SchemaCode != schema.SchemaCode))
				{
					nHManager.Instance.ChangeUserSchema(schema.SchemaCode, User.MapToDBUsername(AppContext.User.Name), forceChangeSessionFactory);
					ActiveSchema = schema;
					ReloadUser();
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.SetCaller(ex, "PrincipalBase::ChangeActiveSchema");
				throw ex;
			}
		}
		public virtual void ChangeUserSchema(string schema, bool forceChangeSessionFactory = false)
		{
			try
			{
				if (schema == String.Empty) return;

				SchemaInfo empresa = SchemaInfo.New();
				empresa.Code = schema;

				if (schema == null) return;

				if ((AppContext.ActiveSchema == null) || (AppContext.ActiveSchema.SchemaCode != empresa.SchemaCode))
				{
					nHManager.Instance.ChangeUserSchema(empresa.SchemaCode, User.MapToDBUsername(AppContext.User.Name), forceChangeSessionFactory);
					ActiveSchema = empresa;
					ReloadUser();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		public static bool ChangePassword(string username, string oldPassword, string newPassword)
		{
			User user = null;

			try
			{
				user = User.Get(username, oldPassword, false);
				if (user == null) return false;

				user.PlainPwd = newPassword;
				user.Save();

				nHManager.Instance.ChangeCredentials(username, user.PlainPwd, string.Empty);

				return true;
			}
			catch
			{
				return false;
			}
			finally
			{
				if (user != null) user.CloseSession();
			}
		}
		public static bool ChangePassword(string username, string newPassword)
		{
			User user = null;

			try
			{
				//Only users with ADMIN rol are allowed to change the other user password without old one
				if (!AppContext.User.IsAdmin) return false;

				user = User.GetByUserName(username, false);
				if (user == null) return false;

				user.PlainPwd = newPassword;
				user.Save();

				nHManager.Instance.ChangeCredentials(username, user.PlainPwd, string.Empty);

				return true;
			}
			catch
			{
				return false;
			}
			finally 
			{
				if (user != null) user.CloseSession();
			}
		}

		public static void ClearCredentials(string username)
		{
			nHManager.Instance.RemoveSessionFactory(User.MapToDBUsername(username));
		}

		/// <summary>
		/// Login con usuario por defecto
		/// </summary>
		public static void Login() { Login(SettingsMng.Instance.GetDBUser(), SettingsMng.Instance.GetDBPassword(), string.Empty); }
		public static void Login(string username, string password) { Login(username, password, string.Empty); }
		public static bool Login(string username, string password, string server)
		{
			List<string> servers = new List<string>();

			if (server != string.Empty) servers.Add(server);
			//servers.Add(GetActiveServer());
			if (SettingsMng.Instance.GetLANServer() != string.Empty) servers.Add(SettingsMng.Instance.GetLANServer());
			if (SettingsMng.Instance.GetWANServer() != string.Empty) servers.Add(SettingsMng.Instance.GetWANServer());
			servers.Add(nHManager.Instance.Host);

			for (int i = 1; i <= servers.Count; i++)
			{
				try
				{
					nHManager.Instance.SetCredentials(User.MapToDBUsername(username), password, string.Empty, servers[i - 1]);

					if (DoLogin(username, password))
					{
						SettingsMng.Instance.SetDBUser(username);
						SettingsMng.Instance.SetDBPassword(password);

						return true;
					}
					else
						ClearCredentials(username);

					return false;
				}
				catch (Exception ex)
				{
					iQException iQex = iQExceptionHandler.ConvertToiQException(ex);

					switch (iQex.Code)
					{
						case iQExceptionCode.SERVER_NOT_FOUND:
							if (i == servers.Count) throw iQex;
							break;

						case iQExceptionCode.DB_VERSION_MISSMATCH:
							throw iQex;

						case iQExceptionCode.PASSWORD_FAILED:
							throw new iQAuthorizationException(moleQule.Library.Resources.Errors.LOGIN_FAILED,
																iQex.SysMessage,
																iQex.Code);
						case iQExceptionCode.LOCKED_USER:
							throw new iQAuthorizationException(string.Format(moleQule.Library.Resources.Errors.USER_LOCKED, username),
																iQex.SysMessage,
																iQex.Code);

						case iQExceptionCode.LOCKED_ROW:
							throw new iQLockException(string.Format(moleQule.Library.Resources.Errors.USER_LOGGED, username));

						default:
							throw iQex;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Realiza un login en la aplicación
		/// </summary>
		/// <param name="username"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		internal static bool DoLogin(string username, string password)
		{
			User user = null;

			try
			{
				//Control de SQL Injection
				if (username.Contains(" ") || password.Contains(" "))
					throw new iQException(moleQule.Library.Resources.Messages.SQL_INYECTION);

				user = User.Get(username, password);

				if (user != null)
				{
					if (user.EEstado == EEstadoItem.LockedOut)
						throw new iQAuthorizationException(iQExceptionCode.LOCKED_USER);

					user.LoadPrivileges();

					if (user.Name != SettingsMng.Instance.GetDBUser())
					{
						user.LastLoginDate = DateTime.Now;
						user.Save();
					}

					if (AppContext.Principal != null) AppContext.Principal.Close();

					AppContext.Principal.Identity = user;

					AppContext.Principal.LoadUserContext();
				}
				else
					AppContext.Principal.ClearUserContext();

				return (user != null) && user.IsAuthenticated;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				if (user != null) user.CloseSession();
			}
		}

        /// <summary>
        /// Logout del sistema
        /// </summary>
        public virtual void Logout()
        {
			try 
			{
				ClearUserContext();

				//El mapeo del superuser no lo quitamos porque se usa para el login del resto de usuarios
                if (AppContext.User.Name != SettingsMng.GetSuperUser()) ClearCredentials(AppContext.User.Name);
			} 
			catch { }

            AppContext.User.CopyFrom(User.UnauthenticatedIdentity());
        }

		/// <summary>
		/// Recarga el usuario para actualizar sus esquemas y permisos
		/// cuando se cambia de esquema o se borra un esquema
		/// </summary>
		public virtual void ReloadUser() { ReloadUser(AppContext.User.Oid, AppContext.ActiveSchema.Oid); }
		public virtual void ReloadUser(long oid, long oidSchema)
		{
			try
			{
				if (oid != 0)
				{
					AppContext.User.Reload();
				}
				else
				{
					User user = User.Get(oid);
					user.LoadPrivileges();
					
					if (AppContext.Principal != null) AppContext.Principal.Close();
					AppContext.Principal.Identity = user;

					SchemaInfo schema = SchemaInfo.Get(oidSchema);
					AppContext.Principal.ChangeUserSchema(schema);
				}

				AppContext.Principal.LoadUserContext();
			}
			catch (Exception ex)
			{
				if (iQExceptionHandler<iQAuthorizationException>.GetiQException(ex) != null)
					throw iQExceptionHandler<iQAuthorizationException>.GetiQException(ex);
				else
					throw ex;
			}
		}

		public static string ResetPassword(string username, string answer = null)
		{
			User user = null;

			try
			{
				//Only users wiht ADMIN rol are allowed to change the other user password without old one
				if (string.IsNullOrEmpty(answer))
					if (!(AppContext.User.IsAdmin && AppContext.User.Name != username)) return string.Empty;

				user = User.Get(username, false);
				if (user == null) return string.Empty;

				user.PlainPwd = Membership.GeneratePassword(8, 4);
				user.Save();

				nHManager.Instance.ChangeCredentials(username, user.PlainPwd, string.Empty);

				return user.PlainPwd;
			}
			catch
			{
				return string.Empty;
			}
			finally
			{
				if (user != null) user.CloseSession();
			}
		}

        #endregion

        #region Deprecated

          /// <summary>
        /// Busca actualizaciones disponibles en el FTP
        /// </summary>
        public static string LookForUpdates(string host,
                                            string user,
                                            string pwd,
                                            string remote_path,
                                            string remote_file)
        {
            return AppControllerBase.LookForUpdates(host, user, pwd, remote_path, remote_file, string.Empty);
        }

        #endregion
    }

	[Serializable()]
	public class PrincipalBaseSerialized
	{
		public long Oid { get; set; }
		public string UserName { get; set; }
		public long OidSchema { get; set; }

		public PrincipalBaseSerialized() {}
		public PrincipalBaseSerialized(IPrincipalEx principal)
		{
			Oid = principal.Identity.Oid;
			UserName = principal.Identity.Name;
			OidSchema = principal.ActiveSchema.Oid;
		}
	}
}
