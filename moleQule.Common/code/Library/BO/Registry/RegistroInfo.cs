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
	/// ReadOnly Root Object With Editable Child Collection
	/// ReadOnly Child Object With Editable Child Collection
	/// </summary>
	[Serializable()]
	public class RegistroInfo : ReadOnlyBaseEx<RegistroInfo>
	{	
		#region Attributes

		public RegistryBase _base = new RegistryBase();
		
		protected LineaRegistroList _lineas = null;

		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidUsuario { get { return _base.Record.OidUsuario; } }
		public long TipoRegistro { get { return _base.Record.TipoRegistro; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }
        public long TipoExportacion { get { return _base.Record.TipoExportacion; } }
		
		public LineaRegistroList LineaRegistros { get { return _lineas; } }

		//NO ENLAZADAS
		public string Usuario { get { return _base.Usuario; } }
		public virtual ETipoRegistro ETipoRegistro { get { return (ETipoRegistro)_base.Record.TipoRegistro; } set { _base.Record.TipoRegistro = (long)value; } }
		public virtual string TipoRegistroLabel { get { return Library.Common.EnumText<ETipoRegistro>.GetLabel(ETipoRegistro); } }
		public virtual EEstado EEstado { get { return (EEstado)_base.Record.Estado; } set { _base.Record.Estado = (long)value; } }
		public virtual string EstadoLabel { get { return Library.Common.EnumText<EEstado>.GetLabel(EEstado); } }
        public virtual ETipoExportacion ETipoExportacion { get { return (ETipoExportacion)_base.Record.TipoExportacion; } set { _base.Record.TipoExportacion = (long)value; } }
        public virtual string TipoExportacionLabel { get { return Library.Common.EnumText<ETipoExportacion>.GetLabel(ETipoExportacion); } }

		#endregion
		
		#region Business Methods
						
		public void CopyFrom(Registro source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected RegistroInfo() { /* require use of factory methods */ }
		private RegistroInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal RegistroInfo(Registro item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				_lineas = (item.LineaRegistros != null) ? LineaRegistroList.GetChildList(item.LineaRegistros) : null;				
			}
		}
	
		public static RegistroInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static RegistroInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new RegistroInfo(sessionCode, reader, childs);
		}
		
 		#endregion
		
		#region Root Factory Methods
		
		public static RegistroInfo Get(long oid, bool childs = false)
		{
			CriteriaEx criteria = Registro.GetCriteria(Registro.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = RegistroInfo.SELECT(oid);
	
			RegistroInfo obj = DataPortal.Fetch<RegistroInfo>(criteria);
			Registro.CloseSession(criteria.SessionCode);
			return obj;
		}
		
		#endregion
		
		#region Common Data Access
								
        /// <summary>
        /// Obtiene un objeto a partir de un <see cref="IDataReader"/>.
        /// Obtiene los hijos si los tiene y se solicitan
        /// </summary>
        /// <param name="criteria"><see cref="IDataReader"/> con los datos</param>
        /// <remarks>
        /// La utiliza el <see cref="ReadOnlyListBaseEx"/> correspondiente para construir los objetos de la lista
        /// </remarks>
		private void Fetch(IDataReader source)
		{
			try
			{
				_base.CopyValues(source);
				
				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;
					
					query = LineaRegistroList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
					_lineas = LineaRegistroList.GetChildList(SessionCode, reader);					
				}
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
		
		#region Root Data Access
		 
        /// <summary>
        /// Obtiene un registro de la base de datos
        /// </summary>
        /// <param name="criteria"><see cref="CriteriaEx"/> con los criterios</param>
        /// <remarks>
        /// La llama el DataPortal
        /// </remarks>
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
	                    
						query = LineaRegistroList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
						_lineas = LineaRegistroList.GetChildList(SessionCode, reader);						
                    }
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}
		
		#endregion
					
        #region SQL

        public static string SELECT(long oid) { return Registro.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return Registro.SELECT(conditions, false); }
		
        #endregion		
	}

	/// <summary>
	/// ReadOnly Root Object
	/// </summary>
	[Serializable()]
	public class SerialRegistroInfo : SerialInfo
	{
		#region Common Factory Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <remarks>
		///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
		/// </remarks>
		protected SerialRegistroInfo() { /* require use of factory methods */ }

		#endregion

		#region Root Factory Methods

		public static SerialRegistroInfo Get(ETipoRegistro tipo) { return Get(tipo, 0); }
		public static SerialRegistroInfo Get(ETipoRegistro tipo, int year)
		{
			CriteriaEx criteria = Registro.GetCriteria(Registro.OpenSession());
			criteria.Childs = false;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SELECT(tipo, year);

			SerialRegistroInfo obj = DataPortal.Fetch<SerialRegistroInfo>(criteria);
			Registro.CloseSession(criteria.SessionCode);
			return obj;
		}

		public static long GetNext(ETipoRegistro tipo) { return Get(tipo).Value + 1; }
		public static long GetNext(ETipoRegistro tipo, int year) { return Get(tipo, year).Value + 1; }

		#endregion

		#region Root Data Access

		#endregion

		#region SQL

		public static string SELECT(ETipoRegistro tipo, int year)
		{
            string p = nHManager.Instance.GetSQLTable(typeof(RegistryRecord));
			string a = string.Empty;
			string query = string.Empty;

			QueryConditions conditions = new QueryConditions 
			{ 
				TipoRegistro = tipo,
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};
			
			query = 
			"	SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
			"	FROM " + p + " AS P" +
			"	WHERE \"TIPO_REGISTRO\" = " + (long)conditions.TipoRegistro;

			if (year != 0)
				query += " AND \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

			return query + ";";
		}

		#endregion
	}
}
