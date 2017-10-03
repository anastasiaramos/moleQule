using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using moleQule.Face;

namespace moleQule.Face.Skin04
{
	public partial class ChartSkinForm : ChartBaseForm
	{
		#region Factory Methods

		public ChartSkinForm()
            : this(true) {}

		public ChartSkinForm(bool is_modal)
			: this(is_modal, null) { }

		public ChartSkinForm(bool is_modal, Form parent)
			: base(is_modal, parent)
        {
            InitializeComponent();
        }

		#endregion

		#region Layout

		public override void FormatControls()
		{
			MaximizeForm(new Size(0, 0));

			Estilo_GB.Left = (Width - Estilo_GB.Width) / 2;

			base.FormatControls();
		}

		protected override void SetStyle(SeriesChartType style, bool refresh)
		{
			foreach (Series sItem in Chart.Series)
				sItem.ChartType = style;

			if (refresh)
			{
				switch (style)
				{
					case SeriesChartType.Spline: Lineas_RB.Checked = true; break;
					case SeriesChartType.Column: Columnas_RB.Checked = true; break;
				}
			}
		}

		#endregion

		#region Events

		private void Lineas_RB_CheckedChanged(object sender, EventArgs e)
		{
			if (!Lineas_RB.Checked) return;

			SetStyle(SeriesChartType.Spline, false);
		}

		private void Columnas_RB_CheckedChanged(object sender, EventArgs e)
		{
			if (!Columnas_RB.Checked) return;
			
			SetStyle(SeriesChartType.Column, false);
		}

		#endregion
	}
}
