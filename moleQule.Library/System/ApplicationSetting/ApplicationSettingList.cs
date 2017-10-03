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
	public class ApplicationSettingList : ReadOnlyListBaseEx<ApplicationSettingList, ApplicationSettingInfo>
	{

		#region Factory Methods

		private ApplicationSettingList() { }

		public ApplicationSettingInfo GetItem(string name)
		{
			foreach (ApplicationSettingInfo obj in this)
				if (obj.Name == name)
					return obj;
			return null;
		}

		/// <summary>
		/// Retrieve the complete list from db
		/// </summary>
		/// <returns>VariableList</returns>
		public static ApplicationSettingList GetList()
		{
			CriteriaEx criteria = ApplicationSetting.GetCriteria(ApplicationSetting.OpenSession());
			criteria.AddOrder("Name", true);

			//No criteria. Retrieve all de List
			ApplicationSettingList list = DataPortal.Fetch<ApplicationSettingList>(criteria);

            CloseSession(criteria.SessionCode);

			return list;
		}

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static ApplicationSettingList GetList(CriteriaEx criteria)
        {
            return ApplicationSettingList.RetrieveList(typeof(ApplicationSetting), AppContext.ActiveSchema.Code, criteria);
        }

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<ApplicationSettingInfo> GetSortedList(	string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<ApplicationSettingInfo> sortedList =
				new SortedBindingList<ApplicationSettingInfo>(GetList());
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

					foreach (ApplicationSetting item in list)
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
