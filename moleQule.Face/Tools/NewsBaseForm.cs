using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Csla;
using moleQule.Library.CslaEx;
using moleQule.Library;
using moleQule.Library.Reports.Notification;

namespace moleQule.Face
{
	public partial class NewsBaseForm : Skin01.ActionSkinForm
	{

		#region Business Methods

		public const string ID = "NewsBaseForm";
		public static Type Type { get { return typeof(NewsBaseForm); } }

		public void SetNotification(string notification)
		{
			if (News_LB.Items.Count == 0) News_LB.Items.Add("");
			News_LB.Items.Add("    " + notification);
		}

        public void SetNotifications(List<string> notifications)
        {
            if (notifications == null) return;

            if (News_LB.Items.Count == 0) News_LB.Items.Add("");
            
            foreach (string item in notifications)
                News_LB.Items.Add("    " + item);
        }

		#endregion

		#region Factory Methods

        private NewsBaseForm() : this(null) {}

        public NewsBaseForm(List<string> notifications)
            : this(notifications, null) {}

        public NewsBaseForm(List<string> notifications, Form parent)
            : base(true, parent)
        {
            InitializeComponent();
            this.Text = Resources.Labels.NEWS_TITLE;
            SetNotifications(notifications);
        }

		#endregion

		#region Layout & Source

		/// <summary>Formatea los Controles del formulario
		/// <returns>void</returns>
		/// </summary>
		public override void FormatControls()
		{
            Print_BT.Visible = true;
            Print_BT.Enabled = true;
			base.FormatControls();
		}

		/// <summary>
		/// Asigna el objeto principal al origen de datos 
		/// <returns>void</returns>
		/// </summary>
		protected override void RefreshMainData()
		{
            bool show = SettingsMng.Instance.GetShowAutopilot();
			Mostrar_CB.Checked = !show;
		}

		#endregion

		#region Actions

		/// <summary>
		/// Implementa Save_button_Click
		/// </summary>
		protected override void SubmitAction()
		{
            SettingsMng.Instance.SetShowAutopilot(!Mostrar_CB.Checked);
            _action_result = DialogResult.OK;
		}

        protected override void PrintAction() 
        {
            NotificationReportMng reportMng = new NotificationReportMng(AppContext.ActiveSchema);

            List<string> lista = new List<string>();
            
            foreach (object item in News_LB.Items)
                lista.Add((string)item);

            NotificationListRpt report = reportMng.GetListReport(lista);

            if (report != null)
            {
                ReportViewer.SetReport(report);
                ReportViewer.ShowDialog();
            }
            else
            {
                MessageBox.Show(moleQule.Face.Resources.Messages.NO_DATA_REPORTS,
                                moleQule.Face.Resources.Labels.EMPTY_REPORT,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
            }
        }

		#endregion

        #region Events

        #endregion

    }
}