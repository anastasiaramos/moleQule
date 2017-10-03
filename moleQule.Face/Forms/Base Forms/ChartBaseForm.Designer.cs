namespace moleQule.Face
{
	partial class ChartBaseForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
			System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
			System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
			System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartBaseForm));
			this.Grafica_Panel = new System.Windows.Forms.SplitContainer();
			this.Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.Print_BT = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Grafica_Panel.Panel1.SuspendLayout();
			this.Grafica_Panel.Panel2.SuspendLayout();
			this.Grafica_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Chart)).BeginInit();
			this.SuspendLayout();
			// 
			// Grafica_Panel
			// 
			this.Grafica_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Grafica_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.Grafica_Panel.Location = new System.Drawing.Point(0, 0);
			this.Grafica_Panel.Name = "Grafica_Panel";
			this.Grafica_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// Grafica_Panel.Panel1
			// 
			this.Grafica_Panel.Panel1.Controls.Add(this.Chart);
			// 
			// Grafica_Panel.Panel2
			// 
			this.Grafica_Panel.Panel2.Controls.Add(this.Print_BT);
			this.Grafica_Panel.Panel2MinSize = 34;
			this.Grafica_Panel.Size = new System.Drawing.Size(864, 507);
			this.Grafica_Panel.SplitterDistance = 472;
			this.Grafica_Panel.SplitterWidth = 1;
			this.Grafica_Panel.TabIndex = 23;
			// 
			// Chart
			// 
			this.Chart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
			this.Chart.BorderSkin.BackColor = System.Drawing.Color.DarkGray;
			chartArea1.Area3DStyle.Enable3D = true;
			chartArea1.Area3DStyle.Inclination = 10;
			chartArea1.Area3DStyle.Rotation = 10;
			chartArea1.AxisX.IsLabelAutoFit = false;
			chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			chartArea1.AxisX.LineColor = System.Drawing.Color.DarkGray;
			chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
			chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gray;
			chartArea1.AxisX.MajorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Gray;
			chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisX.MinorTickMark.LineColor = System.Drawing.Color.Gray;
			chartArea1.AxisX.MinorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisX.ScaleBreakStyle.Enabled = true;
			chartArea1.AxisX.ScaleBreakStyle.LineColor = System.Drawing.Color.Silver;
			chartArea1.AxisX.ScaleBreakStyle.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisX.Title = "Titulo del Eje X";
			chartArea1.AxisX2.LineColor = System.Drawing.Color.Silver;
			chartArea1.AxisX2.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisX2.ScaleBreakStyle.LineColor = System.Drawing.Color.Silver;
			chartArea1.AxisY.IsLabelAutoFit = false;
			chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			chartArea1.AxisY.LabelStyle.Format = "N2";
			chartArea1.AxisY.LineColor = System.Drawing.Color.DarkGray;
			chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
			chartArea1.AxisY.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gray;
			chartArea1.AxisY.MajorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.LightGray;
			chartArea1.AxisY.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisY.MinorTickMark.LineColor = System.Drawing.Color.Gray;
			chartArea1.AxisY.MinorTickMark.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisY.ScaleBreakStyle.LineColor = System.Drawing.Color.LightGray;
			chartArea1.AxisY.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated270;
			chartArea1.AxisY.Title = "Titulo del Eje Y";
			chartArea1.AxisY2.LineColor = System.Drawing.Color.Silver;
			chartArea1.AxisY2.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
			chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Silver;
			chartArea1.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.Silver;
			chartArea1.AxisY2.MinorGrid.LineColor = System.Drawing.Color.Silver;
			chartArea1.AxisY2.MinorTickMark.LineColor = System.Drawing.Color.Silver;
			chartArea1.AxisY2.ScaleBreakStyle.LineColor = System.Drawing.Color.Silver;
			chartArea1.BackColor = System.Drawing.Color.White;
			chartArea1.BorderColor = System.Drawing.Color.DarkGray;
			chartArea1.Name = "ChartArea";
			this.Chart.ChartAreas.Add(chartArea1);
			this.Chart.Dock = System.Windows.Forms.DockStyle.Fill;
			legend1.BackColor = System.Drawing.Color.White;
			legend1.BorderColor = System.Drawing.Color.Silver;
			legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Column;
			legend1.Name = "Título de la Leyenda";
			legend1.Title = "Leyenda";
			legend1.TitleFont = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Chart.Legends.Add(legend1);
			this.Chart.Location = new System.Drawing.Point(0, 0);
			this.Chart.Name = "Chart";
			series1.BorderWidth = 2;
			series1.ChartArea = "ChartArea";
			series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
			series1.IsValueShownAsLabel = true;
			series1.Legend = "Título de la Leyenda";
			series1.Name = "Series";
			this.Chart.Series.Add(series1);
			this.Chart.Size = new System.Drawing.Size(864, 472);
			this.Chart.TabIndex = 2;
			this.Chart.Text = "Ingresos";
			title1.Font = new System.Drawing.Font("Eras Demi ITC", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			title1.Name = "Title";
			title1.Text = "Título de la Gráfica";
			this.Chart.Titles.Add(title1);
			// 
			// Print_BT
			// 
			this.Print_BT.Font = new System.Drawing.Font("Tahoma", 8.5F, System.Drawing.FontStyle.Bold);
			this.Print_BT.Location = new System.Drawing.Point(742, 2);
			this.Print_BT.Name = "Print_BT";
			this.Print_BT.Size = new System.Drawing.Size(110, 32);
			this.Print_BT.TabIndex = 0;
			this.Print_BT.Text = "Imprimir";
			this.Print_BT.UseVisualStyleBackColor = true;
			this.Print_BT.Click += new System.EventHandler(this.Print_BT_Click);
			// 
			// ChartBaseForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(864, 507);
			this.Controls.Add(this.Grafica_Panel);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ChartBaseForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Informe Gráfico";
			this.Controls.SetChildIndex(this.Grafica_Panel, 0);
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Grafica_Panel.Panel1.ResumeLayout(false);
			this.Grafica_Panel.Panel2.ResumeLayout(false);
			this.Grafica_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Chart)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.DataVisualization.Charting.Chart Chart;
		protected System.Windows.Forms.SplitContainer Grafica_Panel;
		private System.Windows.Forms.Button Print_BT;



	}
}