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
	public class MonitorRecord : RecordBase
	{
		#region Attributes

		private long _status;
		private long _component_type;
		private string _component_serial = string.Empty;
		private string _component_name = string.Empty;
		private string _component_ip = string.Empty;
		private long _component_interval;
		private long _component_status;
		private long _error_type;
		private long _error_level;
        private string _description = string.Empty;
		private DateTime _last_update;
		private long _error_count;
		private long _warning_count;
		private bool _notify;

		#endregion

		#region Properties

		public virtual long Status { get { return _status; } set { _status = value; } }
		public virtual long ComponentType { get { return _component_type; } set { _component_type = value; } }
		public virtual string ComponentSerial { get { return _component_serial; } set { _component_serial = value; } }
		public virtual string ComponentName { get { return _component_name; } set { _component_name = value; } }
		public virtual string ComponentIP { get { return _component_ip; } set { _component_ip = value; } }
		public virtual long ComponentInterval { get { return _component_interval; } set { _component_interval = value; } }
		public virtual long ComponentStatus { get { return _component_status; } set { _component_status = value; } }
		public virtual long ErrorType { get { return _error_type; } set { _error_type = value; } }
		public virtual long ErrorLevel { get { return _error_level; } set { _error_level = value; } }
		public virtual string Description { get { return _description; } set { _description = value; } }
		public virtual DateTime LastUpdate { get { return _last_update; } set { _last_update = value; } }
		public virtual long ErrorCount { get { return _error_count; } set { _error_count = value; } }
		public virtual long WarningCount { get { return _warning_count; } set { _warning_count = value; } }
		public virtual bool Notify { get { return _notify; } set { _notify = value; } }

		#endregion

		#region Business Methods

		public MonitorRecord() { }

		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_status = Format.DataReader.GetInt64(source, "STATUS");
            _component_type = Format.DataReader.GetInt64(source, "COMPONENT_TYPE");
			_component_serial = Format.DataReader.GetString(source, "COMPONENT_SERIAL");
			_component_name = Format.DataReader.GetString(source, "COMPONENT_NAME");
			_component_ip = Format.DataReader.GetString(source, "COMPONENT_IP");
            _component_interval = Format.DataReader.GetInt64(source, "COMPONENT_INTERVAL");
			_component_status = Format.DataReader.GetInt64(source, "COMPONENT_STATUS");
			_error_type = Format.DataReader.GetInt64(source, "ERROR_TYPE");
			_error_level = Format.DataReader.GetInt64(source, "ERROR_LEVEL");
            _description = Format.DataReader.GetString(source, "DESCRIPTION");
			_last_update = Format.DataReader.GetDateTime(source, "LAST_UPDATE");
			_error_count = Format.DataReader.GetInt64(source, "ERROR_COUNT");
			_warning_count = Format.DataReader.GetInt64(source, "WARNING_COUNT");
			_notify = Format.DataReader.GetBool(source, "NOTIFY");
		}
		public virtual void CopyValues(MonitorRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_status = source.Status;
			_component_type = source.ComponentType;
			_component_serial = source.ComponentSerial;
			_component_name = source.ComponentName;
			_component_ip = source.ComponentIP;
			_component_interval = source.ComponentInterval;
			_component_status = source.ComponentStatus;
			_error_type = source.ErrorType;
			_error_level = source.ErrorLevel;
			_description = source.Description;
			_last_update = source.LastUpdate;
			_error_count = source.ErrorCount;
			_warning_count = source.WarningCount;
			_notify = source.Notify;
		}

		#endregion
	}

	[Serializable()]
	public class MonitorBase
	{
		#region Attributes

		private MonitorRecord _record = new MonitorRecord();

		#endregion

		#region Properties

		public MonitorRecord Record { get { return _record; } }

		public EComponentStatus EStatus { get { return (EComponentStatus)_record.Status; } }
		public string StatusLabel { get { return Library.EnumText<EComponentStatus>.GetLabel(EStatus); } }
		public EComponentStatus EComponentStatus { get { return (EComponentStatus)_record.ComponentStatus; } }
		public string ComponentStatusLabel { get { return Library.EnumText<EComponentStatus>.GetLabel(EComponentStatus); } }
		public EComponentType EComponentType { get { return (EComponentType)_record.ComponentType; } }
		public string ComponentTypeLabel { get { return Library.EnumText<EComponentType>.GetLabel(EComponentType); } }
		public EErrorType EErrorType { get { return (EErrorType)_record.ErrorType; } }
		public string ErrorTypeLabel { get { return Library.EnumText<EErrorType>.GetLabel(EErrorType); } }
		public EErrorLevel EErrorLevel { get { return (EErrorLevel)_record.ErrorLevel; } }
		public string ErrorLevelLabel { get { return Library.EnumText<EErrorLevel>.GetLabel(EErrorLevel); } }

		#endregion

		#region Business Methods

		internal void CopyValues(IDataReader source)
		{
			if (source == null) return;

			_record.CopyValues(source);
		}
		public void CopyValues(Monitor source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}
		public void CopyValues(MonitorInfo source)
		{
			if (source == null) return;

			_record.CopyValues(source.Base.Record);
		}

		#endregion
	}
		
	/// <summary>
	/// Editable Root Business Object
	/// </summary>	
    [Serializable()]
	public class Monitor : BusinessBaseEx<Monitor>
	{
		#region Attributes

		protected MonitorBase _base = new MonitorBase();

		private MonitorLines _lines = MonitorLines.NewChildList();

		#endregion

		#region Properties

		public MonitorBase Base { get { return _base; } }

		public override long Oid
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				return _base.Record.Oid;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				if (!_base.Record.Oid.Equals(value))
				{
					_base.Record.Oid = value;
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
		public virtual long ComponentType
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ComponentType;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.ComponentType.Equals(value))
				{
					_base.Record.ComponentType = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string ComponentSerial
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ComponentSerial;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.ComponentSerial.Equals(value))
				{
					_base.Record.ComponentSerial = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string ComponentName
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ComponentName;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.ComponentName.Equals(value))
				{
					_base.Record.ComponentName = value;
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
		public virtual long ErrorType
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ErrorType;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.ErrorType.Equals(value))
				{
					_base.Record.ErrorType = value;
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
                if (value == null) value = string.Empty;
				if (!_base.Record.Description.Equals(value))				{
					_base.Record.Description = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual DateTime LastUpdate
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.LastUpdate;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.LastUpdate.Equals(value))
				{
					_base.Record.LastUpdate = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long ErrorCount
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.ErrorCount;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.ErrorCount.Equals(value))
				{
					_base.Record.ErrorCount = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long WarningCount
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.WarningCount;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.WarningCount.Equals(value))
				{
					_base.Record.WarningCount = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual bool Notify
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Notify;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (!_base.Record.Notify.Equals(value))
				{
					_base.Record.Notify = value;
					PropertyHasChanged();
				}
			}
		}

		public virtual MonitorLines Lines
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _lines;
			}
		}

		//LINKED
        public virtual EComponentStatus EStatus { get { return _base.EStatus; } set { Status = (long)value; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
		public virtual EComponentStatus EComponentStatus { get { return _base.EComponentStatus; } set { _base.Record.ComponentStatus = (long)value; } }
		public virtual string ComnponentStatusLabel { get { return _base.ComponentStatusLabel; } }
        public virtual EComponentType EComponentType { get { return _base.EComponentType; } set { ComponentType = (long)value; } }
        public virtual string ComponentTypeLabel { get { return _base.ComponentTypeLabel; } }
        public virtual EErrorType EErrorType { get { return _base.EErrorType; } set { ErrorType = (long)value; } }
        public virtual string ErrorTypeLabel { get { return _base.ErrorTypeLabel; } }
        public virtual EErrorLevel EErrorLevel { get { return _base.EErrorLevel; } set { ErrorLevel = (long)value; } }
        public virtual string ErrorLevelLabel { get { return _base.ErrorLevelLabel; } }

		public override bool IsValid
		{
			get
			{
				return base.IsValid
					   && _lines.IsValid;
			}
		}
		public override bool IsDirty
		{
			get
			{
				return base.IsDirty
					   || _lines.IsDirty;
			}
		}

		#endregion
		
		#region Business Methods
		
		public virtual Monitor CloneAsNew()
		{
			Monitor clon = base.Clone();
			
			//Se definen el Oid y el Coidgo como nueva entidad
			Random rd = new Random();
			clon.Oid = rd.Next();
			
			clon.SessionCode = Monitor.OpenSession();
			Monitor.BeginTransaction(clon.SessionCode);
			
			clon.MarkNew();

			clon.MarkNew();
			clon.Lines.MarkAsNew();

			return clon;
		}	
	
		protected virtual void CopyFrom(MonitorInfo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			Status = source.Status;
			ComponentType = source.ComponentType;
			ComponentSerial = source.ComponentSerial;
			ComponentName = source.ComponentName;
			ComponentIP = source.ComponentIP;
			ComponentInterval = source.ComponentInterval;
			ComponentStatus = source.ComponentStatus;
			ErrorType = source.ErrorType;
			ErrorLevel = source.ErrorLevel;
			Description = source.Description;
			LastUpdate = source.LastUpdate;
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
            return AppContext.User.IsService || AppContext.User.IsAdmin;
        }
        public static bool CanGetObject()
        {
            return AppContext.User.IsService || AppContext.User.IsAdmin;
        }
        public static bool CanDeleteObject()
        {
            return AppContext.User.IsService || AppContext.User.IsAdmin;
        }
        public static bool CanEditObject()
        {
            return AppContext.User.IsService || AppContext.User.IsAdmin;
        }

		public static void IsPosibleDelete(long oid)
		{
			QueryConditions conditions = new QueryConditions
			{
				Monitor = MonitorInfo.New(oid),
			};
		}
		
		#endregion
		 
		#region Common Factory Methods
		 
		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION New o NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate.
		/// </summary>
		protected Monitor() 
		{
		}
		private Monitor(Monitor source, bool childs)
        {
			MarkAsChild();
			Childs = childs;
            Fetch(source);
        }
        private Monitor(int sessionCode, IDataReader source, bool childs)
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
		public static Monitor NewChild() 
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Monitor obj = DataPortal.Create<Monitor>(new CriteriaCs(-1));		
			obj.MarkAsChild();
            return obj;
		}
		
		/// <summary>
		/// Crea un objeto
		/// </summary>
		/// <param name="source">Monitor con los datos para el objeto</param>
		/// <returns>Objeto creado</returns>
		/// <remarks>
		/// La utiliza la BusinessListBaseEx correspondiente para montar la lista
		/// NO OBTIENE los hijos. Para ello utilice GetChild(Monitor source, bool childs)
		/// <remarks/>
		internal static Monitor GetChild(Monitor source, bool childs = false) { return new Monitor(source, childs); }
        internal static Monitor GetChild(int sessionCode, IDataReader source, bool childs = false) { return new Monitor(sessionCode, source, childs); }
		
		/// <summary>
		/// Construye y devuelve un objeto de solo lectura copia de si mismo.
		/// </summary>
		/// <param name="get_childs">Flag para solicitar que se copien los hijos</param>
		/// <returns>Réplica de solo lectura del objeto</returns>
		public virtual MonitorInfo GetInfo (bool childs = false) { return new MonitorInfo(this, childs); }

		public virtual void LoadChilds(Type type, bool childs)
		{
			if (type.Equals(typeof(MonitorLine)))
			{
				_lines = MonitorLines.GetChildList(this, childs);
			}
		}
		
		#endregion
		
		#region Root Factory Methods
		
		public static Monitor New(int sessionCode = -1)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			Monitor obj = DataPortal.Create<Monitor>(new CriteriaCs(-1));
			obj.SetSharedSession(sessionCode);
			return obj;
		}
        public static Monitor New(MonitorInfo source, int sessionCode = -1)
        {
            if (!CanAddObject())
                throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            Monitor obj = DataPortal.Create<Monitor>(new CriteriaCs(-1));
            obj.SetSharedSession(sessionCode);
            obj.CopyFrom(source);
            return obj;
        }
		
		public new static Monitor Get(string query, bool childs, int sessionCode = -1)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return BusinessBaseEx<Monitor>.Get(query, childs, -1);
		}
		
		public static Monitor Get(long oid, bool childs = true) { return Get(SELECT(oid), childs); }

        public static Monitor GetBySerial(string serial, long type, bool childs = false)
        {
            if (serial.Contains(" ")) serial = string.Empty;

            QueryConditions conditions = new QueryConditions
            {
                Monitor = MonitorInfo.New(-1)
            };
            conditions.Monitor.ComponentSerial = serial;
            conditions.Monitor.ComponentType = type;
            return Get(SELECT(conditions, false), childs);
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
			
			IsPosibleDelete(oid);
			
			DataPortal.Delete(new CriteriaCs(oid));
		}
		
		/// <summary>
		/// Elimina todos los Monitor. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = Monitor.OpenSession();
			ISession sess = Monitor.Session(sessCode);
			ITransaction trans = Monitor.BeginTransaction(sessCode);
			
			try
			{
                sess.Delete("from MonitorRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				Monitor.CloseSession(sessCode);
			}
		}
		
		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override Monitor Save()
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

				_lines.Update(this);				
				
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
			EStatus = EComponentStatus.OK;
			Notify = true;

			_lines = MonitorLines.NewChildList();
		}
		
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="source">Objeto fuente</param>
		private void Fetch(Monitor source)
		{
			SessionCode = source.SessionCode;

			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{
					MonitorLine.DoLOCK(Session());
					string query = MonitorLines.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_lines = MonitorLines.GetChildList(SessionCode, reader, false);
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
			_base.CopyValues(source);

			if (Childs)
			{
				if (nHMng.UseDirectSQL)
				{
					MonitorLine.DoLOCK(Session());
					string query = MonitorLines.SELECT(this);
					IDataReader reader = nHMng.SQLNativeSelect(query);
					_lines = MonitorLines.GetChildList(SessionCode, reader, false);
				}
			} 

            MarkOld();
        }

		/// <summary>
		/// Inserta el registro en la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para insertar elementos<remarks/>
		internal void Insert(Monitors parent)
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
		internal void Update(Monitors parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;
			
			ValidationRules.CheckRules();

			if (!IsValid)
				throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

			SessionCode = parent.SessionCode;
            MonitorRecord obj = Session().Get<MonitorRecord>(Oid);
			obj.CopyValues(this._base.Record);
			Session().Update(obj);
			
			MarkOld();
		}
		
		/// <summary>
		/// Borra el registro de la base de datos
		/// </summary>
		/// <param name="parent">Lista padre</param>
		/// <remarks>La utiliza la BusinessListBaseEx correspondiente para borrar elementos<remarks/>
		internal void DeleteSelf(Monitors parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			SessionCode = parent.SessionCode;
			Session().Delete(Session().Get<MonitorRecord>(Oid));
		
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
					Monitor.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
					
					if (reader.Read())
						_base.CopyValues(reader);

					if (Childs)
					{
						string query = string.Empty;

						MonitorLine.DoLOCK(Session());
						query = MonitorLine.SELECT(this);
						reader = nHMng.SQLNativeSelect(query);
						_lines = MonitorLines.GetChildList(SessionCode, reader, Childs);
					} 					
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

			MonitorRecord obj = Session().Get<MonitorRecord>(Oid);
			obj.CopyValues(Base.Record);
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
				Session().Delete((MonitorRecord)(criterio.UniqueResult()));
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

		internal static Dictionary<String, ForeignField> ForeignFields()
        {
            return new Dictionary<String, ForeignField>() {};
        }
		
        public new static string SELECT(long oid) { return SELECT(oid, true); }
		public static string SELECT(QueryConditions conditions) { return SELECT(conditions, true); }
		
        internal static string SELECT_FIELDS()
        {
            string query;

            query = @"
				SELECT MO.*";

            return query;
        }

		internal static string JOIN(QueryConditions conditions)
		{
            string mo = nHManager.Instance.GetSQLTable(typeof(MonitorRecord));

			string query;

            query = @"
				FROM " + mo + @" AS MO";
				
			return query + " " + conditions.ExtraJoin;
		}
		
		internal static string WHERE(QueryConditions conditions)
		{
			string query;

            query = @" 
				WHERE " + FilterMng.GET_FILTERS_SQL(conditions.Filters, "MO", ForeignFields());
				
			/*query += @" 
				AND (MO.""DATE"" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "')";*/
 
			query += EntityBase.STATUS_LIST_CONDITION(conditions.Status, "MO");
			query += EntityBase.GET_IN_LIST_CONDITION(conditions.OidList, "MO");

            if (conditions.Monitor != null)
			{	
				if (conditions.Monitor.Oid != -1)
					query += @"
						AND MO.""OID"" = " + conditions.Monitor.Oid;				
                if (conditions.Monitor.ComponentSerial != string.Empty) 
					query += @"
						AND MO.""COMPONENT_SERIAL"" = '" + conditions.Monitor.ComponentSerial + "'";
                if (conditions.Monitor.ComponentType != 0) 
					query += @"
						AND MO.""COMPONENT_TYPE"" = " + conditions.Monitor.ComponentType;
            }

			return query + " " + conditions.ExtraWhere;
		}
		
	    internal static string SELECT(QueryConditions conditions, bool lockTable)
        {
			string query;

			query =
				SELECT_FIELDS() +
				JOIN(conditions) +
				WHERE(conditions);

			if (conditions != null)
			{
				query += ORDER(conditions.Orders, "MO", ForeignFields());
				query += LIMIT(conditions.PagingInfo);
			}

			query += EntityBase.LOCK("MO", lockTable);

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

		public static string SELECT_COUNT() { return SELECT_COUNT(new QueryConditions()); }
		public static string SELECT_COUNT(QueryConditions conditions)
		{
			string query;

			query = @"
                SELECT COUNT(*) AS ""TOTAL_ROWS""" +
				SELECT(conditions) +
				WHERE(conditions);

			return query;
		}

		internal static string SELECT(long oid, bool lockTable)
        {			
			return SELECT(new QueryConditions { Monitor = MonitorInfo.New(oid) }, lockTable);
        }
		
		#endregion
	}
}
