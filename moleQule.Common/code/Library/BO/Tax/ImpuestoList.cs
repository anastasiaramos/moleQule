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
	public class ImpuestoList : ReadOnlyListBaseEx<ImpuestoList, ImpuestoInfo>
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
		private ImpuestoList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private ImpuestoList(IList<Impuesto> list, bool retrieve_childs)
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
		private ImpuestoList(IDataReader reader, bool retrieve_childs)
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
		private ImpuestoList(IList<ImpuestoInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static ImpuestoList NewList() { return new ImpuestoList(); }

		public static ImpuestoList GetList() { return ImpuestoList.GetList(true); }
		public static ImpuestoList GetList(bool childs)
		{
			CriteriaEx criteria = Impuesto.GetCriteria(Impuesto.OpenSession());
			criteria.Childs = childs;
			
			//No criteria. Retrieve all de List			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = SELECT();
            
			ImpuestoList list = DataPortal.Fetch<ImpuestoList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		
		/// <summary>
		/// Devuelve una lista de todos los elementos
		/// </summary>
		/// <returns>Lista de elementos</returns>
		public static ImpuestoList GetList(CriteriaEx criteria)
		{
			return ImpuestoList.RetrieveList(typeof(Impuesto), AppContext.ActiveSchema.Code, criteria);
		}
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static ImpuestoList GetList(IList<Impuesto> list) { return new ImpuestoList(list,false); }
        public static ImpuestoList GetList(IList<ImpuestoInfo> list) { return new ImpuestoList(list, false); }
		public static ImpuestoList GetList(bool childs, bool cache)
		{
			ImpuestoList list;

			if (!Cache.Instance.Contains(typeof(ImpuestoList)))
			{
				list = ImpuestoList.GetList(childs);
				Cache.Instance.Save(typeof(ImpuestoList), list);
			}
			else
				list = Cache.Instance.Get(typeof(ImpuestoList)) as ImpuestoList;

			return list;
		}

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<ImpuestoInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<ImpuestoInfo> sortedList = new SortedBindingList<ImpuestoInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<ImpuestoInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<ImpuestoInfo> sortedList = new SortedBindingList<ImpuestoInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Impuesto> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Impuesto item in lista)
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
                this.AddItem(ImpuestoInfo.GetChild(reader, Childs));

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
						this.AddItem(ImpuestoInfo.GetChild(reader, Childs));

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
		public static string SELECT(QueryConditions conditions) { return Impuesto.SELECT(conditions, false); }
		
		#endregion		
	}
}
