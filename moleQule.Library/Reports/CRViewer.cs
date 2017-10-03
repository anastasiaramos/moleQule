using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using CrystalDecisions.CrystalReports.Engine;

namespace moleQule.Library.Reports
{
	public partial class CRViewer : Form
	{
		public CRViewer()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Asigna el informe a visualizar
		/// </summary>
		/// <param name="report">Informe a visualizar</param>
		public void SetReport(ReportClass report)
		{
			Visor.ReportSource = report;
		}
	}
}