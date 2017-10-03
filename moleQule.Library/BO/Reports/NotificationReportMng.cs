using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using moleQule.Library.Reports;
using moleQule.Library.Reports.Notification;

namespace moleQule.Library
{
    public class NotificationReportMng : BaseReportMng
    {
        #region Business Methods

        public NotificationListRpt GetListReport(List<string> list)
        {
            if (list.Count == 0) return null;

            NotificationListRpt doc = new NotificationListRpt();

            List<NotificationPrint> pList = new List<NotificationPrint>();

            foreach (string item in list)
                pList.Add(NotificationPrint.New(item));

            doc.SetDataSource(pList);
            doc.SetParameterValue("Empresa", Schema.Name);

            return doc;
        }

        #endregion

        #region Factory Methods

        public NotificationReportMng() {}

        public NotificationReportMng(ISchemaInfo empresa)
            : this(empresa, string.Empty) {}

        public NotificationReportMng(ISchemaInfo empresa, string title)
            : this(empresa, title, string.Empty) { }

        public NotificationReportMng(ISchemaInfo empresa, string title, string filter)
            : base(empresa, title, filter) { }

        #endregion
    }
}
