using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

namespace moleQule.Library
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
	[Serializable()]
	public class SchemaUserList : ReadOnlyListBaseEx<SchemaUserList, SchemaUserInfo>
    {
        #region Business Methods

        public SchemaUserInfo GetItemBySchema(long oid_schema)
        {
            foreach (SchemaUserInfo item in this)
                if (item.OidSchema == oid_schema)
                    return item;

            return null;
        }

        #endregion
        
        #region Factory Methods

        private SchemaUserList() { }

		/// <summary>
		/// Retrieve the complete list from db
		/// </summary>
		/// <returns>SchemaUserList</returns>
		public static SchemaUserList GetChildList()
		{
            CriteriaEx criteria = SchemaUser.GetCriteria(SchemaUser.OpenSession());

            //No criteria. Retrieve all de List
            SchemaUserList list = DataPortal.Fetch<SchemaUserList>(criteria);

            CloseSession(criteria.SessionCode);

            return list;
		}

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<SchemaUserInfo> GetSortedList(	string sortProperty,
																		ListSortDirection sortDirection)
		{
			SortedBindingList<SchemaUserInfo> sortedList =
				new SortedBindingList<SchemaUserInfo>(GetChildList());
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
                IList list = criteria.List();

				if (list.Count > 0)
				{
					IsReadOnly = false;

					foreach (SchemaUser item in list)
						this.Add(item.GetInfo());

					IsReadOnly = true;
				}
			}
            catch (Exception ex)
            {
                string msg = Resources.Errors.NO_OPERATION + System.Environment.NewLine +
                                iQExceptionHandler.GetAllMessages(ex);
                throw new iQPersistentException(msg);
            }

			this.RaiseListChangedEvents = true;
		}

		#endregion

	}
}
