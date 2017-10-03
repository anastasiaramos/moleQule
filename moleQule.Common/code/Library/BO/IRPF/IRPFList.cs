using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx;

namespace moleQule.Library.Common
{	
	/// <summary>
	/// ReadOnly Business Object Root Collection
	/// </summary>
    [Serializable()]
	public class IRPFList : ReadOnlyListBaseEx<IRPFList, IRPFInfo>
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
		private IRPFList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private IRPFList(IList<IRPF> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private IRPFList(IDataReader reader, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(reader);
        }
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private IRPFList(IList<IRPFInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static IRPFList NewList() { return new IRPFList(); }

		public static IRPFList GetList() { return IRPFList.GetList(true); }
		public static IRPFList GetList(bool childs)
		{
			CriteriaEx criteria = IRPF.GetCriteria(IRPF.OpenSession());
			criteria.Childs = childs;
			
			//No criteria. Retrieve all de List			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();
            
			IRPFList list = DataPortal.Fetch<IRPFList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		
		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static IRPFList GetList(CriteriaEx criteria)
		{
			return IRPFList.RetrieveList(typeof(IRPF), AppContext.ActiveSchema.Code, criteria);
		}
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static IRPFList GetList(IList<IRPF> list) { return new IRPFList(list,false); }
        public static IRPFList GetList(IList<IRPFInfo> list) { return new IRPFList(list, false); }
		public static IRPFList GetList(bool childs, bool cache)
		{
			IRPFList list;

			if (!Cache.Instance.Contains(typeof(IRPFList)))
			{
				list = IRPFList.GetList(childs);
				Cache.Instance.Save(typeof(IRPFList), list);
			}
			else
				list = Cache.Instance.Get(typeof(IRPFList)) as IRPFList;

			return list;
		}

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<IRPFInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<IRPFInfo> sortedList = new SortedBindingList<IRPFInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<IRPFInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<IRPFInfo> sortedList = new SortedBindingList<IRPFInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<IRPF> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (IRPF item in lista)
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
                this.AddItem(IRPFInfo.GetChild(reader, Childs));

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
						this.AddItem(IRPFInfo.GetChild(reader, Childs));

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
		public static string SELECT(QueryConditions conditions) { return IRPF.SELECT(conditions, false); }
		
		#endregion		
	}
}
