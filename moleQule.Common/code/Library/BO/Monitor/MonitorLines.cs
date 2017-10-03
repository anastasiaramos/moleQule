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
	/// Editable Business Object Root Collection
	/// Editable Business Object Child Collection
	/// </summary>
    [Serializable()]
    public class MonitorLines : BusinessListBaseEx<MonitorLines, MonitorLine>
    {
		#region Business Methods
	
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public MonitorLine NewItem()
        {
            this.AddItem(MonitorLine.NewChild());
            return this[Count - 1];
        }
		public MonitorLine NewItem(Monitor parent)
		{
			this.NewItem(MonitorLine.NewChild(parent));
			MonitorLine item = this[Count - 1];
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
        private MonitorLines() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static MonitorLines NewList() 
		{ 	
			if (!MonitorLine.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new MonitorLines(); 
		}

		private static MonitorLines GetList(string query, bool childs)
		{
			if (!MonitorLine.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return GetList(query, childs, -1);
		}		
		
		public static MonitorLines GetList(bool childs = true) 
		{
			return GetList(MonitorLines.SELECT(), childs);
		}
		public static MonitorLines GetList(QueryConditions conditions, bool childs)
		{
			return GetList(MonitorLines.SELECT(conditions), childs);
		}
		
        #endregion
		
		#region Child Factory Methods

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="lista">IList de objetos</param>
        /// <remarks>NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods</remarks>
        private MonitorLines(IList<MonitorLine> lista)
		{
			MarkAsChild();
			Fetch(lista);
		}
        private MonitorLines(int sessionCode, IDataReader reader, bool childs)
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
        public static MonitorLines NewChildList() 
        { 
            MonitorLines list = new MonitorLines(); 
            list.MarkAsChild(); 
            return list; 
        }
		
		/// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="lista">IList origen</param>
        /// <returns>Lista creada</returns>
		public static MonitorLines GetChildList(IList<MonitorLine> lista) { return new MonitorLines(lista); }

        /// <summary>
        /// Construye una nueva lista
        /// </summary>
        /// <param name="reader">IDataReader origen</param>
        /// <returns>Lista creada</returns>
        /// <remarks>Obtiene los hijos</remarks>
        public static MonitorLines GetChildList(int sessionCode, IDataReader reader, bool childs = true) { return new MonitorLines(sessionCode, reader, childs); }		
		
		public static MonitorLines GetChildList(Monitor parent, bool childs)
		{
			CriteriaEx criteria = Monitor.GetCriteria(parent.SessionCode);

			criteria.Query = SELECT(parent);
			criteria.Childs = childs;

			MonitorLines list = DataPortal.Fetch<MonitorLines>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}		
		
		#endregion
		
		#region Root Data Access

        /// <summary>
        /// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        /// <remarks>LA UTILIZA EL DATAPORTAL</remarks>
        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
        /// hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria">Criterios de la consulta</param>
        private void Fetch(CriteriaEx criteria)
        {
            try
            {
				this.RaiseListChangedEvents = false;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
                {
                    MonitorLine.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(MonitorLine.GetChild(SessionCode, reader, Childs));
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
            finally
            {
                this.RaiseListChangedEvents = true;
            }
        }

        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="reader">IDataReader origen</param>
        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (MonitorLine obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (MonitorLine obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
							obj.Insert(this);
						else
							obj.Update(this);
					}
				}

                if (!SharedTransaction) Transaction().Commit();
            }
            catch (Exception ex)
            {
                if ((!SharedTransaction) && (Transaction() != null)) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                if (!SharedTransaction) BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion
		
		#region Child Data Access

		/// <summary>
        /// Construye la lista y obtiene los datos de los hijos de la bd
		/// </summary>
		/// <param name="lista">IList origen</param>
        private void Fetch(IList<MonitorLine> lista)
		{
			this.RaiseListChangedEvents = false;

			foreach (MonitorLine item in lista)
				this.AddItem(MonitorLine.GetChild(item, Childs));

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
                this.AddItem(MonitorLine.GetChild(SessionCode, reader, Childs));

            this.RaiseListChangedEvents = true;
        }
		
        /// <summary>
        /// Realiza el Save de los objetos de la lista. Inserta, Actualiza o Borra en función
		/// de los flags de cada objeto de la lista
		/// </summary>
		/// <param name="parent">BusinessBaseEx padre de la lista</param>
		internal void Update(Monitor parent)
		{
			try
			{
				this.RaiseListChangedEvents = false;

				SessionCode = parent.SessionCode;
				
				// update (thus deleting) any deleted child objects
				foreach (MonitorLine obj in DeletedList)
					obj.DeleteSelf(parent);

				// now that they are deleted, remove them from memory too
				DeletedList.Clear();

				// add/update any current child objects
				foreach (MonitorLine obj in this)
				{	
					if (obj.IsNew)
						obj.Insert(parent);
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

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return MonitorLine.SELECT(conditions, true); }		
		public static string SELECT(Monitor parent) { return MonitorLine.SELECT(new QueryConditions{ Monitor = parent.GetInfo(false) }, true); }
			
		#endregion
    }
}

