namespace moleQule.Face.Skin02
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
			this.SC1 = new System.Windows.Forms.SplitContainer();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.Add_Button = new System.Windows.Forms.ToolStripButton();
			this.Edit_Button = new System.Windows.Forms.ToolStripButton();
			this.View_Button = new System.Windows.Forms.ToolStripButton();
			this.Delete_Button = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.Cerrar_Button = new System.Windows.Forms.ToolStripButton();
			this.SC2 = new System.Windows.Forms.SplitContainer();
			this.Select_Button = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SC1)).BeginInit();
			this.SC1.Panel1.SuspendLayout();
			this.SC1.Panel2.SuspendLayout();
			this.SC1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SC2)).BeginInit();
			this.SC2.Panel2.SuspendLayout();
			this.SC2.SuspendLayout();
			this.SuspendLayout();
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(25, 74);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(458, 366);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(197, 234);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(197, 149);
			// 
			// SC1
			// 
			this.SC1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SC1.IsSplitterFixed = true;
			this.SC1.Location = new System.Drawing.Point(0, 0);
			this.SC1.Name = "SC1";
			this.SC1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// SC1.Panel1
			// 
			this.SC1.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
			this.SC1.Panel1.Controls.Add(this.toolStrip1);
			// 
			// SC1.Panel2
			// 
			this.SC1.Panel2.Controls.Add(this.SC2);
			this.SC1.Size = new System.Drawing.Size(458, 366);
			this.SC1.SplitterDistance = 87;
			this.SC1.TabIndex = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Add_Button,
            this.Edit_Button,
            this.View_Button,
            this.Delete_Button,
            this.toolStripSeparator1,
            this.Cerrar_Button});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(458, 39);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
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
			this.Delete_Button.Text = "Eliminar";
			this.Delete_Button.Click += new System.EventHandler(this.Delete_Button_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
			// 
			// Cerrar_Button
			// 
			this.Cerrar_Button.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Cerrar_Button.Image = global::moleQule.Face.Properties.Resources.error;
			this.Cerrar_Button.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Cerrar_Button.Name = "Cerrar_Button";
			this.Cerrar_Button.Size = new System.Drawing.Size(36, 36);
			this.Cerrar_Button.Text = "Cerrar";
			this.Cerrar_Button.Click += new System.EventHandler(this.Cerrar_Button_Click);
			// 
			// SC2
			// 
			this.SC2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SC2.IsSplitterFixed = true;
			this.SC2.Location = new System.Drawing.Point(0, 0);
			this.SC2.Name = "SC2";
			this.SC2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// SC2.Panel2
			// 
			this.SC2.Panel2.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.SC2.Panel2.Controls.Add(this.Select_Button);
			this.SC2.Size = new System.Drawing.Size(458, 275);
			this.SC2.SplitterDistance = 217;
			this.SC2.TabIndex = 1;
			// 
			// Select_Button
			// 
			this.Select_Button.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Select_Button.Image = global::moleQule.Face.Properties.Resources.aceptar;
			this.Select_Button.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.Select_Button.Location = new System.Drawing.Point(329, 5);
			this.Select_Button.Name = "Select_Button";
			this.Select_Button.Size = new System.Drawing.Size(126, 44);
			this.Select_Button.TabIndex = 0;
			this.Select_Button.Text = "Seleccionar";
			this.Select_Button.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.Select_Button.UseVisualStyleBackColor = true;
			// 
			// SelectSkinForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(458, 366);
			this.Controls.Add(this.SC1);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "SelectSkinForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
			this.Controls.SetChildIndex(this.SC1, 0);
			this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
			this.Controls.SetChildIndex(this.Progress_PB, 0);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.SC1.Panel1.ResumeLayout(false);
			this.SC1.Panel1.PerformLayout();
			this.SC1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SC1)).EndInit();
			this.SC1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.SC2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SC2)).EndInit();
			this.SC2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        protected System.Windows.Forms.ToolStripButton Add_Button;
        protected System.Windows.Forms.ToolStripButton Edit_Button;
        protected System.Windows.Forms.ToolStripButton View_Button;
        protected System.Windows.Forms.ToolStripButton Delete_Button;
        protected System.Windows.Forms.Button Select_Button;
        protected System.Windows.Forms.SplitContainer SC1;
        protected System.Windows.Forms.SplitContainer SC2;
        protected System.Windows.Forms.ToolStrip toolStrip1;
        protected System.Windows.Forms.ToolStripButton Cerrar_Button;
    }
}
