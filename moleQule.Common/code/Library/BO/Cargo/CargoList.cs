using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class CargoList : ReadOnlyListBaseEx<CargoList, CargoInfo>
    {
        #region Factory Methods

        private CargoList() { }


        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>Lista de elementos</returns>
        public static CargoList GetList(bool childs)
        {
            CriteriaEx criteria = Cargo.GetCriteria(Cargo.OpenSession());
            criteria.Childs = childs;
            criteria.Query = SELECT();

            //No criteria. Retrieve all de List
            CargoList list = DataPortal.Fetch<CargoList>(criteria);

            CloseSession(criteria.SessionCode);

            return list;
        }

        /// <summary>
        /// Default call for GetList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static CargoList GetList()
        {
            return CargoList.GetList(true);
        }

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static CargoList GetList(CriteriaEx criteria)
        {
            return CargoList.RetrieveList(typeof(Cargo), "COMMON", criteria);
        }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<CargoInfo> GetSortedList(string sortProperty,
                                                                    ListSortDirection sortDirection)
        {
            SortedBindingList<CargoInfo> sortedList =
                new SortedBindingList<CargoInfo>(GetList());
            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        #endregion

        #region Data Access

        // called to retrieve data from db
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
                        this.AddItem(CargoInfo.GetChild(reader, Childs));

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
		public static string SELECT(QueryConditions conditions) { return Cargo.SELECT(conditions, false); }

		#endregion	
    }
}
