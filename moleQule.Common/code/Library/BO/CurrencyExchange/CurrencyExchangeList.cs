using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
	/// </summary>
    [Serializable()]
	public class CurrencyExchangeList : ReadOnlyListBaseEx<CurrencyExchangeList, CurrencyExchangeInfo, CurrencyExchange>
	{	
		#region Business Methods

		public CurrencyExchangeInfo GetItem(string fromCurrencyIso, string toCurrencyIso)
		{
			return this.FirstOrDefault(x => (x.FromCurrencyIso == fromCurrencyIso) && (x.ToCurrencyIso == toCurrencyIso));
		}

		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private CurrencyExchangeList() {}
		private CurrencyExchangeList(IList<CurrencyExchange> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private CurrencyExchangeList(IList<CurrencyExchangeInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods
		
		public static CurrencyExchangeList NewList() { return new CurrencyExchangeList(); }
		
		private static CurrencyExchangeList GetList(string query, bool childs)
		{
			CriteriaEx criteria = CurrencyExchange.GetCriteria(CurrencyExchange.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

			CurrencyExchangeList list = DataPortal.Fetch<CurrencyExchangeList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}	
		public static CurrencyExchangeList GetList(QueryConditions conditions, bool childs) {	return GetList(SELECT(conditions), childs); }				
		public static CurrencyExchangeList GetList(bool childs = true)
		{
			
            return GetList(SELECT(), childs);
            
		}
		
        public static CurrencyExchangeList GetList(IList<CurrencyExchange> list) { return new CurrencyExchangeList(list,false); }
        public static CurrencyExchangeList GetList(IList<CurrencyExchangeInfo> list) { return new CurrencyExchangeList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<CurrencyExchangeInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<CurrencyExchangeInfo> sortedList = new SortedBindingList<CurrencyExchangeInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<CurrencyExchangeInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<CurrencyExchangeInfo> sortedList = new SortedBindingList<CurrencyExchangeInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<CurrencyExchange> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (CurrencyExchange item in lista)
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
                this.AddItem(CurrencyExchangeInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(CurrencyExchangeInfo.GetChild(SessionCode, reader, Childs));

                    if (criteria.PagingInfo != null)
                    {
                        reader = nHManager.Instance.SQLNativeSelect(CurrencyExchange.SELECT_COUNT(criteria), criteria.Session);
                        if (reader.Read()) criteria.PagingInfo.TotalItems = Format.DataReader.GetInt32(reader, "TOTAL_ROWS");
                    }
					
					IsReadOnly = true;
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
		public static string SELECT(QueryConditions conditions) { return CurrencyExchange.SELECT(conditions, false); }
		
		#endregion		
	}
}
