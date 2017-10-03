using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using moleQule.Face;

namespace moleQule.Face
{
	public class ChartMng
	{
		#region Attributes & Properties
		
		Skin05.ChartSkinForm _chartForm;
		Form _parent;

		protected ChartParams _cParams = new ChartParams();

		public Chart Chart { get { return (_chartForm != null) ? _chartForm.Chart : null; } }
		public Skin05.ChartSkinForm ChartForm { get { return _chartForm; } }

		#endregion
		        
		#region Factory Methods

		public ChartMng(Form parent, Type chartFormType)
        {
			_parent = parent;
			_chartForm = new Skin05.ChartSkinForm(true, parent);
        }

		public Chart NewChart()
		{
			_chartForm.Chart.Series.Clear();
			_chartForm.Chart.Legends.Clear();

			return _chartForm.Chart;
		}

        #endregion

		#region Layout

		public void SetStyle(SeriesChartType style) { _chartForm.SetStyle(style); }

		#endregion

		#region Business Methods

		public virtual void ShowChart() 
		{
			try { _chartForm.ShowDialog(_parent); }
			catch {}
		}

		#endregion

    }
}