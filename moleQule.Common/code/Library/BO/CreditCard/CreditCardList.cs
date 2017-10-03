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
	/// </summary>
    [Serializable()]
	public class CreditCardList : ReadOnlyListBaseEx<CreditCardList, CreditCardInfo>
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
		private CreditCardList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private CreditCardList(IList<CreditCard> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private CreditCardList(IDataReader reader, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private CreditCardList(IList<CreditCardInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
		/// Default call for GetList(bool retrieve_childs)
		/// </summary>
		/// <returns></returns>
		public static CreditCardList GetList() { return CreditCardList.GetList(true); }
		public static CreditCardList GetList(bool childs) { return GetList(new QueryConditions(), childs); }
		public static CreditCardList GetList(ETipoTarjeta tipo, bool childs) 
		{ 
			return GetList(new QueryConditions{ TipoTarjeta = tipo}, childs); 
		}

		public static CreditCardList GetList(QueryConditions conditions, bool childs)
		{
			CriteriaEx criteria = CreditCard.GetCriteria(CreditCard.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = CreditCardList.SELECT(conditions);

			CreditCardList list = DataPortal.Fetch<CreditCardList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		public static CreditCardList GetList(CriteriaEx criteria)
		{
			return CreditCardList.RetrieveList(typeof(CreditCard), AppContext.ActiveSchema.Code, criteria);
		}
        public static CreditCardList GetList(IList<CreditCard> list) { return new CreditCardList(list,false); }
        public static CreditCardList GetList(IList<CreditCardInfo> list) { return new CreditCardList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<CreditCardInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<CreditCardInfo> sortedList = new SortedBindingList<CreditCardInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<CreditCardInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<CreditCardInfo> sortedList = new SortedBindingList<CreditCardInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<CreditCard> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (CreditCard item in lista)
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
                this.AddItem(CreditCardInfo.GetChild(reader, Childs));

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
						this.AddItem(CreditCardInfo.GetChild(reader, Childs));

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
		public static string SELECT(QueryConditions conditions) { return CreditCard.SELECT(conditions, false); }
		
		#endregion		
	}
}
