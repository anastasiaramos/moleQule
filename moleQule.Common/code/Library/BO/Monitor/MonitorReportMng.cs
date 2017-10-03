using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class MonitorReportMng : BaseReportMng
    {
	
        #region Factory Methods

        public MonitorReportMng() {}

        public MonitorReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public MonitorReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public MonitorReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(MonitorRpt rpt, string logo)
        {
            string path = Images.GetRootPath() + "\\" + Resources.Paths.LOGO_EMPRESAS + logo;

            if (File.Exists(path))
            {
                Image image = Image.FromFile(path);
                int width = rpt.Section1.ReportObjects["Logo"].Width;
                int height = rpt.Section1.ReportObjects["Logo"].Height;

                rpt.Section1.ReportObjects["Logo"].Width = 15 * image.Width;
                rpt.Section1.ReportObjects["Logo"].Height = 15 * image.Height;
                rpt.Section1.ReportObjects["Logo"].Left += (width - 15 * image.Width) / 2;
                rpt.Section1.ReportObjects["Logo"].Top += (height - 15 * image.Height) / 2;
            }
        }*/

        #endregion

        #region Business Methods Monitor
		
        public MonitorRpt GetDetailReport(MonitorInfo item)
        {
            if (item == null) return null;
			
            MonitorRpt doc = new MonitorRpt();
            
            List<MonitorPrint> pList = new List<MonitorPrint>();

            pList.Add(MonitorPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public MonitorListRpt GetListReport(MonitorList list)
		{
			if (list.Count == 0) return null;

			MonitorListRpt doc = new ClienteListRpt();

			List<MonitorPrint> pList = new List<MonitorPrint>();
			
			foreach (MonitorInfo item in list)
			{
				pList.Add(MonitorPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion

    }
}
