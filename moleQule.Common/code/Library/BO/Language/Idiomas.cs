using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using Csla;
using moleQule.Library.CslaEx; 

using moleQule.Library;

using NHibernate;

namespace moleQule.Library.Common
{
    /// <summary>
    /// Editable Root Collection
    /// </summary>
    [Serializable()]
    public class Idiomas : BusinessListBaseEx<Idiomas, Idioma>
    {

        #region Business Methods

        public Idioma NewItem()
        {
            this.AddItem(Idioma.NewChild());
            return this[Count - 1];
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

        private Idiomas() { }

        public static Idiomas NewList() { return new Idiomas(); }

        public static Idiomas GetList()
        {
            CriteriaEx criteria = Idioma.GetCriteria(Idioma.OpenSession());

            Idioma.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<Idiomas>(criteria);
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
                /*if (nHMng.UseDirectSQL)
                {
					Idioma.DoLOCK( Session());

                    IDataReader reader = Idiomas.DoSELECT(AppContext.ActiveSchema.Code, Session());

                    while (reader.Read())
                    {
                        this.AddItem(Idioma.GetChild(reader));
                    }
                }*/
            }
            catch (NHibernate.ADOException)
            {
                if (Transaction() != null) Transaction().Rollback();
                throw new iQLockException(moleQule.Library.Resources.Messages.LOCK_ERROR);
            }
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
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
            foreach (Idioma obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // AddItem/update any current child objects
                foreach (Idioma obj in this)
                {
                    if (!Contains(obj))
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

    }
}