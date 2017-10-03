using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{
	/// <summary>
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class MonitorLineInfo : ReadOnlyBaseEx<MonitorLineInfo, MonitorLine>
	{	
		#region Attributes

		protected MonitorLineBase _base = new MonitorLineBase();
		
		#endregion
		
		#region Properties
		
		public MonitorLineBase Base { get { return _base; } }		
		
		public override long Oid { get { return _base.Record.Oid; } }
		public long OidMonitor { get { return _base.Record.OidMonitor; } }
		public DateTime Date { get { return _base.Record.Date; } }
		public string ComponentIP { get { return _base.Record.ComponentIP; } }
		public long ComponentInterval { get { return _base.Record.ComponentInterval; } }
		public long ComponentStatus { get { return _base.Record.ComponentStatus; } }
		public long Status { get { return _base.Record.Status; } }
		public long ErrorLevel { get { return _base.Record.ErrorLevel; } }
		public string Description { get { return _base.Record.Description; } }

		//FOREIGN PROPERTIES
		public virtual EComponentStatus EComponentStatus { get { return _base.EComponentStatus; } set { _base.Record.ComponentStatus = (long)value; } }
		public virtual string ComnponentStatusLabel { get { return _base.ComponentStatusLabel; } }
		public virtual EComponentStatus EStatus { get { return _base.EStatus; } set { _base.Record.Status = (long)value; } }
		public virtual string StatusLabel { get { return _base.StatusLabel; } }
		public virtual EErrorLevel EErrorLevel { get { return _base.EErrorLevel; } set { _base.Record.ErrorLevel = (long)value; } }
		public virtual string ErrorLevelLabel { get { return _base.ErrorLevelLabel; } }

		#endregion
		
		#region Business Methods
						
		public void CopyFrom(MonitorLine source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected MonitorLineInfo() { /* require use of factory methods */ }
		private MonitorLineInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal MonitorLineInfo(MonitorLine item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				
			}
		}
		
		public static MonitorLineInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new MonitorLineInfo(sessionCode, reader, childs);
		}
		
		public static MonitorLineInfo New(long oid = 0) { return new MonitorLineInfo(){ Oid = oid}; }
		
 		#endregion
		
		#region Root Factory Methods
	
		public static MonitorLineInfo Get(long oid, bool childs = false) 
		{ 
            if (!MonitorLine.CanGetObject()) throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			return Get(MonitorLine.SELECT(oid, false), childs); 
		}
		
		#endregion
					
		#region Common Data Access
								
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);				
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
		#region Root Data Access
		 
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
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex, new object[] { criteria.Query }); }
		}
		
		#endregion					
	}
}
