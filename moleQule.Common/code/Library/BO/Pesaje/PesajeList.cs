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
	public class PesajeList : ReadOnlyListBaseEx<PesajeList, PesajeInfo>
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
		private PesajeList() {}
		private PesajeList(IList<Pesaje> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		private PesajeList(IList<PesajeInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods
		
		public static PesajeList NewList() { return new PesajeList(); }
		
		public static PesajeList GetList() { return PesajeList.GetList(true); }
		public static PesajeList GetList(bool childs) { return GetList(SELECT(), childs); }
		public static PesajeList GetList(DateTime fechaIni, DateTime fechaFin, bool childs) 
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = fechaIni,
				FechaFin = fechaFin
			};

			return GetList(SELECT(conditions), childs); 
		}
		
		private static PesajeList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Pesaje.GetCriteria(Pesaje.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

			PesajeList list = DataPortal.Fetch<PesajeList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		
		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static PesajeList GetList(CriteriaEx criteria)
		{
			return PesajeList.RetrieveList(typeof(Pesaje), AppContext.ActiveSchema.Code, criteria);
		}
        public static PesajeList GetList(IList<Pesaje> list) { return new PesajeList(list,false); }
        public static PesajeList GetList(IList<PesajeInfo> list) { return new PesajeList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<PesajeInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<PesajeInfo> sortedList = new SortedBindingList<PesajeInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<PesajeInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<PesajeInfo> sortedList = new SortedBindingList<PesajeInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Pesaje> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Pesaje item in lista)
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
                this.AddItem(PesajeInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(PesajeInfo.GetChild(SessionCode, reader, Childs));

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

        public static string SELECT() { return PesajeInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Pesaje.SELECT(conditions, false); }
		
		#endregion		
	}
}
