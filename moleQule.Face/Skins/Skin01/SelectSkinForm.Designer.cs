namespace moleQule.Face.Skin01
{
    partial class SelectSkinForm
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
			this.PanelesV = new System.Windows.Forms.SplitContainer();
			this.Paneles2 = new System.Windows.Forms.SplitContainer();
			this.Submit_BT = new System.Windows.Forms.Button();
			this.Cancel_BT = new System.Windows.Forms.Button();
			this.BarraEstado_ST = new System.Windows.Forms.StatusStrip();
			this.Info_SL = new System.Windows.Forms.ToolStripStatusLabel();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).BeginInit();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.Panel2.SuspendLayout();
			this.Paneles2.SuspendLayout();
			this.BarraEstado_ST.SuspendLayout();
			this.SuspendLayout();
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(192, 74);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(792, 366);
			// 
			// PanelesV
			// 
			this.PanelesV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PanelesV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelesV.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.PanelesV.IsSplitterFixed = true;
			this.PanelesV.Location = new System.Drawing.Point(0, 0);
			this.PanelesV.Name = "PanelesV";
			this.PanelesV.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// PanelesV.Panel2
			// 
			this.PanelesV.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.PanelesV.Panel2.Controls.Add(this.Paneles2);
			this.PanelesV.Panel2MinSize = 39;
			this.PanelesV.Size = new System.Drawing.Size(792, 366);
			this.PanelesV.SplitterDistance = 326;
			this.PanelesV.SplitterWidth = 1;
			this.PanelesV.TabIndex = 104;
			this.PanelesV.TabStop = false;
			// 
			// Paneles2
			// 
			this.Paneles2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Paneles2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.Paneles2.IsSplitterFixed = true;
			this.Paneles2.Location = new System.Drawing.Point(0, 0);
			this.Paneles2.Name = "Paneles2";
			this.Paneles2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// Paneles2.Panel1
			// 
			this.Paneles2.Panel1.Controls.Add(this.Submit_BT);
			this.Paneles2.Panel1.Controls.Add(this.Cancel_BT);
			this.Paneles2.Panel1MinSize = 36;
			// 
			// Paneles2.Panel2
			// 
			this.Paneles2.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Paneles2.Panel2.Controls.Add(this.BarraEstado_ST);
			this.Paneles2.Panel2MinSize = 0;
			this.Paneles2.Size = new System.Drawing.Size(790, 37);
			this.Paneles2.SplitterDistance = 36;
			this.Paneles2.SplitterWidth = 1;
			this.Paneles2.TabIndex = 200;
			this.Paneles2.TabStop = false;
			// 
			// Submit_BT
			// 
			this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Submit_BT.Location = new System.Drawing.Point(177, 6);
			this.Submit_BT.Name = "Submit_BT";
			this.Submit_BT.Size = new System.Drawing.Size(87, 23);
			this.Submit_BT.TabIndex = 200;
			this.Submit_BT.Text = "&Seleccionar";
			this.Submit_BT.UseVisualStyleBackColor = true;
			this.Submit_BT.Click += new System.EventHandler(this.Aceptar_Button_Click);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Cancel_BT.Location = new System.Drawing.Point(270, 6);
			this.Cancel_BT.Name = "Cancel_BT";
			this.Cancel_BT.Size = new System.Drawing.Size(87, 23);
			this.Cancel_BT.TabIndex = 205;
			this.Cancel_BT.Text = "&Cancelar";
			this.Cancel_BT.UseVisualStyleBackColor = true;
			this.Cancel_BT.Click += new System.EventHandler(this.Cancelar_Button_Click);
			// 
			// BarraEstado_ST
			// 
			this.BarraEstado_ST.BackColor = System.Drawing.SystemColors.Control;
			this.BarraEstado_ST.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.BarraEstado_ST.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Info_SL});
			this.BarraEstado_ST.Location = new System.Drawing.Point(0, -22);
			this.BarraEstado_ST.Name = "BarraEstado_ST";
			this.BarraEstado_ST.Size = new System.Drawing.Size(790, 22);
			this.BarraEstado_ST.TabIndex = 0;
			// 
			// Info_SL
			// 
			this.Info_SL.Name = "Info_SL";
			this.Info_SL.Size = new System.Drawing.Size(45, 17);
			this.Info_SL.Text = "Info_SL";
			// 
			// SelectSkinForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(792, 366);
			this.Controls.Add(this.PanelesV);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "SelectSkinForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "SelectSkinForm";
			this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
			this.Controls.SetChildIndex(this.PanelesV, 0);
			this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
			this.Controls.SetChildIndex(this.Progress_PB, 0);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.PanelesV.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			this.Paneles2.Panel2.ResumeLayout(false);
			this.Paneles2.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).EndInit();
			this.Paneles2.ResumeLayout(false);
			this.BarraEstado_ST.ResumeLayout(false);
			this.BarraEstado_ST.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer PanelesV;
        protected System.Windows.Forms.SplitContainer Paneles2;
        protected System.Windows.Forms.Button Submit_BT;
        protected System.Windows.Forms.Button Cancel_BT;
        private System.Windows.Forms.StatusStrip BarraEstado_ST;
        protected System.Windows.Forms.ToolStripStatusLabel Info_SL;

    }
}
