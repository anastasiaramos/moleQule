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
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class RegistryLines : BusinessListBaseEx<RegistryLines, LineaRegistro>
	{
		#region Child Business Methods

		/// <summary>
		/// Crea un nuevo elemento y lo añade a la lista
		/// </summary>
		/// <returns>Nuevo item</returns>
		public LineaRegistro NewItem(Registro parent, IEntidadRegistroInfo source)
		{
			this.NewItem(LineaRegistro.NewChild(parent, source));
			LineaRegistro obj = this[Count - 1];
			SetNextCode(parent, obj);
			return obj;
		}

		public void SetNextCode(Registro parent, LineaRegistro item)
		{
			int index = this.IndexOf(item);

			if (index == 0)
			{
				item.GetNewCode(parent);
			}
			else
			{
				item.Serial = this[index - 1].Serial + 1;
				item.Codigo = item.Serial.ToString(Resources.Defaults.LINEA_REGISTRO_CODE_FORMAT);

				if (parent.ETipoRegistro == ETipoRegistro.Contabilidad)
				{
					long serial = Convert.ToInt64(this[this.IndexOf(item) - 1].SerialExportacion) + 1;
					item.SerialExportacion = serial.ToString(Resources.Defaults.ID_EXPORTACION_CODE_FORMAT);
				}
			}
		}
		
		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private RegistryLines() { }

		#endregion		
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private RegistryLines(IList<LineaRegistro> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
        private RegistryLines(int sessionCode, IDataReader reader, bool childs)
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
        public static RegistryLines NewChildList() 
        { 
            RegistryLines list = new RegistryLines(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static RegistryLines GetChildList(IList<LineaRegistro> lista) { return new RegistryLines(lista); }
		public static RegistryLines GetChildList(int sessionCode, IDataReader reader, bool childs = true) { return new RegistryLines(sessionCode, reader, childs); }

		#endregion
		
		#region Child Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
        private void Fetch(IList<LineaRegistro> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (LineaRegistro item in lista)
				this.AddItem(LineaRegistro.GetChild(item, Childs));

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
                this.AddItem(LineaRegistro.GetChild(reader, Childs));

            this.RaiseListChangedEvents = true;
        }
		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(Registro parent, List<TEntidadRegistroList> lists)
		{
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;

				// update (thus deleting) any deleted child objects
				foreach (LineaRegistro obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (LineaRegistro obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
						{
							SetNextCode(parent, obj);
							obj.Insert(parent);
						}
						else
							obj.Update(parent, lists);
					}
				}
			}
			finally
			{
				this.RaiseListChangedEvents = true;
			}
		}
		
		#endregion
			
        #region SQL

        public static string SELECT() { return LineaRegistro.SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return LineaRegistro.SELECT(conditions, true); }
		public static string SELECT(Registro parent) { return LineaRegistro.SELECT(new QueryConditions { Registro = parent.GetInfo(false) }, true); }
		
		#endregion
    }
}

