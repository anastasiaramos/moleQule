namespace moleQule.Face.Skin05
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
			this.Datos_GB = new System.Windows.Forms.GroupBox();
			this.Agrupado_RB = new System.Windows.Forms.RadioButton();
			this.Detallado_RB = new System.Windows.Forms.RadioButton();
			this.Estilo_GB = new System.Windows.Forms.GroupBox();
			this.TresD_RB = new System.Windows.Forms.RadioButton();
			this.Plano_RB = new System.Windows.Forms.RadioButton();
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
			this.Datos_GB.SuspendLayout();
			this.Estilo_GB.SuspendLayout();
			this.SuspendLayout();
			// 
			// Chart
			// 
			this.Chart.BorderSkin.BackColor = System.Drawing.Color.DarkGray;
			this.Chart.Size = new System.Drawing.Size(864, 463);
			// 
			// Grafica_Panel
			// 
			// 
			// Grafica_Panel.Panel2
			// 
			this.Grafica_Panel.Panel2.Controls.Add(this.Estilo_GB);
			this.Grafica_Panel.Panel2.Controls.Add(this.Datos_GB);
			this.Grafica_Panel.Panel2MinSize = 40;
			this.Grafica_Panel.SplitterDistance = 463;
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
			// Datos_GB
			// 
			this.Datos_GB.Controls.Add(this.Agrupado_RB);
			this.Datos_GB.Controls.Add(this.Detallado_RB);
			this.Datos_GB.Location = new System.Drawing.Point(144, 0);
			this.Datos_GB.Name = "Datos_GB";
			this.Datos_GB.Size = new System.Drawing.Size(245, 37);
			this.Datos_GB.TabIndex = 0;
			this.Datos_GB.TabStop = false;
			this.Datos_GB.Text = "Datos";
			// 
			// Agrupado_RB
			// 
			this.Agrupado_RB.AutoSize = true;
			this.Agrupado_RB.Location = new System.Drawing.Point(137, 14);
			this.Agrupado_RB.Name = "Agrupado_RB";
			this.Agrupado_RB.Size = new System.Drawing.Size(71, 17);
			this.Agrupado_RB.TabIndex = 1;
			this.Agrupado_RB.Text = "Agrupado";
			this.Agrupado_RB.UseVisualStyleBackColor = true;
			this.Agrupado_RB.CheckedChanged += new System.EventHandler(this.Agrupado_RB_CheckedChanged);
			// 
			// Detallado_RB
			// 
			this.Detallado_RB.AutoSize = true;
			this.Detallado_RB.Checked = true;
			this.Detallado_RB.Location = new System.Drawing.Point(36, 14);
			this.Detallado_RB.Name = "Detallado_RB";
			this.Detallado_RB.Size = new System.Drawing.Size(70, 17);
			this.Detallado_RB.TabIndex = 0;
			this.Detallado_RB.TabStop = true;
			this.Detallado_RB.Text = "Detallado";
			this.Detallado_RB.UseVisualStyleBackColor = true;
			this.Detallado_RB.CheckedChanged += new System.EventHandler(this.Detallado_RB_CheckedChanged);
			// 
			// Estilo_GB
			// 
			this.Estilo_GB.Controls.Add(this.TresD_RB);
			this.Estilo_GB.Controls.Add(this.Plano_RB);
			this.Estilo_GB.Location = new System.Drawing.Point(430, 0);
			this.Estilo_GB.Name = "Estilo_GB";
			this.Estilo_GB.Size = new System.Drawing.Size(245, 37);
			this.Estilo_GB.TabIndex = 1;
			this.Estilo_GB.TabStop = false;
			this.Estilo_GB.Text = "Estilo";
			// 
			// TresD_RB
			// 
			this.TresD_RB.AutoSize = true;
			this.TresD_RB.Checked = true;
			this.TresD_RB.Location = new System.Drawing.Point(153, 14);
			this.TresD_RB.Name = "TresD_RB";
			this.TresD_RB.Size = new System.Drawing.Size(39, 17);
			this.TresD_RB.TabIndex = 1;
			this.TresD_RB.TabStop = true;
			this.TresD_RB.Text = "3D";
			this.TresD_RB.UseVisualStyleBackColor = true;
			this.TresD_RB.CheckedChanged += new System.EventHandler(this.TresD_RB_CheckedChanged);
			// 
			// Plano_RB
			// 
			this.Plano_RB.AutoSize = true;
			this.Plano_RB.Location = new System.Drawing.Point(52, 14);
			this.Plano_RB.Name = "Plano_RB";
			this.Plano_RB.Size = new System.Drawing.Size(52, 17);
			this.Plano_RB.TabIndex = 0;
			this.Plano_RB.Text = "Plano";
			this.Plano_RB.UseVisualStyleBackColor = true;
			this.Plano_RB.CheckedChanged += new System.EventHandler(this.Plano_RB_CheckedChanged);
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
			this.Datos_GB.ResumeLayout(false);
			this.Datos_GB.PerformLayout();
			this.Estilo_GB.ResumeLayout(false);
			this.Estilo_GB.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

        public System.Windows.Forms.RadioButton Agrupado_RB;
        public System.Windows.Forms.RadioButton Detallado_RB;
		public System.Windows.Forms.RadioButton TresD_RB;
        public System.Windows.Forms.RadioButton Plano_RB;
        public System.Windows.Forms.GroupBox Datos_GB;
        public System.Windows.Forms.GroupBox Estilo_GB;

	}
}