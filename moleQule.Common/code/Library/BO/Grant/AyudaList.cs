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
	/// ReadOnly Business Object With Childs Root Collection  
	/// </summary>
    [Serializable()]
	public class AyudaList : ReadOnlyListBaseEx<AyudaList, AyudaInfo>
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
		private AyudaList() {}
		private AyudaList(IList<Ayuda> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		private AyudaList(IList<AyudaInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static AyudaList NewList() { return new AyudaList(); }

		public static AyudaList GetList() { return AyudaList.GetList(true); }
		public static AyudaList GetList(bool childs)
		{
			CriteriaEx criteria = Ayuda.GetCriteria(Ayuda.OpenSession());
			criteria.Childs = childs;
			
			//No criteria. Retrieve all de List
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = AyudaList.SELECT();
            
			AyudaList list = DataPortal.Fetch<AyudaList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		
		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static AyudaList GetList(CriteriaEx criteria)
		{
			return AyudaList.RetrieveList(typeof(Ayuda), AppContext.ActiveSchema.Code, criteria);
		}
        public static AyudaList GetList(IList<Ayuda> list) { return new AyudaList(list,false); }
        public static AyudaList GetList(IList<AyudaInfo> list) { return new AyudaList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<AyudaInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<AyudaInfo> sortedList = new SortedBindingList<AyudaInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<AyudaInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<AyudaInfo> sortedList = new SortedBindingList<AyudaInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Ayuda> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Ayuda item in lista)
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
                this.AddItem(AyudaInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(AyudaInfo.GetChild(SessionCode, reader, Childs));

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

        public static string SELECT() { return AyudaInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Ayuda.SELECT(conditions, false); }
		
		#endregion		
	}
}
