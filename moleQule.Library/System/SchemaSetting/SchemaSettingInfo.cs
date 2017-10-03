using System;
using System.Data;
using System.Collections.Generic;

using NHibernate;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	[Serializable()]
	public class SchemaSettingInfo : ReadOnlyBaseEx<SchemaSettingInfo>
	{
		#region Business Methods

        public SchemaSettingBase _base = new SchemaSettingBase();

		public override long Oid { get { return _base.Record.Oid; } }
		public virtual string Name { get { return _base.Record.Name; } }
        public virtual string Value { get { return _base.Record.Value; } }

		public override string ToString() {	return _base.Record.Name; }

		/// <summary>
		/// Copia los atributos del objeto
		/// </summary>
		/// <param name="_s">Objeto origen</param>
		protected virtual void CopyValues(SchemaSetting source)
		{
			if (source == null) return;

			Oid = source.Oid;
            _base.CopyValues(source);
		}
        protected void CopyValues(IDataReader source)
        {
			Oid = Format.DataReader.GetInt64(source, "OID");
            _base.CopyValues(source);
        }

		#endregion

		#region Factory Methods

		private SchemaSettingInfo()	{ /* require use of factory methods */ }

		internal SchemaSettingInfo(SchemaSetting schema)
		{
			CopyValues(schema);
		}

		public static SchemaSettingInfo Get(string nombre)
		{
			CriteriaEx criteria = SchemaSetting.GetCriteria(SchemaSetting.OpenSession());
            
            criteria.Query = SchemaSetting.SELECT_BY_NAME(nombre);

            SchemaSettingInfo obj = DataPortal.Fetch<SchemaSettingInfo>(criteria);
			SchemaSetting.CloseSession(criteria.SessionCode);
			
            return obj;
		}

        #endregion

        #region Data Access

        private void DataPortal_Fetch(CriteriaEx criteria)
        {
            _base.Record.Oid = 0;
			SessionCode = criteria.SessionCode;
			Childs = criteria.Childs;

			try
            {               
				if (nHMng.UseDirectSQL)
                {
                    IDataReader reader = null;

                    reader = nHMng.SQLNativeSelect(criteria.Query, Session());

                    if (reader.Read())
                        CopyValues(reader);
                }
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		#endregion

        #region SQL

        public static string SELECT_BY_NAME(string name) { return SchemaSetting.SELECT_BY_NAME(name, false); }

        #endregion
	}
}
