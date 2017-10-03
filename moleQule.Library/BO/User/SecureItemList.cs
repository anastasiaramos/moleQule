using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	/// <summary>
	/// Read Only Root Collection of Business Objects With Child Collection
	/// </summary>
    [Serializable()]
    public class SecureItemList : ReadOnlyListBaseEx<SecureItemList, SecureItemInfo>
	{
		#region Business Methods

		public SecureItemInfo GetItemByTipo(long tipo)
		{
			return this.FirstOrDefault(x => x.Tipo == tipo);
		}

		#endregion

		#region Factory Methods

		private SecureItemList() { }

		/// <summary>
		/// Retrieve the complete list from db
		/// </summary>
		/// <returns>SecureItemList</returns>
        public static SecureItemList GetList()
        {
            CriteriaEx criteria = SecureItem.GetCriteria(SecureItem.OpenSession());
			criteria.Query = SELECT();

            SecureItemList list = DataPortal.Fetch<SecureItemList>(criteria);

            CloseSession(criteria.SessionCode);

            return list;
        }

        /// <summary>
        /// Devuelve una lista de todos los elementos que cumplen el criterio pasado
        /// </summary>
        /// <param name="criteria">Criterio de búsqueda</param>
        /// <returns>Lista de elementos</returns>
        public static SecureItemList GetList(CriteriaEx criteria)
        {
            return SecureItemList.RetrieveList(typeof(SecureItem), AppContext.ActiveSchema.Code, criteria);
        }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<SecureItemInfo> GetSortedList(string sortProperty,
                                                                    ListSortDirection sortDirection)
        {
            SortedBindingList<SecureItemInfo> sortedList =
                new SortedBindingList<SecureItemInfo>(GetList());
            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        #endregion

        #region Data Access

        protected override void Fetch(CriteriaEx criteria)
        {
			this.RaiseListChangedEvents = false;

			SessionCode = criteria.SessionCode;

			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

					IsReadOnly = false;

					while (reader.Read())
						this.AddItem(SecureItemInfo.GetChild(reader, Childs));

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
		public static string SELECT(QueryConditions conditions) { return SecureItem.SELECT(conditions, false); }

		#endregion	
    }
}
