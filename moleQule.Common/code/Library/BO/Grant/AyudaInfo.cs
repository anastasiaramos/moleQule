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
	public class AyudaInfo : ReadOnlyBaseEx<AyudaInfo>
	{	
		#region Attributes

		public GrantBase _base = new GrantBase();

		protected AyudaPeriodoList _periodos = null;
		
		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long Serial { get { return _base.Record.Serial; } }
		public string Codigo { get { return _base.Record.Codigo; } }
		public long Estado { get { return _base.Record.Estado; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public string CuentaContable { get { return _base.Record.CuentaContable; } }
        public string Observaciones { get { return _base.Record.Observaciones; } }
		
		public AyudaPeriodoList Periodos { get { return _periodos; } }

		//NO ENLAZADOS	
		public virtual EEstado EEstado { get { return _base.EEstado; } }
		public virtual string EstadoLabel { get { return _base.EstadoLabel; } }	

		#endregion
		
		#region Business Methods
		
		protected void CopyValues(Ayuda source)
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
				
		public void CopyFrom(Ayuda source) { CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected AyudaInfo() { /* require use of factory methods */ }
		private AyudaInfo(int sessionCode, IDataReader reader, bool childs)
		{
			Childs = childs;
			SessionCode = sessionCode;
			Fetch(reader);
		}
		internal AyudaInfo(Ayuda item, bool copy_childs)
		{
			CopyValues(item);
			
			if (copy_childs)
			{
				_periodos = (item.Periodos != null) ? AyudaPeriodoList.GetChildList(item.Periodos) : null;
				
			}
		}
		
		public static AyudaInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static AyudaInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new AyudaInfo(sessionCode, reader, childs);
		}
		
 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        public static AyudaInfo Get(long oid) { return Get(oid, false); }
		public static AyudaInfo Get(long oid, bool childs)
		{
			CriteriaEx criteria = Ayuda.GetCriteria(Ayuda.OpenSession());
			criteria.Childs = childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = AyudaInfo.SELECT(oid);
	
			AyudaInfo obj = DataPortal.Fetch<AyudaInfo>(criteria);
			Ayuda.CloseSession(criteria.SessionCode);
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
				
				if (Childs)
				{
					string query = string.Empty;
					IDataReader reader;
					
					query = AyudaPeriodoList.SELECT(this);
                    reader = nHMng.SQLNativeSelect(query, Session());
                    _periodos = AyudaPeriodoList.GetChildList(SessionCode, reader);
					
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
						CopyValues(reader);
					
                    if (Childs)
					{
						string query = string.Empty;
	                    
						query = AyudaPeriodoList.SELECT(this);
                        reader = nHMng.SQLNativeSelect(query, Session());
                        _periodos = AyudaPeriodoList.GetChildList(SessionCode, reader);
						
                    }
				}
			}
            catch (Exception ex) { iQExceptionHandler.TreatException(ex); }
		}
		
		#endregion
					
        #region SQL

        public static string SELECT(long oid) { return Ayuda.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return Ayuda.SELECT(conditions, false); }
		
        #endregion		
	}
}
