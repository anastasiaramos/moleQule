namespace moleQule.Face
{
	partial class RestoreInputBaseForm
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.Fichero_TB = new System.Windows.Forms.TextBox();
			this.Examinar_Button = new System.Windows.Forms.Button();
			this.Browser = new System.Windows.Forms.OpenFileDialog();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.Source_GB.SuspendLayout();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.SuspendLayout();
			// 
			// Aceptar_Button
			// 
			this.Submit_BT.Location = new System.Drawing.Point(169, 7);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Location = new System.Drawing.Point(264, 7);
			// 
			// Source_GB
			// 
			this.Source_GB.Controls.Add(this.Examinar_Button);
			this.Source_GB.Controls.Add(this.Fichero_TB);
			this.Source_GB.Location = new System.Drawing.Point(17, 19);
			this.Source_GB.Size = new System.Drawing.Size(486, 72);
			this.Source_GB.Text = "Seleccione la copia a restaurar";
			// 
			// PanelesV
			// 
			this.PanelesV.Size = new System.Drawing.Size(523, 153);
			this.PanelesV.SplitterDistance = 113;
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(313, 0);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 1;
			// 
			// Fichero_TB
			// 
			this.Fichero_TB.Location = new System.Drawing.Point(11, 29);
			this.Fichero_TB.Name = "Fichero_TB";
			this.Fichero_TB.ReadOnly = true;
			this.Fichero_TB.Size = new System.Drawing.Size(433, 21);
			this.Fichero_TB.TabIndex = 7;
			// 
			// Examinar_Button
			// 
			this.Examinar_Button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.Examinar_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Examinar_Button.Location = new System.Drawing.Point(450, 27);
			this.Examinar_Button.Name = "Examinar_Button";
			this.Examinar_Button.Size = new System.Drawing.Size(25, 25);
			this.Examinar_Button.TabIndex = 31;
			this.Examinar_Button.UseVisualStyleBackColor = true;
			this.Examinar_Button.Click += new System.EventHandler(this.Examinar_Button_Click);
			// 
			// Browser
			// 
			this.Browser.DefaultExt = "dmp";
			this.Browser.Filter = "PostgreSQL (*.backup)|*.backup";
			// 
			// RestoreInputForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(523, 153);
			this.Controls.Add(this.textBox1);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "RestoreInputForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Restaurar copia de seguridad";
			this.Controls.SetChildIndex(this.textBox1, 0);
			this.Controls.SetChildIndex(this.PanelesV, 0);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.Source_GB.ResumeLayout(false);
			this.Source_GB.PerformLayout();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox Fichero_TB;
		private System.Windows.Forms.TextBox textBox1;
		protected System.Windows.Forms.Button Examinar_Button;
		private System.Windows.Forms.OpenFileDialog Browser;

	}
}
