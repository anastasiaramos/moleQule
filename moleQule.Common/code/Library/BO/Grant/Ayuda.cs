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
	public class GrantRecord : RecordBase
	{
		#region Attributes

		private long _serial;
		private string _codigo = string.Empty;
		private long _estado;
		private string _nombre = string.Empty;
		private string _cuenta_contable = string.Empty;
		private string _observaciones = string.Empty;
  
		#endregion
		
		#region Properties
		public virtual long Serial { get { return _serial; } set { _serial = value; } }
		public virtual string Codigo { get { return _codigo; } set { _codigo = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual string Nombre { get { return _nombre; } set { _nombre = value; } }
		public virtual string CuentaContable { get { return _cuenta_contable; } set { _cuenta_contable = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		
		#endregion
		
		#region Business Methods
		
		public GrantRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_codigo = Format.DataReader.GetString(source, "CODIGO");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_nombre = Format.DataReader.GetString(source, "NOMBRE");
			_cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");

		}
		
		public virtual void CopyValues(GrantRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_serial = source.Serial;
			_codigo = source.Codigo;
			_estado = source.Estado;
			_nombre = source.Nombre;
			_cuenta_contable = source.CuentaContable;
			_observaciones = source.Observaciones;
		}
		#endregion	
	}

    [Serializable()]
	public class GrantBase 
	{	 
		#region Attributes
		
		private GrantRecord _record = new GrantRecord();
		
		public GrantRecord Record { get { return _record; } }
		
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
		
		internal void CopyValues(Ayuda source)
		{
			if (source == null) return;
			
			_record.CopyValues(source._base.Record);
		}
		internal void CopyValues(AyudaInfo source)
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
	public class Ayuda : BusinessBaseEx<Ayuda>
	{	 
		#region Attributes
		
		public GrantBase _base = new GrantBase();		

		private AyudaPeriodos _periodos = AyudaPeriodos.NewChildList();

		#endregion
		
		#region Properties

		public GrantBase Base { get { return _base; } }

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
		
		public virtual AyudaPeriodos Periodos
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _periodos;
			}
		}

		//NO ENLAZADOS	
		public virtual EEstado EEstado { get { return _base.EEstado; } set { Estado = (long)value; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }	

		public override bool IsValid
		{
			get { return base.IsValid
						 && _periodos.IsValid ; }
		}
		public override bool IsDirty
		{
			get { return base.IsDirty
						 || _periodos.IsDirty ; }
		}
		
		#endregion
		
		#region Business Methods
		
		public virtual Ayuda CloneAsNew()
		{
			Ayuda clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.GetNewCode();
			
			clon.SessionCode = Ayuda.OpenSession();
			Ayuda.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			clon.Periodos.MarkAsNew();
			
			return clon;
		}
		
		protected void CopyValues(AyudaInfo source)
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
		
		protected virtual void CopyFrom(AyudaInfo source)
		{
			if (source == null) return;
			
			Oid = source.Oid;
			Serial = source.Serial;
			Codigo = source.Codigo;
			Estado = source.Estado;
			Nombre = source.Nombre;
			Observaciones = source.Observaciones;
		}		
		
        public virtual void GetNewCode()
        {
            Serial = SerialInfo.GetNext(typeof(Ayuda));
            Codigo = Serial.ToString(Resources.Defaults.AYUDA_CODE_FORMAT);
        }

		public static Ayuda ChangeEstado(long oid, EEstado estado)
		{
			Ayuda item = Ayuda.Get(oid, false);
			item.BeginEdit();
			item.EEstado = estado;
			item.ApplyEdit();
			item.Save();
			item.CloseSession();

			return item;
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
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.EMPRESA);
        }

        public static bool CanGetObject()
        {
			return AutorizationRulesControler.CanGetObject(Resources.SecureItems.EMPRESA);
        }

        public static bool CanDeleteObject()
        {
			return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.EMPRESA);
        }

        public static bool CanEditObject()
        {
			return AutorizationRulesControler.CanEditObject(Resources.SecureItems.EMPRESA);
        }

		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Ayuda () {}		
		
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE. LAS UTILIZAN LAS FUNCIONES DE CREACION DE LISTAS
		/// </summary>
		private Ayuda(Ayuda source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Ayuda(int sessionCode, IDataReader source, bool childs)
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
		public static Ayuda NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Ayuda obj = DataPortal.Create<Ayuda>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Ayuda con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Ayuda source, bool childs)
		/// <remarks/>
		internal static Ayuda GetChild(Ayuda source) { return new Ayuda(source, false); }
		internal static Ayuda GetChild(Ayuda source, bool childs) { return new Ayuda(source, childs); }
        internal static Ayuda GetChild(int sessionCode, IDataReader source) { return new Ayuda(sessionCode, source, false); }
        internal static Ayuda GetChild(int sessionCode, IDataReader source, bool childs) { return new Ayuda(sessionCode, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual AyudaInfo GetInfo() { return GetInfo(true); }	
		public virtual AyudaInfo GetInfo (bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new AyudaInfo(this, childs);
		}
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Crea un nuevo objeto
		/// </summary>
		/// <returns>Nuevo objeto creado</returns>
		public static Ayuda New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return DataPortal.Create<Ayuda>(new CriteriaCs(-1));
		}
		
		public static Ayuda Get(long oid) { return Get(oid, true); }
		public static Ayuda Get(long oid, bool childs)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			CriteriaEx criteria = GetCriteria(OpenSession());
			criteria.Childs = childs;
			
			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(oid);
				
			BeginTransaction(criteria.Session);
			
			return DataPortal.Fetch<Ayuda>(criteria);
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
		/// Elimina todos los Ayuda. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Ayuda.OpenSession();
			ISession sess = Ayuda.Session(sessCode);
			ITransaction trans = Ayuda.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from GrantRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				Ayuda.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Ayuda Save()
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
								
				_periodos.Update(this);				
				
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
			EEstado = EEstado.Abierto;

			GetNewCode();			
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Ayuda source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{					
					AyudaPeriodo.DoLOCK(Session());
					string query = AyudaPeriodos.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_periodos = AyudaPeriodos.GetChildList(SessionCode, reader);					
				}
			} 

			MarkOld();
		}

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">DataReader fuente</param>
        private void Fetch(IDataReader source)
        {
			CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{					
					AyudaPeriodo.DoLOCK(Session());
					string query = AyudaPeriodos.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_periodos = AyudaPeriodos.GetChildList(SessionCode, reader);					
				}
			}   

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Ayudas parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			GetNewCode();
		
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
		internal void Update(Ayudas parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			GrantRecord obj = Session().Get<GrantRecord>(Oid);
			obj.CopyValues(this._base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Ayudas parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<GrantRecord>(Oid));
		
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
					Ayuda.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						CopyValues(reader);
					
					if (Childs)
					{
						string query = string.Empty;

						AyudaPeriodo.DoLOCK(Session());
						query = AyudaPeriodos.SELECT(this);
						reader = nHMng.SQLNativeSelect(query);
						_periodos = AyudaPeriodos.GetChildList(SessionCode, reader);
					}
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
			//Borrar si no hay código
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
			
			GrantRecord obj = Session().Get<GrantRecord>(Oid);
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
				Session().Delete((GrantRecord)(criterio.UniqueResult()));
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

            query = "SELECT A.*";

            return query;
        }

		internal static string WHERE(QueryConditions conditions)
		{
			string query;

			query = " WHERE TRUE";
 
            if (conditions.Ayuda != null)
		       if (conditions.Ayuda.Oid != 0)
                   query += " AND A.\"OID\" = " + conditions.Ayuda.Oid;				

			return query;
		}
		
        internal static string SELECT(long oid, bool lock_table)
        {			
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions { Ayuda = Ayuda.New().GetInfo(false) };
			conditions.Ayuda.Oid = oid;

			query = SELECT(conditions, lock_table);

			return query;
        }
	
	    internal static string SELECT(QueryConditions conditions, bool lock_table)
        {
            string a = nHManager.Instance.GetSQLTable(typeof(GrantRecord));
            
			string query;

            query = SELECT_FIELDS() +
                    " FROM " + a + " AS A";
					
			query += WHERE(conditions);	
		
			//if (lock_table) query += " FOR UPDATE OF A NOWAIT";

            return query;
        }
		
		#endregion
	}
}
