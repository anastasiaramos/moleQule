using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 
using NHibernate;

namespace moleQule.Library
{
    [Serializable()]
    public class SchemaUserRecord : RecordBase
    {
        #region Attributes

        private long _oid_user;
        private long _oid_schema;

        #endregion

        #region Properties

        public virtual long OidUser { get { return _oid_user; } set { _oid_user = value; } }
        public virtual long OidSchema { get { return _oid_schema; } set { _oid_schema = value; } }

        #endregion

        #region Business Methods

        public SchemaUserRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_user = Format.DataReader.GetInt64(source, "OID_USER");
            _oid_schema = Format.DataReader.GetInt64(source, "OID_SCHEMA");

        }

        public virtual void CopyValues(SchemaUserRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_user = source.OidUser;
            _oid_schema = source.OidSchema;
        }
        #endregion
    }

    [Serializable()]
    public class SchemaUserBase
    {
        #region Attributes

        public SchemaUserRecord _record = new SchemaUserRecord();

        #endregion

        #region Properties

		public SchemaUserRecord Record { get { return _record; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(SchemaUser source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(SchemaUserInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        #endregion
    }
		
	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
    public class SchemaUser : BusinessBaseEx<SchemaUser>
    {
        #region Attributes

        public SchemaUserBase _base = new SchemaUserBase();

		#endregion

		#region Properties

		public SchemaUserBase Base { get { return _base; } }

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
                CanReadProperty(true);
                return _base.Record.OidUser;
            }

            set
            {
                CanWriteProperty(true);
                _base.Record.OidUser = value;
                PropertyHasChanged();
            }
        }
        public virtual long OidSchema
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.OidSchema;
            }

            set
            {
                CanWriteProperty(true);
                _base.Record.OidSchema = value;
                PropertyHasChanged();
            }
        }

		#endregion

		#region Business Methods

		protected void CopyValues(SchemaUser source)
        {
			if (source == null) return;

            Oid = source.Oid;
            _base.CopyValues(source);
        }

        protected void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _base.CopyValues(source);
        }

        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CommonRules.MinValue<long>,
									new CommonRules.MinValueRuleArgs<long>("OidUser", 1));
		
			ValidationRules.AddRule(CommonRules.MinValue<long>,
									new CommonRules.MinValueRuleArgs<long>("OidSchema", 1));
		}

        #endregion

        #region Authorization Rules

        protected override void AddAuthorizationRules()
        {

        }

        public static bool CanAddObject() { return User.CanAddObject(); }

        public static bool CanGetObject() { return true; }

        public static bool CanDeleteObject() { return User.CanDeleteObject(); }

        public static bool CanEditObject() { return User.CanEditObject(); }

        #endregion

        #region Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero es protected por exigencia de NHibernate.
        /// </summary>
        protected SchemaUser() {}

        private SchemaUser(SchemaUser source)
        {
            MarkAsChild();
            Fetch(source);
        }
        private SchemaUser(int sessionCode, IDataReader reader)
        {
            MarkAsChild();
			SessionCode = sessionCode;
            Fetch(reader);
        }

		/// <summary>
		/// Crea un nuevo objeto hijo de "parent"
		/// </summary>
		/// <param name="parent"></param>
		/// <returns>SchemaUser</returns>
        public static SchemaUser NewChild(User parent)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			SchemaUser obj = DataPortal.Create<SchemaUser>(new CriteriaCs(-1));
			obj.MarkAsChild();
			obj.OidUser = parent.Oid;
			return obj;
        }
		public static SchemaUser NewChild(User parent, long oid_schema)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			SchemaUser obj = DataPortal.Create<SchemaUser>(new CriteriaCs(-1));
			obj.MarkAsChild();
			obj.OidUser = parent.Oid;
			obj.OidSchema = oid_schema;
			return obj;
		}
		public static SchemaUser NewChild(ISchema parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			SchemaUser obj = DataPortal.Create<SchemaUser>(new CriteriaCs(-1));
			obj.MarkAsChild();
			obj.OidSchema = parent.Oid;
			return obj;
		}

        internal static SchemaUser GetChild(SchemaUser source)
        {
            return new SchemaUser(source);
        }
        internal static SchemaUser GetChild(int sessionCode, IDataReader reader)
        {
            return new SchemaUser(sessionCode, reader);
        }

		/// <summary>
		/// Devuelve el objeto de solo lectura 
		/// </summary>
		/// <returns>SchemaUserInfo</returns>
        public virtual SchemaUserInfo GetInfo()
        {
            return new SchemaUserInfo(this);
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

		/// <summary>
		/// No se debe utilizar esta función para guardar. Hace falta el padre.
		/// Utilizar Insert o Update en sustitución de Save.
		/// </summary>
		/// <returns></returns>
		public override SchemaUser Save()
		{
			throw new iQException(Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
		}

        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
			MarkAsChild();
			_base.Record.Oid = (long)(new Random()).Next();
		}

		#endregion

		#region Child Data Access

		private void Fetch(SchemaUser source)
        {
            CopyValues(source);
            MarkOld();
        }

        private void Fetch(IDataReader reader)
        {
            CopyValues(reader);
            MarkOld();
        }

        internal void Insert(User parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            _base.Record.OidUser = parent.Oid;

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

        internal void Update(User parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            _base.Record.OidUser = parent.Oid;

            try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(Resources.Messages.GENERIC_VALIDATION_ERROR);

                SchemaUserRecord obj = parent.Session().Get<SchemaUserRecord>(Oid);
                obj.CopyValues(this._base.Record);
                parent.Session().Update(obj);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkOld();
        }

        internal void DeleteSelf(User parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
				CriteriaEx criterio = SchemaUser.GetCriteria(parent.Session());
                criterio.AddOidSearch(this.Oid);
                parent.Session().Delete(parent.Session().Get<SchemaUserRecord>(Oid));
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            MarkNew();
        }

        #endregion

		#region SQL

		internal static string SELECT_FIELDS()
		{
			string query;

			query = @"
                SELECT SU.*";

			return query;
		}

		internal static string JOIN(QueryConditions conditions)
		{
            string su = nHManager.Instance.GetSQLTable(typeof(SchemaUserRecord));
			string query;

			query = @"
		        FROM " + su + " AS SU";

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query = @" 
                WHERE TRUE";

			if (conditions.User != null)
				query += @" 
                    AND SU.""OID_USER"" = " + conditions.User.Oid;

			if (conditions.Schema != null) 
                query += @"
                    AND SU.""OID_SCHEMA"" = " + conditions.Schema.Oid;

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
            string su = nHManager.Instance.GetSQLTable(typeof(SchemaUserRecord));
			string query;

			query =
                SELECT_FIELDS() +
                JOIN(conditions) +
                WHERE(conditions);

			//if (lock_table) query += " FOR UPDATE OF SU NOWAIT";

			return query + ";";
		}

		#endregion
    }
}
