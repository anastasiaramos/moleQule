using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace moleQule.Face
{
	public partial class ChartBaseForm : ChildForm
	{
		#region Factory Methods

		public ChartBaseForm()
            : this(true) {}

		public ChartBaseForm(bool is_modal)
			: this(is_modal, null) { }

		public ChartBaseForm(bool is_modal, Form parent)
			: base(is_modal, parent)
        {
            InitializeComponent();
        }

		#endregion

		#region Layout

		public override void FormatControls()
		{
			base.FormatControls();

			int botones = 0, espacio = 10, tab, pos = 0, x = 0, y = 0;
			int formWidth = this.Width;
			int formHeight = Grafica_Panel.Panel2.Height;
			int controlWidth = 0;

			foreach (Control ctl in Grafica_Panel.Panel2.Controls)
			{
				if (ctl.GetType().Equals(typeof(GroupBox)) && ctl.Visible)
				{
					botones++;
					controlWidth += ctl.Width;
				}
			}

			// Centrado de los botones
			tab = (formWidth - espacio * (botones - 1) - controlWidth) / 2;
			foreach (Control ctl in Grafica_Panel.Panel2.Controls)
			{
				if (ctl.GetType().Equals(typeof(GroupBox)) && ctl.Visible)
				{
					x = tab;
					y = (formHeight - ctl.Height) / 2;

					ctl.SetBounds(x, y, ctl.Width, ctl.Height);
					pos++;

					tab = x + espacio + ctl.Width;
				}
			}

			Print_BT.Left = formWidth - Print_BT.Width - 50;
			Print_BT.Top = (formHeight - Print_BT.Height) / 2;
		}

		public void SetStyle(SeriesChartType style) { SetStyle(style, true); }

		protected virtual void SetStyle(SeriesChartType style, bool refresh) {}

		#endregion

		#region Actions

		public void PrintChartAction()
		{
			Color color = Chart.BackColor;
			Chart.BackColor = Color.White;

            Chart.Printing.PrintDocument.PrinterSettings.DefaultPageSettings.Landscape = true;
            Chart.Printing.Print(true);
            
			Chart.BackColor = color;
		}

		#endregion

		#region Events
		
		private void Print_BT_Click(object sender, EventArgs e)
		{
			PrintChartAction();
		}

		#endregion
	}
}
