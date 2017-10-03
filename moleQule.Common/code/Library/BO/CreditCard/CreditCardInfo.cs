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
    public class CreditCardInfo : ReadOnlyBaseEx<CreditCardInfo>
	{	
		#region Attributes

        public CreditCardBase _base = new CreditCardBase();

        protected CreditCardStatementList _statements = null;

		#endregion
		
		#region Properties

        public CreditCardBase Base { get { return _base; } }

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
        public long OidCuentaBancaria { get { return _base.Record.OidCuentaBancaria; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public long Tipo { get { return _base.Record.Tipo; } }
        public string Numeracion { get { return _base.Record.Numeracion; } }
		public string CuentaContable { get { return _base.Record.CuentaContable; } }
		public long FormaPago { get { return _base.Record.FormaPago; } }
		public long DiasPago { get { return _base.Record.DiasPago; } }
		public long DiaExtracto { get { return _base.Record.DiaExtracto; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
        public decimal PComision { get { return _base.Record.PComision; } }

        public CreditCardStatementList Statements { get { return _statements; } }

        //LINKED
        public string CuentaBancaria { get { return _base.CuentaBancaria; } }
		public ETipoTarjeta ETipoTarjeta { get { return (ETipoTarjeta)Tipo; } }
		public string TipoTarjetaLabel { get { return EnumText<ETipoTarjeta>.GetLabel(ETipoTarjeta); } }
		public EFormaPago EFormaPago { get { return (EFormaPago)FormaPago; } }
		public string FormaPagoLabel { get { return Common.EnumText<EFormaPago>.GetLabel(EFormaPago); ; } }

		#endregion
		
		#region Business Methods
								
		public void CopyFrom(CreditCard source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected CreditCardInfo() { /* require use of factory methods */ }
		private CreditCardInfo(IDataReader reader, bool childs)
		{
			Childs = childs;
			Fetch(reader);
		}
		internal CreditCardInfo(CreditCard item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
                _statements = (item.Statements != null) ? CreditCardStatementList.GetChildList(item.Statements) : null;
			}
		}
	
		public static CreditCardInfo GetChild(IDataReader reader, bool childs = true)
        {
			return new CreditCardInfo(reader, childs);
		}

        public virtual void LoadChilds(Type type, bool childs)
        {
            if (type.Equals(typeof(CreditCardStatement)))
            {
                _statements = CreditCardStatementList.GetChildList(this, childs);
            }
        }

        public static CreditCardInfo New(long oid = 0) { return new CreditCardInfo() { Oid = oid }; }

 		#endregion
		
		#region Root Factory Methods
		
		public static CreditCardInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = CreditCard.GetCriteria(CreditCard.OpenSession());
            criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = CreditCardInfo.SELECT(oid);
	
			CreditCardInfo obj = DataPortal.Fetch<CreditCardInfo>(criteria);
			CreditCard.CloseSession(criteria.SessionCode);
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

                if (Childs)
                {
                    string query = string.Empty;
                    IDataReader reader;

                    query = CreditCardStatementList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _statements = CreditCardStatementList.GetChildList(SessionCode, reader);
                }
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

                    if (Childs)
                    {
                        string query = string.Empty;

                        query = CreditCardStatementList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _statements = CreditCardStatementList.GetChildList(SessionCode, reader);
                    }
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

        public static string SELECT(long oid) { return CreditCard.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return CreditCard.SELECT(conditions, false); }

        #endregion		
	}
}