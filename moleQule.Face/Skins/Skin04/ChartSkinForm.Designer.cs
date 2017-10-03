namespace moleQule.Face.Skin04
{
	partial class ChartSkinForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartSkinForm));
			this.Estilo_GB = new System.Windows.Forms.GroupBox();
			this.Columnas_RB = new System.Windows.Forms.RadioButton();
			this.Lineas_RB = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.Chart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Grafica_Panel)).BeginInit();
			this.Grafica_Panel.Panel1.SuspendLayout();
			this.Grafica_Panel.Panel2.SuspendLayout();
			this.Grafica_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			this.Estilo_GB.SuspendLayout();
			this.SuspendLayout();
			// 
			// Chart
			// 
			this.Chart.BorderSkin.BackColor = System.Drawing.Color.DarkGray;
			this.Chart.Size = new System.Drawing.Size(864, 472);
			// 
			// Grafica_Panel
			// 
			// 
			// Grafica_Panel.Panel2
			// 
			this.Grafica_Panel.Panel2.Controls.Add(this.Estilo_GB);
			this.Grafica_Panel.SplitterDistance = 472;
			// 
			// CancelBkJob_BT
			// 
			this.CancelBkJob_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.CancelBkJob_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(228, 144);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(864, 507);
			// 
			// Estilo_GB
			// 
			this.Estilo_GB.Controls.Add(this.Columnas_RB);
			this.Estilo_GB.Controls.Add(this.Lineas_RB);
			this.Estilo_GB.Location = new System.Drawing.Point(259, 0);
			this.Estilo_GB.Name = "Estilo_GB";
			this.Estilo_GB.Size = new System.Drawing.Size(347, 32);
			this.Estilo_GB.TabIndex = 0;
			this.Estilo_GB.TabStop = false;
			this.Estilo_GB.Text = "Estilo";
			// 
			// Columnas_RB
			// 
			this.Columnas_RB.AutoSize = true;
			this.Columnas_RB.Checked = true;
			this.Columnas_RB.Location = new System.Drawing.Point(174, 11);
			this.Columnas_RB.Name = "Columnas_RB";
			this.Columnas_RB.Size = new System.Drawing.Size(71, 17);
			this.Columnas_RB.TabIndex = 1;
			this.Columnas_RB.TabStop = true;
			this.Columnas_RB.Text = "Columnas";
			this.Columnas_RB.UseVisualStyleBackColor = true;
			this.Columnas_RB.CheckedChanged += new System.EventHandler(this.Columnas_RB_CheckedChanged);
			// 
			// Lineas_RB
			// 
			this.Lineas_RB.AutoSize = true;
			this.Lineas_RB.Location = new System.Drawing.Point(101, 11);
			this.Lineas_RB.Name = "Lineas_RB";
			this.Lineas_RB.Size = new System.Drawing.Size(58, 17);
			this.Lineas_RB.TabIndex = 0;
			this.Lineas_RB.Text = "Líneas";
			this.Lineas_RB.UseVisualStyleBackColor = true;
			this.Lineas_RB.CheckedChanged += new System.EventHandler(this.Lineas_RB_CheckedChanged);
			// 
			// ChartSkinForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(864, 507);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ChartSkinForm";
			this.HelpProvider.SetShowHelp(this, true);
			((System.ComponentModel.ISupportInitialize)(this.Chart)).EndInit();
			this.Grafica_Panel.Panel1.ResumeLayout(false);
			this.Grafica_Panel.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Grafica_Panel)).EndInit();
			this.Grafica_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.Estilo_GB.ResumeLayout(false);
			this.Estilo_GB.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox Estilo_GB;
		public System.Windows.Forms.RadioButton Columnas_RB;
		public System.Windows.Forms.RadioButton Lineas_RB;

	}
}