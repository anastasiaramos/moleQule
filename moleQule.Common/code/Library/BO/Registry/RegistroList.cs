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
	public class RegistroList : ReadOnlyListBaseEx<RegistroList, RegistroInfo>
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
		private RegistroList() {}
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private RegistroList(IList<Registro> list, bool retrieve_childs)
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
		private RegistroList(IDataReader reader, bool retrieve_childs)
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
		private RegistroList(IList<RegistroInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Root Factory Methods

		public static RegistroList NewList() { return new RegistroList(); }

		public static RegistroList GetList() { return RegistroList.GetList(true); }
		public static RegistroList GetList(bool childs)
		{
			return GetList(RegistroList.SELECT(), childs);
		}
		public static RegistroList GetList(ETipoRegistro tipo, bool childs)
		{
			QueryConditions conditions = new QueryConditions { TipoRegistro = tipo };
			return GetList(RegistroList.SELECT(conditions), childs);
		}
		public static RegistroList GetList(ETipoRegistro tipo, int year, bool childs)
		{
			return GetList(tipo, DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static RegistroList GetList(ETipoRegistro tipo, DateTime f_ini, DateTime f_fin, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoRegistro = tipo,
				FechaIni = f_ini,
				FechaFin = f_fin
			};

			return GetList(RegistroList.SELECT(conditions), childs);
		}

		private static RegistroList GetList(string query, bool childs)
		{
			CriteriaEx criteria = Registro.GetCriteria(Registro.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			RegistroList list = DataPortal.Fetch<RegistroList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		public static RegistroList GetList(CriteriaEx criteria)
		{
			return RegistroList.RetrieveList(typeof(Registro), AppContext.ActiveSchema.Code, criteria);
		}
        public static RegistroList GetList(IList<Registro> list) { return new RegistroList(list,false); }
        public static RegistroList GetList(IList<RegistroInfo> list) { return new RegistroList(list, false); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<RegistroInfo> GetSortedList (string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<RegistroInfo> sortedList = new SortedBindingList<RegistroInfo>(GetList());
			
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
        public static SortedBindingList<RegistroInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
        {
            SortedBindingList<RegistroInfo> sortedList = new SortedBindingList<RegistroInfo>(GetList(childs));

            sortedList.ApplySort(sortProperty, sortDirection);
            return sortedList;
        }
			
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<Registro> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (Registro item in lista)
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
                this.AddItem(RegistroInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(RegistroInfo.GetChild(SessionCode, reader, Childs));

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

        public static string SELECT() { return RegistroInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Registro.SELECT(conditions, false); }
		
		#endregion		
	}
}
