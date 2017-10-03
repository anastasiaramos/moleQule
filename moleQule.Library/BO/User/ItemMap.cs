using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
    [Serializable()]
    public class ItemMapRecord : RecordBase
    {
        #region Attributes

        private long _oid_item;
        private long _privilege;
        private long _oid_associate_item;
        private long _associate_privilege;

        #endregion

        #region Properties
        public virtual long OidItem { get { return _oid_item; } set { _oid_item = value; } }
        public virtual long Privilege { get { return _privilege; } set { _privilege = value; } }
        public virtual long OidAssociateItem { get { return _oid_associate_item; } set { _oid_associate_item = value; } }
        public virtual long AssociatePrivilege { get { return _associate_privilege; } set { _associate_privilege = value; } }

        #endregion

        #region Business Methods

        public ItemMapRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_item = Format.DataReader.GetInt64(source, "OID_ITEM");
            _privilege = Format.DataReader.GetInt64(source, "PRIVILEGE");
            _oid_associate_item = Format.DataReader.GetInt64(source, "OID_ASSOCIATE_ITEM");
            _associate_privilege = Format.DataReader.GetInt64(source, "ASSOCIATE_PRIVILEGE");

        }

        public virtual void CopyValues(ItemMapRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_item = source.OidItem;
            _privilege = source.Privilege;
            _oid_associate_item = source.OidAssociateItem;
            _associate_privilege = source.AssociatePrivilege;
        }
        #endregion
    }

    [Serializable()]
    public class ItemMapBase
    {
        #region Attributes

        public ItemMapRecord _record = new ItemMapRecord();

        #endregion

        #region Properties

		public ItemMapRecord Record { get { return _record; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(ItemMap source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(ItemMapInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        #endregion
    }
		
	/// <summary>
	/// Editable Root Business Object With Editable Child Collection
	/// </summary>
    [Serializable()]
    public class ItemMap : BusinessBaseEx<ItemMap>
    {
        #region Attributes

        public ItemMapBase _base = new ItemMapBase();

		#endregion

		#region Properties

		public ItemMapBase Base { get { return _base; } }

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
        public virtual long OidItem
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.OidItem;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (_base.Record.OidItem != value)
                {
                    _base.Record.OidItem = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long Privilege
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Privilege;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (_base.Record.Privilege != value)
                {
                    _base.Record.Privilege = value;
                    PropertyHasChanged();
                }
            }
        }       
        public virtual long OidAssociateItem
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.OidAssociateItem;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (_base.Record.OidAssociateItem != value)
                {
                    _base.Record.OidAssociateItem = value;
                    PropertyHasChanged();
                }
            }
        }        
        public virtual long AssociatePrivilege
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.AssociatePrivilege;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (_base.Record.AssociatePrivilege != value)
                {
                    _base.Record.AssociatePrivilege = value;
                    PropertyHasChanged();
                }
            }
        }

        public virtual EPrivilege TipoPrivilegio { get { return (EPrivilege)_base.Record.Privilege; } }
        public virtual string TipoPrivilegioLabel { get { return moleQule.Library.EnumTextBase<EPrivilege>.GetLabel(Resources.Enums.ResourceManager, TipoPrivilegio); } }
        public virtual EPrivilege TipoPrivilegioAsociado { get { return (EPrivilege)_base.Record.AssociatePrivilege; } }
        public virtual string TipoPrivilegioAsociadoLabel { get { return moleQule.Library.EnumTextBase<EPrivilege>.GetLabel(Resources.Enums.ResourceManager, TipoPrivilegioAsociado); } }
        
        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            //OidItem
            if (OidItem == 0)
            {
                e.Description = Resources.Messages.NO_FIELD_SELECTED;
                throw new iQValidationException(e.Description, string.Empty, "OidItem");
            }

            //Privilege
            if (Privilege == 0)
            {
                e.Description = Resources.Messages.NO_FIELD_SELECTED;
                throw new iQValidationException(e.Description, string.Empty, "Privilege");
            }

            //OidAssociateItem
            if (OidAssociateItem == 0)
            {
                e.Description = Resources.Messages.NO_FIELD_SELECTED;
                throw new iQValidationException(e.Description, string.Empty, "OidAssociateItem");
            }

            //AssociatePrivilege
            if (AssociatePrivilege == 0)
            {
                e.Description = Resources.Messages.NO_FIELD_SELECTED;
                throw new iQValidationException(e.Description, string.Empty, "AssociatePrivilege");
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
			return AppContext.User.IsAdmin;
        }

        public static bool CanGetObject()
        {
			return AppContext.User.IsAdmin;
        }

        public static bool CanDeleteObject()
        {
			return AppContext.User.IsAdmin;
        }

        public static bool CanEditObject()
        {
			return AppContext.User.IsAdmin;
        }

        #endregion

        #region Factory Methods

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USA LA FUNCION New
		/// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
		/// pero es protected por exigencia de NHibernate.
		/// </summary>
        protected ItemMap() { }

        public static ItemMap New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(
                  Resources.Messages.USER_NOT_ALLOWED);

            return DataPortal.Create<ItemMap>(new CriteriaCs(-1));
        }

        public static ItemMap Get(long oid)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(
				  Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = ItemMap.GetCriteria(ItemMap.OpenSession());
            criteria.AddOidSearch(oid);
            ItemMap.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<ItemMap>(criteria);
        }

        public virtual ItemMapInfo GetInfo()
        {
            return new ItemMapInfo(Oid, _base.Record.OidItem, _base.Record.Privilege, _base.Record.OidAssociateItem, _base.Record.AssociatePrivilege);
        }

		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
        public static void Delete(long oid)
        {
			if (!CanDeleteObject())
                throw new System.Security.SecurityException(
				  Resources.Messages.USER_NOT_ALLOWED);
            
			DataPortal.Delete(new CriteriaCs(oid));
        }

        public override ItemMap Save()
        {
			// Por la posible doble interfaz Root/Child
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
				return null;
			}
			finally
			{
				if (CloseSessions) CloseSession();
				else BeginTransaction();
			}
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

				_base.CopyValues((ItemMap)(criteria.UniqueResult()));
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.SetCaller(ex, "ItemMap::DataPortal_Fetch");
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
                    ItemMapRecord obj = Session().Get<ItemMapRecord>(Oid);
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
        private void DataPortal_Delete(CriteriaCs criteria)
        {
            try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();

				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
                Session().Delete((ItemMapRecord)(criterio.UniqueResult()));
                
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

		#region SQL

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

		public static string SELECT_FIELDS()
		{
			string query = @"
				SELECT IM.*";

			return query;
		}

		internal static string JOIN(QueryConditions conditions)
		{
			string im = nHManager.Instance.GetSQLTable(typeof(ItemMapRecord));

			string query;

			query = @"
				FROM " + im + @" AS IM";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = @" 
				WHERE TRUE " ;

			if (conditions.ItemMap != null)
				query += @"
					AND IM.""OID"" = " + conditions.ItemMap.Oid;

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query;

			query =
				SELECT_FIELDS() +
				JOIN(conditions) +
				WHERE(conditions);

			if (conditions.PagingInfo != null) query += LIMIT(conditions.PagingInfo);

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			return SELECT(new QueryConditions { ItemMap = ItemMapInfo.New(oid) }, lockTable);
		}

		internal static string SELECT_BY_ITEM(long oidItem, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions();

			conditions.ExtraWhere = @"
				AND IM.""OID_ITEM"" = " + oidItem;

			return SELECT(conditions, lockTable);
		}

		internal static string SELECT_BY_ASSOCIATED_ITEM(long oidItem, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions();

			conditions.ExtraWhere = @"
				AND IM.""OID_ASSOCIATE_ITEM"" = " + oidItem;

			return SELECT(conditions, lockTable);
		}

		#endregion
    }
}
