using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library;

namespace moleQule.Library.Common
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
	/// </summary>
    [Serializable()]
	public class SubtipoFacturaList : ReadOnlyListBaseEx<SubtipoFacturaList, SubtipoFacturaInfo>
	{	
		#region Business Methods
			
		#endregion
		 
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private SubtipoFacturaList() {}
		private SubtipoFacturaList(IList<SubtipoFactura> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private SubtipoFacturaList(IList<SubtipoFacturaInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods
		
		public static SubtipoFacturaList NewList() { return new SubtipoFacturaList(); }
		
		public static SubtipoFacturaList GetList() { return SubtipoFacturaList.GetList(true); }
		public static SubtipoFacturaList GetList(bool childs)
		{
			//No criteria. Retrieve all de List
			return GetList(SELECT(), childs);
			
		}
        public static SubtipoFacturaList GetList(ESubtipoFactura tipo)
        {
            QueryConditions conditions = new QueryConditions { SubtipoFactura = tipo };

            return GetList(SubtipoFactura.SELECT(conditions), false);
        }
		
		private static SubtipoFacturaList GetList(string query, bool childs)
		{
			CriteriaEx criteria = SubtipoFactura.GetCriteria(SubtipoFactura.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

			SubtipoFacturaList list = DataPortal.Fetch<SubtipoFacturaList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		
		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static SubtipoFacturaList GetList(CriteriaEx criteria)
		{
			return SubtipoFacturaList.RetrieveList(typeof(SubtipoFactura), AppContext.ActiveSchema.Code, criteria);
		}
        public static SubtipoFacturaList GetList(IList<SubtipoFactura> list) { return new SubtipoFacturaList(list,false); }
        public static SubtipoFacturaList GetList(IList<SubtipoFacturaInfo> list) { return new SubtipoFacturaList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<SubtipoFacturaInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<SubtipoFacturaInfo> sortedList = new SortedBindingList<SubtipoFacturaInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<SubtipoFacturaInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<SubtipoFacturaInfo> sortedList = new SortedBindingList<SubtipoFacturaInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<SubtipoFactura> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (SubtipoFactura item in lista)
				this.AddItem(item.GetInfo(Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
                this.AddItem(SubtipoFacturaInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion

		#region Root Data Access
		 
		/// <summary>
		/// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
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
						this.AddItem(SubtipoFacturaInfo.GetChild(SessionCode, reader, Childs));

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
			orders.NewOrder("Tipo", ListSortDirection.Ascending, typeof(SubtipoFactura));
			orders.NewOrder("Codigo", ListSortDirection.Ascending, typeof(SubtipoFactura));
			conditions.Orders = orders;
			return SubtipoFactura.SELECT(conditions, false);
		}

		#endregion
	}
}
