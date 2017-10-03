namespace moleQule.Face.Common
{
	partial class CurrencyExchangeUIForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrencyExchangeUIForm));
			this.Datos_DGW = new System.Windows.Forms.DataGridView();
			this.DatosLocal_BS = new System.Windows.Forms.BindingSource(this.components);
			this.Datos_Tipo = new System.Windows.Forms.BindingSource(this.components);
			this.FromCurrency = new System.Windows.Forms.DataGridViewButtonColumn();
			this.ToCurrency = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
			((System.ComponentModel.ISupportInitialize)(this.Datos_Tipo)).BeginInit();
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
			this.PanelesV.Size = new System.Drawing.Size(728, 472);
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
			this.Paneles2.Size = new System.Drawing.Size(726, 38);
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
			this.Contenido_Panel.Size = new System.Drawing.Size(726, 429);
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(155, 114);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(728, 472);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(327, 284);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(327, 199);
			// 
			// Datos_DGW
			// 
			this.Datos_DGW.AllowUserToAddRows = false;
			this.Datos_DGW.AllowUserToDeleteRows = false;
			this.Datos_DGW.AllowUserToOrderColumns = true;
			this.Datos_DGW.AutoGenerateColumns = false;
			this.Datos_DGW.ColumnHeadersHeight = 35;
			this.Datos_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FromCurrency,
            this.ToCurrency,
            this.Rate,
            this.Comments});
			this.Datos_DGW.DataSource = this.DatosLocal_BS;
			this.Datos_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Datos_DGW.Location = new System.Drawing.Point(0, 0);
			this.Datos_DGW.Name = "Datos_DGW";
			this.Datos_DGW.RowHeadersWidth = 25;
			this.Datos_DGW.Size = new System.Drawing.Size(726, 386);
			this.Datos_DGW.TabIndex = 2;
			this.Datos_DGW.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Datos_DG_CellClick);
			this.Datos_DGW.DoubleClick += new System.EventHandler(this.Datos_DG_DoubleClick);
			// 
			// DatosLocal_BS
			// 
			this.DatosLocal_BS.DataSource = typeof(moleQule.Library.Common.CurrencyExchange);
			// 
			// Datos_Tipo
			// 
			this.Datos_Tipo.DataSource = typeof(moleQule.Library.ComboBoxSource);
			// 
			// FromCurrency
			// 
			this.FromCurrency.DataPropertyName = "FromCurrency";
			this.FromCurrency.HeaderText = "Moneda Fuente";
			this.FromCurrency.Name = "FromCurrency";
			this.FromCurrency.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.FromCurrency.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.FromCurrency.Width = 150;
			// 
			// ToCurrency
			// 
			this.ToCurrency.DataPropertyName = "ToCurrency";
			this.ToCurrency.HeaderText = "Moneda Destino";
			this.ToCurrency.Name = "ToCurrency";
			this.ToCurrency.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.ToCurrency.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.ToCurrency.Width = 150;
			// 
			// Relation
			// 
			this.Rate.DataPropertyName = "Rate";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
			dataGridViewCellStyle1.Format = "N5";
			dataGridViewCellStyle1.NullValue = null;
			this.Rate.DefaultCellStyle = dataGridViewCellStyle1;
			this.Rate.HeaderText = "Tasa Cambio";
			this.Rate.Name = "Rate";
			this.Rate.Width = 80;
			// 
			// Comments
			// 
			this.Comments.DataPropertyName = "Comments";
			this.Comments.HeaderText = "Observaciones";
			this.Comments.Name = "Comments";
			this.Comments.Width = 300;
			// 
			// CurrencyExchangeUIForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.ClientSize = new System.Drawing.Size(728, 472);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CurrencyExchangeUIForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Tarjetas de Crédito";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CurrencyExchangeUIForm_FormClosing);
			this.Shown += new System.EventHandler(this.CurrencyExchangeUIForm_Shown);
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
			((System.ComponentModel.ISupportInitialize)(this.Datos_Tipo)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource DatosLocal_BS;
		public System.Windows.Forms.DataGridView Datos_DGW;
		protected System.Windows.Forms.BindingSource Datos_Tipo;
		private System.Windows.Forms.DataGridViewButtonColumn FromCurrency;
		private System.Windows.Forms.DataGridViewButtonColumn ToCurrency;
		private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
		private System.Windows.Forms.DataGridViewTextBoxColumn Comments;
    }
}
