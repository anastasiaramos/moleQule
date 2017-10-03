using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class Municipios : BusinessListBaseEx<Municipios, Municipio>
    {
        #region Business Methods

		public Municipio NewItem()
		{
			this.NewItem(Municipio.NewChild());
			return this[Count - 1];
		}

        public bool ExistOtherItem(Municipio municipio)
        {
            foreach (Municipio obj in this)
                if ((obj.Localidad == municipio.Localidad) && (obj.Nombre == municipio.Nombre))
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

        private Municipios() { }

        public static Municipios NewList() { return new Municipios(); }

		public static Municipios GetList()
		{
            CriteriaEx criteria = Municipio.GetCriteria(Municipio.OpenSession());

            Municipio.BeginTransaction(criteria.SessionCode);

            //No criteria. Retrieve all de List
            return DataPortal.Fetch<Municipios>(criteria);
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
					Municipio.DoLOCK(Session());

					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					while (reader.Read())
						this.AddItem(Municipio.GetChild(SessionCode, reader, Childs));
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
            foreach (Municipio obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (Municipio obj in this)
                {
                    if (!ExistOtherItem(obj))
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

		#region SQL

		internal static string SELECT() { return SELECT(new QueryConditions()); }
		internal static string SELECT(QueryConditions conditions)
		{
			OrderList orders = new OrderList();
			orders.NewOrder("Valor", ListSortDirection.Ascending, typeof(Municipio));
			conditions.Orders = orders;
			return Municipio.SELECT(conditions, true);
		}

		#endregion
    }
}