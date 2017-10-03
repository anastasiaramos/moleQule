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
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class AyudaPeriodos : BusinessListBaseEx<AyudaPeriodos, AyudaPeriodo>
    {
		#region Root Business Methods
	
		/// <summary>
		/// Crea un nuevo elemento y lo añade a la lista
		/// </summary>
		/// <returns>Nuevo item</returns>
		public AyudaPeriodo NewItem(Ayuda parent)
		{
			this.NewItem(AyudaPeriodo.NewChild(parent));
			AyudaPeriodo item = this[Count - 1];
			return item;
		}
		
		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private AyudaPeriodos() { }

		#endregion		
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private AyudaPeriodos(IList<AyudaPeriodo> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <param name="retrieve_childs">Flag para indicar si quiere obtener los hijos</param>
		
        private AyudaPeriodos(int sessionCode, IDataReader reader, bool childs)
        {
            MarkAsChild();
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
		
		
		/// <summary>
        /// Construye una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static AyudaPeriodos NewChildList() 
        { 
            AyudaPeriodos list = new AyudaPeriodos(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static AyudaPeriodos GetChildList(IList<AyudaPeriodo> lista) { return new AyudaPeriodos(lista); }

        /// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        /// <returns>Lista creada</returns>
        /// <remarks>Obtiene los hijos</remarks>
		
        public static AyudaPeriodos GetChildList(int sessionCode,IDataReader reader) { return GetChildList(sessionCode, reader, true); }
        public static AyudaPeriodos GetChildList(int sessionCode,IDataReader reader, bool childs) { return new AyudaPeriodos(sessionCode, reader, childs); }
		
		#endregion
		
		#region Child Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
        private void Fetch(IList<AyudaPeriodo> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (AyudaPeriodo item in lista)
				this.AddItem(AyudaPeriodo.GetChild(item, Childs));

			this.RaiseListChangedEvents = true;
		}
		
        /// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="reader">IDataReader origen con los elementos a insertar</param>
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            while (reader.Read())
                this.AddItem(AyudaPeriodo.GetChild(SessionCode, reader, Childs));

            this.RaiseListChangedEvents = true;
        }
		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(Ayuda parent)
		{
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (AyudaPeriodo obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (AyudaPeriodo obj in this)
				{
					if (obj.IsNew)
					{
						obj.Insert(parent);
					}
					else
						obj.Update(parent);
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
		}
		
		#endregion
			
        #region SQL

        public static string SELECT() { return AyudaPeriodo.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return AyudaPeriodo.SELECT(conditions, true); }
		
		public static string SELECT(Ayuda parent) { return AyudaPeriodo.SELECT(new QueryConditions{ Ayuda = parent.GetInfo(false) }, true); }
			
		#endregion
    }
}

