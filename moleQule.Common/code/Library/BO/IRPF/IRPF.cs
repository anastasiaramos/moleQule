using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class IRPFRecord : RecordBase
    {
        #region Attributes

        private string _nombre = string.Empty;
        private Decimal _porcentaje;
        private string _observaciones = string.Empty;

        #endregion

        #region Properties
        public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
        public virtual Decimal Porcentaje { get { return _porcentaje; } set { _porcentaje = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

        #endregion

        #region Business Methods

        public IRPFRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _nombre = Format.DataReader.GetString(source, "NOMBRE");
            _porcentaje = Format.DataReader.GetDecimal(source, "PORCENTAJE");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

        }

        public virtual void CopyValues(IRPFRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _nombre = source.Nombre;
            _porcentaje = source.Porcentaje;
            _observaciones = source.Observaciones;
        }
        #endregion
    }

    [Serializable()]
    public class IRPFBase
    {
        #region Attributes

        private IRPFRecord _record = new IRPFRecord();

        public IRPFRecord Record { get { return _record; } }

        #endregion

        #region Properties

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(IRPF source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(IRPFInfo source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        #endregion
    }
		
	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
	[Serializable()]
	public class IRPF : BusinessBaseEx<IRPF>
	{
		#region Attributes

        public IRPFBase _base = new IRPFBase();

		#endregion

		#region Properties

		public IRPFBase Base { get { return _base; } }

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
		public virtual Decimal Porcentaje
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Porcentaje;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Porcentaje.Equals(value))
				{
					_base.Record.Porcentaje = value;
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

		#endregion

		#region Business Methods

		/// <summary>
		/// Clona la entidad y sus subentidades y las marca como nuevas
		/// </summary>
		/// <returns>Una entidad clon</returns>
		public virtual IRPF CloneAsNew()
		{
			IRPF clon = base.Clone();

			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();

			clon.SessionCode = IRPF.OpenSession();
			IRPF.BeginTransaction(clon.SessionCode);

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
            ////OidSubtipoFacturaEmitida
            //if (OidSubtipoFacturaEmitida <= 0)
            //{
            //    e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "OidSubtipoFacturaEmitida");
            //    throw new iQValidationException(e.Description, string.Empty);
            //}

            ////OidSubtipoFacturaRecibida
            //if (OidSubtipoFacturaRecibida <= 0)
            //{
            //    e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "OidSubtipoFacturaRecibida");
            //    throw new iQValidationException(e.Description, string.Empty);
            //}
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
		protected IRPF() { }

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private IRPF(IRPF source, bool retrieve_childs)
		{
			MarkAsChild();
			Childs = retrieve_childs;
			Fetch(source);
		}

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private IRPF(IDataReader source, bool retrieve_childs)
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
		public static IRPF NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			IRPF obj = DataPortal.Create<IRPF>(new CriteriaCs(-1));
			obj.MarkAsChild();
			return obj;
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">IRPF con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(IRPF source, bool retrieve_childs)
		/// <remarks/>
		internal static IRPF GetChild(IRPF source)
		{
			return new IRPF(source, false);
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">IRPF con los datos para el objeto</param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
		internal static IRPF GetChild(IRPF source, bool retrieve_childs)
		{
			return new IRPF(source, retrieve_childs);
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
		internal static IRPF GetChild(IDataReader source)
		{
			return new IRPF(source, false);
		}

		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">IDataReader con los datos para el objeto</param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para montar la lista<remarks/>
		internal static IRPF GetChild(IDataReader source, bool retrieve_childs)
		{
			return new IRPF(source, retrieve_childs);
		}

		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// También copia los datos de los hijos del objeto.
		/// </summary>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual IRPFInfo GetInfo()
		{
			return GetInfo(true);
		}

		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual IRPFInfo GetInfo(bool get_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			return new IRPFInfo(this, get_childs);
		}

		#endregion

		#region Root Factory Methods

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static IRPF New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			return DataPortal.Create<IRPF>(new CriteriaCs(-1));
		}

		/// <summary>
		/// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
		/// </summary>
		/// <param name="oid"></param>
		/// <returns>Objeto con los valores del registro</returns>
		public static IRPF Get(long oid)
		{
			return Get(oid, true);
		}

		/// <summary>
		/// Obtiene un registro de la base de datos y lo convierte en un objeto de este tipo
		/// </summary>
		/// <param name="oid"></param>
		/// <param name="retrieve_childs">Flag para obtener también los hijos</param>
		/// <returns>Objeto con los valores del registro</returns>
		public static IRPF Get(long oid, bool retrieve_childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = IRPF.GetCriteria(IRPF.OpenSession());
			criteria.Childs = retrieve_childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = IRPF.SELECT(oid);

			IRPF.BeginTransaction(criteria.Session);

			return DataPortal.Fetch<IRPF>(criteria);
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
		/// Elimina todos los IRPF. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = IRPF.OpenSession();
			ISession sess = IRPF.Session(sessCode);
			ITransaction trans = IRPF.BeginTransaction(sessCode);

			try
			{
                sess.Delete("from IRPFRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				IRPF.CloseSession(sessCode);
			}
		}

		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override IRPF Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(
				moleQule.Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

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
		private void Fetch(IRPF source)
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
		internal void Insert(IRPFs parent)
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
			catch (Exception ex) { throw ex; }

			MarkOld();
		}

		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(IRPFs parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				IRPFRecord obj = Session().Get<IRPFRecord>(Oid);
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
		internal void DeleteSelf(IRPFs parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<IRPFRecord>(Oid));
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
					IRPF.DoLOCK(Session());
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
				//Borrar si no hay código

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
					IRPFRecord obj = Session().Get<IRPFRecord>(Oid);
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
				Session().Delete((IRPFRecord)(criterio.UniqueResult()));
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

			query = "SELECT I.*";

			return query;
		}

		internal static string SELECT(long oid, bool lock_table)
		{
            string i = nHManager.Instance.GetSQLTable(typeof(IRPFRecord));
			string query;

            query = SELECT_FIELDS() +
                    " FROM " + i + " AS I";

			if (oid > 0)
				query += " WHERE I.\"OID\" = " + oid.ToString();

			if (lock_table)
				query += " FOR UPDATE OF I NOWAIT;";

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lock_table)
		{
			string query;

			query = SELECT(0, lock_table);

			return query;
		}

		#endregion
	}

	[Serializable()]
	public class IRPFResumen
	{
		private decimal _importe;
		private decimal _base;

		public string Nombre { get; set; }
		public long OidIRPF { get; set; }
		public decimal Importe { get { return Decimal.Round(_importe, 2); } set { _importe = value; } }
		public decimal BaseImponible { get { return Decimal.Round(_base, 2); } set { _base = value; } }
        public decimal Total { get { return BaseImponible + Importe; } }
        public decimal Porcentaje { get; set; }
        
		#region Factory Methods

		public IRPFResumen() { }
		public IRPFResumen(SerializationInfo info, StreamingContext context) { }

		#endregion
	}

	[Serializable()]
	public class IRPFResumenList : Hashtable, IDisposable
	{
		#region Factory Methods

		public IRPFResumenList() { }
		public IRPFResumenList(SerializationInfo info, StreamingContext context) { }

		public void Dispose()
		{
			Cache.Instance.Remove(typeof(IRPFList));
		}

		#endregion

		#region Business Methods

		public void Insert(IRPFResumen item)
		{
			IRPFList impuestos = IRPFList.GetList(false, true);

			IRPFResumen cr = (IRPFResumen)this[item.OidIRPF];

			if (cr == null)
			{
				IRPFInfo impuesto = impuestos.GetItem(item.OidIRPF);

				this.Add(item.OidIRPF, new IRPFResumen
				{
					Nombre = impuesto.Nombre,
					OidIRPF = item.OidIRPF,
					BaseImponible = item.BaseImponible
				});
			}
			else
			{
				cr.BaseImponible += item.BaseImponible;
			}
		}

		public decimal TotalizeIRPFs()
		{
			IRPFList impuestos = IRPFList.GetList(false, true);

			decimal total = 0;

			//Calculo del impuesto para evitar errores de redondeo
			foreach (DictionaryEntry item in this)
			{
				IRPFResumen cr = (IRPFResumen)item.Value;

				IRPFInfo impuesto = impuestos.GetItem(cr.OidIRPF);

				cr.Importe = cr.BaseImponible * impuesto.Porcentaje / 100;
				total += cr.Importe;
			}

			return total;
		}

		#endregion
	}
}
