using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Web.Mvc;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx;
using moleQule.Library.CslaEx.Validation;

namespace moleQule.Library
{
    [Serializable()]
    public class UserRecord : RecordBase
    {
        #region Attributes

        private long _serial = 0;
        private string _id = string.Empty;
        private string _name = string.Empty;
        private string _password = string.Empty;
        private string _email = string.Empty;
        private bool _admin = false;
        private bool _super_user = false;
        private long _status;
        private bool _is_partner = false;
		private bool _is_service = false;
        private long _entity_type;
        private long _oid_entity;
        private string _pin = string.Empty;
        private string _password_question = string.Empty;
        private string _password_response = string.Empty;
        private DateTime _creation_date;
        private DateTime _last_login_date;
        private DateTime _last_password_date;
        private DateTime _last_activity_date;
        private DateTime _last_locked_out_date;
        private DateTime _birth_date;
        
		#endregion

        #region Properties

        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Code { get { return _id; } set { _id = value; } }
        public virtual long EntityType { get { return _entity_type; } set { _entity_type = value; } }
        public virtual long OidEntity { get { return _oid_entity; } set { _oid_entity = value; } }
        public virtual string Name { get { return _name; } set { _name = value; } }
        public virtual string Password { get { return _password; } set { _password = value; } }
        public virtual string Email { get { return _email; } set { _email = value; } }
        public virtual bool IsAdmin { get { return _admin; } set { _admin = value; } }
        public virtual bool IsSuperUser { get { return _super_user; } set { _super_user = value; } }
        public virtual bool IsPartner { get { return _is_partner; } set { _is_partner = value; } }
        public virtual bool IsService { get { return _is_service; } set { _is_service = value; } }
        public virtual long Estado { get { return _status; } set { _status = value; } }
        public virtual string Pin { get { return _pin; } set { _pin = value; } }
        public virtual DateTime CreationDate { get { return _creation_date; } set { _creation_date = value; } }
        public virtual DateTime LastLoginDate { get { return _last_login_date; } set { _last_login_date = value; } }
        public virtual DateTime LastPasswordDate { get { return _last_password_date; } set { _last_password_date = value; } }
        public virtual DateTime LastLockedOutDate { get { return _last_locked_out_date; } set { _last_locked_out_date = value; } }
        public virtual DateTime LastActivityDate { get { return _last_activity_date; } set { _last_activity_date = value; } }
        public virtual DateTime BirthDate { get { return _birth_date; } set { _birth_date = value; } }
        public virtual string PasswordQuestion { get { return _password_question; } set { _password_question = value; } }
        public virtual string PasswordResponse { get { return _password_response; } set { _password_response = value; } }

        #endregion

        #region Factory Methods

        public UserRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            //Se copia especificamente porque el Set del password modifica el password
            //para convertirlo a MD5

            Oid = Format.DataReader.GetInt64(source, "OID");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _id = Format.DataReader.GetString(source, "ID");
            _name = Format.DataReader.GetString(source, "NAME");
            _password = Format.DataReader.GetString(source, "PASSWORD");
            _email = Format.DataReader.GetString(source, "EMAIL");
            _admin = Format.DataReader.GetBool(source, "ADMOR");
            _super_user = Format.DataReader.GetBool(source, "MAIN");
            _status = Format.DataReader.GetInt64(source, "ESTADO");
            _is_partner = Format.DataReader.GetBool(source, "IS_PARTNER");
			_is_service = Format.DataReader.GetBool(source, "IS_SERVICE");
            _entity_type = Format.DataReader.GetInt64(source, "ENTITY_TYPE");
            _oid_entity = Format.DataReader.GetInt64(source, "OID_ENTITY");
            _pin = Format.DataReader.GetString(source, "PIN");
            _creation_date = Format.DataReader.GetDateTime(source, "CREATION_DATE");
            _last_login_date = Format.DataReader.GetDateTime(source, "LAST_LOGIN_DATE");
            _last_password_date = Format.DataReader.GetDateTime(source, "LAST_PASSWORD_DATE");
            _last_activity_date = Format.DataReader.GetDateTime(source, "LAST_LOCK_OUT_DATE");
            _last_locked_out_date = Format.DataReader.GetDateTime(source, "LAST_ACTIVITY_DATE");
            _birth_date = Format.DataReader.GetDateTime(source, "BIRTH_DATE");
            _password_question = Format.DataReader.GetString(source, "PASSWORD_QUESTION");
            _password_response = Format.DataReader.GetString(source, "PASSWORD_RESPONSE");
        }
        public virtual void CopyValues(UserRecord source)
        {
            if (source == null) return;

			_serial = source.Serial;
            _serial = source.Serial;
            _id = source.Code;
            _name = source.Name;
            _password = source.Password;
            _email = source.Email;
            _admin = source.IsAdmin;
            _super_user = source.IsSuperUser;
            _status = source.Estado;
            _is_partner = source.IsPartner;
			_is_service = source.IsService;
            _entity_type = source.EntityType;
            _oid_entity = source.OidEntity;
            _pin = source.Pin;
            _creation_date = source.CreationDate;
            _last_login_date = source.LastLoginDate;
            _last_password_date = source.LastPasswordDate;
            _last_activity_date = source.LastActivityDate;
            _last_locked_out_date = source.LastLockedOutDate;
            _birth_date = source.BirthDate;
            _password_question = source.PasswordQuestion;
            _password_response = source.PasswordResponse;
        }

        #endregion
    }

    [Serializable()]
    public class UserBase
    {
        #region Attributes

        public UserRecord _record = new UserRecord();

        public bool _is_authenticated = false;
        public string _plain_pwd = string.Empty;
        public string _default_schema = string.Empty;
        public long _default_oid_schema;
        public string _default_schema_code = string.Empty;

        #endregion

        #region Properties

		public UserRecord Record { get { return _record; } }

        public EEstadoItem EEstado { get { return (EEstadoItem)_record.Estado; } }
        public string EstadoLabel { get { return EnumText<EEstadoItem>.GetLabel(EEstado); } }
		public bool IsUser { get { return _record.OidEntity == 0 ; } set { } }
		public bool IsClient { get { return _record.EntityType == 7; } set { } }
		public bool IsProvider { get { return (new List<long> { 8, 9, 10, 11, 12, 14, 33 }).Contains(_record.EntityType); } }
        public bool IsApproved { get { return (EEstado == EEstadoItem.Active); } set { } }
        public bool IsLockedOut { get { return (EEstado == EEstadoItem.LockedOut); } set { } }
		public ERol Rol 
		{ 
			get 
			{
				if (_record.IsSuperUser) return ERol.SuperUser;
				if (_record.IsAdmin) return ERol.Admin;
				if (IsUser) return ERol.User;
				if (IsClient) return ERol.Client;
				if (IsProvider) return ERol.Provider;
				if (_record.IsPartner) return ERol.Partner;
				return ERol.None;
			}  
		}

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            //Se copia especificamente porque el Set del password modifica el password
            //para convertirlo a MD5

            _record.CopyValues(source);

            _default_oid_schema = Format.DataReader.GetInt64(source, "DEFAULT_SCHEMA_OID");
            _default_schema = Format.DataReader.GetString(source, "DEFAULT_SCHEMA_NAME");
            _default_schema_code = Format.DataReader.GetString(source, "DEFAULT_SCHEMA_CODE");
        }
        internal void CopyValues(User source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _plain_pwd = source.PlainPwd;
            _default_schema = source.DefaultSchema;
            _default_oid_schema = source.DefaultOidSchema;
            _is_authenticated = source.IsAuthenticated;
            _default_schema_code = source.DefaultSchemaCode;
        }
        internal void CopyValues(UserInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _is_authenticated = source.IsAuthenticated;
            _plain_pwd = source.PlainPwd;
            _default_oid_schema = source.DefaultOidSchema;
            _default_schema = source.DefaultSchema;
            _default_schema_code = source.DefaultSchemaCode;
        }

		public string GetCrypFileName(string name, string ext)
		{
			return AppControllerBase.GetCryptFileName(Record.Oid, name + "_" + Record.Oid.ToString()) + "." + ext;
		}

		public string GetAbsolutePath()
		{
			return Path.Combine(new string[] {	
									AppContext.StartUpPath,
									SettingsMng.Instance.GetDataPath(),
									Resources.Paths.USERS_PATH,
									AppContext.ActiveSchema.SchemaCode,
									Record.Oid.ToString(Library.Resources.Defaults.PATH_FORMAT) 
								});
		}

        #endregion
    }

    /// <summary>
    /// Editable Root Business Object With Editable Child Collection
    /// Editable Child Business Object With Editable Child Collection
    /// </summary>
    [Serializable()]
    public class User : BusinessBaseEx<User>, IIdentityEx
    {
        #region Attributes

        public UserBase _base = new UserBase();

        private Privileges _licences = Privileges.NewChildList();
        private Privileges _checked_licences = Privileges.NewChildList();
        private Privileges _verified_licences = Privileges.NewChildList();
        private SchemasUsers _schemas = SchemasUsers.NewChildList();

        #endregion

        #region Properties

		public UserBase Base { get { return _base; } }

        public override long Oid
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Oid;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
               // CanWriteProperty(true);

                if (!_base.Record.Oid.Equals(value))
                {
                    _base.Record.Oid = value;
                    //PropertyHasChanged();
                }
            }
        }
		public virtual long EntityType
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.EntityType;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.EntityType.Equals(value))
				{
					_base.Record.EntityType = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidEntity
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidEntity;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				// CanWriteProperty(true);
				if (!_base.Record.OidEntity.Equals(value))
				{
					_base.Record.OidEntity = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual long Serial
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Serial;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.Serial.Equals(value))
                {
                    _base.Record.Serial = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Code
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Code;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (value == null) value = string.Empty;

                if (!_base.Record.Code.Equals(value))
                {
                    _base.Record.Code = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Name
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Name;
            }

            set
            {
                CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (_base.Record.Name != value)
                {
                    _base.Record.Name = value;
                    PropertyHasChanged();
                }
            }

        }
        public virtual string Password
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Password;
            }

            set
            {
                CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (_base.Record.Password != value)
                {
                    _base.Record.Password = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Email
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Email;
            }

            set
            {
                CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (_base.Record.Email != value)
                {
                    _base.Record.Email = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool IsAdmin
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.IsAdmin || _base.Record.IsSuperUser;
            }

            set
            {
                //CanWriteProperty(true);
                if (_base.Record.IsAdmin != value)
                {
                    _base.Record.IsAdmin = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool IsSuperUser
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.IsSuperUser;
            }

            set
            {
                //CanWriteProperty(true);
                if (_base.Record.IsSuperUser != value)
                {
                    _base.Record.IsSuperUser = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool IsPartner
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.IsPartner;
            }

            set
            {
                //CanWriteProperty(true);
                if (_base.Record.IsPartner != value)
                {
                    _base.Record.IsPartner = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool IsService
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.IsService;
            }

            set
            {
                //CanWriteProperty(true);
                if (_base.Record.IsService != value)
                {
                    _base.Record.IsService = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long Estado
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Estado;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (!_base.Record.Estado.Equals(value))
                {
                    _base.Record.Estado = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Pin
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.Pin;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (_base.Record.Pin != value)
                {
                    _base.Record.Pin = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime CreationDate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.CreationDate;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.CreationDate.Equals(value))
                {
                    _base.Record.CreationDate = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime LastLoginDate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.LastLoginDate;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.LastLoginDate.Equals(value))
                {
                    _base.Record.LastLoginDate = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime LastPasswordDate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.LastPasswordDate;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.LastPasswordDate.Equals(value))
                {
                    _base.Record.LastPasswordDate = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime LastLockedOutDate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.LastLockedOutDate;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.LastLockedOutDate.Equals(value))
                {
                    _base.Record.LastLockedOutDate = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime LastActivityDate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.LastActivityDate;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.LastActivityDate.Equals(value))
                {
                    _base.Record.LastActivityDate = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime BirthDate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.BirthDate;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);

                if (!_base.Record.BirthDate.Equals(value))
                {
                    _base.Record.BirthDate = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string PasswordQuestion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PasswordQuestion;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (_base.Record.PasswordQuestion != value)
                {
                    _base.Record.PasswordQuestion = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string PasswordResponse
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _base.Record.PasswordResponse;
            }

            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
                if (_base.Record.PasswordResponse != value)
                {
                    _base.Record.PasswordResponse = value;
                    PropertyHasChanged();
                }
            }
        }

        public virtual Privileges Licences
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _licences;
            }

            set
            {
                //CanWriteProperty(true);
                _licences = value;
                //PropertyHasChanged();
            }
        }
        public virtual Privileges CheckedLicences
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _checked_licences;
            }

            set
            {
                //CanWriteProperty(true);
                _checked_licences = value;
                //PropertyHasChanged();
            }
        }
        public virtual Privileges VerifiedLicences
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _verified_licences;
            }

            set
            {
                //CanWriteProperty(true);
                _verified_licences = value;
                //PropertyHasChanged();
            }
        }
        public virtual SchemasUsers Schemas
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
                return _schemas;
            }
            set
            {
                //CanWriteProperty(true);
                _schemas = value;
                //PropertyHasChanged();
            }
        }

        public virtual EEstadoItem EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
        public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual ERol Rol { get { return _base.Rol; } }
		public virtual bool IsUser { get { return _base.IsUser; } }
        public virtual bool IsClient { get { return _base.IsClient; } set { _base.IsClient = value; } }
		public virtual bool IsProvider { get { return _base.IsProvider; } }
        public virtual bool IsAuthenticated { get { return _base._is_authenticated; } set { _base._is_authenticated = value; } }
        public virtual bool IsApproved { get { return _base.IsApproved; } set { } }
        public virtual bool IsLockedOut { get { return _base.IsLockedOut; } set { } }
        public virtual string PlainPwd { get { return _base._plain_pwd; } set { _base._plain_pwd = value; if (value != string.Empty) Password = ClassMD5.getMd5Hash(value); } }
        public virtual string DefaultSchema { get { return _base._default_schema; } set { _base._default_schema = value; } }
        public virtual long DefaultOidSchema { get { return _base._default_oid_schema; } set { _base._default_oid_schema = value; } }
        public virtual string DefaultSchemaCode { get { return _base._default_schema_code; } set { _base._default_schema_code = value; } }

        public override bool IsValid
        {
            get { return base.IsValid && _licences.IsValid && _schemas.IsValid; }
        }
        public override bool IsDirty
        {
            get { return base.IsDirty || _licences.IsDirty || _schemas.IsDirty; }
        }

        public static string MapToDBUsername(string username)
        {
			List<string> systemusers = new List<string>() { SettingsMng.GetSuperUser(), SettingsMng.GetServicesUser() }; 

            if (!String.IsNullOrEmpty(username))
                return (systemusers.Contains(username)) ? username : SettingsMng.Instance.GetDBName() + "_" + username;
            else
                return SettingsMng.GetAdminUser();
        }

        #endregion

        #region Business Methods

        /// <summary>
        /// Clona la entidad y sus subentidades y las marca como nuevas
        /// </summary>
        /// <returns>Una entidad clon</returns>
        public virtual User CloneAsNew()
        {
            User clon = base.Clone();

            clon.SessionCode = User.OpenSession();
            User.BeginTransaction(clon.SessionCode);

            clon.MarkNew();
            clon.Licences.MarkAsNew();
            clon.Schemas.MarkAsNew();
            return clon;
        }

        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(User));
            Code = Serial.ToString(Resources.Defaults.USER_CODE_FORMAT);
        }

        public virtual void SetNewCode()
        {
            SerialInfo.Check(typeof(User), Oid, Code);
            try { Serial = Convert.ToInt64(Code); }
            catch { Serial = SerialInfo.GetNext(typeof(User)); }
        }

        public virtual string AuthenticationType
        {
            get { return "Csla"; }
        }

        public virtual void GeneratePin(int maxValue)
        {
            Pin = (new Random().Next(0, 9999)).ToString("0000");
        }
        public virtual string GetName() { return Name; }
        public virtual string GetPassword() { return Password; }
        public virtual void SetPassword(string pwd) { PlainPwd = pwd; }

        protected void CopyValues(User source)
        {
            if (source == null) return;

            _licences = source.Licences;
            _schemas = source.Schemas;

            _base.CopyValues(source);
        }

        public virtual void CopyFrom(User source)
        {
            if (source == null) return;

            Name = source.Name;
            Password = source.Password;
            IsAdmin = source.IsAdmin;
            IsSuperUser = source.IsSuperUser;
            IsPartner = source.IsPartner;
            IsAuthenticated = source.IsAuthenticated;
            Estado = source.Estado;
            OidEntity = source.OidEntity;
            EntityType = source.EntityType;

            PlainPwd = source.PlainPwd;
            DefaultSchema = source.DefaultSchema;
            DefaultOidSchema = source.DefaultOidSchema;
            DefaultSchemaCode = source.DefaultSchemaCode;
        }
        public virtual void CopyFrom(UserInfo source)
        {
            if (source == null) return;

            Oid = source.Oid;
            Name = source.Name;
            Password = source.Password;
            IsAdmin = source.IsAdmin;
            IsSuperUser = source.IsSuperUser;
            IsPartner = source.IsPartner;
			IsService = source.IsService;
            IsAuthenticated = false;
            Estado = source.Estado;
            OidEntity = source.OidEntity;
            EntityType = source.EntityType;

            PlainPwd = source.PlainPwd;
            DefaultSchema = source.DefaultSchema;
            DefaultOidSchema = source.DefaultOidSchema;
            DefaultSchemaCode = source.DefaultSchemaCode;
        }

        /// <summary>
        ///  Indica si el usuario actual tiene permiso para 
        /// leer el elemento pasado como una string
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        public virtual bool IsReadable(long elemento)
        {
            //Si es administrador, siempre tiene permisos
            if (this.IsAdmin) return true;

            Privilege perm = _licences.GetItem(elemento);

            if (perm != null)
                return perm.Read;

            return false;
        }

        /// <summary>
        /// Indica si el usuario actual tiene permiso para 
        /// escribir el elemento pasado como una string
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        public virtual bool IsCreable(long elemento)
        {
            //Si es administrador, siempre tiene permisos
            if (this.IsAdmin) return true;

            Privilege perm = _licences.GetItem(elemento);

            if (perm != null)
                return perm.Create;

            return false;
        }
        /// <summary>
        /// Indica si el usuario actual tiene permiso para 
        /// escribir el elemento pasado como una string
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        public virtual bool IsModifiable(long elemento)
        {
            //Si es administrador, siempre tiene permisos
            if (this.IsAdmin) return true;

            Privilege perm = _licences.GetItem(elemento);

            if (perm != null)
                return perm.Modify;

            return false;
        }

        /// <summary>
        /// Indica si el usuario actual tiene permiso para 
        /// escribir el elemento pasado como una string
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        public virtual bool IsRemovable(long elemento)
        {
            //Si es administrador, siempre tiene permisos
            if (this.IsAdmin) return true;

            Privilege perm = _licences.GetItem(elemento);

            if (perm != null)
                return perm.Remove;

            return false;
        }

        public virtual void AttachSchema(ISchemaInfo schema)
        {
            Schemas.NewItem(this, schema.Oid);
            Licences = Privileges.CreatePerms(this);
            if (DefaultOidSchema == 0)
            {
                DefaultOidSchema = schema.Oid;
                DefaultSchema = schema.Name;
            }
        }
        public virtual void DettachSchema(ISchemaInfo schema)
        {
            Schemas.RemoveItem(schema);
            Licences.RemoveAll();
        }

        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            //Name
            if (Name == string.Empty)
            {
                e.Description = Resources.Messages.NO_NAME_SELECTED;
				throw new iQValidationException(e.Description, string.Empty, "Name");
            }

            //Password
            if (Password == string.Empty)
            {
                e.Description = Resources.Messages.NO_PASSWORD_SELECTED;
				throw new iQValidationException(e.Description, string.Empty, "Password");
            }

            return true;
        }

        #endregion

        #region Authorization Rules

        protected override void AddAuthorizationRules() { }

        public static bool CanAddObject() { return true; }
        public static bool CanGetObject() { return true; }
        public static bool CanDeleteObject() { return AppContext.User.IsAdmin; }
        public static bool CanEditObject() { return true; }

        public virtual bool CanAccessSchema(long oid)
        {
            return (IsSuperUser || (_schemas.GetItemBySchema(oid) != null));
        }

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USA LA FUNCION New
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero es protected por exigencia de NHibernate.
        /// </summary>
        public User() { }

        public virtual UserInfo GetInfo() { return GetInfo(true); }
        public virtual UserInfo GetInfo(bool childs) { return new UserInfo(this, childs); }

        #endregion

        #region Root Factory Methods

        public static User UnauthenticatedIdentity()
        {
            return DataPortal.Create<User>(new CriteriaCs(-1));
        }

        public static User New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            return DataPortal.Create<User>(new CriteriaCs(-1));
        }

        public new static User Get(string query, bool childs, int sessionCode = -1)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            return BusinessBaseEx<User>.Get(query, childs, -1);
        }

        public static User Get(long oid, bool childs = true) { return Get(User.SELECT(oid), childs); }
        public static User Get(string username, string password, bool childs = true) { return Get(User.SELECT(username, ClassMD5.getMd5Hash(password)), childs); }
		public static User GetByUserName(string username, bool childs = true) { return Get(User.SELECT(username, true), childs); }

        public static User GetSuperUser() { return Get(User.SELECT_SUPER_USER(), true); }

        public virtual void LoadPrivileges()
        {
            LoadChilds(typeof(Privilege), true);
        }
        public virtual void LoadChilds(Type type, bool childs)
        {
            if (type.Equals(typeof(Schema)))
            {
                _schemas = SchemasUsers.GetChildList(this, childs);
            }
            if (type.Equals(typeof(Privilege)))
            {
                _licences = Privileges.GetChildList(this, childs);
            }
        }

        public virtual void Reload()
        {
            SessionCode = User.OpenSession();
            LoadChilds(typeof(Schema), true);
            LoadChilds(typeof(Privilege), true);

            CloseSession();
        }

        /// <summary>
        /// Borrado inmediato, no cabe "undo"
        /// (La función debe ser "estática")
        /// </summary>
        /// <param name="oid"></param>
        public static void Delete(long oid)
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            DataPortal.Delete(new CriteriaCs(oid));
        }

        public override User Save()
        {
            // Por doble interfaz Root/Child
            if (IsChild) throw new iQException(Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

            if (IsDeleted && !CanDeleteObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            else if (IsNew && !CanAddObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            else if (!CanEditObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                Privileges.AssociatePerms(this);
                ValidationRules.CheckRules();
            }
            catch (iQValidationException ex)
            {
                iQExceptionHandler.TreatException(ex);
                return this;
            }

            try
            {
                base.Save();

                _licences.Update(this);
                _schemas.Update(this);

                if (!SharedTransaction) Transaction().Commit();
                return this;
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
                return this;
            }
            finally
            {
				if (!SharedTransaction)
				{
					if (CloseSessions) CloseSession();
					else BeginTransaction();
				}
            }
        }

        #endregion

        #region Child Factory Methods

        public User(User source)
        {
            MarkAsChild();
            Fetch(source);
        }

        private User(int session_code, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
            SessionCode = session_code;
            Fetch(reader);
        }

        internal static User NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            User obj = DataPortal.Create<User>(new CriteriaCs(-1));
            obj.MarkAsChild();
            return obj;
        }

        internal static User GetChild(User source)
        {
            return new User(source);
        }
        internal static User GetChild(int session_code, IDataReader reader) { return GetChild(session_code, reader, true); }
        internal static User GetChild(int session_code, IDataReader reader, bool childs)
        {
            return new User(session_code, reader, childs);
        }

        #endregion

        #region Common Data Access

        private void DataPortal_Create(CriteriaCs criteria)
        {
            _base.Record.Oid = (long)(new Random()).Next();
			Name = Resources.Messages.NEW_USER;
			BirthDate = DateTime.Today;
			CreationDate = DateTime.Now;
            LastActivityDate = DateTime.Now;
            LastPasswordDate = DateTime.Now;
            LastActivityDate = DateTime.Now;
            LastLoginDate = DateTime.Now;

            IsAuthenticated = false;
            EEstado = EEstadoItem.Active;

            _licences = Privileges.NewChildList();
            _schemas = SchemasUsers.NewChildList();
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                Oid = 0;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    //User.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                    {
                        IsAuthenticated = true;
                        _base.CopyValues(reader);
                    }
                    else
                        throw new iQAuthorizationException(iQExceptionCode.PASSWORD_FAILED);

                    if (Childs)
                    {
                        string query = string.Empty;

                        query = SchemasUsers.SELECT(this);
                        reader = nHManager.Instance.SQLNativeSelect(query, Session());
                        _schemas = SchemasUsers.GetChildList(SessionCode, reader);
                    }
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.SetCaller(ex, "User::DataPortal_Fetch");
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Insert()
        {
            try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }

                GetNewCode();
				LastActivityDate = DateTime.Now;

                //Creamos el usuario de PostgresQL
                nHMng.SQLNativeExecute(CREATE_USER(Name, PlainPwd, IsAdmin), Session());

                Session().Save(Base.Record);

                UserSettings settings = UserSettings.NewList(this);
                settings.SessionCode = SessionCode;
                settings.SaveAsChild();
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Update()
        {
			if (!IsDirty) return;

			try
            {
                UserRecord obj = Session().Get<UserRecord>(this.Oid);

                if (AppContext.User != null)
                {
                    //Si el usuario que está intentando guardar es nuevo y está intentando modificar al Admin
                    //no se trata de una modificación, se trata de que el usuario Admin está iniciando sesión
                    //Se llama desde el Login del PrincipalBase cuando guarda la última fecha de conexión del usuario
                    //El usuario Admin no puede ser editado por otro usuario
                    //En el caso de que el usuario no esté autenticado y no sea super usuario es porque se está tratando de 
                    //iniciar sesión con el usuario moladmin y se está grabando su última hora de conexión
                    if ((obj.IsSuperUser) && (AppContext.User.IsAuthenticated && !AppContext.User.IsSuperUser) && (!AppContext.User.IsNew))
                        throw new iQException(String.Format(moleQule.Library.Resources.Messages.EDIT_USER_NOT_ALLOWED, obj.Name));
                }

                //Editamos el usuario PostgreSQL
                if ((PlainPwd != string.Empty) && (obj.Password != Password))
                    nHMng.SQLNativeExecute(EDIT_USER_PWD(obj.Name, PlainPwd, IsAdmin), Session());

                //Editamos el usuario PostgreSQL
                if ((Name != string.Empty) && (obj.Name != Name))
                    nHMng.SQLNativeExecute(EDIT_USER_NAME(obj.Name, Name, PlainPwd), Session());

                obj.CopyValues(this._base.Record);
                Session().Update(obj);
            }
            catch (Exception ex)
            {
				iQExceptionHandler.SetCaller(ex, "User::DataPortal_Fetch");
                iQExceptionHandler.TreatException(ex);
            }
        }

        // deferred deletion
        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new CriteriaCs(Oid));
        }

        // inmediate deletion
        [Transactional(TransactionalTypes.Manual)]
        private void DataPortal_Delete(CriteriaCs criteria)
        {
            try
            {
                // Iniciamos la conexion y la transaccion
                SessionCode = OpenSession();
                BeginTransaction();

                CriteriaEx criterio = GetCriteria();
                criterio.AddOidSearch(criteria.Oid);

                UserRecord obj = (UserRecord)(criterio.UniqueResult());

                // El usuario Admin no puede ser borrado
                if (obj.Oid == 1) throw new iQException(String.Format(Resources.Messages.DELETE_USER_NOT_ALLOWED, obj.Name));

                //Borramos el usuario PostgreSQL
                nHMng.SQLNativeExecute(DELETE_USER(obj.Name), Session());

                Session().Delete(obj);

                Transaction().Commit();
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                CloseSession();
            }
        }

        #endregion

        #region Child Data Access

        private void Fetch(User source)
        {
            try
            {
                SessionCode = source.SessionCode;

                CopyValues(source);

                CriteriaEx criteria = Privilege.GetCriteria(Session());
                criteria.AddEq("OidUser", this.Oid);
                _licences = Privileges.GetChildList(criteria.List<Privilege>());

                criteria = SchemaUser.GetCriteria(Session());
                criteria.AddEq("OidUser", this.Oid);
                _schemas = SchemasUsers.GetChildList(criteria.List<SchemaUser>());
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }
        private void Fetch(IDataReader source)
        {
            _base.CopyValues(source);

            if (Childs)
            {
                IDataReader reader;
                string query;

                Privilege.DoLOCK(Session());

                query = Privileges.SELECT(this);
                reader = nHMng.SQLNativeSelect(query, Session());
                _licences = Privileges.GetChildList(SessionCode, reader, Childs);

                SchemaUser.DoLOCK(Session());

                query = SchemasUsers.SELECT(this);
                reader = nHMng.SQLNativeSelect(query, Session());
                _schemas = SchemasUsers.GetChildList(SessionCode, reader);
            }

            MarkOld();
        }

        internal void Insert(Users parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            try
            {
				//Debe obtener la sesion del padre pq User es, a su vez, padre de permisos
				SessionCode = parent.SessionCode;

				LastActivityDate = DateTime.Now;

				Privileges.AssociatePerms(this);
                ValidationRules.CheckRules();
                if (!IsValid)
                    throw new iQValidationException(Resources.Messages.GENERIC_VALIDATION_ERROR);

                //Creamos el usuario de PostgresQL
                nHMng.SQLNativeExecute(CREATE_USER(Name, PlainPwd, IsAdmin), Session());

                parent.Session().Save(Base.Record);

                _licences.Update(this);
                _schemas.Update(this);

                UserSettings settings = UserSettings.NewList(this);
                settings.SetValue(Properties.Settings.Default.SETTING_NAME_DEFAULT_SCHEMA, AppContext.ActiveSchema.Oid.ToString());
                settings.SharedTransaction = true;
                settings.SessionCode = SessionCode;
                settings.Save();
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        internal void Update(Users parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            //Debe obtener la sesion del padre pq User es, a su vez, padre de permisos
            SessionCode = parent.SessionCode;

            try
            {
                Privileges.AssociatePerms(this);

                ValidationRules.CheckRules();

                if (!IsValid) throw new iQValidationException(Resources.Messages.GENERIC_VALIDATION_ERROR);

                UserRecord obj = Session().Get<UserRecord>(Oid);

                if (obj != null)
                {
                    // El usuario Admin no puede ser editado por otro usuario
                    if (!AppContext.User.IsSuperUser && obj.IsSuperUser)
                        throw new iQException(String.Format(moleQule.Library.Resources.Messages.EDIT_USER_NOT_ALLOWED, obj.Name));

                    //Editamos el usuario PostgreSQL
                    if (obj.Password != Password) nHMng.SQLNativeExecute(EDIT_USER_PWD(obj.Name, PlainPwd, IsAdmin), Session());

                    if (obj.Name != Name)
                        nHMng.SQLNativeExecute(EDIT_USER_NAME(obj.Name, Name, PlainPwd), Session());

                    obj.CopyValues(this._base.Record);
                    Session().Update(obj);
                }

                _licences.Update(this);
                _schemas.Update(this);

                /*if (AppContext.User.Oid == this.Oid)
                    PrincipalBase.Instance.SetDefaultSchema(this.DefaultOidSchema);
                else
                {
                    UserSettings settings = UserSettings.GetListByUser(this.Oid);
                    settings.SetValue(Properties.Settings.Default.SETTING_NAME_DEFAULT_SCHEMA, DefaultOidSchema.ToString());
                    settings.Save();
                    settings.CloseSession();
                }*/
            }
            catch (Exception ex)
            {
				iQExceptionHandler.SetCaller(ex, "User::Update");
                iQExceptionHandler.TreatException(ex);
            }

            MarkOld();
        }

        internal void DeleteSelf(Users parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            SessionCode = parent.SessionCode;

            UserRecord obj = Session().Get<UserRecord>(Oid);

            // El usuario Admin no puede ser borrado
            if (obj.Oid == 1) throw new iQException(String.Format(Resources.Messages.DELETE_USER_NOT_ALLOWED, obj.Name));

            //Borramos el usuario PostgreSQL
            nHMng.SQLNativeExecute(DELETE_USER(obj.Name), Session());

            Session().Delete(obj);

            MarkNew();
        }

        #endregion

        #region SQL

        internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>()
            {

            };
        }

        public new static string SELECT(long oid) { return SELECT(oid, true); }
        public static string SELECT(string username) { return SELECT(username, true); }
        public static string SELECT(string username, string pwd) { return SELECT(username, pwd, true); }
        public static string SELECT_SUPER_USER() { return SELECT_SUPER_USER(true); }

        public static string CREATE_USER(string username, string pwd, bool admin)
        {
            string query = string.Empty;

            username = MapToDBUsername(username);

            query = "CREATE USER \"" + username + "\"" +
                    " PASSWORD '" + pwd + "'" +
                    " IN ROLE \"MOLEQULE_ADMINISTRATOR\"";

            if (admin) query += " SUPERUSER";

            return query;
        }

        public static string EDIT_USER_PWD(string username, string pwd, bool admin)
        {
            string query = string.Empty;

            username = MapToDBUsername(username);

            query = "ALTER USER \"" + username + "\"" + " PASSWORD '" + pwd + "'";
            //query += admin ? " SUPERUSER;" : " NOSUPERUSER;";

            return query;
        }

        public static string EDIT_USER_NAME(string username, string newname, string pwd)
        {
            string query = string.Empty;

            username = MapToDBUsername(username);
            newname = MapToDBUsername(newname);

            query = "ALTER USER \"" + username + "\"" + " RENAME TO \"" + newname + "\";";

            return query;
        }

        public static string DELETE_USER(string username)
        {
            string query = string.Empty;

            username = MapToDBUsername(username);

            query = "DROP USER \"" + username + "\"";

            return query;
        }

        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT U.*" +
                    "		,COALESCE(SC.\"OID\", 0) AS \"DEFAULT_SCHEMA_OID\"" +
                    "		,COALESCE(SC.\"NOMBRE\", '') AS \"DEFAULT_SCHEMA_NAME\"" +
                    "       ,COALESCE(SC.\"CODIGO\", '') AS \"DEFAULT_SCHEMA_CODE\"";

            return query;
        }

        internal static string JOIN(QueryConditions conditions)
        {
            string us = nHManager.Instance.GetSQLTable(typeof(UserRecord));
            string su = nHManager.Instance.GetSQLTable(typeof(SchemaUserRecord));
            string st = nHManager.Instance.GetSQLTable(typeof(UserSettingRecord));

            Assembly assembly = Assembly.Load("moleQule.Library.Common");
            Type schematype = assembly.GetType("moleQule.Library.Common.CompanyRecord");

            string sc = nHManager.Instance.GetSQLTable(schematype);

            string query;

            query = " FROM " + us + " AS U" +
                    " INNER JOIN " + st + " AS ST ON ST.\"OID_USER\" = U.\"OID\" AND ST.\"NAME\" = '" + PrincipalBase.DefaultSchemaSettingName + "'" +
                    " LEFT JOIN " + sc + " AS SC ON SC.\"OID\" = to_number(ST.\"VALUE\", '000')";

            if (conditions.Schema != null)
                query += " INNER JOIN " + su + " AS SU ON SU.\"OID_USER\" = U.\"OID\"";

            return query;
        }

        internal static string WHERE(QueryConditions conditions)
        {
            string query = string.Empty;

            query = " WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "U");

            if (conditions.User != null)
            {
                if (conditions.User.Oid != 0) query += " AND U.\"OID\" = " + conditions.User.Oid;
                if (conditions.User.Name != string.Empty) query += " AND U.\"NAME\" = '" + conditions.User.Name + "'";
                if (conditions.User.Password != string.Empty) query += " AND U.\"PASSWORD\" = '" + conditions.User.Password + "'";
                if (conditions.User.IsSuperUser == true) query += " AND U.\"MAIN\" = TRUE";
            }
            if (conditions.Schema != null) query += " AND SU.\"OID_SCHEMA\" = " + conditions.Schema.Oid;

            return query;
        }

        internal static string SELECT(long oid, bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions
            {
                User = new UserInfo(oid)
            };

            query =
                SELECT_FIELDS() +
                JOIN(conditions) +
                WHERE(conditions);

            return query;
        }

        public static string SELECT(string username, bool lockTable)
        {
            string query = string.Empty;

            if (username.Contains(" ")) username = string.Empty;

            QueryConditions conditions = new QueryConditions
            {
                User = new UserInfo(0)
            };
            conditions.User.Name = username;

            query = SELECT(conditions, lockTable);

            return query;
        }
        public static string SELECT(string username, string pwd, bool lockTable)
        {
            string query = string.Empty;

            if (username.Contains(" ")) username = string.Empty;
            if (pwd.Contains(" ")) pwd = string.Empty;

            QueryConditions conditions = new QueryConditions
            {
                User = new UserInfo(0)
            };
            conditions.User.Name = username;
            conditions.User.Password = pwd;

            query = SELECT(conditions, lockTable);

            return query;
        }

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string query;

            query =
                SELECT_FIELDS() +
                JOIN(conditions) +
                WHERE(conditions);

            if ((conditions.User == null) || (conditions.User.Name != Properties.Settings.Default.ADMIN_USER))
                query += "	AND U.\"MAIN\" = FALSE";

            query += ORDER((conditions != null) ? conditions.Orders : null, "U", null);

            query += LIMIT(conditions.PagingInfo);

            //if (lockTable) query += " FOR UPDATE OF U NOWAIT";

            return query;
        }

        public static string SELECT(CriteriaEx criteria, bool lockTable)
        {
            QueryConditions conditions = new QueryConditions
            {
                PagingInfo = criteria.PagingInfo,
                Filters = criteria.Filters,
                Orders = criteria.Orders,
				Schema = AppContext.ActiveSchema
            };
            return SELECT(conditions, lockTable);
        }

        public static string SELECT_COUNT() { return SELECT_COUNT(new QueryConditions()); }
        public static string SELECT_COUNT(QueryConditions conditions)
        {
            string query;

            query = @"
                SELECT COUNT(*) AS ""TOTAL_ROWS""" +
                JOIN(conditions) +
                WHERE(conditions);

            if ((conditions.User == null) || (conditions.User.Name != Properties.Settings.Default.ADMIN_USER))
                query += "	AND U.\"MAIN\" = FALSE";

            return query;
        }

        public static string SELECT_SUPER_USER(bool lockTable)
        {
            string query = string.Empty;

            QueryConditions conditions = new QueryConditions
            {
                User = new UserInfo(0)
            };
            conditions.User.IsSuperUser = true;

            query =
                SELECT_FIELDS() +
                JOIN(conditions) +
                WHERE(conditions);

            return query;
        }

        #endregion
    }
}
