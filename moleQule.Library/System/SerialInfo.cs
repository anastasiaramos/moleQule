using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Globalization;

using Csla;
using moleQule.Library.CslaEx; 
using NHibernate;

using moleQule.Library.Resources;

namespace moleQule.Library
{
	/// <summary>
	/// ReadOnly Root Object
	/// </summary>
	[Serializable()]
    public class SerialInfo : ReadOnlyBaseEx<SerialInfo>
	{
		#region Attributes
		
		protected long _value = 0;
        
        #endregion
		
		#region Properties

		public long Value { get { return _value; } set { _value = value; } }

		#endregion
		
		#region Business Methods
		
		/// <summary>
		/// Copia los atributos del objeto
		/// </summary>
		/// <param name="source">Objeto origen</param>
		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

            Oid = Format.DataReader.GetInt64(source, "OID");
            _value = Format.DataReader.GetInt64(source, "SERIAL");
		}

        public static void Check(Type entity, long oid, string code)
        {
            long serial = 0;

            try
            {
                serial = Convert.ToInt64(code);
            }
            catch
            {
                throw new iQValidationException(String.Format(Resources.Errors.INVALID_CODE, code));
            }

            Check(entity, oid, serial);
        }

        public static void Check(Type entity, long oid, long code)
        {
            SerialInfo sInfo = Get(entity, code);
            if ((sInfo.Oid != 0) && (sInfo.Oid != oid))
                throw new iQDuplicateCodeException(code.ToString());
        }

		#endregion		
		
		#region Common Factory Methods
		
		/// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>
        ///  NO UTILIZAR DIRECTAMENTE. Object creation require use of factory methods
        /// </remarks>
		protected SerialInfo() { /* require use of factory methods */ }
		
 		#endregion
		
		#region Root Factory Methods
		
        /// <summary>
        /// Obtiene el último serial para una entidad desde la base de datos
        /// </summary>
        /// <param name="oid">Oid del objeto</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
		public static SerialInfo Get(Type entity, int sessionCode = -1)
		{
			bool sharedSession = (sessionCode != -1);

			if (sessionCode == -1) sessionCode = nHManager.Instance.OpenSession();

			CriteriaEx criteria = nHManager.Instance.GetCriteria(entity, sessionCode);
			criteria.Childs = false;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SerialInfo.SELECT(entity);
	
			SerialInfo obj = DataPortal.Fetch<SerialInfo>(criteria);
			if (!sharedSession) nHManager.Instance.CloseSession(criteria.SessionCode);
			return obj;
		}

		/// <summary>
		/// Obtiene el registro con el serial solicitado
		/// </summary>
		/// <param name="oid">Oid del objeto</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
		public static SerialInfo Get(Type entity, long serial)
		{
			CriteriaEx criteria = nHManager.Instance.GetCriteria(entity, nHManager.Instance.OpenSession());
			criteria.Childs = false;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SerialInfo.SELECT(entity, serial);

			SerialInfo obj = DataPortal.Fetch<SerialInfo>(criteria);
			nHManager.Instance.CloseSession(criteria.SessionCode);
			return obj;
		}

		public static SerialInfo GetByYear(Type entity, int year)
		{
			CriteriaEx criteria = nHManager.Instance.GetCriteria(entity, nHManager.Instance.OpenSession());
			criteria.Childs = false;

			if (nHManager.Instance.UseDirectSQL)
				criteria.Query = SerialInfo.SELECT_BY_YEAR(entity, year);

			SerialInfo obj = DataPortal.Fetch<SerialInfo>(criteria);
			nHManager.Instance.CloseSession(criteria.SessionCode);
			return obj;
		}

        /// <summary>
        /// Obtiene el siguiente serial para una entidad desde la base de datos
        /// </summary>
        /// <param name="entity">Tipo de Entidad</param>
        /// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
        public static long GetNext(Type entity, int sessionCode = -1)
        {
			return Get(entity, sessionCode).Value + 1;
        }

		/// <summary>
		/// Obtiene el siguiente serial para una entidad desde la base de datos
		/// </summary>
		/// <param name="entity">Tipo de Entidad</param>
		/// <returns>Objeto <see cref="ReadOnlyBaseEx"/>Construido a partir del registro</returns>
		public static long GetNextByYear(Type entity, int year)
		{
			return GetByYear(entity, year).Value + 1;
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
            Oid = 0;
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
            catch (Exception ex)
            {
                if (Transaction() != null) Transaction().Rollback();
                iQExceptionHandler.TreatException(ex);
            }
		}
		
		#endregion

        #region SQL

        public static string SELECT(Type entityType)
        {
			Type entityRecordType = AppControllerBase.AppControler.RecordEntities[entityType];

			string tabla = nHManager.Instance.GetSQLTable(entityRecordType);
            string query;

            query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
                    " FROM " + tabla + " AS T";
 
            return query;
        }

		public static string SELECT_BY_YEAR(Type entityType, int year)
		{
			Type entityRecordType = AppControllerBase.AppControler.RecordEntities[entityType];

			string table = nHManager.Instance.GetSQLTable(entityRecordType);
            string query = string.Empty;

			QueryConditions conditions = new QueryConditions
			{
				FechaIni = DateAndTime.FirstDay(year),
				FechaFin = DateAndTime.LastDay(year)
			};

			query = "SELECT 0 AS \"OID\", MAX(\"SERIAL\") AS \"SERIAL\"" +
					" FROM " + table + " AS T" +
					" WHERE \"FECHA\" BETWEEN '" + conditions.FechaIniLabel + "' AND '" + conditions.FechaFinLabel + "'";

			return query;
		}

		public new static string SELECT(Type entityType, long serial)
        {
			Type entityRecordType = AppControllerBase.AppControler.RecordEntities[entityType];

			string table = nHManager.Instance.GetSQLTable(entityRecordType);
            string query;

            query = "SELECT \"OID\", \"SERIAL\"" +
                    " FROM " + table + " AS T" +
                    " WHERE \"SERIAL\" = " + serial;

            return query;
        }

        #endregion		
	}
}
