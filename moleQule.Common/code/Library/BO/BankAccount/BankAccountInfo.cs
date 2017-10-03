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
using moleQule.Library.Hipatia;

namespace moleQule.Library.Common
{
	/// <summary>
	/// ReadOnly Child Business Object
    /// </summary>
	[Serializable()]
	public class BankAccountInfo : ReadOnlyBaseEx<BankAccountInfo>, IAgenteHipatia
	{
		#region IAgenteHipatia

		public string NombreHipatia { get { return Valor; } }
		public string IDHipatia { get { return Valor.Substring(Valor.Length - 4); } }
		public Type TipoEntidad { get { return typeof(BankAccount); } }
		public string ObservacionesHipatia { get { return Observaciones; } }

		#endregion

	    #region Attributes

        public BankAccountBase _base = new BankAccountBase();
		
		protected BankAccountList _cuentas_asociadas = null;
        protected BankAccountList _fondos_inversion = null;

        #endregion

        #region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidCuentaAsociada { get { return _base.Record.OidAsociada; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string Entidad { get { return _base.Record.Entidad; } }		
        public string Valor { get { return _base.Record.Valor; } }
		public string Swift { get { return _base.Record.Swift; } }
		public long TipoCuenta { get { return _base.Record.Tipo; } }
		public string CuentaContable { get { return _base.Record.CuentaContable; } }
        public string CuentaContableGastos { get { return _base.Record.CuentaContableGastos; } }
		public Decimal SaldoInicial { get { return _base.Record.SaldoInicial; } }
		public string Observaciones	{ get { return _base.Record.Observaciones; } }
        public DateTime FechaFirma { get { return _base.Record.FechaFirma; } }
        public DateTime DuracionPoliza { get { return _base.Record.DuracionPoliza; } }
        public Decimal Comision { get { return _base.Record.Comision; } }
        public Decimal TipoInteres { get { return _base.Record.TipoInteres; } }
        public bool PagoGastosInicio { get { return _base.Record.PagoGastosInicio; } }
        public long DiasCredito { get { return _base.Record.DiasCredito; } }

		public virtual BankAccountList CuentasAsociadas { get { return _cuentas_asociadas; } }
        public virtual BankAccountList FondosInversion { get { return _fondos_inversion; } }

		//NO ENLAZADAS
		public EEstado EEstado { get { return (EEstado)_base.EEstado; } }
		public virtual string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
		public ETipoCuenta ETipoCuenta { get { return (ETipoCuenta)_base.Record.Tipo; } }
		public string TipoCuentaLabel { get { return Library.Common.EnumText<ETipoCuenta>.GetLabel(ETipoCuenta); } }
		public Decimal Saldo { get { return _base.Saldo; } set { _base.Saldo = value; } }
		public Decimal SaldoDispuesto { get { return (_base.Record.Tipo == (long)ETipoCuenta.CuentaCorriente) ? 0 : _base.Record.SaldoInicial - _base.Saldo; } }
        public Decimal SaldoDisponible { get { return _base.SaldoDisponible; } }
        public string CuentaAsociada { get { return _base.CuentaAsociada ; } }

		public decimal SaldoParcial { get; set; }

        #endregion

        #region Business Methods
        
		public void CopyFrom(BankAccount source) { _base.CopyValues(source); }

		#endregion		 

		#region Common Factory Methods

		protected BankAccountInfo() { /* require use of factory methods */ }
		private BankAccountInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal BankAccountInfo(BankAccount source, bool childs)
		{
			_base.CopyValues(source);

			if (childs)
			{
				_cuentas_asociadas = (source.CuentasAsociadas != null) ? BankAccountList.GetChildList(source.CuentasAsociadas) : null;
                _fondos_inversion = (source.FondosInversion != null) ? BankAccountList.GetChildList(source.FondosInversion) : null;
			}
		}

        public static BankAccountInfo New(long oid = 0) { return new BankAccountInfo() { Oid = oid }; }

		#endregion

		#region Root Factory Methods

		/// <summary>
		/// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
		/// </summary>
		/// <param name="oid">Oid del objeto</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static BankAccountInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = BankAccount.GetCriteria(BankAccount.OpenSession());
			criteria.Childs = childs;

			criteria.Query = BankAccountInfo.SELECT(oid);

			BankAccountInfo obj = DataPortal.Fetch<BankAccountInfo>(criteria);
			BankAccount.CloseSession(criteria.SessionCode);

			return obj;
		}
		public static BankAccountInfo Get(long oid, bool childs, bool cache)
		{
			BankAccountInfo item;

			//No está en la cache de listas
			if (!Cache.Instance.Contains(typeof(BankAccountList)))
			{
                BankAccountList items = BankAccountList.NewList();

                item = Get(oid, childs);
                items.AddItem(item);
                Cache.Instance.Save(typeof(BankAccountList), items);
			}
			else
			{
				BankAccountList items = Cache.Instance.Get(typeof(BankAccountList)) as BankAccountList;
				item = items.GetItem(oid);

				//No está en la lista de la cache de listas
				if (item == null)
				{
					item = Get(oid, childs);
					items.AddItem(item);
					Cache.Instance.Save(typeof(BankAccountList), items);
				}
			}

			return item;
		}

		public virtual void LoadChilds(Type type, bool childs)
		{
			/*if (type.Equals(typeof(ProductoCliente)))
			{
				_producto_clientes = ProductoClienteList.GetChildList(this, get_childs);
			}*/
		}

		#endregion

		#region Child Factory Methods

		public static BankAccountInfo GetChild(int sessionCode, IDataReader reader, bool childs)
		{
			return new BankAccountInfo(sessionCode, reader, childs);
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

						query = BankAccountList.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
                        _cuentas_asociadas = BankAccountList.GetChildList(SessionCode, reader, false);

                        query = BankAccountList.SELECT(this, ETipoCuenta.FondoInversion);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _fondos_inversion = BankAccountList.GetChildList(SessionCode, reader, false);
					}
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

		#region Child Data Access
		 
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

					query = BankAccountList.SELECT(this);
					reader = nHMng.SQLNativeSelect(query, Session());
                    _cuentas_asociadas = BankAccountList.GetChildList(SessionCode, reader, false);

                    query = BankAccountList.SELECT(this, ETipoCuenta.FondoInversion);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _fondos_inversion = BankAccountList.GetChildList(SessionCode, reader, false);
				}
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
					
		#endregion

		#region SQL

		public static string SELECT(long oid) { return BankAccount.SELECT(oid, false); }

		#endregion		
	}
}