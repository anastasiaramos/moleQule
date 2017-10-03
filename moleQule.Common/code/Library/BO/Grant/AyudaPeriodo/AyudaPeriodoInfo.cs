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
	public class AyudaPeriodoInfo : ReadOnlyBaseEx<AyudaPeriodoInfo>
	{	
		#region Attributes

		public GrantPeriodoBase _base = new GrantPeriodoBase();

		
		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidAyuda { get { return _base.Record.OidAyuda; } }
		public long Estado { get { return _base.Record.Estado; } }
		public long TipoDescuento { get { return _base.Record.TipoDescuento; } }
		public Decimal Porcentaje { get { return _base.Record.Porcentaje; } }
		public Decimal Cantidad { get { return _base.Record.Cantidad; } }
		public DateTime FechaIni { get { return _base.Record.FechaIni; } }
		public DateTime FechaFin { get { return _base.Record.FechaFin; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }

		//NO ENLAZADOS	
		public virtual EEstado EEstado { get { return _base.EEstado; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }
		public virtual ETipoDescuento ETipoDescuento { get { return _base.ETipoDescuento; } }
		public virtual string TipoDescuentoLabel { get { return _base.TipoDescuentoLabel; } }	
		
		#endregion
		
		#region Business Methods
		
		protected void CopyValues(AyudaPeriodo source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_base.CopyValues(source);
		}
		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;
			
			Oid = Format.DataReader.GetInt64(source, "OID");
			_base.CopyValues(source);
		}
				
		public void CopyFrom(AyudaPeriodo source) { CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected AyudaPeriodoInfo() { /* require use of factory methods */ }
		private AyudaPeriodoInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal AyudaPeriodoInfo(AyudaPeriodo item, bool copy_childs)
		{
			CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}
		
		public static AyudaPeriodoInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static AyudaPeriodoInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new AyudaPeriodoInfo(sessionCode, reader, childs);
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
				CopyValues(source);
				
			}
            catch (Exception ex) { throw ex; }
		}
		
		#endregion
					
        #region SQL

        public static string SELECT(long oid) { return AyudaPeriodo.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return AyudaPeriodo.SELECT(conditions, false); }
		
		public static string SELECT(AyudaInfo item) { return SELECT(new Library.Common.QueryConditions { Ayuda = item }); }
			
		
        #endregion		
	}
}
