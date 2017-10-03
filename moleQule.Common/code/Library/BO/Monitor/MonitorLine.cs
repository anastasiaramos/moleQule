using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
	[Serializable()]
	public class MonitorLineRecord : RecordBase
	{
		#region Attributes

		private long _oid_monitor;
		private DateTime _date;
		private string _component_ip = string.Empty;
		private long _component_interval;
		private long _component_status;
		private long _status;
		private long _error_level;
		private string _description = string.Empty;
  
		#endregion
		
		#region Properties
		
		public virtual long OidMonitor { get { return _oid_monitor; } set { _oid_monitor = value; } }
		public virtual DateTime Date { get { return _date; } set { _date = value; } }
		public virtual string ComponentIP { get { return _component_ip; } set { _component_ip = value; } }
		public virtual long ComponentInterval { get { return _component_interval; } set { _component_interval = value; } }
		public virtual long ComponentStatus { get { return _component_status; } set { _component_status = value; } }
		public virtual long Status { get { return _status; } set { _status = value; } }
		public virtual long ErrorLevel { get { return _error_level; } set { _error_level = value; } }
		public virtual string Description { get { return _description; } set { _description = value; } }

		#endregion
		
		#region Business Methods
		
		public MonitorLineRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_monitor = Format.DataReader.GetInt64(source, "OID_MONITOR");
			_date = Format.DataReader.GetDateTime(source, "DATE");
			_component_ip = Format.DataReader.GetString(source, "COMPONENT_IP");
			_component_interval = Format.DataReader.GetInt64(source, "COMPONENT_INTERVAL");
			_component_status = Format.DataReader.GetInt64(source, "COMPONENT_STATUS");
			_status = Format.DataReader.GetInt64(source, "STATUS");
			_error_level = Format.DataReader.GetInt64(source, "ERROR_LEVEL");
			_description = Format.DataReader.GetString(source, "DESCRIPTION");
		}		
		public virtual void CopyValues(MonitorLineRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_monitor = source.OidMonitor;
			_date = source.Date;
			_component_ip = source.ComponentIP;
			_component_interval = source.ComponentInterval;
			_component_status = source.ComponentStatus;
			_status = source.Status;
			_error_level = source.ErrorLevel;
			_description = source.Description;
		}
		
		#endregion	
	}

    [Serializable()]
	public class MonitorLineBase 
	{	 
		#region Attributes
		
		private MonitorLineRecord _record = new MonitorLineRecord();
		
		#endregion
		
		#region Properties
		
		public MonitorLineRecord Record { get { return _record; } }

		public EComponentStatus EStatus { get { return (EComponentStatus)_record.Status; } }
		public string StatusLabel { get { return Library.EnumText<EComponentStatus>.GetLabel(EStatus); } }
		public EComponentStatus EComponentStatus { get { return (EComponentStatus)_record.ComponentStatus; } }
		public string ComponentStatusLabel { get { return Library.EnumText<EComponentStatus>.GetLabel(EComponentStatus); } }
		public EErrorLevel EErrorLevel { get { return (EErrorLevel)_record.ErrorLevel; } }
		public string ErrorLevelLabel { get { return Library.EnumText<EErrorLevel>.GetLabel(EErrorLevel); } }

		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			_record.CopyValues(source);
		}		
		public void CopyValues(MonitorLine source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);
		}
		public void CopyValues(MonitorLineInfo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source.Base.Record);
		}
		
		#endregion	
	}
		
	/// <summary>
	/// Editable Root Business Object
	/// Editable Child Business Object
	/// </summary>	
    [Serializable()]
	public class MonitorLine : BusinessBaseEx<MonitorLine>
	{	 
		#region Attributes
		
		protected MonitorLineBase _base = new MonitorLineBase();		

		#endregion
		
		#region Properties
		
		public MonitorLineBase Base { get { return _base; } }
		
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
				if (!_base.Record.Oid.Equals(value))
				{
					_base.Record.Oid = value;
					//PropertyHasChanged();
				}
			}
		}
		public virtual long OidMonitor
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.OidMonitor;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.OidMonitor.Equals(value))
				{
					_base.Record.OidMonitor = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime Date
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Date;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Date.Equals(value))
				{
					_base.Record.Date = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string ComponentIP
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ComponentIP;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (value == null) value = string.Empty;
				
				if (!_base.Record.ComponentIP.Equals(value))
				{
					_base.Record.ComponentIP = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long ComponentInterval
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ComponentInterval;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.ComponentInterval.Equals(value))
				{
					_base.Record.ComponentInterval = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long ComponentStatus
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ComponentStatus;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.ComponentStatus.Equals(value))
				{
					_base.Record.ComponentStatus = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Status
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Status;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.Status.Equals(value))
				{
					_base.Record.Status = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long ErrorLevel
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ErrorLevel;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				
				if (!_base.Record.ErrorLevel.Equals(value))
				{
					_base.Record.ErrorLevel = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Description
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Description;
			}
            
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Description.Equals(value))
				{
					_base.Record.Description = value;
					PropertyHasChanged();
				}
			}
		}

		//FOREIGN PROPERTIES
		public virtual EComponentStatus EComponentStatus { get { return _base.EComponentStatus; } set { ComponentStatus = (long)value; } }
		public virtual string ComnponentStatusLabel { get { return _base.ComponentStatusLabel; } }
		public virtual EComponentStatus EStatus { get { return _base.EStatus; } set { Status = (long)value; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
		public virtual EErrorLevel EErrorLevel { get { return _base.EErrorLevel; } set { ErrorLevel = (long)value; } }
		public virtual string ErrorLevelLabel { get { return _base.ErrorLevelLabel; } }

		#endregion
		
		#region Business Methods
		
		public virtual MonitorLine CloneAsNew()
		{
			MonitorLine clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.SessionCode = MonitorLine.OpenSession();
			MonitorLine.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();
			
			return clon;
		}
		
		protected virtual void CopyFrom(MonitorLineInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			OidMonitor = source.OidMonitor;
			Date = source.Date;
			ComponentIP = source.ComponentIP;
			ComponentInterval = source.ComponentInterval;
			ComponentStatus = source.ComponentStatus;
			Status = source.Status;
			ErrorLevel = source.ErrorLevel;
			Description = source.Description;
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
			//Monitor
			if (OidMonitor <= 0)
			{
				e.Description = String.Format(Library.Resources.Messages.NO_VALUE_SELECTED, "OidMonitor");
				throw new iQValidationException(e.Description, string.Empty);
			}

			return true;
		}	
		 
		#endregion
		 
		#region Autorization Rules
				
		public static bool CanAddObject()
        {
            return AppContext.User.IsService || AppContext.User.IsAdmin;;
        }
        public static bool CanGetObject()
        {
			return AppContext.User.IsService || AppContext.User.IsAdmin; ;
        }
        public static bool CanDeleteObject()
        {
			return AppContext.User.IsService || AppContext.User.IsAdmin; ;
        }
        public static bool CanEditObject()
        {
			return AppContext.User.IsService || AppContext.User.IsAdmin; ;
        }

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				MonitorLine = MonitorLineInfo.New(oid),
			};
		}
		
		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected MonitorLine () 
		{
			Oid = (long)(new Random()).Next();
		}
				
		private MonitorLine(MonitorLine source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private MonitorLine(int sessionCode, IDataReader source, bool childs)
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
		public static MonitorLine NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			MonitorLine obj = DataPortal.Create<MonitorLine>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">MonitorLine con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(MonitorLine source, bool childs)
		/// <remarks/>
		internal static MonitorLine GetChild(MonitorLine source) { return new MonitorLine(source, false); }
		internal static MonitorLine GetChild(MonitorLine source, bool childs) { return new MonitorLine(source, childs); }
        internal static MonitorLine GetChild(int sessionCode, IDataReader source) { return new MonitorLine(sessionCode, source, false); }
        internal static MonitorLine GetChild(int sessionCode, IDataReader source, bool childs) { return new MonitorLine(sessionCode, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual MonitorLineInfo GetInfo() { return GetInfo(true); }	
		public virtual MonitorLineInfo GetInfo (bool childs) { return new MonitorLineInfo(this, childs); }
		
		#endregion
		
		#region Root Factory Methods
		
		public static MonitorLine New(int sessionCode = -1)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			MonitorLine obj = DataPortal.Create<MonitorLine>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
			return obj;
		}
		
		public new static MonitorLine Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return BusinessBaseEx<MonitorLine>.Get(query, childs, -1);
		}
		
		public static MonitorLine Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }
		
		
		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			IsPosibleDelete(oid);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los MonitorLine. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = MonitorLine.OpenSession();
			ISession sess = MonitorLine.Session(sessCode);
			ITransaction trans = MonitorLine.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from MonitorLineRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				MonitorLine.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override MonitorLine Save()
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
				
				if (!SharedTransaction) Transaction().Commit();
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
				if (!SharedTransaction)
				{
					if (CloseSessions) CloseSession(); 
					else BeginTransaction();
				}
			}
		}
				
		#endregion				
		
		#region Child Factory Methods
		
		/// <summary>
        /// NO UTILIZAR DIRECTAMENTE. LO UTILIZA LA FUNCION DE CREACION DE LA LISTA DEL PADRE
        /// </summary>
        private MonitorLine(Monitor parent)
        {
            OidMonitor = parent.Oid;
			ComponentInterval = parent.ComponentInterval;
			ComponentIP = parent.ComponentIP;
			ComponentStatus = parent.ComponentStatus;
			Status = parent.Status;
			ErrorLevel = parent.ErrorLevel;
			Description = parent.Description;
			Date = DateTime.Now;
            MarkAsChild();
        }
		
		/// <summary>
		/// Crea un nuevo objeto hijo
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <returns>Nuevo objeto creado</returns>
		internal static MonitorLine NewChild(Monitor parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return new MonitorLine(parent);
		}
				
		/// <summary>
        /// Borrado aplazado, es posible el undo 
        /// (La función debe ser "no estática")
        /// </summary>
        public override void Delete()
        {
            if (!CanDeleteObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            MarkDeleted();
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
		private void Fetch(MonitorLine source)
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
		internal void Insert(MonitorLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;			
	
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(this);
			
			MarkOld();
		}
	
		/// <summary>
		/// Actualiza el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para actualizar elementos<remarks/>
		internal void Update(MonitorLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
			MonitorLineRecord obj = Session().Get<MonitorLineRecord>(Oid);
			obj.CopyValues(Base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(MonitorLines parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<MonitorLineRecord>(Oid));
		
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
				Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					//MonitorLine.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);					
					
				}

				MarkOld();
			}
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
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
			
			Session().Save(this);
		}
		
		/// <summary>
		/// Modifica un elemento en la tabla
		/// </summary>
		/// <remarks>Lo llama el DataPortal cuando se llama al Save y el objeto isDirty</remarks>
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (!IsDirty) return;
			
			MonitorLineRecord obj = Session().Get<MonitorLineRecord>(Oid);
			obj.CopyValues(this.Base.Record);
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
				Session().Delete((MonitorLineRecord)(criterio.UniqueResult()));
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
				
		/// <summary>
		/// Inserta un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Insert(Monitor parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidMonitor = parent.Oid;	
			
			ValidationRules.CheckRules();
			
			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			parent.Session().Save(Base.Record);			
			
			MarkOld();
		}

		/// <summary>
		/// Actualiza un registro en la base de datos
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		internal void Update(Monitor parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			//Debe obtener la sesion del padre pq el objeto es padre a su vez
			SessionCode = parent.SessionCode;

			OidMonitor = parent.Oid;

			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			MonitorLineRecord obj = parent.Session().Get<MonitorLineRecord>(Oid);
			obj.CopyValues(Base.Record);
			parent.Session().Update(obj);
			
			MarkOld();
		}

		/// <summary>
		/// Borra un registro de la base de datos.
		/// </summary>
		/// <param name="parent">Objeto padre</param>
		/// <remarks>Borrado inmediato<remarks/>
		internal void DeleteSelf(Monitor parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<MonitorLineRecord>(Oid));

			MarkNew();
		}
		
		#endregion
				
        #region SQL

		internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>() {};
        }
		
        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
		public static string SELECT(Monitor item) 
		{ 
			Library.Common.QueryConditions conditions = new Library.Common.QueryConditions { Monitor = item.GetInfo(false) };
			return SELECT(conditions, false); 
		}		
		
        internal static string SELECT_FIELDS()
        {
            string query;

            query = @"
				SELECT ML.*";

            return query;
        }

		internal static string JOIN(QueryConditions conditions)
		{
            string mo = nHManager.Instance.GetSQLTable(typeof(MonitorLineRecord));

			string query = @"
				FROM " + mo + @" AS ML";
				
			return query + " " + conditions.ExtraJoin;
		}
		
		internal static string WHERE(QueryConditions conditions)
		{
			if (conditions == null) return string.Empty;
		
			string query;

            query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "ML", ForeignFields());

            query += @" 
				AND (ML.""DATE"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";
 
			query += Common.EntityBase.STATUS_LIST_CONDITION(conditions.Status, "ML");
			query += Common.EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "ML");

			if (conditions.MonitorLine != null)
			{
				if (conditions.MonitorLine.Oid != -1)
					query += @"
						AND ML.""OID"" = " + conditions.MonitorLine.Oid;
			}

			if (conditions.Monitor != null)
			{
				if (conditions.Monitor.Oid != -1)
					query += @"
						AND ML.""OID_MONITOR"" = " + conditions.Monitor.Oid;
			}
            
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
				query += ORDER(conditions.Orders, "ML", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}				

			query += Common.EntityBase.LOCK("ML", lockTable);

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
			return SELECT(new QueryConditions { MonitorLine = MonitorLineInfo.New(oid) }, lockTable);
        }

		internal static string SELECT_BY_COMPONENT(QueryConditions conditions, bool lockTable)
		{
			string mo = nHManager.Instance.GetSQLTable(typeof(MonitorRecord));

			conditions.ExtraJoin = @"
				INNER JOIN " + mo + @" AS MO ON ML.""OID_MONITOR"" = MO.""OID""";

			conditions.ExtraWhere += @"
					AND MO.""COMPONENT_SERIAL"" = " + conditions.Monitor.ComponentSerial + @"
					AND MO.""COMPONENT_TYPE"" = " + conditions.Monitor.ComponentType;

			return SELECT(conditions, lockTable);
		}

		#endregion
	}
}
