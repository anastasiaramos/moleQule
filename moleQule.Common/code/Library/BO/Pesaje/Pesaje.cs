using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Common
{
    [Serializable()]
    public class WeighingRecord : RecordBase
    {
        #region Attributes

        private long _serial;
        private string _codigo = string.Empty;
        private long _estado;
        private DateTime _fecha;
        private string _descripcion = string.Empty;
        private Decimal _bruto;
        private Decimal _neto;
        private Decimal _tara;
        private string _observaciones = string.Empty;

        #endregion

        #region Properties
        public virtual long Serial { get { return _serial; } set { _serial = value; } }
        public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
        public virtual long Estado { get { return _estado; } set { _estado = value; } }
        public virtual DateTime Fecha { get { return _fecha; } set { _fecha = value; } }
        public virtual string Descripcion { get { return _descripcion; } set { _descripcion = value; } }
        public virtual Decimal Bruto { get { return _bruto; } set { _bruto = value; } }
        public virtual Decimal Neto { get { return _neto; } set { _neto = value; } }
        public virtual Decimal Tara { get { return _tara; } set { _tara = value; } }
        public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }

        #endregion

        #region Business Methods

        public WeighingRecord() { }

        public virtual void CopyValues(IDataReader source)
        {
            if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _serial = Format.DataReader.GetInt64(source, "SERIAL");
            _codigo = Format.DataReader.GetString(source, "CODIGO");
            _estado = Format.DataReader.GetInt64(source, "ESTADO");
            _fecha = Format.DataReader.GetDateTime(source, "FECHA");
            _descripcion = Format.DataReader.GetString(source, "DESCRIPCION");
            _bruto = Format.DataReader.GetDecimal(source, "BRUTO");
            _neto = Format.DataReader.GetDecimal(source, "NETO");
            _tara = Format.DataReader.GetDecimal(source, "TARA");
            _observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

        }

        public virtual void CopyValues(WeighingRecord source)
        {
            if (source == null) return;

            Oid = source.Oid;
            _serial = source.Serial;
            _codigo = source.Codigo;
            _estado = source.Estado;
            _fecha = source.Fecha;
            _descripcion = source.Descripcion;
            _bruto = source.Bruto;
            _neto = source.Neto;
            _tara = source.Tara;
            _observaciones = source.Observaciones;
        }
        #endregion
    }

    [Serializable()]
	public class WeighingBase
    {
        #region Attributes

        private WeighingRecord _record = new WeighingRecord();

        public WeighingRecord Record { get { return _record; } }

        #endregion

        #region Properties

        public EEstado EEstado { get { return (EEstado)_record.Estado; } }
        public string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }

        #endregion

        #region Business Methods

        internal void CopyValues(IDataReader source)
        {
            if (source == null) return;

            _record.CopyValues(source);
        }

        internal void CopyValues(Pesaje source)
        {
            if (source == null) return;

            _record.CopyValues(source._base.Record);
        }
        internal void CopyValues(PesajeInfo source)
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
	public class Pesaje : BusinessBaseEx<Pesaje>
	{	 
		#region Attributes

		public WeighingBase _base = new WeighingBase();		

		#endregion
		
		#region Properties

		public WeighingBase Base { get { return _base; } }

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
		public virtual long Serial
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Serial;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Serial.Equals(value))
				{
					_base.Record.Serial = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Codigo
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Codigo;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Codigo.Equals(value))
				{
					_base.Record.Codigo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Estado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Estado;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Estado.Equals(value))
				{
					_base.Record.Estado = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Fecha
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Fecha;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Fecha.Equals(value))
				{
					_base.Record.Fecha = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Descripcion
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Descripcion;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.Descripcion.Equals(value))
				{
					_base.Record.Descripcion = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Bruto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Bruto;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Bruto.Equals(value))
				{
					_base.Record.Bruto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Neto
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Neto;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Neto.Equals(value))
				{
					_base.Record.Neto = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual Decimal Tara
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Tara;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Tara.Equals(value))
				{
					_base.Record.Tara = value;
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
		
		public virtual Pesaje CloneAsNew()
		{
			Pesaje clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.GetNewCode();
			
			clon.SessionCode = Pesaje.OpenSession();
			Pesaje.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
		
		protected virtual void CopyFrom(PesajeInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			Serial = source.Serial;
			Codigo = source.Codigo;
			Estado = source.Estado;
			Fecha = source.Fecha;
			Descripcion = source.Descripcion;
			Bruto = source.Bruto;
			Neto = source.Neto;
			Tara = source.Tara;
			Observaciones = source.Observaciones;
		}		
		
        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(Pesaje));
            Codigo = Serial.ToString(Resources.Defaults.PESAJE_CODE_FORMAT);
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
		 
		#region Autorization Rules
				
		public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.REGISTRO);
        }

        public static bool CanGetObject()
        {
			return AutorizationRulesControler.CanGetObject(Resources.SecureItems.REGISTRO);
        }

        public static bool CanDeleteObject()
        {
			return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.REGISTRO);
        }

        public static bool CanEditObject()
        {
			return AutorizationRulesControler.CanEditObject(Resources.SecureItems.REGISTRO);
        }

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Pesaje () {}
				
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private Pesaje(Pesaje source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Pesaje(int sessionCode, IDataReader source, bool childs)
        {
            MarkAsChild();	
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(source);
        }

		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		/// La utiliza la BusinessListBaseEx correspondiente para crear nuevos elementos
		public static Pesaje NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Pesaje obj = DataPortal.Create<Pesaje>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Pesaje con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Pesaje source, bool childs)
		/// <remarks/>
		internal static Pesaje GetChild(Pesaje source) { return new Pesaje(source, false); }
		internal static Pesaje GetChild(Pesaje source, bool childs) { return new Pesaje(source, childs); }
        internal static Pesaje GetChild(int sessionCode, IDataReader source) { return new Pesaje(sessionCode, source, false); }
        internal static Pesaje GetChild(int sessionCode, IDataReader source, bool childs) { return new Pesaje(sessionCode, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual PesajeInfo GetInfo() { return GetInfo(true); }	
		public virtual PesajeInfo GetInfo (bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new PesajeInfo(this, childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static Pesaje New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Pesaje>(new CriteriaCs(-1));
		}
		
		public static Pesaje Get(long oid) { return Get(oid, true); }
		public static Pesaje Get(long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = GetCriteria(OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(oid);
				
			BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Pesaje>(criteria);
		}
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Pesaje. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Pesaje.OpenSession();
			ISession sess = Pesaje.Session(sessCode);
			ITransaction trans = Pesaje.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from WeighingRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				Pesaje.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Pesaje Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);			
		
			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

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
		
		#region Common Data Access
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="criteria">Criterios de consulta</param>
		/// <remarks>La llama el DataPortal a partir del New o NewChild</remarks>		
		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			_base.Record.Oid = (long)(new Random()).Next();
			Fecha = DateTime.Now;
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Pesaje source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);			 

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
        private void Fetch(IDataReader source)
        {
			_base.CopyValues(source);			   

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Pesajes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(Pesajes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			WeighingRecord obj = Session().Get<WeighingRecord>(Oid);
			obj.CopyValues(this._base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Pesajes parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<WeighingRecord>(Oid));
		
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
					Pesaje.DoLOCK(Session());
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
            if (!SharedTransaction)
            {
                if (SessionCode < 0) SessionCode = OpenSession();
                BeginTransaction();
            }
			
			GetNewCode();

			Session().Save(Base.Record);
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;
			
			WeighingRecord obj = Session().Get<WeighingRecord>(Oid);
			obj.CopyValues(this._base.Record);
			Session().Update(obj);
			MarkOld();			
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
				Session().Delete((WeighingRecord)(criterio.UniqueResult()));
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
		
        internal static string SELECT_FIELDS()
        {
            string query;

            query = "SELECT P.*";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE (P.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";
 
            if (conditions.Pesaje != null)
		       if (conditions.Pesaje.Oid != 0)
                   query += " AND P.\"OID\" = " + conditions.Pesaje.Oid;

			return query;
		}
		
        internal static string SELECT(long oid, bool lock_table)
        {			
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Pesaje = Pesaje.New().GetInfo(false) };
			conditions.Pesaje.Oid = oid;

			query = SELECT(conditions, lock_table);

			return query;
        }
	
	    internal static string SELECT(QueryConditions conditions, bool lock_table)
        {
            string p = nHManager.Instance.GetSQLTable(typeof(WeighingRecord));
            
			string query;

            query = SELECT_FIELDS() +
                    " FROM " + p + " AS P";
					
			query += WHERE(conditions);	
		
			//if (lock_table) query += " FOR UPDATE OF P NOWAIT";

            return query;
        }
		
		#endregion
	}
}
