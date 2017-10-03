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
	/// ReadOnly Root Object
	/// ReadOnly Child Object
	/// </summary>
	[Serializable()]
	public class PesajeInfo : ReadOnlyBaseEx<PesajeInfo>
	{	
		#region Attributes

		public WeighingBase _base = new WeighingBase();
		
		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Estado { get { return _base.Record.Estado; } }
		public DateTime Fecha { get { return _base.Record.Fecha; } }
		public string Descripcion { get { return _base.Record.Descripcion; } }
		public Decimal Bruto { get { return _base.Record.Bruto; } }
		public Decimal Neto { get { return _base.Record.Neto; } }
		public Decimal Tara { get { return _base.Record.Tara; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }		
		
		#endregion
		
		#region Business Methods
		
		protected void CopyValues(Pesaje source)
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
				
		public void CopyFrom(Pesaje source) { CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected PesajeInfo() { /* require use of factory methods */ }
		private PesajeInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal PesajeInfo(Pesaje item, bool copy_childs)
		{
			CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}
		
		public static PesajeInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static PesajeInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new PesajeInfo(sessionCode, reader, childs);
		}
		
 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        public static PesajeInfo Get(long oid) { return Get(oid, false); }
		public static PesajeInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = Pesaje.GetCriteria(Pesaje.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = PesajeInfo.SELECT(oid);
	
			PesajeInfo obj = DataPortal.Fetch<PesajeInfo>(criteria);
			Pesaje.CloseSession(criteria.SessionCode);
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
				CopyValues(source);
				
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
						CopyValues(reader);
					
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}
		
		#endregion
					
        #region SQL

        public static string SELECT(long oid) { return Pesaje.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return Pesaje.SELECT(conditions, false); }
		
        #endregion		
	}
}
