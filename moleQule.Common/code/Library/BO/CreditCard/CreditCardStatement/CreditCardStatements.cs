using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Common
{
	/// <summary>
	/// Editable Business Object Root Collection
	/// </summary>
    [Serializable()]
    public class CreditCardStatements : BusinessListBaseEx<CreditCardStatements, CreditCardStatement>
    {		
		#region Root Business Methods
        
        public CreditCardStatement NewItem()
        {
            this.AddItem(CreditCardStatement.NewChild());
            return this[Count - 1];
        }
        public CreditCardStatement NewItem(CreditCard parent)
        {
            this.AddItem(CreditCardStatement.NewChild(parent));
            return this[Count - 1];
        }

        public CreditCardStatement GetByFromItem(DateTime from)
        {
            return Items.FirstOrDefault(x => x.From == from);
        }

        public CreditCardStatement GetByDueDateItem(DateTime dueDate)
        {
            return Items.FirstOrDefault(x => x.DueDate == dueDate);
        }

        #endregion
		
		#region Common Factory Methods

		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
        private CreditCardStatements() { }

		#endregion		
		
        #region Child Factory Methods

		public CreditCardStatements(bool is_child)
        {
			if (is_child) MarkAsChild();
        }
        private CreditCardStatements(IList<CreditCardStatement> list)
        {
            MarkAsChild();
            Fetch(list);
        }
        private CreditCardStatements(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

        public static CreditCardStatements NewChildList() { return new CreditCardStatements(true); }

        public static CreditCardStatements GetChildList(IList<CreditCardStatement> lista) { return new CreditCardStatements(lista); }
        public static CreditCardStatements GetChildList(int sessionCode, IDataReader reader, bool childs = true) { return new CreditCardStatements(sessionCode, reader, childs); }

        public static CreditCardStatements GetChildList(CreditCard parent, bool childs)
        {
            CriteriaEx criteria = CreditCardStatement.GetCriteria(parent.SessionCode);
            criteria.Query = SELECT(parent);
            criteria.Childs = childs;

            return DataPortal.Fetch<CreditCardStatements>(criteria);
        }

        #endregion

		#region Root Factory Methods
		
        public static CreditCardStatements NewList() 
		{ 	
			if (!CreditCardStatement.CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			
			return new CreditCardStatements(); 
		}

        public static CreditCardStatements GetList(bool childs = false)
        {
            if (!CreditCardStatement.CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            CriteriaEx criteria = CreditCardStatement.GetCriteria(CreditCardStatement.OpenSession());
			criteria.Childs = childs;
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = CreditCardStatements.SELECT();
            
            CreditCardStatement.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<CreditCardStatements>(criteria);
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
                    CreditCardStatement.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query);

                    while (reader.Read())
                        this.AddItem(CreditCardStatement.GetChild(SessionCode, reader, Childs));
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
            foreach (CreditCardStatement obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
				// add/update any current child objects
				foreach (CreditCardStatement obj in this)
				{
					if (!this.Contains(obj))
					{
						if (obj.IsNew)
							obj.Insert(this);
						else
							obj.Update(this);
					}
				}

                Transaction().Commit();
            }
            catch (Exception ex)
            {
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
            }
            finally
            {
                BeginTransaction();
                this.RaiseListChangedEvents = true;
            }
        }

        #endregion		
			
        #region Child Data Access

        // called to copy objects data from list
        private void Fetch(IList<CreditCardStatement> lista)
        {
            this.RaiseListChangedEvents = false;

            foreach (CreditCardStatement item in lista)
            {
                this.AddItem(CreditCardStatement.GetChild(item));
            }

            this.RaiseListChangedEvents = true;
        }

        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            MaxSerial = 0;

            while (reader.Read())
            {
                CreditCardStatement item = CreditCardStatement.GetChild(SessionCode, reader);
                this.AddItem(item);
            }

            this.RaiseListChangedEvents = true;
        }

        internal void Update(CreditCard parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (CreditCardStatement obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (CreditCardStatement obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }

        internal void Delete(CreditCard parent)
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (CreditCardStatement obj in DeletedList)
                obj.DeleteSelf(parent);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            // add/update any current child objects
            foreach (CreditCardStatement obj in this)
            {
                if (obj.IsNew)
                    obj.Insert(parent);
                else
                    obj.Update(parent);
            }

            this.RaiseListChangedEvents = true;
        }

        #endregion

        #region SQL

        public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return CreditCardStatement.SELECT(conditions, true); }
        public static string SELECT(CreditCard parent) { return CreditCardStatement.SELECT(new QueryConditions() { CreditCard = parent.GetInfo(false) }, true); }

		#endregion
    }
}

