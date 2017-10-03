using System;
using System.Reflection;
using System.Runtime.Remoting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace moleQule.Library.Common.Reports
{
    [Serializable()]
    public abstract class ReportMng : moleQule.Library.Reports.BaseReportMng
	{
		#region Attributes & Properties
		
		#endregion

		#region Factory Methods

		public ReportMng() : this (null) {}

		public ReportMng(ISchemaInfo schema)
            : this(schema, string.Empty) {}

        public ReportMng(ISchemaInfo schema, string title)
            : this(schema, title, string.Empty) {}

		public ReportMng(ISchemaInfo schema, string title, string filter)
			: base(schema, title, filter) {}

		#endregion

		#region Business Methods

		protected override ReportClass GetReportFromName(string folder, string className)
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			ObjectHandle object_handle = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, "moleQule.Library.Common.Reports." + folder + ".s" + AppContext.ActiveSchema.SchemaCode + "." + className);
			return (ReportClass)object_handle.Unwrap();
		}

		#endregion
    }

}