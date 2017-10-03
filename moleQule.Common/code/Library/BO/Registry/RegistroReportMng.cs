using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Reports;
using moleQule.Library.Application;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class InformeReportMng : BaseReportMng
    {

        #region Business Methods Informe
		
        public InformeRpt GetDetailReport(InformeInfo item)
        {
            if (item == null) return null;
			
            InformeRpt doc = new InformeRpt();
            
            List<InformePrint> pList = new List<InformePrint>();

            pList.Add(InformePrint.New(item));
            doc.SetDataSource(pList);
			doc.SetParameterValue("Empresa", Schema.Name);
			
			
			List<LineaInformePrint> pLineaInformes = new List<LineaInformePrint>();
            
			foreach (LineaInformeInfo child in item.LineaInformes)
			{
				pLineaInformes.Add(LineaInformePrint.New(child));
			}

			doc.Subreports["LineaInformeSubRpt"].SetDataSource(pLineaInformes);
			

            //FormatReport(doc, empresa.Logo);

            return doc;
        }

		public InformeListRpt GetListReport(InformeList list)
		{
			if (list.Count == 0) return null;

			InformeListRpt doc = new ClienteListRpt();

			List<InformePrint> pList = new List<InformePrint>();
			
			foreach (InformeInfo item in list)
			{
				pList.Add(InformePrint.New(item));;
			}
			
			doc.SetDataSource(pList);
			
			FormatHeader(doc);

			return doc;
		}
		
        #endregion

        #region Factory Methods

        public InformeReportMng() {}

        public InformeReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) { }

        public InformeReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) {}

        public InformeReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }
			
        #endregion
        
        #region Style

        /*private static void FormatReport(InformeRpt rpt, string logo)
        {
            /*string path = Images.GetRootPath() + "\\" + Resources.Paths.LOGO_EMPRESAS + logo;

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

    }
}
