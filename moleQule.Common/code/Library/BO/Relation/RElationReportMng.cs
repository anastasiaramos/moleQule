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
    public class RElationReportMng : BaseReportMng
    {
	
        #region Factory Methods

        public RElationReportMng() {}

        public RElationReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public RElationReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public RElationReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(RElationRpt rpt, string logo)
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

        #region Business Methods RElation
		
        public RElationRpt GetDetailReport(RElationInfo item)
        {
            if (item == null) return null;
			
            RElationRpt doc = new RElationRpt();
            
            List<RElationPrint> pList = new List<RElationPrint>();

            pList.Add(RElationPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public RElationListRpt GetListReport(RElationList list)
		{
			if (list.Count == 0) return null;

			RElationListRpt doc = new ClienteListRpt();

			List<RElationPrint> pList = new List<RElationPrint>();
			
			foreach (RElationInfo item in list)
			{
				pList.Add(RElationPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion

    }
}
