namespace moleQule.Face
{
    partial class EntityMngForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityMngForm));
			this.Datos = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.SuspendLayout();
			// 
			// EntityMngForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(844, 516);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HelpProvider.SetHelpKeyword(this, "30");
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "EntityMngForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "EntityMngForm";
			this.Load += new System.EventHandler(this.EntityMngForm_Load);
			this.Shown += new System.EventHandler(this.EntityMngForm_Shown);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EntityMngForm_FormClosed);
			this.Resize += new System.EventHandler(this.EntityMngForm_Resize);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.BindingSource Datos;
    }
}