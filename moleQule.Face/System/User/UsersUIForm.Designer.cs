using moleQule.Library;

namespace moleQule.Face
{
    partial class UsersUIForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersUIForm));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.Users_Panel = new System.Windows.Forms.SplitContainer();
			this.label1 = new System.Windows.Forms.Label();
			this.Users_TS = new System.Windows.Forms.ToolStrip();
			this.NewUser_TI = new System.Windows.Forms.ToolStripButton();
			this.EditProducto_TI = new System.Windows.Forms.ToolStripButton();
			this.DeleteProducto_TI = new System.Windows.Forms.ToolStripButton();
			this.AttachUser_TI = new System.Windows.Forms.ToolStripButton();
			this.DetachUser_TI = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.AdminUser_TI = new System.Windows.Forms.ToolStripButton();
			this.Users_DGW = new System.Windows.Forms.DataGridView();
			this.Datos_Usuarios = new System.Windows.Forms.BindingSource(this.components);
			this.Privileges_Panel = new System.Windows.Forms.SplitContainer();
			this.label2 = new System.Windows.Forms.Label();
			this.Privileges_TS = new System.Windows.Forms.ToolStrip();
			this.SetAll_TI = new System.Windows.Forms.ToolStripButton();
			this.UnsetAll_TI = new System.Windows.Forms.ToolStripButton();
			this.Privileges_DGW = new System.Windows.Forms.DataGridView();
			this.Item = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Read = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Create = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Modify = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Remove = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Datos_Permisos = new System.Windows.Forms.BindingSource(this.components);
			this.Main_Panel = new System.Windows.Forms.SplitContainer();
			this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IsSuperUser = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.IsAdmin = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.IsPartner = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.EstadoLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Empresa = new System.Windows.Forms.DataGridViewButtonColumn();
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).BeginInit();
			this.PanelesV.Panel1.SuspendLayout();
			this.PanelesV.Panel2.SuspendLayout();
			this.PanelesV.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).BeginInit();
			this.Paneles2.Panel1.SuspendLayout();
			this.Paneles2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).BeginInit();
			this.Progress_Panel.SuspendLayout();
			this.ProgressBK_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Users_Panel)).BeginInit();
			this.Users_Panel.Panel1.SuspendLayout();
			this.Users_Panel.Panel2.SuspendLayout();
			this.Users_Panel.SuspendLayout();
			this.Users_TS.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Users_DGW)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Usuarios)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Privileges_Panel)).BeginInit();
			this.Privileges_Panel.Panel1.SuspendLayout();
			this.Privileges_Panel.Panel2.SuspendLayout();
			this.Privileges_Panel.SuspendLayout();
			this.Privileges_TS.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Privileges_DGW)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Permisos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Main_Panel)).BeginInit();
			this.Main_Panel.Panel1.SuspendLayout();
			this.Main_Panel.Panel2.SuspendLayout();
			this.Main_Panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// PanelesV
			// 
			resources.ApplyResources(this.PanelesV, "PanelesV");
			this.ErrorMng_EP.SetError(this.PanelesV, resources.GetString("PanelesV.Error"));
			this.HelpProvider.SetHelpKeyword(this.PanelesV, resources.GetString("PanelesV.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.PanelesV, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("PanelesV.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.PanelesV, resources.GetString("PanelesV.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.PanelesV, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("PanelesV.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.PanelesV, ((int)(resources.GetObject("PanelesV.IconPadding"))));
			// 
			// PanelesV.Panel1
			// 
			resources.ApplyResources(this.PanelesV.Panel1, "PanelesV.Panel1");
			this.PanelesV.Panel1.Controls.Add(this.Main_Panel);
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
			// Submit_BT
			// 
			resources.ApplyResources(this.Submit_BT, "Submit_BT");
			this.ErrorMng_EP.SetError(this.Submit_BT, resources.GetString("Submit_BT.Error"));
			this.HelpProvider.SetHelpKeyword(this.Submit_BT, resources.GetString("Submit_BT.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Submit_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Submit_BT.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Submit_BT, resources.GetString("Submit_BT.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Submit_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Submit_BT.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Submit_BT, ((int)(resources.GetObject("Submit_BT.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Submit_BT, ((bool)(resources.GetObject("Submit_BT.ShowHelp"))));
			// 
			// Cancel_BT
			// 
			resources.ApplyResources(this.Cancel_BT, "Cancel_BT");
			this.ErrorMng_EP.SetError(this.Cancel_BT, resources.GetString("Cancel_BT.Error"));
			this.HelpProvider.SetHelpKeyword(this.Cancel_BT, resources.GetString("Cancel_BT.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Cancel_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Cancel_BT.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Cancel_BT, resources.GetString("Cancel_BT.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Cancel_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Cancel_BT.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Cancel_BT, ((int)(resources.GetObject("Cancel_BT.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Cancel_BT, ((bool)(resources.GetObject("Cancel_BT.ShowHelp"))));
			// 
			// Paneles2
			// 
			resources.ApplyResources(this.Paneles2, "Paneles2");
			this.ErrorMng_EP.SetError(this.Paneles2, resources.GetString("Paneles2.Error"));
			this.HelpProvider.SetHelpKeyword(this.Paneles2, resources.GetString("Paneles2.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Paneles2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Paneles2.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Paneles2, resources.GetString("Paneles2.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Paneles2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Paneles2.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Paneles2, ((int)(resources.GetObject("Paneles2.IconPadding"))));
			// 
			// Paneles2.Panel1
			// 
			resources.ApplyResources(this.Paneles2.Panel1, "Paneles2.Panel1");
			this.ErrorMng_EP.SetError(this.Paneles2.Panel1, resources.GetString("Paneles2.Panel1.Error"));
			this.HelpProvider.SetHelpKeyword(this.Paneles2.Panel1, resources.GetString("Paneles2.Panel1.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Paneles2.Panel1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Paneles2.Panel1.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Paneles2.Panel1, resources.GetString("Paneles2.Panel1.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Paneles2.Panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Paneles2.Panel1.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Paneles2.Panel1, ((int)(resources.GetObject("Paneles2.Panel1.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, ((bool)(resources.GetObject("Paneles2.Panel1.ShowHelp"))));
			// 
			// Paneles2.Panel2
			// 
			resources.ApplyResources(this.Paneles2.Panel2, "Paneles2.Panel2");
			this.ErrorMng_EP.SetError(this.Paneles2.Panel2, resources.GetString("Paneles2.Panel2.Error"));
			this.HelpProvider.SetHelpKeyword(this.Paneles2.Panel2, resources.GetString("Paneles2.Panel2.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Paneles2.Panel2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Paneles2.Panel2.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Paneles2.Panel2, resources.GetString("Paneles2.Panel2.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Paneles2.Panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Paneles2.Panel2.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Paneles2.Panel2, ((int)(resources.GetObject("Paneles2.Panel2.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, ((bool)(resources.GetObject("Paneles2.Panel2.ShowHelp"))));
			this.HelpProvider.SetShowHelp(this.Paneles2, ((bool)(resources.GetObject("Paneles2.ShowHelp"))));
			// 
			// Imprimir_Button
			// 
			resources.ApplyResources(this.Imprimir_Button, "Imprimir_Button");
			this.ErrorMng_EP.SetError(this.Imprimir_Button, resources.GetString("Imprimir_Button.Error"));
			this.HelpProvider.SetHelpKeyword(this.Imprimir_Button, resources.GetString("Imprimir_Button.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Imprimir_Button, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Imprimir_Button.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Imprimir_Button, resources.GetString("Imprimir_Button.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Imprimir_Button, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Imprimir_Button.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Imprimir_Button, ((int)(resources.GetObject("Imprimir_Button.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, ((bool)(resources.GetObject("Imprimir_Button.ShowHelp"))));
			// 
			// Docs_BT
			// 
			resources.ApplyResources(this.Docs_BT, "Docs_BT");
			this.ErrorMng_EP.SetError(this.Docs_BT, resources.GetString("Docs_BT.Error"));
			this.HelpProvider.SetHelpKeyword(this.Docs_BT, resources.GetString("Docs_BT.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Docs_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Docs_BT.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Docs_BT, resources.GetString("Docs_BT.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Docs_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Docs_BT.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Docs_BT, ((int)(resources.GetObject("Docs_BT.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Docs_BT, ((bool)(resources.GetObject("Docs_BT.ShowHelp"))));
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Library.Users);
			// 
			// SaveFile_SFD
			// 
			resources.ApplyResources(this.SaveFile_SFD, "SaveFile_SFD");
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
			this.ErrorMng_EP.SetError(this.Animation, resources.GetString("Animation.Error"));
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
			this.ErrorMng_EP.SetError(this.CancelBkJob_BT, resources.GetString("CancelBkJob_BT.Error"));
			this.HelpProvider.SetHelpKeyword(this.CancelBkJob_BT, resources.GetString("CancelBkJob_BT.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.CancelBkJob_BT, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("CancelBkJob_BT.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.CancelBkJob_BT, resources.GetString("CancelBkJob_BT.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.CancelBkJob_BT, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("CancelBkJob_BT.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.CancelBkJob_BT, ((int)(resources.GetObject("CancelBkJob_BT.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.CancelBkJob_BT, ((bool)(resources.GetObject("CancelBkJob_BT.ShowHelp"))));
			// 
			// ProgressMsg_LB
			// 
			resources.ApplyResources(this.ProgressMsg_LB, "ProgressMsg_LB");
			this.ErrorMng_EP.SetError(this.ProgressMsg_LB, resources.GetString("ProgressMsg_LB.Error"));
			this.HelpProvider.SetHelpKeyword(this.ProgressMsg_LB, resources.GetString("ProgressMsg_LB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.ProgressMsg_LB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("ProgressMsg_LB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.ProgressMsg_LB, resources.GetString("ProgressMsg_LB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.ProgressMsg_LB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("ProgressMsg_LB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.ProgressMsg_LB, ((int)(resources.GetObject("ProgressMsg_LB.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.ProgressMsg_LB, ((bool)(resources.GetObject("ProgressMsg_LB.ShowHelp"))));
			// 
			// Title_LB
			// 
			resources.ApplyResources(this.Title_LB, "Title_LB");
			this.ErrorMng_EP.SetError(this.Title_LB, resources.GetString("Title_LB.Error"));
			this.HelpProvider.SetHelpKeyword(this.Title_LB, resources.GetString("Title_LB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Title_LB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Title_LB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Title_LB, resources.GetString("Title_LB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Title_LB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Title_LB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Title_LB, ((int)(resources.GetObject("Title_LB.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Title_LB, ((bool)(resources.GetObject("Title_LB.ShowHelp"))));
			// 
			// Progress_Panel
			// 
			resources.ApplyResources(this.Progress_Panel, "Progress_Panel");
			this.ErrorMng_EP.SetError(this.Progress_Panel, resources.GetString("Progress_Panel.Error"));
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
			this.ErrorMng_EP.SetError(this.ProgressBK_Panel, resources.GetString("ProgressBK_Panel.Error"));
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
			this.ErrorMng_EP.SetError(this.ProgressInfo_PB, resources.GetString("ProgressInfo_PB.Error"));
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
			this.ErrorMng_EP.SetError(this.Progress_PB, resources.GetString("Progress_PB.Error"));
			this.HelpProvider.SetHelpKeyword(this.Progress_PB, resources.GetString("Progress_PB.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Progress_PB, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Progress_PB.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Progress_PB, resources.GetString("Progress_PB.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Progress_PB, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Progress_PB.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Progress_PB, ((int)(resources.GetObject("Progress_PB.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Progress_PB, ((bool)(resources.GetObject("Progress_PB.ShowHelp"))));
			// 
			// Users_Panel
			// 
			resources.ApplyResources(this.Users_Panel, "Users_Panel");
			this.ErrorMng_EP.SetError(this.Users_Panel, resources.GetString("Users_Panel.Error"));
			this.Users_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.HelpProvider.SetHelpKeyword(this.Users_Panel, resources.GetString("Users_Panel.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Users_Panel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Users_Panel.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Users_Panel, resources.GetString("Users_Panel.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Users_Panel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Users_Panel.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Users_Panel, ((int)(resources.GetObject("Users_Panel.IconPadding"))));
			this.Users_Panel.Name = "Users_Panel";
			// 
			// Users_Panel.Panel1
			// 
			resources.ApplyResources(this.Users_Panel.Panel1, "Users_Panel.Panel1");
			this.Users_Panel.Panel1.Controls.Add(this.label1);
			this.Users_Panel.Panel1.Controls.Add(this.Users_TS);
			this.ErrorMng_EP.SetError(this.Users_Panel.Panel1, resources.GetString("Users_Panel.Panel1.Error"));
			this.HelpProvider.SetHelpKeyword(this.Users_Panel.Panel1, resources.GetString("Users_Panel.Panel1.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Users_Panel.Panel1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Users_Panel.Panel1.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Users_Panel.Panel1, resources.GetString("Users_Panel.Panel1.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Users_Panel.Panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Users_Panel.Panel1.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Users_Panel.Panel1, ((int)(resources.GetObject("Users_Panel.Panel1.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Users_Panel.Panel1, ((bool)(resources.GetObject("Users_Panel.Panel1.ShowHelp"))));
			// 
			// Users_Panel.Panel2
			// 
			resources.ApplyResources(this.Users_Panel.Panel2, "Users_Panel.Panel2");
			this.Users_Panel.Panel2.Controls.Add(this.Users_DGW);
			this.ErrorMng_EP.SetError(this.Users_Panel.Panel2, resources.GetString("Users_Panel.Panel2.Error"));
			this.HelpProvider.SetHelpKeyword(this.Users_Panel.Panel2, resources.GetString("Users_Panel.Panel2.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Users_Panel.Panel2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Users_Panel.Panel2.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Users_Panel.Panel2, resources.GetString("Users_Panel.Panel2.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Users_Panel.Panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Users_Panel.Panel2.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Users_Panel.Panel2, ((int)(resources.GetObject("Users_Panel.Panel2.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Users_Panel.Panel2, ((bool)(resources.GetObject("Users_Panel.Panel2.ShowHelp"))));
			this.HelpProvider.SetShowHelp(this.Users_Panel, ((bool)(resources.GetObject("Users_Panel.ShowHelp"))));
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.BackColor = System.Drawing.SystemColors.Control;
			this.ErrorMng_EP.SetError(this.label1, resources.GetString("label1.Error"));
			this.HelpProvider.SetHelpKeyword(this.label1, resources.GetString("label1.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.label1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label1.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.label1, resources.GetString("label1.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
			this.label1.Name = "label1";
			this.HelpProvider.SetShowHelp(this.label1, ((bool)(resources.GetObject("label1.ShowHelp"))));
			// 
			// Users_TS
			// 
			resources.ApplyResources(this.Users_TS, "Users_TS");
			this.ErrorMng_EP.SetError(this.Users_TS, resources.GetString("Users_TS.Error"));
			this.HelpProvider.SetHelpKeyword(this.Users_TS, resources.GetString("Users_TS.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Users_TS, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Users_TS.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Users_TS, resources.GetString("Users_TS.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Users_TS, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Users_TS.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Users_TS, ((int)(resources.GetObject("Users_TS.IconPadding"))));
			this.Users_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.Users_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewUser_TI,
            this.EditProducto_TI,
            this.DeleteProducto_TI,
            this.AttachUser_TI,
            this.DetachUser_TI,
            this.toolStripSeparator1,
            this.AdminUser_TI});
			this.Users_TS.Name = "Users_TS";
			this.HelpProvider.SetShowHelp(this.Users_TS, ((bool)(resources.GetObject("Users_TS.ShowHelp"))));
			// 
			// NewUser_TI
			// 
			resources.ApplyResources(this.NewUser_TI, "NewUser_TI");
			this.NewUser_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.NewUser_TI.Image = global::moleQule.Face.Properties.Resources.new_item;
			this.NewUser_TI.Name = "NewUser_TI";
			this.NewUser_TI.Click += new System.EventHandler(this.NewUser_TI_Click);
			// 
			// EditProducto_TI
			// 
			resources.ApplyResources(this.EditProducto_TI, "EditProducto_TI");
			this.EditProducto_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.EditProducto_TI.Image = global::moleQule.Face.Properties.Resources.edit_item;
			this.EditProducto_TI.Name = "EditProducto_TI";
			this.EditProducto_TI.Click += new System.EventHandler(this.EditUser_TI_Click);
			// 
			// DeleteProducto_TI
			// 
			resources.ApplyResources(this.DeleteProducto_TI, "DeleteProducto_TI");
			this.DeleteProducto_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DeleteProducto_TI.Image = global::moleQule.Face.Properties.Resources.delete_item;
			this.DeleteProducto_TI.Name = "DeleteProducto_TI";
			this.DeleteProducto_TI.Click += new System.EventHandler(this.DeleteUser_TI_Click);
			// 
			// AttachUser_TI
			// 
			resources.ApplyResources(this.AttachUser_TI, "AttachUser_TI");
			this.AttachUser_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AttachUser_TI.Image = global::moleQule.Face.Properties.Resources.add_item1;
			this.AttachUser_TI.Name = "AttachUser_TI";
			this.AttachUser_TI.Click += new System.EventHandler(this.AttachUser_TI_Click);
			// 
			// DetachUser_TI
			// 
			resources.ApplyResources(this.DetachUser_TI, "DetachUser_TI");
			this.DetachUser_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DetachUser_TI.Image = global::moleQule.Face.Properties.Resources.unselect_item;
			this.DetachUser_TI.Name = "DetachUser_TI";
			this.DetachUser_TI.Click += new System.EventHandler(this.DettachUser_TI_Click);
			// 
			// toolStripSeparator1
			// 
			resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			// 
			// AdminUser_TI
			// 
			resources.ApplyResources(this.AdminUser_TI, "AdminUser_TI");
			this.AdminUser_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AdminUser_TI.Image = global::moleQule.Face.Properties.Resources.set_admin_user;
			this.AdminUser_TI.Name = "AdminUser_TI";
			this.AdminUser_TI.Click += new System.EventHandler(this.AdminUser_TI_Click);
			// 
			// Users_DGW
			// 
			resources.ApplyResources(this.Users_DGW, "Users_DGW");
			this.Users_DGW.AllowUserToAddRows = false;
			this.Users_DGW.AllowUserToDeleteRows = false;
			this.Users_DGW.AutoGenerateColumns = false;
			this.Users_DGW.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.Users_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Password,
            this.Usuario,
            this.IsSuperUser,
            this.IsAdmin,
            this.IsPartner,
            this.EstadoLabel,
            this.Empresa});
			this.Users_DGW.DataSource = this.Datos_Usuarios;
			this.ErrorMng_EP.SetError(this.Users_DGW, resources.GetString("Users_DGW.Error"));
			this.HelpProvider.SetHelpKeyword(this.Users_DGW, resources.GetString("Users_DGW.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Users_DGW, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Users_DGW.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Users_DGW, resources.GetString("Users_DGW.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Users_DGW, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Users_DGW.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Users_DGW, ((int)(resources.GetObject("Users_DGW.IconPadding"))));
			this.Users_DGW.MultiSelect = false;
			this.Users_DGW.Name = "Users_DGW";
			this.HelpProvider.SetShowHelp(this.Users_DGW, ((bool)(resources.GetObject("Users_DGW.ShowHelp"))));
			this.Users_DGW.SelectionChanged += new System.EventHandler(this.Users_DGW_SelectionChanged);
			// 
			// Datos_Usuarios
			// 
			this.Datos_Usuarios.DataSource = typeof(moleQule.Library.Users);
			// 
			// Privileges_Panel
			// 
			resources.ApplyResources(this.Privileges_Panel, "Privileges_Panel");
			this.ErrorMng_EP.SetError(this.Privileges_Panel, resources.GetString("Privileges_Panel.Error"));
			this.Privileges_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.HelpProvider.SetHelpKeyword(this.Privileges_Panel, resources.GetString("Privileges_Panel.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Privileges_Panel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Privileges_Panel.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Privileges_Panel, resources.GetString("Privileges_Panel.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Privileges_Panel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Privileges_Panel.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Privileges_Panel, ((int)(resources.GetObject("Privileges_Panel.IconPadding"))));
			this.Privileges_Panel.Name = "Privileges_Panel";
			// 
			// Privileges_Panel.Panel1
			// 
			resources.ApplyResources(this.Privileges_Panel.Panel1, "Privileges_Panel.Panel1");
			this.Privileges_Panel.Panel1.Controls.Add(this.label2);
			this.Privileges_Panel.Panel1.Controls.Add(this.Privileges_TS);
			this.ErrorMng_EP.SetError(this.Privileges_Panel.Panel1, resources.GetString("Privileges_Panel.Panel1.Error"));
			this.HelpProvider.SetHelpKeyword(this.Privileges_Panel.Panel1, resources.GetString("Privileges_Panel.Panel1.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Privileges_Panel.Panel1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Privileges_Panel.Panel1.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Privileges_Panel.Panel1, resources.GetString("Privileges_Panel.Panel1.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Privileges_Panel.Panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Privileges_Panel.Panel1.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Privileges_Panel.Panel1, ((int)(resources.GetObject("Privileges_Panel.Panel1.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Privileges_Panel.Panel1, ((bool)(resources.GetObject("Privileges_Panel.Panel1.ShowHelp"))));
			// 
			// Privileges_Panel.Panel2
			// 
			resources.ApplyResources(this.Privileges_Panel.Panel2, "Privileges_Panel.Panel2");
			this.Privileges_Panel.Panel2.Controls.Add(this.Privileges_DGW);
			this.ErrorMng_EP.SetError(this.Privileges_Panel.Panel2, resources.GetString("Privileges_Panel.Panel2.Error"));
			this.HelpProvider.SetHelpKeyword(this.Privileges_Panel.Panel2, resources.GetString("Privileges_Panel.Panel2.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Privileges_Panel.Panel2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Privileges_Panel.Panel2.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Privileges_Panel.Panel2, resources.GetString("Privileges_Panel.Panel2.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Privileges_Panel.Panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Privileges_Panel.Panel2.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Privileges_Panel.Panel2, ((int)(resources.GetObject("Privileges_Panel.Panel2.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Privileges_Panel.Panel2, ((bool)(resources.GetObject("Privileges_Panel.Panel2.ShowHelp"))));
			this.HelpProvider.SetShowHelp(this.Privileges_Panel, ((bool)(resources.GetObject("Privileges_Panel.ShowHelp"))));
			// 
			// label2
			// 
			resources.ApplyResources(this.label2, "label2");
			this.label2.BackColor = System.Drawing.SystemColors.Control;
			this.ErrorMng_EP.SetError(this.label2, resources.GetString("label2.Error"));
			this.HelpProvider.SetHelpKeyword(this.label2, resources.GetString("label2.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.label2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("label2.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.label2, resources.GetString("label2.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
			this.label2.Name = "label2";
			this.HelpProvider.SetShowHelp(this.label2, ((bool)(resources.GetObject("label2.ShowHelp"))));
			// 
			// Privileges_TS
			// 
			resources.ApplyResources(this.Privileges_TS, "Privileges_TS");
			this.ErrorMng_EP.SetError(this.Privileges_TS, resources.GetString("Privileges_TS.Error"));
			this.HelpProvider.SetHelpKeyword(this.Privileges_TS, resources.GetString("Privileges_TS.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Privileges_TS, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Privileges_TS.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Privileges_TS, resources.GetString("Privileges_TS.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Privileges_TS, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Privileges_TS.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Privileges_TS, ((int)(resources.GetObject("Privileges_TS.IconPadding"))));
			this.Privileges_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.Privileges_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetAll_TI,
            this.UnsetAll_TI});
			this.Privileges_TS.Name = "Privileges_TS";
			this.HelpProvider.SetShowHelp(this.Privileges_TS, ((bool)(resources.GetObject("Privileges_TS.ShowHelp"))));
			// 
			// SetAll_TI
			// 
			resources.ApplyResources(this.SetAll_TI, "SetAll_TI");
			this.SetAll_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SetAll_TI.Image = global::moleQule.Face.Properties.Resources.select_all;
			this.SetAll_TI.Name = "SetAll_TI";
			this.SetAll_TI.Click += new System.EventHandler(this.AsignarPermisos_Button_Click);
			// 
			// UnsetAll_TI
			// 
			resources.ApplyResources(this.UnsetAll_TI, "UnsetAll_TI");
			this.UnsetAll_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.UnsetAll_TI.Image = global::moleQule.Face.Properties.Resources.unselect_all;
			this.UnsetAll_TI.Name = "UnsetAll_TI";
			this.UnsetAll_TI.Click += new System.EventHandler(this.QuitarPermisos_Button_Click);
			// 
			// Privileges_DGW
			// 
			resources.ApplyResources(this.Privileges_DGW, "Privileges_DGW");
			this.Privileges_DGW.AllowUserToAddRows = false;
			this.Privileges_DGW.AllowUserToDeleteRows = false;
			this.Privileges_DGW.AutoGenerateColumns = false;
			this.Privileges_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Item,
            this.Read,
            this.Create,
            this.Modify,
            this.Remove});
			this.Privileges_DGW.DataSource = this.Datos_Permisos;
			this.ErrorMng_EP.SetError(this.Privileges_DGW, resources.GetString("Privileges_DGW.Error"));
			this.HelpProvider.SetHelpKeyword(this.Privileges_DGW, resources.GetString("Privileges_DGW.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Privileges_DGW, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Privileges_DGW.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Privileges_DGW, resources.GetString("Privileges_DGW.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Privileges_DGW, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Privileges_DGW.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Privileges_DGW, ((int)(resources.GetObject("Privileges_DGW.IconPadding"))));
			this.Privileges_DGW.MultiSelect = false;
			this.Privileges_DGW.Name = "Privileges_DGW";
			this.HelpProvider.SetShowHelp(this.Privileges_DGW, ((bool)(resources.GetObject("Privileges_DGW.ShowHelp"))));
			this.Privileges_DGW.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Privileges_DGW_CellClick);
			// 
			// Item
			// 
			this.Item.DataPropertyName = "ItemLabel";
			resources.ApplyResources(this.Item, "Item");
			this.Item.Name = "Item";
			this.Item.ReadOnly = true;
			// 
			// Read
			// 
			this.Read.DataPropertyName = "CanRead";
			resources.ApplyResources(this.Read, "Read");
			this.Read.Name = "Read";
			this.Read.ReadOnly = true;
			this.Read.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// Create
			// 
			this.Create.DataPropertyName = "CanCreate";
			resources.ApplyResources(this.Create, "Create");
			this.Create.Name = "Create";
			this.Create.ReadOnly = true;
			this.Create.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// Modify
			// 
			this.Modify.DataPropertyName = "CanModify";
			resources.ApplyResources(this.Modify, "Modify");
			this.Modify.Name = "Modify";
			this.Modify.ReadOnly = true;
			this.Modify.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// Remove
			// 
			this.Remove.DataPropertyName = "CanDelete";
			resources.ApplyResources(this.Remove, "Remove");
			this.Remove.Name = "Remove";
			this.Remove.ReadOnly = true;
			this.Remove.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// Datos_Permisos
			// 
			this.Datos_Permisos.DataSource = typeof(moleQule.Library.Privileges);
			// 
			// Main_Panel
			// 
			resources.ApplyResources(this.Main_Panel, "Main_Panel");
			this.ErrorMng_EP.SetError(this.Main_Panel, resources.GetString("Main_Panel.Error"));
			this.HelpProvider.SetHelpKeyword(this.Main_Panel, resources.GetString("Main_Panel.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Main_Panel, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Main_Panel.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Main_Panel, resources.GetString("Main_Panel.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Main_Panel, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Main_Panel.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Main_Panel, ((int)(resources.GetObject("Main_Panel.IconPadding"))));
			this.Main_Panel.Name = "Main_Panel";
			// 
			// Main_Panel.Panel1
			// 
			resources.ApplyResources(this.Main_Panel.Panel1, "Main_Panel.Panel1");
			this.Main_Panel.Panel1.Controls.Add(this.Users_Panel);
			this.ErrorMng_EP.SetError(this.Main_Panel.Panel1, resources.GetString("Main_Panel.Panel1.Error"));
			this.HelpProvider.SetHelpKeyword(this.Main_Panel.Panel1, resources.GetString("Main_Panel.Panel1.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Main_Panel.Panel1, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Main_Panel.Panel1.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Main_Panel.Panel1, resources.GetString("Main_Panel.Panel1.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Main_Panel.Panel1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Main_Panel.Panel1.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Main_Panel.Panel1, ((int)(resources.GetObject("Main_Panel.Panel1.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Main_Panel.Panel1, ((bool)(resources.GetObject("Main_Panel.Panel1.ShowHelp"))));
			// 
			// Main_Panel.Panel2
			// 
			resources.ApplyResources(this.Main_Panel.Panel2, "Main_Panel.Panel2");
			this.Main_Panel.Panel2.Controls.Add(this.Privileges_Panel);
			this.ErrorMng_EP.SetError(this.Main_Panel.Panel2, resources.GetString("Main_Panel.Panel2.Error"));
			this.HelpProvider.SetHelpKeyword(this.Main_Panel.Panel2, resources.GetString("Main_Panel.Panel2.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this.Main_Panel.Panel2, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("Main_Panel.Panel2.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this.Main_Panel.Panel2, resources.GetString("Main_Panel.Panel2.HelpString"));
			this.ErrorMng_EP.SetIconAlignment(this.Main_Panel.Panel2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("Main_Panel.Panel2.IconAlignment"))));
			this.ErrorMng_EP.SetIconPadding(this.Main_Panel.Panel2, ((int)(resources.GetObject("Main_Panel.Panel2.IconPadding"))));
			this.HelpProvider.SetShowHelp(this.Main_Panel.Panel2, ((bool)(resources.GetObject("Main_Panel.Panel2.ShowHelp"))));
			this.HelpProvider.SetShowHelp(this.Main_Panel, ((bool)(resources.GetObject("Main_Panel.ShowHelp"))));
			// 
			// Password
			// 
			this.Password.DataPropertyName = "Password";
			resources.ApplyResources(this.Password, "Password");
			this.Password.Name = "Password";
			this.Password.ReadOnly = true;
			// 
			// Usuario
			// 
			this.Usuario.DataPropertyName = "Name";
			resources.ApplyResources(this.Usuario, "Usuario");
			this.Usuario.Name = "Usuario";
			this.Usuario.ReadOnly = true;
			// 
			// IsSuperUser
			// 
			this.IsSuperUser.DataPropertyName = "IsSuperUser";
			resources.ApplyResources(this.IsSuperUser, "IsSuperUser");
			this.IsSuperUser.Name = "IsSuperUser";
			// 
			// IsAdmin
			// 
			this.IsAdmin.DataPropertyName = "IsAdmin";
			resources.ApplyResources(this.IsAdmin, "IsAdmin");
			this.IsAdmin.Name = "IsAdmin";
			// 
			// IsPartner
			// 
			this.IsPartner.DataPropertyName = "IsPartner";
			resources.ApplyResources(this.IsPartner, "IsPartner");
			this.IsPartner.Name = "IsPartner";
			// 
			// EstadoLabel
			// 
			this.EstadoLabel.DataPropertyName = "EstadoLabel";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.EstadoLabel.DefaultCellStyle = dataGridViewCellStyle1;
			resources.ApplyResources(this.EstadoLabel, "EstadoLabel");
			this.EstadoLabel.Name = "EstadoLabel";
			this.EstadoLabel.ReadOnly = true;
			// 
			// Empresa
			// 
			this.Empresa.DataPropertyName = "DefaultSchema";
			resources.ApplyResources(this.Empresa, "Empresa");
			this.Empresa.Name = "Empresa";
			this.Empresa.ReadOnly = true;
			this.Empresa.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Empresa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// UsersUIForm
			// 
			resources.ApplyResources(this, "$this");
			this.HelpProvider.SetHelpKeyword(this, resources.GetString("$this.HelpKeyword"));
			this.HelpProvider.SetHelpNavigator(this, ((System.Windows.Forms.HelpNavigator)(resources.GetObject("$this.HelpNavigator"))));
			this.HelpProvider.SetHelpString(this, resources.GetString("$this.HelpString"));
			this.Name = "UsersUIForm";
			this.HelpProvider.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UsuarioUIForm_FormClosing);
			this.Shown += new System.EventHandler(this.UsuarioUIForm_Shown);
			this.PanelesV.Panel1.ResumeLayout(false);
			this.PanelesV.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.PanelesV)).EndInit();
			this.PanelesV.ResumeLayout(false);
			this.Paneles2.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Paneles2)).EndInit();
			this.Paneles2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ErrorMng_EP)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Animation)).EndInit();
			this.Progress_Panel.ResumeLayout(false);
			this.Progress_Panel.PerformLayout();
			this.ProgressBK_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Progress_PB)).EndInit();
			this.Users_Panel.Panel1.ResumeLayout(false);
			this.Users_Panel.Panel1.PerformLayout();
			this.Users_Panel.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Users_Panel)).EndInit();
			this.Users_Panel.ResumeLayout(false);
			this.Users_TS.ResumeLayout(false);
			this.Users_TS.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Users_DGW)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Usuarios)).EndInit();
			this.Privileges_Panel.Panel1.ResumeLayout(false);
			this.Privileges_Panel.Panel1.PerformLayout();
			this.Privileges_Panel.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Privileges_Panel)).EndInit();
			this.Privileges_Panel.ResumeLayout(false);
			this.Privileges_TS.ResumeLayout(false);
			this.Privileges_TS.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.Privileges_DGW)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Permisos)).EndInit();
			this.Main_Panel.Panel1.ResumeLayout(false);
			this.Main_Panel.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Main_Panel)).EndInit();
			this.Main_Panel.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.BindingSource Datos_Permisos;
		protected System.Windows.Forms.SplitContainer Privileges_Panel;
		protected System.Windows.Forms.ToolStrip Privileges_TS;
		protected System.Windows.Forms.ToolStripButton SetAll_TI;
		protected System.Windows.Forms.ToolStripButton UnsetAll_TI;
		protected System.Windows.Forms.SplitContainer Users_Panel;
		protected System.Windows.Forms.ToolStrip Users_TS;
		protected System.Windows.Forms.ToolStripButton NewUser_TI;
		protected System.Windows.Forms.ToolStripButton EditProducto_TI;
		protected System.Windows.Forms.ToolStripButton DeleteProducto_TI;
		private System.Windows.Forms.ToolStripButton AttachUser_TI;
		private System.Windows.Forms.SplitContainer Main_Panel;
		private System.Windows.Forms.DataGridView Users_DGW;
		private System.Windows.Forms.BindingSource Datos_Usuarios;
		private System.Windows.Forms.ToolStripButton DetachUser_TI;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton AdminUser_TI;
		private System.Windows.Forms.DataGridViewTextBoxColumn Item;
		private System.Windows.Forms.DataGridViewButtonColumn Read;
		private System.Windows.Forms.DataGridViewButtonColumn Create;
		private System.Windows.Forms.DataGridViewButtonColumn Modify;
		private System.Windows.Forms.DataGridViewButtonColumn Remove;
		protected System.Windows.Forms.DataGridView Privileges_DGW;
		private System.Windows.Forms.DataGridViewTextBoxColumn Password;
		private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsSuperUser;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsAdmin;
		private System.Windows.Forms.DataGridViewCheckBoxColumn IsPartner;
		private System.Windows.Forms.DataGridViewTextBoxColumn EstadoLabel;
		private System.Windows.Forms.DataGridViewButtonColumn Empresa;
    }
}
