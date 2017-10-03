namespace moleQule.Face.Skin04
{
	partial class TreeMngSkinForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeMngSkinForm));
			this.Mng_CMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.DateSelect_MI = new System.Windows.Forms.ToolStripMenuItem();
			this.Refresh_MI = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.Print_MI = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.CustomAction1_MI = new System.Windows.Forms.ToolStripMenuItem();
			this.CustomAction2_MI = new System.Windows.Forms.ToolStripMenuItem();
			this.CustomAction3_MI = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.miniToolStrip = new System.Windows.Forms.BindingNavigator(this.components);
			this.Base_Panel = new System.Windows.Forms.SplitContainer();
			this.Tree_Panel = new System.Windows.Forms.SplitContainer();
			this.Tree_TV = new System.Windows.Forms.TreeView();
			this.Nodes_IL = new System.Windows.Forms.ImageList(this.components);
			this.Menu_TS = new System.Windows.Forms.ToolStrip();
			this.Date_TI = new System.Windows.Forms.ToolStripTextBox();
			this.DateSelect_TI = new System.Windows.Forms.ToolStripButton();
			this.Refresh_TI = new System.Windows.Forms.ToolStripButton();
			this.DateBlock_TSS = new System.Windows.Forms.ToolStripSeparator();
			this.Print_TI = new System.Windows.Forms.ToolStripButton();
			this.PrintBlock_TSS = new System.Windows.Forms.ToolStripSeparator();
			this.CustomAction1_TI = new System.Windows.Forms.ToolStripButton();
			this.CustomAction2_TI = new System.Windows.Forms.ToolStripButton();
			this.CustomAction3_TI = new System.Windows.Forms.ToolStripButton();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DatosSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			this.Mng_CMenu.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.miniToolStrip)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Base_Panel)).BeginInit();
			this.Base_Panel.Panel1.SuspendLayout();
			this.Base_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Tree_Panel)).BeginInit();
			this.Tree_Panel.Panel1.SuspendLayout();
			this.Tree_Panel.SuspendLayout();
			this.Menu_TS.SuspendLayout();
			this.SuspendLayout();
			// 
			// HelpProvider
			// 
			resources.ApplyResources(this.HelpProvider, "HelpProvider");
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
			// Mng_CMenu
			// 
			this.Mng_CMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DateSelect_MI,
            this.Refresh_MI,
            this.toolStripSeparator4,
            this.Print_MI,
            this.toolStripSeparator2,
            this.CustomAction1_MI,
            this.CustomAction2_MI,
            this.CustomAction3_MI,
            this.toolStripSeparator5});
			this.Mng_CMenu.Name = "Mng_CMenu";
			this.HelpProvider.SetShowHelp(this.Mng_CMenu, ((bool)(resources.GetObject("Mng_CMenu.ShowHelp"))));
			resources.ApplyResources(this.Mng_CMenu, "Mng_CMenu");
			// 
			// DateSelect_MI
			// 
			this.DateSelect_MI.Image = global::moleQule.Face.Properties.Resources.calendar;
			this.DateSelect_MI.Name = "DateSelect_MI";
			resources.ApplyResources(this.DateSelect_MI, "DateSelect_MI");
			this.DateSelect_MI.Click += new System.EventHandler(this.DateSelect_TI_Click);
			// 
			// Refresh_MI
			// 
			this.Refresh_MI.Image = global::moleQule.Face.Properties.Resources.refresh;
			this.Refresh_MI.Name = "Refresh_MI";
			resources.ApplyResources(this.Refresh_MI, "Refresh_MI");
			this.Refresh_MI.Click += new System.EventHandler(this.Refresh_MI_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
			// 
			// Print_MI
			// 
			this.Print_MI.Image = global::moleQule.Face.Properties.Resources.print_16;
			this.Print_MI.Name = "Print_MI";
			resources.ApplyResources(this.Print_MI, "Print_MI");
			this.Print_MI.Click += new System.EventHandler(this.Print_TI_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
			// 
			// CustomAction1_MI
			// 
			this.CustomAction1_MI.Image = global::moleQule.Face.Properties.Resources.operation;
			this.CustomAction1_MI.Name = "CustomAction1_MI";
			resources.ApplyResources(this.CustomAction1_MI, "CustomAction1_MI");
			this.CustomAction1_MI.Click += new System.EventHandler(this.Action1_TI_Click);
			// 
			// CustomAction2_MI
			// 
			this.CustomAction2_MI.Image = global::moleQule.Face.Properties.Resources.operation;
			this.CustomAction2_MI.Name = "CustomAction2_MI";
			resources.ApplyResources(this.CustomAction2_MI, "CustomAction2_MI");
			this.CustomAction2_MI.Click += new System.EventHandler(this.Action2_TI_Click);
			// 
			// CustomAction3_MI
			// 
			this.CustomAction3_MI.Image = global::moleQule.Face.Properties.Resources.operation;
			this.CustomAction3_MI.Name = "CustomAction3_MI";
			resources.ApplyResources(this.CustomAction3_MI, "CustomAction3_MI");
			this.CustomAction3_MI.Click += new System.EventHandler(this.Action3_TI_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
			// 
			// miniToolStrip
			// 
			this.miniToolStrip.AddNewItem = null;
			this.miniToolStrip.BackColor = System.Drawing.Color.Navy;
			this.miniToolStrip.CanOverflow = false;
			this.miniToolStrip.CountItem = null;
			this.miniToolStrip.DeleteItem = null;
			resources.ApplyResources(this.miniToolStrip, "miniToolStrip");
			this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.miniToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.miniToolStrip.MoveFirstItem = null;
			this.miniToolStrip.MoveLastItem = null;
			this.miniToolStrip.MoveNextItem = null;
			this.miniToolStrip.MovePreviousItem = null;
			this.miniToolStrip.Name = "miniToolStrip";
			this.miniToolStrip.PositionItem = null;
			this.miniToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.HelpProvider.SetShowHelp(this.miniToolStrip, ((bool)(resources.GetObject("miniToolStrip.ShowHelp"))));
			// 
			// Base_Panel
			// 
			resources.ApplyResources(this.Base_Panel, "Base_Panel");
			this.Base_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.Base_Panel.Name = "Base_Panel";
			// 
			// Base_Panel.Panel1
			// 
			this.Base_Panel.Panel1.Controls.Add(this.Tree_Panel);
			this.Base_Panel.Panel2Collapsed = true;
			// 
			// Tree_Panel
			// 
			resources.ApplyResources(this.Tree_Panel, "Tree_Panel");
			this.Tree_Panel.Name = "Tree_Panel";
			// 
			// Tree_Panel.Panel1
			// 
			this.Tree_Panel.Panel1.Controls.Add(this.Tree_TV);
			this.Tree_Panel.Panel1.Controls.Add(this.Menu_TS);
			this.Tree_Panel.Panel2Collapsed = true;
			// 
			// Tree_TV
			// 
			resources.ApplyResources(this.Tree_TV, "Tree_TV");
			this.Tree_TV.FullRowSelect = true;
			this.Tree_TV.ImageList = this.Nodes_IL;
			this.Tree_TV.ItemHeight = 20;
			this.Tree_TV.Name = "Tree_TV";
			this.Tree_TV.ShowRootLines = false;
			this.Tree_TV.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.Tree_TV_AfterCollapse);
			this.Tree_TV.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.Tree_TV_AfterExpand);
			this.Tree_TV.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tree_TV_AfterSelect);
			// 
			// Nodes_IL
			// 
			this.Nodes_IL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Nodes_IL.ImageStream")));
			this.Nodes_IL.TransparentColor = System.Drawing.Color.Transparent;
			this.Nodes_IL.Images.SetKeyName(0, "Selected");
			this.Nodes_IL.Images.SetKeyName(1, "Open");
			this.Nodes_IL.Images.SetKeyName(2, "Close");
			// 
			// Menu_TS
			// 
			this.Menu_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.Menu_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Date_TI,
            this.DateSelect_TI,
            this.Refresh_TI,
            this.DateBlock_TSS,
            this.Print_TI,
            this.PrintBlock_TSS,
            this.CustomAction1_TI,
            this.CustomAction2_TI,
            this.CustomAction3_TI});
			resources.ApplyResources(this.Menu_TS, "Menu_TS");
			this.Menu_TS.Name = "Menu_TS";
			this.Menu_TS.Stretch = true;
			// 
			// Date_TI
			// 
			this.Date_TI.Name = "Date_TI";
			this.Date_TI.ReadOnly = true;
			resources.ApplyResources(this.Date_TI, "Date_TI");
			// 
			// DateSelect_TI
			// 
			this.DateSelect_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DateSelect_TI.Image = global::moleQule.Face.Properties.Resources.calendar;
			resources.ApplyResources(this.DateSelect_TI, "DateSelect_TI");
			this.DateSelect_TI.Name = "DateSelect_TI";
			this.DateSelect_TI.Click += new System.EventHandler(this.DateSelect_TI_Click);
			// 
			// Refresh_TI
			// 
			this.Refresh_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Refresh_TI.Image = global::moleQule.Face.Properties.Resources.refresh;
			resources.ApplyResources(this.Refresh_TI, "Refresh_TI");
			this.Refresh_TI.Name = "Refresh_TI";
			this.Refresh_TI.Click += new System.EventHandler(this.Refresh_MI_Click);
			// 
			// DateBlock_TSS
			// 
			this.DateBlock_TSS.Name = "DateBlock_TSS";
			resources.ApplyResources(this.DateBlock_TSS, "DateBlock_TSS");
			// 
			// Print_TI
			// 
			this.Print_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Print_TI.Image = global::moleQule.Face.Properties.Resources.print_item;
			resources.ApplyResources(this.Print_TI, "Print_TI");
			this.Print_TI.Name = "Print_TI";
			this.Print_TI.Click += new System.EventHandler(this.Print_TI_Click);
			// 
			// PrintBlock_TSS
			// 
			this.PrintBlock_TSS.Name = "PrintBlock_TSS";
			resources.ApplyResources(this.PrintBlock_TSS, "PrintBlock_TSS");
			// 
			// CustomAction1_TI
			// 
			this.CustomAction1_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CustomAction1_TI.Image = global::moleQule.Face.Properties.Resources.operation;
			resources.ApplyResources(this.CustomAction1_TI, "CustomAction1_TI");
			this.CustomAction1_TI.Name = "CustomAction1_TI";
			this.CustomAction1_TI.Click += new System.EventHandler(this.Action1_TI_Click);
			// 
			// CustomAction2_TI
			// 
			this.CustomAction2_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CustomAction2_TI.Image = global::moleQule.Face.Properties.Resources.operation;
			resources.ApplyResources(this.CustomAction2_TI, "CustomAction2_TI");
			this.CustomAction2_TI.Name = "CustomAction2_TI";
			this.CustomAction2_TI.Click += new System.EventHandler(this.Action2_TI_Click);
			// 
			// CustomAction3_TI
			// 
			this.CustomAction3_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CustomAction3_TI.Image = global::moleQule.Face.Properties.Resources.operation;
			resources.ApplyResources(this.CustomAction3_TI, "CustomAction3_TI");
			this.CustomAction3_TI.Name = "CustomAction3_TI";
			this.CustomAction3_TI.Click += new System.EventHandler(this.Action3_TI_Click);
			// 
			// TreeMngSkinForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ContextMenuStrip = this.Mng_CMenu;
			this.Controls.Add(this.Base_Panel);
			this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
			this.Name = "TreeMngSkinForm";
			this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.Tag = "Show";
			this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
			this.Controls.SetChildIndex(this.Base_Panel, 0);
			this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
			this.Controls.SetChildIndex(this.Progress_PB, 0);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DatosSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.Mng_CMenu.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.miniToolStrip)).EndInit();
			this.Base_Panel.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Base_Panel)).EndInit();
			this.Base_Panel.ResumeLayout(false);
			this.Tree_Panel.Panel1.ResumeLayout(false);
			this.Tree_Panel.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Tree_Panel)).EndInit();
			this.Tree_Panel.ResumeLayout(false);
			this.Menu_TS.ResumeLayout(false);
			this.Menu_TS.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.ContextMenuStrip Mng_CMenu;
		protected System.Windows.Forms.ToolStripMenuItem Print_MI;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		protected System.Windows.Forms.BindingNavigator miniToolStrip;
		protected System.Windows.Forms.SplitContainer Base_Panel;
		private System.Windows.Forms.ToolStrip Menu_TS;
		private System.Windows.Forms.ToolStripMenuItem CustomAction1_MI;
		private System.Windows.Forms.ToolStripMenuItem CustomAction2_MI;
		private System.Windows.Forms.ToolStripMenuItem CustomAction3_MI;
		protected System.Windows.Forms.SplitContainer Tree_Panel;
		protected System.Windows.Forms.TreeView Tree_TV;
		protected System.Windows.Forms.ToolStripTextBox Date_TI;
		public System.Windows.Forms.ImageList Nodes_IL;
		protected System.Windows.Forms.ToolStripButton Print_TI;
		private System.Windows.Forms.ToolStripMenuItem Refresh_MI;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem DateSelect_MI;
		protected System.Windows.Forms.ToolStripButton DateSelect_TI;
		protected System.Windows.Forms.ToolStripSeparator PrintBlock_TSS;
		protected System.Windows.Forms.ToolStripButton CustomAction1_TI;
		protected System.Windows.Forms.ToolStripButton CustomAction2_TI;
		protected System.Windows.Forms.ToolStripButton Refresh_TI;
		protected System.Windows.Forms.ToolStripButton CustomAction3_TI;
		protected System.Windows.Forms.ToolStripSeparator DateBlock_TSS;
    }
}