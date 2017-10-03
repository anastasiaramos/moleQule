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
    public class MunicipioList :
		ReadOnlyListBaseEx<MunicipioList, MunicipioInfo>
    {
        #region Factory Methods

        private MunicipioList() {}

        private MunicipioList(IList<MunicipioInfo> list)
        {
			Childs = false;
            Fetch(list);
        }

		public static MunicipioList NewList() { return new MunicipioList(); }

		public static MunicipioList GetList(bool childs)
		{
			CriteriaEx criteria = Municipio.GetCriteria(Municipio.OpenSession());
            criteria.Childs = childs;
            criteria.Query = SELECT();

			//No criteria. Retrieve all de List
			MunicipioList list = DataPortal.Fetch<MunicipioList>(criteria);

            CloseSession(criteria.SessionCode);

			return list;
		}

        /// <summary>
        /// Default call for GetList(bool get_childs)
        /// </summary>
        /// <returns></returns>
        public static MunicipioList GetList()
        {
            return MunicipioList.GetList(true);
        }

        /// <summary>
        /// Devuelve una lista de todos los elementos
        /// </summary>
        /// <returns>Lista de elementos</returns>
        public static MunicipioList GetList(CriteriaEx criteria)
        {
            return MunicipioList.RetrieveList(typeof(Municipio), AppContext.ActiveSchema.Code, criteria);
        }

        public static MunicipioList GetList(IList<MunicipioInfo> list) { return new MunicipioList(list); }

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<MunicipioInfo> GetSortedList(string sortProperty,
																	ListSortDirection sortDirection)
		{
			SortedBindingList<MunicipioInfo> sortedList =
				new SortedBindingList<MunicipioInfo>(GetList());
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
						this.AddItem(MunicipioInfo.GetChild(reader, Childs));

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

		internal static string SELECT() { return SELECT(new QueryConditions()); }
		internal static string SELECT(QueryConditions conditions)
		{
			OrderList orders = new OrderList();
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(Municipio));
			conditions.Orders = orders;
			return Municipio.SELECT(conditions, false);
		}

		#endregion
    }
}
