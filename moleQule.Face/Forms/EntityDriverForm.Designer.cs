namespace moleQule.Face
{
    partial class EntityDriverForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityDriverForm));
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.SuspendLayout();
			// 
			// EntityDriverForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(519, 363);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EntityDriverForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "EntityDriverForm";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EntityDriverForm_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion
    }
}