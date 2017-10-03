using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;
using moleQule.Library;

namespace moleQule.Library.Common
{
	/// <summary>
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class LineaRegistroInfo : ReadOnlyBaseEx<LineaRegistroInfo>
	{	
		#region Attributes

		public LineaRegistroBase _base = new LineaRegistroBase();

		#endregion

		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidRegistro { get { return _base.Record.OidRegistro; } }
		public long OidEntidad { get { return _base.Record.OidEntidad; } }
		public long TipoEntidad { get { return _base.Record.TipoEntidad; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Estado { get { return _base.Record.Estado; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public string Descripcion { get { return _base.Record.Descripcion; } }
		public string SerialExportacion { get { return _base.Record.IdExportacion; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }

		//NO ENLAZADAS
		public long TipoRegistro { get { return _base._tipo_registro; } }
		public ETipoRegistro ETipoRegistro { get { return _base.ETipoRegistro; } set { _base._tipo_registro = (long)value; } }
		public string TipoRegistroLabel { get { return Library.Common.EnumText<ETipoRegistro>.GetLabel(ETipoRegistro); } }
		public string IDExportacion { get { return _base.Record.IdExportacion; } }
		public string CodigoRegistro { get { return _base._codigo_registro; } set { _base._codigo_registro = value; } }
		public string IDCompuesto { get { return _base.IDCompuesto; } }
		public ETipoEntidad ETipoEntidad { get { return _base.ETipoEntidad; } }
		public string TipoEntidadLabel { get { return _base.TipoEntidadLabel; } }
		public EEstado EEstado { get { return _base.EEstado; } }
		public string EstadoLabel { get { return _base.EstadoLabel; } }
		public string CodigoEntidad { get { return _base._codigo_entidad; } }
		public long EstadoEntidad { get { return _base._estado_entidad; } }
		public EEstado EEstadoEntidad { get { return _base.EEstadoEntidad; } set { _base._estado_entidad = (long)value; } }
		public string EstadoEntidadLabel { get { return _base.EstadoEntidadLabel; } }

        public string Expediente { get { return _base._expediente; } }
        public string Producto { get { return _base._producto; } }
        public string LineaFomento { get { return _base._linea_fomento; } }
        public DateTime FechaConocimiento { get { return _base._fecha_conocimiento; } }
        public decimal Subvencion { get { return Decimal.Round(_base._subvencion, 2); } }

		#endregion
		
		#region Business Methods
        				
		public void CopyFrom(LineaRegistro source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected LineaRegistroInfo() { /* require use of factory methods */ }
		private LineaRegistroInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal LineaRegistroInfo(LineaRegistro item, bool childs)
		{
			_base.CopyValues(item);
			
			if (childs)
			{
				
			}
		}
	
		public static LineaRegistroInfo GetChild(int sessionCode, IDataReader reader, bool childs = false)
        {
			return new LineaRegistroInfo(sessionCode, reader, childs);
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
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

        #region SQL

        public static string SELECT(long oid) { return LineaRegistro.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return LineaRegistro.SELECT(conditions, false); }
		public static string SELECT(RegistroInfo item) { return SELECT(new QueryConditions { Registro = item }); }			
		
        #endregion		
	}

	/// <summary>
	/// ReadOnly Root Object
	/// </summary>
	[Serializable()]
	public class SerialLineaRegistroInfo : SerialInfo
	{
		#region Attributes

		#endregion

		#region Properties

		#endregion

		#region Business Methods

		#endregion

		#region Common Factory Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
		/// </remarks>
		protected SerialLineaRegistroInfo() { /* require use of factory methods */ }

		#endregion

		#region Root Factory Methods

		public static SerialRegistroInfo Get(ETipoRegistro tipo) { return Get(tipo, 0); }
		public static SerialRegistroInfo Get(ETipoRegistro tipo, int year)
		{
			CriteriaEx criteria = LineaRegistro.GetCriteria(LineaRegistro.OpenSession());
			criteria.Childs = false;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(tipo, year);

			SerialRegistroInfo obj = DataPortal.Fetch<SerialRegistroInfo>(criteria);
			LineaRegistro.CloseSession(criteria.SessionCode);
			return obj;
		}
		public static SerialLineaRegistroInfo GetIDExportacion(ETipoRegistro tipo, int year)
		{
			CriteriaEx criteria = LineaRegistro.GetCriteria(LineaRegistro.OpenSession());
			criteria.Childs = false;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT_EXPORTACION(tipo, year);

			SerialLineaRegistroInfo obj = DataPortal.Fetch<SerialLineaRegistroInfo>(criteria);
			LineaRegistro.CloseSession(criteria.SessionCode);
			return obj;
		}

		public static long GetNext(ETipoRegistro tipo) { return Get(tipo).Value + 1; }
		public static long GetNext(ETipoRegistro tipo, int year) { return Get(tipo, year).Value + 1; }
		public static long GetNextIDExportacion(ETipoRegistro tipo, int year) { return GetIDExportacion(tipo, year).Value + 1; }

		#endregion

		#region Root Data Access

		#endregion

		#region SQL

		public static string SELECT(ETipoRegistro tipo, int year)
		{
            string lr = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
            string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));
			string query;

			QueryConditions conditions = new QueryConditions
			{
				TipoRegistro = tipo,
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year),				
			};	

			query = "SELECT 0 AS \"OID\", MAX(LR.\"SERIAL\") AS \"SERIAL\"" +
					" FROM " + lr + " AS LR" +
					" INNER JOIN " + rg + " AS RG ON RG.\"OID\" = LR.\"OID_REGISTRO\"" +
					" WHERE TRUE";

			if (year != 0)
				query += " AND LR.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

			if (conditions.TipoRegistro != ETipoRegistro.Todos)
				query += " AND RG.\"TIPO_REGISTRO\" = " + (long)conditions.TipoRegistro;

			return query + ";";
		}
		public static string SELECT_EXPORTACION(ETipoRegistro tipo, int year)
		{
            string lr = nHManager.Instance.GetSQLTable(typeof(RegistryLineRecord));
            string rg = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));
			string query;

			QueryConditions conditions = new QueryConditions
			{
				TipoRegistro = tipo,
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year),
			};	

			query = "SELECT 0 AS \"OID\", TO_NUMBER(MAX(\"ID_EXPORTACION\"),'00000') AS \"SERIAL\"" +
					" FROM " + lr + " AS LR" +
					" INNER JOIN " + rg + " AS RG ON RG.\"OID\" = LR.\"OID_REGISTRO\"" +
					" WHERE TRUE";

			if (year != 0) 
				query += " AND LR.\"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

			if (conditions.TipoRegistro != ETipoRegistro.Todos) 
				query += " AND RG.\"TIPO_REGISTRO\" = " + (long)conditions.TipoRegistro;

			return query + ";";
		}

		#endregion

	}
}
