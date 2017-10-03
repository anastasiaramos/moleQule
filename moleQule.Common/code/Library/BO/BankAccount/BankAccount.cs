using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Remoting;

using Csla;
using Csla.Validation;
using NHibernate;
using moleQule.Library;
using moleQule.Library.CslaEx; 

namespace moleQule.Library.Common
{
	[Serializable()]
	public class BankAccountRecord : RecordBase
	{
		#region Attributes

		private long _oid_asociada;
		private long _estado;
		private long _tipo;
		private string _entidad = string.Empty;
		private string _valor = string.Empty;
		private string _swift = string.Empty;
		private string _cuenta_contable = string.Empty;
		private string _cuenta_contable_gastos = string.Empty;
		private Decimal _saldo_inicial;
		private string _observaciones = string.Empty;
		private DateTime _fecha_firma;
		private DateTime _duracion_poliza;
		private Decimal _comision;
		private Decimal _tipo_interes;
		private bool _pago_gastos_inicio = false;
		private long _dias_credito;
  
		#endregion
		
		#region Properties

		public virtual long OidAsociada { get { return _oid_asociada; } set { _oid_asociada = value; } }
		public virtual long Estado { get { return _estado; } set { _estado = value; } }
		public virtual long Tipo { get { return _tipo; } set { _tipo = value; } }
		public virtual string Entidad { get { return _entidad; } set { _entidad = value; } }
		public virtual string Valor { get { return _valor; } set { _valor = value; } }
		public virtual string Swift { get { return _swift; } set { _swift = value; } }
		public virtual string CuentaContable { get { return _cuenta_contable; } set { _cuenta_contable = value; } }
		public virtual string CuentaContableGastos { get { return _cuenta_contable_gastos; } set { _cuenta_contable_gastos = value; } }
		public virtual Decimal SaldoInicial { get { return _saldo_inicial; } set { _saldo_inicial = value; } }
		public virtual string Observaciones { get { return _observaciones; } set { _observaciones = value; } }
		public virtual DateTime FechaFirma { get { return _fecha_firma; } set { _fecha_firma = value; } }
		public virtual DateTime DuracionPoliza { get { return _duracion_poliza; } set { _duracion_poliza = value; } }
		public virtual Decimal Comision { get { return _comision; } set { _comision = value; } }
		public virtual Decimal TipoInteres { get { return _tipo_interes; } set { _tipo_interes = value; } }
		public virtual bool PagoGastosInicio { get { return _pago_gastos_inicio; } set { _pago_gastos_inicio = value; } }
		public virtual long DiasCredito { get { return _dias_credito; } set { _dias_credito = value; } }
		
		#endregion
		
		#region Business Methods
		
		public BankAccountRecord(){}
		
		public virtual void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_oid_asociada = Format.DataReader.GetInt64(source, "OID_ASOCIADA");
			_estado = Format.DataReader.GetInt64(source, "ESTADO");
			_tipo = Format.DataReader.GetInt64(source, "TIPO");
			_entidad = Format.DataReader.GetString(source, "ENTIDAD");
			_valor = Format.DataReader.GetString(source, "VALOR");
			_swift = Format.DataReader.GetString(source, "SWIFT");
			_cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");
			_cuenta_contable_gastos = Format.DataReader.GetString(source, "CUENTA_CONTABLE_GASTOS");
			_saldo_inicial = Format.DataReader.GetDecimal(source, "SALDO_INICIAL");
			_observaciones = Format.DataReader.GetString(source, "OBSERVACIONES");
			_fecha_firma = Format.DataReader.GetDateTime(source, "FECHA_FIRMA");
			_duracion_poliza = Format.DataReader.GetDateTime(source, "DURACION_POLIZA");
			_comision = Format.DataReader.GetDecimal(source, "COMISION");
			_tipo_interes = Format.DataReader.GetDecimal(source, "TIPO_INTERES");
			_pago_gastos_inicio = Format.DataReader.GetBool(source, "PAGO_GASTOS_INICIO");
            _dias_credito = Format.DataReader.GetInt64(source, "DIAS_CREDITO");
            _cuenta_contable = Format.DataReader.GetString(source, "CUENTA_CONTABLE");

		}
		
		public virtual void CopyValues(BankAccountRecord source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_oid_asociada = source.OidAsociada;
			_estado = source.Estado;
			_tipo = source.Tipo;
			_entidad = source.Entidad;
			_valor = source.Valor;
			_swift = source.Swift;
			_cuenta_contable = source.CuentaContable;
			_cuenta_contable_gastos = source.CuentaContableGastos;
			_saldo_inicial = source.SaldoInicial;
			_observaciones = source.Observaciones;
			_fecha_firma = source.FechaFirma;
			_duracion_poliza = source.DuracionPoliza;
			_comision = source.Comision;
			_tipo_interes = source.TipoInteres;
			_pago_gastos_inicio = source.PagoGastosInicio;
			_dias_credito = source.DiasCredito;
		}
		#endregion	
	}

    [Serializable()]
	public class BankAccountBase 
	{	 
		#region Attributes
		
		private BankAccountRecord _record = new BankAccountRecord();        

		private Decimal _saldo;
		private string _cuenta_asociada = string.Empty;
		
		public BankAccountRecord Record { get { return _record; } }
		
		#endregion
		
		#region Properties

		public EEstado EEstado { get { return (EEstado)_record.Estado; } }
        public string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
        public virtual ETipoCuenta ETipoCuenta { get { return (ETipoCuenta)_record.Tipo; } set { _record.Tipo = (long)value; } }
        public virtual string TipoCuentaLabel { get { return Library.Common.EnumText<ETipoCuenta>.GetLabel(ETipoCuenta); } }
        public virtual Decimal Saldo 
        { 
            get 
            {
                switch (ETipoCuenta)
                {
                    case ETipoCuenta.ComercioExterior:
                        return -_saldo;

                    default:
                        return _saldo;
                }
            } 

            set { _saldo = value; } 
        }
        public virtual Decimal SaldoDisponible 
        {
            get
            {
                switch (ETipoCuenta)
                {
                    case ETipoCuenta.LineaCredito:
                        return (Saldo >= _record.SaldoInicial) 
                            ? _record.SaldoInicial 
                            : (Saldo > 0) 
                                ? _record.SaldoInicial - Saldo
                                : _record.SaldoInicial + Saldo;

                    case ETipoCuenta.ComercioExterior:
                        return (Saldo >= _record.SaldoInicial) ? _record.SaldoInicial : _record.SaldoInicial + Saldo;

                    default:
                        return 0;
                }
            }
        }
        public virtual string CuentaAsociada { get { return _cuenta_asociada; } set { _cuenta_asociada = value; } }
		
		#endregion
		
		#region Business Methods
		
		internal void CopyValues(IDataReader source)
		{
            if (source == null) return;

            _saldo = _record.SaldoInicial + Format.DataReader.GetDecimal(source, "SALDO");
            _cuenta_asociada = Format.DataReader.GetString(source, "CUENTA_ASOCIADA");
			
			_record.CopyValues(source);
		}
		
		internal void CopyValues(BankAccount source)
		{
            if (source == null) return;

            _saldo = source.SaldoInicial + source.Saldo;
            _cuenta_asociada = source.CuentaAsociada;
			
			_record.CopyValues(source._base.Record);
		}
		internal void CopyValues(BankAccountInfo source)
		{
			if (source == null) return;
			
			_record.CopyValues(source._base.Record);
		}
		#endregion	
	}
		
	/// <summary>
	/// Editable Child Business Object
	/// </summary>
    [Serializable()]
	public class BankAccount : BusinessBaseEx<BankAccount>
	{	
	    #region Attributes
        
        public BankAccountBase _base = new BankAccountBase();

		private BankAccounts _cuentas_asociadas = BankAccounts.NewChildList();
        private BankAccounts _fondos_inversion = BankAccounts.NewChildList();

        #endregion

        #region Properties

		public BankAccountBase Base { get { return _base; } }

		public override long Oid
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Oid;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);
				_base.Record.Oid = value;
			}
		}
		public virtual long OidCuentaAsociada
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.Record.OidAsociada;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

				if (!_base.Record.OidAsociada.Equals(value))
				{
					_base.Record.OidAsociada = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long Estado
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.Record.Estado;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

				if (!_base.Record.Estado.Equals(value))
				{
					_base.Record.Estado = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Entidad
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.Record.Entidad;
			}
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (!_base.Record.Entidad.Equals(value))
				{
					_base.Record.Entidad = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual string Valor
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.Record.Valor;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (!_base.Record.Valor.Equals(value))
				{
					_base.Record.Valor = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string Swift
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				//CanReadProperty(true);
				return _base.Record.Swift;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				//CanWriteProperty(true);

				if (value == null) value = string.Empty;

				if (!_base.Record.Swift.Equals(value))
				{
					_base.Record.Swift = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual long TipoCuenta
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.Record.Tipo;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);
				if (!_base.Record.Tipo.Equals(value))
				{
					_base.Record.Tipo = value;
					PropertyHasChanged();
				}
			}
		}
		public virtual string CuentaContable
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.Record.CuentaContable;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (!_base.Record.CuentaContable.Equals(value))
				{
					_base.Record.CuentaContable = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual string CuentaContableGastos
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.CuentaContableGastos;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);
                if (value == null) value = string.Empty;
				if (!_base.Record.CuentaContableGastos.Equals(value))
                {
					_base.Record.CuentaContableGastos = value;
                    PropertyHasChanged();
                }
            }
        }
		public virtual Decimal SaldoInicial
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.Record.SaldoInicial;
			}

			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);

				if (!_base.Record.SaldoInicial.Equals(value))
				{
					_base.Record.SaldoInicial = Decimal.Round(value, 2);
					PropertyHasChanged();
				}
			}
		}
        public virtual string Observaciones
		{			
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _base.Record.Observaciones;
            }
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			set
			{
				CanWriteProperty(true);
				if (value == null) value = string.Empty;
				if (!_base.Record.Observaciones.Equals(value))
				{
					_base.Record.Observaciones = value;
					PropertyHasChanged();
				}
			}
		}
        public virtual DateTime FechaFirma
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.FechaFirma;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.FechaFirma.Equals(value))
                {
                    _base.Record.FechaFirma = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual DateTime DuracionPoliza
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.DuracionPoliza;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.DuracionPoliza.Equals(value))
                {
                    _base.Record.DuracionPoliza = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal Comision
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.Comision;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.Comision.Equals(value))
                {
                    _base.Record.Comision = Decimal.Round(value, 2);
                    PropertyHasChanged();
                }
            }
        }
        public virtual Decimal TipoInteres
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.TipoInteres;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.TipoInteres.Equals(value))
                {
                    _base.Record.TipoInteres = Decimal.Round(value, 2);
                    PropertyHasChanged();
                }
            }
        }
        public virtual bool PagoGastosInicio
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.PagoGastosInicio;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.PagoGastosInicio.Equals(value))
                {
                    _base.Record.PagoGastosInicio = value;
                    PropertyHasChanged();
                }
            }
        }
        public virtual long DiasCredito
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _base.Record.DiasCredito;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            set
            {
                CanWriteProperty(true);

                if (!_base.Record.DiasCredito.Equals(value))
                {
                    _base.Record.DiasCredito = value;
                    PropertyHasChanged();
                }
            }
        }

		public virtual BankAccounts CuentasAsociadas
		{
			[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
			get
			{
				CanReadProperty(true);
				return _cuentas_asociadas;
			}

			set
			{
				_cuentas_asociadas = value;
			}
		}

        public virtual BankAccounts FondosInversion
        {
            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
            get
            {
                CanReadProperty(true);
                return _fondos_inversion;
            }

            set
            {
                _fondos_inversion = value;
            }
        }

		//NO ENLAZADAS
		public EEstado EEstado { get { return (EEstado)_base.Record.Estado; } set { Estado = (long)value; } }
		public string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
		public ETipoCuenta ETipoCuenta { get { return (ETipoCuenta)_base.Record.Tipo; } set { TipoCuenta = (long)value; } }
		public string TipoCuentaLabel { get { return Library.Common.EnumText<ETipoCuenta>.GetLabel(ETipoCuenta); } }
		public Decimal Saldo { get { return _base.Saldo; } set { _base.Saldo = value; } }
		public Decimal SaldoDispuesto { get { return (_base.Record.Tipo == (long)ETipoCuenta.CuentaCorriente) ? 0 : _base.Record.SaldoInicial - _base.Saldo; } }
        public Decimal SaldoDisponible { get { return _base.SaldoDisponible; } }
		public string CuentaAsociada { get { return _base.CuentaAsociada; } set { _base.CuentaAsociada = value; } }

		public override bool IsValid
		{
			get
			{
				return base.IsValid
				   && _cuentas_asociadas.IsValid && _fondos_inversion.IsValid;
			}
		}
		public override bool IsDirty
		{
			get
			{
				return base.IsDirty
				   || _cuentas_asociadas.IsDirty || _fondos_inversion.IsDirty;
			}
		}

        #endregion

        #region Business Methods
        
		protected void CopyFrom(BankAccount parent)
		{
			ETipoCuenta = ETipoCuenta.ComercioExterior;
			OidCuentaAsociada = parent.Oid;
			EEstado = EEstado.Active;
		}
		
		public virtual void GetNewCode()
		{
			/*Serial = SerialInfo.GetNext(typeof(CuentaBancaria));
			Codigo = Serial.ToString(Resources.Defaults.DEFAULT_CODE_FORMAT);*/
		}

        #endregion

        #region Validation Rules

		protected override void AddBusinessRules()
		{
			ValidationRules.AddRule(CheckValidation, "Oid");
		}

		private bool CheckValidation(object target, Csla.Validation.RuleArgs e)
		{
			//Codigo
			if (Valor == string.Empty)
			{
				e.Description = Resources.Messages.NO_CUENTA_SELECTED;
				throw new iQValidationException(e.Description, string.Empty);
			}

			return true;
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

		#region Common Factory Methods

		/// <summary>
		/// NO UTILIZAR DIRECTAMENTE, SE DEBE USAR LA FUNCION NewChild
		/// Debería ser private para CSLA porque la creación requiere el uso de los Factory Methods,
		/// pero debe ser protected por exigencia de NHibernate
		/// y public para que funcionen los DataGridView
		/// </summary>
		public BankAccount()
		{
			Random r = new Random();
			Oid = (long)r.Next();
			//Rellenar si hay más campos que deban ser inicializados aquí
            _base.Record.FechaFirma = DateTime.Now;
            _base.Record.DuracionPoliza = DateTime.MaxValue;
		}

		private BankAccount(BankAccount source)
		{
			MarkAsChild();
			Fetch(source);
		}
		private BankAccount(int sessionCode, IDataReader reader, bool childs)
		{
			MarkAsChild();
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}

		internal static BankAccount GetChild(BankAccount source) { return new BankAccount(source); }
		internal static BankAccount GetChild(int sessionCode, IDataReader reader, bool childs) { return new BankAccount(sessionCode, reader, childs); }

		public virtual BankAccountInfo GetInfo(bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			return new BankAccountInfo(this, childs);
		}

		#endregion

		#region Root Factory Methods

		public static BankAccount New()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			return DataPortal.Create<BankAccount>(new CriteriaCs(-1));
		}

		public static BankAccount Get(long oid, bool childs = true)
		{
			if (!CanGetObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			CriteriaEx criteria = BankAccount.GetCriteria(BankAccount.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = BankAccount.SELECT(oid);

			Registro.BeginTransaction(criteria.Session);

			return DataPortal.Fetch<BankAccount>(criteria);
		}

		/// <summary>
		/// Borrado inmediato, no cabe "undo"
		/// (La función debe ser "estática")
		/// </summary>
		/// <param name="oid"></param>
		public static void Delete(long oid)
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

			DataPortal.Delete(new CriteriaCs(oid));
		}

		/// <summary>
		/// Elimina todos los Informe. 
		/// Si no existe integridad referencial, hay que eliminar las listas hijo en esta función.
		/// </summary>
		public static void DeleteAll()
		{
			//Iniciamos la conexion y la transaccion
			int sessCode = BankAccount.OpenSession();
			ISession sess = BankAccount.Session(sessCode);
			ITransaction trans = BankAccount.BeginTransaction(sessCode);

			try
			{
                sess.Delete("from BankAccountRecord");
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
			finally
			{
				BankAccount.CloseSession(sessCode);
			}
		}

		/// <summary>
		/// Guarda en la base de datos todos los cambios del objeto.
		/// También guarda los cambios de los hijos si los tiene
		/// </summary>
		/// <returns>Objeto actualizado y con los flags reseteados</returns>
		public override BankAccount Save()
		{
			// Por la posible doble interfaz Root/Child
			if (IsChild) throw new iQException(Library.Resources.Messages.CHILD_SAVE_NOT_ALLOWED);

			if (IsDeleted && !CanDeleteObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (IsNew && !CanAddObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);
			else if (!CanEditObject())
				throw new System.Security.SecurityException(Library.Resources.Messages.USER_NOT_ALLOWED);

            try
            {
                ValidationRules.CheckRules();
            }
            catch (iQValidationException ex)
            {
                iQExceptionHandler.TreatException(ex);
                return this;
            }

            try
            {
                base.Save();

				_cuentas_asociadas.Update(this);
                _fondos_inversion.Update(this);

				Transaction().Commit();

				return this;
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
				return this;
			}
			finally
			{
				if (CloseSessions) CloseSession(); 
				else BeginTransaction();
			}
		}

		#endregion				

		#region Child Factory Methods
		
		//Por cada padre que tenga la clase
		public static BankAccount NewChild()
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);
			
			BankAccount obj = new BankAccount();
			obj.MarkAsChild();
			return obj;
		}
		public static BankAccount NewChild(BankAccount parent)
		{
			if (!CanAddObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);

			BankAccount obj = DataPortal.Create<BankAccount>(new CriteriaCs(-1));
			obj.CopyFrom(parent);
			obj.MarkAsChild();

			return obj;
		}
        public static BankAccount NewChild(BankAccount parent, ETipoCuenta tipo)
        {
            BankAccount obj = NewChild(parent);
            obj.ETipoCuenta = tipo;

            return obj;
        }

		/// <summary>
		/// Borrado aplazado, es posible el undo 
		/// (La función debe ser "no estática")
		/// </summary>
		public override void Delete()
		{
			if (!CanDeleteObject())
				throw new System.Security.SecurityException(moleQule.Library.Resources.Messages.USER_NOT_ALLOWED);			
				
			MarkDeleted();
		}
			
		#endregion

		#region Common Data Access

		[RunLocal()]
		private void DataPortal_Create(CriteriaCs criteria)
		{
			_base.Record.Oid = (long)(new Random()).Next();
			EEstado = EEstado.Active;
			ETipoCuenta = ETipoCuenta.CuentaCorriente;

			GetNewCode();
		}

		private void Fetch(IDataReader source)
		{
			_base.CopyValues(source);

			if (Childs)
			{
				string query = string.Empty;
				IDataReader reader;

				BankAccount.DoLOCK(Session());
				query = BankAccounts.SELECT(this);
				reader = nHMng.SQLNativeSelect(query, Session());
				_cuentas_asociadas = BankAccounts.GetChildList(SessionCode, reader, false);

                BankAccount.DoLOCK(Session());
                query = BankAccounts.SELECT(this, ETipoCuenta.FondoInversion);
                reader = nHMng.SQLNativeSelect(query, Session());
                _fondos_inversion = BankAccounts.GetChildList(SessionCode, reader, false);
			}

			MarkOld();
		}

		internal void Insert(BankAccounts parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				parent.Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Update(BankAccounts parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(moleQule.Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

				SessionCode = parent.SessionCode;
				BankAccountRecord obj = Session().Get<BankAccountRecord>(Oid);
				obj.CopyValues(Base.Record);
				Session().Update(obj);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void DeleteSelf(BankAccounts parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
				Session().Delete(Session().Get<BankAccountRecord>(Oid));
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkNew();
		}

		#endregion

		#region Root Data Access

		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                _base.Record.Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				if (nHMng.UseDirectSQL)
				{
					BankAccount.DoLOCK(Session());
					IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

					if (reader.Read())
						_base.CopyValues(reader);

					if (Childs)
					{
						string query = string.Empty;

						BankAccount.DoLOCK(Session());
						query = BankAccounts.SELECT(this);
						reader = nHMng.SQLNativeSelect(query, Session());
                        _cuentas_asociadas = BankAccounts.GetChildList(SessionCode, reader, false);

                        BankAccount.DoLOCK(Session());
                        query = BankAccounts.SELECT(this, ETipoCuenta.FondoInversion);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _fondos_inversion = BankAccounts.GetChildList(SessionCode, reader, false);
					}
				}
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
		}

		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Insert()
		{
			try
            {
                if (!SharedTransaction)
                {
                    if (SessionCode < 0) SessionCode = OpenSession();
                    BeginTransaction();
                }
				//Si hay codigo o serial, hay que obtenerlos aquí
				GetNewCode();
				Session().Save(Base.Record);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_Update()
		{
			if (IsDirty)
			{
				BankAccountRecord obj = Session().Get<BankAccountRecord>(Oid);
				obj.CopyValues(this._base.Record);
				Session().Update(obj);
				MarkOld();
			}
		}

		//Deferred deletion
		[Transactional(TransactionalTypes.Manual)]
		protected override void DataPortal_DeleteSelf()
		{
			DataPortal_Delete(new CriteriaCs(Oid));
		}

		[Transactional(TransactionalTypes.Manual)]
		private void DataPortal_Delete(CriteriaCs criteria)
		{
			try
			{
				// Iniciamos la conexion y la transaccion
				SessionCode = OpenSession();
				BeginTransaction();

				//Si no hay integridad referencial, aquí se deben borrar las listas hijo
				CriteriaEx criterio = GetCriteria();
				criterio.AddOidSearch(criteria.Oid);
				Session().Delete((BankAccountRecord)(criterio.UniqueResult()));
				Transaction().Commit();
			}
			catch (Exception ex)
			{
				if (Transaction() != null) Transaction().Rollback();
				iQExceptionHandler.TreatException(ex);
			}
			finally
			{
				CloseSession();
			}
		}

		#endregion

		#region Child Data Access

		private void Fetch(BankAccount source)
		{
			_base.CopyValues(source);

			if (Childs)
			{
				/*string query = string.Empty;
				IDataReader reader = null;*/
			}

			MarkOld();
		}

		internal void Insert(BankAccount parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			this.OidCuentaAsociada = parent.Oid;
            if (_base.Record.FechaFirma <= DateTime.MinValue) _base.Record.FechaFirma = DateTime.Now;
            if (_base.Record.DuracionPoliza <= DateTime.MinValue) _base.Record.DuracionPoliza = DateTime.MaxValue;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);
                
                Assembly assembly = Assembly.Load("moleQule.Library.Invoice");
                Type type = assembly.GetType("moleQule.Library.Invoice.MovimientoBanco");

                parent.Session().Save(Base.Record);

                type.InvokeMember("InsertItemComisiones", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new object[2] { GetInfo(false), parent.SessionCode });
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void Update(BankAccount parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			this.OidCuentaAsociada = parent.Oid;

			try
			{
				ValidationRules.CheckRules();

				if (!IsValid)
					throw new iQValidationException(Library.Resources.Messages.GENERIC_VALIDATION_ERROR);

                SessionCode = parent.SessionCode;
                if (_base.Record.FechaFirma <= DateTime.MinValue) _base.Record.FechaFirma = DateTime.Now;
                if (_base.Record.DuracionPoliza <= DateTime.MinValue) _base.Record.DuracionPoliza = DateTime.MaxValue;
				BankAccountRecord obj = Session().Get<BankAccountRecord>(Oid);
				obj.CopyValues(this._base.Record);
                Session().Update(obj);

                Assembly assembly = Assembly.Load("moleQule.Library.Invoice");
                Type type = assembly.GetType("moleQule.Library.Invoice.MovimientoBanco");

                type.InvokeMember("EditItemComisiones", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new object[3] { GetInfo(false), this.GetInfo(false), parent.SessionCode });
			
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkOld();
		}

		internal void DeleteSelf(BankAccount parent)
		{
			// if we're not dirty then don't update the database
			if (!this.IsDirty) return;

			// if we're new then don't update the database
			if (this.IsNew) return;

			try
			{
				SessionCode = parent.SessionCode;
                Session().Delete(Session().Get<BankAccountRecord>(Oid));

                Assembly assembly = Assembly.Load("moleQule.Library.Invoice");
                Type type = assembly.GetType("moleQule.Library.Invoice.MovimientoBancoRecord");

                type.InvokeMember("AnulaItemComisiones", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new object[2] { GetInfo(false), parent.SessionCode });
			
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}

			MarkNew();
		}

		#endregion

		#region SQL

		public new static string SELECT(long oid) { return BankAccount.SELECT(oid, true); }
        internal static string SELECT(long oid, bool lockTable)
        {
            return SELECT(new QueryConditions { BankAccount = BankAccountInfo.New(oid) }, lockTable);
        }

		internal static string FIELDS()
		{
			string query = @"
            SELECT CB.*
			        ,MV.""SALDO"" + CB.""SALDO_INICIAL"" AS ""SALDO""
					,CB2.""VALOR"" AS ""CUENTA_ASOCIADA""";

			return query;
		}

		internal static string WHERE(QueryConditions conditions)
		{
			string query = @"
            WHERE TRUE";

			if (conditions.Estado != EEstado.Todos) 
                query += @" 
                AND CB.""ESTADO"" = " + ((long)conditions.Estado);

			if (conditions.BankAccount != null)
			{
				if (conditions.BankAccount.Oid != 0) 
                    query += @" 
                    AND CB.""OID"" = " + conditions.BankAccount.Oid;

                if (conditions.BankAccount.OidCuentaAsociada != 0)
                {
                    query += @" 
                    AND CB.""OID_ASOCIADA"" = " + conditions.BankAccount.OidCuentaAsociada;

                    if (conditions.TipoCuenta != ETipoCuenta.FondoInversion)
                        query += @" 
                        AND CB.""TIPO"" != " + (long)ETipoCuenta.FondoInversion;
                }
			}

			if (conditions.TipoCuenta != ETipoCuenta.Todas) 
                query += @" 
                AND CB.""TIPO"" = " + ((long)conditions.TipoCuenta);

			return query + " " + conditions.ExtraWhere;
		}

		internal static string SELECT_BASE(QueryConditions conditions)
		{
            string cb = nHManager.Instance.GetSQLTable(typeof(BankAccountRecord));

			Assembly assembly = Assembly.Load("moleQule.Library.Invoice");
			Type type = assembly.GetType("moleQule.Library.Invoice.BankLine");

			EEstado estado = conditions.Estado;
			conditions.Estado = EEstado.Todos;

			//string subquery = (string)type.InvokeMember("SELECT_RESUMEN_CUENTA", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new object[1] { conditions });
            string subquery = (string)type.InvokeMember("SELECT_SALDOS", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, null);

			string query =	
            FIELDS() + @"
			FROM " + cb + @" AS CB
			LEFT JOIN " + cb + @" AS CB2 ON CB2.""OID"" = CB.""OID_ASOCIADA""
			LEFT JOIN (" + subquery + @") AS MV ON MV.""OID_CUENTA"" = CB.""OID""";

			conditions.Estado = estado;

			return query;
		}

		internal static string SELECT(QueryConditions conditions, bool lockTable)
		{
			string query = 
            SELECT_BASE(conditions) +
            WHERE(conditions);
			
			query += @"
            ORDER BY ""ENTIDAD""";

			//if (lock_table) query += " FOR UPDATE OF CB NOWAIT";

			return query;
		}
        
        internal static string SELECT_ASOCIADAS(QueryConditions conditions, bool lockTable) 
		{
            conditions.ExtraWhere = @"
                AND CB.""OID_ASOCIADA"" != 0";

			string query = 
            SELECT_BASE(conditions) +
            WHERE(conditions);

			query += @"
            ORDER BY ""VALOR""";

            //query += EntityBase.LOCK("CB", locktTable);

			return query;
		}
        internal static string SELECT_NO_ASOCIADAS(QueryConditions conditions, bool lockTable)
		{
            conditions.ExtraWhere = @"
                AND CB.""OID_ASOCIADA"" = 0";

			string query = 
            SELECT_BASE(conditions) +
            WHERE(conditions);

			query += @"
            ORDER BY ""VALOR""";

            //query += EntityBase.LOCK("CB", locktTable);

			return query;
		}

		#endregion
	}
}