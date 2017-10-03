namespace moleQule.Face
{
  partial class LoginBaseForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginBaseForm));
        this.Cancel = new System.Windows.Forms.Button();
        this.OK = new System.Windows.Forms.Button();
        this.PanelesH = new System.Windows.Forms.SplitContainer();
        this.LogoPictureBox = new System.Windows.Forms.PictureBox();
        this.PasswordTextBox = new System.Windows.Forms.TextBox();
        this.UsernameTextBox = new System.Windows.Forms.TextBox();
        this.PasswordLabel = new System.Windows.Forms.Label();
        this.UsernameLabel = new System.Windows.Forms.Label();
        this.PanelesH.Panel1.SuspendLayout();
        this.PanelesH.Panel2.SuspendLayout();
        this.PanelesH.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
        this.SuspendLayout();
        // 
        // Cancel
        // 
        this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        this.Cancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.Cancel.Image = ((System.Drawing.Image)(resources.GetObject("Cancel.Image")));
        this.Cancel.Location = new System.Drawing.Point(149, 152);
        this.Cancel.Name = "Cancel";
        this.Cancel.Size = new System.Drawing.Size(92, 44);
        this.Cancel.TabIndex = 20;
        this.Cancel.Text = "&Salir";
        this.Cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
        this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
        // 
        // OK
        // 
        this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
        this.OK.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.OK.Image = ((System.Drawing.Image)(resources.GetObject("OK.Image")));
        this.OK.Location = new System.Drawing.Point(35, 152);
        this.OK.Name = "OK";
        this.OK.Size = new System.Drawing.Size(92, 44);
        this.OK.TabIndex = 18;
        this.OK.Text = "&Entrar";
        this.OK.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
        this.OK.Click += new System.EventHandler(this.OK_Click);
        // 
        // PanelesH
        // 
        this.PanelesH.Dock = System.Windows.Forms.DockStyle.Fill;
        this.PanelesH.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
        this.PanelesH.IsSplitterFixed = true;
        this.PanelesH.Location = new System.Drawing.Point(0, 0);
        this.PanelesH.Name = "PanelesH";
        // 
        // PanelesH.Panel1
        // 
        this.PanelesH.Panel1.Controls.Add(this.LogoPictureBox);
        this.PanelesH.Panel1MinSize = 100;
        // 
        // PanelesH.Panel2
        // 
        this.PanelesH.Panel2.Controls.Add(this.PasswordTextBox);
        this.PanelesH.Panel2.Controls.Add(this.Cancel);
        this.PanelesH.Panel2.Controls.Add(this.OK);
        this.PanelesH.Panel2.Controls.Add(this.UsernameTextBox);
        this.PanelesH.Panel2.Controls.Add(this.PasswordLabel);
        this.PanelesH.Panel2.Controls.Add(this.UsernameLabel);
        this.PanelesH.Size = new System.Drawing.Size(377, 226);
        this.PanelesH.SplitterDistance = 100;
        this.PanelesH.SplitterWidth = 1;
        this.PanelesH.TabIndex = 13;
        // 
        // LogoPictureBox
        // 
        this.LogoPictureBox.BackColor = System.Drawing.Color.Transparent;
        this.LogoPictureBox.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LogoPictureBox.BackgroundImage")));
        this.LogoPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        this.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
        this.LogoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("LogoPictureBox.Image")));
        this.LogoPictureBox.Location = new System.Drawing.Point(0, 0);
        this.LogoPictureBox.Name = "LogoPictureBox";
        this.LogoPictureBox.Size = new System.Drawing.Size(100, 226);
        this.LogoPictureBox.TabIndex = 8;
        this.LogoPictureBox.TabStop = false;
        // 
        // PasswordTextBox
        // 
        this.PasswordTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.PasswordTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
        this.PasswordTextBox.Location = new System.Drawing.Point(31, 107);
        this.PasswordTextBox.Name = "PasswordTextBox";
        this.PasswordTextBox.PasswordChar = '*';
        this.PasswordTextBox.Size = new System.Drawing.Size(220, 21);
        this.PasswordTextBox.TabIndex = 14;
        // 
        // UsernameTextBox
        // 
        this.UsernameTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.UsernameTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
        this.UsernameTextBox.Location = new System.Drawing.Point(31, 50);
        this.UsernameTextBox.Name = "UsernameTextBox";
        this.UsernameTextBox.Size = new System.Drawing.Size(220, 21);
        this.UsernameTextBox.TabIndex = 12;
        this.UsernameTextBox.Text = "Admin";
        // 
        // PasswordLabel
        // 
        this.PasswordLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.PasswordLabel.Location = new System.Drawing.Point(29, 87);
        this.PasswordLabel.Name = "PasswordLabel";
        this.PasswordLabel.Size = new System.Drawing.Size(220, 23);
        this.PasswordLabel.TabIndex = 13;
        this.PasswordLabel.Text = "&Contraseña";
        this.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // UsernameLabel
        // 
        this.UsernameLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.UsernameLabel.Location = new System.Drawing.Point(29, 30);
        this.UsernameLabel.Name = "UsernameLabel";
        this.UsernameLabel.Size = new System.Drawing.Size(220, 23);
        this.UsernameLabel.TabIndex = 11;
        this.UsernameLabel.Text = "&Nombre de usuario";
        this.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // LoginBaseForm
        // 
        this.AcceptButton = this.OK;
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.CancelButton = this.Cancel;
        this.ClientSize = new System.Drawing.Size(377, 226);
        this.Controls.Add(this.PanelesH);
        this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "LoginBaseForm";
        this.HelpProvider.SetShowHelp(this, true);
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        this.Text = "Control de Usuarios";
        this.PanelesH.Panel1.ResumeLayout(false);
        this.PanelesH.Panel2.ResumeLayout(false);
        this.PanelesH.Panel2.PerformLayout();
        this.PanelesH.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

      private System.Windows.Forms.SplitContainer PanelesH;
	  internal System.Windows.Forms.Label PasswordLabel;
	  internal System.Windows.Forms.Label UsernameLabel;
      protected System.Windows.Forms.PictureBox LogoPictureBox;
      protected System.Windows.Forms.Button Cancel;
      protected System.Windows.Forms.Button OK;
      protected System.Windows.Forms.TextBox PasswordTextBox;
      protected System.Windows.Forms.TextBox UsernameTextBox;

  }
}