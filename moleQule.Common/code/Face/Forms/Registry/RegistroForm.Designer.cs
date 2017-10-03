namespace moleQule.Face.Common
{
	partial class RegistroForm
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
			System.Windows.Forms.Label nombreLabel;
			System.Windows.Forms.Label numeroClienteLabel;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label fechaLabel;
			System.Windows.Forms.Label estadoLabel;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistroForm));
			this.Datos_LineaRegistros = new System.Windows.Forms.BindingSource(this.components);
			this.Main_Panel = new System.Windows.Forms.SplitContainer();
			this.Usuario_TB = new System.Windows.Forms.TextBox();
			this.Estado_CB = new System.Windows.Forms.ComboBox();
			this.Datos_Estados = new System.Windows.Forms.BindingSource(this.components);
			this.ID_TB = new System.Windows.Forms.TextBox();
			this.fechaDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.Observaciones_RTB = new System.Windows.Forms.RichTextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.TipoRegistro_CB = new System.Windows.Forms.ComboBox();
			this.Datos_TipoRegistros = new System.Windows.Forms.BindingSource(this.components);
			this.Nombre_TB = new System.Windows.Forms.TextBox();
			this.Productos_Panel = new System.Windows.Forms.SplitContainer();
			this.Productos_TS = new System.Windows.Forms.ToolStrip();
			this.SelectAll_TI = new System.Windows.Forms.ToolStripButton();
			this.Null_TI = new System.Windows.Forms.ToolStripButton();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.Lock_TI = new System.Windows.Forms.ToolStripButton();
			this.LineaRegistros_DGW = new System.Windows.Forms.DataGridView();
			this.Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TipoRegistroLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CodigoEntidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.EstadoEntidadLabel = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.IDExportacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Estado = new System.Windows.Forms.DataGridViewButtonColumn();
			this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
			nombreLabel = new System.Windows.Forms.Label();
			numeroClienteLabel = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			fechaLabel = new System.Windows.Forms.Label();
			estadoLabel = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
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
			((System.ComponentModel.ISupportInitialize)(this.Datos_LineaRegistros)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Main_Panel)).BeginInit();
			this.Main_Panel.Panel1.SuspendLayout();
			this.Main_Panel.Panel2.SuspendLayout();
			this.Main_Panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Datos_Estados)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_TipoRegistros)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Productos_Panel)).BeginInit();
			this.Productos_Panel.Panel1.SuspendLayout();
			this.Productos_Panel.Panel2.SuspendLayout();
			this.Productos_Panel.SuspendLayout();
			this.Productos_TS.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.LineaRegistros_DGW)).BeginInit();
			this.SuspendLayout();
			// 
			// PanelesV
			// 
			// 
			// PanelesV.Panel1
			// 
			this.PanelesV.Panel1.Controls.Add(this.Main_Panel);
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel1, true);
			// 
			// PanelesV.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.PanelesV.Panel2, true);
			this.HelpProvider.SetShowHelp(this.PanelesV, true);
			this.PanelesV.Size = new System.Drawing.Size(994, 791);
			this.PanelesV.SplitterDistance = 736;
			// 
			// Submit_BT
			// 
			this.Submit_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Submit_BT.Location = new System.Drawing.Point(252, 6);
			this.HelpProvider.SetShowHelp(this.Submit_BT, true);
			// 
			// Cancel_BT
			// 
			this.Cancel_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Cancel_BT.Location = new System.Drawing.Point(348, 6);
			this.HelpProvider.SetShowHelp(this.Cancel_BT, true);
			// 
			// Paneles2
			// 
			// 
			// Paneles2.Panel1
			// 
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel1, true);
			// 
			// Paneles2.Panel2
			// 
			this.HelpProvider.SetShowHelp(this.Paneles2.Panel2, true);
			this.HelpProvider.SetShowHelp(this.Paneles2, true);
			this.Paneles2.Size = new System.Drawing.Size(992, 52);
			// 
			// Imprimir_Button
			// 
			this.Imprimir_Button.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Imprimir_Button.Location = new System.Drawing.Point(156, 6);
			this.HelpProvider.SetShowHelp(this.Imprimir_Button, true);
			// 
			// Docs_BT
			// 
			this.Docs_BT.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
			this.Docs_BT.Location = new System.Drawing.Point(190, 6);
			this.HelpProvider.SetShowHelp(this.Docs_BT, true);
			// 
			// Datos
			// 
			this.Datos.DataSource = typeof(moleQule.Library.Common.Registro);
			// 
			// Progress_Panel
			// 
			this.Progress_Panel.Location = new System.Drawing.Point(318, 286);
			// 
			// ProgressBK_Panel
			// 
			this.ProgressBK_Panel.Size = new System.Drawing.Size(994, 791);
			// 
			// ProgressInfo_PB
			// 
			this.ProgressInfo_PB.Location = new System.Drawing.Point(465, 444);
			// 
			// Progress_PB
			// 
			this.Progress_PB.Location = new System.Drawing.Point(465, 359);
			// 
			// nombreLabel
			// 
			nombreLabel.AutoSize = true;
			nombreLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			nombreLabel.Location = new System.Drawing.Point(28, 56);
			nombreLabel.Name = "nombreLabel";
			nombreLabel.Size = new System.Drawing.Size(54, 13);
			nombreLabel.TabIndex = 11;
			nombreLabel.Text = "Nombre*:";
			// 
			// numeroClienteLabel
			// 
			numeroClienteLabel.AutoSize = true;
			numeroClienteLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			numeroClienteLabel.Location = new System.Drawing.Point(28, 12);
			numeroClienteLabel.Name = "numeroClienteLabel";
			numeroClienteLabel.Size = new System.Drawing.Size(22, 13);
			numeroClienteLabel.TabIndex = 13;
			numeroClienteLabel.Text = "ID:";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label3.Location = new System.Drawing.Point(28, 101);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(82, 13);
			label3.TabIndex = 42;
			label3.Text = "Observaciones:";
			// 
			// fechaLabel
			// 
			fechaLabel.AutoSize = true;
			fechaLabel.Location = new System.Drawing.Point(155, 12);
			fechaLabel.Name = "fechaLabel";
			fechaLabel.Size = new System.Drawing.Size(40, 13);
			fechaLabel.TabIndex = 42;
			fechaLabel.Text = "Fecha:";
			// 
			// estadoLabel
			// 
			estadoLabel.AutoSize = true;
			estadoLabel.Location = new System.Drawing.Point(588, 12);
			estadoLabel.Name = "estadoLabel";
			estadoLabel.Size = new System.Drawing.Size(44, 13);
			estadoLabel.TabIndex = 44;
			estadoLabel.Text = "Estado:";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label1.Location = new System.Drawing.Point(787, 10);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(47, 13);
			label1.TabIndex = 46;
			label1.Text = "Usuario:";
			// 
			// Datos_LineaRegistros
			// 
			this.Datos_LineaRegistros.DataSource = typeof(moleQule.Library.Common.LineaRegistroList);
			// 
			// Main_Panel
			// 
			this.Main_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Main_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.Main_Panel.Location = new System.Drawing.Point(0, 0);
			this.Main_Panel.Name = "Main_Panel";
			this.Main_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// Main_Panel.Panel1
			// 
			this.Main_Panel.Panel1.AutoScroll = true;
			this.Main_Panel.Panel1.Controls.Add(label1);
			this.Main_Panel.Panel1.Controls.Add(this.Usuario_TB);
			this.Main_Panel.Panel1.Controls.Add(estadoLabel);
			this.Main_Panel.Panel1.Controls.Add(this.Estado_CB);
			this.Main_Panel.Panel1.Controls.Add(this.ID_TB);
			this.Main_Panel.Panel1.Controls.Add(fechaLabel);
			this.Main_Panel.Panel1.Controls.Add(this.fechaDateTimePicker);
			this.Main_Panel.Panel1.Controls.Add(label3);
			this.Main_Panel.Panel1.Controls.Add(this.Observaciones_RTB);
			this.Main_Panel.Panel1.Controls.Add(this.label9);
			this.Main_Panel.Panel1.Controls.Add(this.TipoRegistro_CB);
			this.Main_Panel.Panel1.Controls.Add(nombreLabel);
			this.Main_Panel.Panel1.Controls.Add(this.Nombre_TB);
			this.Main_Panel.Panel1.Controls.Add(numeroClienteLabel);
			// 
			// Main_Panel.Panel2
			// 
			this.Main_Panel.Panel2.Controls.Add(this.Productos_Panel);
			this.Main_Panel.Size = new System.Drawing.Size(992, 734);
			this.Main_Panel.SplitterDistance = 201;
			this.Main_Panel.TabIndex = 6;
			// 
			// Usuario_TB
			// 
			this.Usuario_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Usuario", true));
			this.Usuario_TB.Enabled = false;
			this.Usuario_TB.Location = new System.Drawing.Point(787, 28);
			this.Usuario_TB.Name = "Usuario_TB";
			this.Usuario_TB.ReadOnly = true;
			this.Usuario_TB.Size = new System.Drawing.Size(175, 21);
			this.Usuario_TB.TabIndex = 47;
			this.Usuario_TB.TabStop = false;
			// 
			// Estado_CB
			// 
			this.Estado_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos, "Estado", true));
			this.Estado_CB.DataSource = this.Datos_Estados;
			this.Estado_CB.DisplayMember = "Texto";
			this.Estado_CB.FormattingEnabled = true;
			this.Estado_CB.Location = new System.Drawing.Point(591, 28);
			this.Estado_CB.Name = "Estado_CB";
			this.Estado_CB.Size = new System.Drawing.Size(148, 21);
			this.Estado_CB.TabIndex = 45;
			this.Estado_CB.ValueMember = "Oid";
			// 
			// Datos_Estados
			// 
			this.Datos_Estados.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
			// 
			// ID_TB
			// 
			this.ID_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
			this.ID_TB.Enabled = false;
			this.ID_TB.Location = new System.Drawing.Point(31, 28);
			this.ID_TB.Name = "ID_TB";
			this.ID_TB.ReadOnly = true;
			this.ID_TB.Size = new System.Drawing.Size(79, 21);
			this.ID_TB.TabIndex = 44;
			this.ID_TB.TabStop = false;
			this.ID_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// fechaDateTimePicker
			// 
			this.fechaDateTimePicker.CustomFormat = "dd/MM/yyyy HH:mm";
			this.fechaDateTimePicker.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.Datos, "Fecha", true));
			this.fechaDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.fechaDateTimePicker.Location = new System.Drawing.Point(158, 28);
			this.fechaDateTimePicker.Name = "fechaDateTimePicker";
			this.fechaDateTimePicker.ShowCheckBox = true;
			this.fechaDateTimePicker.Size = new System.Drawing.Size(145, 21);
			this.fechaDateTimePicker.TabIndex = 43;
			// 
			// Observaciones_RTB
			// 
			this.Observaciones_RTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
			this.Observaciones_RTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Observaciones_RTB.Location = new System.Drawing.Point(31, 117);
			this.Observaciones_RTB.Name = "Observaciones_RTB";
			this.Observaciones_RTB.Size = new System.Drawing.Size(934, 67);
			this.Observaciones_RTB.TabIndex = 41;
			this.Observaciones_RTB.Text = "Texto";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.Location = new System.Drawing.Point(348, 10);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(89, 13);
			this.label9.TabIndex = 40;
			this.label9.Text = "Tipo de Registro:";
			// 
			// TipoRegistro_CB
			// 
			this.TipoRegistro_CB.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.Datos, "TipoRegistro", true));
			this.TipoRegistro_CB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos_TipoRegistros, "Texto", true));
			this.TipoRegistro_CB.DataSource = this.Datos_TipoRegistros;
			this.TipoRegistro_CB.DisplayMember = "Texto";
			this.TipoRegistro_CB.FormattingEnabled = true;
			this.TipoRegistro_CB.Location = new System.Drawing.Point(351, 28);
			this.TipoRegistro_CB.Name = "TipoRegistro_CB";
			this.TipoRegistro_CB.Size = new System.Drawing.Size(192, 21);
			this.TipoRegistro_CB.TabIndex = 39;
			this.TipoRegistro_CB.ValueMember = "Oid";
			// 
			// Datos_TipoRegistros
			// 
			this.Datos_TipoRegistros.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
			// 
			// Nombre_TB
			// 
			this.Nombre_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Nombre", true));
			this.Nombre_TB.Location = new System.Drawing.Point(31, 74);
			this.Nombre_TB.Name = "Nombre_TB";
			this.Nombre_TB.Size = new System.Drawing.Size(934, 21);
			this.Nombre_TB.TabIndex = 12;
			// 
			// Productos_Panel
			// 
			this.Productos_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Productos_Panel.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.Productos_Panel.Location = new System.Drawing.Point(0, 0);
			this.Productos_Panel.Name = "Productos_Panel";
			this.Productos_Panel.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// Productos_Panel.Panel1
			// 
			this.Productos_Panel.Panel1.Controls.Add(this.Productos_TS);
			this.Productos_Panel.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			// 
			// Productos_Panel.Panel2
			// 
			this.Productos_Panel.Panel2.Controls.Add(this.LineaRegistros_DGW);
			this.Productos_Panel.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Productos_Panel.Size = new System.Drawing.Size(992, 529);
			this.Productos_Panel.SplitterDistance = 38;
			this.Productos_Panel.SplitterWidth = 1;
			this.Productos_Panel.TabIndex = 7;
			// 
			// Productos_TS
			// 
			this.Productos_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.Productos_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectAll_TI,
            this.Null_TI,
            this.toolStripLabel1,
            this.Lock_TI});
			this.Productos_TS.Location = new System.Drawing.Point(0, 0);
			this.Productos_TS.Name = "Productos_TS";
			this.HelpProvider.SetShowHelp(this.Productos_TS, true);
			this.Productos_TS.Size = new System.Drawing.Size(992, 39);
			this.Productos_TS.TabIndex = 4;
			// 
			// SelectAll_TI
			// 
			this.SelectAll_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SelectAll_TI.Image = global::moleQule.Face.Common.Properties.Resources.selectAll;
			this.SelectAll_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SelectAll_TI.Name = "SelectAll_TI";
			this.SelectAll_TI.Size = new System.Drawing.Size(36, 36);
			this.SelectAll_TI.Text = "Seleccionar Todos";
			this.SelectAll_TI.Visible = false;
			this.SelectAll_TI.Click += new System.EventHandler(this.SelectAll_TI_Click);
			// 
			// Null_TI
			// 
			this.Null_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Null_TI.Image = global::moleQule.Face.Common.Properties.Resources.state_null;
			this.Null_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Null_TI.Name = "Null_TI";
			this.Null_TI.Size = new System.Drawing.Size(36, 36);
			this.Null_TI.Text = "Seleccionar";
			this.Null_TI.Click += new System.EventHandler(this.Null_TI_Click);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(0, 36);
			// 
			// Lock_TI
			// 
			this.Lock_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.Lock_TI.Image = global::moleQule.Face.Common.Properties.Resources.state_lock;
			this.Lock_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Lock_TI.Name = "Lock_TI";
			this.Lock_TI.Size = new System.Drawing.Size(36, 36);
			this.Lock_TI.Text = "Cambiar Estado";
			this.Lock_TI.Click += new System.EventHandler(this.Lock_TI_Click);
			// 
			// LineaRegistros_DGW
			// 
			this.LineaRegistros_DGW.AllowUserToAddRows = false;
			this.LineaRegistros_DGW.AllowUserToDeleteRows = false;
			this.LineaRegistros_DGW.AllowUserToOrderColumns = true;
			this.LineaRegistros_DGW.AutoGenerateColumns = false;
			this.LineaRegistros_DGW.ColumnHeadersHeight = 36;
			this.LineaRegistros_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seleccionar,
            this.Codigo,
            this.TipoRegistroLabel,
            this.CodigoEntidad,
            this.Descripcion,
            this.EstadoEntidadLabel,
            this.IDExportacion,
            this.Estado,
            this.Observaciones});
			this.LineaRegistros_DGW.DataSource = this.Datos_LineaRegistros;
			this.LineaRegistros_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LineaRegistros_DGW.Location = new System.Drawing.Point(0, 0);
			this.LineaRegistros_DGW.Name = "LineaRegistros_DGW";
			this.LineaRegistros_DGW.RowHeadersWidth = 25;
			this.LineaRegistros_DGW.Size = new System.Drawing.Size(992, 490);
			this.LineaRegistros_DGW.TabIndex = 7;
			this.LineaRegistros_DGW.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.LineaRegistros_DGW_CellClick);
			// 
			// Seleccionar
			// 
			this.Seleccionar.FalseValue = "False";
			this.Seleccionar.Name = "Seleccionar";
			this.Seleccionar.TrueValue = "True";
			this.Seleccionar.Visible = false;
			this.Seleccionar.Width = 20;
			// 
			// Codigo
			// 
			this.Codigo.DataPropertyName = "Codigo";
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.Codigo.DefaultCellStyle = dataGridViewCellStyle1;
			this.Codigo.HeaderText = "ID";
			this.Codigo.Name = "Codigo";
			this.Codigo.ReadOnly = true;
			this.Codigo.Width = 45;
			// 
			// TipoRegistroLabel
			// 
			this.TipoRegistroLabel.DataPropertyName = "TipoEntidadLabel";
			this.TipoRegistroLabel.HeaderText = "Tipo Entidad";
			this.TipoRegistroLabel.Name = "TipoRegistroLabel";
			this.TipoRegistroLabel.ReadOnly = true;
			// 
			// CodigoEntidad
			// 
			this.CodigoEntidad.DataPropertyName = "CodigoEntidad";
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.CodigoEntidad.DefaultCellStyle = dataGridViewCellStyle2;
			this.CodigoEntidad.HeaderText = "ID Entidad";
			this.CodigoEntidad.Name = "CodigoEntidad";
			this.CodigoEntidad.ReadOnly = true;
			this.CodigoEntidad.Width = 50;
			// 
			// Descripcion
			// 
			this.Descripcion.DataPropertyName = "Descripcion";
			this.Descripcion.HeaderText = "Descripción";
			this.Descripcion.Name = "Descripcion";
			this.Descripcion.ReadOnly = true;
			this.Descripcion.Width = 360;
			// 
			// EstadoEntidadLabel
			// 
			this.EstadoEntidadLabel.DataPropertyName = "EstadoEntidadLabel";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.EstadoEntidadLabel.DefaultCellStyle = dataGridViewCellStyle3;
			this.EstadoEntidadLabel.HeaderText = "Estado Entidad";
			this.EstadoEntidadLabel.Name = "EstadoEntidadLabel";
			this.EstadoEntidadLabel.ReadOnly = true;
			this.EstadoEntidadLabel.Width = 110;
			// 
			// IDExportacion
			// 
			this.IDExportacion.DataPropertyName = "IDExportacion";
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.IDExportacion.DefaultCellStyle = dataGridViewCellStyle4;
			this.IDExportacion.HeaderText = "ID Mov. Contable";
			this.IDExportacion.Name = "IDExportacion";
			this.IDExportacion.ReadOnly = true;
			this.IDExportacion.Width = 70;
			// 
			// Estado
			// 
			this.Estado.DataPropertyName = "EstadoLabel";
			this.Estado.HeaderText = "Estado";
			this.Estado.Name = "Estado";
			this.Estado.ReadOnly = true;
			this.Estado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.Estado.Width = 110;
			// 
			// Observaciones
			// 
			this.Observaciones.DataPropertyName = "Observaciones";
			this.Observaciones.HeaderText = "Observaciones";
			this.Observaciones.Name = "Observaciones";
			this.Observaciones.ReadOnly = true;
			// 
			// RegistroForm
			// 
			this.ClientSize = new System.Drawing.Size(994, 791);
			this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "RegistroForm";
			this.HelpProvider.SetShowHelp(this, true);
			this.Text = "Registro";
			this.Shown += new System.EventHandler(this.RegistroForm_Shown);
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
			((System.ComponentModel.ISupportInitialize)(this.Datos_LineaRegistros)).EndInit();
			this.Main_Panel.Panel1.ResumeLayout(false);
			this.Main_Panel.Panel1.PerformLayout();
			this.Main_Panel.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Main_Panel)).EndInit();
			this.Main_Panel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Datos_Estados)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Datos_TipoRegistros)).EndInit();
			this.Productos_Panel.Panel1.ResumeLayout(false);
			this.Productos_Panel.Panel1.PerformLayout();
			this.Productos_Panel.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Productos_Panel)).EndInit();
			this.Productos_Panel.ResumeLayout(false);
			this.Productos_TS.ResumeLayout(false);
			this.Productos_TS.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.LineaRegistros_DGW)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.BindingSource Datos_LineaRegistros;
		private System.Windows.Forms.SplitContainer Main_Panel;
		protected System.Windows.Forms.TextBox Nombre_TB;
		protected System.Windows.Forms.BindingSource Datos_TipoRegistros;
		protected System.Windows.Forms.Label label9;
		protected System.Windows.Forms.ComboBox TipoRegistro_CB;
		protected System.Windows.Forms.RichTextBox Observaciones_RTB;
		protected System.Windows.Forms.DateTimePicker fechaDateTimePicker;
		protected System.Windows.Forms.TextBox ID_TB;
		private System.Windows.Forms.ComboBox Estado_CB;
		protected System.Windows.Forms.BindingSource Datos_Estados;
		protected System.Windows.Forms.SplitContainer Productos_Panel;
		protected System.Windows.Forms.ToolStrip Productos_TS;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		protected System.Windows.Forms.DataGridView LineaRegistros_DGW;
		protected System.Windows.Forms.ToolStripButton SelectAll_TI;
		protected System.Windows.Forms.ToolStripButton Null_TI;
		protected System.Windows.Forms.TextBox Usuario_TB;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionar;
		private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
		private System.Windows.Forms.DataGridViewTextBoxColumn TipoRegistroLabel;
		private System.Windows.Forms.DataGridViewTextBoxColumn CodigoEntidad;
		private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
		private System.Windows.Forms.DataGridViewTextBoxColumn EstadoEntidadLabel;
		private System.Windows.Forms.DataGridViewTextBoxColumn IDExportacion;
		private System.Windows.Forms.DataGridViewButtonColumn Estado;
		private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
		private System.Windows.Forms.ToolStripButton Lock_TI;
		

    }
}
