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
	/// Editable Business Object Root Collection
	/// </summary>
    [Serializable()]
    public class SubtipoFacturas : BusinessListBaseEx<SubtipoFacturas, SubtipoFactura>
    {
		#region Business Methods
        		
		/// <summary>
        /// Crea y añade un nuevo elemento a la lista principal
        /// El elemento SE CREARA en la tabla correspondiente cuando se guarde la lista
        /// </summary>
        public SubtipoFactura NewItem()
        {
            this.AddItem(SubtipoFactura.NewChild());
            return this[Count - 1];
        }
		
		#endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private SubtipoFacturas() { }

		#endregion		
		
		#region Root Factory Methods
		
        /// <summary>
        /// Crea una nueva lista vacía
        /// </summary>
        /// <returns>Lista vacía</returns>
        public static SubtipoFacturas NewList() 
		{ 	
			if (!SubtipoFactura.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new SubtipoFacturas(); 
		}

        public static SubtipoFacturas GetList() { return GetList(true); }		
		public static SubtipoFacturas GetList(bool childs) 
		{
			return GetList(SELECT(), childs);
		}
        public static SubtipoFacturas GetList(ESubtipoFactura tipo)
        {
            QueryConditions conditions = new QueryConditions { SubtipoFactura = tipo };
            return GetList(conditions, false);
        }
		public static SubtipoFacturas GetList(QueryConditions conditions, bool childs)
		{
			return GetList(SubtipoFacturas.SELECT(conditions), childs);
		}
		
		private static SubtipoFacturas GetList(string query, bool childs)
		{
            if (!SubtipoFactura.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = SubtipoFactura.GetCriteria(SubtipoFactura.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;
				
			SubtipoFactura.BeginTransaction(criteria.SessionCode);

            return DataPortal.Fetch<SubtipoFacturas>(criteria);
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
                    SubtipoFactura.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(SubtipoFactura.GetChild(SessionCode, reader, Childs));
                }
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
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
            foreach (SubtipoFactura obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (SubtipoFactura obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
							obj.Insert(this);
						else
							obj.Update(this);
					}
				}

                if (!SaveAsChildList) Transaction().Commit();
            }
            catch (Exception ex)
            {
                if (!SaveAsChildList) if (Transaction() != null) Transaction().Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
            finally
            {
                if (!SaveAsChildList) BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion		
			
        #region SQL

		internal static string SELECT() { return SELECT(new QueryConditions()); }
		internal static string SELECT(QueryConditions conditions)
		{
			OrderList orders = new OrderList();
			orders.NewOrder("Tipo", ListSortDirection.Ascending, typeof(SubtipoFactura));
			orders.NewOrder("Codigo", ListSortDirection.Ascending, typeof(SubtipoFactura));
			conditions.Orders = orders;
			return SubtipoFactura.SELECT(conditions, true);
		}

		#endregion
    }
}

