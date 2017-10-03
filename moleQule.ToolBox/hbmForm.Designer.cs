namespace moleQule.ToolBox
{
	partial class hbmForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(hbmForm));
            this.Browser = new System.Windows.Forms.OpenFileDialog();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.Cancelar_Button = new System.Windows.Forms.Button();
            this.Aceptar_Button = new System.Windows.Forms.Button();
            this.Model_TB = new System.Windows.Forms.TextBox();
            this.Examinar_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Copy_TB = new System.Windows.Forms.TextBox();
            this.Folder_Button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Browser
            // 
            this.Browser.FileName = "openFileDialog1";
            // 
            // Cancelar_Button
            // 
            this.Cancelar_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancelar_Button.Location = new System.Drawing.Point(225, 229);
            this.Cancelar_Button.Name = "Cancelar_Button";
            this.Cancelar_Button.Size = new System.Drawing.Size(75, 23);
            this.Cancelar_Button.TabIndex = 0;
            this.Cancelar_Button.Text = "&Cancelar";
            this.Cancelar_Button.UseVisualStyleBackColor = true;
            this.Cancelar_Button.Click += new System.EventHandler(this.Cancelar_Button_Click);
            // 
            // Aceptar_Button
            // 
            this.Aceptar_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aceptar_Button.Location = new System.Drawing.Point(144, 229);
            this.Aceptar_Button.Name = "Aceptar_Button";
            this.Aceptar_Button.Size = new System.Drawing.Size(75, 23);
            this.Aceptar_Button.TabIndex = 1;
            this.Aceptar_Button.Text = "&Aceptar";
            this.Aceptar_Button.UseVisualStyleBackColor = true;
            this.Aceptar_Button.Click += new System.EventHandler(this.Aceptar_Button_Click);
            // 
            // Model_TB
            // 
            this.Model_TB.Location = new System.Drawing.Point(47, 62);
            this.Model_TB.Name = "Model_TB";
            this.Model_TB.Size = new System.Drawing.Size(326, 20);
            this.Model_TB.TabIndex = 2;
            // 
            // Examinar_Button
            // 
            this.Examinar_Button.BackgroundImage = global::moleQule.ToolBox.Properties.Resources.search;
            this.Examinar_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Examinar_Button.Location = new System.Drawing.Point(379, 60);
            this.Examinar_Button.Name = "Examinar_Button";
            this.Examinar_Button.Size = new System.Drawing.Size(25, 25);
            this.Examinar_Button.TabIndex = 3;
            this.Examinar_Button.Text = "...";
            this.Examinar_Button.UseVisualStyleBackColor = true;
            this.Examinar_Button.Click += new System.EventHandler(this.Examinar_Button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Carpeta origen del modelo:";
            // 
            // Copy_TB
            // 
            this.Copy_TB.Location = new System.Drawing.Point(47, 147);
            this.Copy_TB.Name = "Copy_TB";
            this.Copy_TB.Size = new System.Drawing.Size(326, 20);
            this.Copy_TB.TabIndex = 5;
            // 
            // Folder_Button
            // 
            this.Folder_Button.BackgroundImage = global::moleQule.ToolBox.Properties.Resources.search;
            this.Folder_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Folder_Button.Location = new System.Drawing.Point(379, 145);
            this.Folder_Button.Name = "Folder_Button";
            this.Folder_Button.Size = new System.Drawing.Size(25, 25);
            this.Folder_Button.TabIndex = 6;
            this.Folder_Button.Text = "...";
            this.Folder_Button.UseVisualStyleBackColor = true;
            this.Folder_Button.Click += new System.EventHandler(this.Folder_Button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Carpeta de salida:";
            // 
            // hbmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 266);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Folder_Button);
            this.Controls.Add(this.Copy_TB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Examinar_Button);
            this.Controls.Add(this.Model_TB);
            this.Controls.Add(this.Aceptar_Button);
            this.Controls.Add(this.Cancelar_Button);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "hbmForm";
            this.Text = "Creación de Ficheros de Configuración de nHibernate";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.OpenFileDialog Browser;
		private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.Button Cancelar_Button;
        private System.Windows.Forms.Button Aceptar_Button;
        private System.Windows.Forms.TextBox Model_TB;
        private System.Windows.Forms.Button Examinar_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Copy_TB;
        private System.Windows.Forms.Button Folder_Button;
        private System.Windows.Forms.Label label2;
	}
}