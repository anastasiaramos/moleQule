using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

using NHibernate;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class IdiomaList :
        ReadOnlyListBaseEx<IdiomaList, IdiomaInfo>
    {

        #region Factory Methods

        private IdiomaList() { }

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <param name="get_childs">retrieving the childs</param>
        /// <returns>Lista de elementos</returns>
        public static IdiomaList GetList(bool childs)
        {
            CriteriaEx criteria = Idioma.GetCriteria(Idioma.OpenSession());
            /*criteria.Childs = childs;
            criteria.Query = IdiomaList.SELECT(typeof(Idioma), "COMMON");
            criteria.AddOrder("Valor", true);*/

            //No criteria. Retrieve all de List
            IdiomaList list = DataPortal.Fetch<IdiomaList>(criteria);

            CloseSession(criteria.SessionCode);

            return list;
        }

        /// <summary>
        /// Default call for GetList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static IdiomaList GetList()
        {
            return IdiomaList.GetList(true);
        }

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static IdiomaList GetList(CriteriaEx criteria)
        {
            return IdiomaList.RetrieveList(typeof(Idioma), AppContext.ActiveSchema.Code, criteria);
        }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<IdiomaInfo> GetSortedList(string sortProperty,
                                                                    ListSortDirection sortDirection)
        {
            SortedBindingList<IdiomaInfo> sortedList =
                new SortedBindingList<IdiomaInfo>(GetList());
            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        #endregion

        #region Data Access

        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;
            Childs = criteria.Childs;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session()); ;

                    IsReadOnly = false;

                    while (reader.Read())
                    {
                        this.AddItem(IdiomaInfo.Get(reader, Childs));
                    }

                    IsReadOnly = true;
                }
                else
                {
                    IList list = criteria.List();

                    if (list.Count > 0)
                    {
                        IsReadOnly = false;

                        foreach (Idioma item in list)
                            this.AddItem(item.GetInfo());

                        IsReadOnly = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

    }
}
