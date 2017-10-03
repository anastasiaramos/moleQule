using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
    [Serializable()]
    public class SecureItemRecord : RecordBase
    {
        #region Attributes

        private string _name = string.Empty;
        private long _tipo;
        private string _descriptor = string.Empty;

        #endregion

        #region Properties
        public virtual string Name { get { return _name; } set { _name = value; } }
        public virtual long Tipo { get { return _tipo; } set { _tipo = value; } }
        public virtual string Descriptor { get { return _descriptor; } set { _descriptor = value; } }

        #endregion

        #region Business Methods

        public SecureItemRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _name = Format.DataReader.GetString(source, "NAME");
            _tipo = Format.DataReader.GetInt64(source, "TIPO");
            _descriptor = Format.DataReader.GetString(source, "DESCRIPTOR");

        }

        public virtual void CopyValues(SecureItemRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _name = source.Name;
            _tipo = source.Tipo;
            _descriptor = source.Descriptor;
        }
        #endregion
    }

    [Serializable()]
    public class SecureItemBase
    {
        #region Attributes

        public SecureItemRecord _record = new SecureItemRecord();

        #endregion

        #region Properties

		public SecureItemRecord Record { get { return _record; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(SecureItem source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(SecureItemInfo source)
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
    public class SecureItem : BusinessBaseEx<SecureItem>
    {
        #region Attributes

        public SecureItemBase _base = new SecureItemBase();

		#endregion

		#region Properties

		public SecureItemBase Base { get { return _base; } }

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
        public virtual long Tipo
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Tipo;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (_base.Record.Tipo != value)
                {
                    _base.Record.Tipo = value;
                    PropertyHasChanged();
                }
            }
        }

        #endregion

        #region Validation Rules        

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            if (_base.Record.Tipo == 0)
                throw new iQValidationException(Resources.Messages.SECURE_ITEM_TIPO_NULL, string.Empty);
            if (_base.Record.Name == string.Empty)
                throw new iQValidationException(Resources.Messages.SECURE_ITEM_NAME_EMPTY, string.Empty);

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
            return true;// AppContext.User.IsAdmin;
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
        protected SecureItem() { }

        public static SecureItem New()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(
                  Resources.Messages.USER_NOT_ALLOWED);
            
			return DataPortal.Create<SecureItem>(new CriteriaCs(-1));
        }

        public static SecureItem Get(long oid)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = SecureItem.GetCriteria(SecureItem.OpenSession());
            criteria.AddOidSearch(oid);
            SecureItem.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<SecureItem>(criteria);
        }

        public virtual SecureItemInfo GetInfo() { return new SecureItemInfo(this); }

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

        public override SecureItem Save()
        {
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

            if (IsDeleted && !CanDeleteObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            else if (IsNew && !CanAddObject())
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            else if (!CanEditObject())
            {
                throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);
            }

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
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;

				_base.CopyValues((SecureItem)(criteria.UniqueResult()));
            }
			catch (NHibernate.ADOException)
			{
				if (Transaction() != null) Transaction().Rollback();
				throw new iQLockException(Resources.Messages.LOCK_ERROR);
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
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
                    SecureItemRecord obj = Session().Get<SecureItemRecord>(Oid);
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
                Session().Delete((SecureItemRecord)(criterio.UniqueResult()));
                
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

		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = "SELECT SI.*";

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE TRUE";

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string si = nHManager.Instance.GetSQLTable(typeof(SecureItemRecord));

			string query;

			query = SELECT_FIELDS() +
					" FROM " + si + " AS SI";

			query += WHERE(conditions);

			return query;
		}

		#endregion
    }
}
