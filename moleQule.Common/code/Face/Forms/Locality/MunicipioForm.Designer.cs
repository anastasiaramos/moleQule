namespace moleQule.Face.Common
{
    partial class MunicipioForm
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
			System.Windows.Forms.Label municipioLabel;
			System.Windows.Forms.Label provinciaLabel;
			System.Windows.Forms.Label poblacionLabel;
			System.Windows.Forms.Label codigoPostalLabel;
			System.Windows.Forms.Label label1;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MunicipioForm));
			this.Municipio_TB = new System.Windows.Forms.TextBox();
			this.Provincia_TB = new System.Windows.Forms.TextBox();
			this.Poblacion_TB = new System.Windows.Forms.TextBox();
			this.CodigoPostal_TB = new System.Windows.Forms.TextBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			municipioLabel = new System.Windows.Forms.Label();
			provinciaLabel = new System.Windows.Forms.Label();
			poblacionLabel = new System.Windows.Forms.Label();
			codigoPostalLabel = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).BeginInit();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			this.SuspendLayout();
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.Controls.Add(label1);
			this.PanelesV.Panel1.Controls.Add(this.textBox1);
			this.PanelesV.Panel1.Controls.Add(this.Municipio_TB);
			this.PanelesV.Panel1.Controls.Add(municipioLabel);
			this.PanelesV.Panel1.Controls.Add(provinciaLabel);
			this.PanelesV.Panel1.Controls.Add(this.Provincia_TB);
			this.PanelesV.Panel1.Controls.Add(poblacionLabel);
			this.PanelesV.Panel1.Controls.Add(this.Poblacion_TB);
			this.PanelesV.Panel1.Controls.Add(codigoPostalLabel);
			this.PanelesV.Panel1.Controls.Add(this.CodigoPostal_TB);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(592, 323);
			this.PanelesV.SplitterDistance = 277;
			// 
			// Submit_BT
			// 
			this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Submit_BT.Location = new System.Drawing.Point(252, 6);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Cancel_BT.Location = new System.Drawing.Point(348, 6);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Paneles2
			// 
			// 
			// Paneles2.Panel1
			// 
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, true);
			// 
			// Paneles2.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, true);
			this.HelpProvider.SetShowHelp(this.Paneles2, true);
			this.Paneles2.Size = new System.Drawing.Size(590, 43);
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Imprimir_Button.Location = new System.Drawing.Point(156, 6);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Docs_BT.Location = new System.Drawing.Point(201, 6);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Library.Common.Municipio);
			this.Datos.DataSourceChanged += new System.EventHandler(this.Datos_DataSourceChanged);
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(87, 96);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(592, 323);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(259, 210);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(259, 125);
			// 
			// municipioLabel
			// 
			municipioLabel.AutoSize = true;
			municipioLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			municipioLabel.Location = new System.Drawing.Point(59, 130);
			municipioLabel.Name = "municipioLabel";
			municipioLabel.Size = new System.Drawing.Size(54, 13);
			municipioLabel.TabIndex = 23;
			municipioLabel.Text = "Municipio:";
			// 
			// provinciaLabel
			// 
			provinciaLabel.AutoSize = true;
			provinciaLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			provinciaLabel.Location = new System.Drawing.Point(59, 157);
			provinciaLabel.Name = "provinciaLabel";
			provinciaLabel.Size = new System.Drawing.Size(54, 13);
			provinciaLabel.TabIndex = 22;
			provinciaLabel.Text = "Provincia:";
			// 
			// poblacionLabel
			// 
			poblacionLabel.AutoSize = true;
			poblacionLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			poblacionLabel.Location = new System.Drawing.Point(59, 103);
			poblacionLabel.Name = "poblacionLabel";
			poblacionLabel.Size = new System.Drawing.Size(55, 13);
			poblacionLabel.TabIndex = 20;
			poblacionLabel.Text = "Localidad:";
			// 
			// codigoPostalLabel
			// 
			codigoPostalLabel.AutoSize = true;
			codigoPostalLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			codigoPostalLabel.Location = new System.Drawing.Point(59, 76);
			codigoPostalLabel.Name = "codigoPostalLabel";
			codigoPostalLabel.Size = new System.Drawing.Size(76, 13);
			codigoPostalLabel.TabIndex = 17;
			codigoPostalLabel.Text = "Código Postal:";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label1.Location = new System.Drawing.Point(59, 184);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(30, 13);
			label1.TabIndex = 25;
			label1.Text = "País:";
			// 
			// Municipio_TB
			// 
			this.Municipio_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Nombre", true));
			this.Municipio_TB.Location = new System.Drawing.Point(120, 127);
			this.Municipio_TB.Name = "Municipio_TB";
			this.Municipio_TB.Size = new System.Drawing.Size(411, 21);
			this.Municipio_TB.TabIndex = 2;
			// 
			// Provincia_TB
			// 
			this.Provincia_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Provincia", true));
			this.Provincia_TB.Location = new System.Drawing.Point(119, 154);
			this.Provincia_TB.Name = "Provincia_TB";
			this.Provincia_TB.Size = new System.Drawing.Size(411, 21);
			this.Provincia_TB.TabIndex = 3;
			// 
			// Poblacion_TB
			// 
			this.Poblacion_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Localidad", true));
			this.Poblacion_TB.Location = new System.Drawing.Point(120, 100);
			this.Poblacion_TB.Name = "Poblacion_TB";
			this.Poblacion_TB.Size = new System.Drawing.Size(411, 21);
			this.Poblacion_TB.TabIndex = 1;
			// 
			// CodigoPostal_TB
			// 
			this.CodigoPostal_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CodPostal", true));
			this.CodigoPostal_TB.Location = new System.Drawing.Point(141, 73);
			this.CodigoPostal_TB.Name = "CodigoPostal_TB";
			this.CodigoPostal_TB.Size = new System.Drawing.Size(71, 21);
			this.CodigoPostal_TB.TabIndex = 0;
			this.CodigoPostal_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// textBox1
			// 
			this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Pais", true));
			this.textBox1.Location = new System.Drawing.Point(119, 181);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(169, 21);
			this.textBox1.TabIndex = 4;
			// 
			// MunicipioForm
			// 
			this.ClientSize = new System.Drawing.Size(592, 323);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MunicipioForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "MunicipioForm";
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel1.PerformLayout();
			this.PanelesV.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).EndInit();
			this.Paneles2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TextBox Municipio_TB;
        protected System.Windows.Forms.TextBox Provincia_TB;
        protected System.Windows.Forms.TextBox Poblacion_TB;
        protected System.Windows.Forms.TextBox CodigoPostal_TB;
		protected System.Windows.Forms.TextBox textBox1;
		
		

    }
}
