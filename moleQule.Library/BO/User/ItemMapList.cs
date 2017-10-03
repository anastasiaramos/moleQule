using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	/// <summary>
	/// Read Only Root Collection of Business Objects With Child Collection
	/// </summary>
    [Serializable()]
    public class ItemMapList : ReadOnlyListBaseEx<ItemMapList, ItemMapInfo>
    {
        #region Factory Methods

        private ItemMapList() { }
        private ItemMapList(IDataReader reader)
        {
            Fetch(reader);
        }

		/// <summary>
		/// Retrieve the complete list from db
		/// </summary>
		/// <returns>ItemMapList</returns>
        public static ItemMapList GetList()
        {
            CriteriaEx criteria = ItemMap.GetCriteria(ItemMap.OpenSession());

            ItemMapList list = DataPortal.Fetch<ItemMapList>(criteria);

            CloseSession(criteria.SessionCode);

            return list;
        }

        public static ItemMapList GetAssociatedItemsList(long oid)
        {
            CriteriaEx criteria = ItemMap.GetCriteria(ItemMap.OpenSession());

            //No criteria. Retrieve all de List
            criteria.Query = SELECT_BY_ITEM(oid);
            ItemMapList list = DataPortal.Fetch<ItemMapList>(criteria);

            CloseSession(criteria.SessionCode);

            return list;
        }

        public static ItemMapList GetIsAssociatedItemsList(long oid)
        {
            CriteriaEx criteria = ItemMap.GetCriteria(ItemMap.OpenSession());

            //No criteria. Retrieve all de List
            criteria.Query = SELECT_BY_ASSOCIATED_ITEM(oid);
            ItemMapList list = DataPortal.Fetch<ItemMapList>(criteria);

            CloseSession(criteria.SessionCode);

            return list;
        }

        /// <summary>
        /// Devuelve una lista de todos los elementos que cumplen el criterio pasado
        /// </summary>
        /// <param name="criteria">Criterio de búsqueda</param>
        /// <returns>Lista de elementos</returns>
        public static ItemMapList GetList(CriteriaEx criteria)
        {
            return ItemMapList.RetrieveList(typeof(ItemMap), AppContext.ActiveSchema.Code, criteria);
        }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<ItemMapInfo> GetSortedList(string sortProperty,
                                                                    ListSortDirection sortDirection)
        {
            SortedBindingList<ItemMapInfo> sortedList =
                new SortedBindingList<ItemMapInfo>(GetList());
            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        public static ItemMapList GetChildList(IDataReader reader) { return new ItemMapList(reader); }

        #endregion

        #region Data Access

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(ItemMapInfo.GetChild(SessionCode, reader));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }


        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;
                    while (reader.Read())
                        this.AddItem(ItemMapInfo.GetChild(SessionCode, reader));
                    IsReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return ItemMap.SELECT(conditions, false); }
		internal static string SELECT_BY_ITEM(long oidItem) { return ItemMap.SELECT_BY_ITEM(oidItem, false); }
		internal static string SELECT_BY_ASSOCIATED_ITEM(long oidItem) { return ItemMap.SELECT_BY_ASSOCIATED_ITEM(oidItem, false); }

		#endregion
	}
}
