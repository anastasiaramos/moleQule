using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
	/// ReadOnly Business Object Child Collection
	/// </summary>
    [Serializable()]
	public class MonitorLineList : ReadOnlyListBaseEx<MonitorLineList, MonitorLineInfo, MonitorLine>
	{	
		#region Business Methods
			
		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private MonitorLineList() {}
		private MonitorLineList(IList<MonitorLine> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private MonitorLineList(IList<MonitorLineInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods
		
		public static MonitorLineList NewList() { return new MonitorLineList(); }
		
		private static MonitorLineList GetList(string query, bool childs)
		{
			CriteriaEx criteria = MonitorLine.GetCriteria(MonitorLine.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

			MonitorLineList list = DataPortal.Fetch<MonitorLineList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}	
		public static MonitorLineList GetList(QueryConditions conditions, bool childs) {	return GetList(SELECT(conditions), childs); }				
		public static MonitorLineList GetList(bool childs = true) {	return GetList(SELECT(), childs); }
		
        public static MonitorLineList GetList(IList<MonitorLine> list) { return new MonitorLineList(list,false); }
        public static MonitorLineList GetList(IList<MonitorLineInfo> list) { return new MonitorLineList(list, false); }

		public static MonitorLineList GetByMonitorList(long oidMonitor, bool childs)
		{
			return GetByMonitorList(oidMonitor, null, childs);
		}
		public static MonitorLineList GetByMonitorList(long oidMonitor, CriteriaEx criteria, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Monitor = MonitorInfo.New(oidMonitor),
				PagingInfo = (criteria != null) ? criteria.PagingInfo : null,
				Filters = (criteria != null) ? criteria.Filters : null,
				Orders = (criteria != null) ? criteria.Orders : null
			};

			return GetList(MonitorLineList.SELECT(conditions), childs);
		}
		
		public static MonitorLineList GetByComponentList(string componentSerial, EComponentType componentType, bool childs)
		{
			return GetByComponentList(componentSerial, componentType, null, childs);
		}
		public static MonitorLineList GetByComponentList(string componentSerial, EComponentType componentType, CriteriaEx criteria, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				Monitor = MonitorInfo.New(-1),
				PagingInfo = (criteria != null) ? criteria.PagingInfo : null,
				Filters = (criteria != null) ? criteria.Filters : null,
				Orders = (criteria != null) ? criteria.Orders : null
			};

			conditions.Monitor.ComponentSerial = componentSerial;
			conditions.Monitor.ComponentType = (long)componentType;

			return GetList(MonitorLine.SELECT_BY_COMPONENT(conditions, false), childs);
		}

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<MonitorLineInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<MonitorLineInfo> sortedList = new SortedBindingList<MonitorLineInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<MonitorLineInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<MonitorLineInfo> sortedList = new SortedBindingList<MonitorLineInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Child Factory Methods
						
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>	
		private MonitorLineList(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static MonitorLineList GetChildList(int sessionCode, IDataReader reader, bool childs = false) { return new MonitorLineList(sessionCode, reader, childs); }
		public static MonitorLineList GetChildList(IList<MonitorLine> list, bool childs = false) { return new MonitorLineList(list, childs); }
        public static MonitorLineList GetChildList(IList<MonitorLineInfo> list, bool childs = false) { return new MonitorLineList(list, childs); }
		
		public static MonitorLineList GetChildList(MonitorInfo parent, bool childs)
		{
			CriteriaEx criteria = MonitorLine.GetCriteria(MonitorLine.OpenSession());

			criteria.Query = MonitorLineList.SELECT(parent);
			criteria.Childs = childs;

			MonitorLineList list = DataPortal.Fetch<MonitorLineList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}		
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<MonitorLine> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (MonitorLine item in lista)
				this.AddItem(item.GetInfo(Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(MonitorLineInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion

		#region Root Data Access
		 
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
		protected override void Fetch(CriteriaEx criteria)
		{
			this.RaiseListChangedEvents = false;
			
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;
			
			try
			{
				if (nHMng.UseDirectSQL)
				{					
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session()); 
					
					IsReadOnly = false;
					
					while (reader.Read())
						this.AddItem(MonitorLineInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(MonitorLine.SELECT_COUNT(criteria), criteria.Session);
						if (reader.Read()) criteria.PagingInfo.TotalItems = Format.DataReader.GetInt32(reader, "TOTAL_ROWS");
					}
				}
			}
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
			
			this.RaiseListChangedEvents = true;
		}
				
		#endregion
		
        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return MonitorLine.SELECT(conditions, false); }
		
		public static string SELECT(MonitorInfo parent) 
		{ 
			QueryConditions conditions = new QueryConditions
			{ 
				Monitor = parent,
 				Orders = new OrderList() 
			};
			conditions.Orders.NewOrder("Date", ListSortDirection.Descending, typeof(MonitorLine));
 
			return  MonitorLine.SELECT(conditions, false); 
		}
		
		#endregion		
	}
}
