using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Common
{
    /// <summary>
    /// Editable Child Collection
    /// </summary>
    [Serializable()]
    public class BankAccounts : BusinessListBaseEx<BankAccounts, BankAccount>
    {
        #region Business Methods

        public BankAccount NewItem()
        {
            this.NewItem(BankAccount.NewChild());
            return this[Count - 1];
        }
		public BankAccount NewChildItem(BankAccount parent)
		{
			this.NewItem(BankAccount.NewChild(parent));
			return this[Count - 1];
		}
        public BankAccount NewChildItem(BankAccount parent, ETipoCuenta tipo)
        {
            this.NewItem(BankAccount.NewChild(parent, tipo));
            return this[Count - 1];
        }

        public bool ExistOtherItem(BankAccount CuentaBancaria)
        {
            foreach (BankAccount obj in this)
                if ((obj.Oid != CuentaBancaria.Oid) && (obj.Valor == CuentaBancaria.Valor))
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

		#region Root Factory Methods

		private BankAccounts() {}

        public static BankAccounts NewList() { return new BankAccounts(); }

		public static BankAccounts GetList()
		{
			CriteriaEx criteria = BankAccount.GetCriteria(BankAccount.OpenSession());
		
			criteria.Query = SELECT(new QueryConditions());

			BankAccount.BeginTransaction(criteria.SessionCode);

			return DataPortal.Fetch<BankAccounts>(criteria);
		}

		public static BankAccountList GetAsociadasList(long oid, EEstado estado, bool childs)
		{
			CriteriaEx criteria = BankAccount.GetCriteria(BankAccount.OpenSession());
			criteria.Childs = childs;

			BankAccount cuenta = BankAccount.New();
			cuenta.Oid = 0;
			cuenta.OidCuentaAsociada = oid;

			QueryConditions conditions = new QueryConditions
			{
				BankAccount = cuenta.GetInfo(false),
				Estado = estado
			};

			criteria.Query = SELECT_ASOCIADAS(conditions);

			BankAccountList list = DataPortal.Fetch<BankAccountList>(criteria);
			return list;
		}

        #endregion

		#region Child Factory Methods

		private BankAccounts(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
            Childs = childs;
            Fetch(reader);
        }

		public static BankAccounts NewChildList() { return new BankAccounts(); }

		public static BankAccounts GetChildList(int sessionCode, IDataReader reader) { return GetChildList(sessionCode, reader, true); }
		public static BankAccounts GetChildList(int sessionCode, IDataReader reader, bool childs) { return new BankAccounts(sessionCode, reader, childs); }

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
                    BankAccount.DoLOCK(Session());

                    IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    while (reader.Read())
                        this.AddItem(BankAccount.GetChild(SessionCode, reader, Childs));
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
            foreach (BankAccount obj in DeletedList)
                obj.DeleteSelf(this);

            // now that they are deleted, remove them from memory too
            DeletedList.Clear();

            try
            {
                // add/update any current child objects
                foreach (BankAccount obj in this)
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

		#region Child Data Access

		private void Fetch(IDataReader reader)
		{
			this.RaiseListChangedEvents = false;

			while (reader.Read())
				this.AddItem(BankAccount.GetChild(SessionCode, reader, Childs));

			this.RaiseListChangedEvents = true;
		}

		internal void Update(BankAccount parent)
		{
			this.RaiseListChangedEvents = false;

			// update (thus deleting) any deleted child objects
			foreach (BankAccount obj in DeletedList)
				obj.DeleteSelf(parent);

			// now that they are deleted, remove them from memory too
			DeletedList.Clear();

			// add/update any current child objects
			foreach (BankAccount obj in this)
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
		public static string SELECT(QueryConditions conditions) { return BankAccount.SELECT(conditions, true); }
		public static string SELECT(BankAccount cuenta, ETipoCuenta tipo = ETipoCuenta.Todas) 
		{
			BankAccount cuenta_asociada = BankAccount.New();
			cuenta_asociada.Oid = 0;
			cuenta_asociada.OidCuentaAsociada = cuenta.Oid;

			return BankAccount.SELECT(new QueryConditions { BankAccount = cuenta_asociada.GetInfo(false), TipoCuenta = tipo }, true); 
		}
		public static string SELECT_ASOCIADAS(QueryConditions conditions) { return BankAccount.SELECT_ASOCIADAS(conditions, true); }

        #endregion
    }
}