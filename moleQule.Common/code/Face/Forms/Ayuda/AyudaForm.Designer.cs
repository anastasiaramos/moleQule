namespace moleQule.Face.Common
{
	partial class AyudaForm
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
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AyudaForm));
            this.Datos_Periodos = new System.Windows.Forms.BindingSource(this.components);
            this.Main_Panel = new System.Windows.Forms.SplitContainer();
            this.CuentaContable_TB = new System.Windows.Forms.MaskedTextBox();
            this.NoContabilizar_BT = new System.Windows.Forms.Button();
            this.Estado_BT = new System.Windows.Forms.Button();
            this.Estado_TB = new System.Windows.Forms.TextBox();
            this.ID_TB = new System.Windows.Forms.TextBox();
            this.Observaciones_RTB = new System.Windows.Forms.RichTextBox();
            this.Nombre_TB = new System.Windows.Forms.TextBox();
            this.Periodos_SC = new System.Windows.Forms.SplitContainer();
            this.Periodos_TS = new System.Windows.Forms.ToolStrip();
            this.SelectAll_TI = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.AddPeriodo_TI = new System.Windows.Forms.ToolStripButton();
            this.DeletePeriodo_TI = new System.Windows.Forms.ToolStripButton();
            this.Periodos_DGW = new System.Windows.Forms.DataGridView();
            this.Seleccionar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.FechaIni = new CalendarColumn();
            this.FechaFin = new CalendarColumn();
            this.TipoDescuentoLabel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Porcentaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Observaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            nombreLabel = new System.Windows.Forms.Label();
            numeroClienteLabel = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Periodos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_Panel)).BeginInit();
            this.Main_Panel.Panel1.SuspendLayout();
            this.Main_Panel.Panel2.SuspendLayout();
            this.Main_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Periodos_SC)).BeginInit();
            this.Periodos_SC.Panel1.SuspendLayout();
            this.Periodos_SC.Panel2.SuspendLayout();
            this.Periodos_SC.SuspendLayout();
            this.Periodos_TS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Periodos_DGW)).BeginInit();
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
            this.PanelesV.Size = new System.Drawing.Size(686, 672);
            this.PanelesV.SplitterDistance = 617;
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
            this.Paneles2.Size = new System.Drawing.Size(684, 52);
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
            this.Datos.DataSource = typeof(moleQule.Library.Common.Ayuda);
            // 
            // Progress_Panel
            // 
            this.Progress_Panel.Location = new System.Drawing.Point(164, 96);
            // 
            // ProgressBK_Panel
            // 
            this.ProgressBK_Panel.Size = new System.Drawing.Size(686, 672);
            // 
            // ProgressInfo_PB
            // 
            this.ProgressInfo_PB.Location = new System.Drawing.Point(306, 384);
            // 
            // Progress_PB
            // 
            this.Progress_PB.Location = new System.Drawing.Point(306, 299);
            // 
            // nombreLabel
            // 
            nombreLabel.AutoSize = true;
            nombreLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            nombreLabel.Location = new System.Drawing.Point(25, 50);
            nombreLabel.Name = "nombreLabel";
            nombreLabel.Size = new System.Drawing.Size(54, 13);
            nombreLabel.TabIndex = 11;
            nombreLabel.Text = "Nombre*:";
            // 
            // numeroClienteLabel
            // 
            numeroClienteLabel.AutoSize = true;
            numeroClienteLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            numeroClienteLabel.Location = new System.Drawing.Point(25, 26);
            numeroClienteLabel.Name = "numeroClienteLabel";
            numeroClienteLabel.Size = new System.Drawing.Size(22, 13);
            numeroClienteLabel.TabIndex = 13;
            numeroClienteLabel.Text = "ID:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label3.Location = new System.Drawing.Point(25, 95);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(82, 13);
            label3.TabIndex = 42;
            label3.Text = "Observaciones:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label10.Location = new System.Drawing.Point(468, 30);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(44, 13);
            label10.TabIndex = 170;
            label10.Text = "Estado:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label8.Location = new System.Drawing.Point(27, 198);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(92, 13);
            label8.TabIndex = 172;
            label8.Text = "Cuenta Contable:";
            // 
            // Datos_Periodos
            // 
            this.Datos_Periodos.DataSource = typeof(moleQule.Library.Common.AyudaPeriodos);
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
            this.Main_Panel.Panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Main_Panel.Panel1.Controls.Add(this.CuentaContable_TB);
            this.Main_Panel.Panel1.Controls.Add(this.NoContabilizar_BT);
            this.Main_Panel.Panel1.Controls.Add(label8);
            this.Main_Panel.Panel1.Controls.Add(this.Estado_BT);
            this.Main_Panel.Panel1.Controls.Add(label10);
            this.Main_Panel.Panel1.Controls.Add(this.Estado_TB);
            this.Main_Panel.Panel1.Controls.Add(this.ID_TB);
            this.Main_Panel.Panel1.Controls.Add(label3);
            this.Main_Panel.Panel1.Controls.Add(this.Observaciones_RTB);
            this.Main_Panel.Panel1.Controls.Add(nombreLabel);
            this.Main_Panel.Panel1.Controls.Add(this.Nombre_TB);
            this.Main_Panel.Panel1.Controls.Add(numeroClienteLabel);
            // 
            // Main_Panel.Panel2
            // 
            this.Main_Panel.Panel2.Controls.Add(this.Periodos_SC);
            this.Main_Panel.Size = new System.Drawing.Size(684, 615);
            this.Main_Panel.SplitterDistance = 250;
            this.Main_Panel.TabIndex = 6;
            // 
            // CuentaContable_TB
            // 
            this.CuentaContable_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "CuentaContable", true));
            this.CuentaContable_TB.Location = new System.Drawing.Point(125, 194);
            this.CuentaContable_TB.Name = "CuentaContable_TB";
            this.CuentaContable_TB.Size = new System.Drawing.Size(126, 21);
            this.CuentaContable_TB.TabIndex = 174;
            this.CuentaContable_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // NoContabilizar_BT
            // 
            this.NoContabilizar_BT.Image = global::moleQule.Face.Common.Properties.Resources.close_16;
            this.NoContabilizar_BT.Location = new System.Drawing.Point(257, 193);
            this.NoContabilizar_BT.Name = "NoContabilizar_BT";
            this.NoContabilizar_BT.Size = new System.Drawing.Size(29, 22);
            this.NoContabilizar_BT.TabIndex = 173;
            this.NoContabilizar_BT.UseVisualStyleBackColor = true;
            // 
            // Estado_BT
            // 
            this.Estado_BT.Image = global::moleQule.Face.Common.Properties.Resources.Seleccionar_16;
            this.Estado_BT.Location = new System.Drawing.Point(631, 25);
            this.Estado_BT.Name = "Estado_BT";
            this.Estado_BT.Size = new System.Drawing.Size(29, 22);
            this.Estado_BT.TabIndex = 171;
            this.Estado_BT.UseVisualStyleBackColor = true;
            // 
            // Estado_TB
            // 
            this.Estado_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "EstadoLabel", true));
            this.Estado_TB.Location = new System.Drawing.Point(518, 26);
            this.Estado_TB.Name = "Estado_TB";
            this.Estado_TB.ReadOnly = true;
            this.Estado_TB.Size = new System.Drawing.Size(107, 21);
            this.Estado_TB.TabIndex = 169;
            this.Estado_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ID_TB
            // 
            this.ID_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Codigo", true));
            this.ID_TB.Enabled = false;
            this.ID_TB.Location = new System.Drawing.Point(53, 22);
            this.ID_TB.Name = "ID_TB";
            this.ID_TB.ReadOnly = true;
            this.ID_TB.Size = new System.Drawing.Size(79, 21);
            this.ID_TB.TabIndex = 44;
            this.ID_TB.TabStop = false;
            this.ID_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Observaciones_RTB
            // 
            this.Observaciones_RTB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Observaciones", true));
            this.Observaciones_RTB.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Observaciones_RTB.Location = new System.Drawing.Point(28, 111);
            this.Observaciones_RTB.Name = "Observaciones_RTB";
            this.Observaciones_RTB.Size = new System.Drawing.Size(632, 67);
            this.Observaciones_RTB.TabIndex = 41;
            this.Observaciones_RTB.Text = "";
            // 
            // Nombre_TB
            // 
            this.Nombre_TB.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.Datos, "Nombre", true));
            this.Nombre_TB.Location = new System.Drawing.Point(28, 68);
            this.Nombre_TB.Name = "Nombre_TB";
            this.Nombre_TB.Size = new System.Drawing.Size(632, 21);
            this.Nombre_TB.TabIndex = 12;
            // 
            // Periodos_SC
            // 
            this.Periodos_SC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Periodos_SC.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.Periodos_SC.Location = new System.Drawing.Point(0, 0);
            this.Periodos_SC.Name = "Periodos_SC";
            this.Periodos_SC.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Periodos_SC.Panel1
            // 
            this.Periodos_SC.Panel1.Controls.Add(this.Periodos_TS);
            this.Periodos_SC.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // Periodos_SC.Panel2
            // 
            this.Periodos_SC.Panel2.Controls.Add(this.Periodos_DGW);
            this.Periodos_SC.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Periodos_SC.Size = new System.Drawing.Size(684, 361);
            this.Periodos_SC.SplitterDistance = 38;
            this.Periodos_SC.SplitterWidth = 1;
            this.Periodos_SC.TabIndex = 7;
            // 
            // Periodos_TS
            // 
            this.Periodos_TS.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.Periodos_TS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectAll_TI,
            this.toolStripLabel1,
            this.AddPeriodo_TI,
            this.DeletePeriodo_TI});
            this.Periodos_TS.Location = new System.Drawing.Point(0, 0);
            this.Periodos_TS.Name = "Periodos_TS";
            this.HelpProvider.SetShowHelp(this.Periodos_TS, true);
            this.Periodos_TS.Size = new System.Drawing.Size(684, 39);
            this.Periodos_TS.TabIndex = 4;
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
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(0, 36);
            // 
            // AddPeriodo_TI
            // 
            this.AddPeriodo_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddPeriodo_TI.Image = global::moleQule.Face.Common.Properties.Resources.add;
            this.AddPeriodo_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddPeriodo_TI.Name = "AddPeriodo_TI";
            this.AddPeriodo_TI.Size = new System.Drawing.Size(36, 36);
            this.AddPeriodo_TI.Text = "Nuevo";
            this.AddPeriodo_TI.Click += new System.EventHandler(this.AddPeriodo_TI_Click);
            // 
            // DeletePeriodo_TI
            // 
            this.DeletePeriodo_TI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DeletePeriodo_TI.Image = global::moleQule.Face.Common.Properties.Resources.delete;
            this.DeletePeriodo_TI.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeletePeriodo_TI.Name = "DeletePeriodo_TI";
            this.DeletePeriodo_TI.Size = new System.Drawing.Size(36, 36);
            this.DeletePeriodo_TI.Text = "Borrar";
            this.DeletePeriodo_TI.Click += new System.EventHandler(this.DeletePeriodo_TI_Click);
            // 
            // Periodos_DGW
            // 
            this.Periodos_DGW.AllowUserToAddRows = false;
            this.Periodos_DGW.AllowUserToDeleteRows = false;
            this.Periodos_DGW.AllowUserToOrderColumns = true;
            this.Periodos_DGW.AutoGenerateColumns = false;
            this.Periodos_DGW.ColumnHeadersHeight = 36;
            this.Periodos_DGW.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Seleccionar,
            this.FechaIni,
            this.FechaFin,
            this.TipoDescuentoLabel,
            this.Porcentaje,
            this.Cantidad,
            this.Estado,
            this.Observaciones});
            this.Periodos_DGW.DataSource = this.Datos_Periodos;
            this.Periodos_DGW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Periodos_DGW.Location = new System.Drawing.Point(0, 0);
            this.Periodos_DGW.MultiSelect = false;
            this.Periodos_DGW.Name = "Periodos_DGW";
            this.Periodos_DGW.RowHeadersWidth = 25;
            this.Periodos_DGW.Size = new System.Drawing.Size(684, 322);
            this.Periodos_DGW.TabIndex = 7;
            this.Periodos_DGW.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Periodos_DGW_CellClick);
            // 
            // Seleccionar
            // 
            this.Seleccionar.FalseValue = "False";
            this.Seleccionar.HeaderText = "Seleccionar";
            this.Seleccionar.Name = "Seleccionar";
            this.Seleccionar.TrueValue = "True";
            this.Seleccionar.Visible = false;
            this.Seleccionar.Width = 20;
            // 
            // FechaIni
            // 
            this.FechaIni.DataPropertyName = "FechaIni";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Format = "d";
            this.FechaIni.DefaultCellStyle = dataGridViewCellStyle1;
            this.FechaIni.HeaderText = "Fecha Inicial";
            this.FechaIni.Name = "FechaIni";
            this.FechaIni.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FechaIni.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.FechaIni.Width = 90;
            // 
            // FechaFin
            // 
            this.FechaFin.DataPropertyName = "FechaFin";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Format = "d";
            this.FechaFin.DefaultCellStyle = dataGridViewCellStyle2;
            this.FechaFin.HeaderText = "Fecha Final";
            this.FechaFin.Name = "FechaFin";
            this.FechaFin.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.FechaFin.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.FechaFin.Width = 90;
            // 
            // TipoDescuentoLabel
            // 
            this.TipoDescuentoLabel.DataPropertyName = "TipoDescuentoLabel";
            this.TipoDescuentoLabel.HeaderText = "Tipo Ayuda";
            this.TipoDescuentoLabel.Name = "TipoDescuentoLabel";
            this.TipoDescuentoLabel.ReadOnly = true;
            this.TipoDescuentoLabel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TipoDescuentoLabel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.TipoDescuentoLabel.Width = 85;
            // 
            // Porcentaje
            // 
            this.Porcentaje.DataPropertyName = "Porcentaje";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.Porcentaje.DefaultCellStyle = dataGridViewCellStyle3;
            this.Porcentaje.HeaderText = "%";
            this.Porcentaje.Name = "Porcentaje";
            this.Porcentaje.Width = 60;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "Cantidad";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.Cantidad.DefaultCellStyle = dataGridViewCellStyle4;
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.Width = 60;
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "EstadoLabel";
            this.Estado.HeaderText = "Estado";
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Estado.Width = 75;
            // 
            // Observaciones
            // 
            this.Observaciones.DataPropertyName = "Observaciones";
            this.Observaciones.HeaderText = "Observaciones";
            this.Observaciones.MinimumWidth = 100;
            this.Observaciones.Name = "Observaciones";
            // 
            // AyudaForm
            // 
            this.ClientSize = new System.Drawing.Size(686, 672);
            this.HelpProvider.SetHelpNavigator(this, System.Windows.Forms.HelpNavigator.TopicId);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AyudaForm";
            this.HelpProvider.SetShowHelp(this, true);
            this.Text = "Ayuda";
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
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Periodos)).EndInit();
            this.Main_Panel.Panel1.ResumeLayout(false);
            this.Main_Panel.Panel1.PerformLayout();
            this.Main_Panel.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Main_Panel)).EndInit();
            this.Main_Panel.ResumeLayout(false);
            this.Periodos_SC.Panel1.ResumeLayout(false);
            this.Periodos_SC.Panel1.PerformLayout();
            this.Periodos_SC.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Periodos_SC)).EndInit();
            this.Periodos_SC.ResumeLayout(false);
            this.Periodos_TS.ResumeLayout(false);
            this.Periodos_TS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Periodos_DGW)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.BindingSource Datos_Periodos;
		private System.Windows.Forms.SplitContainer Main_Panel;
		protected System.Windows.Forms.TextBox Nombre_TB;
		protected System.Windows.Forms.RichTextBox Observaciones_RTB;
		protected System.Windows.Forms.TextBox ID_TB;
		protected System.Windows.Forms.SplitContainer Periodos_SC;
		protected System.Windows.Forms.ToolStrip Periodos_TS;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		protected System.Windows.Forms.DataGridView Periodos_DGW;
		protected System.Windows.Forms.ToolStripButton SelectAll_TI;
		protected System.Windows.Forms.ToolStripButton AddPeriodo_TI;
		protected System.Windows.Forms.ToolStripButton DeletePeriodo_TI;
		protected System.Windows.Forms.Button Estado_BT;
		protected System.Windows.Forms.TextBox Estado_TB;
		private System.Windows.Forms.DataGridViewCheckBoxColumn Seleccionar;
		private CalendarColumn FechaIni;
		private CalendarColumn FechaFin;
		private System.Windows.Forms.DataGridViewButtonColumn TipoDescuentoLabel;
		private System.Windows.Forms.DataGridViewTextBoxColumn Porcentaje;
		private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
		private System.Windows.Forms.DataGridViewButtonColumn Estado;
		private System.Windows.Forms.DataGridViewTextBoxColumn Observaciones;
		protected System.Windows.Forms.MaskedTextBox CuentaContable_TB;
		protected System.Windows.Forms.Button NoContabilizar_BT;
		

    }
}
