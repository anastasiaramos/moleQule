using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

namespace moleQule.Library
{
	/// <summary>
	/// Read Only Root Collection of Business Objects With Child Collection
	/// </summary>
    [Serializable()]
    public class UserList : ReadOnlyListBaseEx<UserList, UserInfo, User>
    {
        #region Factory Methods

        private UserList() { }

		public static UserList NewList() { return new UserList(); }

		/// <summary>
		/// Retrieve the complete list from db
		/// </summary>
		/// <returns>UserList</returns>
		public static UserList GetList() { return GetList(true); }
		public static UserList GetList(bool childs)
		{
            CriteriaEx criteria = User.GetCriteria(User.OpenSession());
			criteria.Childs = childs;

			criteria.Query = SELECT();

			UserList list = DataPortal.Fetch<UserList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}
		public static UserList GetList(ISchemaInfo schema) { return GetList(schema, true); }
		public static UserList GetList(ISchemaInfo schema, bool childs)
		{
            CriteriaEx criteria = User.GetCriteria(User.OpenSession());
            User.BeginTransaction(criteria.SessionCode);

			string query = string.Empty;

			QueryConditions conditions = new QueryConditions
			{
				Schema = schema
			};

			criteria.Query = SELECT(conditions);
			criteria.Childs = childs;

			UserList list = DataPortal.Fetch<UserList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}

		public static UserList GetList(IList<UserInfo> list)
		{
			UserList flist = new UserList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (UserInfo item in list)
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
        public static SortedBindingList<UserInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
        {
            SortedBindingList<UserInfo> sortedList = new SortedBindingList<UserInfo>(GetList());
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
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					IsReadOnly = false;

					while (reader.Read())
						this.AddItem(UserInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;
                    
                    if (criteria.PagingInfo != null)
                    {
                        reader = nHManager.Instance.SQLNativeSelect(User.SELECT_COUNT(criteria), criteria.Session);
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
		public static string SELECT(QueryConditions conditions)
		{
			string query = string.Empty;

			query = User.SELECT(conditions, false);

			return query;
		}

		#endregion
	}
}