namespace moleQule.Face.Skin06
{
    partial class ListMngSkinForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListMngSkinForm));
			this.Buttons_SC = new System.Windows.Forms.SplitContainer();
			this.Content_SC = new System.Windows.Forms.SplitContainer();
			this.Tool_Strip = new System.Windows.Forms.ToolStrip();
			this.Add_TI = new System.Windows.Forms.ToolStripButton();
			this.Delete_TI = new System.Windows.Forms.ToolStripButton();
			this.Select_TI = new System.Windows.Forms.ToolStripButton();
			this.Separator4_TI = new System.Windows.Forms.ToolStripSeparator();
			this.Print_TI = new System.Windows.Forms.ToolStripButton();
			this.Separator2_TI = new System.Windows.Forms.ToolStripSeparator();
			this.Close_TI = new System.Windows.Forms.ToolStripButton();
			this.Status_SC = new System.Windows.Forms.SplitContainer();
			this.Print_BT = new System.Windows.Forms.Button();
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
			((System.ComponentModel.ISupportInitialize)(this.Buttons_SC)).BeginInit();
			this.Buttons_SC.Panel1.SuspendLayout();
			this.Buttons_SC.Panel2.SuspendLayout();
			this.Buttons_SC.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Content_SC)).BeginInit();
			this.Content_SC.Panel1.SuspendLayout();
			this.Content_SC.SuspendLayout();
			this.Tool_Strip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Status_SC)).BeginInit();
			this.Status_SC.Panel1.SuspendLayout();
			this.Status_SC.Panel2.SuspendLayout();
			this.Status_SC.SuspendLayout();
			this.BarraEstado_ST.SuspendLayout();
			this.SuspendLayout();
			// 
			// Progress_Panel
			// 
			resources.ApplyResources(this.Progress_Panel, "Progress_Panel");
			// 
			// ProgressBK_Panel
			// 
			resources.ApplyResources(this.ProgressBK_Panel, "ProgressBK_Panel");
			// 
			// ProgressInfo_PB
			// 
			resources.ApplyResources(this.ProgressInfo_PB, "ProgressInfo_PB");
			// 
			// Progress_PB
			// 
			resources.ApplyResources(this.Progress_PB, "Progress_PB");
			// 
			// Buttons_SC
			// 
			this.Buttons_SC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			resources.ApplyResources(this.Buttons_SC, "Buttons_SC");
			this.Buttons_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.Buttons_SC.Name = "Buttons_SC";
			// 
			// Buttons_SC.Panel1
			// 
			this.Buttons_SC.Panel1.Controls.Add(this.Content_SC);
			this.HelpProvider.SetShowHelp(this.Buttons_SC.Panel1, ((bool)(resources.GetObject("Buttons_SC.Panel1.ShowHelp"))));
			// 
			// Buttons_SC.Panel2
			// 
			this.Buttons_SC.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Buttons_SC.Panel2.Controls.Add(this.Status_SC);
			this.HelpProvider.SetShowHelp(this.Buttons_SC.Panel2, ((bool)(resources.GetObject("Buttons_SC.Panel2.ShowHelp"))));
			this.HelpProvider.SetShowHelp(this.Buttons_SC, ((bool)(resources.GetObject("Buttons_SC.ShowHelp"))));
			this.Buttons_SC.TabStop = false;
			// 
			// Content_SC
			// 
			resources.ApplyResources(this.Content_SC, "Content_SC");
			this.Content_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.Content_SC.Name = "Content_SC";
			// 
			// Content_SC.Panel1
			// 
			this.Content_SC.Panel1.Controls.Add(this.Tool_Strip);
			// 
			// Tool_Strip
			// 
			this.Tool_Strip.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.Tool_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_TI,
            this.Delete_TI,
            this.Select_TI,
            this.Separator4_TI,
            this.Print_TI,
            this.Separator2_TI,
            this.Close_TI});
			resources.ApplyResources(this.Tool_Strip, "Tool_Strip");
			this.Tool_Strip.Name = "Tool_Strip";
			this.HelpProvider.SetShowHelp(this.Tool_Strip, ((bool)(resources.GetObject("Tool_Strip.ShowHelp"))));
			// 
			// Add_TI
			// 
			this.Add_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Add_TI.Image = global::moleQule.Face.Properties.Resources.new_item;
			resources.ApplyResources(this.Add_TI, "Add_TI");
			this.Add_TI.Name = "Add_TI";
			this.Add_TI.Click += new System.EventHandler(this.Add_TI_Click);
			// 
			// Delete_TI
			// 
			this.Delete_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Delete_TI.Image = global::moleQule.Face.Properties.Resources.delete_item;
			resources.ApplyResources(this.Delete_TI, "Delete_TI");
			this.Delete_TI.Name = "Delete_TI";
			this.Delete_TI.Click += new System.EventHandler(this.Delete_TI_Click);
			// 
			// Select_TI
			// 
			this.Select_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Select_TI.Image = global::moleQule.Face.Properties.Resources.select_item;
			resources.ApplyResources(this.Select_TI, "Select_TI");
			this.Select_TI.Name = "Select_TI";
			this.Select_TI.Click += new System.EventHandler(this.Select_TI_Click);
			// 
			// Separator4_TI
			// 
			this.Separator4_TI.Name = "Separator4_TI";
			resources.ApplyResources(this.Separator4_TI, "Separator4_TI");
			// 
			// Print_TI
			// 
			this.Print_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Print_TI.Image = global::moleQule.Face.Properties.Resources.print_item;
			resources.ApplyResources(this.Print_TI, "Print_TI");
			this.Print_TI.Name = "Print_TI";
			this.Print_TI.Click += new System.EventHandler(this.Print_TI_Click);
			// 
			// Separator2_TI
			// 
			this.Separator2_TI.Name = "Separator2_TI";
			resources.ApplyResources(this.Separator2_TI, "Separator2_TI");
			// 
			// Close_TI
			// 
			this.Close_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Close_TI.Image = global::moleQule.Face.Properties.Resources.error;
			resources.ApplyResources(this.Close_TI, "Close_TI");
			this.Close_TI.Name = "Close_TI";
			this.Close_TI.Click += new System.EventHandler(this.Close_TI_Click);
			// 
			// Status_SC
			// 
			resources.ApplyResources(this.Status_SC, "Status_SC");
			this.Status_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.Status_SC.Name = "Status_SC";
			// 
			// Status_SC.Panel1
			// 
			this.Status_SC.Panel1.Controls.Add(this.Print_BT);
			this.Status_SC.Panel1.Controls.Add(this.Submit_BT);
			this.Status_SC.Panel1.Controls.Add(this.Cancel_BT);
			this.HelpProvider.SetShowHelp(this.Status_SC.Panel1, ((bool)(resources.GetObject("Status_SC.Panel1.ShowHelp"))));
			// 
			// Status_SC.Panel2
			// 
			this.Status_SC.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
			this.Status_SC.Panel2.Controls.Add(this.BarraEstado_ST);
			this.HelpProvider.SetShowHelp(this.Status_SC.Panel2, ((bool)(resources.GetObject("Status_SC.Panel2.ShowHelp"))));
			this.Status_SC.Panel2Collapsed = true;
			this.HelpProvider.SetShowHelp(this.Status_SC, ((bool)(resources.GetObject("Status_SC.ShowHelp"))));
			this.Status_SC.TabStop = false;
			// 
			// Print_BT
			// 
			resources.ApplyResources(this.Print_BT, "Print_BT");
			this.Print_BT.Name = "Print_BT";
			this.HelpProvider.SetShowHelp(this.Print_BT, ((bool)(resources.GetObject("Print_BT.ShowHelp"))));
			this.Print_BT.UseVisualStyleBackColor = true;
			this.Print_BT.Click += new System.EventHandler(this.Imprimir_Button_Click);
			// 
			// Submit_BT
			// 
			resources.ApplyResources(this.Submit_BT, "Submit_BT");
			this.Submit_BT.Name = "Submit_BT";
			this.HelpProvider.SetShowHelp(this.Submit_BT, ((bool)(resources.GetObject("Submit_BT.ShowHelp"))));
			this.Submit_BT.UseVisualStyleBackColor = true;
			this.Submit_BT.Click += new System.EventHandler(this.Guardar_Button_Click);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			resources.ApplyResources(this.Cancel_BT, "Cancel_BT");
			this.Cancel_BT.Name = "Cancel_BT";
			this.HelpProvider.SetShowHelp(this.Cancel_BT, ((bool)(resources.GetObject("Cancel_BT.ShowHelp"))));
			this.Cancel_BT.UseVisualStyleBackColor = true;
			this.Cancel_BT.Click += new System.EventHandler(this.Cancelar_Button_Click);
			// 
			// BarraEstado_ST
			// 
			this.BarraEstado_ST.BackColor = System.Drawing.SystemColors.Control;
			this.BarraEstado_ST.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
			this.BarraEstado_ST.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Info_SL});
			resources.ApplyResources(this.BarraEstado_ST, "BarraEstado_ST");
			this.BarraEstado_ST.Name = "BarraEstado_ST";
			this.HelpProvider.SetShowHelp(this.BarraEstado_ST, ((bool)(resources.GetObject("BarraEstado_ST.ShowHelp"))));
			// 
			// Info_SL
			// 
			this.Info_SL.Name = "Info_SL";
			resources.ApplyResources(this.Info_SL, "Info_SL");
			// 
			// ListMngSkinForm
			// 
			this.AcceptButton = this.Submit_BT;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Cancel_BT;
			this.Controls.Add(this.Buttons_SC);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
			this.Name = "ListMngSkinForm";
			this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.Resize += new System.EventHandler(this.ListMngSkinForm_Resize);
			this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
			this.Controls.SetChildIndex(this.Buttons_SC, 0);
			this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
			this.Controls.SetChildIndex(this.Progress_PB, 0);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.Buttons_SC.Panel1.ResumeLayout(false);
			this.Buttons_SC.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Buttons_SC)).EndInit();
			this.Buttons_SC.ResumeLayout(false);
			this.Content_SC.Panel1.ResumeLayout(false);
			this.Content_SC.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Content_SC)).EndInit();
			this.Content_SC.ResumeLayout(false);
			this.Tool_Strip.ResumeLayout(false);
			this.Tool_Strip.PerformLayout();
			this.Status_SC.Panel1.ResumeLayout(false);
			this.Status_SC.Panel2.ResumeLayout(false);
			this.Status_SC.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Status_SC)).EndInit();
			this.Status_SC.ResumeLayout(false);
			this.BarraEstado_ST.ResumeLayout(false);
			this.BarraEstado_ST.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		protected System.Windows.Forms.SplitContainer Buttons_SC;
		protected System.Windows.Forms.Button Submit_BT;
		protected System.Windows.Forms.Button Cancel_BT;
		private System.Windows.Forms.StatusStrip BarraEstado_ST;
		protected System.Windows.Forms.ToolStripStatusLabel Info_SL;
		protected System.Windows.Forms.SplitContainer Status_SC;
		protected System.Windows.Forms.Button Print_BT;
        protected System.Windows.Forms.SplitContainer Content_SC;
        protected System.Windows.Forms.ToolStrip Tool_Strip;
        protected System.Windows.Forms.ToolStripButton Add_TI;
        protected System.Windows.Forms.ToolStripButton Delete_TI;
        protected System.Windows.Forms.ToolStripButton Select_TI;
        private System.Windows.Forms.ToolStripSeparator Separator4_TI;
        protected System.Windows.Forms.ToolStripButton Print_TI;
        protected System.Windows.Forms.ToolStripSeparator Separator2_TI;
        protected System.Windows.Forms.ToolStripButton Close_TI;
	}
}