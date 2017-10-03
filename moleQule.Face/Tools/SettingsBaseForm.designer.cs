namespace moleQule.Face
{
    partial class SettingsBaseForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsBaseForm));
			System.Windows.Forms.Label numeroClienteLabel;
			System.Windows.Forms.Label Servidores;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label label5;
			this.Settings_TP = new System.Windows.Forms.TabControl();
			this.General_TP = new System.Windows.Forms.TabPage();
			this.Design_GB = new System.Windows.Forms.GroupBox();
			this.ShowNullRecords_CkB = new System.Windows.Forms.CheckBox();
			this.FormatGrids_CkB = new System.Windows.Forms.CheckBox();
			this.Idioma_GB = new System.Windows.Forms.GroupBox();
			this.English_RB = new System.Windows.Forms.RadioButton();
			this.Spanish_RB = new System.Windows.Forms.RadioButton();
			this.Predeterminado_RB = new System.Windows.Forms.RadioButton();
			this.Backups_TP = new System.Windows.Forms.TabPage();
			this.Backups_GB = new System.Windows.Forms.GroupBox();
			this.BackupsHour_DTP = new System.Windows.Forms.DateTimePicker();
			this.label23 = new System.Windows.Forms.Label();
			this.BackupsLastDate_DTP = new System.Windows.Forms.DateTimePicker();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.BackupsDays_TB = new System.Windows.Forms.TextBox();
			this.Hosts_TP = new System.Windows.Forms.TabPage();
			this.SMTP_GB = new System.Windows.Forms.GroupBox();
			this.SMTPEnableSSL_CkB = new System.Windows.Forms.CheckBox();
			this.SMTPMail_TB = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.MostrarPassword_CkB = new System.Windows.Forms.CheckBox();
			this.SMTPPwd_TB = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SMTPUser_TB = new System.Windows.Forms.TextBox();
			this.SMTPPort_TB = new System.Windows.Forms.TextBox();
			this.SMTPHost_TB = new System.Windows.Forms.TextBox();
			this.Folders_GB = new System.Windows.Forms.GroupBox();
			this.PDFPrintsFolder_TB = new System.Windows.Forms.TextBox();
			this.PDFPrinstFolder_BT = new System.Windows.Forms.Button();
			this.DBHosts_GB = new System.Windows.Forms.GroupBox();
			this.WANHost_TB = new System.Windows.Forms.TextBox();
			this.LANHost_TB = new System.Windows.Forms.TextBox();
			this.FilesHost_GB = new System.Windows.Forms.GroupBox();
			this.FilesHost_TB = new System.Windows.Forms.TextBox();
			this.Browser = new System.Windows.Forms.FolderBrowserDialog();
			numeroClienteLabel = new System.Windows.Forms.Label();
			Servidores = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			this.Settings_TP.SuspendLayout();
			this.General_TP.SuspendLayout();
			this.Design_GB.SuspendLayout();
			this.Idioma_GB.SuspendLayout();
			this.Backups_TP.SuspendLayout();
			this.Backups_GB.SuspendLayout();
			this.Hosts_TP.SuspendLayout();
			this.SMTP_GB.SuspendLayout();
			this.Folders_GB.SuspendLayout();
			this.DBHosts_GB.SuspendLayout();
			this.FilesHost_GB.SuspendLayout();
			this.SuspendLayout();
			// 
			// Submit_BT
			// 
			resources.ApplyResources(this.Submit_BT, "Submit_BT");
			this.ErrorMng_EP.SetError(this.Submit_BT, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Submit_BT, resources.GetString("Submit_BT.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Submit_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Submit_BT.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Submit_BT, resources.GetString("Submit_BT.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Submit_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Submit_BT.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Submit_BT, ((int)(resources.GetObject("Submit_BT.IconPadding"))));
			this.Submit_BT.ImageKey = string.Empty;
			this.HelpProvider.SetShowHelp(this.Submit_BT, ((bool)(resources.GetObject("Submit_BT.ShowHelp"))));
			// 
			// Cancel_BT
			// 
			resources.ApplyResources(this.Cancel_BT, "Cancel_BT");
			this.ErrorMng_EP.SetError(this.Cancel_BT, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Cancel_BT, resources.GetString("Cancel_BT.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Cancel_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Cancel_BT.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Cancel_BT, resources.GetString("Cancel_BT.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Cancel_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Cancel_BT.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Cancel_BT, ((int)(resources.GetObject("Cancel_BT.IconPadding"))));
			this.Cancel_BT.ImageKey = string.Empty;
			this.HelpProvider.SetShowHelp(this.Cancel_BT, ((bool)(resources.GetObject("Cancel_BT.ShowHelp"))));
			// 
			// Source_GB
			// 
			resources.ApplyResources(this.Source_GB, "Source_GB");
			this.ErrorMng_EP.SetError(this.Source_GB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Source_GB, resources.GetString("Source_GB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Source_GB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Source_GB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Source_GB, resources.GetString("Source_GB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Source_GB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Source_GB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Source_GB, ((int)(resources.GetObject("Source_GB.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Source_GB, ((bool)(resources.GetObject("Source_GB.ShowHelp"))));
			// 
			// PanelesV
			// 
			resources.ApplyResources(this.PanelesV, "PanelesV");
			this.ErrorMng_EP.SetError(this.PanelesV, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.PanelesV, resources.GetString("PanelesV.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.PanelesV, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("PanelesV.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.PanelesV, resources.GetString("PanelesV.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.PanelesV, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("PanelesV.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.PanelesV, ((int)(resources.GetObject("PanelesV.IconPadding"))));
			// 
			// PanelesV.Panel1
			// 
			resources.ApplyResources(this.PanelesV.Panel1, "PanelesV.Panel1");
			this.PanelesV.Panel1.Controls.Add(this.Settings_TP);
			this.ErrorMng_EP.SetError(this.PanelesV.Panel1, resources.GetString("PanelesV.Panel1.Error"));
			this.HelpProvider.SetHelpKeyword(this.PanelesV.Panel1, resources.GetString("PanelesV.Panel1.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.PanelesV.Panel1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("PanelesV.Panel1.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.PanelesV.Panel1, resources.GetString("PanelesV.Panel1.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.PanelesV.Panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("PanelesV.Panel1.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.PanelesV.Panel1, ((int)(resources.GetObject("PanelesV.Panel1.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, ((bool)(resources.GetObject("PanelesV.Panel1.ShowHelp"))));
			// 
			// PanelesV.Panel2
			// 
			resources.ApplyResources(this.PanelesV.Panel2, "PanelesV.Panel2");
			this.ErrorMng_EP.SetError(this.PanelesV.Panel2, resources.GetString("PanelesV.Panel2.Error"));
			this.HelpProvider.SetHelpKeyword(this.PanelesV.Panel2, resources.GetString("PanelesV.Panel2.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.PanelesV.Panel2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("PanelesV.Panel2.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.PanelesV.Panel2, resources.GetString("PanelesV.Panel2.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.PanelesV.Panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("PanelesV.Panel2.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.PanelesV.Panel2, ((int)(resources.GetObject("PanelesV.Panel2.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, ((bool)(resources.GetObject("PanelesV.Panel2.ShowHelp"))));
			this.HelpProvider.SetShowHelp(this.PanelesV, ((bool)(resources.GetObject("PanelesV.ShowHelp"))));
			// 
			// SaveFile_SFD
			// 
			this.SaveFile_SFD.Filter = string.Empty;
			this.SaveFile_SFD.Title = string.Empty;
			// 
			// ErrorMng_EP
			// 
			resources.ApplyResources(this.ErrorMng_EP, "ErrorMng_EP");
			// 
			// HelpProvider
			// 
			resources.ApplyResources(this.HelpProvider, "HelpProvider");
			// 
			// Animation
			// 
			resources.ApplyResources(this.Animation, "Animation");
			this.ErrorMng_EP.SetError(this.Animation, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Animation, resources.GetString("Animation.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Animation, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Animation.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Animation, resources.GetString("Animation.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Animation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Animation.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Animation, ((int)(resources.GetObject("Animation.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Animation, ((bool)(resources.GetObject("Animation.ShowHelp"))));
			// 
			// CancelBkJob_BT
			// 
			resources.ApplyResources(this.CancelBkJob_BT, "CancelBkJob_BT");
			this.ErrorMng_EP.SetError(this.CancelBkJob_BT, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.CancelBkJob_BT, resources.GetString("CancelBkJob_BT.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.CancelBkJob_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("CancelBkJob_BT.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.CancelBkJob_BT, resources.GetString("CancelBkJob_BT.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.CancelBkJob_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("CancelBkJob_BT.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.CancelBkJob_BT, ((int)(resources.GetObject("CancelBkJob_BT.IconPadding"))));
			this.CancelBkJob_BT.ImageKey = string.Empty;
			this.HelpProvider.SetShowHelp(this.CancelBkJob_BT, ((bool)(resources.GetObject("CancelBkJob_BT.ShowHelp"))));
			// 
			// ProgressMsg_LB
			// 
			resources.ApplyResources(this.ProgressMsg_LB, "ProgressMsg_LB");
			this.ErrorMng_EP.SetError(this.ProgressMsg_LB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.ProgressMsg_LB, resources.GetString("ProgressMsg_LB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.ProgressMsg_LB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("ProgressMsg_LB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.ProgressMsg_LB, resources.GetString("ProgressMsg_LB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.ProgressMsg_LB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("ProgressMsg_LB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.ProgressMsg_LB, ((int)(resources.GetObject("ProgressMsg_LB.IconPadding"))));
			this.ProgressMsg_LB.ImageKey = string.Empty;
			this.HelpProvider.SetShowHelp(this.ProgressMsg_LB, ((bool)(resources.GetObject("ProgressMsg_LB.ShowHelp"))));
			// 
			// Title_LB
			// 
			resources.ApplyResources(this.Title_LB, "Title_LB");
			this.ErrorMng_EP.SetError(this.Title_LB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Title_LB, resources.GetString("Title_LB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Title_LB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Title_LB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Title_LB, resources.GetString("Title_LB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Title_LB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Title_LB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Title_LB, ((int)(resources.GetObject("Title_LB.IconPadding"))));
			this.Title_LB.ImageKey = string.Empty;
			this.HelpProvider.SetShowHelp(this.Title_LB, ((bool)(resources.GetObject("Title_LB.ShowHelp"))));
			// 
			// Progress_Panel
			// 
			resources.ApplyResources(this.Progress_Panel, "Progress_Panel");
			this.ErrorMng_EP.SetError(this.Progress_Panel, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Progress_Panel, resources.GetString("Progress_Panel.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Progress_Panel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Progress_Panel.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Progress_Panel, resources.GetString("Progress_Panel.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Progress_Panel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Progress_Panel.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Progress_Panel, ((int)(resources.GetObject("Progress_Panel.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Progress_Panel, ((bool)(resources.GetObject("Progress_Panel.ShowHelp"))));
			// 
			// ProgressBK_Panel
			// 
			resources.ApplyResources(this.ProgressBK_Panel, "ProgressBK_Panel");
			this.ErrorMng_EP.SetError(this.ProgressBK_Panel, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.ProgressBK_Panel, resources.GetString("ProgressBK_Panel.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.ProgressBK_Panel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("ProgressBK_Panel.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.ProgressBK_Panel, resources.GetString("ProgressBK_Panel.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.ProgressBK_Panel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("ProgressBK_Panel.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.ProgressBK_Panel, ((int)(resources.GetObject("ProgressBK_Panel.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.ProgressBK_Panel, ((bool)(resources.GetObject("ProgressBK_Panel.ShowHelp"))));
			// 
			// ProgressInfo_PB
			// 
			resources.ApplyResources(this.ProgressInfo_PB, "ProgressInfo_PB");
			this.ErrorMng_EP.SetError(this.ProgressInfo_PB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.ProgressInfo_PB, resources.GetString("ProgressInfo_PB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.ProgressInfo_PB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("ProgressInfo_PB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.ProgressInfo_PB, resources.GetString("ProgressInfo_PB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.ProgressInfo_PB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("ProgressInfo_PB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.ProgressInfo_PB, ((int)(resources.GetObject("ProgressInfo_PB.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.ProgressInfo_PB, ((bool)(resources.GetObject("ProgressInfo_PB.ShowHelp"))));
			// 
			// Progress_PB
			// 
			resources.ApplyResources(this.Progress_PB, "Progress_PB");
			this.ErrorMng_EP.SetError(this.Progress_PB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Progress_PB, resources.GetString("Progress_PB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Progress_PB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Progress_PB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Progress_PB, resources.GetString("Progress_PB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Progress_PB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Progress_PB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Progress_PB, ((int)(resources.GetObject("Progress_PB.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Progress_PB, ((bool)(resources.GetObject("Progress_PB.ShowHelp"))));
			// 
			// numeroClienteLabel
			// 
			resources.ApplyResources(numeroClienteLabel, "numeroClienteLabel");
			this.ErrorMng_EP.SetError(numeroClienteLabel, string.Empty);
			this.HelpProvider.SetHelpKeyword(numeroClienteLabel, resources.GetString("numeroClienteLabel.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(numeroClienteLabel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("numeroClienteLabel.HelpNavigator"))));
			this.HelpProvider.SetHelpString(numeroClienteLabel, resources.GetString("numeroClienteLabel.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(numeroClienteLabel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("numeroClienteLabel.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(numeroClienteLabel, ((int)(resources.GetObject("numeroClienteLabel.IconPadding"))));
			numeroClienteLabel.ImageKey = string.Empty;
			numeroClienteLabel.Name = "numeroClienteLabel";
			this.HelpProvider.SetShowHelp(numeroClienteLabel, ((bool)(resources.GetObject("numeroClienteLabel.ShowHelp"))));
			// 
			// Servidores
			// 
			resources.ApplyResources(Servidores, "Servidores");
			this.ErrorMng_EP.SetError(Servidores, string.Empty);
			this.HelpProvider.SetHelpKeyword(Servidores, resources.GetString("Servidores.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(Servidores, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Servidores.HelpNavigator"))));
			this.HelpProvider.SetHelpString(Servidores, resources.GetString("Servidores.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(Servidores, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Servidores.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(Servidores, ((int)(resources.GetObject("Servidores.IconPadding"))));
			Servidores.ImageKey = string.Empty;
			Servidores.Name = "Servidores";
			this.HelpProvider.SetShowHelp(Servidores, ((bool)(resources.GetObject("Servidores.ShowHelp"))));
			// 
			// label2
			// 
			resources.ApplyResources(label2, "label2");
			this.ErrorMng_EP.SetError(label2, string.Empty);
			this.HelpProvider.SetHelpKeyword(label2, resources.GetString("label2.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(label2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label2.HelpNavigator"))));
			this.HelpProvider.SetHelpString(label2, resources.GetString("label2.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(label2, ((int)(resources.GetObject("label2.IconPadding"))));
			label2.ImageKey = string.Empty;
			label2.Name = "label2";
			this.HelpProvider.SetShowHelp(label2, ((bool)(resources.GetObject("label2.ShowHelp"))));
			// 
			// label1
			// 
			resources.ApplyResources(label1, "label1");
			this.ErrorMng_EP.SetError(label1, string.Empty);
			this.HelpProvider.SetHelpKeyword(label1, resources.GetString("label1.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(label1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label1.HelpNavigator"))));
			this.HelpProvider.SetHelpString(label1, resources.GetString("label1.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(label1, ((int)(resources.GetObject("label1.IconPadding"))));
			label1.ImageKey = string.Empty;
			label1.Name = "label1";
			this.HelpProvider.SetShowHelp(label1, ((bool)(resources.GetObject("label1.ShowHelp"))));
			// 
			// label3
			// 
			resources.ApplyResources(label3, "label3");
			this.ErrorMng_EP.SetError(label3, string.Empty);
			this.HelpProvider.SetHelpKeyword(label3, resources.GetString("label3.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(label3, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label3.HelpNavigator"))));
			this.HelpProvider.SetHelpString(label3, resources.GetString("label3.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(label3, ((int)(resources.GetObject("label3.IconPadding"))));
			label3.ImageKey = string.Empty;
			label3.Name = "label3";
			this.HelpProvider.SetShowHelp(label3, ((bool)(resources.GetObject("label3.ShowHelp"))));
			// 
			// label4
			// 
			resources.ApplyResources(label4, "label4");
			this.ErrorMng_EP.SetError(label4, string.Empty);
			this.HelpProvider.SetHelpKeyword(label4, resources.GetString("label4.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(label4, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label4.HelpNavigator"))));
			this.HelpProvider.SetHelpString(label4, resources.GetString("label4.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(label4, ((int)(resources.GetObject("label4.IconPadding"))));
			label4.ImageKey = string.Empty;
			label4.Name = "label4";
			this.HelpProvider.SetShowHelp(label4, ((bool)(resources.GetObject("label4.ShowHelp"))));
			// 
			// label5
			// 
			resources.ApplyResources(label5, "label5");
			this.ErrorMng_EP.SetError(label5, string.Empty);
			this.HelpProvider.SetHelpKeyword(label5, resources.GetString("label5.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(label5, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label5.HelpNavigator"))));
			this.HelpProvider.SetHelpString(label5, resources.GetString("label5.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(label5, ((int)(resources.GetObject("label5.IconPadding"))));
			label5.ImageKey = string.Empty;
			label5.Name = "label5";
			this.HelpProvider.SetShowHelp(label5, ((bool)(resources.GetObject("label5.ShowHelp"))));
			// 
			// Settings_TP
			// 
			resources.ApplyResources(this.Settings_TP, "Settings_TP");
			this.Settings_TP.Controls.Add(this.General_TP);
			this.Settings_TP.Controls.Add(this.Backups_TP);
			this.Settings_TP.Controls.Add(this.Hosts_TP);
			this.ErrorMng_EP.SetError(this.Settings_TP, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Settings_TP, resources.GetString("Settings_TP.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Settings_TP, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Settings_TP.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Settings_TP, resources.GetString("Settings_TP.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Settings_TP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Settings_TP.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Settings_TP, ((int)(resources.GetObject("Settings_TP.IconPadding"))));
			this.Settings_TP.Name = "Settings_TP";
			this.Settings_TP.SelectedIndex = 0;
			this.HelpProvider.SetShowHelp(this.Settings_TP, ((bool)(resources.GetObject("Settings_TP.ShowHelp"))));
			this.Settings_TP.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			// 
			// General_TP
			// 
			resources.ApplyResources(this.General_TP, "General_TP");
			this.General_TP.Controls.Add(this.Design_GB);
			this.General_TP.Controls.Add(this.Idioma_GB);
			this.ErrorMng_EP.SetError(this.General_TP, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.General_TP, resources.GetString("General_TP.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.General_TP, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("General_TP.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.General_TP, resources.GetString("General_TP.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.General_TP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("General_TP.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.General_TP, ((int)(resources.GetObject("General_TP.IconPadding"))));
			this.General_TP.ImageKey = string.Empty;
			this.General_TP.Name = "General_TP";
			this.HelpProvider.SetShowHelp(this.General_TP, ((bool)(resources.GetObject("General_TP.ShowHelp"))));
			this.General_TP.ToolTipText = string.Empty;
			this.General_TP.UseVisualStyleBackColor = true;
			// 
			// Design_GB
			// 
			resources.ApplyResources(this.Design_GB, "Design_GB");
			this.Design_GB.Controls.Add(this.ShowNullRecords_CkB);
			this.Design_GB.Controls.Add(this.FormatGrids_CkB);
			this.ErrorMng_EP.SetError(this.Design_GB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Design_GB, resources.GetString("Design_GB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Design_GB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Design_GB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Design_GB, resources.GetString("Design_GB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Design_GB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Design_GB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Design_GB, ((int)(resources.GetObject("Design_GB.IconPadding"))));
			this.Design_GB.Name = "Design_GB";
			this.HelpProvider.SetShowHelp(this.Design_GB, ((bool)(resources.GetObject("Design_GB.ShowHelp"))));
			this.Design_GB.TabStop = false;
			// 
			// ShowNullRecords_CkB
			// 
			resources.ApplyResources(this.ShowNullRecords_CkB, "ShowNullRecords_CkB");
			this.ErrorMng_EP.SetError(this.ShowNullRecords_CkB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.ShowNullRecords_CkB, resources.GetString("ShowNullRecords_CkB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.ShowNullRecords_CkB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("ShowNullRecords_CkB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.ShowNullRecords_CkB, resources.GetString("ShowNullRecords_CkB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.ShowNullRecords_CkB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("ShowNullRecords_CkB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.ShowNullRecords_CkB, ((int)(resources.GetObject("ShowNullRecords_CkB.IconPadding"))));
			this.ShowNullRecords_CkB.ImageKey = string.Empty;
			this.ShowNullRecords_CkB.Name = "ShowNullRecords_CkB";
			this.HelpProvider.SetShowHelp(this.ShowNullRecords_CkB, ((bool)(resources.GetObject("ShowNullRecords_CkB.ShowHelp"))));
			this.ShowNullRecords_CkB.UseVisualStyleBackColor = true;
			// 
			// FormatGrids_CkB
			// 
			resources.ApplyResources(this.FormatGrids_CkB, "FormatGrids_CkB");
			this.ErrorMng_EP.SetError(this.FormatGrids_CkB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.FormatGrids_CkB, resources.GetString("FormatGrids_CkB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.FormatGrids_CkB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("FormatGrids_CkB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.FormatGrids_CkB, resources.GetString("FormatGrids_CkB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.FormatGrids_CkB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("FormatGrids_CkB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.FormatGrids_CkB, ((int)(resources.GetObject("FormatGrids_CkB.IconPadding"))));
			this.FormatGrids_CkB.ImageKey = string.Empty;
			this.FormatGrids_CkB.Name = "FormatGrids_CkB";
			this.HelpProvider.SetShowHelp(this.FormatGrids_CkB, ((bool)(resources.GetObject("FormatGrids_CkB.ShowHelp"))));
			this.FormatGrids_CkB.UseVisualStyleBackColor = true;
			// 
			// Idioma_GB
			// 
			resources.ApplyResources(this.Idioma_GB, "Idioma_GB");
			this.Idioma_GB.Controls.Add(this.English_RB);
			this.Idioma_GB.Controls.Add(this.Spanish_RB);
			this.Idioma_GB.Controls.Add(this.Predeterminado_RB);
			this.ErrorMng_EP.SetError(this.Idioma_GB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Idioma_GB, resources.GetString("Idioma_GB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Idioma_GB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Idioma_GB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Idioma_GB, resources.GetString("Idioma_GB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Idioma_GB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Idioma_GB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Idioma_GB, ((int)(resources.GetObject("Idioma_GB.IconPadding"))));
			this.Idioma_GB.Name = "Idioma_GB";
			this.HelpProvider.SetShowHelp(this.Idioma_GB, ((bool)(resources.GetObject("Idioma_GB.ShowHelp"))));
			this.Idioma_GB.TabStop = false;
			this.Idioma_GB.Validating += new System.ComponentModel.CancelEventHandler(this.Idioma_GB_Validating);
			// 
			// English_RB
			// 
			resources.ApplyResources(this.English_RB, "English_RB");
			this.ErrorMng_EP.SetError(this.English_RB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.English_RB, resources.GetString("English_RB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.English_RB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("English_RB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.English_RB, resources.GetString("English_RB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.English_RB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("English_RB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.English_RB, ((int)(resources.GetObject("English_RB.IconPadding"))));
			this.English_RB.ImageKey = string.Empty;
			this.English_RB.Name = "English_RB";
			this.HelpProvider.SetShowHelp(this.English_RB, ((bool)(resources.GetObject("English_RB.ShowHelp"))));
			this.English_RB.TabStop = true;
			this.English_RB.Tag = "en";
			this.English_RB.UseVisualStyleBackColor = true;
			this.English_RB.CheckedChanged += new System.EventHandler(this.English_RB_CheckedChanged);
			// 
			// Spanish_RB
			// 
			resources.ApplyResources(this.Spanish_RB, "Spanish_RB");
			this.ErrorMng_EP.SetError(this.Spanish_RB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Spanish_RB, resources.GetString("Spanish_RB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Spanish_RB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Spanish_RB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Spanish_RB, resources.GetString("Spanish_RB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Spanish_RB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Spanish_RB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Spanish_RB, ((int)(resources.GetObject("Spanish_RB.IconPadding"))));
			this.Spanish_RB.ImageKey = string.Empty;
			this.Spanish_RB.Name = "Spanish_RB";
			this.HelpProvider.SetShowHelp(this.Spanish_RB, ((bool)(resources.GetObject("Spanish_RB.ShowHelp"))));
			this.Spanish_RB.TabStop = true;
			this.Spanish_RB.Tag = "es";
			this.Spanish_RB.UseVisualStyleBackColor = true;
			this.Spanish_RB.CheckedChanged += new System.EventHandler(this.Spanish_RB_CheckedChanged);
			// 
			// Predeterminado_RB
			// 
			resources.ApplyResources(this.Predeterminado_RB, "Predeterminado_RB");
			this.ErrorMng_EP.SetError(this.Predeterminado_RB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Predeterminado_RB, resources.GetString("Predeterminado_RB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Predeterminado_RB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Predeterminado_RB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Predeterminado_RB, resources.GetString("Predeterminado_RB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Predeterminado_RB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Predeterminado_RB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Predeterminado_RB, ((int)(resources.GetObject("Predeterminado_RB.IconPadding"))));
			this.Predeterminado_RB.ImageKey = string.Empty;
			this.Predeterminado_RB.Name = "Predeterminado_RB";
			this.HelpProvider.SetShowHelp(this.Predeterminado_RB, ((bool)(resources.GetObject("Predeterminado_RB.ShowHelp"))));
			this.Predeterminado_RB.TabStop = true;
			this.Predeterminado_RB.Tag = "system";
			this.Predeterminado_RB.UseVisualStyleBackColor = true;
			this.Predeterminado_RB.CheckedChanged += new System.EventHandler(this.Predeterminado_RB_CheckedChanged);
			// 
			// Backups_TP
			// 
			resources.ApplyResources(this.Backups_TP, "Backups_TP");
			this.Backups_TP.Controls.Add(this.Backups_GB);
			this.ErrorMng_EP.SetError(this.Backups_TP, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Backups_TP, resources.GetString("Backups_TP.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Backups_TP, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Backups_TP.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Backups_TP, resources.GetString("Backups_TP.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Backups_TP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Backups_TP.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Backups_TP, ((int)(resources.GetObject("Backups_TP.IconPadding"))));
			this.Backups_TP.ImageKey = string.Empty;
			this.Backups_TP.Name = "Backups_TP";
			this.HelpProvider.SetShowHelp(this.Backups_TP, ((bool)(resources.GetObject("Backups_TP.ShowHelp"))));
			this.Backups_TP.ToolTipText = string.Empty;
			this.Backups_TP.UseVisualStyleBackColor = true;
			// 
			// Backups_GB
			// 
			resources.ApplyResources(this.Backups_GB, "Backups_GB");
			this.Backups_GB.Controls.Add(this.BackupsHour_DTP);
			this.Backups_GB.Controls.Add(this.label23);
			this.Backups_GB.Controls.Add(this.BackupsLastDate_DTP);
			this.Backups_GB.Controls.Add(this.label14);
			this.Backups_GB.Controls.Add(this.label13);
			this.Backups_GB.Controls.Add(this.label12);
			this.Backups_GB.Controls.Add(this.BackupsDays_TB);
			this.ErrorMng_EP.SetError(this.Backups_GB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Backups_GB, resources.GetString("Backups_GB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Backups_GB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Backups_GB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Backups_GB, resources.GetString("Backups_GB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Backups_GB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Backups_GB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Backups_GB, ((int)(resources.GetObject("Backups_GB.IconPadding"))));
			this.Backups_GB.Name = "Backups_GB";
			this.HelpProvider.SetShowHelp(this.Backups_GB, ((bool)(resources.GetObject("Backups_GB.ShowHelp"))));
			this.Backups_GB.TabStop = false;
			// 
			// BackupsHour_DTP
			// 
			resources.ApplyResources(this.BackupsHour_DTP, "BackupsHour_DTP");
			this.ErrorMng_EP.SetError(this.BackupsHour_DTP, string.Empty);
			this.BackupsHour_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.HelpProvider.SetHelpKeyword(this.BackupsHour_DTP, resources.GetString("BackupsHour_DTP.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.BackupsHour_DTP, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("BackupsHour_DTP.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.BackupsHour_DTP, resources.GetString("BackupsHour_DTP.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.BackupsHour_DTP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("BackupsHour_DTP.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.BackupsHour_DTP, ((int)(resources.GetObject("BackupsHour_DTP.IconPadding"))));
			this.BackupsHour_DTP.Name = "BackupsHour_DTP";
			this.HelpProvider.SetShowHelp(this.BackupsHour_DTP, ((bool)(resources.GetObject("BackupsHour_DTP.ShowHelp"))));
			this.BackupsHour_DTP.Tag = "NO FORMAT";
			// 
			// label23
			// 
			resources.ApplyResources(this.label23, "label23");
			this.ErrorMng_EP.SetError(this.label23, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.label23, resources.GetString("label23.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.label23, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label23.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.label23, resources.GetString("label23.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.label23, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label23.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.label23, ((int)(resources.GetObject("label23.IconPadding"))));
			this.label23.ImageKey = string.Empty;
			this.label23.Name = "label23";
			this.HelpProvider.SetShowHelp(this.label23, ((bool)(resources.GetObject("label23.ShowHelp"))));
			// 
			// BackupsLastDate_DTP
			// 
			resources.ApplyResources(this.BackupsLastDate_DTP, "BackupsLastDate_DTP");
			this.ErrorMng_EP.SetError(this.BackupsLastDate_DTP, string.Empty);
			this.BackupsLastDate_DTP.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.HelpProvider.SetHelpKeyword(this.BackupsLastDate_DTP, resources.GetString("BackupsLastDate_DTP.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.BackupsLastDate_DTP, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("BackupsLastDate_DTP.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.BackupsLastDate_DTP, resources.GetString("BackupsLastDate_DTP.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.BackupsLastDate_DTP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("BackupsLastDate_DTP.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.BackupsLastDate_DTP, ((int)(resources.GetObject("BackupsLastDate_DTP.IconPadding"))));
			this.BackupsLastDate_DTP.Name = "BackupsLastDate_DTP";
			this.HelpProvider.SetShowHelp(this.BackupsLastDate_DTP, ((bool)(resources.GetObject("BackupsLastDate_DTP.ShowHelp"))));
			this.BackupsLastDate_DTP.Tag = "NO FORMAT";
			// 
			// label14
			// 
			resources.ApplyResources(this.label14, "label14");
			this.ErrorMng_EP.SetError(this.label14, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.label14, resources.GetString("label14.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.label14, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label14.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.label14, resources.GetString("label14.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.label14, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label14.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.label14, ((int)(resources.GetObject("label14.IconPadding"))));
			this.label14.ImageKey = string.Empty;
			this.label14.Name = "label14";
			this.HelpProvider.SetShowHelp(this.label14, ((bool)(resources.GetObject("label14.ShowHelp"))));
			// 
			// label13
			// 
			resources.ApplyResources(this.label13, "label13");
			this.ErrorMng_EP.SetError(this.label13, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.label13, resources.GetString("label13.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.label13, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label13.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.label13, resources.GetString("label13.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.label13, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label13.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.label13, ((int)(resources.GetObject("label13.IconPadding"))));
			this.label13.ImageKey = string.Empty;
			this.label13.Name = "label13";
			this.HelpProvider.SetShowHelp(this.label13, ((bool)(resources.GetObject("label13.ShowHelp"))));
			// 
			// label12
			// 
			resources.ApplyResources(this.label12, "label12");
			this.ErrorMng_EP.SetError(this.label12, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.label12, resources.GetString("label12.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.label12, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label12.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.label12, resources.GetString("label12.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.label12, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label12.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.label12, ((int)(resources.GetObject("label12.IconPadding"))));
			this.label12.ImageKey = string.Empty;
			this.label12.Name = "label12";
			this.HelpProvider.SetShowHelp(this.label12, ((bool)(resources.GetObject("label12.ShowHelp"))));
			// 
			// BackupsDays_TB
			// 
			resources.ApplyResources(this.BackupsDays_TB, "BackupsDays_TB");
			this.ErrorMng_EP.SetError(this.BackupsDays_TB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.BackupsDays_TB, resources.GetString("BackupsDays_TB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.BackupsDays_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("BackupsDays_TB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.BackupsDays_TB, resources.GetString("BackupsDays_TB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.BackupsDays_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("BackupsDays_TB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.BackupsDays_TB, ((int)(resources.GetObject("BackupsDays_TB.IconPadding"))));
			this.BackupsDays_TB.Name = "BackupsDays_TB";
			this.HelpProvider.SetShowHelp(this.BackupsDays_TB, ((bool)(resources.GetObject("BackupsDays_TB.ShowHelp"))));
			// 
			// Hosts_TP
			// 
			resources.ApplyResources(this.Hosts_TP, "Hosts_TP");
			this.Hosts_TP.Controls.Add(this.SMTP_GB);
			this.Hosts_TP.Controls.Add(this.Folders_GB);
			this.Hosts_TP.Controls.Add(this.DBHosts_GB);
			this.Hosts_TP.Controls.Add(this.FilesHost_GB);
			this.ErrorMng_EP.SetError(this.Hosts_TP, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Hosts_TP, resources.GetString("Hosts_TP.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Hosts_TP, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Hosts_TP.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Hosts_TP, resources.GetString("Hosts_TP.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Hosts_TP, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Hosts_TP.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Hosts_TP, ((int)(resources.GetObject("Hosts_TP.IconPadding"))));
			this.Hosts_TP.ImageKey = string.Empty;
			this.Hosts_TP.Name = "Hosts_TP";
			this.HelpProvider.SetShowHelp(this.Hosts_TP, ((bool)(resources.GetObject("Hosts_TP.ShowHelp"))));
			this.Hosts_TP.ToolTipText = string.Empty;
			this.Hosts_TP.UseVisualStyleBackColor = true;
			// 
			// SMTP_GB
			// 
			resources.ApplyResources(this.SMTP_GB, "SMTP_GB");
			this.SMTP_GB.Controls.Add(this.SMTPEnableSSL_CkB);
			this.SMTP_GB.Controls.Add(this.SMTPMail_TB);
			this.SMTP_GB.Controls.Add(this.label7);
			this.SMTP_GB.Controls.Add(this.MostrarPassword_CkB);
			this.SMTP_GB.Controls.Add(this.SMTPPwd_TB);
			this.SMTP_GB.Controls.Add(this.label6);
			this.SMTP_GB.Controls.Add(label5);
			this.SMTP_GB.Controls.Add(this.SMTPUser_TB);
			this.SMTP_GB.Controls.Add(label3);
			this.SMTP_GB.Controls.Add(this.SMTPPort_TB);
			this.SMTP_GB.Controls.Add(label4);
			this.SMTP_GB.Controls.Add(this.SMTPHost_TB);
			this.ErrorMng_EP.SetError(this.SMTP_GB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.SMTP_GB, resources.GetString("SMTP_GB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.SMTP_GB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("SMTP_GB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.SMTP_GB, resources.GetString("SMTP_GB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.SMTP_GB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("SMTP_GB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.SMTP_GB, ((int)(resources.GetObject("SMTP_GB.IconPadding"))));
			this.SMTP_GB.Name = "SMTP_GB";
			this.HelpProvider.SetShowHelp(this.SMTP_GB, ((bool)(resources.GetObject("SMTP_GB.ShowHelp"))));
			this.SMTP_GB.TabStop = false;
			// 
			// SMTPEnableSSL_CkB
			// 
			resources.ApplyResources(this.SMTPEnableSSL_CkB, "SMTPEnableSSL_CkB");
			this.ErrorMng_EP.SetError(this.SMTPEnableSSL_CkB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.SMTPEnableSSL_CkB, resources.GetString("SMTPEnableSSL_CkB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.SMTPEnableSSL_CkB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("SMTPEnableSSL_CkB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.SMTPEnableSSL_CkB, resources.GetString("SMTPEnableSSL_CkB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.SMTPEnableSSL_CkB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("SMTPEnableSSL_CkB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.SMTPEnableSSL_CkB, ((int)(resources.GetObject("SMTPEnableSSL_CkB.IconPadding"))));
			this.SMTPEnableSSL_CkB.ImageKey = string.Empty;
			this.SMTPEnableSSL_CkB.Name = "SMTPEnableSSL_CkB";
			this.HelpProvider.SetShowHelp(this.SMTPEnableSSL_CkB, ((bool)(resources.GetObject("SMTPEnableSSL_CkB.ShowHelp"))));
			this.SMTPEnableSSL_CkB.UseVisualStyleBackColor = true;
			// 
			// SMTPMail_TB
			// 
			resources.ApplyResources(this.SMTPMail_TB, "SMTPMail_TB");
			this.ErrorMng_EP.SetError(this.SMTPMail_TB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.SMTPMail_TB, resources.GetString("SMTPMail_TB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.SMTPMail_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("SMTPMail_TB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.SMTPMail_TB, resources.GetString("SMTPMail_TB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.SMTPMail_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("SMTPMail_TB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.SMTPMail_TB, ((int)(resources.GetObject("SMTPMail_TB.IconPadding"))));
			this.SMTPMail_TB.Name = "SMTPMail_TB";
			this.HelpProvider.SetShowHelp(this.SMTPMail_TB, ((bool)(resources.GetObject("SMTPMail_TB.ShowHelp"))));
			this.SMTPMail_TB.TabStop = false;
			// 
			// label7
			// 
			resources.ApplyResources(this.label7, "label7");
			this.ErrorMng_EP.SetError(this.label7, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.label7, resources.GetString("label7.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.label7, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label7.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.label7, resources.GetString("label7.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.label7, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label7.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.label7, ((int)(resources.GetObject("label7.IconPadding"))));
			this.label7.ImageKey = string.Empty;
			this.label7.Name = "label7";
			this.HelpProvider.SetShowHelp(this.label7, ((bool)(resources.GetObject("label7.ShowHelp"))));
			// 
			// MostrarPassword_CkB
			// 
			resources.ApplyResources(this.MostrarPassword_CkB, "MostrarPassword_CkB");
			this.ErrorMng_EP.SetError(this.MostrarPassword_CkB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.MostrarPassword_CkB, resources.GetString("MostrarPassword_CkB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.MostrarPassword_CkB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("MostrarPassword_CkB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.MostrarPassword_CkB, resources.GetString("MostrarPassword_CkB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.MostrarPassword_CkB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("MostrarPassword_CkB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.MostrarPassword_CkB, ((int)(resources.GetObject("MostrarPassword_CkB.IconPadding"))));
			this.MostrarPassword_CkB.ImageKey = string.Empty;
			this.MostrarPassword_CkB.Name = "MostrarPassword_CkB";
			this.HelpProvider.SetShowHelp(this.MostrarPassword_CkB, ((bool)(resources.GetObject("MostrarPassword_CkB.ShowHelp"))));
			this.MostrarPassword_CkB.UseVisualStyleBackColor = true;
			this.MostrarPassword_CkB.CheckedChanged += new System.EventHandler(this.MostrarPassword_CkB_CheckedChanged);
			// 
			// SMTPPwd_TB
			// 
			resources.ApplyResources(this.SMTPPwd_TB, "SMTPPwd_TB");
			this.ErrorMng_EP.SetError(this.SMTPPwd_TB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.SMTPPwd_TB, resources.GetString("SMTPPwd_TB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.SMTPPwd_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("SMTPPwd_TB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.SMTPPwd_TB, resources.GetString("SMTPPwd_TB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.SMTPPwd_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("SMTPPwd_TB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.SMTPPwd_TB, ((int)(resources.GetObject("SMTPPwd_TB.IconPadding"))));
			this.SMTPPwd_TB.Name = "SMTPPwd_TB";
			this.HelpProvider.SetShowHelp(this.SMTPPwd_TB, ((bool)(resources.GetObject("SMTPPwd_TB.ShowHelp"))));
			this.SMTPPwd_TB.UseSystemPasswordChar = true;
			// 
			// label6
			// 
			resources.ApplyResources(this.label6, "label6");
			this.ErrorMng_EP.SetError(this.label6, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.label6, resources.GetString("label6.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.label6, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label6.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.label6, resources.GetString("label6.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.label6, ((int)(resources.GetObject("label6.IconPadding"))));
			this.label6.ImageKey = string.Empty;
			this.label6.Name = "label6";
			this.HelpProvider.SetShowHelp(this.label6, ((bool)(resources.GetObject("label6.ShowHelp"))));
			// 
			// SMTPUser_TB
			// 
			resources.ApplyResources(this.SMTPUser_TB, "SMTPUser_TB");
			this.ErrorMng_EP.SetError(this.SMTPUser_TB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.SMTPUser_TB, resources.GetString("SMTPUser_TB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.SMTPUser_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("SMTPUser_TB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.SMTPUser_TB, resources.GetString("SMTPUser_TB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.SMTPUser_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("SMTPUser_TB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.SMTPUser_TB, ((int)(resources.GetObject("SMTPUser_TB.IconPadding"))));
			this.SMTPUser_TB.Name = "SMTPUser_TB";
			this.HelpProvider.SetShowHelp(this.SMTPUser_TB, ((bool)(resources.GetObject("SMTPUser_TB.ShowHelp"))));
			this.SMTPUser_TB.TabStop = false;
			// 
			// SMTPPort_TB
			// 
			resources.ApplyResources(this.SMTPPort_TB, "SMTPPort_TB");
			this.ErrorMng_EP.SetError(this.SMTPPort_TB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.SMTPPort_TB, resources.GetString("SMTPPort_TB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.SMTPPort_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("SMTPPort_TB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.SMTPPort_TB, resources.GetString("SMTPPort_TB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.SMTPPort_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("SMTPPort_TB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.SMTPPort_TB, ((int)(resources.GetObject("SMTPPort_TB.IconPadding"))));
			this.SMTPPort_TB.Name = "SMTPPort_TB";
			this.HelpProvider.SetShowHelp(this.SMTPPort_TB, ((bool)(resources.GetObject("SMTPPort_TB.ShowHelp"))));
			this.SMTPPort_TB.TabStop = false;
			// 
			// SMTPHost_TB
			// 
			resources.ApplyResources(this.SMTPHost_TB, "SMTPHost_TB");
			this.ErrorMng_EP.SetError(this.SMTPHost_TB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.SMTPHost_TB, resources.GetString("SMTPHost_TB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.SMTPHost_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("SMTPHost_TB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.SMTPHost_TB, resources.GetString("SMTPHost_TB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.SMTPHost_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("SMTPHost_TB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.SMTPHost_TB, ((int)(resources.GetObject("SMTPHost_TB.IconPadding"))));
			this.SMTPHost_TB.Name = "SMTPHost_TB";
			this.HelpProvider.SetShowHelp(this.SMTPHost_TB, ((bool)(resources.GetObject("SMTPHost_TB.ShowHelp"))));
			this.SMTPHost_TB.TabStop = false;
			// 
			// Folders_GB
			// 
			resources.ApplyResources(this.Folders_GB, "Folders_GB");
			this.Folders_GB.Controls.Add(this.PDFPrintsFolder_TB);
			this.Folders_GB.Controls.Add(this.PDFPrinstFolder_BT);
			this.Folders_GB.Controls.Add(label1);
			this.ErrorMng_EP.SetError(this.Folders_GB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.Folders_GB, resources.GetString("Folders_GB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Folders_GB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Folders_GB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Folders_GB, resources.GetString("Folders_GB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Folders_GB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Folders_GB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Folders_GB, ((int)(resources.GetObject("Folders_GB.IconPadding"))));
			this.Folders_GB.Name = "Folders_GB";
			this.HelpProvider.SetShowHelp(this.Folders_GB, ((bool)(resources.GetObject("Folders_GB.ShowHelp"))));
			this.Folders_GB.TabStop = false;
			// 
			// PDFPrintsFolder_TB
			// 
			resources.ApplyResources(this.PDFPrintsFolder_TB, "PDFPrintsFolder_TB");
			this.ErrorMng_EP.SetError(this.PDFPrintsFolder_TB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.PDFPrintsFolder_TB, resources.GetString("PDFPrintsFolder_TB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.PDFPrintsFolder_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("PDFPrintsFolder_TB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.PDFPrintsFolder_TB, resources.GetString("PDFPrintsFolder_TB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.PDFPrintsFolder_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("PDFPrintsFolder_TB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.PDFPrintsFolder_TB, ((int)(resources.GetObject("PDFPrintsFolder_TB.IconPadding"))));
			this.PDFPrintsFolder_TB.Name = "PDFPrintsFolder_TB";
			this.PDFPrintsFolder_TB.ReadOnly = true;
			this.HelpProvider.SetShowHelp(this.PDFPrintsFolder_TB, ((bool)(resources.GetObject("PDFPrintsFolder_TB.ShowHelp"))));
			// 
			// PDFPrinstFolder_BT
			// 
			resources.ApplyResources(this.PDFPrinstFolder_BT, "PDFPrinstFolder_BT");
			this.ErrorMng_EP.SetError(this.PDFPrinstFolder_BT, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.PDFPrinstFolder_BT, resources.GetString("PDFPrinstFolder_BT.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.PDFPrinstFolder_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("PDFPrinstFolder_BT.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.PDFPrinstFolder_BT, resources.GetString("PDFPrinstFolder_BT.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.PDFPrinstFolder_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("PDFPrinstFolder_BT.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.PDFPrinstFolder_BT, ((int)(resources.GetObject("PDFPrinstFolder_BT.IconPadding"))));
			this.PDFPrinstFolder_BT.Image = global::moleQule.Face.Properties.Resources.browse_16;
			this.PDFPrinstFolder_BT.ImageKey = string.Empty;
			this.PDFPrinstFolder_BT.Name = "PDFPrinstFolder_BT";
			this.HelpProvider.SetShowHelp(this.PDFPrinstFolder_BT, ((bool)(resources.GetObject("PDFPrinstFolder_BT.ShowHelp"))));
			this.PDFPrinstFolder_BT.UseVisualStyleBackColor = true;
			this.PDFPrinstFolder_BT.Click += new System.EventHandler(this.PDFPrinstFolder_BT_Click);
			// 
			// DBHosts_GB
			// 
			resources.ApplyResources(this.DBHosts_GB, "DBHosts_GB");
			this.DBHosts_GB.Controls.Add(Servidores);
			this.DBHosts_GB.Controls.Add(this.WANHost_TB);
			this.DBHosts_GB.Controls.Add(label2);
			this.DBHosts_GB.Controls.Add(this.LANHost_TB);
			this.ErrorMng_EP.SetError(this.DBHosts_GB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.DBHosts_GB, resources.GetString("DBHosts_GB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.DBHosts_GB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("DBHosts_GB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.DBHosts_GB, resources.GetString("DBHosts_GB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.DBHosts_GB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("DBHosts_GB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.DBHosts_GB, ((int)(resources.GetObject("DBHosts_GB.IconPadding"))));
			this.DBHosts_GB.Name = "DBHosts_GB";
			this.HelpProvider.SetShowHelp(this.DBHosts_GB, ((bool)(resources.GetObject("DBHosts_GB.ShowHelp"))));
			this.DBHosts_GB.TabStop = false;
			// 
			// WANHost_TB
			// 
			resources.ApplyResources(this.WANHost_TB, "WANHost_TB");
			this.ErrorMng_EP.SetError(this.WANHost_TB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.WANHost_TB, resources.GetString("WANHost_TB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.WANHost_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("WANHost_TB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.WANHost_TB, resources.GetString("WANHost_TB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.WANHost_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("WANHost_TB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.WANHost_TB, ((int)(resources.GetObject("WANHost_TB.IconPadding"))));
			this.WANHost_TB.Name = "WANHost_TB";
			this.HelpProvider.SetShowHelp(this.WANHost_TB, ((bool)(resources.GetObject("WANHost_TB.ShowHelp"))));
			this.WANHost_TB.TabStop = false;
			// 
			// LANHost_TB
			// 
			resources.ApplyResources(this.LANHost_TB, "LANHost_TB");
			this.ErrorMng_EP.SetError(this.LANHost_TB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.LANHost_TB, resources.GetString("LANHost_TB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.LANHost_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("LANHost_TB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.LANHost_TB, resources.GetString("LANHost_TB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.LANHost_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("LANHost_TB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.LANHost_TB, ((int)(resources.GetObject("LANHost_TB.IconPadding"))));
			this.LANHost_TB.Name = "LANHost_TB";
			this.HelpProvider.SetShowHelp(this.LANHost_TB, ((bool)(resources.GetObject("LANHost_TB.ShowHelp"))));
			this.LANHost_TB.TabStop = false;
			// 
			// FilesHost_GB
			// 
			resources.ApplyResources(this.FilesHost_GB, "FilesHost_GB");
			this.FilesHost_GB.Controls.Add(numeroClienteLabel);
			this.FilesHost_GB.Controls.Add(this.FilesHost_TB);
			this.ErrorMng_EP.SetError(this.FilesHost_GB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.FilesHost_GB, resources.GetString("FilesHost_GB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.FilesHost_GB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("FilesHost_GB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.FilesHost_GB, resources.GetString("FilesHost_GB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.FilesHost_GB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("FilesHost_GB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.FilesHost_GB, ((int)(resources.GetObject("FilesHost_GB.IconPadding"))));
			this.FilesHost_GB.Name = "FilesHost_GB";
			this.HelpProvider.SetShowHelp(this.FilesHost_GB, ((bool)(resources.GetObject("FilesHost_GB.ShowHelp"))));
			this.FilesHost_GB.TabStop = false;
			// 
			// FilesHost_TB
			// 
			resources.ApplyResources(this.FilesHost_TB, "FilesHost_TB");
			this.ErrorMng_EP.SetError(this.FilesHost_TB, string.Empty);
			this.HelpProvider.SetHelpKeyword(this.FilesHost_TB, resources.GetString("FilesHost_TB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.FilesHost_TB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("FilesHost_TB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.FilesHost_TB, resources.GetString("FilesHost_TB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.FilesHost_TB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("FilesHost_TB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.FilesHost_TB, ((int)(resources.GetObject("FilesHost_TB.IconPadding"))));
			this.FilesHost_TB.Name = "FilesHost_TB";
			this.HelpProvider.SetShowHelp(this.FilesHost_TB, ((bool)(resources.GetObject("FilesHost_TB.ShowHelp"))));
			this.FilesHost_TB.TabStop = false;
			// 
			// Browser
			// 
			this.Browser.Description = string.Empty;
			this.Browser.SelectedPath = string.Empty;
			// 
			// SettingsBaseForm
			// 
			resources.ApplyResources(this, "$this");
			this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this, resources.GetString("$this.HelpString"));
			this.Name = "SettingsBaseForm";
			this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.Controls.SetChildIndex(this.ProgressBK_Panel, 0);
			this.Controls.SetChildIndex(this.PanelesV, 0);
			this.Controls.SetChildIndex(this.ProgressInfo_PB, 0);
			this.Controls.SetChildIndex(this.Progress_PB, 0);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
			this.PanelesV.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.Settings_TP.ResumeLayout(false);
			this.General_TP.ResumeLayout(false);
			this.Design_GB.ResumeLayout(false);
			this.Idioma_GB.ResumeLayout(false);
			this.Idioma_GB.PerformLayout();
			this.Backups_TP.ResumeLayout(false);
			this.Backups_GB.ResumeLayout(false);
			this.Backups_GB.PerformLayout();
			this.Hosts_TP.ResumeLayout(false);
			this.SMTP_GB.ResumeLayout(false);
			this.SMTP_GB.PerformLayout();
			this.Folders_GB.ResumeLayout(false);
			this.Folders_GB.PerformLayout();
			this.DBHosts_GB.ResumeLayout(false);
			this.DBHosts_GB.PerformLayout();
			this.FilesHost_GB.ResumeLayout(false);
			this.FilesHost_GB.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.TabControl Settings_TP;
		protected System.Windows.Forms.GroupBox Idioma_GB;
		protected System.Windows.Forms.TabPage General_TP;
		protected System.Windows.Forms.RadioButton English_RB;
		protected System.Windows.Forms.RadioButton Spanish_RB;
		protected System.Windows.Forms.RadioButton Predeterminado_RB;
		protected System.Windows.Forms.GroupBox DBHosts_GB;
		protected System.Windows.Forms.TextBox WANHost_TB;
		protected System.Windows.Forms.TextBox LANHost_TB;
		protected System.Windows.Forms.GroupBox FilesHost_GB;
		protected System.Windows.Forms.TextBox FilesHost_TB;
		protected System.Windows.Forms.TabPage Hosts_TP;
		protected System.Windows.Forms.TabPage Backups_TP;
		protected System.Windows.Forms.CheckBox FormatGrids_CkB;
		protected System.Windows.Forms.GroupBox Design_GB;
		protected System.Windows.Forms.GroupBox Folders_GB;
		protected System.Windows.Forms.Button PDFPrinstFolder_BT;
		protected System.Windows.Forms.FolderBrowserDialog Browser;
		protected System.Windows.Forms.TextBox PDFPrintsFolder_TB;
		protected System.Windows.Forms.GroupBox SMTP_GB;
		protected System.Windows.Forms.TextBox SMTPUser_TB;
		protected System.Windows.Forms.TextBox SMTPPort_TB;
		protected System.Windows.Forms.TextBox SMTPHost_TB;
		protected System.Windows.Forms.TextBox SMTPMail_TB;
		protected System.Windows.Forms.CheckBox ShowNullRecords_CkB;
		protected System.Windows.Forms.DateTimePicker BackupsHour_DTP;
		protected System.Windows.Forms.Label label23;
		protected System.Windows.Forms.DateTimePicker BackupsLastDate_DTP;
		protected System.Windows.Forms.Label label14;
		protected System.Windows.Forms.Label label13;
		protected System.Windows.Forms.Label label12;
		protected System.Windows.Forms.TextBox BackupsDays_TB;
		protected System.Windows.Forms.Label label7;
		protected System.Windows.Forms.CheckBox MostrarPassword_CkB;
		protected System.Windows.Forms.TextBox SMTPPwd_TB;
		protected System.Windows.Forms.Label label6;
		protected System.Windows.Forms.CheckBox SMTPEnableSSL_CkB;
		protected System.Windows.Forms.GroupBox Backups_GB;
    }
}
