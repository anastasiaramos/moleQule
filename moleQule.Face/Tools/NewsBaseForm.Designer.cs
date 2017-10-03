namespace moleQule.Face
{
	partial class NewsBaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewsBaseForm));
            this.News_LB = new System.Windows.Forms.ListBox();
            this.Mostrar_CB = new System.Windows.Forms.CheckBox();
            this.PanelesV.Panel1.SuspendLayout();
            this.PanelesV.Panel2.SuspendLayout();
            this.PanelesV.SuspendLayout();
            this.SuspendLayout();
            // 
            // Submit_BT
            // 
            this.Submit_BT.Location = new System.Drawing.Point(269, 8);
            this.HelpProvider.SetShowHelp(this.Submit_BT, true);
            this.Submit_BT.Size = new System.Drawing.Size(75, 23);
            // 
            // Cancel_BT
            // 
            this.Cancel_BT.Location = new System.Drawing.Point(606, 8);
            this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
            this.Cancel_BT.Size = new System.Drawing.Size(75, 23);
            this.Cancel_BT.Visible = false;
            // 
            // Source_GB
            // 
            this.Source_GB.Location = new System.Drawing.Point(345, 212);
            this.HelpProvider.SetShowHelp(this.Source_GB, true);
            this.Source_GB.Visible = false;
            // 
            // PanelesV
            // 
            // 
            // PanelesV.Panel1
            // 
            this.PanelesV.Panel1.Controls.Add(this.News_LB);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
            // 
            // PanelesV.Panel2
            // 
            this.PanelesV.Panel2.Controls.Add(this.Mostrar_CB);
            this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
            this.HelpProvider.SetShowHelp(this.PanelesV, true);
            this.PanelesV.Size = new System.Drawing.Size(694, 576);
            this.PanelesV.SplitterDistance = 535;
            // 
            // News_LB
            // 
            this.News_LB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.News_LB.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.News_LB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.News_LB.FormattingEnabled = true;
            this.News_LB.HorizontalScrollbar = true;
            this.News_LB.ItemHeight = 14;
            this.News_LB.Location = new System.Drawing.Point(0, 0);
            this.News_LB.Name = "News_LB";
            this.News_LB.Size = new System.Drawing.Size(692, 522);
            this.News_LB.TabIndex = 1;
            this.News_LB.Tag = "NoFormat";
            // 
            // Mostrar_CB
            // 
            this.Mostrar_CB.AutoSize = true;
            this.Mostrar_CB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mostrar_CB.Location = new System.Drawing.Point(60, 10);
            this.Mostrar_CB.Name = "Mostrar_CB";
            this.Mostrar_CB.Size = new System.Drawing.Size(188, 17);
            this.Mostrar_CB.TabIndex = 207;
            this.Mostrar_CB.Text = "No mostrar notificaciones al iniciar";
            this.Mostrar_CB.UseVisualStyleBackColor = true;
            // 
            // NewsBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 576);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewsBaseForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "NewsForm";
            this.PanelesV.Panel1.ResumeLayout(false);
            this.PanelesV.Panel2.ResumeLayout(false);
            this.PanelesV.Panel2.PerformLayout();
            this.PanelesV.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.ListBox News_LB;
        private System.Windows.Forms.CheckBox Mostrar_CB;
	}
}