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
	public class AyudaPeriodoList : ReadOnlyListBaseEx<AyudaPeriodoList, AyudaPeriodoInfo>
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
		private AyudaPeriodoList() {}
		private AyudaPeriodoList(IList<AyudaPeriodo> list, bool retrieve_childs)
        {
			Childs = retrieve_childs;
            Fetch(list);
        }
		private AyudaPeriodoList(IList<AyudaPeriodoInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
		
		#endregion
		
		#region Child Factory Methods
						
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>		
		private AyudaPeriodoList(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
				
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static AyudaPeriodoList GetChildList(IList<AyudaPeriodo> list) { return new AyudaPeriodoList(list, false); }
		public static AyudaPeriodoList GetChildList(IList<AyudaPeriodo> list, bool childs) { return new AyudaPeriodoList(list, childs); }

		/// <summary>
        /// Construye la lista
        /// </summary>
        /// <param name="reader">IDataReader</param>
        /// <returns>Lista de objetos de solo lectura</returns>
        /// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		
		public static AyudaPeriodoList GetChildList(int sessionCode, IDataReader reader) { return new AyudaPeriodoList(sessionCode, reader, false); } 
		public static AyudaPeriodoList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new AyudaPeriodoList(sessionCode, reader, childs); }
		
		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
        /// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
        public static AyudaPeriodoList GetChildList(IList<AyudaPeriodoInfo> list) { return new AyudaPeriodoList(list, false); }
		
		#endregion
		
		#region Common Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
		private void Fetch(IList<AyudaPeriodo> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;
			
			foreach (AyudaPeriodo item in lista)
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
                this.AddItem(AyudaPeriodoInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
        #endregion
		
        #region SQL

        public static string SELECT() { return AyudaPeriodoInfo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return AyudaPeriodo.SELECT(conditions, false); }
		
		public static string SELECT(AyudaInfo parent) { return  AyudaPeriodo.SELECT(new QueryConditions{ Ayuda = parent }, true); }
		
		#endregion		
	}
}
