namespace moleQule.Face
{
    partial class EntityLMngForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityMngBaseForm));
			this.Datos = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.SuspendLayout();
			// 
			// EntityMngBaseForm
			// 
			this.Name = "EntityMngBaseForm";
			this.Text = "EntityMngBaseForm";
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

    }
}