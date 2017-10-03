namespace moleQule.Face
{
    partial class ImageViewBaseForm
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
            this.Image_PB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Image_PB)).BeginInit();
            this.SuspendLayout();
            // 
            // Image_PB
            // 
            this.Image_PB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Image_PB.Location = new System.Drawing.Point(0, 0);
            this.Image_PB.Name = "Image_PB";
            this.Image_PB.Size = new System.Drawing.Size(621, 457);
            this.Image_PB.TabIndex = 0;
            this.Image_PB.TabStop = false;
            // 
            // ImageViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(621, 457);
            this.Controls.Add(this.Image_PB);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Name = "ImageViewForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "ImageViewForm";
            this.Load += new System.EventHandler(this.ImageViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Image_PB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.PictureBox Image_PB;

    }
}
