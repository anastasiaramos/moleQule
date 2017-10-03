namespace moleQule.Face.Skin01
{
    partial class ItemMngSkinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemMngSkinForm));
            this.PanelesV = new System.Windows.Forms.SplitContainer();
            this.Paneles2 = new System.Windows.Forms.SplitContainer();
            this.Cancel_BT = new System.Windows.Forms.Button();
            this.Submit_BT = new System.Windows.Forms.Button();
            this.Docs_BT = new System.Windows.Forms.Button();
            this.Imprimir_Button = new System.Windows.Forms.Button();
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
            // Animation
            // 
            resources.ApplyResources(this.Animation, "Animation");
            // 
            // CancelBkJob_BT
            // 
            this.CancelBkJob_BT.ImageKey = global::moleQule.Face.Resources.Labels.LEYENDA;
            resources.ApplyResources(this.CancelBkJob_BT, "CancelBkJob_BT");
            // 
            // ProgressMsg_LB
            // 
            this.ProgressMsg_LB.ImageKey = global::moleQule.Face.Resources.Labels.LEYENDA;
            resources.ApplyResources(this.ProgressMsg_LB, "ProgressMsg_LB");
            // 
            // Title_LB
            // 
            this.Title_LB.ImageKey = global::moleQule.Face.Resources.Labels.LEYENDA;
            resources.ApplyResources(this.Title_LB, "Title_LB");
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
            // PanelesV
            // 
            this.PanelesV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.PanelesV, "PanelesV");
            this.PanelesV.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.PanelesV.Name = "PanelesV";
            // 
            // PanelesV.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, ((bool)(resources.GetObject("PanelesV.Panel1.ShowHelp"))));
            // 
            // PanelesV.Panel2
            // 
            this.PanelesV.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PanelesV.Panel2.Controls.Add(this.Paneles2);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, ((bool)(resources.GetObject("PanelesV.Panel2.ShowHelp"))));
            this.HelpProvider.SetShowHelp(this.PanelesV, ((bool)(resources.GetObject("PanelesV.ShowHelp"))));
            this.PanelesV.TabStop = false;
            // 
            // Paneles2
            // 
            resources.ApplyResources(this.Paneles2, "Paneles2");
            this.ErrorMng_EP.SetError(this.Paneles2, global::moleQule.Face.Resources.Labels.LEYENDA);
            this.Paneles2.Name = "Paneles2";
            // 
            // Paneles2.Panel1
            // 
            this.Paneles2.Panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Paneles2.Panel1.Controls.Add(this.Cancel_BT);
            this.Paneles2.Panel1.Controls.Add(this.Submit_BT);
            this.Paneles2.Panel1.Controls.Add(this.Imprimir_Button);
            this.Paneles2.Panel1.Controls.Add(this.Docs_BT);
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, ((bool)(resources.GetObject("Paneles2.Panel1.ShowHelp"))));
            // 
            // Paneles2.Panel2
            // 
            this.Paneles2.Panel2.BackColor = System.Drawing.Color.LemonChiffon;
            this.Paneles2.Panel2.Controls.Add(this.BarraEstado_ST);
            this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, ((bool)(resources.GetObject("Paneles2.Panel2.ShowHelp"))));
            this.Paneles2.Panel2Collapsed = true;
            this.HelpProvider.SetShowHelp(this.Paneles2, ((bool)(resources.GetObject("Paneles2.ShowHelp"))));
            this.Paneles2.TabStop = false;
            // 
            // Cancel_BT
            // 
            resources.ApplyResources(this.Cancel_BT, "Cancel_BT");
            this.Cancel_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Cancel_BT.Image = global::moleQule.Face.Properties.Resources.cancel_24;
            this.Cancel_BT.Name = "Cancel_BT";
            this.HelpProvider.SetShowHelp(this.Cancel_BT, ((bool)(resources.GetObject("Cancel_BT.ShowHelp"))));
            this.Cancel_BT.UseVisualStyleBackColor = true;
            this.Cancel_BT.Click += new System.EventHandler(this.Cancel_BT_Click);
            // 
            // Submit_BT
            // 
            resources.ApplyResources(this.Submit_BT, "Submit_BT");
            this.Submit_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Submit_BT.Image = global::moleQule.Face.Properties.Resources.accept_24;
            this.Submit_BT.Name = "Submit_BT";
            this.HelpProvider.SetShowHelp(this.Submit_BT, ((bool)(resources.GetObject("Submit_BT.ShowHelp"))));
            this.Submit_BT.UseVisualStyleBackColor = true;
            this.Submit_BT.Click += new System.EventHandler(this.Save_BT_Click);
            // 
            // Docs_BT
            // 
            resources.ApplyResources(this.Docs_BT, "Docs_BT");
            this.Docs_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Docs_BT.Image = global::moleQule.Face.Properties.Resources.docs_24;
            this.Docs_BT.Name = "Docs_BT";
            this.HelpProvider.SetShowHelp(this.Docs_BT, ((bool)(resources.GetObject("Docs_BT.ShowHelp"))));
            this.Docs_BT.UseVisualStyleBackColor = true;
            this.Docs_BT.Click += new System.EventHandler(this.Docs_BT_Click);
            // 
            // Imprimir_Button
            // 
            resources.ApplyResources(this.Imprimir_Button, "Imprimir_Button");
            this.Imprimir_Button.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.Imprimir_Button.Image = global::moleQule.Face.Properties.Resources.print_24;
            this.Imprimir_Button.Name = "Imprimir_Button";
            this.HelpProvider.SetShowHelp(this.Imprimir_Button, ((bool)(resources.GetObject("Imprimir_Button.ShowHelp"))));
            this.Imprimir_Button.UseVisualStyleBackColor = true;
            this.Imprimir_Button.Click += new System.EventHandler(this.Print_BT_Click);
            // 
            // BarraEstado_ST
            // 
            this.BarraEstado_ST.BackColor = System.Drawing.Color.Gainsboro;
            this.ErrorMng_EP.SetError(this.BarraEstado_ST, global::moleQule.Face.Resources.Labels.LEYENDA);
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
            // ItemMngSkinForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.PanelesV);
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.Name = "ItemMngSkinForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.Resize += new System.EventHandler(this.ManagerEntitySkinForm_Resize);
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
		protected System.Windows.Forms.Button Submit_BT;
		protected System.Windows.Forms.Button Cancel_BT;
		protected System.Windows.Forms.StatusStrip BarraEstado_ST;
		protected System.Windows.Forms.ToolStripStatusLabel Info_SL;
		protected System.Windows.Forms.SplitContainer Paneles2;
		protected System.Windows.Forms.Button Imprimir_Button;
        protected System.Windows.Forms.Button Docs_BT;
	}
}