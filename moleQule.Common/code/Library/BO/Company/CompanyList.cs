using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Common
{
    /// <summary>
    /// Read Only Root Collection of Business Objects With Child Collection
    /// </summary>
    [Serializable()]
    public class CompanyList : ReadOnlyListBaseEx<CompanyList, CompanyInfo, Company>
    {
        #region Business Methods

        public void SetPrincipal(long oid)
        {
            foreach (ISchemaInfo item in this)
                item.Principal = item.Oid.Equals(oid);
        }

        #endregion

        #region Factory Methods

        public CompanyList() { }

		public static CompanyList NewList() { return new CompanyList(); }

		private static CompanyList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Company.GetCriteria(Company.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			CompanyList list = DataPortal.Fetch<CompanyList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}

		public static CompanyList GetList() { return GetList(true); }
        public static CompanyList GetList(bool childs)
        {
            CriteriaEx criteria = Company.GetCriteria(Company.OpenSession());
            criteria.Query = SELECT();
            criteria.Childs = childs;

            CompanyList list = DataPortal.Fetch<CompanyList>(criteria);

            CloseSession(criteria.SessionCode);

            return list;
        }
		public static CompanyList GetList(UserInfo user, bool childs)
		{
			CriteriaEx criteria = Company.GetCriteria(Company.OpenSession());
			criteria.Query = CompanyList.SELECT(user);
			criteria.Childs = childs;

			CompanyList list = DataPortal.Fetch<CompanyList>(criteria);

			CloseSession(criteria.SessionCode);

			return list;
		}

        public static CompanyList GetList(IList<CompanyInfo> list)
        {
            CompanyList flist = new CompanyList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (CompanyInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }

        /// <summary>
        /// Devuelve una lista ordenada de todos los elementos
        /// </summary>
        /// <param name="sortProperty">Campo de ordenación</param>
        /// <param name="sortDirection">Sentido de ordenación</param>
        /// <returns>Lista ordenada de elementos</returns>
        public static SortedBindingList<CompanyInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<CompanyInfo> sortedList =
                new SortedBindingList<CompanyInfo>(GetList());
            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }

        #endregion

        #region Data Access

        // called to retrieve data from database
        protected override void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    IsReadOnly = false;

                    while (reader.Read())
                        this.AddItem(CompanyInfo.Get(SessionCode, reader, Childs));

                    IsReadOnly = true;

					if (criteria.PagingInfo != null)
					{
						reader = nHManager.Instance.SQLNativeSelect(Company.SELECT_COUNT(criteria), criteria.Session);
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
		public static string SELECT(QueryConditions conditions) { return Company.SELECT(conditions, true); }
		public static string SELECT(UserInfo user) { return Company.SELECT(new QueryConditions { User = user }, true); }

		#endregion
	}
}



