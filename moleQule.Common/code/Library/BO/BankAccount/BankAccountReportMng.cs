using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.Library.Common.Reports.BankAccount;
using moleQule.Library.Reports;

namespace moleQule.Library.Common
{
    public class BankAccountReportMng : BaseReportMng
    {
		#region Factory Methods

		public BankAccountReportMng()
		{ }

		public BankAccountReportMng(ISchemaInfo empresa)
			: this(empresa, string.Empty) { }

		public BankAccountReportMng(ISchemaInfo empresa, string title)
			: this(empresa, title, string.Empty) { }

		public BankAccountReportMng(ISchemaInfo empresa, string title, string filter)
			: base(empresa, title, filter) { }

		#endregion

        #region Business Methods

		public ReportClass GetListReport(BankAccountList list)
        {
            if (list.Count == 0) return null;

            BankAccountListRpt doc = new BankAccountListRpt();

            /*foreach (CuentaBancariaInfo item in list)
            {
                pList.Add(ClientePrint.New(item));
            }*/

            doc.SetDataSource(list);

			FormatHeader(doc);

            return doc;
        }

        #endregion
    }
}
