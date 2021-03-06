namespace moleQule.Face.Common
{
    partial class CargoUIForm
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
			this.Datos_Grid = new System.Windows.Forms.DataGridView();
			this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).BeginInit();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Grid)).BeginInit();
			this.SuspendLayout();
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.AutoScroll = true;
			this.PanelesV.Panel1.Controls.Add(this.Datos_Grid);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(546, 266);
			this.PanelesV.SplitterDistance = 225;
			// 
			// Submit_BT
			// 
			this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Submit_BT.Location = new System.Drawing.Point(251, 6);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Cancel_BT.Location = new System.Drawing.Point(341, 6);
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
			this.Paneles2.Size = new System.Drawing.Size(544, 38);
			this.Paneles2.SplitterDistance = 37;
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Imprimir_Button.Location = new System.Drawing.Point(161, 6);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Docs_BT.Location = new System.Drawing.Point(286, 4);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Library.Common.Cargo);
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(64, 96);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(546, 266);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(236, 181);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(236, 96);
			// 
			// Datos_Grid
			// 
			this.Datos_Grid.AllowUserToOrderColumns = true;
			this.Datos_Grid.AutoGenerateColumns = false;
			this.Datos_Grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Valor});
			this.Datos_Grid.DataSource = this.Datos;
			this.Datos_Grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Datos_Grid.Location = new System.Drawing.Point(0, 0);
			this.Datos_Grid.Name = "Datos_Grid";
			this.Datos_Grid.Size = new System.Drawing.Size(544, 223);
			this.Datos_Grid.TabIndex = 0;
			// 
			// valorDataGridViewTextBoxColumn
			// 
			this.Valor.DataPropertyName = "Valor";
			this.Valor.HeaderText = "Valor";
			this.Valor.Name = "valorDataGridViewTextBoxColumn";
			// 
			// CargoUIForm
			// 
			this.ClientSize = new System.Drawing.Size(546, 266);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "CargoUIForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Cargos";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CargoUIForm_FormClosing);
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).EndInit();
			this.Paneles2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Grid)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView Datos_Grid;
		private System.Windows.Forms.DataGridViewTextBoxColumn Valor;

    }
}
