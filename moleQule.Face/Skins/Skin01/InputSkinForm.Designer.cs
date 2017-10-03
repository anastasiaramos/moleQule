namespace moleQule.Face.Skin01
{
    partial class InputSkinForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputSkinForm));
            this.Main_CM = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Refresh_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.Separator1_MI = new System.Windows.Forms.ToolStripSeparator();
            this.CustomAction1_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomAction2_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomAction3_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomAction4_MI = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            this.Progress_Panel.SuspendLayout();
            this.ProgressBK_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            this.Main_CM.SuspendLayout();
            this.SuspendLayout();
            // 
            // Submit_BT
            // 
            resources.ApplyResources(this.Submit_BT, "Submit_BT");
            this.Submit_BT.Image = global::moleQule.Face.Properties.Resources.accept_24;
            this.HelpProvider.SetShowHelp(this.Submit_BT, ((bool)(resources.GetObject("Submit_BT.ShowHelp"))));
            this.Submit_BT.Click += new System.EventHandler(this.Aceptar_Button_Click);
            // 
            // Cancel_BT
            // 
            resources.ApplyResources(this.Cancel_BT, "Cancel_BT");
            this.Cancel_BT.Image = global::moleQule.Face.Properties.Resources.cancel_24;
            this.HelpProvider.SetShowHelp(this.Cancel_BT, ((bool)(resources.GetObject("Cancel_BT.ShowHelp"))));
            this.Cancel_BT.Click += new System.EventHandler(this.Cancelar_Button_Click);
            // 
            // Source_GB
            // 
            resources.ApplyResources(this.Source_GB, "Source_GB");
            this.HelpProvider.SetShowHelp(this.Source_GB, ((bool)(resources.GetObject("Source_GB.ShowHelp"))));
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, ((bool)(resources.GetObject("PanelesV.Panel1.ShowHelp"))));
            // 
            // PanelesV.Panel2
            // 
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, ((bool)(resources.GetObject("PanelesV.Panel2.ShowHelp"))));
            resources.ApplyResources(this.PanelesV, "PanelesV");
            this.HelpProvider.SetShowHelp(this.PanelesV, ((bool)(resources.GetObject("PanelesV.ShowHelp"))));
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
            // Main_CM
            // 
            this.Main_CM.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Refresh_MI,
            this.Separator1_MI,
            this.CustomAction1_MI,
            this.CustomAction2_MI,
            this.CustomAction3_MI,
            this.CustomAction4_MI});
            this.Main_CM.Name = "Mng_CMenu";
            this.HelpProvider.SetShowHelp(this.Main_CM, ((bool)(resources.GetObject("Main_CM.ShowHelp"))));
            resources.ApplyResources(this.Main_CM, "Main_CM");
            // 
            // Refresh_MI
            // 
            this.Refresh_MI.Image = global::moleQule.Face.Properties.Resources.refresh;
            this.Refresh_MI.Name = "Refresh_MI";
            resources.ApplyResources(this.Refresh_MI, "Refresh_MI");
            // 
            // Separator1_MI
            // 
            this.Separator1_MI.Name = "Separator1_MI";
            resources.ApplyResources(this.Separator1_MI, "Separator1_MI");
            // 
            // CustomAction1_MI
            // 
            this.CustomAction1_MI.Image = global::moleQule.Face.Properties.Resources.operation;
            this.CustomAction1_MI.Name = "CustomAction1_MI";
            resources.ApplyResources(this.CustomAction1_MI, "CustomAction1_MI");
            this.CustomAction1_MI.Click += new System.EventHandler(this.CustomAction1_MI_Click);
            // 
            // CustomAction2_MI
            // 
            this.CustomAction2_MI.Image = global::moleQule.Face.Properties.Resources.operation;
            this.CustomAction2_MI.Name = "CustomAction2_MI";
            resources.ApplyResources(this.CustomAction2_MI, "CustomAction2_MI");
            this.CustomAction2_MI.Click += new System.EventHandler(this.CustomAction2_MI_Click);
            // 
            // CustomAction3_MI
            // 
            this.CustomAction3_MI.Image = global::moleQule.Face.Properties.Resources.operation;
            this.CustomAction3_MI.Name = "CustomAction3_MI";
            resources.ApplyResources(this.CustomAction3_MI, "CustomAction3_MI");
            this.CustomAction3_MI.Click += new System.EventHandler(this.CustomAction3_MI_Click);
            // 
            // CustomAction4_MI
            // 
            this.CustomAction4_MI.Image = global::moleQule.Face.Properties.Resources.operation;
            this.CustomAction4_MI.Name = "CustomAction4_MI";
            resources.ApplyResources(this.CustomAction4_MI, "CustomAction4_MI");
            this.CustomAction4_MI.Click += new System.EventHandler(this.CustomAction4_MI_Click);
            // 
            // InputSkinForm
            // 
            resources.ApplyResources(this, "$this");
            this.ContextMenuStrip = this.Main_CM;
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.Name = "InputSkinForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
            this.Controls.SetChildIndex(this.PanelesV, 0);
            this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
            this.Controls.SetChildIndex(this.Progress_PB, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
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
            this.Main_CM.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.ContextMenuStrip Main_CM;
		private System.Windows.Forms.ToolStripMenuItem Refresh_MI;
		private System.Windows.Forms.ToolStripSeparator Separator1_MI;
		private System.Windows.Forms.ToolStripMenuItem CustomAction1_MI;
		private System.Windows.Forms.ToolStripMenuItem CustomAction2_MI;
		private System.Windows.Forms.ToolStripMenuItem CustomAction3_MI;
		private System.Windows.Forms.ToolStripMenuItem CustomAction4_MI;
    }
}
