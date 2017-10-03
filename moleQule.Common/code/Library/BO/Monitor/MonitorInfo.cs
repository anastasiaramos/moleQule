using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;

namespace moleQule.Library.Common
{
	/// <summary>
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class MonitorInfo : ReadOnlyBaseEx<MonitorInfo, Monitor>
	{
		#region Attributes

		protected MonitorBase _base = new MonitorBase();

		protected MonitorLineList _lines = null;

		#endregion

		#region Properties

		public MonitorBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long Status { get { return _base.Record.Status; } }
		public long ComponentType { get { return _base.Record.ComponentType; } set { _base.Record.ComponentType = value; } }
		public string ComponentSerial { get { return _base.Record.ComponentSerial; } set { _base.Record.ComponentSerial = value; } }
		public string ComponentName { get { return _base.Record.ComponentName; } }
		public string ComponentIP { get { return _base.Record.ComponentIP; } }
		public long ComponentInterval { get { return _base.Record.ComponentInterval; } }
		public long ComponentStatus { get { return _base.Record.ComponentStatus; } }
		public long ErrorType { get { return _base.Record.ErrorType; } }
		public long ErrorLevel { get { return _base.Record.ErrorLevel; } }
		public string Description { get { return _base.Record.Description; } }
		public DateTime LastUpdate { get { return _base.Record.LastUpdate; } }
		public long ErrorCount { get { return _base.Record.ErrorCount; } }
		public long WarningCount { get { return _base.Record.WarningCount; } }
		public bool Notify { get { return _base.Record.Notify; } }

		public MonitorLineList LineaRegistros { get { return _lines; } }

		//LINKED
        public virtual EComponentStatus EStatus { get { return _base.EStatus; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
		public virtual EComponentStatus EComponentStatus { get { return _base.EComponentStatus; } set { _base.Record.ComponentStatus = (long)value; } }
		public virtual string ComnponentStatusLabel { get { return _base.ComponentStatusLabel; } }
        public EComponentType EComponentType { get { return _base.EComponentType; } }
        public string ComponentTypeLabel { get { return _base.ComponentTypeLabel; } }
        public EErrorType EErrorType { get { return _base.EErrorType; } }
        public string ErrorTypeLabel { get { return _base.ErrorTypeLabel; } }
        public EErrorLevel EErrorLevel { get { return _base.EErrorLevel; } }
        public string ErrorLevelLabel { get { return _base.ErrorLevelLabel; } }		
		
		#endregion
		
		#region Business Methods
				
		public void CopyFrom(Monitor source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		public MonitorInfo() { /* require use of factory methods */ }
		private MonitorInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal MonitorInfo(Monitor item, bool childs)
		{
			_base.CopyValues(item);

			if (childs)
			{
				_lines = (item.Lines != null) ? MonitorLineList.GetChildList(item.Lines) : null;
			}
		}
		
		public static MonitorInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new MonitorInfo(sessionCode, reader, childs);
		}
		
		public static MonitorInfo New(long oid = 0) { return new MonitorInfo(){ Oid = oid}; }
		
 		#endregion
		
		#region Root Factory Methods

		public static MonitorInfo Get(long oid, bool childs = false) 
		{
			if (!Monitor.CanGetObject()) throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			return Get(Monitor.SELECT(oid, false), childs); 
		}

        public static MonitorInfo GetBySerial(string serial, long type, bool childs = false)
        {
            if (serial.Contains(" ")) serial = string.Empty;

            QueryConditions conditions = new QueryConditions { Monitor = MonitorInfo.New(-1) };
            conditions.Monitor.ComponentSerial = serial;
            conditions.Monitor.ComponentType = type;

            return ReadOnlyBaseEx<MonitorInfo, Monitor>.Get(Monitor.SELECT(conditions, false), childs);
       }
		
		#endregion
					
		#region Common Data Access
								
        /// <summary>
        /// Obtiene un objeto a partir de un <see cref="IDataReader"/>.
        /// Obtiene los hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria"><see cref="IDataReader"/> con los datos</param>
        /// <remarks>
        /// La utiliza el <see cref="ReadOnlyListBaseEx"/> correspondiente para construir los objetos de la lista
        /// </remarks>
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);

				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;

					query = MonitorLineList.SELECT(this);
					reader = nHMng.SQLNativeSelect(query, Session());
					_lines = MonitorLineList.GetChildList(SessionCode, reader);
				}
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
		#region Root Data Access
		 
        /// <summary>
        /// Obtiene un registro de la base de datos
        /// </summary>
        /// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
        /// <remarks>
        /// La llama el DataPortal
        /// </remarks>
		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
			{
				Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;
				
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
                        _base.CopyValues(reader);

					if (Childs)
					{
						string query = string.Empty;

						query = MonitorLineList.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
						_lines = MonitorLineList.GetChildList(SessionCode, reader);
					}					
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex, new object[] { criteria.Query }); }
		}
		
		#endregion
					
        #region SQL
		
        #endregion		
	}
}
