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
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
    public class CreditCardStatementInfo : ReadOnlyBaseEx<CreditCardStatementInfo>, ITransactionPayment
	{	
		#region Attributes

        protected CreditCardStatementBase _base = new CreditCardStatementBase();

		#endregion

        #region ITransactionPayment

        public Decimal Total { get { return _base.Record.Amount; } }
        public decimal Asignado { get { return _base.Allocated; } set { _base.Allocated = value; } }
        public decimal TotalPagado { get { return _base.Payed; } set { _base.Payed = value; } }
        public decimal Pendiente { get { return _base.Pending; } set { _base.Pending = value; } }
        public decimal PendienteAsignar { get { return _base.Deallocated; } set { _base.Deallocated = value; } }
        public decimal Acumulado { get { return _base.Aggregate; } set { _base.Aggregate = value; } }
        public string FechaAsignacion { get { return _base.AllocationDate; } set { _base.AllocationDate = value; } }
        public string Vinculado { get { return _base.Linked; } set { _base.Linked = value; } }
        public string NFactura { get { return string.Empty; } set { } }
        public long OidExpediente { get { return 0; } }
        public ETipoAcreedor ETipoAcreedor { get { return _base.ETipoAcreedor; } set { } }

        #endregion

		#region Properties

        public CreditCardStatementBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidCreditCard { get { return _base.Record.OidCreditCard; } }
        public DateTime From { get { return _base.Record.From; } }
        public DateTime Till { get { return _base.Record.Till; } }
        public DateTime DueDate { get { return _base.Record.DueDate; } }
        public decimal Amount { get { return _base.Record.Amount; } }
        public string Comments { get { return _base.Record.Comments; } }

        public string BankAccount { get { return _base.BankAccount; } set { _base.BankAccount = value; } }
        public string CreditCard { get { return _base.CreditCard; } set { _base.CreditCard = value; } }
        public EEstado EStatus { get { return _base.EStatus; } set { _base.EStatus = value; } }
        public string StatusLabel { get { return _base.StatusLabel; } }
        public decimal CashAmount { get { return _base.CashAmount; } }

		#endregion
		
		#region Business Methods

        public void CopyFrom(CreditCardStatement source) { _base.CopyValues(source); }

        public void Vincula()
        {
            Vinculado = Resources.Labels.RESET_PAGO;
            Asignado = Total;
            TotalPagado = Total;
            PendienteAsignar = 0;
        }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected CreditCardStatementInfo() { /* require use of factory methods */ }
        private CreditCardStatementInfo(int sessionCode, IDataReader reader, bool childs)
        {
            Childs = childs;
			SessionCode = sessionCode;
            Fetch(reader);
        }
        internal CreditCardStatementInfo(CreditCardStatement item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				
			}
		}

        public static CreditCardStatementInfo New(long oid = 0) { return new CreditCardStatementInfo() { Oid = oid }; }

 		#endregion

        #region Child Factory Methods

        public static CreditCardStatementInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
            return new CreditCardStatementInfo(sessionCode, reader, childs);
        }

        #endregion

		#region Root Factory Methods
		
        public static CreditCardStatementInfo Get(long oid, bool childs = false)
		{
            CriteriaEx criteria = CreditCardStatement.GetCriteria(CreditCardStatement.OpenSession());
            criteria.Childs = childs;
            criteria.Query = SELECT(oid);

            CreditCardStatementInfo obj = DataPortal.Fetch<CreditCardStatementInfo>(criteria);
            CreditCardStatement.CloseSession(criteria.SessionCode);
			return obj;
		}
		
		#endregion

        #region Common Data Access

        //called to copy data from IDataReader
        private void Fetch(IDataReader source)
        {
            try
            {
                _base.CopyValues(source);
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
        }

        #endregion

        #region Root Data Access
		 
		private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;
			
			try
			{
				if (nHMng.UseDirectSQL)
				{
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());
		
					if (reader.Read())
						_base.CopyValues(reader);					
				}
			}
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex, new object[] { criteria.Query });
            }
		}
		
		#endregion

        #region SQL

        public static string SELECT(long oid) { return CreditCardStatement.SELECT(oid, false); }
        public static string SELECT(QueryConditions conditions) { return CreditCardStatement.SELECT(conditions, false); }

        #endregion		
	}
}