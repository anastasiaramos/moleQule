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
	/// Read Only Root Collection of Business Objects
	/// </summary>
	[Serializable()]
	public class SettingItemList : ReadOnlyListBaseEx<SettingItemList, SettingItemInfo>
	{
		#region Factory Methods

		private SettingItemList() { }

		public SettingItemInfo GetItem(string name)
		{
			foreach (SettingItemInfo obj in this)
				if (obj.Name == name)
					return obj;
			return null;
		}

		public static SettingItemList GetList()
		{
			CriteriaEx criteria = SettingItem.GetCriteria(SettingItem.OpenSession());
			criteria.Query = SELECT();

			SettingItemList list = DataPortal.Fetch<SettingItemList>(criteria);

            CloseSession(criteria.SessionCode);

			return list;
		}
		public static SettingItemList GetListByUser(UserInfo user)
		{
			CriteriaEx criteria = SettingItem.GetCriteria(SettingItem.OpenSession());
			criteria.Query = SELECT(user);

			SettingItemList list = DataPortal.Fetch<SettingItemList>(criteria);

			CloseSession(criteria.SessionCode);

			return list;
		}

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static SettingItemList GetList(CriteriaEx criteria)
        {
            return SettingItemList.RetrieveList(typeof(SettingItem), AppContext.ActiveSchema.Code, criteria);
        }

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<SettingItemInfo> GetSortedList(	string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<SettingItemInfo> sortedList =
				new SortedBindingList<SettingItemInfo>(GetList());
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
			Childs = criteria.Childs;

			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					IsReadOnly = false;

					while (reader.Read())
						this.AddItem(SettingItemInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return SettingItem.SELECT(conditions, false); }
		public static string SELECT(UserInfo user) { return SettingItem.SELECT(new QueryConditions { User = user }, false); }

		#endregion

    }
}
