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
    public class PrivilegeRecord : RecordBase
    {
        #region Attributes

        private long _oid_user;
        private long _oid_item;
        private bool _read = false;
        private bool _create = false;
        private bool _modify = false;
        private bool _delete = false;

        #endregion

        #region Properties
        public virtual long OidUser { get { return _oid_user; } set { _oid_user = value; } }
        public virtual long OidItem { get { return _oid_item; } set { _oid_item = value; } }
        public virtual bool Read { get { return _read; } set { _read = value; } }
        public virtual bool Create { get { return _create; } set { _create = value; } }
        public virtual bool Modify { get { return _modify; } set { _modify = value; } }
        public virtual bool Remove { get { return _delete; } set { _delete = value; } }

        #endregion

        #region Business Methods

        public PrivilegeRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_user = Format.DataReader.GetInt64(source, "OID_USER");
            _oid_item = Format.DataReader.GetInt64(source, "OID_ITEM");
            _read = Format.DataReader.GetBool(source, "READ");
            _create = Format.DataReader.GetBool(source, "CREATE");
            _modify = Format.DataReader.GetBool(source, "MODIFY");
            _delete = Format.DataReader.GetBool(source, "DELETE");

        }

        public virtual void CopyValues(PrivilegeRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_user = source.OidUser;
            _oid_item = source.OidItem;
            _read = source.Read;
            _create = source.Create;
            _modify = source.Modify;
            _delete = source.Remove;
        }
        #endregion
    }

    [Serializable()]
    public class PrivilegeBase
    {
        #region Attributes

        public PrivilegeRecord _record = new PrivilegeRecord();

		public long _item;

        #endregion

        #region Properties

		public PrivilegeRecord Record { get { return _record; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

			_item = Format.DataReader.GetInt64(source, "ITEM");
        }

        internal void CopyValues(Privilege source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

			_item = source.Item;
        }

        #endregion
    }
		
	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
    public class Privilege : BusinessBaseEx<Privilege>
    {
        #region Attributes

        public PrivilegeBase _base = new PrivilegeBase();
        
        private ItemMapList _associated_items = null;
        private ItemMapList _is_associated_item = null;

        #endregion

        #region Properties

		public PrivilegeBase Base { get { return _base; } }

        public override long Oid
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
				return _base.Record.Oid;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.Oid.Equals(value))
                {
					_base.Record.Oid = value;
                    //PropertyHasChanged();
                }
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

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.OidUser.Equals(value))
                {
                    _base.Record.OidUser = value;
                    PropertyHasChanged();
                }
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

                if (!_base.Record.OidItem.Equals(value))
                {
                    _base.Record.OidItem = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Read
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Read;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.Read.Equals(value))
                {
                    _base.Record.Read = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Create
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Create;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.Create.Equals(value))
                {
                    _base.Record.Create = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Modify
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Modify;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.Modify.Equals(value))
                {
                    _base.Record.Modify = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool Remove
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Remove;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.Remove.Equals(value))
                {
                    _base.Record.Remove = value;
                    PropertyHasChanged();
                }
            }
        }		

        public virtual string CanRead
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Read ? Resources.Labels.SET_PRIVILEGES : Resources.Labels.UNSET_PRIVILEGES;
            }
        }
        public virtual string CanCreate
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Create ? Resources.Labels.SET_PRIVILEGES : Resources.Labels.UNSET_PRIVILEGES;
            }
        }        
        public virtual string CanModify
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Modify ? Resources.Labels.SET_PRIVILEGES : Resources.Labels.UNSET_PRIVILEGES;
            }
        }
        public virtual string CanDelete
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Remove ? Resources.Labels.SET_PRIVILEGES : Resources.Labels.UNSET_PRIVILEGES;
            }
        }

        public virtual long Item { get { return _base._item; } set { _base._item = value; } }
		public virtual ESecureItem ESecureItem { get { return (ESecureItem)_base._item; } set { _base._item = (long)value; } }
        public virtual string ItemLabel { get { return EnumTextBase<ESecureItem>.GetLabel(Resources.Enums.ResourceManager, (ESecureItem)_base._item); } }
		public virtual bool All { get { return Create && Read && Modify && Remove; } }

        public virtual ItemMapList AssociatedItems { get { return _associated_items; } set { _associated_items = value; } }
        public virtual ItemMapList IsAssociatedItem { get { return _is_associated_item; } set { _is_associated_item = value; } }

        #endregion

        #region Business Methods
        
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
			return AppContext.User.IsAdmin;
        }

        public static bool CanEditObject()
        {
			return true;
        }

        #endregion

        #region Factory Methods

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New
		/// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
		/// pero es protected por exigencia de NHibernate.
        /// Lo van a usar los AutorizationRulesControler y por eso se hace público
		/// </summary>
		public Privilege() 
        {
            Read = false;
            Create = false;
            Modify = false;
            Remove = false;
        }

		private Privilege(Privilege source)
		{
			MarkAsChild();
			Fetch(source);
		}
        private Privilege(int sessionCode, IDataReader reader)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Fetch(reader);
		}

        public static Privilege NewChild(User parent)
        {
             if (!CanAddObject())
                 throw new System.Security.SecurityException(Resources.Messages.USER_NOT_ALLOWED);

			 Privilege obj = DataPortal.Create<Privilege>(new CriteriaCs(-1));
			 obj.MarkAsChild();
			 obj.OidUser = parent.Oid;
			 return obj;
        }

        internal static Privilege GetChild(Privilege source)
        {
            return new Privilege(source);
        }
        internal static Privilege GetChild(int sessionCode, IDataReader reader, bool childs = false) { return new Privilege(sessionCode, reader); }
        
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
		public override Privilege Save()
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
		
		private void Fetch(Privilege source)
        {
			_base.CopyValues(source);
            MarkOld();

			IDataReader reader;
            string query = string.Empty;

            query = ItemMapList.SELECT_BY_ITEM(this.OidItem);
            reader = nHManager.Instance.SQLNativeSelect(query, Session());
            _associated_items = ItemMapList.GetChildList(reader);

            query = ItemMapList.SELECT_BY_ASSOCIATED_ITEM(this.OidItem);
            reader = nHManager.Instance.SQLNativeSelect(query, Session());
            _is_associated_item = ItemMapList.GetChildList(reader);
        }

        private void Fetch(IDataReader source)
        {
			_base.CopyValues(source);
            MarkOld();

			IDataReader reader;
            string query = string.Empty;

			query = ItemMapList.SELECT_BY_ITEM(this.OidItem);
            reader = nHManager.Instance.SQLNativeSelect(query, Session());
            _associated_items = ItemMapList.GetChildList(reader);

			query = ItemMapList.SELECT_BY_ASSOCIATED_ITEM(this.OidItem);
            reader = nHManager.Instance.SQLNativeSelect(query, Session());
            _is_associated_item = ItemMapList.GetChildList(reader);
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

                PrivilegeRecord obj = parent.Session().Get<PrivilegeRecord>(Oid);
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
				parent.Session().Delete(Session().Get<PrivilegeRecord>(Oid));
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

            query = "SELECT IL.*," +
                    "       SI.\"TIPO\" AS \"ITEM\"";

            return query;
        }
        
        internal static string SELECT(long oid, bool lock_table)
        {
            string il = nHManager.Instance.GetSQLTable(typeof(PrivilegeRecord));
            string si = nHManager.Instance.GetSQLTable(typeof(SecureItemRecord));
            string query;

            query = Privilege.SELECT_FIELDS() +
                    " FROM " + il + " AS IL" +
                    " INNER JOIN " + si + " AS SI ON SI.\"OID\" = IL.\"OID_ITEM\"";
                    
            if (oid != 0) query += " WHERE IL.\"OID\" = " + oid;

            //if (lock_table) query += " FOR UPDATE OF P NOWAIT";

            return query;
        }

        internal static string SELECT(QueryConditions conditions, bool lock_table)
        {
            string il = nHManager.Instance.GetSQLTable(typeof(PrivilegeRecord));
            string si = nHManager.Instance.GetSQLTable(typeof(SecureItemRecord));
            string query;

            query = 
				SELECT_FIELDS() + @"
                FROM " + il + @" AS IL
                INNER JOIN " + si + @" AS SI ON SI.""OID"" = IL.""OID_ITEM""
                WHERE TRUE";

            if (conditions.User != null) query += " AND IL.\"OID_USER\" = " + conditions.User.Oid; 
            
			query += @"
				ORDER BY SI.""NAME""";

            //if (lock_table) query += " FOR UPDATE OF P NOWAIT";

            return query;
        }

        #endregion
    }
}