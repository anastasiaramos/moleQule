using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using moleQule.Library;

namespace moleQule.Face.Skin04
{
    public partial class ReportSkinForm : moleQule.Face.ActionBaseForm
    {        
        #region Attributes & Properties

		ChartSkinForm _chartForm;
		protected ChartParams _cParams = new ChartParams();

		protected Chart Chart { get { return (_chartForm != null) ?_chartForm.Chart : null; } }

        #endregion

        #region Factory Methods

		public ReportSkinForm()
            : this(true) {}

		public ReportSkinForm(bool is_modal)
			: this(is_modal, null) { }

		public ReportSkinForm(bool is_modal, Form parent)
			: base(is_modal, parent)
        {
            InitializeComponent();

			_chartForm = new ChartSkinForm(true, this);
        }

        #endregion

        #region Layout

        public override void FormatControls()
        {
			base.FormatControls();

            Source_GB.SendToBack();

			PanelesV.SplitterDistance = PanelesV.Height - PanelesV.Panel2MinSize - PanelesV.SplitterWidth;
			ControlsMng.CenterButtons(PanelesV.Panel2);
        }

        #endregion

		#region Business Methods

		protected virtual void ShowChart() 
		{
			_chartForm.ShowDialog(this);
		}

		protected Chart NewChart()
		{
			_chartForm.Chart.Series.Clear();
			_chartForm.Chart.Legends.Clear();

			return _chartForm.Chart;
		}

		protected virtual string GetFilterValues() { throw new iQImplementationException("ReportSkinForm::GetFilterValues()"); }

		#endregion

        #region Buttons

        private void Aceptar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Submit); }

        private void Cancelar_Button_Click(object sender, EventArgs e) { ExecuteAction(molAction.Cancel); }

        private void Print_BT_Click(object sender, EventArgs e) { ExecuteAction(molAction.Print); }

        #endregion
                
        #region Events

        #endregion
    }

}

