namespace moleQule.Face.Common
{
    partial class ImpuestoUIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImpuestoUIForm));
            this.Datos_DG = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Porcentaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoImpuestoA3Emitida = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CodigoImpuestoA3Recibida = new System.Windows.Forms.DataGridViewButtonColumn();
            this.CuentaContableRepercutido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CuentaContableSoportado = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_DG)).BeginInit();
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
            this.PanelesV.Size = new System.Drawing.Size(928, 472);
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
            this.Paneles2.Size = new System.Drawing.Size(926, 38);
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
            this.Contenido_Panel.Panel2.Controls.Add(this.Datos_DG);
            this.Contenido_Panel.Size = new System.Drawing.Size(926, 429);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(255, 114);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(928, 472);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(427, 284);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(427, 199);
            // 
            // Datos_DG
            // 
            this.Datos_DG.AllowUserToAddRows = false;
            this.Datos_DG.AllowUserToDeleteRows = false;
            this.Datos_DG.AllowUserToOrderColumns = true;
            this.Datos_DG.AutoGenerateColumns = false;
            this.Datos_DG.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.Porcentaje,
            this.CodigoImpuestoA3Emitida,
            this.CodigoImpuestoA3Recibida,
            this.CuentaContableRepercutido,
            this.CuentaContableSoportado,
            this.Observaciones});
            this.Datos_DG.DataSource = this.DatosLocal_BS;
            this.Datos_DG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Datos_DG.Location = new System.Drawing.Point(0, 0);
            this.Datos_DG.Name = "Datos_DG";
            this.Datos_DG.Size = new System.Drawing.Size(926, 386);
            this.Datos_DG.TabIndex = 2;
            this.Datos_DG.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Datos_DG_CellContentClick);
            this.Datos_DG.DoubleClick += new System.EventHandler(this.Datos_DG_DoubleClick);
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "Nombre";
            this.Nombre.HeaderText = "Identificador";
            this.Nombre.Name = "Nombre";
            this.Nombre.Width = 130;
            // 
            // Porcentaje
            // 
            this.Porcentaje.DataPropertyName = "Porcentaje";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Porcentaje.DefaultCellStyle = dataGridViewCellStyle1;
            this.Porcentaje.HeaderText = "%";
            this.Porcentaje.Name = "Porcentaje";
            this.Porcentaje.Width = 50;
            // 
            // CodigoImpuestoA3Emitida
            // 
            this.CodigoImpuestoA3Emitida.DataPropertyName = "CodigoImpuestoA3Emitida";
            this.CodigoImpuestoA3Emitida.HeaderText = "Impuesto A3 Emitidas";
            this.CodigoImpuestoA3Emitida.Name = "CodigoImpuestoA3Emitida";
            this.CodigoImpuestoA3Emitida.ReadOnly = true;
            this.CodigoImpuestoA3Emitida.Width = 75;
            // 
            // CodigoImpuestoA3Recibida
            // 
            this.CodigoImpuestoA3Recibida.DataPropertyName = "CodigoImpuestoA3Recibida";
            this.CodigoImpuestoA3Recibida.HeaderText = "Impuesto A3 Recibidas";
            this.CodigoImpuestoA3Recibida.Name = "CodigoImpuestoA3Recibida";
            this.CodigoImpuestoA3Recibida.ReadOnly = true;
            this.CodigoImpuestoA3Recibida.Width = 75;
            // 
            // CuentaContableRepercutido
            // 
            this.CuentaContableRepercutido.DataPropertyName = "CuentaContableRepercutido";
            this.CuentaContableRepercutido.HeaderText = "Cuenta Contable (Repercutido)";
            this.CuentaContableRepercutido.Name = "CuentaContableRepercutido";
            this.CuentaContableRepercutido.Width = 120;
            // 
            // CuentaContableSoportado
            // 
            this.CuentaContableSoportado.DataPropertyName = "CuentaContableSoportado";
            this.CuentaContableSoportado.HeaderText = "Cuenta Contable (Soportado)";
            this.CuentaContableSoportado.Name = "CuentaContableSoportado";
            this.CuentaContableSoportado.Width = 120;
            // 
            // Observaciones
            // 
            this.Observaciones.DataPropertyName = "Observaciones";
            this.Observaciones.HeaderText = "Observaciones";
            this.Observaciones.Name = "Observaciones";
            this.Observaciones.Width = 300;
            // 
            // DatosLocal_BS
            // 
            this.DatosLocal_BS.DataSource = typeof(moleQule.Library.Common.Impuesto);
            // 
            // ImpuestoUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.ClientSize = new System.Drawing.Size(928, 472);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ImpuestoUIForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Impuestos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImpuestoUIForm_FormClosing);
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_DG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DatosLocal_BS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource DatosLocal_BS;
        public System.Windows.Forms.DataGridView Datos_DG;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Porcentaje;
        private System.Windows.Forms.DataGridViewButtonColumn CodigoImpuestoA3Emitida;
        private System.Windows.Forms.DataGridViewButtonColumn CodigoImpuestoA3Recibida;
        private System.Windows.Forms.DataGridViewTextBoxColumn CuentaContableRepercutido;
        private System.Windows.Forms.DataGridViewTextBoxColumn CuentaContableSoportado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
    }
}
