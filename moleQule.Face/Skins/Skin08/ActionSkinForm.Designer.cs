namespace moleQule.Face.Skin08
{
    public partial class ActionSkinForm 
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActionSkinForm));
			this.Print_BT = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			this.SuspendLayout();
			// 
			// Submit_BT
			// 
			resources.ApplyResources(this.Submit_BT, "Submit_BT");
			this.Submit_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.HelpProvider.SetShowHelp(this.Submit_BT, ((bool)(resources.GetObject("Submit_BT.ShowHelp"))));
			this.Submit_BT.Click += new System.EventHandler(this.Aceptar_Button_Click);
			// 
			// Cancel_BT
			// 
			resources.ApplyResources(this.Cancel_BT, "Cancel_BT");
			this.Cancel_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.HelpProvider.SetShowHelp(this.Cancel_BT, ((bool)(resources.GetObject("Cancel_BT.ShowHelp"))));
			this.Cancel_BT.Click += new System.EventHandler(this.Cancelar_Button_Click);
			// 
			// Source_GB
			// 
			this.HelpProvider.SetShowHelp(this.Source_GB, ((bool)(resources.GetObject("Source_GB.ShowHelp"))));
			resources.ApplyResources(this.Source_GB, "Source_GB");
			// 
			// PanelesV
			// 
			resources.ApplyResources(this.PanelesV, "PanelesV");
			// 
			// PanelesV.Panel1
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, ((bool)(resources.GetObject("PanelesV.Panel1.ShowHelp"))));
			// 
			// PanelesV.Panel2
			// 
			this.PanelesV.Panel2.BackColor = System.Drawing.Color.AntiqueWhite;
			this.PanelesV.Panel2.Controls.Add(this.Print_BT);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, ((bool)(resources.GetObject("PanelesV.Panel2.ShowHelp"))));
			this.HelpProvider.SetShowHelp(this.PanelesV, ((bool)(resources.GetObject("PanelesV.ShowHelp"))));
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
			// Print_BT
			// 
			resources.ApplyResources(this.Print_BT, "Print_BT");
			this.Print_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Print_BT.Name = "Print_BT";
			this.HelpProvider.SetShowHelp(this.Print_BT, ((bool)(resources.GetObject("Print_BT.ShowHelp"))));
			this.Print_BT.UseVisualStyleBackColor = true;
			this.Print_BT.Click += new System.EventHandler(this.Print_BT_Click);
			// 
			// ActionSkinForm
			// 
			resources.ApplyResources(this, "$this");
			this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
			this.Name = "ActionSkinForm";
			this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
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
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Button Print_BT;
    }
}
