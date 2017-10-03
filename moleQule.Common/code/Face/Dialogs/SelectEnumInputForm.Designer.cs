namespace moleQule.Face.Common
{
	partial class SelectEnumInputForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectEnumInputForm));
			this.Tabla = new System.Windows.Forms.DataGridView();
			this.Texto = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.Source_GB.SuspendLayout();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Tabla)).BeginInit();
			this.SuspendLayout();
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
			// 
			// Submit_BT
			// 
			this.Submit_BT.Location = new System.Drawing.Point(53, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(143, 7);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Source_GB
			// 
			this.Source_GB.Controls.Add(this.Tabla);
			this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Source_GB.Location = new System.Drawing.Point(0, 0);
			this.HelpProvider.SetShowHelp(this.Source_GB, true);
			this.Source_GB.Size = new System.Drawing.Size(282, 274);
			this.Source_GB.Text = "";
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.AutoScroll = true;
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(284, 316);
			this.PanelesV.SplitterDistance = 276;
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Location = new System.Drawing.Point(-67, 74);
			// 
			// Tabla
			// 
			this.Tabla.AllowUserToAddRows = false;
			this.Tabla.AllowUserToDeleteRows = false;
			this.Tabla.AutoGenerateColumns = false;
			this.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Texto});
			this.Tabla.DataSource = this.Datos;
			this.Tabla.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Tabla.Location = new System.Drawing.Point(3, 17);
			this.Tabla.MultiSelect = false;
			this.Tabla.Name = "Tabla";
			this.Tabla.ReadOnly = true;
			this.Tabla.RowHeadersWidth = 25;
			this.Tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.Tabla.Size = new System.Drawing.Size(276, 254);
			this.Tabla.TabIndex = 0;
			this.Tabla.DoubleClick += new System.EventHandler(this.Tabla_DoubleClick);
			// 
			// Texto
			// 
			this.Texto.DataPropertyName = "Texto";
			this.Texto.HeaderText = "Texto";
			this.Texto.Name = "Texto";
			this.Texto.ReadOnly = true;
			this.Texto.Width = 220;
			// 
			// SelectEnumInputForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(284, 316);
			this.ControlBox = false;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectEnumInputForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.ShowInTaskbar = false;
			this.Text = "Seleccione un elemento";
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.Source_GB.ResumeLayout(false);
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.ProgressBK_Panel.ResumeLayout(false);
			this.ProgressBK_Panel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Tabla)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.DataGridView Tabla;
		private System.Windows.Forms.DataGridViewTextBoxColumn Texto;

    }
}
