namespace moleQule.Face.Skin02
{
    partial class EntityMngSkinForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityMngSkinForm));
            this.PanelesH = new System.Windows.Forms.SplitContainer();
            this.Tool_Strip = new System.Windows.Forms.ToolStrip();
            this.Add_Button = new System.Windows.Forms.ToolStripButton();
            this.Edit_Button = new System.Windows.Forms.ToolStripButton();
            this.View_Button = new System.Windows.Forms.ToolStripButton();
            this.Delete_Button = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Find_Button = new System.Windows.Forms.ToolStripButton();
            this.Print_Button = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Close_Button = new System.Windows.Forms.ToolStripButton();
            this.PanelesV = new System.Windows.Forms.SplitContainer();
            this.Filtros = new System.Windows.Forms.TabControl();
            this.Todos_TP = new System.Windows.Forms.TabPage();
            this.Advanced_TP = new System.Windows.Forms.TabPage();
            this.Mng_CMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Nuevo_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.Detalle_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.Modificar_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.Borrar_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.Duplicar_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.Localizar_MI = new System.Windows.Forms.ToolStripMenuItem();
            this.Imprimir_MI = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
            this.PanelesH.Panel1.SuspendLayout();
            this.PanelesH.Panel2.SuspendLayout();
            this.PanelesH.SuspendLayout();
            this.Tool_Strip.SuspendLayout();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.SuspendLayout();
            this.Filtros.SuspendLayout();
            this.Mng_CMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelesH
            // 
            this.PanelesH.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.PanelesH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelesH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelesH.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.PanelesH.Location = new System.Drawing.Point(0, 0);
            this.PanelesH.Name = "PanelesH";
            this.PanelesH.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PanelesH.Panel1
            // 
            this.PanelesH.Panel1.BackColor = System.Drawing.Color.Maroon;
            this.PanelesH.Panel1.Controls.Add(this.Tool_Strip);
            this.PanelesH.Panel1MinSize = 40;
            // 
            // PanelesH.Panel2
            // 
            this.PanelesH.Panel2.BackColor = System.Drawing.Color.White;
            this.PanelesH.Panel2.Controls.Add(this.PanelesV);
            this.PanelesH.Size = new System.Drawing.Size(844, 516);
            this.PanelesH.SplitterDistance = 40;
            this.PanelesH.SplitterWidth = 1;
            this.PanelesH.TabIndex = 1;
            // 
            // Tool_Strip
            // 
            this.Tool_Strip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Tool_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_Button,
            this.Edit_Button,
            this.View_Button,
            this.Delete_Button,
            this.toolStripSeparator2,
            this.Find_Button,
            this.Print_Button,
            this.toolStripSeparator1,
            this.Close_Button});
            this.Tool_Strip.Location = new System.Drawing.Point(0, 0);
            this.Tool_Strip.Name = "Tool_Strip";
            this.Tool_Strip.Size = new System.Drawing.Size(842, 39);
            this.Tool_Strip.TabIndex = 0;
            this.Tool_Strip.Text = "Imprimir";
            // 
            // Add_Button
            // 
            this.Add_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Add_Button.Image = global::moleQule.Face.Properties.Resources.new_item;
            this.Add_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Add_Button.Name = "Add_Button";
            this.Add_Button.Size = new System.Drawing.Size(36, 36);
            this.Add_Button.Text = "Nuevo";
            this.Add_Button.Click += new System.EventHandler(this.Add_Button_Click);
            // 
            // Edit_Button
            // 
            this.Edit_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Edit_Button.Image = global::moleQule.Face.Properties.Resources.edit_item;
            this.Edit_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Edit_Button.Name = "Edit_Button";
            this.Edit_Button.Size = new System.Drawing.Size(36, 36);
            this.Edit_Button.Text = "Editar";
            this.Edit_Button.Click += new System.EventHandler(this.Edit_Button_Click);
            // 
            // View_Button
            // 
            this.View_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.View_Button.Image = global::moleQule.Face.Properties.Resources.view_item;
            this.View_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.View_Button.Name = "View_Button";
            this.View_Button.Size = new System.Drawing.Size(36, 36);
            this.View_Button.Text = "Ver";
            this.View_Button.Click += new System.EventHandler(this.View_Button_Click);
            // 
            // Delete_Button
            // 
            this.Delete_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Delete_Button.Image = global::moleQule.Face.Properties.Resources.delete_item;
            this.Delete_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Delete_Button.Name = "Delete_Button";
            this.Delete_Button.Size = new System.Drawing.Size(36, 36);
            this.Delete_Button.Text = "Borrar";
            this.Delete_Button.Click += new System.EventHandler(this.Delete_Button_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // Find_Button
            // 
            this.Find_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Find_Button.Image = global::moleQule.Face.Properties.Resources.find;
            this.Find_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Find_Button.Name = "Find_Button";
            this.Find_Button.Size = new System.Drawing.Size(36, 36);
            this.Find_Button.Text = "Localizar";
            this.Find_Button.Click += new System.EventHandler(this.Find_Button_Click);
            // 
            // Print_Button
            // 
            this.Print_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Print_Button.Image = global::moleQule.Face.Properties.Resources.print_item;
            this.Print_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Print_Button.Name = "Print_Button";
            this.Print_Button.Size = new System.Drawing.Size(36, 36);
            this.Print_Button.Text = "Imprimir";
            this.Print_Button.Click += new System.EventHandler(this.Print_Button_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // Close_Button
            // 
            this.Close_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Close_Button.Image = global::moleQule.Face.Properties.Resources.error;
            this.Close_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Close_Button.Name = "Close_Button";
            this.Close_Button.Size = new System.Drawing.Size(36, 36);
            this.Close_Button.Text = "Cerrar";
            this.Close_Button.Click += new System.EventHandler(this.Close_Button_Click);
            // 
            // PanelesV
            // 
            this.PanelesV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelesV.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.PanelesV.IsSplitterFixed = true;
            this.PanelesV.Location = new System.Drawing.Point(0, 0);
            this.PanelesV.Name = "PanelesV";
            this.PanelesV.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.Controls.Add(this.Filtros);
            this.PanelesV.Size = new System.Drawing.Size(842, 473);
            this.PanelesV.SplitterDistance = 25;
            this.PanelesV.SplitterWidth = 1;
            this.PanelesV.TabIndex = 0;
            // 
            // Filtros
            // 
            this.Filtros.Controls.Add(this.Todos_TP);
            this.Filtros.Controls.Add(this.Advanced_TP);
            this.Filtros.Location = new System.Drawing.Point(304, 3);
            this.Filtros.Name = "Filtros";
            this.Filtros.SelectedIndex = 0;
            this.Filtros.Size = new System.Drawing.Size(118, 25);
            this.Filtros.TabIndex = 1;
            this.Filtros.SelectedIndexChanged += new System.EventHandler(this.Filtros_SelectedIndexChanged);
            // 
            // Todos_TP
            // 
            this.Todos_TP.Location = new System.Drawing.Point(4, 22);
            this.Todos_TP.Name = "Todos_TP";
            this.Todos_TP.Padding = new System.Windows.Forms.Padding(3);
            this.Todos_TP.Size = new System.Drawing.Size(110, 0);
            this.Todos_TP.TabIndex = 0;
            this.Todos_TP.Text = "Todos";
            this.Todos_TP.UseVisualStyleBackColor = true;
            // 
            // Advanced_TP
            // 
            this.Advanced_TP.Location = new System.Drawing.Point(4, 22);
            this.Advanced_TP.Name = "Advanced_TP";
            this.Advanced_TP.Padding = new System.Windows.Forms.Padding(3);
            this.Advanced_TP.Size = new System.Drawing.Size(110, -1);
            this.Advanced_TP.TabIndex = 5;
            this.Advanced_TP.Text = "Búsqueda";
            this.Advanced_TP.UseVisualStyleBackColor = true;
            // 
            // Mng_CMenu
            // 
            this.Mng_CMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Nuevo_MI,
            this.Detalle_MI,
            this.Modificar_MI,
            this.Borrar_MI,
            this.Duplicar_MI,
            this.Localizar_MI,
            this.Imprimir_MI});
            this.Mng_CMenu.Name = "Mng_CMenu";
            this.Mng_CMenu.Size = new System.Drawing.Size(149, 158);
            // 
            // Nuevo_MI
            // 
            this.Nuevo_MI.Name = "Nuevo_MI";
            this.Nuevo_MI.Size = new System.Drawing.Size(148, 22);
            this.Nuevo_MI.Text = "Nuevo";
            this.Nuevo_MI.Click += new System.EventHandler(this.Add_Button_Click);
            // 
            // Detalle_MI
            // 
            this.Detalle_MI.Name = "Detalle_MI";
            this.Detalle_MI.Size = new System.Drawing.Size(148, 22);
            this.Detalle_MI.Text = "Ver Detalle";
            this.Detalle_MI.Click += new System.EventHandler(this.View_Button_Click);
            // 
            // Modificar_MI
            // 
            this.Modificar_MI.Name = "Modificar_MI";
            this.Modificar_MI.Size = new System.Drawing.Size(148, 22);
            this.Modificar_MI.Text = "Modificar";
            this.Modificar_MI.Click += new System.EventHandler(this.Edit_Button_Click);
            // 
            // Borrar_MI
            // 
            this.Borrar_MI.Name = "Borrar_MI";
            this.Borrar_MI.Size = new System.Drawing.Size(148, 22);
            this.Borrar_MI.Text = "Eliminar";
            this.Borrar_MI.Click += new System.EventHandler(this.Delete_Button_Click);
            // 
            // Duplicar_MI
            // 
            this.Duplicar_MI.Name = "Duplicar_MI";
            this.Duplicar_MI.Size = new System.Drawing.Size(148, 22);
            this.Duplicar_MI.Text = "Duplicar";
            this.Duplicar_MI.Click += new System.EventHandler(this.Copy_Button_Click);
            // 
            // Localizar_MI
            // 
            this.Localizar_MI.Name = "Localizar_MI";
            this.Localizar_MI.Size = new System.Drawing.Size(148, 22);
            this.Localizar_MI.Text = "Localizar";
            this.Localizar_MI.Click += new System.EventHandler(this.Find_Button_Click);
            // 
            // Imprimir_MI
            // 
            this.Imprimir_MI.Name = "Imprimir_MI";
            this.Imprimir_MI.Size = new System.Drawing.Size(148, 22);
            this.Imprimir_MI.Text = "Imprimir Lista";
            this.Imprimir_MI.Click += new System.EventHandler(this.Print_Button_Click);
            // 
            // EntityMngSkinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 516);
            this.ContextMenuStrip = this.Mng_CMenu;
            this.Controls.Add(this.PanelesH);
            this.HelpProvider.SetHelpKeyword(this, "30");
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EntityMngSkinForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "EntityMngSkin02Form";
            this.Load += new System.EventHandler(this.EntityMngSkinForm_Load);
			this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
			this.Controls.SetChildIndex(this.Progress_PB, 0);
            ((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
            this.PanelesH.Panel1.ResumeLayout(false);
            this.PanelesH.Panel1.PerformLayout();
            this.PanelesH.Panel2.ResumeLayout(false);
            this.PanelesH.ResumeLayout(false);
            this.Tool_Strip.ResumeLayout(false);
            this.Tool_Strip.PerformLayout();
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.ResumeLayout(false);
            this.Filtros.ResumeLayout(false);
            this.Mng_CMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.SplitContainer PanelesH;
		protected System.Windows.Forms.SplitContainer PanelesV;
		protected System.Windows.Forms.TabControl Filtros;
		protected System.Windows.Forms.TabPage Todos_TP;
		protected System.Windows.Forms.TabPage Advanced_TP;
        protected System.Windows.Forms.ContextMenuStrip Mng_CMenu;
        protected System.Windows.Forms.ToolStripMenuItem Nuevo_MI;
        protected System.Windows.Forms.ToolStripMenuItem Detalle_MI;
        protected System.Windows.Forms.ToolStripMenuItem Modificar_MI;
        protected System.Windows.Forms.ToolStripMenuItem Borrar_MI;
        protected System.Windows.Forms.ToolStripMenuItem Duplicar_MI;
        protected System.Windows.Forms.ToolStripMenuItem Localizar_MI;
        protected System.Windows.Forms.ToolStripMenuItem Imprimir_MI;
		protected System.Windows.Forms.ToolStrip Tool_Strip;
		protected System.Windows.Forms.ToolStripButton Add_Button;
		protected System.Windows.Forms.ToolStripButton Edit_Button;
		protected System.Windows.Forms.ToolStripButton View_Button;
		protected System.Windows.Forms.ToolStripButton Delete_Button;
		protected System.Windows.Forms.ToolStripButton Find_Button;
		protected System.Windows.Forms.ToolStripButton Print_Button;
		protected System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		protected System.Windows.Forms.ToolStripButton Close_Button;
		protected System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}