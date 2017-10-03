namespace moleQule.Face
{
	partial class ProgressInfoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProgressInfoForm));
			this.ProgressInfo_PB = new System.Windows.Forms.ProgressBar();
			this.Animation = new System.Windows.Forms.PictureBox();
			this.ProgressInfo_TB = new System.Windows.Forms.TextBox();
			this.BkWorker = new System.ComponentModel.BackgroundWorker();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.SuspendLayout();
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(22, 31);
			this.ProgressInfo_PB.MarqueeAnimationSpeed = 10;
			this.ProgressInfo_PB.Name = "ProgressInfo_PB";
			this.ProgressInfo_PB.Size = new System.Drawing.Size(296, 23);
			this.ProgressInfo_PB.Step = 1;
			this.ProgressInfo_PB.TabIndex = 0;
			// 
			// Animation
			// 
			this.Animation.Image = ((System.Drawing.Image)(resources.GetObject("Animation.Image")));
			this.Animation.Location = new System.Drawing.Point(346, 12);
			this.Animation.Name = "Animation";
			this.Animation.Size = new System.Drawing.Size(59, 55);
			this.Animation.TabIndex = 23;
			this.Animation.TabStop = false;
			// 
			// ProgressInfo_TB
			// 
			this.ProgressInfo_TB.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.ProgressInfo_TB.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ProgressInfo_TB.Location = new System.Drawing.Point(22, 87);
			this.ProgressInfo_TB.Multiline = true;
			this.ProgressInfo_TB.Name = "ProgressInfo_TB";
			this.ProgressInfo_TB.ReadOnly = true;
			this.ProgressInfo_TB.Size = new System.Drawing.Size(383, 20);
			this.ProgressInfo_TB.TabIndex = 24;
			this.ProgressInfo_TB.TabStop = false;
			this.ProgressInfo_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// BkWorker
			// 
			this.BkWorker.WorkerReportsProgress = true;
			this.BkWorker.WorkerSupportsCancellation = true;
			this.BkWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BkWorker_DoWork);
			this.BkWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BkWorker_ProgressChanged);
			this.BkWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BkWorker_RunWorkerCompleted);
			// 
			// ProgressInfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(426, 157);
			this.ControlBox = false;
			this.Controls.Add(this.ProgressInfo_TB);
			this.Controls.Add(this.Animation);
			this.Controls.Add(this.ProgressInfo_PB);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ProgressInfoForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Tag = "No Format";
			this.Text = "Información de progreso";
			this.Deactivate += new System.EventHandler(this.ProgressInfoForm_Deactivate);
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.ProgressBar ProgressInfo_PB;
		public System.Windows.Forms.PictureBox Animation;
        private System.Windows.Forms.TextBox ProgressInfo_TB;
        public System.ComponentModel.BackgroundWorker BkWorker;
	}
}