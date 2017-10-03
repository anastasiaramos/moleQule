using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
    [Serializable()]
    public class ApplicationSettingRecord : RecordBase
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

        public ApplicationSettingRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _name = Format.DataReader.GetString(source, "NAME");
            _value = Format.DataReader.GetString(source, "VALUE");
        }

        public virtual void CopyValues(ApplicationSettingRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _name = source.Name;
            _value = source.Value;
        }

        #endregion
    }

    [Serializable()]
    public class ApplicationSettingBase
    {
        #region Attributes

        public ApplicationSettingRecord _record = new ApplicationSettingRecord();

        #endregion

        #region Properties

		public ApplicationSettingRecord Record { get { return _record; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(ApplicationSetting source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(ApplicationSettingInfo source)
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
	public class ApplicationSetting : BusinessBaseEx<ApplicationSetting>
	{
		#region Attributes

        public ApplicationSettingBase _base = new ApplicationSettingBase();

		#endregion

		#region  Properties

		public ApplicationSettingBase Base { get { return _base; } }

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
		protected ApplicationSetting() {}
		private ApplicationSetting(int sessionCode, IDataReader reader, bool childs)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}

		internal static ApplicationSetting GetChild(int sessionCode, IDataReader reader, bool childs = false) { return new ApplicationSetting(sessionCode, reader, childs); }

		public virtual ApplicationSettingInfo GetInfo() { return new ApplicationSettingInfo(this); }

		#endregion

		#region Root Factory Methods

		public static ApplicationSetting New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
					Resources.Messages.USER_NOT_ALLOWED);
            
			return DataPortal.Create<ApplicationSetting>(new CriteriaCs(-1));
		}

		public static ApplicationSetting Get(string nombre)
        {
            if (!CanGetObject())
                throw new System.Security.SecurityException(
                  Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = ApplicationSetting.GetCriteria(ApplicationSetting.OpenSession());
            criteria.Query = ApplicationSetting.SELECT_BY_NAME(nombre);
            
            ApplicationSetting.BeginTransaction(criteria.Session);
            return DataPortal.Fetch<ApplicationSetting>(criteria);
        }

		public static ApplicationSetting Get(CriteriaEx criteria)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(
				  Resources.Messages.USER_NOT_ALLOWED);

			ApplicationSetting.BeginTransaction(criteria.Session);
			return DataPortal.Fetch<ApplicationSetting>(criteria);
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

		public override ApplicationSetting Save()
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
				return null;
			}
			finally
			{
				if (CloseSessions) CloseSession();
				else BeginTransaction();
			}
		}

		#endregion

		#region Child Factory Methods

		private ApplicationSetting(ApplicationSetting source)
		{
			MarkAsChild();
			Fetch(source);
		}

		internal static ApplicationSetting GetChild(ApplicationSetting source) { return new ApplicationSetting(source);	}

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
                    ApplicationSetting.DoLOCK(Session());
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
                    ApplicationSettingRecord obj = Session().Get<ApplicationSettingRecord>(Oid);
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
				Session().Delete((ApplicationSettingRecord)(criteria.UniqueResult()));

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

		private void Fetch(ApplicationSetting source)
		{
			_base.CopyValues(source);
			MarkOld();
		}

		private void Fetch(IDataReader reader)
		{
			_base.CopyValues(reader);
			MarkOld();
		}

		internal void Insert(ApplicationSettings parent)
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

		internal void Update(ApplicationSettings parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
                ApplicationSettingRecord obj = Session().Get<ApplicationSettingRecord>(Oid);
				obj.CopyValues(this._base.Record);

				Session().Update(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}

			MarkOld();
		}

		internal void DeleteSelf(ApplicationSettings parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<ApplicationSettingRecord>(Oid));
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

            query = "SELECT VR.*";

            return query;
        }

		internal static string SELECT_BY_NAME(string name, bool lockTable)
        {
            string vr = nHManager.Instance.GetSQLTable(typeof(ApplicationSettingRecord));
            
            string query;

            query = SELECT_FIELDS() +
                    " FROM " + vr + " AS VR";

            if (name != string.Empty) query += " WHERE VR.\"NAME\" = '" + name + "'";

			//if (lockTable) query += " FOR UPDATE OF VR NOWAIT";

            return query;
        }

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string vr = nHManager.Instance.GetSQLTable(typeof(ApplicationSettingRecord));

			string query;

			query = SELECT_FIELDS() +
					" FROM " + vr + " AS VR";

			return query;
		}

        #endregion
    }
}