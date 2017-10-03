namespace moleQule.Face.Skin02
{
	partial class PrintSelectSkinForm
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
			this.PanelesV = new System.Windows.Forms.SplitContainer();
			this.Source_GB = new System.Windows.Forms.GroupBox();
			this.Todos_RB = new System.Windows.Forms.RadioButton();
			this.Seleccion_RB = new System.Windows.Forms.RadioButton();
			this.Submit_BT = new System.Windows.Forms.Button();
			this.Cancel_BT = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			this.Source_GB.SuspendLayout();
			this.SuspendLayout();
			// 
			// CancelBkJob_BT
			// 
			this.CancelBkJob_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.CancelBkJob_BT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(-62, 29);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(294, 165);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(110, 129);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(110, 44);
			// 
			// PanelesV
			// 
			this.PanelesV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PanelesV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelesV.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.PanelesV.Location = new System.Drawing.Point(0, 0);
			this.PanelesV.Name = "PanelesV";
			this.PanelesV.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.PanelesV.Panel1.Controls.Add(this.Source_GB);
			// 
			// PanelesV.Panel2
			// 
			this.PanelesV.Panel2.BackColor = System.Drawing.SystemColors.Control;
			this.PanelesV.Panel2.Controls.Add(this.Submit_BT);
			this.PanelesV.Panel2.Controls.Add(this.Cancel_BT);
			this.PanelesV.Panel2MinSize = 39;
			this.PanelesV.Size = new System.Drawing.Size(294, 165);
			this.PanelesV.SplitterDistance = 125;
			this.PanelesV.SplitterWidth = 1;
			this.PanelesV.TabIndex = 0;
			// 
			// Source_GB
			// 
			this.Source_GB.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.Source_GB.Controls.Add(this.Todos_RB);
			this.Source_GB.Controls.Add(this.Seleccion_RB);
			this.Source_GB.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.Source_GB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Source_GB.Location = new System.Drawing.Point(33, 24);
			this.Source_GB.Name = "Source_GB";
			this.Source_GB.Size = new System.Drawing.Size(229, 77);
			this.Source_GB.TabIndex = 0;
			this.Source_GB.TabStop = false;
			this.Source_GB.Text = "Origen de Datos";
			this.Source_GB.Validated += new System.EventHandler(this.Source_GB_Validated);
			// 
			// Todos_RB
			// 
			this.Todos_RB.AutoSize = true;
			this.Todos_RB.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.Todos_RB.Checked = true;
			this.Todos_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Todos_RB.Location = new System.Drawing.Point(38, 30);
			this.Todos_RB.Name = "Todos_RB";
			this.Todos_RB.Size = new System.Drawing.Size(54, 17);
			this.Todos_RB.TabIndex = 3;
			this.Todos_RB.TabStop = true;
			this.Todos_RB.Text = "Todos";
			this.Todos_RB.UseVisualStyleBackColor = false;
			// 
			// Seleccion_RB
			// 
			this.Seleccion_RB.AutoSize = true;
			this.Seleccion_RB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Seleccion_RB.Location = new System.Drawing.Point(121, 30);
			this.Seleccion_RB.Name = "Seleccion_RB";
			this.Seleccion_RB.Size = new System.Drawing.Size(69, 17);
			this.Seleccion_RB.TabIndex = 2;
			this.Seleccion_RB.Text = "Selección";
			this.Seleccion_RB.UseVisualStyleBackColor = true;
			// 
			// Submit_BT
			// 
			this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Submit_BT.Location = new System.Drawing.Point(56, 8);
			this.Submit_BT.Name = "Submit_BT";
			this.Submit_BT.Size = new System.Drawing.Size(87, 23);
			this.Submit_BT.TabIndex = 200;
			this.Submit_BT.Text = "&Aceptar";
			this.Submit_BT.UseVisualStyleBackColor = true;
			this.Submit_BT.Click += new System.EventHandler(this.Aceptar_Button_Click);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Cancel_BT.Location = new System.Drawing.Point(151, 8);
			this.Cancel_BT.Name = "Cancel_BT";
			this.Cancel_BT.Size = new System.Drawing.Size(87, 23);
			this.Cancel_BT.TabIndex = 201;
			this.Cancel_BT.Text = "&Cancelar";
			this.Cancel_BT.UseVisualStyleBackColor = true;
			this.Cancel_BT.Click += new System.EventHandler(this.Cancelar_Button_Click);
			// 
			// PrintSelectSkinForm
			// 
			this.AcceptButton = this.Submit_BT;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Cancel_BT;
			this.ClientSize = new System.Drawing.Size(294, 165);
			this.Controls.Add(this.PanelesV);
			this.HelpProvider.SetHelpKeyword(this, "45");
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Name = "PrintSelectSkinForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
			this.Controls.SetChildIndex(this.PanelesV, 0);
			this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
			this.Controls.SetChildIndex(this.Progress_PB, 0);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
			this.PanelesV.ResumeLayout(false);
			this.Source_GB.ResumeLayout(false);
			this.Source_GB.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer PanelesV;
		protected System.Windows.Forms.Button Submit_BT;
		protected System.Windows.Forms.Button Cancel_BT;
        protected System.Windows.Forms.GroupBox Source_GB;
        protected System.Windows.Forms.RadioButton Seleccion_RB;
        protected System.Windows.Forms.RadioButton Todos_RB;

	}
}
