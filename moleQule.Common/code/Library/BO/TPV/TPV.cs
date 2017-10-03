using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx;
using NHibernate;
using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class TPVRecord : RecordBase
    {
        #region Attributes

        private long _oid_cuenta_bancaria;
        private string _nombre = string.Empty;
        private string _cuenta_contable = string.Empty;
        private string _observaciones = string.Empty;
        private Decimal _p_comision;

        #endregion

        #region Properties
        public virtual long OidCuentaBancaria { get { return _oid_cuenta_bancaria; } set { _oid_cuenta_bancaria = value; } }
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual string CuentaContable { get { return _cuenta_contable; } set { _cuenta_contable = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
        public virtual Decimal PComision { get { return _p_comision; } set { _p_comision = value; } }

        #endregion

        #region Business Methods

        public TPVRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _oid_cuenta_bancaria = Format.DataReader.GetInt64(source, "OID_CUENTA_BANCARIA");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
            _p_comision = Format.DataReader.GetDecimal(source, "P_COMISION");

        }

        public virtual void CopyValues(TPVRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _oid_cuenta_bancaria = source.OidCuentaBancaria;
            _nombre = source.Nombre;
            _cuenta_contable = source.CuentaContable;
            _observaciones = source.Observaciones;
            _p_comision = source.PComision;
        }
        #endregion
    }

    [Serializable()]
    public class TPVBase
    {
        #region Attributes

        private TPVRecord _record = new TPVRecord();
        private string _cuenta_bancaria = string.Empty;

        public TPVRecord Record { get { return _record; } }

        #endregion

        #region Properties

        public virtual string CuentaBancaria { get { return _cuenta_bancaria; } set { _cuenta_bancaria = value; } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);

            _cuenta_bancaria = Format.DataReader.GetString(source, "CUENTA_BANCARIA");
        }

        internal void CopyValues(TPV source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _cuenta_bancaria = source.CuentaBancaria;
        }
        internal void CopyValues(TPVInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);

            _cuenta_bancaria = source.CuentaBancaria;
        }
        #endregion
    }

	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class TPV : BusinessBaseEx<TPV>
	{	 
		#region Attributes

        public TPVBase _base = new TPVBase();

        #endregion
		
		#region Properties

		public TPVBase Base { get { return _base; } }

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
		public virtual long OidCuentaBancaria
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidCuentaBancaria;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.OidCuentaBancaria.Equals(value))
				{
                    _base.Record.OidCuentaBancaria = value;
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
                return _base.Record.Nombre;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

                if (!_base.Record.Nombre.Equals(value))
				{
                    _base.Record.Nombre = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaContable
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.CuentaContable;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

                if (!_base.Record.CuentaContable.Equals(value))
				{
                    _base.Record.CuentaContable = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual decimal PComision
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.PComision;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

                if (!_base.Record.PComision.Equals(value))
				{
                    _base.Record.PComision = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Observaciones
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
                return _base.Record.Observaciones;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;

                if (!_base.Record.Observaciones.Equals(value))
				{
                    _base.Record.Observaciones = value;
					PropertyHasChanged();
				}
			}
		}

        public virtual string CuentaBancaria { get { return _base.CuentaBancaria; } set { _base.CuentaBancaria = value; } }

		#endregion
		
		#region Business Methods
		
		public virtual TPV CloneAsNew()
		{
			TPV clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.SessionCode = TPV.OpenSession();
			TPV.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
		
		protected virtual void CopyFrom (TPVInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			OidCuentaBancaria = source.OidCuentaBancaria;
			Nombre = source.Nombre;
			CuentaContable = source.CuentaContable;
			PComision = source.PComision;
			Observaciones = source.Observaciones;

            CuentaBancaria = source.CuentaBancaria;
		}
        
        #endregion

        #region Validation Rules

        protected override void AddBusinessRules()
        {
            ValidationRules.AddRule(CheckValidation, "Oid");
        }

        private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
        {
            //OidCuentaBancaria
            if (OidCuentaBancaria == 0)
            {
                e.Description = Resources.Messages.NO_CUENTA_SELECTED;
                throw new iQValidationException(e.Description, string.Empty, "OidCuentaBancaria");
            }

            return true;
        }
		 
		#endregion
		 
		#region Autorization Rules
		
		public static bool CanAddObject()
		{
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.AUXILIARES);
		}
		
		public static bool CanGetObject()
		{
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.AUXILIARES);
		}
		
		public static bool CanDeleteObject()
		{
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.AUXILIARES);
		}
		
		public static bool CanEditObject()
		{
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.AUXILIARES);
		}

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected TPV () {}		
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private TPV(TPV source, bool retrieve_childs)
        {
			MarkAsChild();
			Childs = retrieve_childs;
            Fetch(source);
        }
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
        private TPV(IDataReader source, bool retrieve_childs)
        {
            MarkAsChild();	
			Childs = retrieve_childs;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static TPV NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
				  moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			
			TPV obj = DataPortal.Create<TPV>(new CriteriaCs(-1));
            obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">TPV con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(TPV source, bool retrieve_childs)
		/// <remarks/>
		internal static TPV GetChild(TPV source)
		{
			return new TPV(source, false);
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">TPV con los datos para el objeto</param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
		internal static TPV GetChild(TPV source, bool retrieve_childs)
		{
			return new TPV(source, retrieve_childs);
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="reader">DataReader con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(IDataReader source, bool retrieve_childs)
		/// <remarks/>
        internal static TPV GetChild(IDataReader source)
        {
            return new TPV(source, false);
        }
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">IDataReader con los datos para el objeto</param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
        internal static TPV GetChild(IDataReader source, bool retrieve_childs)
        {
            return new TPV(source, retrieve_childs);
        }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// También copia los datos de los hijos del objeto.
		/// </summary>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual TPVInfo GetInfo()
		{
			return GetInfo(true);
		}
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual TPVInfo GetInfo (bool get_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(
					moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new TPVInfo(this, get_childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static TPV New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(
					moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<TPV>(new CriteriaCs(-1));
		}
		
		/// <summary>
		/// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
		/// </summary>
		/// <param name="oid"></param>
		/// <returns>Objeto con los valores del registro</returns>
		public static TPV Get(long oid)
		{
			return Get(oid, true);
		}
		
		/// <summary>
		/// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
		/// </summary>
		/// <param name="oid"></param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto con los valores del registro</returns>
		public static TPV Get(long oid, bool retrieve_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(
					moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = TPV.GetCriteria(TPV.OpenSession());
			criteria.Childs = retrieve_childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = TPV.SELECT(oid);
			else
				criteria.AddOidSearch(oid);
			
			TPV.BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<TPV>(criteria);
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
		/// Elimina todos los TPV. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = TPV.OpenSession();
			ISession sess = TPV.Session(sessCode);
			ITransaction trans = TPV.BeginTransaction(sessCode);
			
			try
			{	
				sess.Delete("from TPVRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				TPV.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override TPV Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(moleQule.Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);
			
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

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
		
		#region Common Data Access
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{			
			// El código va al constructor porque los DataGrid no llamana al DataPortal sino directamente al constructor			
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(TPV source)
		{
            try
            {
                SessionCode = source.SessionCode;
				_base.CopyValues(source);				
            }
            catch (Exception ex) { throw ex; }

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);
            }
            catch (Exception ex) { throw ex; }

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(TPVs parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{	
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(
						moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
			}
			catch (Exception ex) { throw ex; }
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(TPVs parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			try
			{
				ValidationRules.CheckRules();

                if (!IsValid)
                    throw new iQValidationException(
						moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				TPVRecord obj = Session().Get<TPVRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(TPVs parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;
			
			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<TPVRecord>(Oid));
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		
			MarkNew(); 
		}

		#endregion
		
		#region Root Data Access
		
		/// <summary>
		/// Obtiene un registro de la base de datos
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>Lo llama el DataPortal tras generar el objeto</remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					TPV.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);
				}

				MarkOld();
			}
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
		}
		
		/// <summary>
		/// Inserta un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isNew</remarks>
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
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				try
				{
					TPVRecord obj = Session().Get<TPVRecord>(Oid);
					obj.CopyValues(this._base.Record);
					Session().Update(obj);
					MarkOld();
				}
				catch (Exception ex)
				{
					throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
				}
			}
		}
		
		/// <summary>
		/// Borrado aplazado, no se ejecuta hasta que se llama al Save
		/// </summary>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}
		
		/// <summary>
		/// Elimina un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal</remarks>
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
				Session().Delete((TPVRecord)(criterio.UniqueResult()));
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
            string query;

            query = "SELECT TP.*," +
                    "       CB.\"VALOR\" AS \"CUENTA_BANCARIA\"";

            return query;
        }

        internal static string SELECT(long oid, bool lock_table)
        {
            string t = nHManager.Instance.GetSQLTable(typeof(TPVRecord));
            string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));

            string query;

            query = SELECT_FIELDS() +
                    " FROM " + t + " AS TP" +
                    " LEFT JOIN " + cb + " AS CB ON TP.\"OID_CUENTA_BANCARIA\" = CB.\"OID\"" +
                    " WHERE TRUE";

            if (oid > 0) query += " AND TP.\"OID\" = " + oid.ToString();

            if (lock_table) query += " FOR UPDATE OF TP NOWAIT;";

            return query;
        }
	
	    internal static string SELECT(QueryConditions conditions, bool lock_table)
        {
            string query;

            query = SELECT(0, false);

            if (conditions.BankAccount != null) query += " AND TP.\"OID_CUENTA_BANCARIA\" = " + conditions.BankAccount.Oid.ToString();
            
            //if (lock_table) query += " FOR UPDATE OF TP NOWAIT";

            return query;
        }
		
		#endregion

	}
}
