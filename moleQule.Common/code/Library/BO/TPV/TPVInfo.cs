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
	public class TPVInfo : ReadOnlyBaseEx<TPVInfo>
	{	
		#region Attributes

        public TPVBase _base = new TPVBase();

		#endregion
		
		#region Properties

		public override long Oid { get { return _base.Record.Oid; } set { _base.Record.Oid = value; } }
		public long OidCuentaBancaria { get { return _base.Record.OidCuentaBancaria; } }
		public string Nombre { get { return _base.Record.Nombre; } }
		public string CuentaContable { get { return _base.Record.CuentaContable; } }
		public decimal PComision { get { return _base.Record.PComision; } }
		public string Observaciones { get { return _base.Record.Observaciones; } }

        public string CuentaBancaria { get { return _base.CuentaBancaria; } }		
		
		#endregion
		
		#region Business Methods
						
		public void CopyFrom(TPV source) { _base.CopyValues(source); }
			
		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected TPVInfo() { /* require use of factory methods */ }
		private TPVInfo(int sessionCode, IDataReader reader, bool childs)
		{
			SessionCode = sessionCode;
			Childs = childs;
			Fetch(reader);
		}
		internal TPVInfo(TPV item, bool copy_childs)
		{
			_base.CopyValues(item);
			
			if (copy_childs)
			{
				
			}
		}

		public static TPVInfo GetChild(int sessionCode, IDataReader reader) { return GetChild(sessionCode, reader, false); }
		public static TPVInfo GetChild(int sessionCode, IDataReader reader, bool childs)
        {
			return new TPVInfo(sessionCode, reader, childs);
		}
		
 		#endregion
		
		#region Root Factory Methods
		
		/// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
        public static TPVInfo Get(long oid)
        {
            return Get(oid, false);
        }
		
        /// <summary>
        /// Obtiene un <see cref="ReadOnlyBaseEx"/> de la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
		/// <param name="get_childs">Flag para obtener los hijos de la bd</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/> construido a partir del registro</returns>
		public static TPVInfo Get(long oid, bool retrieve_childs)
		{
			CriteriaEx criteria = TPV.GetCriteria(TPV.OpenSession());
			criteria.Childs = retrieve_childs;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = TPVInfo.SELECT(oid);
			else
				criteria.AddOidSearch(oid);
	
			TPVInfo obj = DataPortal.Fetch<TPVInfo>(criteria);
			TPV.CloseSession(criteria.SessionCode);
			return obj;
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
					
				}
				else
				{
					_base.CopyValues((TPV)(criteria.UniqueResult()));
					
				}
			}
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
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
            }
            catch (Exception ex)
            {
                throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
            }
        }

        #endregion

        #region SQL

        public static string SELECT(long oid) { return TPV.SELECT(oid, false); }
		public static string SELECT(QueryConditions conditions) { return TPV.SELECT(conditions, false); }

        #endregion		
	}
}
