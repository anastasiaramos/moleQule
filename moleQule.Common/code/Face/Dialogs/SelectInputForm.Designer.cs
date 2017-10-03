namespace moleQule.Face.Common
{
    partial class SelectInputForm
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.Tabla = new System.Windows.Forms.DataGridView();
			this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Comments = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.Source_GB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Tabla)).BeginInit();
			this.SuspendLayout();
			// 
			// Submit_BT
			// 
			this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Submit_BT.Location = new System.Drawing.Point(153, 7);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Cancel_BT.Location = new System.Drawing.Point(243, 7);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Source_GB
			// 
			this.Source_GB.Controls.Add(this.Tabla);
			this.Source_GB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Source_GB.Location = new System.Drawing.Point(0, 0);
			this.HelpProvider.SetShowHelp(this.Source_GB, true);
			this.Source_GB.Size = new System.Drawing.Size(482, 424);
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
			this.PanelesV.Size = new System.Drawing.Size(484, 466);
			this.PanelesV.SplitterDistance = 426;
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(38, 41);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(484, 466);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(210, 284);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(210, 199);
			// 
			// Tabla
			// 
			this.Tabla.AllowUserToAddRows = false;
			this.Tabla.AllowUserToDeleteRows = false;
			this.Tabla.AutoGenerateColumns = false;
			this.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Tabla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Numero,
            this.Nombre,
            this.Comments});
			this.Tabla.DataSource = this.Datos;
			this.Tabla.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Tabla.Location = new System.Drawing.Point(3, 17);
			this.Tabla.MultiSelect = false;
			this.Tabla.Name = "Tabla";
			this.Tabla.ReadOnly = true;
			this.Tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.Tabla.Size = new System.Drawing.Size(476, 404);
			this.Tabla.TabIndex = 0;
			this.Tabla.DoubleClick += new System.EventHandler(this.Tabla_DoubleClick);
			// 
			// Numero
			// 
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.Numero.DefaultCellStyle = dataGridViewCellStyle1;
			this.Numero.HeaderText = "Código";
			this.Numero.Name = "Numero";
			this.Numero.ReadOnly = true;
			this.Numero.Visible = false;
			// 
			// Nombre
			// 
			this.Nombre.HeaderText = "Nombre";
			this.Nombre.Name = "Nombre";
			this.Nombre.ReadOnly = true;
			// 
			// Comments
			// 
			this.Comments.HeaderText = "Observaciones";
			this.Comments.Name = "Comments";
			this.Comments.ReadOnly = true;
			this.Comments.Visible = false;
			// 
			// SelectInputForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(484, 466);
			this.ControlBox = false;
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "SelectInputForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.ShowInTaskbar = false;
			this.Text = "Seleccione un elemento";
			this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
			this.Controls.SetChildIndex(this.PanelesV, 0);
			this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
			this.Controls.SetChildIndex(this.Progress_PB, 0);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.Source_GB.ResumeLayout(false);
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
			this.PanelesV.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Tabla)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.DataGridView Tabla;
		protected System.Windows.Forms.DataGridViewTextBoxColumn Numero;
		protected System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
		protected System.Windows.Forms.DataGridViewTextBoxColumn Comments;

    }
}
