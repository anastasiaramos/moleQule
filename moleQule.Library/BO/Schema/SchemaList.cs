using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel; 

using Csla;
using moleQule.Library.CslaEx;  

namespace moleQule.Library
{
	/// <summary>
	/// Read Only MAIN Root Collection of Business Objects
	/// </summary>
    [Serializable()]
    public class SchemaList : ReadOnlyListBaseEx<SchemaList, SchemaInfo>
    {
        #region Business Methods

        public void SetPrincipal(long oid)
        {
            foreach (ISchemaInfo item in this)
                item.Principal = item.Oid.Equals(oid);
        }

        #endregion

        #region Factory Methods

        public SchemaList() { }

		/// <summary>
		/// Retrieve the complete list from db
		/// </summary>
		/// <returns>SchemaList</returns>
		public static SchemaList GetList()
		{
			CriteriaEx criteria = Schema.GetCriteria(Schema.OpenSession());

			//No criteria. Retrieve all de List
            SchemaList list = DataPortal.Fetch<SchemaList>(criteria);

            CloseSession(criteria.SessionCode);

            return list;
		}

        /// <summary>
        /// Devuelve una lista de todos los elementos que cumplen el criterio pasado
        /// </summary>
        /// <param name="criteria">Criterio de búsqueda</param>
        /// <returns>Lista de elementos</returns>
        public static SchemaList GetList(CriteriaEx criteria)
        {
            return SchemaList.RetrieveList(typeof(Schema), AppContext.ActiveSchema.Code, criteria);
        }

		/// <summary>
		/// Builds a SchemaList from a IList<!--<SchemaInfo>-->.
		/// Doesn`t retrieve child data from DB.
		/// </summary>
		/// <param name="list"></param>
		/// <returns>SchemaList</returns>
		public static SchemaList GetList(IList<SchemaInfo> list)
		{
			SchemaList flist = new SchemaList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (SchemaInfo item in list)
					flist.Add(item);

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
		public static SortedBindingList<SchemaInfo> GetSortedList(	string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<SchemaInfo> sortedList =
				new SortedBindingList<SchemaInfo>(GetList());
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
				IList list = criteria.List();

                if (list.Count > 0)
                {
                    IsReadOnly = false;

                    foreach (Schema item in list)
                        this.Add(item.GetInfo());

                    IsReadOnly = true;
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



