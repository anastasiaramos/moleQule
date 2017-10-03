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
	/// ReadOnly Business Object Child Collection
	/// </summary>
    [Serializable()]
	public class LineaRegistroList : ReadOnlyListBaseEx<LineaRegistroList, LineaRegistroInfo>
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
		private LineaRegistroList() {}
		private LineaRegistroList(IList<LineaRegistro> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		private LineaRegistroList(int sessionCode, IDataReader reader, bool retrieve_childs)
        {
			SessionCode = sessionCode;
			Childs = retrieve_childs;
            Fetch(reader);
        }
		private LineaRegistroList(IList<LineaRegistroInfo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		
		#endregion

		#region Root Factory Methods

		public static LineaRegistroList NewList() { return new LineaRegistroList(); }

		public static LineaRegistroList GetList() {	return LineaRegistroList.GetList(true);	}
		public static LineaRegistroList GetList(bool childs)
		{
			return GetList(LineaRegistroList.SELECT(), childs);
		}
		public static LineaRegistroList GetList(ETipoRegistro tipo) { return GetList(tipo, true); }
		public static LineaRegistroList GetList(ETipoRegistro tipo, bool childs)
		{
			QueryConditions conditions = new QueryConditions { TipoRegistro = tipo };
			return GetList(LineaRegistroList.SELECT(conditions), childs);
		}
		public static LineaRegistroList GetList(ETipoRegistro tipo, int year, bool childs)
		{
			return GetList(tipo, DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static LineaRegistroList GetList(ETipoRegistro tipo, DateTime f_ini, DateTime f_fin, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				TipoRegistro = tipo,
				FechaIni = f_ini,
				FechaFin = f_fin
			};

			return GetList(LineaRegistroList.SELECT(conditions), childs);
		}

        public static LineaRegistroList GetRegistroEmailsByCliente(long oid_cliente, bool childs)
        {
            QueryConditions conditions = new QueryConditions 
            {
                TipoRegistro = ETipoRegistro.Email,
                OidEntity = oid_cliente,
                Order = ListSortDirection.Descending
            };

            return GetList(LineaRegistroList.SELECT(conditions), childs);
        }

		private static LineaRegistroList GetList(string query, bool childs)
		{
			CriteriaEx criteria = LineaRegistro.GetCriteria(LineaRegistro.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = query;

			LineaRegistroList list = DataPortal.Fetch<LineaRegistroList>(criteria);
			CloseSession(criteria.SessionCode);
			return list;
		}
		public static LineaRegistroList GetList(CriteriaEx criteria)
		{
			return LineaRegistroList.RetrieveList(typeof(LineaRegistro), AppContext.ActiveSchema.Code, criteria);
		}
		public static LineaRegistroList GetList(IList<LineaRegistro> list) { return new LineaRegistroList(list, false); }
		public static LineaRegistroList GetList(IList<LineaRegistroInfo> list) { return new LineaRegistroList(list, false); }
		public static SortedBindingList<LineaRegistroInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<LineaRegistroInfo> sortedList = new SortedBindingList<LineaRegistroInfo>(GetList());

			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		public static SortedBindingList<LineaRegistroInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
		{
			SortedBindingList<LineaRegistroInfo> sortedList = new SortedBindingList<LineaRegistroInfo>(GetList(childs));

			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}

		#endregion

		#region Child Factory Methods
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static LineaRegistroList GetChildList(IList<LineaRegistro> list, bool childs = false) { return new LineaRegistroList(list, childs); }
		public static LineaRegistroList GetChildList(int sessionCode, IDataReader reader, bool childs = false) { return new LineaRegistroList(sessionCode, reader, childs); }
        public static LineaRegistroList GetChildList(IList<LineaRegistroInfo> list) { return new LineaRegistroList(list, false); }
		
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<LineaRegistro> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (LineaRegistro item in lista)
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
                this.AddItem(LineaRegistroInfo.GetChild(SessionCode, reader, Childs));

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
						this.AddItem(LineaRegistroInfo.GetChild(SessionCode, reader, Childs));

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

        public static string SELECT() { return LineaRegistroInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return LineaRegistro.SELECT(conditions, false); }
		public static string SELECT(RegistroInfo parent) { return LineaRegistro.SELECT(new QueryConditions { Registro = parent }, false); }
		
		#endregion		
	}
}
