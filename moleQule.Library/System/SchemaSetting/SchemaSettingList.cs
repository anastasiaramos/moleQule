using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	/// <summary>
	/// Read Only Root Collection of Business Objects
	/// </summary>
	[Serializable()]
	public class SchemaSettingList : ReadOnlyListBaseEx<SchemaSettingList, SchemaSettingInfo>
	{

		#region Factory Methods

		private SchemaSettingList() { }

		public SchemaSettingInfo GetItem(string name)
		{
			foreach (SchemaSettingInfo obj in this)
				if (obj.Name == name)
					return obj;
			return null;
		}

		/// <summary>
		/// Retrieve the complete list from db
		/// </summary>
		/// <returns>AppSettingList</returns>
		public static SchemaSettingList GetList()
		{
			CriteriaEx criteria = SchemaSetting.GetCriteria(SchemaSetting.OpenSession());
			criteria.AddOrder("Name", true);

			//No criteria. Retrieve all de List
			SchemaSettingList list = DataPortal.Fetch<SchemaSettingList>(criteria);

            CloseSession(criteria.SessionCode);

			return list;
		}

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static SchemaSettingList GetList(CriteriaEx criteria)
        {
            return SchemaSettingList.RetrieveList(typeof(SchemaSetting), AppContext.ActiveSchema.Code, criteria);
        }

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<SchemaSettingInfo> GetSortedList(	string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<SchemaSettingInfo> sortedList =
				new SortedBindingList<SchemaSettingInfo>(GetList());
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
				IList list = criteria.List();

				if (list.Count > 0)
				{
					IsReadOnly = false;

					foreach (SchemaSetting item in list)
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

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return SchemaSetting.SELECT(conditions, true); }

		#endregion
    }
}
