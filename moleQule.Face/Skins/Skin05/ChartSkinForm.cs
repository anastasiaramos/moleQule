using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using moleQule.Face;

namespace moleQule.Face.Skin05
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
			MaximizeForm(new Size(1280, 0));

			Datos_GB.Left = (Width - Datos_GB.Width) / 2;

			base.FormatControls();
		}

		public virtual void DetalladoAction() {}
		public virtual void AgrupadoAction() {}

		#endregion

		#region Events

		private void Detallado_RB_CheckedChanged(object sender, EventArgs e)
		{
			if (!Detallado_RB.Checked) return;
			DetalladoAction();
		}

		private void Agrupado_RB_CheckedChanged(object sender, EventArgs e)
		{
			if (!Agrupado_RB.Checked) return;
			AgrupadoAction();
		}

		private void TresD_RB_CheckedChanged(object sender, EventArgs e)
		{
			Chart.ChartAreas[0].Area3DStyle.Enable3D = true;
		}

		private void Plano_RB_CheckedChanged(object sender, EventArgs e)
		{
			Chart.ChartAreas[0].Area3DStyle.Enable3D = false;
		}

		#endregion

	}
}
