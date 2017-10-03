using System;
using System.Collections.Generic;

using moleQule.Library;

namespace moleQule.Library.Common
{
    #region Querys

    public class QueryConditions : moleQule.Library.QueryConditions
    {
		public long Oid = 0;
		public ETipoEntidad EntityType = ETipoEntidad.Todos; 
		public EEstado[] Status = null;

		public AyudaInfo Ayuda = null;
		public AyudaPeriodoInfo AyudaPeriodo = null;
		public BankAccountInfo BankAccount = null;
        public CreditCardInfo CreditCard = null;
        public CreditCardStatementInfo CreditCardStatement = null;
		public Modelo Modelo = null;
		public MonitorInfo Monitor = null;
		public MonitorLineInfo MonitorLine = null;
		public MunicipioInfo Municipio = null;
		public RelationInfo Relation = null;
		public RegistroInfo Registro = null;
		public PesajeInfo Pesaje = null;

		public EEstado Estado = EEstado.Todos;
		public ETipoCuenta TipoCuenta = ETipoCuenta.Todas;
		public ETipoEntidad TipoEntidad = ETipoEntidad.Todos;
		public ETipoRegistro TipoRegistro = ETipoRegistro.Todos;
		public ETipoTarjeta TipoTarjeta = ETipoTarjeta.Todos;
		public ETipoTitular TipoTitular = ETipoTitular.Todos;
        public ESubtipoFactura SubtipoFactura = ESubtipoFactura.Todas;
	}

	public delegate string SelectCaller(QueryConditions conditions);

    #endregion

    #region Enums
	
	public enum ECurrency { All = 0, Euro = 1, Dolar = 2, Pound = 3 }

	public enum EEstado { 
		Todos = 0, 
		Abierto = 1, 
		Emitido = 2, 
		Contabilizado = 3, 
		Anulado = 4, 
		Exportado = 5, 
		Failed = 28,
		Billed = 6, 
		Pendiente = 7, 
		Closed = 8, 
		NoAnulado = 9, 
		Active = 10, 
		Baja = 11, 
		Pagado = 12,
		Oculto = 17,
		Created = 22,
		Inactive = 23,
		LockedOut = 24,
		Paused = 25,
		Inconsistent = 26,
		Expired = 27,
		Processed = 31,

		//Transaction
		Released = 29,

		//Facturas
		DudosoCobro = 20,
		Charged = 21,
        Devuelto = 28,

		//Fomento
		EnSolicitud = 13,
		Solicitado = 14,
		Aceptado = 15,
		Desestimado = 16,
		Cancelado = 19,
		Alta = 18,

        //Movimientos Bancarios
        Auditado = 30,
		
		//Especiales
		NoOculto = -2,
	}

	public enum EFile { Photo = 1, Passport = 2, DrivingLicense = 3 }
 
	public enum EFormaPago { Contado = 1, XDiasFechaFactura = 2, XDiasMes = 3, Trimestral = 4, MesVencido = 5 }

	public enum EMedioPago
	{
		Seleccione = -1, Efectivo = 1, Ingreso = 2, Cheque = 3, Pagare = 4, Giro = 5, Transferencia = 6,
		CompensacionFactura = 7, Tarjeta = 8, Domiciliacion = 9, ComercioExterior = 10,
		//Valores especiales por debajo de -1. Pueden o no mostrarse en los combo.    
		Todos = 0, NoEfectivo = -3, NoTarjeta = -4
	}

	public enum EModelo { Modelo111 = 1, Modelo347 = 2, Modelo420 = 3, Modelo425 = 4 }

    public enum ENotice { All = 0, Error = 1, Info = 2, SubscriptionActive = 3, SubscriptionExpired = 4, SubscriptionFinished = 5, Contact = 6, NewRegistration = 7, Monitor = 8, CrewNotice = 9 }

    public enum ETipoAcreedor
    {
        Todos = 0,
        Proveedor = 1, Naviera = 2, TransportistaOrigen = 3,
        TransportistaDestino = 4, Despachante = 5, Instructor = 6,
        Acreedor = 7, Empleado = 8, Partner = 9, CreditCardStatement = 10
    }
	public enum ETipoModelo { Soportado = 1, Repercutido = 2, Profesionales = 3, EmpleadosTrabajo = 4, EmpleadosEspecie = 5 }

	public enum EPeriodo { Periodo1T = 1, Periodo2T = 2, Periodo3T = 3, Periodo4T = 4, Anual = 5, }
	
	public enum ETipoAgente
	{
		Alumno = 1,		// 0000000000000001
		Instructor = 2		// 0000000000000010
	}
	public enum ETipoCuenta { Todas = 0, CuentaCorriente = 1, ComercioExterior = 2, LineaCredito = 3, FondoInversion = 4 }
	public enum ETipoDescuento { Precio = 1, Porcentaje = 2 }
	public enum ETipoEntidad 
	{ 
		Todos = 0, 
		FacturaRecibida = 1, 
		FacturaEmitida = 2, 
		Pago = 3, 
		Cobro = 4,
		CobroFactura = 5,
		PagoFactura = 6,
		Cliente = 7, 
		Proveedor = 8, 
		Despachante = 9, 
		Naviera = 10, 
		TransportistaOrigen = 11, 
		TransportistaDestino = 12, 
		Instructor = 13, 
		Acreedor = 14,
		Empleado = 15,
		Nomina = 16,
		Gasto = 17,
		LineaFomento = 18,
		TipoGasto = 19,
		Impuesto = 20,
		Familia = 21,
		Expediente = 22,
		PResumen = 23,
		Partida = 24,
		CuentaBancaria = 25,
		Caja = 26,
		Ticket = 27,
		TarjetaCredito = 28,
		MovimientoBanco = 29,
        Auditoria = 30,
		ExpedienteREA = 31,
		CResumen = 32,
		Partner = 33, 
        Prestamo = 34,
		Subscription = 35,
        Traspaso = 36,
		Tool = 37,
		OutputDelivery = 38,
		WorkReport = 39,
		Transportista = 40,
		InputDelivery = 41,
		Producto = 42,
		CurrencyExchange = 43,
        CreditCardStatement = 44,
        FinancialCash = 45,
        Acepcion = 46, 
        Clave = 47,
        Diccionario = 48,
        Ejemplo = 49, 
        Geografia = 50,
        Incluyente = 51,
        Locucion = 52,
        ClaveLocucion = 53,
        Uso = 54
	}
    public enum ETipoExportacion { ContaWin = 1, Tinfor = 2, A3 = 3 }
	public enum ETipoID { OTROS = 0, CIF = 1, NIF = 2, NIE = 4, DNI = 8 }
    public enum ETipoImpuesto { Defecto = 0, Definido = 1 }

	public enum ETipoNotificacion 
    { 
        IngresoPendiente = 1, GastoPendiente = 2, Gastos = 3, Ingresos = 4, FacturaRecibida = 5, 
		Existencias = 6, Node = 7, PagoVencido = 8, PagoTarjetaVencido = 9, CobroVencido = 10, 
		ExtractoTarjeta = 11, PagoBancoPendiente = 12, DiscrepanciaAbierta = 13, InformeNoGenerado = 14, 
		GastoEstimado = 15, ComercioExterior = 16, Morosos = 17, Riesgo = 18, ProductoStockNegativo = 19,
        Loans = 20 
    }

	public enum ETipoRegistro { Todos = 0, Contabilidad = 1, Notificacion = 2, Fomento = 3, Email = 4, CompanyExport = 5 }

	public enum ETipoTarjeta { Todos = 0, Credito = 1, Debito = 2 }

    public enum ETipoTitular {	
        Todos = 0, 
		Cliente = 1, Proveedor = 2, Despachante = 3, Naviera = 4, 
		TransportistaOrigen = 5, Instructor = 6, REA = 7, Acreedor = 8,
		Varios = 9, Empleado = 10, Partner = 11, Fomento = 12, TransportistaDestino = 13
	}
	
    public enum EIdioma { Todos = 0, Spanish = 1, English = 2 }

	public enum ESexo { Macho = 1, Hembra = 2 }

    public enum ESubtipoFactura { Todas = 0, Emitida = 1, Recibida = 2 }

	public static class EnumConvert
	{
		public static EEstado ToEEstado(EEstadoItem source)
		{
			switch (source)
			{
				case EEstadoItem.Unlock: return EEstado.Abierto;
				case EEstadoItem.Contabilizado: return EEstado.Contabilizado;
				case EEstadoItem.Emitido: return EEstado.Emitido;
				case EEstadoItem.Anulado: return EEstado.Anulado;
			}

			return EEstado.Todos;
		}
	}

    public class EnumText<T> : EnumTextBase<T>
    {
		public static ComboBoxList<T> GetList(bool emptyValue = true, bool allValue = false, bool specialValues = false)
		{
			return GetList(Resources.Enums.ResourceManager, emptyValue, specialValues, allValue);
		}

		public static ComboBoxList<T> GetList(T[] list)
		{
			return GetList(Resources.Enums.ResourceManager, list);
		}

		public static ComboBoxList<T> GetList(T[] list, bool emptyValue)
        {
			return GetList(Resources.Enums.ResourceManager, list, emptyValue);
        }

        public static string GetLabel(object value)
        {
            return GetLabel(Resources.Enums.ResourceManager, value);
        }

		public static string GetPrintLabel(object value)
		{
			return GetPrintLabel(Resources.Enums.ResourceManager, value);
		}
    }

    #endregion

	#region Enum Functions

	public static class EnumFunctions
	{
		public static DateTime GetPrevisionPago(EFormaPago forma_pago, DateTime fecha_base, long dias_credito)
		{ 
            return GetPrevisionPago(forma_pago, fecha_base, dias_credito, 1); 
        }
		public static DateTime GetPrevisionPago(EFormaPago forma_pago, DateTime fecha_base, long dias_credito, long dia_extracto)
		{
			switch (forma_pago)
			{
				case EFormaPago.Contado:
					return fecha_base;

				case EFormaPago.XDiasFechaFactura:
					return fecha_base.AddDays(dias_credito);

				case EFormaPago.XDiasMes:
					{
						DateTime f;
						f = fecha_base.AddMonths(1);
						f = new DateTime(f.Year, f.Month, 1);
						return f.AddDays(dias_credito - 1);
					}

				case EFormaPago.MesVencido:
					{
						DateTime f;
						f = fecha_base.AddMonths(1);
						f = new DateTime(f.Year, f.Month, (dias_credito != 0) ? (int)dias_credito : 1);

						if (fecha_base.Day <= dia_extracto)
							return f;
						else
							return f.AddMonths(1);
					}

				case EFormaPago.Trimestral:
					{
						DateTime f = fecha_base;

						switch ((int)(fecha_base.Month % 3))
						{
							case 1: f = fecha_base.AddMonths(3); break;
							case 2: f = fecha_base.AddMonths(2); break;
							case 0: f = fecha_base.AddMonths(1); break;
						}

						try
						{
							return DateAndTime.Parse(Convert.ToInt32(dias_credito), f.Month, f.Year);
						}
						catch { return f; }
					}
			}

			return fecha_base;
		}

		public static bool NeedsCuentaBancaria(EMedioPago medio)
		{
			return (medio == EMedioPago.Cheque) ||
					(medio == EMedioPago.Pagare) ||
					(medio == EMedioPago.Transferencia) ||
					(medio == EMedioPago.Giro) ||
					(medio == EMedioPago.Ingreso) ||
					(medio == EMedioPago.Domiciliacion) ||
					(medio == EMedioPago.ComercioExterior) ||
					(medio == EMedioPago.Tarjeta);
		}

		public static string SQL_IN_MEDIO_PAGO_NEEDS_CUENTA_BANCARIA()
		{
			return "(" + ((long)EMedioPago.Pagare).ToString() +
					"," + ((long)EMedioPago.Cheque).ToString() +
					"," + ((long)EMedioPago.Transferencia).ToString() +
					"," + ((long)EMedioPago.Giro).ToString() +
					"," + ((long)EMedioPago.Ingreso).ToString() +
					"," + ((long)EMedioPago.Domiciliacion).ToString() +
					"," + ((long)EMedioPago.ComercioExterior).ToString() +
					"," + ((long)EMedioPago.Tarjeta).ToString() + ")";
		}

		public static string SQL_IN_MEDIO_PAGO_NOT_NEEDS_CUENTA_BANCARIA()
		{
			return "(" + ((long)EMedioPago.CompensacionFactura).ToString() +
					"," + ((long)EMedioPago.Efectivo).ToString() + ")";
		}
	}

	#endregion

	#region Contabilidad y Modelos

	public struct CuentaResumen
	{
		private decimal _importe;

		public long OidFamilia;
		public decimal Importe { get { return Decimal.Round(_importe, 2); } set { _importe = value; } }
		public string CuentaContable;
		public ImpuestoResumen Impuesto;
        public string Nombre;
	}
	
	public class Modelo
	{
		public EModelo EModelo = EModelo.Modelo347;
		public ETipoModelo ETipoModelo = ETipoModelo.Soportado;
		public decimal MinImporte = 0;
		public decimal MinEfectivo = 0;
	}

	#endregion
}
