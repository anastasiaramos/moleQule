namespace moleQule.Face
{
    partial class EntityMngBaseForm
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
			this.DatosSearch = new System.Windows.Forms.BindingSource(this.components);
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DatosSearch)).BeginInit();
			this.SuspendLayout();
			// 
			// Animation
			// 
			resources.ApplyResources(this.Animation, "Animation");
			// 
			// CancelBkJob_BT
			// 
			resources.ApplyResources(this.CancelBkJob_BT, "CancelBkJob_BT");
			this.CancelBkJob_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			// 
			// ProgressMsg_LB
			// 
			resources.ApplyResources(this.ProgressMsg_LB, "ProgressMsg_LB");
			// 
			// Title_LB
			// 
			resources.ApplyResources(this.Title_LB, "Title_LB");
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
			// Datos
			// 
			this.Datos.DataSourceChanged += new System.EventHandler(this.Datos_DataSourceChanged);
			// 
			// DatosSearch
			// 
			this.DatosSearch.CurrentChanged += new System.EventHandler(this.DatosSearch_CurrentChanged);
			// 
			// EntityMngBaseForm
			// 
			resources.ApplyResources(this, "$this");
			this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
			this.Name = "EntityMngBaseForm";
			this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EntityMngBaseForm_FormClosed);
			this.Shown += new System.EventHandler(this.EntityMngBaseForm_Shown);
			this.Layout += new System.Windows.Forms.LayoutEventHandler(this.EntityMngBaseForm_Layout);
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DatosSearch)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.BindingSource Datos;
		protected System.Windows.Forms.BindingSource DatosSearch;
    }
}