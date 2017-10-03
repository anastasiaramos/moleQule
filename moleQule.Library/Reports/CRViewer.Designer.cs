namespace moleQule.Library.Reports
{
	partial class CRViewer
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
			this.Visor = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.SuspendLayout();
			// 
			// Visor
			// 
			this.Visor.ActiveViewIndex = -1;
			this.Visor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Visor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Visor.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Visor.Location = new System.Drawing.Point(0, 0);
			this.Visor.Name = "Visor";
			this.Visor.SelectionFormula = "";
			this.Visor.Size = new System.Drawing.Size(490, 431);
			this.Visor.TabIndex = 0;
			this.Visor.ViewTimeSelectionFormula = "";
			// 
			// CRViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(490, 431);
			this.Controls.Add(this.Visor);
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "CRViewer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Vista Previa";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);

		}

		#endregion

		protected CrystalDecisions.Windows.Forms.CrystalReportViewer Visor;

	}
}