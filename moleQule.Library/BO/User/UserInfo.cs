using System;
using System.Data;
using System.Collections.Generic;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
    [Serializable()]
    public class UserInfo : ReadOnlyBaseEx<UserInfo, User>
    {
        #region Attributes

		public UserBase _base = new UserBase();

		#endregion

		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Code { get { return _base.Record.Code; } }
		public long EntityType { get { return _base.Record.EntityType; } }
		public long OidEntity { get { return _base.Record.OidEntity; } }
		public string Name { get { return _base.Record.Name; } set { _base.Record.Name = value; } }
		public string Password { get { return _base.Record.Password; } set { _base.Record.Password = value; } }
		public string Email { get { return _base.Record.Email; } set { _base.Record.Email = value; } }
		public bool IsAdmin { get { return _base.Record.IsAdmin; } set { _base.Record.IsAdmin = value; } }
		public bool IsSuperUser { get { return _base.Record.IsSuperUser; } set { _base.Record.IsSuperUser = value; } }
		public bool IsPartner { get { return _base.Record.IsPartner; } set { _base.Record.IsPartner = value; } }
        public bool IsService { get { return _base.Record.IsService; } set { _base.Record.IsService = value; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string Pin { get { return _base.Record.Pin; } set { _base.Record.Pin = value; } }
		public DateTime CreationDate { get { return _base.Record.CreationDate; } set { _base.Record.CreationDate = value; } }
		public DateTime LastLoginDate { get { return _base.Record.LastLoginDate; } set { _base.Record.LastLoginDate = value; } }
		public DateTime LastPasswordDate { get { return _base.Record.LastPasswordDate; } set { _base.Record.LastPasswordDate = value; } }
		public DateTime LastLockedOutDate { get { return _base.Record.LastLockedOutDate; } set { _base.Record.LastLockedOutDate = value; } }
		public DateTime LastActivityDate { get { return _base.Record.LastActivityDate; } set { _base.Record.LastActivityDate = value; } }
		public DateTime BirthDate { get { return _base.Record.BirthDate; } set { _base.Record.BirthDate = value; } }
		public string PasswordQuestion { get { return _base.Record.PasswordQuestion; } set { _base.Record.PasswordQuestion = value; } }
		public string PasswordResponse { get { return _base.Record.PasswordResponse; } set { _base.Record.PasswordResponse = value; } }

		public EEstadoItem EEstado { get { return _base.EEstado; } }
		public string EstadoLabel { get { return _base.EstadoLabel; } }
		public ERol Rol { get { return _base.Rol; } }
		public bool IsUser { get { return _base.IsUser; } }
		public bool IsClient { get { return _base.IsClient; } set { _base.IsClient = value; } }
		public bool IsProvider { get { return _base.IsProvider; } }
		public bool IsAuthenticated { get { return _base._is_authenticated; } set { _base._is_authenticated = value; } }
		public bool IsApproved { get { return _base.IsApproved; } set {} }
		public bool IsLockedOut { get { return _base.IsLockedOut; } set {} }
		public string PlainPwd { get { return _base._plain_pwd; } set { _base._plain_pwd = value; } }
		public long DefaultOidSchema { get { return _base._default_oid_schema; } set { _base._default_oid_schema = value; } }
		public string DefaultSchema { get { return _base._default_schema; } set { _base._default_schema = value; } }
        public string DefaultSchemaCode { get { return _base._default_schema_code; } set { _base._default_schema_code = value; } }

		public long Status { get { return _base.Record.Estado; } }
		public string StatusLabel { get { return _base.EstadoLabel; } }

		#endregion

		#region Business Methods

		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

			//Se copia especificamente porque el Set del password modifica el password
			//para convertirlo a MD5

			Oid = Format.DataReader.GetInt64(source, "OID");
			_base.CopyValues(source);
		}
        protected void CopyValues(User source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
		}

		#endregion

		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected UserInfo() { /* require use of factory methods */ }
		private UserInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
        internal UserInfo(User item, bool copy_childs)
		{
			CopyValues(item);
			
			if (copy_childs)
			{
				//_periodos = (item.Periodos != null) ? AyudaPeriodoList.GetChildList(item.Periodos) : null;				
			}
		}
        internal UserInfo(long oid)
		{
			Oid = oid;
		}

		public static UserInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static UserInfo GetChild(int sessionCode, IDataReader reader, bool childs) { return new UserInfo(sessionCode, reader, childs); }

		public static UserInfo New(long oid = 0) { return new UserInfo() { Oid = oid }; }

 		#endregion

		#region Root Factory Methods

		public static UserInfo Get(long oid, bool childs = false)
		{
            if (!User.CanGetObject()) throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			return ReadOnlyBaseEx<UserInfo, User>.Get(SELECT(oid), childs);
		}
		public new static UserInfo Get(string username, bool childs = false)
		{
			return ReadOnlyBaseEx<UserInfo, User>.Get(User.SELECT(username, false), childs);
		}
		public static UserInfo Get(string username, string password, bool childs = false)
		{
			return ReadOnlyBaseEx<UserInfo, User>.Get(User.SELECT(username, ClassMD5.getMd5Hash(password), false), childs);
		}
        public static UserInfo GetByEmail(string email, bool childs = false)
        {
            if (email.Contains(" ")) email = string.Empty;

            QueryConditions conditions = new QueryConditions
            {
                User = new UserInfo(-1)
            };
            conditions.User.Email = email;
            return ReadOnlyBaseEx<UserInfo, User>.Get(User.SELECT(conditions, false), childs);
        }

		#endregion	
				
		#region Common Data Access

		/// <summary>
		/// Obtiene un objeto a partir de un <see cref="IDataReader"/>.
		/// Obtiene los hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="criteria"><see cref="IDataReader"/> con los datos</param>
		/// <remarks>
		/// La utiliza el <see cref="ReadOnlyListBaseEx"/> correspondiente para construir los objetos de la lista
		/// </remarks>
		private void Fetch(IDataReader source)
		{
			try
			{
				CopyValues(source);

				if (Childs)
				{
				}
			}
			catch (Exception ex) { throw ex; }
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
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					if (reader.Read())
						CopyValues(reader);

					if (Childs)
					{
					}
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex, new object[] { criteria.Query }); }
		}

		#endregion

		#region SQL

        public static string SELECT(long oid) { return User.SELECT(oid, false); }
        public static string SELECT(QueryConditions conditions) { return User.SELECT(conditions, false); }

		#endregion		
    }
}
