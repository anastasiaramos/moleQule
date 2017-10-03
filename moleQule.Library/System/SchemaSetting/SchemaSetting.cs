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
    public class SchemaSettingRecord : RecordBase
    {
        #region Attributes

        private string _name = string.Empty;
        private string _value = string.Empty;

        #endregion

        #region Properties
        public virtual string Name { get { return _name; } set { _name = value; } }
        public virtual string Value { get { return _value; } set { _value = value; } }

        #endregion

        #region Business Methods

        public SchemaSettingRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _name = Format.DataReader.GetString(source, "NAME");
            _value = Format.DataReader.GetString(source, "VALUE");

        }

        public virtual void CopyValues(SchemaSettingRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _name = source.Name;
            _value = source.Value;
        }
        #endregion
    }

    [Serializable()]
    public class SchemaSettingBase
    {
        #region Attributes

        public SchemaSettingRecord _record = new SchemaSettingRecord();

        #endregion

        #region Properties

		public SchemaSettingRecord Record { get { return _record; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(SchemaSetting source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(SchemaSettingInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        #endregion
    }
		
	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// Editable Child Business Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class SchemaSetting : BusinessBaseEx<SchemaSetting>
	{
		#region Business Methods

        public SchemaSettingBase _base = new SchemaSettingBase();

		public SchemaSettingBase Base { get { return _base; } }

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
		public virtual string Name
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.Record.Name;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
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
		public virtual string Value
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
                return _base.Record.Value;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);
				if (value == null) value = string.Empty;
                if (_base.Record.Value != value)
				{
                    _base.Record.Value = value;
					PropertyHasChanged();
				}
			}
		}

		protected void CopyValues(SchemaSetting source)
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
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            //Name
            if (Name == string.Empty)
            {
                e.Description = Resources.Messages.NO_FIELD_FILLED;
                throw new iQValidationException(e.Description, string.Empty, "Name");
            }

            return true;
        }

		#endregion

		#region Authorization Rules

		protected override void AddAuthorizationRules()
		{
		}

		public static bool CanAddObject()
		{
			return true;
		}

		public static bool CanGetObject()
		{
			return true;
		}

		public static bool CanDeleteObject()
		{
			return true;
		}

		public static bool CanEditObject()
		{
			return true;
		}

		#endregion

		#region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero es protected por exigencia de NHibernate.
        /// </summary>
		protected SchemaSetting() {}
		private SchemaSetting(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();
			SessionCode = sessionCode;
			Childs = childs;
            Fetch(source);
        }

		internal static SchemaSetting GetChild(int sessionCode, IDataReader source, bool childs = false)
		{
			return new SchemaSetting(sessionCode, source, childs);
		}

		public virtual SchemaSettingInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			return new SchemaSettingInfo(this);
		}

		#endregion

		#region Root Factory Methods

		public static SchemaSetting New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            
			return DataPortal.Create<SchemaSetting>(new CriteriaCs(-1));
		}

		public static SchemaSetting Get(string nombre)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = SchemaSetting.GetCriteria(SchemaSetting.OpenSession());
			criteria.Childs = false;

            criteria.Query = SchemaSetting.SELECT_BY_NAME(nombre);
            
            SchemaSetting.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<SchemaSetting>(criteria);
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

		public override SchemaSetting Save()
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

		private SchemaSetting(SchemaSetting source)
		{
			MarkAsChild();
			Fetch(source);
		}

		internal static SchemaSetting GetChild(SchemaSetting source)
		{
			return new SchemaSetting(source);
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
			try
			{
				CopyValues(source);
			}
			catch (Exception ex) { throw ex; }

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
                    SchemaSetting.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        CopyValues(reader);
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
					SchemaSettingRecord obj = Session().Get<SchemaSettingRecord>(Oid);
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
				Session().Delete((SchemaSettingRecord)(criteria.UniqueResult()));

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

		private void Fetch(SchemaSetting source)
		{
			CopyValues(source);
			MarkOld();
		}

		internal void Insert(SchemaSettings parent)
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

		internal void Update(SchemaSettings parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				SchemaSettingRecord obj = Session().Get<SchemaSettingRecord>(Oid);
				obj.CopyValues(this._base.Record);

				Session().Update(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkOld();
		}

		internal void DeleteSelf(SchemaSettings parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<SchemaSettingRecord>(Oid));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkNew();
		}

		#endregion

        #region SQL

        public static string SELECT_BY_NAME(string name) { return SELECT_BY_NAME(name, true); }

        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT SS.*";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query += " WHERE TRUE";

			if (conditions.SchemaSetting != null) query += " AND SS.\"NAME\" = " + conditions.SchemaSetting.Name;

			return query;
		}

		internal static string SELECT_BASE(QueryConditions conditions)
		{
            string ss = nHManager.Instance.GetSQLTable(typeof(SchemaSettingRecord));

			string query;

			query = SELECT_FIELDS() +
					" FROM " + ss + " AS SS";

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lock_table)
		{
			string query;

			query = SELECT_BASE(conditions) +
					WHERE(conditions);

			//if (lock_table) query += " FOR UPDATE OF SS NOWAIT";

			return query;
		}

		internal static string SELECT(string name, bool lock_table)
		{
			string query;

			QueryConditions conditions = new QueryConditions();
			SchemaSetting schema = SchemaSetting.New();
			schema.Name = name;

			conditions.SchemaSetting = schema.GetInfo();

			query = SELECT(conditions, lock_table);

			return query;
		}

        internal static string SELECT_BY_NAME(string name, bool lock_table) { return SELECT(name, lock_table); }

        #endregion
    }
}