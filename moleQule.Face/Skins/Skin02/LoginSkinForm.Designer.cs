namespace moleQule.Face.Skin02
{
  partial class LoginSkinForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginSkinForm));
			this.PanelesH = new System.Windows.Forms.SplitContainer();
			this.LogoPictureBox = new System.Windows.Forms.PictureBox();
			this.Server_CkB = new System.Windows.Forms.CheckBox();
			this.Server_TB = new System.Windows.Forms.TextBox();
			this.Language_GB = new System.Windows.Forms.GroupBox();
			this.English_RB = new System.Windows.Forms.RadioButton();
			this.Spanish_RB = new System.Windows.Forms.RadioButton();
			this.label1 = new System.Windows.Forms.Label();
			this.Password_TB = new System.Windows.Forms.TextBox();
			this.Cancel_BT = new System.Windows.Forms.Button();
			this.Ok_BT = new System.Windows.Forms.Button();
			this.UserName_TB = new System.Windows.Forms.TextBox();
			this.PasswordLabel = new System.Windows.Forms.Label();
			this.UsernameLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PanelesH)).BeginInit();
			this.PanelesH.Panel1.SuspendLayout();
			this.PanelesH.Panel2.SuspendLayout();
			this.PanelesH.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
			this.Language_GB.SuspendLayout();
			this.SuspendLayout();
			// 
			// _parent
			// 
			resources.ApplyResources(this._parent, "_parent");
			this._parent.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			// 
			// HelpProvider
			// 
			resources.ApplyResources(this.HelpProvider, "HelpProvider");
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
			// PanelesH
			// 
			resources.ApplyResources(this.PanelesH, "PanelesH");
			this.PanelesH.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.PanelesH.Name = "PanelesH";
			// 
			// PanelesH.Panel1
			// 
			this.PanelesH.Panel1.Controls.Add(this.LogoPictureBox);
			this.HelpProvider.SetShowHelp(this.PanelesH.Panel1, ((bool)(resources.GetObject("PanelesH.Panel1.ShowHelp"))));
			// 
			// PanelesH.Panel2
			// 
			this.PanelesH.Panel2.Controls.Add(this.Server_CkB);
			this.PanelesH.Panel2.Controls.Add(this.Server_TB);
			this.PanelesH.Panel2.Controls.Add(this.Language_GB);
			this.PanelesH.Panel2.Controls.Add(this.label1);
			this.PanelesH.Panel2.Controls.Add(this.Password_TB);
			this.PanelesH.Panel2.Controls.Add(this.Cancel_BT);
			this.PanelesH.Panel2.Controls.Add(this.Ok_BT);
			this.PanelesH.Panel2.Controls.Add(this.UserName_TB);
			this.PanelesH.Panel2.Controls.Add(this.PasswordLabel);
			this.PanelesH.Panel2.Controls.Add(this.UsernameLabel);
			this.HelpProvider.SetShowHelp(this.PanelesH.Panel2, ((bool)(resources.GetObject("PanelesH.Panel2.ShowHelp"))));
			this.HelpProvider.SetShowHelp(this.PanelesH, ((bool)(resources.GetObject("PanelesH.ShowHelp"))));
			// 
			// LogoPictureBox
			// 
			this.LogoPictureBox.BackColor = System.Drawing.Color.Gainsboro;
			this.LogoPictureBox.BackgroundImage = global::moleQule.Face.Properties.Resources.shield;
			resources.ApplyResources(this.LogoPictureBox, "LogoPictureBox");
			this.LogoPictureBox.Name = "LogoPictureBox";
			this.HelpProvider.SetShowHelp(this.LogoPictureBox, ((bool)(resources.GetObject("LogoPictureBox.ShowHelp"))));
			this.LogoPictureBox.TabStop = false;
			// 
			// Server_CkB
			// 
			resources.ApplyResources(this.Server_CkB, "Server_CkB");
			this.Server_CkB.Name = "Server_CkB";
			this.HelpProvider.SetShowHelp(this.Server_CkB, ((bool)(resources.GetObject("Server_CkB.ShowHelp"))));
			this.Server_CkB.UseVisualStyleBackColor = true;
			this.Server_CkB.CheckedChanged += new System.EventHandler(this.Server_CkB_CheckedChanged);
			// 
			// Server_TB
			// 
			resources.ApplyResources(this.Server_TB, "Server_TB");
			this.Server_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.Server_TB.Name = "Server_TB";
			this.Server_TB.ReadOnly = true;
			this.HelpProvider.SetShowHelp(this.Server_TB, ((bool)(resources.GetObject("Server_TB.ShowHelp"))));
			// 
			// Language_GB
			// 
			this.Language_GB.Controls.Add(this.English_RB);
			this.Language_GB.Controls.Add(this.Spanish_RB);
			resources.ApplyResources(this.Language_GB, "Language_GB");
			this.Language_GB.Name = "Language_GB";
			this.HelpProvider.SetShowHelp(this.Language_GB, ((bool)(resources.GetObject("Language_GB.ShowHelp"))));
			this.Language_GB.TabStop = false;
			// 
			// English_RB
			// 
			this.English_RB.Image = global::moleQule.Face.Properties.Resources.United_Kingdom;
			resources.ApplyResources(this.English_RB, "English_RB");
			this.English_RB.Name = "English_RB";
			this.HelpProvider.SetShowHelp(this.English_RB, ((bool)(resources.GetObject("English_RB.ShowHelp"))));
			this.English_RB.UseVisualStyleBackColor = true;
			this.English_RB.CheckedChanged += new System.EventHandler(this.English_RB_CheckedChanged);
			// 
			// Spanish_RB
			// 
			this.Spanish_RB.Checked = true;
			this.Spanish_RB.Image = global::moleQule.Face.Properties.Resources.Spain;
			resources.ApplyResources(this.Spanish_RB, "Spanish_RB");
			this.Spanish_RB.Name = "Spanish_RB";
			this.HelpProvider.SetShowHelp(this.Spanish_RB, ((bool)(resources.GetObject("Spanish_RB.ShowHelp"))));
			this.Spanish_RB.TabStop = true;
			this.Spanish_RB.UseVisualStyleBackColor = true;
			this.Spanish_RB.CheckedChanged += new System.EventHandler(this.Spanish_RB_CheckedChanged);
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			this.HelpProvider.SetShowHelp(this.label1, ((bool)(resources.GetObject("label1.ShowHelp"))));
			// 
			// Password_TB
			// 
			resources.ApplyResources(this.Password_TB, "Password_TB");
			this.Password_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.Password_TB.Name = "Password_TB";
			this.HelpProvider.SetShowHelp(this.Password_TB, ((bool)(resources.GetObject("Password_TB.ShowHelp"))));
			// 
			// Cancel_BT
			// 
			resources.ApplyResources(this.Cancel_BT, "Cancel_BT");
			this.Cancel_BT.Image = global::moleQule.Face.Properties.Resources.Cerrar;
			this.Cancel_BT.Name = "Cancel_BT";
			this.HelpProvider.SetShowHelp(this.Cancel_BT, ((bool)(resources.GetObject("Cancel_BT.ShowHelp"))));
			this.Cancel_BT.Click += new System.EventHandler(this.Cancel_BT_Click);
			// 
			// Ok_BT
			// 
			resources.ApplyResources(this.Ok_BT, "Ok_BT");
			this.Ok_BT.Image = global::moleQule.Face.Properties.Resources.login;
			this.Ok_BT.Name = "Ok_BT";
			this.HelpProvider.SetShowHelp(this.Ok_BT, ((bool)(resources.GetObject("Ok_BT.ShowHelp"))));
			this.Ok_BT.Click += new System.EventHandler(this.OK_BT_Click);
			// 
			// UserName_TB
			// 
			resources.ApplyResources(this.UserName_TB, "UserName_TB");
			this.UserName_TB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.UserName_TB.Name = "UserName_TB";
			this.HelpProvider.SetShowHelp(this.UserName_TB, ((bool)(resources.GetObject("UserName_TB.ShowHelp"))));
			// 
			// PasswordLabel
			// 
			resources.ApplyResources(this.PasswordLabel, "PasswordLabel");
			this.PasswordLabel.Name = "PasswordLabel";
			this.HelpProvider.SetShowHelp(this.PasswordLabel, ((bool)(resources.GetObject("PasswordLabel.ShowHelp"))));
			// 
			// UsernameLabel
			// 
			resources.ApplyResources(this.UsernameLabel, "UsernameLabel");
			this.UsernameLabel.Name = "UsernameLabel";
			this.HelpProvider.SetShowHelp(this.UsernameLabel, ((bool)(resources.GetObject("UsernameLabel.ShowHelp"))));
			// 
			// LoginSkinForm
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ControlBox = false;
			this.Controls.Add(this.PanelesH);
			this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
			this.Name = "LoginSkinForm";
			this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
			this.Controls.SetChildIndex(this.PanelesH, 0);
			this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
			this.Controls.SetChildIndex(this.Progress_PB, 0);
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.PanelesH.Panel1.ResumeLayout(false);
			this.PanelesH.Panel2.ResumeLayout(false);
			this.PanelesH.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PanelesH)).EndInit();
			this.PanelesH.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
			this.Language_GB.ResumeLayout(false);
			this.ResumeLayout(false);

    }

    #endregion

	  private System.Windows.Forms.SplitContainer PanelesH;
      protected System.Windows.Forms.PictureBox LogoPictureBox;
      protected System.Windows.Forms.Button Cancel_BT;
      protected System.Windows.Forms.Button Ok_BT;
      protected System.Windows.Forms.TextBox Password_TB;
      protected System.Windows.Forms.TextBox UserName_TB;
	  protected System.Windows.Forms.Label PasswordLabel;
	  protected System.Windows.Forms.Label UsernameLabel;
	  protected System.Windows.Forms.TextBox Server_TB;
	  protected System.Windows.Forms.Label label1;
	  protected System.Windows.Forms.CheckBox Server_CkB;
	  private System.Windows.Forms.GroupBox Language_GB;
	  private System.Windows.Forms.RadioButton Spanish_RB;
	  private System.Windows.Forms.RadioButton English_RB;

  }
}