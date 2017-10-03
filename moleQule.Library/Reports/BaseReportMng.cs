using System;
using System.Reflection;
using System.Runtime.Remoting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace moleQule.Library.Reports
{
    [Serializable()]
    public abstract class BaseReportMng
	{
		#region Attributes & Properties

		protected ISchemaInfo _schema = null;
        protected String _title = string.Empty;
        protected String _filter = "Todos";

		public ISchemaInfo Schema { get { return _schema; } }
        public String Title { get { return _title; } }
        public String Filter { get { return _filter; } }
		
		#endregion

		#region Factory Methods

		public BaseReportMng() : this (null) {}

		public BaseReportMng(ISchemaInfo schema)
            : this(schema, string.Empty) {}

        public BaseReportMng(ISchemaInfo schema, string title)
            : this(schema, title, string.Empty) {}

        public BaseReportMng(ISchemaInfo schema, string title, string filter)
        {
            _schema = schema;
            _title = title;
            _filter = filter;
        }

		#endregion

		#region Business Methods

		protected virtual ReportClass GetReportFromName(string folder, string className)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			ObjectHandle object_handle = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, "moleQule.Library.Reports." + folder + ".s" + AppContext.ActiveSchema.SchemaCode + "." + className);
			return (ReportClass)object_handle.Unwrap();
		}

		#endregion

		#region Style

		protected void FormatHeader(ReportClass report)
        {
            report.SetParameterValue("Title", Title);
            report.SetParameterValue("Empresa", Schema.Name);
            report.SetParameterValue("Filter", Filter);
        }

        #endregion
    }
}