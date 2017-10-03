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
	public class UserSettingList : ReadOnlyListBaseEx<UserSettingList, UserSettingInfo>
	{

		#region Factory Methods

		private UserSettingList() { }

		public UserSettingInfo GetItem(string name)
		{
			foreach (UserSettingInfo obj in this)
				if (obj.Name == name)
					return obj;
			return null;
		}

		public static UserSettingList GetList()
		{
			CriteriaEx criteria = UserSetting.GetCriteria(UserSetting.OpenSession());
			criteria.Query = SELECT();

			UserSettingList list = DataPortal.Fetch<UserSettingList>(criteria);

            CloseSession(criteria.SessionCode);

			return list;
		}
		public static UserSettingList GetListByUser(UserInfo user)
		{
			CriteriaEx criteria = UserSetting.GetCriteria(UserSetting.OpenSession());
			criteria.Query = SELECT(user);

			UserSettingList list = DataPortal.Fetch<UserSettingList>(criteria);

			CloseSession(criteria.SessionCode);

			return list;
		}

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static UserSettingList GetList(CriteriaEx criteria)
        {
            return UserSettingList.RetrieveList(typeof(UserSetting), AppContext.ActiveSchema.Code, criteria);
        }

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<UserSettingInfo> GetSortedList(	string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<UserSettingInfo> sortedList =
				new SortedBindingList<UserSettingInfo>(GetList());
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
						this.AddItem(UserSettingInfo.GetChild(SessionCode, reader, Childs));

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
		public static string SELECT(QueryConditions conditions) { return UserSetting.SELECT(conditions, false); }
		public static string SELECT(UserInfo user) { return UserSetting.SELECT(new QueryConditions { User = user }, false); }

		#endregion

    }
}
