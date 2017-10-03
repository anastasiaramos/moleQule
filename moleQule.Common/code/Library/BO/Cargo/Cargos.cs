using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using Csla.Validation;
using moleQule.Library.CslaEx; 

using moleQule.Library;

using NHibernate;

namespace moleQule.Library.Common
{
    /// <summary>
    /// Editable Root Collection
    /// </summary>
    [Serializable()]
    public class Cargos : BusinessListBaseEx<Cargos, Cargo>
    {
        #region Business Methods

        public Cargo NewItem()
        {
            this.NewItem(Cargo.NewChild());
            return this[Count - 1];
        }

        public bool ExistOtherItem(Cargo child)
        {
            foreach (Cargo obj in this)
                if ((obj.Oid != child.Oid) && (obj.Valor == child.Valor))
                    return true;
            return false;
        }

        #endregion

        #region Authorization Rules

        public static bool CanAddObject()
        {
            return AutorizationRulesControler.CanAddObject(Resources.SecureItems.AUXILIARES);
        }

        public static bool CanGetObject()
        {
            return AutorizationRulesControler.CanGetObject(Resources.SecureItems.AUXILIARES);
        }

        public static bool CanDeleteObject()
        {
            return AutorizationRulesControler.CanDeleteObject(Resources.SecureItems.AUXILIARES);
        }

        public static bool CanEditObject()
        {
            return AutorizationRulesControler.CanEditObject(Resources.SecureItems.AUXILIARES);
        }

        #endregion

        #region Factory Methods

        private Cargos() { }

        public static Cargos NewList() { return new Cargos(); }

        public static Cargos GetList()
        {
            CriteriaEx criteria = Cargo.GetCriteria(Cargo.OpenSession());
			criteria.Query = SELECT();
			criteria.Childs = false;

            Cargo.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<Cargos>(criteria);
        }

        #endregion

        #region Root Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            Fetch(criteria);
        }

        private void Fetch(CriteriaEx criteria)
        {
            this.RaiseListChangedEvents = false;

            SessionCode = criteria.SessionCode;

            try
            {
                if (nHMng.UseDirectSQL)
                {
                    Cargo.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(Cargo.GetChild(SessionCode, reader, Childs));
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
		
        protected override void DataPortal_Update()
        {
            this.RaiseListChangedEvents = false;

            // update (thus deleting) any deleted child objects
            foreach (Cargo obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (Cargo obj in this)
                {
                    if (obj.IsNew)
                        obj.Insert(this);
                    else
                        obj.Update(this);
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

		#region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return Cargo.SELECT(conditions, true); }

		#endregion	
    }
}
