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
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class BankAccountList : ReadOnlyListBaseEx<BankAccountList, BankAccountInfo>
    {
        #region Business Methods

		public decimal TotalSaldo()
		{
			decimal total = 0;
			foreach (BankAccountInfo item in this)
			{
				if (item.EEstado == EEstado.Baja) continue;

				switch (item.ETipoCuenta)
				{
					case ETipoCuenta.CuentaCorriente:
						total += item.Saldo;
						break;

					case ETipoCuenta.ComercioExterior:
					case ETipoCuenta.LineaCredito:
						total -= item.SaldoDispuesto;
						break;
				}
			}

			return total;
		}

        #endregion

        #region Root Factory Methods

        private BankAccountList() { }		
		private BankAccountList(IList<BankAccount> lista)
		{
            Fetch(lista);
        }

		public static BankAccountList NewList() { return new BankAccountList(); }

		public static BankAccountList GetList() { return BankAccountList.GetList(true); }
		public static BankAccountList GetList(bool childs)
		{
			CriteriaEx criteria = BankAccount.GetCriteria(BankAccount.OpenSession());
            criteria.Childs = childs;			
			
            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = BankAccounts.SELECT();	

			BankAccountList list = DataPortal.Fetch<BankAccountList>(criteria);

            CloseSession(criteria.SessionCode);
			return list;
		}
		public static BankAccountList GetList(DateTime f_fin, bool childs)
		{
			CriteriaEx criteria = BankAccount.GetCriteria(BankAccount.OpenSession());
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				FechaFin = f_fin
			};

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = BankAccounts.SELECT(conditions);

			BankAccountList list = DataPortal.Fetch<BankAccountList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
		public static BankAccountList GetList(EEstado estado, DateTime f_fin, bool childs)
		{
			CriteriaEx criteria = BankAccount.GetCriteria(BankAccount.OpenSession());
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				Estado = estado,
				FechaFin = f_fin
			};

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = BankAccounts.SELECT(conditions);

			BankAccountList list = DataPortal.Fetch<BankAccountList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
		public static BankAccountList GetList(EEstado estado, bool childs)
		{
			CriteriaEx criteria = BankAccount.GetCriteria(BankAccount.OpenSession());
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				Estado = estado
			};

			criteria.Query = SELECT(conditions);

			BankAccountList list = DataPortal.Fetch<BankAccountList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}
		public static BankAccountList GetList(ETipoCuenta tipo, EEstado estado, bool childs)
		{
			CriteriaEx criteria = BankAccount.GetCriteria(BankAccount.OpenSession());
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				TipoCuenta = tipo,
				Estado = estado
			};

			criteria.Query = SELECT(conditions);

			BankAccountList list = DataPortal.Fetch<BankAccountList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

		public static BankAccountList GetAsociadasList(EEstado estado, bool childs)
		{
			return GetAsociadasList(0, estado, childs);
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

			CloseSession(criteria.SessionCode);
			return list;
		}

		public static BankAccountList GetNoAsociadasList(EEstado estado, bool childs)
		{
			CriteriaEx criteria = BankAccount.GetCriteria(BankAccount.OpenSession());
			criteria.Childs = childs;

			QueryConditions conditions = new QueryConditions
			{
				Estado = estado
			};

			criteria.Query = SELECT_NO_ASOCIADAS(conditions);

			BankAccountList list = DataPortal.Fetch<BankAccountList>(criteria);

			CloseSession(criteria.SessionCode);
			return list;
		}

        public static BankAccountList GetList(CriteriaEx criteria)
        {
            return BankAccountList.RetrieveList(typeof(BankAccount), AppContext.CommonSchema, criteria);
        }
        public static BankAccountList GetList(IList<BankAccountInfo> list)
        {
            BankAccountList flist = new BankAccountList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (BankAccountInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static BankAccountList GetList(IList<BankAccount> list) { return new BankAccountList(list); }
		
		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<BankAccountInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<BankAccountInfo> sortedList = new SortedBindingList<BankAccountInfo>(GetList());
			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}

		#endregion

		#region Child Factory Methods

		private BankAccountList(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}

		public static BankAccountList GetChildList(IList<BankAccountInfo> list)
		{
			BankAccountList flist = new BankAccountList();

			if (list.Count > 0)
			{
				flist.IsReadOnly = false;

				foreach (BankAccountInfo item in list)
					flist.AddItem(item);

				flist.IsReadOnly = true;
			}

			return flist;
		}
		public static BankAccountList GetChildList(IList<BankAccount> list)
		{
			BankAccountList flist = new BankAccountList();

			if (list != null)
			{
				int sessionCode = BankAccount.OpenSession();

				flist.IsReadOnly = false;

				foreach (BankAccount item in list)
				{
					flist.AddItem(item.GetInfo());
				}

				flist.IsReadOnly = true;

				BankAccount.CloseSession(sessionCode);
			}

			return flist;
		}
		public static BankAccountList GetChildList(int sessionCode, IDataReader reader, bool childs) { return new BankAccountList(sessionCode, reader, childs); }
		public static BankAccountList GetChildList(BankAccountInfo parent, bool childs)
		{
			CriteriaEx criteria = BankAccount.GetCriteria(BankAccount.OpenSession());

			criteria.Query = BankAccountList.SELECT(parent);
			criteria.Childs = childs;

			BankAccountList list = DataPortal.Fetch<BankAccountList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}

		#endregion

		#region Data Access
		
		// called to copy objects data from list
        private void Fetch(IList<BankAccount> lista)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            foreach (BankAccount item in lista)
                this.AddItem(item.GetInfo());

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }

        // called to copy objects data from list
        private void Fetch(IDataReader reader)
        {
            this.RaiseListChangedEvents = false;

            IsReadOnly = false;

            while (reader.Read())
				this.AddItem(BankAccountInfo.GetChild(SessionCode, reader, Childs));

            IsReadOnly = true;

            this.RaiseListChangedEvents = true;
        }
		
		// called to retrieve data from db
		protected override void Fetch(CriteriaEx criteria)
		{
			this.RaiseListChangedEvents = false;

			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHManager.Instance.SQLNativeSelect(criteria.Query, Session());

					IsReadOnly = false;

					while (reader.Read())
					{
						this.AddItem(BankAccountInfo.GetChild(SessionCode, reader, Childs));
					}

					IsReadOnly = true;
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
		public static string SELECT(QueryConditions conditions) { return BankAccount.SELECT(conditions, false); }
		public static string SELECT(BankAccountInfo cuenta, ETipoCuenta tipo = ETipoCuenta.Todas) 
		{
			BankAccount cuenta_asociada = BankAccount.New();
			cuenta_asociada.Oid = 0;
			cuenta_asociada.OidCuentaAsociada = cuenta.Oid;

			return BankAccount.SELECT(new QueryConditions { BankAccount = cuenta_asociada.GetInfo(false), TipoCuenta = tipo }, true); 
		}
		public static string SELECT_ASOCIADAS(QueryConditions conditions) { return BankAccount.SELECT_ASOCIADAS(conditions, false); }
		public static string SELECT_NO_ASOCIADAS(QueryConditions conditions) { return BankAccount.SELECT_NO_ASOCIADAS(conditions, false); }

        #endregion
    }
}