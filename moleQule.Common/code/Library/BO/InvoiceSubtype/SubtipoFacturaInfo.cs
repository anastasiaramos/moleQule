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
	public class SubtipoFacturaInfo : ReadOnlyBaseEx<SubtipoFacturaInfo>
	{	
		#region Attributes

		public InvoiceSubtypeBase _base = new InvoiceSubtypeBase();
		
		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public string Descripcion { get { return _base.Record.Descripcion; } }
        public long Tipo { get { return _base.Record.Tipo; } }

        public ESubtipoFactura ETipo { get { return (ESubtipoFactura)_base.Record.Tipo; } }
        public string ETipoLabel{get{return EnumText<ESubtipoFactura>.GetLabel(ETipo);} }		
		
		#endregion
		
		#region Business Methods
		
		protected void CopyValues(SubtipoFactura source)
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
				
		public void CopyFrom(SubtipoFactura source) { CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected SubtipoFacturaInfo() { /* require use of factory methods */ }
		private SubtipoFacturaInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal SubtipoFacturaInfo(SubtipoFactura item, bool copy_childs)
		{
			CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}
		
		public static SubtipoFacturaInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static SubtipoFacturaInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new SubtipoFacturaInfo(sessionCode, reader, childs);
		}
		
 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        public static SubtipoFacturaInfo Get(long oid) { return Get(oid, false); }
		public static SubtipoFacturaInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = SubtipoFactura.GetCriteria(SubtipoFactura.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SubtipoFacturaInfo.SELECT(oid);
	
			SubtipoFacturaInfo obj = DataPortal.Fetch<SubtipoFacturaInfo>(criteria);
			SubtipoFactura.CloseSession(criteria.SessionCode);
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

        public static string SELECT(long oid) { return SubtipoFactura.SELECT(oid, false); }
        public static string SELECT(QueryConditions conditions) { return SubtipoFactura.SELECT(conditions, false); }
		
        #endregion		
	}
}
