using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

using moleQule.Library.CslaEx;

namespace moleQule.Library
{
	#region Querys

	public class QueryConditions
	{
		public UserInfo User = null;
		public ISchemaInfo Schema = null;
		public ItemMapInfo ItemMap = null;
		public SchemaSettingInfo SchemaSetting = null;
		public SettingItemInfo SettingItem = null;
		public UserSettingInfo UserSetting = null;

		public int Year = DateTime.MinValue.Year;

		public DateTime FechaIni = DateTime.MinValue;
		public DateTime FechaFin = DateTime.MaxValue;

		public string FechaIniLabel { get { return FechaIni.ToString("MM/dd/yyyy 00:00:00"); } }
		public string FechaFinLabel { get { return FechaFin.ToString("MM/dd/yyyy 23:59:59"); } }

		public DateTime FechaAuxIni = DateTime.MinValue;
		public DateTime FechaAuxFin = DateTime.MaxValue;

		public string FechaAuxIniLabel { get { return FechaAuxIni.ToString("MM/dd/yyyy 00:00:00"); } }
		public string FechaAuxFinLabel { get { return FechaAuxFin.ToString("MM/dd/yyyy 23:59:59"); } }

		//Order
		public ListSortDirection Order = ListSortDirection.Ascending;
		public List<string> OrderFields = null;

		public PagingInfo PagingInfo = null;
		public GroupList Groups = null;
		public FilterList Filters = null;
		public OrderList Orders = null;
		public EStepGraph Step = EStepGraph.None;

		public List<long> OidList = null;
        public HashOidList HashOidList = null;

		public long OidEntity = 0;
		public string Query = string.Empty;

		public string ExtraJoin = string.Empty;
		public string ExtraWhere = string.Empty;

		public static string GetFechaLabel(DateTime date) { return date.ToString("MM/dd/yyyy HH:mm:ss"); }
		public static string GetFechaMinLabel() { return GetFechaMinLabel(DateTime.MinValue); }
		public static string GetFechaMinLabel(DateTime date) { return date.ToString("MM/dd/yyyy 00:00:00"); }
		public static string GetFechaMaxLabel() { return GetFechaMaxLabel(DateTime.MaxValue); }
		public static string GetFechaMaxLabel(DateTime date) { return date.ToString("MM/dd/yyyy 23:59:59"); }
	}

	#endregion

	#region Enums

	public enum EAppType { Desktop = 1, Web = 2, Service = 3 }

	public enum EBrowser { IE8 = 1, IE9 = 2, IE10 = 3, Chrome = 4, Firefox12 = 5 }

	public enum EContact { All = 0, Admin = 1, CustomerService = 2, Support = 3, Client = 4 }

	public enum EEnvironment { Release = 1, Staging = 2, Development = 3, Test = 4, Alpha = 5 }

	public enum EErrorLevel { DUMMY = -1, OK = 0, WARNING = 1, CRITICAL = 2 }
	public enum EErrorType { DUMMY = -1, ADMIN = 0, SUPPORT = 1 }
	public enum ELogLevel { DEBUG = 1, ERROR, FATAL, INFO, WARN, ALL }
	public enum EComponentStatus { OK = 1, ERROR = 2, WORKING = 3, UNAVAILABLE = 4, NOTIFIED = 5, READY = 6, DISABLED = 7 }
	public enum EComponentType { All = 0, WinService = 1, Terminal = 2, Cabinet = 3, WebService = 4 }

	public enum EValue { Empty = -1, All = 0, Default = 1 }

	public enum EEstadoItem { Unlock = 0, Contabilizado = 1, Emitido = 2, Anulado = 3, Registered = 4, Baja = 5, Active = 10, LockedOut = 6, Inactive = 7 }

	public enum EPrivilege { Read = 1, Create = 2, Modify = 3, Delete = 4, All = 0 }

	public enum ERol { None = -1, All = 0, SuperUser = 1, Admin = 2, User = 3, Partner = 4, Client = 5, Provider = 6 }

	public enum ESecureItem
	{
		Variable = 000,
		Tabla_Auxiliar = 001,
		Empresa = 002,
		Informe = 003,
		Estado = 004,
		Generico = 005,

		Documento = 101,

		Stock = 201,
		Empleado = 202,
		Expediente = 203,
		Factura_Recibida = 204,
		Gasto = 205,
		Nomina = 206,
		Pago = 207,
		Producto = 208,
		Proveedor = 209,
		Serie = 210,
		Albaran_Recibido = 211,
		Almacen = 212,

		Cliente = 301,
		Factura_Emitida = 302,
		Movimiento_Banco = 303,
		Proforma = 304,
		Caja = 305,
		Albaran_Emitido = 306,
		Cobro = 307,
		CuentaContable = 308,
		Transaction = 309,
		AuditarMovimientos = 310,

		Alumno = 401,
		Curso = 402,
		Curso_Formacion = 403,
		Examen = 404,
		Horario = 405,
		Instructor = 406,
		Material_Docente = 407,
		Modulo = 408,
		Parte_Asistencia = 409,
		Plan_Estudios = 410,
		Plan_Extra = 411,
		Promocion = 412,

		Discrepancia = 501,
		Clase_Auditoria = 502,
		Auditoria = 503,
		Area = 504,
		Ampliacion = 505,
		Acta_Comite_Calidad = 506,
		Accion_Correctora = 507,
		Incidencia = 508,
		Plan_Anual = 509,

		Partner = 601,

		Suscription = 701,
        Cabinet = 702,
	}

	public enum ESMSGateway { All = 0, Esendex = 1 }

	public enum EStatus
	{
		OK = 0,
		Error = 1,
		Working = 2,
		Cancelled = 3,
		Closed = 4
	}

    public enum EStepGraph { None = 0, Hour = 1, Day = 2, Week = 3, Month = 4, Year = 5 }

	public enum EReportVista { Detallado = 0, Resumido = 1, Agrupado = 2, ListaCompleta = 3 }

	public enum molAction
	{
		Copy = 0,
		Print = 1,
		Select = 2,
		Add = 3,
		View = 4,
		Edit = 5,
		Delete = 6,
		Close = 7,
		Default = 8,
		SelectAll = 9,
		FilterOn = 10,
		FilterOff = 11,
		PrintDetail = 12,
		Save = 13,
		Cancel = 14,
		ShowDocuments = 15,
		Submit = 16,
		Unlock = 17,
		Lock = 18,
		CancelBkJob = 19,
		FilterAll = 20,
		SendEmail = 21,
		EmailPDF = 22,
		EmailLink = 23,
		ExportPDF = 24,
		AdvancedSearch = 25,
		FilterGlobal = 26,
		PrintListQR = 27,
		ChangeStateContabilizado = 28,
		ChangeStateEmitido = 29,
		ChangeStateAnulado = 30,
		Refresh = 32,
		CustomAction1 = 33,
		CustomAction2 = 34,
		CustomAction3 = 35,
		CustomAction4 = 36,
		DateSelection = 37,
		CustomAction = 38,
		List = 39,
	}

	public enum TipoId
	{
		CIF = 0,
		NIF = 2,
		NIE = 4,
		DNI = 8,
		OTROS = 16
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

		public static string GetLabel(object value)
		{
			return GetLabel(Resources.Enums.ResourceManager, value);
		}

		public static string GetPrintLabel(object value)
		{
			return GetPrintLabel(Resources.Enums.ResourceManager, value);
		}
	}

	public class EStatusMng
	{
		public static List<EEstadoItem> GetStatusByEntity(Type entityType)
		{
			switch (entityType.FullName)
			{
				case "moleQule.Library.User":
					{
						return new List<EEstadoItem> {
                                            EEstadoItem.Active,
                                            EEstadoItem.Inactive,
                                            EEstadoItem.Registered,
                                            EEstadoItem.LockedOut
                        };
					}
				default:
					return null;
			}
		}
	}

	#endregion

	#region Formats & Reports

	public struct ReportFormat
	{
		public EReportVista Vista;
		public string CampoOrdenacion;
		public CrystalDecisions.Shared.SortDirection Orden;
	}

	#endregion
}
