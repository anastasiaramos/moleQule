using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;

namespace moleQule.Library
{
    [Serializable()]
    public class UserSettingRecord : RecordBase
    {
        #region Attributes

        private long _oid_user;
        private string _name = string.Empty;
        private string _value = string.Empty;
        private long _oid_setting;

        #endregion

        #region Properties
        public virtual long OidUser { get { return _oid_user; } set { _oid_user = value; } }
        public virtual string Name { get { return _name; } set { _name = value; } }
        public virtual string Value { get { return _value; } set { _value = value; } }
        public virtual long OidSetting { get { return _oid_setting; } set { _oid_setting = value; } }

        #endregion

        #region Business Methods

        public UserSettingRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_user = Format.DataReader.GetInt64(source, "OID_USER");
            _name = Format.DataReader.GetString(source, "NAME");
            _value = Format.DataReader.GetString(source, "VALUE");
            _oid_setting = Format.DataReader.GetInt64(source, "OID_SETTING");

        }

        public virtual void CopyValues(UserSettingRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_user = source.OidUser;
            _name = source.Name;
            _value = source.Value;
            _oid_setting = source.OidSetting;
        }
        #endregion
    }

    [Serializable()]
    public class UserSettingBase
    {
        #region Attributes

        public UserSettingRecord _record = new UserSettingRecord();

        public bool _copyable = false;

        #endregion

        #region Properties

		public UserSettingRecord Record { get { return _record; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            _copyable = Format.DataReader.GetBool(source, "COPYABLE");
        }

        internal void CopyValues(UserSetting source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _copyable = source.Copyable;
        }
        internal void CopyValues(UserSettingInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _copyable = source.Copyable;
        }
        #endregion
    }
		
	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// Editable Child Business Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class UserSetting : BusinessBaseEx<UserSetting>
	{
		#region Attributtes

        public UserSettingBase _base = new UserSettingBase();

		#endregion

		#region Properties

		public UserSettingBase Base { get { return _base; } }

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
				//CanWriteProperty(true);
				_base.Record.Oid = value;
			}
		}
		public virtual long OidUser
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidUser;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidUser.Equals(value))
				{
					_base.Record.OidUser = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long OidSetting
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidSetting;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (!_base.Record.OidSetting.Equals(value))
				{
					_base.Record.OidSetting = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Name
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Name;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (_base.Record.Name != value)
				{
					_base.Record.Name = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Value
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Value;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (_base.Record.Value != value)
				{
					_base.Record.Value = value;
					PropertyHasChanged();
				}
			}
		}

		public virtual bool Copyable { get { return _base._copyable; } }

		#endregion

		#region Business Methods

		public virtual void CopyFrom(User parent, UserSetting source)
		{
			OidUser = parent.Oid;

			if (source != null)
			{
				OidSetting = source.OidSetting;
				Name = source.Name;
				if (source.Copyable) Value = source.Value;
			}
		}
		
		#endregion

		#region Validation Rules

		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CommonRules.StringRequired, "Name");
		}

		#endregion

		#region Authorization Rules

		protected override void AddAuthorizationRules() {}

		public static bool CanAddObject() { return true; }
		public static bool CanGetObject() { return true; }
		public static bool CanDeleteObject() { return true;	}
		public static bool CanEditObject() { return true; }

		#endregion

		#region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero es protected por exigencia de NHibernate.
        /// </summary>
		protected UserSetting() { }
		private UserSetting(int sessionCode, IDataReader source, bool childs)
        {
			SessionCode = sessionCode;
            MarkAsChild();
			Childs = childs;
            Fetch(source);
        }

		public virtual UserSettingInfo GetInfo() { return new UserSettingInfo(this); }

		internal static UserSetting GetChild(int sessionCode, IDataReader source, bool childs = false)
		{
			return new UserSetting(sessionCode, source, childs);
		}

		#endregion

		#region Root Factory Methods

		public static UserSetting New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            
			return DataPortal.Create<UserSetting>(new CriteriaCs(-1));
		}

		public static UserSetting Get(long oid)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = UserSetting.GetCriteria(UserSetting.OpenSession());
            criteria.Query = UserSetting.SELECT(oid);
            
            UserSetting.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<UserSetting>(criteria);
        }

		public static UserSetting Get(CriteriaEx criteria)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			UserSetting.BeginTransaction(criteria.Session);
			return DataPortal.Fetch<UserSetting>(criteria);
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

		public override UserSetting Save()
		{
			// Por la doble interfaz Root/Child
			if (IsChild) throw new iQException(Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            try
            {
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

				Transaction().Commit();
				
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
				if (CloseSessions) CloseSession();
				else BeginTransaction();
			}
		}

		#endregion

		#region Child Factory Methods

		private UserSetting(UserSetting source)
		{
			MarkAsChild();
			Fetch(source);
		}

		public static UserSetting NewChild(User parent, UserSetting source)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			UserSetting obj = new UserSetting();
			obj.MarkAsChild();
			obj.CopyFrom(parent, source);

			return obj;
		}

		internal static UserSetting GetChild(UserSetting source)
		{
			return new UserSetting(source);
		}

		/// <summary>
		/// Borrado aplazado, es posible el undo 
		/// (La función debe ser "no estática")
		/// </summary>
		public override void Delete()
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			MarkDeleted();
		}

		#endregion

		#region Common Data Access

		[RunLocal()]
		private void DataPortal_Create(long oid)
		{
			Random rd = new Random();
			Oid = rd.Next();
		}

		private void Fetch(IDataReader source)
		{
			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{
				}
			}

			MarkOld();
		}

		#endregion

		#region Root Data Access

		// called to retrieve data from database
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
                if (nHMng.UseDirectSQL)
                {
                    UserSetting.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);
                }
			}
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
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
				Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		}

		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
					UserSettingRecord obj = Session().Get<UserSettingRecord>(Oid);
					obj.CopyValues(this._base.Record);
					Session().Update(obj);
				}
				catch (Exception ex)
				{
					throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
				}
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
		private void DataPortal_Delete(CriteriaCs criterio)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();
				
				CriteriaEx criteria = GetCriteria();
				criteria.AddOidSearch(criteria.Oid);
				Session().Delete((UserSettingRecord)(criteria.UniqueResult()));

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

		private void Fetch(UserSetting source)
		{
			_base.CopyValues(source);
			MarkOld();
		}

		internal void Insert(UserSettings parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkOld();
		}

		internal void Update(UserSettings parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				UserSettingRecord obj = Session().Get<UserSettingRecord>(Oid);
				obj.CopyValues(this._base.Record);

				Session().Update(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkOld();
		}

		internal void DeleteSelf(UserSettings parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<UserSettingRecord>(Oid));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkNew();
		}

		#endregion

        #region SQL

		public new static string SELECT(long oid) { return SELECT(oid, true); }
        public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

        internal static string SELECT_FIELDS()
        {
            string query;

			query =
			"	SELECT	US.*" +
			"			,SI.\"COPY\" AS \"COPYABLE\"";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query += " WHERE TRUE";

			if (conditions.UserSetting != null) query += " AND US.\"OID\" = " + conditions.UserSetting.Oid;
			if (conditions.User != null) query += " AND US.\"OID_USER\" = " + conditions.User.Oid;

			return query;
		}

        internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
            string us = nHManager.Instance.GetSQLTable(typeof(UserSettingRecord));
            string si = nHManager.Instance.GetSQLTable(typeof(SettingItemRecord));
            
            string query;

			query =
				SELECT_FIELDS() +
			"	FROM " + us + " AS US" +
			"	INNER JOIN " + si + " AS SI ON SI.\"OID\" = US.\"OID_SETTING\"";


			query += WHERE(conditions);

            //if (lock_table) query += " FOR UPDATE OF US NOWAIT";

            return query;
        }

		internal static string SELECT(long oid, bool lockTable)
		{
            string us = nHManager.Instance.GetSQLTable(typeof(UserSettingRecord));

			string query;

			QueryConditions conditions = new QueryConditions { UserSetting = UserSetting.New().GetInfo() };
			conditions.UserSetting.Oid = oid;

			query = SELECT(conditions, lockTable);

			return query;
		}

        #endregion
    }
}