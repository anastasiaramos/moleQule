namespace moleQule.Face
{
    partial class BaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.HelpProvider = new System.Windows.Forms.HelpProvider();
            this.Progress_Panel = new System.Windows.Forms.Panel();
            this.Title_LB = new System.Windows.Forms.Label();
            this.ProgressMsg_LB = new System.Windows.Forms.Label();
            this.Animation = new System.Windows.Forms.PictureBox();
            this.CancelBkJob_BT = new System.Windows.Forms.Button();
            this.Progress_PB = new System.Windows.Forms.PictureBox();
            this.ProgressInfo_PB = new System.Windows.Forms.ProgressBar();
            this.ProgressBK_Panel = new System.Windows.Forms.Panel();
            this.CultureManager = new Infralution.Localization.CultureManager(this.components);
            this.Progress_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
            this.ProgressBK_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Progress_Panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Progress_Panel.Controls.Add(this.Title_LB);
            this.Progress_Panel.Controls.Add(this.ProgressMsg_LB);
            this.Progress_Panel.Controls.Add(this.Animation);
            this.Progress_Panel.Controls.Add(this.CancelBkJob_BT);
            resources.ApplyResources(this.Progress_Panel, "Progress_Panel");
            this.Progress_Panel.Name = "Progress_Panel";
            this.HelpProvider.SetShowHelp(this.Progress_Panel, ((bool)(resources.GetObject("Progress_Panel.ShowHelp"))));
            this.Progress_Panel.Tag = "NO FORMAT";
            // 
            // Title_LB
            // 
            resources.ApplyResources(this.Title_LB, "Title_LB");
            this.Title_LB.ForeColor = System.Drawing.Color.Black;
            this.Title_LB.Name = "Title_LB";
            this.HelpProvider.SetShowHelp(this.Title_LB, ((bool)(resources.GetObject("Title_LB.ShowHelp"))));
            this.Title_LB.Tag = "NO FORMAT";
            // 
            // ProgressMsg_LB
            // 
            resources.ApplyResources(this.ProgressMsg_LB, "ProgressMsg_LB");
            this.ProgressMsg_LB.ForeColor = System.Drawing.Color.Black;
            this.ProgressMsg_LB.Name = "ProgressMsg_LB";
            this.HelpProvider.SetShowHelp(this.ProgressMsg_LB, ((bool)(resources.GetObject("ProgressMsg_LB.ShowHelp"))));
            this.ProgressMsg_LB.Tag = "NO FORMAT";
            // 
            // Animation
            // 
            resources.ApplyResources(this.Animation, "Animation");
            this.Animation.Image = global::moleQule.Face.Properties.Resources.working1;
            this.Animation.Name = "Animation";
            this.HelpProvider.SetShowHelp(this.Animation, ((bool)(resources.GetObject("Animation.ShowHelp"))));
            this.Animation.TabStop = false;
            this.Animation.Tag = "NO FORMAT";
            // 
            // CancelBkJob_BT
            // 
            this.CancelBkJob_BT.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.CancelBkJob_BT, "CancelBkJob_BT");
            this.CancelBkJob_BT.Name = "CancelBkJob_BT";
            this.HelpProvider.SetShowHelp(this.CancelBkJob_BT, ((bool)(resources.GetObject("CancelBkJob_BT.ShowHelp"))));
            this.CancelBkJob_BT.UseVisualStyleBackColor = true;
            // 
            // Progress_PB
            // 
            this.Progress_PB.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.Progress_PB, "Progress_PB");
            this.Progress_PB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Progress_PB.Image = global::moleQule.Face.Properties.Resources.loading3;
            this.Progress_PB.Name = "Progress_PB";
            this.HelpProvider.SetShowHelp(this.Progress_PB, ((bool)(resources.GetObject("Progress_PB.ShowHelp"))));
            this.Progress_PB.TabStop = false;
            this.Progress_PB.Tag = "NO FORMAT";
            // 
            // ProgressInfo_PB
            // 
            resources.ApplyResources(this.ProgressInfo_PB, "ProgressInfo_PB");
            this.ProgressInfo_PB.MarqueeAnimationSpeed = 10;
            this.ProgressInfo_PB.Name = "ProgressInfo_PB";
            this.HelpProvider.SetShowHelp(this.ProgressInfo_PB, ((bool)(resources.GetObject("ProgressInfo_PB.ShowHelp"))));
            this.ProgressInfo_PB.Step = 1;
            this.ProgressInfo_PB.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProgressInfo_PB.Tag = "NO FORMAT";
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.BackColor = System.Drawing.Color.Transparent;
            this.ProgressBK_Panel.Controls.Add(this.Progress_Panel);
            resources.ApplyResources(this.ProgressBK_Panel, "ProgressBK_Panel");
            this.ProgressBK_Panel.Name = "ProgressBK_Panel";
            this.HelpProvider.SetShowHelp(this.ProgressBK_Panel, ((bool)(resources.GetObject("ProgressBK_Panel.ShowHelp"))));
            this.ProgressBK_Panel.Tag = "NO FORMAT";
            // 
            // CultureManager
            // 
            this.CultureManager.ManagedControl = this;
            this.CultureManager.UICultureChanged += new Infralution.Localization.CultureManager.CultureChangedHandler(this.CultureManager_UICultureChanged);
            // 
            // BaseForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.Progress_PB);
            this.Controls.Add(this.ProgressInfo_PB);
            this.Controls.Add(this.ProgressBK_Panel);
            this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
            this.KeyPreview = true;
            this.Name = "BaseForm";
            this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.Resize += new System.EventHandler(this.BaseForm_Resize);
            this.Progress_Panel.ResumeLayout(false);
            this.Progress_Panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
            this.ProgressBK_Panel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

		protected System.Windows.Forms.HelpProvider HelpProvider;
		public System.Windows.Forms.PictureBox Animation;
        public System.Windows.Forms.Button CancelBkJob_BT;
        public System.Windows.Forms.Label ProgressMsg_LB;
		public System.Windows.Forms.Label Title_LB;
		public System.Windows.Forms.Panel Progress_Panel;
		public System.Windows.Forms.Panel ProgressBK_Panel;
		private Infralution.Localization.CultureManager CultureManager;
		protected System.Windows.Forms.ProgressBar ProgressInfo_PB;
		protected System.Windows.Forms.PictureBox Progress_PB;
    }
}