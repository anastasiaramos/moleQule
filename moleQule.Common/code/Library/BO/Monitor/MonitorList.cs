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
	/// ReadOnly Business Object Root Collection
	/// </summary>
    [Serializable()]
	public class MonitorList : ReadOnlyListBaseEx<MonitorList, MonitorInfo, Monitor>
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
		private MonitorList() {}
		private MonitorList(IList<Monitor> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private MonitorList(IList<MonitorInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods
		
		public static MonitorList NewList() { return new MonitorList(); }
		
		private static MonitorList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Monitor.GetCriteria(Monitor.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

			MonitorList list = DataPortal.Fetch<MonitorList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}	
		public static MonitorList GetList(QueryConditions conditions, bool childs) {	return GetList(SELECT(conditions), childs); }
				
		public static MonitorList GetList(bool childs = true) { return GetList(SELECT(), childs); }
		
        public static MonitorList GetList(IList<Monitor> list) { return new MonitorList(list,false); }
        public static MonitorList GetList(IList<MonitorInfo> list) { return new MonitorList(list, false); }

        public static MonitorList GetErrorList(CriteriaEx criteria, bool childs = false)
        {
            QueryConditions conditions = new QueryConditions
            {
                Status = new EEstado[] { EEstado.Inactive },
                PagingInfo = criteria.PagingInfo,
                Filters = criteria.Filters,
                Orders = criteria.Orders
            };

            return GetList(Monitor.SELECT(conditions, false), childs);
        }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<MonitorInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<MonitorInfo> sortedList = new SortedBindingList<MonitorInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<MonitorInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<MonitorInfo> sortedList = new SortedBindingList<MonitorInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Monitor> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Monitor item in lista)
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
                this.AddItem(MonitorInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(MonitorInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Monitor.SELECT_COUNT(criteria), criteria.Session);
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
		public static string SELECT(QueryConditions conditions) { return Monitor.SELECT(conditions, false); }
		
		#endregion		
	}
}
