namespace moleQule.Face.Common
{
	partial class InputDecimalForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputDecimalForm));
			this.Value_NTB = new moleQule.Face.Controls.NumericTextBox();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			this.Source_GB.SuspendLayout();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.SuspendLayout();
			// 
			// Submit_BT
			// 
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Source_GB
			// 
			this.Source_GB.Controls.Add(this.Value_NTB);
			this.Source_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.HelpProvider.SetShowHelp(this.Source_GB, true);
			this.Source_GB.Size = new System.Drawing.Size(271, 102);
			this.Source_GB.Text = "Introduzca el importe";
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(324, 203);
			this.PanelesV.SplitterDistance = 163;
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Location = new System.Drawing.Point(-42, 23);
			// 
			// Value_NTB
			// 
			this.Value_NTB.Location = new System.Drawing.Point(85, 41);
			this.Value_NTB.Name = "Value_NTB";
			this.Value_NTB.Size = new System.Drawing.Size(100, 21);
			this.Value_NTB.TabIndex = 0;
			this.Value_NTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.Value_NTB.TextIsCurrency = false;
			this.Value_NTB.TextIsInteger = false;
			// 
			// InputDecimalForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(324, 203);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "InputDecimalForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Solicitud de Datos";
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.Source_GB.ResumeLayout(false);
			this.Source_GB.PerformLayout();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			this.PanelesV.ResumeLayout(false);
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Controls.NumericTextBox Value_NTB;
	}
}
