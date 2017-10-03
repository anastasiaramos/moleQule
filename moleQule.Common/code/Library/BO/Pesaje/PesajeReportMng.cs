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
    public class PesajeReportMng : BaseReportMng
    {
	
        #region Factory Methods

        public PesajeReportMng() {}

        public PesajeReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public PesajeReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public PesajeReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(PesajeRpt rpt, string logo)
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

        #region Business Methods Pesaje
		
        public PesajeRpt GetDetailReport(PesajeInfo item)
        {
            if (item == null) return null;
			
            PesajeRpt doc = new PesajeRpt();
            
            List<PesajePrint> pList = new List<PesajePrint>();

            pList.Add(PesajePrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public PesajeListRpt GetListReport(PesajeList list)
		{
			if (list.Count == 0) return null;

			PesajeListRpt doc = new ClienteListRpt();

			List<PesajePrint> pList = new List<PesajePrint>();
			
			foreach (PesajeInfo item in list)
			{
				pList.Add(PesajePrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion

    }
}
