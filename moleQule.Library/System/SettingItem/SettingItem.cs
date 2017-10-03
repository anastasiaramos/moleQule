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
    public class SettingItemRecord : RecordBase
    {
        #region Attributes

        private string _name = string.Empty;
        private string _comments = string.Empty;
        private bool _copy = false;

        #endregion

        #region Properties
        public virtual string Name { get { return _name; } set { _name = value; } }
        public virtual string Comments { get { return _comments; } set { _comments = value; } }
        public virtual bool Copyable { get { return _copy; } set { _copy = value; } }

        #endregion

        #region Business Methods

        public SettingItemRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _name = Format.DataReader.GetString(source, "NAME");
            _comments = Format.DataReader.GetString(source, "COMMENTS");
            _copy = Format.DataReader.GetBool(source, "COPY");

        }

        public virtual void CopyValues(SettingItemRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _name = source.Name;
            _comments = source.Comments;
            _copy = source.Copyable;
        }
        #endregion
    }

    [Serializable()]
    public class SettingItemBase
    {
        #region Attributes
        
        public SettingItemRecord _record = new SettingItemRecord();

        #endregion

        #region Properties

		public SettingItemRecord Record { get { return _record; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(SettingItem source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(SettingItemInfo source)
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
	public class SettingItem : BusinessBaseEx<SettingItem>
	{
		#region Attributtes

        public SettingItemBase _base = new SettingItemBase();

		#endregion

		#region Properties

		public SettingItemBase Base { get { return _base; } }

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
		public virtual bool Copyable
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.Copyable;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
                if (!_base.Record.Copyable.Equals(value))
				{
                    _base.Record.Copyable = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Comments
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.Comments;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (value == null) value = string.Empty;
                if (_base.Record.Comments != value)
				{
                    _base.Record.Comments = value;
					PropertyHasChanged();
				}
			}
		}

		#endregion

		#region Business Methods

		protected void CopyValues(SettingItem source)
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
		protected SettingItem() { }
		private SettingItem(int sessionCode, IDataReader source, bool childs)
        {
			SessionCode = sessionCode;
            MarkAsChild();
			Childs = childs;
            Fetch(source);
        }

		public virtual SettingItemInfo GetInfo()
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			return new SettingItemInfo(this);
		}

		internal static SettingItem GetChild(int sessionCode, IDataReader source)
		{
			return new SettingItem(sessionCode, source, false);
		}
		internal static SettingItem GetChild(int sessionCode, IDataReader source, bool childs)
		{
			return new SettingItem(sessionCode, source, childs);
		}

		#endregion

		#region Root Factory Methods

		public static SettingItem New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            
			return DataPortal.Create<SettingItem>(new CriteriaCs(-1));
		}

		public static SettingItem Get(long oid)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = SettingItem.GetCriteria(SettingItem.OpenSession());
            criteria.Query = SettingItem.SELECT(oid);
            
            SettingItem.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<SettingItem>(criteria);
        }

		public static SettingItem Get(CriteriaEx criteria)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			SettingItem.BeginTransaction(criteria.Session);
			return DataPortal.Fetch<SettingItem>(criteria);
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

		public override SettingItem Save()
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

		private SettingItem(SettingItem source)
		{
			MarkAsChild();
			Fetch(source);
		}

		public static SettingItem NewChild(SettingItem source)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			SettingItem obj = new SettingItem();
			obj.MarkAsChild();
			obj.CopyValues(source);

			return obj;
		}

		internal static SettingItem GetChild(SettingItem source)
		{
			return new SettingItem(source);
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
			CopyValues(source);

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
                    SettingItem.DoLOCK(Session());
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
					SettingItemRecord obj = Session().Get<SettingItemRecord>(Oid);
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
				Session().Delete((SettingItemRecord)(criteria.UniqueResult()));

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

		private void Fetch(SettingItem source)
		{
			CopyValues(source);
			MarkOld();
		}

		internal void Insert(SettingItems parent)
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

		internal void Update(SettingItems parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				SettingItemRecord obj = Session().Get<SettingItemRecord>(Oid);
				obj.CopyValues(this._base.Record);

				Session().Update(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkOld();
		}

		internal void DeleteSelf(SettingItems parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<SettingItemRecord>(Oid));
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

            query = "SELECT SI.*";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query = string.Empty;

			query += " WHERE TRUE";

			if (conditions.SettingItem != null) query += " AND SI.\"OID\" = " + conditions.SettingItem.Oid;

			return query;
		}

        internal static string SELECT(QueryConditions conditions, bool lock_table)
        {
            string si = nHManager.Instance.GetSQLTable(typeof(SettingItemRecord));
            
            string query;

            query = SELECT_FIELDS() +
					" FROM " + si + " AS SI";

			query += WHERE(conditions);

            if (lock_table) query += " FOR UPDATE OF SI NOWAIT";

            return query;
        }

		internal static string SELECT(long oid, bool lock_table)
		{
            string us = nHManager.Instance.GetSQLTable(typeof(SettingItemRecord));

			string query;

			QueryConditions conditions = new QueryConditions { SettingItem = SettingItem.New().GetInfo() };
			conditions.SettingItem.Oid = oid;

			query = SELECT(conditions, lock_table);

			return query;
		}

        #endregion
    }
}