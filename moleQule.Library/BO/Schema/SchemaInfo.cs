using System;
using System.Collections;
using System.Data;

using NHibernate;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	/// <summary>
	/// ReadOnly MAIN Root Business Object with ReadOnly Childs
	/// </summary>
	[Serializable()]
	public class SchemaInfo : ReadOnlyBaseEx<SchemaInfo>, ISchemaInfo
	{
        #region ISchemaInfo

        private string _code;
        private string _name;
        private bool _principal;
        private bool _use_default_reports;

		public string Code { get { return _code; } set { _code = value; } }
        public string Name { get { return _name; } }
        public bool Principal { get { return _principal; } set { _principal = value; } }
        public bool UseDefaultReports { get { return _use_default_reports; } }

		public virtual string SchemaCode { get { return Convert.ToInt64(_code).ToString("0000"); } }

        #endregion

		#region Business Methods

		private long _serial;

		public long Serial { get { return _serial; } }

		protected void CopyValues(IDataReader source)
		{
			if (source == null) return;

			Oid = Format.DataReader.GetInt64(source, "OID");
			_serial = Format.DataReader.GetInt64(source, "SERIAL");
			_code = Format.DataReader.GetString(source, "CODE");
			_name = Format.DataReader.GetString(source, "NAME");
		}
		protected virtual void CopyValues(Schema source)
		{
			if (source == null) return;

			Oid = source.Oid;
			_serial = source.Serial;
			_code = source.Code;
			_name = source.Name;
            _use_default_reports = source.UseDefaultReports;
		}

        public System.Byte[] GetImage() { return null; }        

		#endregion

		#region Factory Methods

		private SchemaInfo() { /* require use of factory methods */ }
		internal SchemaInfo(Schema source)
		{
			CopyValues(source);
		}

		public static SchemaInfo Get(long oid)
		{
			CriteriaEx criteria = Schema.GetCriteria(Schema.OpenSession());
			criteria.Query = SELECT(typeof(Schema), oid);

			SchemaInfo obj = DataPortal.Fetch<SchemaInfo>(criteria);
			Schema.CloseSession(criteria.SessionCode);
			return obj;
		}

		public static ISchemaInfo GetISchemaInfo(long oid) { return (ISchemaInfo)SchemaInfo.Get(oid); }
		public static ISchema GetISchema(long oid) { return (ISchema)Schema.Get(oid); }

		public static SchemaInfo New(long oid = -1) { return new SchemaInfo() { Oid = oid }; }

		#endregion

		#region ISchemaInfo

		public ISchemaInfo IGet(long oid)
		{
			return (ISchemaInfo)SchemaInfo.Get(oid);
		}

		public ISchema IGetSchema(long oid)
		{
			return (ISchema)Schema.Get(oid);
		}

		#endregion

		#region Data Access

		private void DataPortal_Fetch(CriteriaEx criteria)
		{
			try
            {
                Oid = 0;
				SessionCode = criteria.SessionCode;
				Childs = criteria.Childs;

				IDataReader reader = nHMng.SQLNativeSelect(criteria.Query, Session());

				if (reader.Read())
					CopyValues(reader);
			}
			catch (Exception ex)
			{
				throw new iQPersistentException(iQExceptionHandler.GetAllMessages(ex));
			}
		}

		#endregion

  }
}