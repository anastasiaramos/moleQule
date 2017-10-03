using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Reports;
using moleQule.Library.Common.Reports.Registry;

namespace moleQule.Library.Common
{
    public class RegistryReportMng : BaseReportMng
    {
		#region Factory Methods

		public RegistryReportMng()
		{ }

		public RegistryReportMng(ISchemaInfo company)
            : this(company, string.Empty) { }

        public RegistryReportMng(ISchemaInfo company, string title)
            : this(company, title, string.Empty) { }

        public RegistryReportMng(ISchemaInfo company, string title, string filter)
            : base(company, title, filter) { }

		#endregion

        #region Business Methods

        public RegistryListRpt GetListReport(RegistroList list)
        {
            if (list.Count == 0) return null;

            RegistryListRpt doc = new RegistryListRpt();

            doc.SetDataSource(list);

            FormatHeader(doc);

            return doc;
        }

		public LineaRegistroListRpt GetListReport(LineaRegistroList list)
        {
            if (list.Count == 0) return null;

			LineaRegistroListRpt doc = new LineaRegistroListRpt();
            
            doc.SetDataSource(list);

			FormatHeader(doc);

            return doc;
        }

        public LineaRegistroFomentoListRpt GetListFomentoReport(LineaRegistroList list)
        {
            if (list.Count == 0) return null;

            LineaRegistroFomentoListRpt doc = new LineaRegistroFomentoListRpt();

            doc.SetDataSource(list);

            FormatHeader(doc);

            return doc;
        }

        #endregion
    }
}
