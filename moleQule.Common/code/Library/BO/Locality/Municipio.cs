using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx;
using NHibernate;
using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class LocalityRecord : RecordBase
    {
        #region Attributes

        private string _valor = string.Empty;
        private string _provincia = string.Empty;
        private string _cod_postal = string.Empty;
        private string _localidad = string.Empty;
        private string _pais = string.Empty;

        #endregion

        #region Properties

        public virtual string Valor { get { return _valor; } set { _valor = value; } }
        public virtual string Provincia { get { return _provincia; } set { _provincia = value; } }
        public virtual string CodPostal { get { return _cod_postal; } set { _cod_postal = value; } }
        public virtual string Localidad { get { return _localidad; } set { _localidad = value; } }
        public virtual string Pais { get { return _pais; } set { _pais = value; } }

        #endregion

        #region Business Methods

		public LocalityRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _valor = Format.DataReader.GetString(source, "VALOR");
            _provincia = Format.DataReader.GetString(source, "PROVINCIA");
            _cod_postal = Format.DataReader.GetString(source, "COD_POSTAL");
            _localidad = Format.DataReader.GetString(source, "LOCALIDAD");
            _pais = Format.DataReader.GetString(source, "PAIS");
        }

		public virtual void CopyValues(LocalityRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _valor = source.Valor;
            _provincia = source.Provincia;
            _cod_postal = source.CodPostal;
            _localidad = source.Localidad;
            _pais = source.Pais;
        }
        #endregion
    }

    [Serializable()]
	public class LocalityBase
    {
        #region Attributes

        private LocalityRecord _record = new LocalityRecord();

        #endregion

        #region Properties

		public LocalityRecord Record { get { return _record; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(Municipio source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(MunicipioInfo source)
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
    public class Municipio : BusinessBaseEx<Municipio>
    {
        #region Attributes

		public LocalityBase _base = new LocalityBase();

        #endregion

        #region Properties

		public LocalityBase Base { get { return _base; } }

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
        public virtual string Localidad
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Localidad;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Localidad != value)
                {
					_base.Record.Localidad = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Nombre
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Valor;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Valor != value)
                {
					_base.Record.Valor = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual string Provincia
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.Provincia;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.Provincia != value)
                {
					_base.Record.Provincia = value.ToUpper();
                    PropertyHasChanged();
                }
            }
        }
		public virtual string Pais
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Pais;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (_base.Record.Pais != value)
				{
					_base.Record.Pais = value.ToUpper();
					PropertyHasChanged();
				}
			}
		}
        public virtual string CodPostal
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                //CanReadProperty(true);
				return _base.Record.CodPostal;
            }
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                //CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (_base.Record.CodPostal != value)
                {
					_base.Record.CodPostal = value;
                    PropertyHasChanged();
                }
            }
        }
        
        #endregion

        #region Business Methods

		public static Municipio CloneAsNew(MunicipioInfo source)
		{
			Municipio clon = Municipio.New();
			clon._base.CopyValues(source);

			clon.MarkNew();

			return clon;
		}

        #endregion

        #region Validation Rules

        /// <summary>
        /// Añade las reglas de validación necesarias para el objeto
        /// </summary>
        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {


            //Propiedad
            /*if (Propiedad <= 0)
            {
                e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "Propiedad");
                throw new iQValidationException(e.Description, string.Empty);
            }*/

            return true;
        }	

        #endregion

        #region Authorization Rules

        protected override void AddAuthorizationRules()
        {
        }

        public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.AUXILIARES)
					|| AppContext.User.IsAdmin;
        }

        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.AUXILIARES)
					|| AppContext.User.IsAdmin;
        }

        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.AUXILIARES)
					|| AppContext.User.IsAdmin;
        }

        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.AUXILIARES)
					|| AppContext.User.IsAdmin;
        }

        #endregion

        #region Common Factory Methods

        /// <summary>
        /// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
        /// Debería ser private para CSLA porque la creación requiere el uso de los factory methods,
        /// pero debe ser protected por exigencia de NHibernate
        /// y public para que funcionen los DataGridView
        /// </summary>
        public Municipio()
        {
            Random r = new Random();
            Oid = (long)r.Next();
        }
		private Municipio(Municipio source)
		{
			MarkAsChild();
			Fetch(source);
		}
		private Municipio(int sessionCode, IDataReader reader, bool childs)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}

		public virtual MunicipioInfo GetInfo() { return new MunicipioInfo(this); }
        
        #endregion

        #region Root Factory Methods
		
		public static Municipio New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Municipio>(new CriteriaCs(-1));
		}
			
		public static Municipio Get(long oid)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = Municipio.GetCriteria(Municipio.OpenSession());
		
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = Municipio.SELECT(oid);
			
			Municipio.BeginTransaction(criteria.Session);			
			return DataPortal.Fetch<Municipio>(criteria);
		}	
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Municipio. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Municipio.OpenSession();
			ISession sess = Municipio.Session(sessCode);
			ITransaction trans = Municipio.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from LocalityRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				Municipio.CloseSession(sessCode);
			}
		}
		
		public override Municipio Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) 
                throw new iQException(
                    moleQule.Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(
                    moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(
                    moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
			{
				throw new System.Security.SecurityException(
                    moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
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

        internal static Municipio NewChild()
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			return new Municipio();
        }

		internal static Municipio GetChild(Municipio source) { return new Municipio(source); }
		internal static Municipio GetChild(int sessionCode, IDataReader reader, bool childs = false) { return new Municipio(sessionCode, reader, childs); }
		
		/// <summary>
		/// Borrado aplazado, es posible el undo 
		/// (La función debe ser "no estática")
		/// </summary>
		public override void Delete()
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);			
			
			MarkDeleted();
		}

        #endregion

        #region Common Data Access

        [RunLocal()]
        private void DataPortal_Create(CriteriaCs criteria)
        {
			_base.Record.Oid = (long)(new Random()).Next();
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            try
            {
                _base.Record.Oid = 0;
                SessionCode = criteria.SessionCode;
                Childs = criteria.Childs;

                if (nHMng.UseDirectSQL)
                {
                    Municipio.DoLOCK(Session());
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        _base.CopyValues(reader);

                    if (Childs)
                    {
                        string query = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
        }

        //Fetch independiente de DataPortal para generar un Municipio a partir de un IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);
            }
            catch (Exception ex)
            {
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
                iQExceptionHandler.TreatException(ex);
            }
        }

        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_Update()
        {
            if (IsDirty)
            {
				LocalityRecord obj = Session().Get<LocalityRecord>(Oid);
                obj.CopyValues(this._base.Record);
                Session().Update(obj);
            }
        }

        //Deferred deletion
        [Transactional(TransactionalTypes.Manual)]
        protected override void DataPortal_DeleteSelf()
        {
            DataPortal_Delete(new CriteriaCs(Oid));
        }

        [Transactional(TransactionalTypes.Manual)]
        private void DataPortal_Delete(CriteriaCs criteria)
        {
            try
            {
                // Iniciamos la conexion y la transaccion
                SessionCode = OpenSession();
                BeginTransaction();

                //Si no hay integridad referencial, aquí se deben borrar las listas hijo
                CriteriaEx criterio = GetCriteria();
                criterio.AddOidSearch(criteria.Oid);
				Session().Delete((LocalityRecord)(criterio.UniqueResult()));
                Transaction().Commit();
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                CloseSession();
            }
        }

        #endregion

		#region Child Data Access

        private void Fetch(Municipio source)
        {
			_base.CopyValues(source);
            MarkOld();
        }

		internal void Insert(Municipios parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
                iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

        internal void Update(Municipios parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
                ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				LocalityRecord obj = Session().Get<LocalityRecord>(Oid);
				obj.CopyValues(this._base.Record);
                Session().Update(obj);
			}
			catch (Exception ex)
			{
                iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

        internal void DeleteSelf(Municipios parent)
        {
            // if we're not dirty then don't update the database
            if (!this.IsDirty) return;

            // if we're new then don't update the database
            if (this.IsNew) return;

            try
            {
				SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<LocalityRecord>(Oid));
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex);
            }

            MarkNew();
        }

        #endregion

		#region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
		{
			return new Dictionary<String, ForeignField>() { };
		}

		public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }

		internal static string SELECT_FIELDS()
		{
			string query;

			query = @"
				SELECT MU.*";

			return query;
		}

		internal static string JOIN(QueryConditions conditions)
		{
			string pu = nHManager.Instance.GetSQLTable(typeof(LocalityRecord));

			string query;

			query = @"
				FROM " + pu + @" AS MU";

			return query + " " + conditions.ExtraJoin;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "MU", ForeignFields());

			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "MU");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "MU");

			if (conditions.Municipio != null)
				query += @"
					AND MU.""OID"" = " + conditions.Municipio.Oid;

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query =
				SELECT_FIELDS() +
				JOIN(conditions) +
				WHERE(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "MU", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += Common.EntityBase.LOCK("MU", lockTable);

			return query;
		}

		public static string SELECT(CriteriaEx criteria, bool lockTable)
		{
			QueryConditions conditions = new QueryConditions
			{
				PagingInfo = criteria.PagingInfo,
				Filters = criteria.Filters,
				Orders = criteria.Orders
			};
			return SELECT(conditions, lockTable);
		}

		internal static string SELECT(long oid, bool lockTable)
		{
			return SELECT(new QueryConditions { Municipio = MunicipioInfo.New(oid) }, lockTable);
		}

		#endregion
    }
}
