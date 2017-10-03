namespace moleQule.Face.Common
{
    partial class TPVUIForm
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TPVUIForm));
			this.Datos_DGW = new System.Windows.Forms.DataGridView();
			this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CuentaBancaria = new System.Windows.Forms.DataGridViewButtonColumn();
			this.CuentaContable = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PComision = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DatosLocal_BS = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).BeginInit();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Contenido_Panel)).BeginInit();
			this.Contenido_Panel.Panel2.SuspendLayout();
			this.Contenido_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_DGW)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DatosLocal_BS)).BeginInit();
			this.SuspendLayout();
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(844, 472);
			this.PanelesV.SplitterDistance = 431;
			// 
			// Submit_BT
			// 
			this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Submit_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Submit_BT.Location = new System.Drawing.Point(258, 6);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Cancel_BT.Location = new System.Drawing.Point(336, 6);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Paneles2
			// 
			// 
			// Paneles2.Panel1
			// 
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, true);
			// 
			// Paneles2.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, true);
			this.HelpProvider.SetShowHelp(this.Paneles2, true);
			this.Paneles2.Size = new System.Drawing.Size(842, 38);
			this.Paneles2.SplitterDistance = 34;
			// 
			// Print_BT
			// 
			this.Print_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Print_BT.Location = new System.Drawing.Point(180, 6);
			this.HelpProvider.SetShowHelp(this.Print_BT, true);
			// 
			// Contenido_Panel
			// 
			// 
			// Contenido_Panel.Panel2
			// 
			this.Contenido_Panel.Panel2.Controls.Add(this.Datos_DGW);
			this.Contenido_Panel.Size = new System.Drawing.Size(842, 429);
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(213, 114);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(844, 472);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(385, 284);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(385, 199);
			// 
			// Datos_DGW
			// 
			this.Datos_DGW.AllowUserToAddRows = false;
			this.Datos_DGW.AllowUserToDeleteRows = false;
			this.Datos_DGW.AllowUserToOrderColumns = true;
			this.Datos_DGW.AutoGenerateColumns = false;
			this.Datos_DGW.ColumnHeadersHeight = 35;
			this.Datos_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.CuentaBancaria,
            this.CuentaContable,
            this.PComision,
            this.Observaciones});
			this.Datos_DGW.DataSource = this.DatosLocal_BS;
			this.Datos_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Datos_DGW.Location = new System.Drawing.Point(0, 0);
			this.Datos_DGW.Name = "Datos_DGW";
			this.Datos_DGW.RowHeadersWidth = 25;
			this.Datos_DGW.Size = new System.Drawing.Size(842, 386);
			this.Datos_DGW.TabIndex = 2;
			this.Datos_DGW.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Datos_DG_CellClick);
			this.Datos_DGW.DoubleClick += new System.EventHandler(this.Datos_DG_DoubleClick);
			// 
			// Nombre
			// 
			this.Nombre.DataPropertyName = "Nombre";
			this.Nombre.HeaderText = "Nombre";
			this.Nombre.MinimumWidth = 150;
			this.Nombre.Name = "Nombre";
			this.Nombre.Width = 150;
			// 
			// CuentaBancaria
			// 
			this.CuentaBancaria.DataPropertyName = "CuentaBancaria";
			this.CuentaBancaria.HeaderText = "Cuenta Bancaria";
			this.CuentaBancaria.Name = "CuentaBancaria";
			this.CuentaBancaria.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.CuentaBancaria.Width = 175;
			// 
			// CuentaContable
			// 
			this.CuentaContable.DataPropertyName = "CuentaContable";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			this.CuentaContable.DefaultCellStyle = dataGridViewCellStyle1;
			this.CuentaContable.HeaderText = "Cuenta Contable";
			this.CuentaContable.Name = "CuentaContable";
			// 
			// PComision
			// 
			this.PComision.DataPropertyName = "PComision";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.Format = "N2";
			dataGridViewCellStyle2.NullValue = null;
			this.PComision.DefaultCellStyle = dataGridViewCellStyle2;
			this.PComision.HeaderText = "% Comisión";
			this.PComision.MinimumWidth = 60;
			this.PComision.Name = "PComision";
			this.PComision.Width = 60;
			// 
			// Observaciones
			// 
			this.Observaciones.DataPropertyName = "Observaciones";
			this.Observaciones.HeaderText = "Observaciones";
			this.Observaciones.MinimumWidth = 260;
			this.Observaciones.Name = "Observaciones";
			this.Observaciones.Width = 260;
			// 
			// DatosLocal_BS
			// 
			this.DatosLocal_BS.DataSource = typeof(moleQule.Library.Common.TPV);
			// 
			// TPVUIForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.ClientSize = new System.Drawing.Size(844, 472);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TPVUIForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "TPV";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TPVUIForm_FormClosing);
			this.Shown += new System.EventHandler(this.TPVUIForm_Shown);
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).EndInit();
			this.Paneles2.ResumeLayout(false);
			this.Contenido_Panel.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Contenido_Panel)).EndInit();
			this.Contenido_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_DGW)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DatosLocal_BS)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource DatosLocal_BS;
		public System.Windows.Forms.DataGridView Datos_DGW;
		private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
		private System.Windows.Forms.DataGridViewButtonColumn CuentaBancaria;
		private System.Windows.Forms.DataGridViewTextBoxColumn CuentaContable;
		private System.Windows.Forms.DataGridViewTextBoxColumn PComision;
		private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
    }
}
