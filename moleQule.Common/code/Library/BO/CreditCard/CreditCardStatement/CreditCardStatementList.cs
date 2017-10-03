using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using Csla;
using NHibernate;
using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Common
{
	/// <summary>
	/// Read Only Child Collection of Business Objects
	/// </summary>
    [Serializable()]
	public class CreditCardStatementList : ReadOnlyListBaseEx<CreditCardStatementList, CreditCardStatementInfo>
	{
		#region Business Methods

        public CreditCardStatementInfo GetByFromItem(DateTime from)
        {
            return Items.FirstOrDefault(x => x.From == from);
        }
        public CreditCardStatementInfo GetByDueDateItem(DateTime dueDate)
        {
            return Items.FirstOrDefault(x => x.DueDate == dueDate);
        }
       
        public CreditCardStatementList GetByYearList(int year)
        {
            return GetList(new List<CreditCardStatementInfo>(Items.Where(x => x.DueDate.Year == year)));
        }

        public decimal Total() { return Items.Sum(x => x.Amount); }

        public void UpdatePaymentValues(long oidPayment)
        {
            CreditCardStatementInfo item;
            decimal acumulado;

            for (int i = 0; i < Items.Count; i++)
            {
                item = Items[i];

                /*if (item.OidPago != pago.Oid)
                    item.Asignado = 0;*/

                if (i == 0) acumulado = 0;
                else acumulado = Items[i - 1].Acumulado;

                item.Acumulado = acumulado + item.Pendiente;
                item.Vinculado = (item.Asignado == 0) ? Resources.Labels.SET_PAGO : Resources.Labels.RESET_PAGO;
            }
        }

		#endregion

		#region Common Factory Methods

		/// <summary>
        /// Constructores
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Objet creation require use of factory methods
        /// </remarks>
		private CreditCardStatementList() {}
		private CreditCardStatementList(IList<CreditCardStatement> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }
        private CreditCardStatementList(IList<CreditCardStatementInfo> list, bool childs)
        {
			Childs = childs;
            Fetch(list);
        }

        public static CreditCardStatementList GetChildList(IList<CreditCardStatementInfo> list)
        {
            CreditCardStatementList flist = new CreditCardStatementList();

            if (list.Count > 0)
            {
                flist.IsReadOnly = false;

                foreach (CreditCardStatementInfo item in list)
                    flist.AddItem(item);

                flist.IsReadOnly = true;
            }

            return flist;
        }
        public static CreditCardStatementList GetChildList(IList<CreditCardStatement> list) { return new CreditCardStatementList(list, false); }
        
        #endregion

		#region Root Factory Methods

		public static CreditCardStatementList NewList() { return new CreditCardStatementList(); }

        public static CreditCardStatementList GetList() {  return CreditCardStatementList.GetList(true); }
		public static CreditCardStatementList GetList(bool childs) { return CreditCardStatementList.GetList(DateTime.MinValue, DateTime.MaxValue, childs); }
		public static CreditCardStatementList GetList(int year, bool childs)
		{
			return GetList(DateAndTime.FirstDay(year), DateAndTime.LastDay(year), childs);
		}
		public static CreditCardStatementList GetList(DateTime from, DateTime till, bool childs)
		{
			QueryConditions conditions = new QueryConditions
			{
				FechaIni = from,
				FechaFin = till,
			};

			return GetList(conditions, childs);
		}
		public static CreditCardStatementList GetList(QueryConditions conditions, bool childs)
		{
			return GetList(CreditCardStatementList.SELECT(conditions), childs);
		}
        public static CreditCardStatementList GetList(string query, bool childs)
        {
            CriteriaEx criteria = CreditCardStatement.GetCriteria(CreditCardStatement.OpenSession());
            criteria.Childs = childs;

            if (nHManager.Instance.UseDirectSQL)
                criteria.Query = query;

            CreditCardStatementList list = DataPortal.Fetch<CreditCardStatementList>(criteria);
            CloseSession(criteria.SessionCode);
            return list;
        }

        public static CreditCardStatementList GetUnpaidList(long oidCreditCard, bool childs)
        {
            return GetUnpaidList(oidCreditCard, DateTime.MinValue, DateTime.MaxValue, childs);
        }
        public static CreditCardStatementList GetUnpaidList(long oidCreditCard, DateTime from, DateTime till, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                OidEntity = -1,
                EntityType = ETipoEntidad.Pago,
                CreditCard = (oidCreditCard != 0) ? CreditCardInfo.New(oidCreditCard) : null,
                FechaAuxIni = from,
                FechaAuxFin = till,
            };
            
            return GetList(CreditCardStatement.SELECT_UNPAID(conditions, false), childs);
        }

        public static CreditCardStatementList GetByPaymentList(long oidPayment, long oidCreditCard, bool childs)
        {
            QueryConditions conditions = new QueryConditions
            {
                OidEntity = oidPayment,
                EntityType = ETipoEntidad.Pago,
                CreditCard = (oidCreditCard != 0) ? CreditCardInfo.New(oidCreditCard) : null,
            };

            return GetList(CreditCardStatement.SELECT_BY_PAYMENT(conditions, false), childs);
        }

        public static CreditCardStatementList GetByPaymentAndUnpaidList(long oidPayment, long oidCreditCard, bool childs)
        {
            return GetByPaymentAndUnpaidList(oidPayment, oidCreditCard, DateTime.MinValue, DateTime.MaxValue, childs);
        }
        public static CreditCardStatementList GetByPaymentAndUnpaidList(long oidPayment, long oidCreditCard, DateTime from, DateTime till, bool childs)
        {
            CreditCardStatementList by_payment = GetByPaymentList(oidPayment, oidCreditCard, childs);
            CreditCardStatementList unpaids = GetUnpaidList(oidCreditCard, childs);

            CreditCardStatementList list = new CreditCardStatementList();
            list.IsReadOnly = false;

            foreach (CreditCardStatementInfo item in by_payment)
                list.AddItem(item);

            foreach (CreditCardStatementInfo item in unpaids)
                if (list.GetItem(item.Oid) == null) list.AddItem(item);

            list.IsReadOnly = true;

            return list;
        }

		/// <summary>
		/// Construye la lista
		/// </summary>
		/// <param name="list">IList origen</param>
		/// <returns>Lista de objetos de solo lectura</returns>
		/// <remarks>NO OBTIENE LOS HIJOS SI EL OBJETO NO LOS TIENE CARGADOS</remarks>
		public static CreditCardStatementList GetList(IList<CreditCardStatement> list) { return new CreditCardStatementList(list, false); }
		public static CreditCardStatementList GetList(IList<CreditCardStatementInfo> list) { return new CreditCardStatementList(list, false); }

		/// <summary>
		/// Devuelve una lista ordenada de todos los elementos
		/// </summary>
		/// <param name="sortProperty">Campo de ordenación</param>
		/// <param name="sortDirection">Sentido de ordenación</param>
		/// <returns>Lista ordenada de elementos</returns>
		public static SortedBindingList<CreditCardStatementInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection)
		{
			SortedBindingList<CreditCardStatementInfo> sortedList = new SortedBindingList<CreditCardStatementInfo>(GetList());

			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}
		public static SortedBindingList<CreditCardStatementInfo> GetSortedList(string sortProperty, ListSortDirection sortDirection, bool childs)
		{
			SortedBindingList<CreditCardStatementInfo> sortedList = new SortedBindingList<CreditCardStatementInfo>(GetList(childs));

			sortedList.ApplySort(sortProperty, sortDirection);
			return sortedList;
		}

		#endregion

		#region Child Factory Methods

        private CreditCardStatementList(int sessionCode, IDataReader reader, bool childs)
        {
			SessionCode = sessionCode;
			Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }

        public static CreditCardStatementList GetChildList(int sessionCode, IDataReader reader, bool childs = false) { return new CreditCardStatementList(sessionCode, reader, childs); }
		public static CreditCardStatementList GetChildList(CreditCardInfo parent, bool childs)
		{
			CriteriaEx criteria = CreditCardStatement.GetCriteria(CreditCardStatement.OpenSession());

			criteria.Query = CreditCardStatementList.SELECT(parent);
			criteria.Childs = childs;

			CreditCardStatementList list = DataPortal.Fetch<CreditCardStatementList>(criteria);
			CloseSession(criteria.SessionCode);

			return list;
		}
		
		#endregion

		#region Common Data Access

		// called to copy objects data from list
		private void Fetch(IList<CreditCardStatement> lista)
		{
			this.RaiseListChangedEvents = false;

			IsReadOnly = false;

			foreach (CreditCardStatement item in lista)
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
				this.AddItem(CreditCardStatementInfo.GetChild(SessionCode, reader, Childs));

			IsReadOnly = true;

			this.RaiseListChangedEvents = true;
		}

		#endregion

		#region Root Data Access

		/// <summary>
		/// Construye el objeto y se encarga de obtener los
		/// hijos si los tiene y se solicitan
		/// </summary>
		/// <param name="criteria">Criterios de la consulta</param>
		protected override void Fetch(CriteriaEx criteria)
		{
			this.RaiseListChangedEvents = false;

			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					IsReadOnly = false;

					while (reader.Read())
						this.AddItem(CreditCardStatementInfo.GetChild(SessionCode, reader, Childs));

					IsReadOnly = true;
				}
			}
			catch (Exception ex)
			{
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
			}

			this.RaiseListChangedEvents = true;
		}

		#endregion

        #region SQL

		public static string SELECT() { return SELECT(new QueryConditions()); }
        public static string SELECT(QueryConditions conditions) { return CreditCardStatement.SELECT(conditions, false); }
		public static string SELECT(CreditCardInfo source) { return CreditCardStatement.SELECT(new QueryConditions { CreditCard = source }, false); }
		
        #endregion
	}
}