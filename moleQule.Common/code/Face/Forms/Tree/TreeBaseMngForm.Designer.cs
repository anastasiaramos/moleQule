namespace moleQule.Face.Common
{
	partial class TreeBaseMngForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeBaseMngForm));
			this.Browser = new System.Windows.Forms.FolderBrowserDialog();
			((System.ComponentModel.ISupportInitialize)(this.Base_Panel)).BeginInit();
			this.Base_Panel.Panel1.SuspendLayout();
			this.Base_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Tree_Panel)).BeginInit();
			this.Tree_Panel.Panel1.SuspendLayout();
			this.Tree_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DatosSearch)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			this.SuspendLayout();
			// 
			// Base_Panel
			// 
			this.Base_Panel.Size = new System.Drawing.Size(294, 576);
			// 
			// Tree_Panel
			// 
			this.Tree_Panel.Size = new System.Drawing.Size(294, 576);
			// 
			// Tree_TV
			// 
			this.Tree_TV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Tree_TV.LineColor = System.Drawing.Color.Black;
			this.Tree_TV.Size = new System.Drawing.Size(294, 537);
			// 
			// Nodes_IL
			// 
			this.Nodes_IL.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("Nodes_IL.ImageStream")));
			this.Nodes_IL.Images.SetKeyName(0, "Selected");
			this.Nodes_IL.Images.SetKeyName(1, "Open");
			this.Nodes_IL.Images.SetKeyName(2, "Close");
			// 
			// CancelBkJob_BT
			// 
			this.CancelBkJob_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(-62, 179);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(294, 576);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(110, 334);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(110, 249);
			// 
			// TreeBaseMngForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.ClientSize = new System.Drawing.Size(294, 576);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.HelpProvider.SetHelpKeyword(this, "30");
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TreeBaseMngForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "TreeBaseMngForm";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NotificacionBaseMngForm_FormClosing);
			this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
			this.Controls.SetChildIndex(this.Base_Panel, 0);
			this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
			this.Controls.SetChildIndex(this.Progress_PB, 0);
			this.Base_Panel.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Base_Panel)).EndInit();
			this.Base_Panel.ResumeLayout(false);
			this.Tree_Panel.Panel1.ResumeLayout(false);
			this.Tree_Panel.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Tree_Panel)).EndInit();
			this.Tree_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DatosSearch)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected System.Windows.Forms.FolderBrowserDialog Browser;
		public System.Windows.Forms.ImageList Nodos_IL;
	}
}
