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
    public class AyudaReportMng : BaseReportMng
    {
	
        #region Factory Methods

        public AyudaReportMng() {}

        public AyudaReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public AyudaReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public AyudaReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(AyudaRpt rpt, string logo)
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

        #region Business Methods Ayuda
		
        public AyudaRpt GetDetailReport(AyudaInfo item)
        {
            if (item == null) return null;
			
            AyudaRpt doc = new AyudaRpt();
            
            List<AyudaPrint> pList = new List<AyudaPrint>();

            pList.Add(AyudaPrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			
			List<AyudaPeriodoPrint> pAyudaPeriodos = new List<AyudaPeriodoPrint>();
            
			foreach (AyudaPeriodoInfo child in item.AyudaPeriodos)
			{
				pAyudaPeriodos.Add(AyudaPeriodoPrint.New(child));
			}

			doc.Subreports["AyudaPeriodoSubRpt"].SetDataSource(pAyudaPeriodos);
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public AyudaListRpt GetListReport(AyudaList list)
		{
			if (list.Count == 0) return null;

			AyudaListRpt doc = new ClienteListRpt();

			List<AyudaPrint> pList = new List<AyudaPrint>();
			
			foreach (AyudaInfo item in list)
			{
				pList.Add(AyudaPrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion

    }
}
