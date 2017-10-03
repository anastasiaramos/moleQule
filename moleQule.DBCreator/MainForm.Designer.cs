namespace DBCreator
{
	partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.User_TB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Password_TB = new System.Windows.Forms.TextBox();
            this.Aceptar_Button = new System.Windows.Forms.Button();
            this.Cancelar_Button = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Port_TB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Info_TB = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.DBUser_TB = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TipoScript_TC = new System.Windows.Forms.TabControl();
            this.Install_TP = new System.Windows.Forms.TabPage();
            this.DB_GB = new System.Windows.Forms.GroupBox();
            this.DBInstall_CB = new System.Windows.Forms.ComboBox();
            this.CrearDB_CkB = new System.Windows.Forms.CheckBox();
            this.DBScript_CkB = new System.Windows.Forms.CheckBox();
            this.DBTemplate_TB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NSchemas_TB = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.User_GB = new System.Windows.Forms.GroupBox();
            this.CrearUsuario_CkB = new System.Windows.Forms.CheckBox();
            this.UserScript_CkB = new System.Windows.Forms.CheckBox();
            this.MostrarDBPassword_CB = new System.Windows.Forms.CheckBox();
            this.DBPassword_TB = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Module_TP = new System.Windows.Forms.TabPage();
            this.DBModule_CB = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.ModuleSchemaFin_TB = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.ModuleSchemaIni_TB = new System.Windows.Forms.TextBox();
            this.Schema_TP = new System.Windows.Forms.TabPage();
            this.DBSchema_CB = new System.Windows.Forms.ComboBox();
            this.DeleteSchema_CkB = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.SchemaSchemaFin_TB = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.SchemaSchemaIni_TB = new System.Windows.Forms.TextBox();
            this.Update_TP = new System.Windows.Forms.TabPage();
            this.CheckVersions_BT = new System.Windows.Forms.Button();
            this.BasePath_BT = new System.Windows.Forms.Button();
            this.BasePath_TB = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.DBUpdate_CB = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.UpdateSchemaFin_TB = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.UpdateSchemaIni_TB = new System.Windows.Forms.TextBox();
            this.Modulos_GB = new System.Windows.Forms.GroupBox();
            this.ModuleVersion_LB = new System.Windows.Forms.Label();
            this.ModuleName_LB = new System.Windows.Forms.Label();
            this.ModuleVersion_TB = new System.Windows.Forms.TextBox();
            this.Module_CB = new System.Windows.Forms.ComboBox();
            this.Modulos_CLB = new System.Windows.Forms.CheckedListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Host_CB = new System.Windows.Forms.ComboBox();
            this.MostrarPassword_CB = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Path_TB = new System.Windows.Forms.TextBox();
            this.Script_FdB = new System.Windows.Forms.FolderBrowserDialog();
            this.Script_FiB = new System.Windows.Forms.OpenFileDialog();
            this.Datos_Modulos = new System.Windows.Forms.BindingSource(this.components);
            this.ScriptPath_BT = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.TipoScript_TC.SuspendLayout();
            this.Install_TP.SuspendLayout();
            this.DB_GB.SuspendLayout();
            this.User_GB.SuspendLayout();
            this.Module_TP.SuspendLayout();
            this.Schema_TP.SuspendLayout();
            this.Update_TP.SuspendLayout();
            this.Modulos_GB.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Modulos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Servidor:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Usuario \r\nAdministrador:";
            // 
            // User_TB
            // 
            this.User_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.User_TB.Location = new System.Drawing.Point(112, 79);
            this.User_TB.Name = "User_TB";
            this.User_TB.Size = new System.Drawing.Size(186, 21);
            this.User_TB.TabIndex = 3;
            this.User_TB.Text = "postgres";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(16, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Contraseña:";
            // 
            // Password_TB
            // 
            this.Password_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Password_TB.Location = new System.Drawing.Point(112, 106);
            this.Password_TB.Name = "Password_TB";
            this.Password_TB.Size = new System.Drawing.Size(186, 21);
            this.Password_TB.TabIndex = 4;
            this.Password_TB.Text = "TebaP2G_1998";
            this.Password_TB.UseSystemPasswordChar = true;
            // 
            // Aceptar_Button
            // 
            this.Aceptar_Button.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Aceptar_Button.Location = new System.Drawing.Point(609, 666);
            this.Aceptar_Button.Name = "Aceptar_Button";
            this.Aceptar_Button.Size = new System.Drawing.Size(100, 31);
            this.Aceptar_Button.TabIndex = 1;
            this.Aceptar_Button.Text = "Ejecutar";
            this.Aceptar_Button.UseVisualStyleBackColor = true;
            this.Aceptar_Button.Click += new System.EventHandler(this.Aceptar_Button_Click);
            // 
            // Cancelar_Button
            // 
            this.Cancelar_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancelar_Button.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cancelar_Button.Location = new System.Drawing.Point(503, 666);
            this.Cancelar_Button.Name = "Cancelar_Button";
            this.Cancelar_Button.Size = new System.Drawing.Size(100, 31);
            this.Cancelar_Button.TabIndex = 110;
            this.Cancelar_Button.Text = "Ver";
            this.Cancelar_Button.UseVisualStyleBackColor = true;
            this.Cancelar_Button.Click += new System.EventHandler(this.Ver_Button_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(283, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Introduzca los datos de conexión de PostgreSQL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(17, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Puerto:";
            // 
            // Port_TB
            // 
            this.Port_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Port_TB.Location = new System.Drawing.Point(112, 52);
            this.Port_TB.Name = "Port_TB";
            this.Port_TB.Size = new System.Drawing.Size(186, 21);
            this.Port_TB.TabIndex = 1;
            this.Port_TB.Text = "5432";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(317, 28);
            this.label7.TabIndex = 13;
            this.label7.Text = "Si ha realizado una instalación estándar solo debe proporcionar \r\nla contraseña d" +
    "el usuario administrador";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Info_TB
            // 
            this.Info_TB.AcceptsTab = true;
            this.Info_TB.BackColor = System.Drawing.Color.White;
            this.Info_TB.Location = new System.Drawing.Point(371, 17);
            this.Info_TB.Multiline = true;
            this.Info_TB.Name = "Info_TB";
            this.Info_TB.ReadOnly = true;
            this.Info_TB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Info_TB.Size = new System.Drawing.Size(573, 593);
            this.Info_TB.TabIndex = 14;
            this.Info_TB.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 14);
            this.label8.TabIndex = 17;
            this.label8.Text = "Base de datos:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(13, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 14);
            this.label10.TabIndex = 21;
            this.label10.Text = "Usuario:";
            // 
            // DBUser_TB
            // 
            this.DBUser_TB.Enabled = false;
            this.DBUser_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.DBUser_TB.Location = new System.Drawing.Point(86, 64);
            this.DBUser_TB.Name = "DBUser_TB";
            this.DBUser_TB.Size = new System.Drawing.Size(156, 21);
            this.DBUser_TB.TabIndex = 4;
            this.DBUser_TB.Text = "moladmin";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TipoScript_TC);
            this.groupBox1.Controls.Add(this.Modulos_GB);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 215);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(340, 486);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BD y Usuario";
            // 
            // TipoScript_TC
            // 
            this.TipoScript_TC.Controls.Add(this.Install_TP);
            this.TipoScript_TC.Controls.Add(this.Module_TP);
            this.TipoScript_TC.Controls.Add(this.Schema_TP);
            this.TipoScript_TC.Controls.Add(this.Update_TP);
            this.TipoScript_TC.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TipoScript_TC.ItemSize = new System.Drawing.Size(161, 30);
            this.TipoScript_TC.Location = new System.Drawing.Point(6, 18);
            this.TipoScript_TC.Multiline = true;
            this.TipoScript_TC.Name = "TipoScript_TC";
            this.TipoScript_TC.SelectedIndex = 0;
            this.TipoScript_TC.Size = new System.Drawing.Size(328, 388);
            this.TipoScript_TC.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TipoScript_TC.TabIndex = 34;
            this.TipoScript_TC.SelectedIndexChanged += new System.EventHandler(this.TipoScript_TC_SelectedIndexChanged);
            // 
            // Install_TP
            // 
            this.Install_TP.Controls.Add(this.DB_GB);
            this.Install_TP.Controls.Add(this.User_GB);
            this.Install_TP.Location = new System.Drawing.Point(4, 64);
            this.Install_TP.Name = "Install_TP";
            this.Install_TP.Padding = new System.Windows.Forms.Padding(3);
            this.Install_TP.Size = new System.Drawing.Size(320, 320);
            this.Install_TP.TabIndex = 0;
            this.Install_TP.Text = "Instalar Aplicación";
            this.Install_TP.UseVisualStyleBackColor = true;
            // 
            // DB_GB
            // 
            this.DB_GB.Controls.Add(this.DBInstall_CB);
            this.DB_GB.Controls.Add(this.CrearDB_CkB);
            this.DB_GB.Controls.Add(this.DBScript_CkB);
            this.DB_GB.Controls.Add(this.DBTemplate_TB);
            this.DB_GB.Controls.Add(this.label8);
            this.DB_GB.Controls.Add(this.label2);
            this.DB_GB.Controls.Add(this.NSchemas_TB);
            this.DB_GB.Controls.Add(this.label12);
            this.DB_GB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DB_GB.Location = new System.Drawing.Point(26, 8);
            this.DB_GB.Name = "DB_GB";
            this.DB_GB.Size = new System.Drawing.Size(271, 154);
            this.DB_GB.TabIndex = 114;
            this.DB_GB.TabStop = false;
            this.DB_GB.Text = "Base de Datos";
            // 
            // DBInstall_CB
            // 
            this.DBInstall_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.DBInstall_CB.FormattingEnabled = true;
            this.DBInstall_CB.Items.AddRange(new object[] {
            "CATTLE",
            "CATTLE_STAGING",
            "FACTO",
            "IRYS",
            "AUTOBUYER"});
            this.DBInstall_CB.Location = new System.Drawing.Point(98, 66);
            this.DBInstall_CB.Name = "DBInstall_CB";
            this.DBInstall_CB.Size = new System.Drawing.Size(158, 21);
            this.DBInstall_CB.TabIndex = 44;
            this.DBInstall_CB.Text = "CATTLE";
            // 
            // CrearDB_CkB
            // 
            this.CrearDB_CkB.AutoSize = true;
            this.CrearDB_CkB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CrearDB_CkB.Location = new System.Drawing.Point(13, 20);
            this.CrearDB_CkB.Name = "CrearDB_CkB";
            this.CrearDB_CkB.Size = new System.Drawing.Size(125, 18);
            this.CrearDB_CkB.TabIndex = 5;
            this.CrearDB_CkB.Text = "Crear base de datos";
            this.CrearDB_CkB.UseVisualStyleBackColor = true;
            this.CrearDB_CkB.CheckedChanged += new System.EventHandler(this.CrearDB_CkB_CheckedChanged);
            // 
            // DBScript_CkB
            // 
            this.DBScript_CkB.AutoSize = true;
            this.DBScript_CkB.Checked = true;
            this.DBScript_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DBScript_CkB.Enabled = false;
            this.DBScript_CkB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DBScript_CkB.Location = new System.Drawing.Point(13, 43);
            this.DBScript_CkB.Name = "DBScript_CkB";
            this.DBScript_CkB.Size = new System.Drawing.Size(79, 18);
            this.DBScript_CkB.TabIndex = 4;
            this.DBScript_CkB.Text = "Usar script";
            this.DBScript_CkB.UseVisualStyleBackColor = true;
            this.DBScript_CkB.CheckedChanged += new System.EventHandler(this.DBScript_CkB_CheckedChanged);
            // 
            // DBTemplate_TB
            // 
            this.DBTemplate_TB.Enabled = false;
            this.DBTemplate_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.DBTemplate_TB.Location = new System.Drawing.Point(98, 93);
            this.DBTemplate_TB.Name = "DBTemplate_TB";
            this.DBTemplate_TB.Size = new System.Drawing.Size(158, 21);
            this.DBTemplate_TB.TabIndex = 36;
            this.DBTemplate_TB.Text = "template1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 14);
            this.label2.TabIndex = 37;
            this.label2.Text = "Plantilla:";
            // 
            // NSchemas_TB
            // 
            this.NSchemas_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.NSchemas_TB.Location = new System.Drawing.Point(98, 120);
            this.NSchemas_TB.Name = "NSchemas_TB";
            this.NSchemas_TB.Size = new System.Drawing.Size(42, 21);
            this.NSchemas_TB.TabIndex = 34;
            this.NSchemas_TB.Text = "1";
            this.NSchemas_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(13, 123);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 14);
            this.label12.TabIndex = 35;
            this.label12.Text = "Nº Schemas:";
            // 
            // User_GB
            // 
            this.User_GB.Controls.Add(this.CrearUsuario_CkB);
            this.User_GB.Controls.Add(this.DBUser_TB);
            this.User_GB.Controls.Add(this.UserScript_CkB);
            this.User_GB.Controls.Add(this.label10);
            this.User_GB.Controls.Add(this.MostrarDBPassword_CB);
            this.User_GB.Controls.Add(this.DBPassword_TB);
            this.User_GB.Controls.Add(this.label9);
            this.User_GB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.User_GB.Location = new System.Drawing.Point(23, 168);
            this.User_GB.Name = "User_GB";
            this.User_GB.Size = new System.Drawing.Size(274, 145);
            this.User_GB.TabIndex = 113;
            this.User_GB.TabStop = false;
            this.User_GB.Text = "Usuario";
            // 
            // CrearUsuario_CkB
            // 
            this.CrearUsuario_CkB.AutoSize = true;
            this.CrearUsuario_CkB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CrearUsuario_CkB.Location = new System.Drawing.Point(15, 18);
            this.CrearUsuario_CkB.Name = "CrearUsuario_CkB";
            this.CrearUsuario_CkB.Size = new System.Drawing.Size(92, 18);
            this.CrearUsuario_CkB.TabIndex = 3;
            this.CrearUsuario_CkB.Text = "Crear usuario";
            this.CrearUsuario_CkB.UseVisualStyleBackColor = true;
            this.CrearUsuario_CkB.CheckedChanged += new System.EventHandler(this.CrearUsuario_CkB_CheckedChanged);
            // 
            // UserScript_CkB
            // 
            this.UserScript_CkB.AutoSize = true;
            this.UserScript_CkB.Checked = true;
            this.UserScript_CkB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UserScript_CkB.Enabled = false;
            this.UserScript_CkB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserScript_CkB.Location = new System.Drawing.Point(15, 41);
            this.UserScript_CkB.Name = "UserScript_CkB";
            this.UserScript_CkB.Size = new System.Drawing.Size(79, 18);
            this.UserScript_CkB.TabIndex = 38;
            this.UserScript_CkB.Text = "Usar script";
            this.UserScript_CkB.UseVisualStyleBackColor = true;
            this.UserScript_CkB.CheckedChanged += new System.EventHandler(this.UserScript_CkB_CheckedChanged);
            // 
            // MostrarDBPassword_CB
            // 
            this.MostrarDBPassword_CB.AutoSize = true;
            this.MostrarDBPassword_CB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MostrarDBPassword_CB.Location = new System.Drawing.Point(108, 118);
            this.MostrarDBPassword_CB.Name = "MostrarDBPassword_CB";
            this.MostrarDBPassword_CB.Size = new System.Drawing.Size(119, 18);
            this.MostrarDBPassword_CB.TabIndex = 31;
            this.MostrarDBPassword_CB.Text = "Mostrar caracteres";
            this.MostrarDBPassword_CB.UseVisualStyleBackColor = true;
            this.MostrarDBPassword_CB.CheckedChanged += new System.EventHandler(this.MostrarDBPassword_CB_CheckedChanged);
            // 
            // DBPassword_TB
            // 
            this.DBPassword_TB.Enabled = false;
            this.DBPassword_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.DBPassword_TB.Location = new System.Drawing.Point(86, 91);
            this.DBPassword_TB.Name = "DBPassword_TB";
            this.DBPassword_TB.Size = new System.Drawing.Size(156, 21);
            this.DBPassword_TB.TabIndex = 5;
            this.DBPassword_TB.Text = "TebaP2G_1998";
            this.DBPassword_TB.UseSystemPasswordChar = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(13, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 14);
            this.label9.TabIndex = 30;
            this.label9.Text = "Contraseña:";
            // 
            // Module_TP
            // 
            this.Module_TP.Controls.Add(this.DBModule_CB);
            this.Module_TP.Controls.Add(this.label19);
            this.Module_TP.Controls.Add(this.label20);
            this.Module_TP.Controls.Add(this.ModuleSchemaFin_TB);
            this.Module_TP.Controls.Add(this.label21);
            this.Module_TP.Controls.Add(this.ModuleSchemaIni_TB);
            this.Module_TP.Location = new System.Drawing.Point(4, 64);
            this.Module_TP.Name = "Module_TP";
            this.Module_TP.Size = new System.Drawing.Size(320, 320);
            this.Module_TP.TabIndex = 3;
            this.Module_TP.Text = "Instalar Módulo";
            this.Module_TP.UseVisualStyleBackColor = true;
            // 
            // DBModule_CB
            // 
            this.DBModule_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.DBModule_CB.FormattingEnabled = true;
            this.DBModule_CB.Items.AddRange(new object[] {
            "CATTLE",
            "CATTLE_STAGING",
            "FACTO",
            "IRYS",
            "AUTOBUYER"});
            this.DBModule_CB.Location = new System.Drawing.Point(131, 67);
            this.DBModule_CB.Name = "DBModule_CB";
            this.DBModule_CB.Size = new System.Drawing.Size(140, 21);
            this.DBModule_CB.TabIndex = 55;
            this.DBModule_CB.Text = "CATTLE";
            this.DBModule_CB.SelectedIndexChanged += new System.EventHandler(this.DBModule_CB_SelectedIndexChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(48, 70);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 14);
            this.label19.TabIndex = 54;
            this.label19.Text = "Base de datos:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(48, 124);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(74, 14);
            this.label20.TabIndex = 52;
            this.label20.Text = "Schema Final:";
            // 
            // ModuleSchemaFin_TB
            // 
            this.ModuleSchemaFin_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ModuleSchemaFin_TB.Location = new System.Drawing.Point(131, 121);
            this.ModuleSchemaFin_TB.Name = "ModuleSchemaFin_TB";
            this.ModuleSchemaFin_TB.Size = new System.Drawing.Size(42, 21);
            this.ModuleSchemaFin_TB.TabIndex = 51;
            this.ModuleSchemaFin_TB.Text = "10";
            this.ModuleSchemaFin_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(48, 97);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(78, 14);
            this.label21.TabIndex = 50;
            this.label21.Text = "Schema Inicial:";
            // 
            // ModuleSchemaIni_TB
            // 
            this.ModuleSchemaIni_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ModuleSchemaIni_TB.Location = new System.Drawing.Point(131, 94);
            this.ModuleSchemaIni_TB.Name = "ModuleSchemaIni_TB";
            this.ModuleSchemaIni_TB.Size = new System.Drawing.Size(42, 21);
            this.ModuleSchemaIni_TB.TabIndex = 49;
            this.ModuleSchemaIni_TB.Text = "1";
            this.ModuleSchemaIni_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Schema_TP
            // 
            this.Schema_TP.Controls.Add(this.DBSchema_CB);
            this.Schema_TP.Controls.Add(this.DeleteSchema_CkB);
            this.Schema_TP.Controls.Add(this.label16);
            this.Schema_TP.Controls.Add(this.label17);
            this.Schema_TP.Controls.Add(this.SchemaSchemaFin_TB);
            this.Schema_TP.Controls.Add(this.label18);
            this.Schema_TP.Controls.Add(this.SchemaSchemaIni_TB);
            this.Schema_TP.Location = new System.Drawing.Point(4, 64);
            this.Schema_TP.Name = "Schema_TP";
            this.Schema_TP.Padding = new System.Windows.Forms.Padding(3);
            this.Schema_TP.Size = new System.Drawing.Size(320, 320);
            this.Schema_TP.TabIndex = 2;
            this.Schema_TP.Text = "Crear Schema";
            this.Schema_TP.UseVisualStyleBackColor = true;
            // 
            // DBSchema_CB
            // 
            this.DBSchema_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.DBSchema_CB.FormattingEnabled = true;
            this.DBSchema_CB.Items.AddRange(new object[] {
            "CATTLE",
            "CATTLE_STAGING",
            "FACTO",
            "IRYS",
            "AUTOBUYER"});
            this.DBSchema_CB.Location = new System.Drawing.Point(131, 67);
            this.DBSchema_CB.Name = "DBSchema_CB";
            this.DBSchema_CB.Size = new System.Drawing.Size(140, 21);
            this.DBSchema_CB.TabIndex = 50;
            this.DBSchema_CB.Text = "CATTLE";
            // 
            // DeleteSchema_CkB
            // 
            this.DeleteSchema_CkB.AutoSize = true;
            this.DeleteSchema_CkB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteSchema_CkB.Location = new System.Drawing.Point(51, 162);
            this.DeleteSchema_CkB.Name = "DeleteSchema_CkB";
            this.DeleteSchema_CkB.Size = new System.Drawing.Size(159, 18);
            this.DeleteSchema_CkB.TabIndex = 49;
            this.DeleteSchema_CkB.Text = "Borrar esquemas si existen";
            this.DeleteSchema_CkB.UseVisualStyleBackColor = true;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(48, 70);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(80, 14);
            this.label16.TabIndex = 48;
            this.label16.Text = "Base de datos:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(48, 124);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(74, 14);
            this.label17.TabIndex = 46;
            this.label17.Text = "Schema Final:";
            // 
            // SchemaSchemaFin_TB
            // 
            this.SchemaSchemaFin_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.SchemaSchemaFin_TB.Location = new System.Drawing.Point(131, 121);
            this.SchemaSchemaFin_TB.Name = "SchemaSchemaFin_TB";
            this.SchemaSchemaFin_TB.Size = new System.Drawing.Size(42, 21);
            this.SchemaSchemaFin_TB.TabIndex = 45;
            this.SchemaSchemaFin_TB.Text = "10";
            this.SchemaSchemaFin_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(48, 97);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(78, 14);
            this.label18.TabIndex = 44;
            this.label18.Text = "Schema Inicial:";
            // 
            // SchemaSchemaIni_TB
            // 
            this.SchemaSchemaIni_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.SchemaSchemaIni_TB.Location = new System.Drawing.Point(131, 94);
            this.SchemaSchemaIni_TB.Name = "SchemaSchemaIni_TB";
            this.SchemaSchemaIni_TB.Size = new System.Drawing.Size(42, 21);
            this.SchemaSchemaIni_TB.TabIndex = 43;
            this.SchemaSchemaIni_TB.Text = "1";
            this.SchemaSchemaIni_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Update_TP
            // 
            this.Update_TP.Controls.Add(this.CheckVersions_BT);
            this.Update_TP.Controls.Add(this.BasePath_BT);
            this.Update_TP.Controls.Add(this.BasePath_TB);
            this.Update_TP.Controls.Add(this.label24);
            this.Update_TP.Controls.Add(this.DBUpdate_CB);
            this.Update_TP.Controls.Add(this.label15);
            this.Update_TP.Controls.Add(this.label14);
            this.Update_TP.Controls.Add(this.UpdateSchemaFin_TB);
            this.Update_TP.Controls.Add(this.label13);
            this.Update_TP.Controls.Add(this.UpdateSchemaIni_TB);
            this.Update_TP.Location = new System.Drawing.Point(4, 64);
            this.Update_TP.Name = "Update_TP";
            this.Update_TP.Padding = new System.Windows.Forms.Padding(3);
            this.Update_TP.Size = new System.Drawing.Size(320, 320);
            this.Update_TP.TabIndex = 1;
            this.Update_TP.Text = "Actualización";
            this.Update_TP.UseVisualStyleBackColor = true;
            // 
            // CheckVersions_BT
            // 
            this.CheckVersions_BT.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CheckVersions_BT.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CheckVersions_BT.Location = new System.Drawing.Point(102, 165);
            this.CheckVersions_BT.Name = "CheckVersions_BT";
            this.CheckVersions_BT.Size = new System.Drawing.Size(100, 47);
            this.CheckVersions_BT.TabIndex = 112;
            this.CheckVersions_BT.Text = "Check Versions";
            this.CheckVersions_BT.UseVisualStyleBackColor = true;
            this.CheckVersions_BT.Click += new System.EventHandler(this.CheckVersions_BT_Click);
            // 
            // BasePath_BT
            // 
            this.BasePath_BT.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.BasePath_BT.Location = new System.Drawing.Point(278, 272);
            this.BasePath_BT.Name = "BasePath_BT";
            this.BasePath_BT.Size = new System.Drawing.Size(33, 23);
            this.BasePath_BT.TabIndex = 111;
            this.BasePath_BT.Text = "...";
            this.BasePath_BT.UseVisualStyleBackColor = true;
            this.BasePath_BT.Click += new System.EventHandler(this.BasePath_BT_Click);
            // 
            // BasePath_TB
            // 
            this.BasePath_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.BasePath_TB.Location = new System.Drawing.Point(13, 273);
            this.BasePath_TB.Name = "BasePath_TB";
            this.BasePath_TB.Size = new System.Drawing.Size(259, 21);
            this.BasePath_TB.TabIndex = 45;
            this.BasePath_TB.Text = "D:\\Proyectos\\";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(10, 257);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(59, 14);
            this.label24.TabIndex = 44;
            this.label24.Text = "Ruta base:";
            // 
            // DBUpdate_CB
            // 
            this.DBUpdate_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.DBUpdate_CB.FormattingEnabled = true;
            this.DBUpdate_CB.Items.AddRange(new object[] {
            "CATTLE",
            "CATTLE_STAGING",
            "FACTO",
            "IRYS",
            "AUTOBUYER"});
            this.DBUpdate_CB.Location = new System.Drawing.Point(131, 67);
            this.DBUpdate_CB.Name = "DBUpdate_CB";
            this.DBUpdate_CB.Size = new System.Drawing.Size(140, 21);
            this.DBUpdate_CB.TabIndex = 43;
            this.DBUpdate_CB.Text = "CATTLE";
            this.DBUpdate_CB.SelectedIndexChanged += new System.EventHandler(this.DBUpdate_CB_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(48, 70);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 14);
            this.label15.TabIndex = 42;
            this.label15.Text = "Base de datos:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(48, 124);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(74, 14);
            this.label14.TabIndex = 40;
            this.label14.Text = "Schema Final:";
            // 
            // UpdateSchemaFin_TB
            // 
            this.UpdateSchemaFin_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.UpdateSchemaFin_TB.Location = new System.Drawing.Point(131, 121);
            this.UpdateSchemaFin_TB.Name = "UpdateSchemaFin_TB";
            this.UpdateSchemaFin_TB.Size = new System.Drawing.Size(42, 21);
            this.UpdateSchemaFin_TB.TabIndex = 39;
            this.UpdateSchemaFin_TB.Text = "10";
            this.UpdateSchemaFin_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(48, 97);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 14);
            this.label13.TabIndex = 38;
            this.label13.Text = "Schema Inicial:";
            // 
            // UpdateSchemaIni_TB
            // 
            this.UpdateSchemaIni_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.UpdateSchemaIni_TB.Location = new System.Drawing.Point(131, 94);
            this.UpdateSchemaIni_TB.Name = "UpdateSchemaIni_TB";
            this.UpdateSchemaIni_TB.Size = new System.Drawing.Size(42, 21);
            this.UpdateSchemaIni_TB.TabIndex = 37;
            this.UpdateSchemaIni_TB.Text = "1";
            this.UpdateSchemaIni_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Modulos_GB
            // 
            this.Modulos_GB.Controls.Add(this.ModuleVersion_LB);
            this.Modulos_GB.Controls.Add(this.ModuleName_LB);
            this.Modulos_GB.Controls.Add(this.ModuleVersion_TB);
            this.Modulos_GB.Controls.Add(this.Module_CB);
            this.Modulos_GB.Controls.Add(this.Modulos_CLB);
            this.Modulos_GB.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Modulos_GB.Location = new System.Drawing.Point(6, 404);
            this.Modulos_GB.Name = "Modulos_GB";
            this.Modulos_GB.Size = new System.Drawing.Size(328, 76);
            this.Modulos_GB.TabIndex = 112;
            this.Modulos_GB.TabStop = false;
            this.Modulos_GB.Text = "Módulos";
            // 
            // ModuleVersion_LB
            // 
            this.ModuleVersion_LB.AutoSize = true;
            this.ModuleVersion_LB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModuleVersion_LB.Location = new System.Drawing.Point(225, 26);
            this.ModuleVersion_LB.Name = "ModuleVersion_LB";
            this.ModuleVersion_LB.Size = new System.Drawing.Size(44, 14);
            this.ModuleVersion_LB.TabIndex = 47;
            this.ModuleVersion_LB.Text = "Versión";
            this.ModuleVersion_LB.Visible = false;
            // 
            // ModuleName_LB
            // 
            this.ModuleName_LB.AutoSize = true;
            this.ModuleName_LB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModuleName_LB.Location = new System.Drawing.Point(42, 26);
            this.ModuleName_LB.Name = "ModuleName_LB";
            this.ModuleName_LB.Size = new System.Drawing.Size(44, 14);
            this.ModuleName_LB.TabIndex = 46;
            this.ModuleName_LB.Text = "Nombre";
            this.ModuleName_LB.Visible = false;
            // 
            // ModuleVersion_TB
            // 
            this.ModuleVersion_TB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ModuleVersion_TB.Location = new System.Drawing.Point(228, 42);
            this.ModuleVersion_TB.Name = "ModuleVersion_TB";
            this.ModuleVersion_TB.Size = new System.Drawing.Size(56, 21);
            this.ModuleVersion_TB.TabIndex = 45;
            this.ModuleVersion_TB.Text = "0.0.0.0";
            this.ModuleVersion_TB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ModuleVersion_TB.Visible = false;
            // 
            // Module_CB
            // 
            this.Module_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Module_CB.FormattingEnabled = true;
            this.Module_CB.Items.AddRange(new object[] {
            "moleQule",
            "Common",
            "Hipatia",
            "Instruction",
            "Invoice",
            "Partner",
            "Quality",
            "Renting",
            "Scale",
            "Store"});
            this.Module_CB.Location = new System.Drawing.Point(45, 42);
            this.Module_CB.Name = "Module_CB";
            this.Module_CB.Size = new System.Drawing.Size(177, 21);
            this.Module_CB.TabIndex = 44;
            this.Module_CB.Text = "Common";
            this.Module_CB.Visible = false;
            this.Module_CB.SelectedIndexChanged += new System.EventHandler(this.Module_CB_SelectedIndexChanged);
            // 
            // Modulos_CLB
            // 
            this.Modulos_CLB.ColumnWidth = 95;
            this.Modulos_CLB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Modulos_CLB.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Modulos_CLB.FormattingEnabled = true;
            this.Modulos_CLB.Items.AddRange(new object[] {
            "Hipatia",
            "Store",
            "Invoice",
            "Instruction",
            "Quality",
            "Partner",
            "Renting"});
            this.Modulos_CLB.Location = new System.Drawing.Point(3, 17);
            this.Modulos_CLB.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.Modulos_CLB.MultiColumn = true;
            this.Modulos_CLB.Name = "Modulos_CLB";
            this.Modulos_CLB.Size = new System.Drawing.Size(322, 56);
            this.Modulos_CLB.TabIndex = 0;
            this.Modulos_CLB.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.Modulos_CkB_ItemCheck);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Host_CB);
            this.groupBox2.Controls.Add(this.MostrarPassword_CB);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.User_TB);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Port_TB);
            this.groupBox2.Controls.Add(this.Password_TB);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(15, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(340, 157);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "PostgreSQL";
            // 
            // Host_CB
            // 
            this.Host_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Host_CB.FormattingEnabled = true;
            this.Host_CB.Items.AddRange(new object[] {
            "localhost",
            "tea.iqingenieros.com",
            "remote.igcan.com",
            "servidor.aerotraining.com",
            "backoffice.snaprent.net",
            "staging.interactiverent.com",
            "www.interactiverent.com"});
            this.Host_CB.Location = new System.Drawing.Point(112, 25);
            this.Host_CB.Name = "Host_CB";
            this.Host_CB.Size = new System.Drawing.Size(186, 21);
            this.Host_CB.TabIndex = 33;
            this.Host_CB.Text = "localhost";
            this.Host_CB.SelectedIndexChanged += new System.EventHandler(this.Host_CB_SelectedIndexChanged);
            // 
            // MostrarPassword_CB
            // 
            this.MostrarPassword_CB.AutoSize = true;
            this.MostrarPassword_CB.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.MostrarPassword_CB.Location = new System.Drawing.Point(142, 133);
            this.MostrarPassword_CB.Name = "MostrarPassword_CB";
            this.MostrarPassword_CB.Size = new System.Drawing.Size(117, 17);
            this.MostrarPassword_CB.TabIndex = 32;
            this.MostrarPassword_CB.Text = "Mostrar caracteres";
            this.MostrarPassword_CB.UseVisualStyleBackColor = true;
            this.MostrarPassword_CB.CheckedChanged += new System.EventHandler(this.MostrarPassword_CB_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(368, 622);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 14);
            this.label11.TabIndex = 34;
            this.label11.Text = "Ruta de los script:";
            // 
            // Path_TB
            // 
            this.Path_TB.Location = new System.Drawing.Point(371, 638);
            this.Path_TB.Name = "Path_TB";
            this.Path_TB.Size = new System.Drawing.Size(534, 21);
            this.Path_TB.TabIndex = 0;
            this.Path_TB.Text = "D:\\Proyectos\\VS Projects\\Facto iQ\\Codigo\\moleQule.Application\\Library\\SQL";
            // 
            // Script_FiB
            // 
            this.Script_FiB.FileName = "openFileDialog1";
            // 
            // Datos_Modulos
            // 
            this.Datos_Modulos.DataSource = typeof(moleQule.Library.ComboBoxSourceList);
            // 
            // ScriptPath_BT
            // 
            this.ScriptPath_BT.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.ScriptPath_BT.Location = new System.Drawing.Point(911, 636);
            this.ScriptPath_BT.Name = "ScriptPath_BT";
            this.ScriptPath_BT.Size = new System.Drawing.Size(33, 23);
            this.ScriptPath_BT.TabIndex = 112;
            this.ScriptPath_BT.Text = "...";
            this.ScriptPath_BT.UseVisualStyleBackColor = true;
            this.ScriptPath_BT.Click += new System.EventHandler(this.ScriptPath_BT_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.Aceptar_Button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancelar_Button;
            this.ClientSize = new System.Drawing.Size(956, 702);
            this.Controls.Add(this.ScriptPath_BT);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.Path_TB);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Info_TB);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Cancelar_Button);
            this.Controls.Add(this.Aceptar_Button);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "moleQule DB Manager";
            this.groupBox1.ResumeLayout(false);
            this.TipoScript_TC.ResumeLayout(false);
            this.Install_TP.ResumeLayout(false);
            this.DB_GB.ResumeLayout(false);
            this.DB_GB.PerformLayout();
            this.User_GB.ResumeLayout(false);
            this.User_GB.PerformLayout();
            this.Module_TP.ResumeLayout(false);
            this.Module_TP.PerformLayout();
            this.Schema_TP.ResumeLayout(false);
            this.Schema_TP.PerformLayout();
            this.Update_TP.ResumeLayout(false);
            this.Update_TP.PerformLayout();
            this.Modulos_GB.ResumeLayout(false);
            this.Modulos_GB.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Datos_Modulos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox User_TB;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox Password_TB;
		private System.Windows.Forms.Button Aceptar_Button;
		private System.Windows.Forms.Button Cancelar_Button;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox Port_TB;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.TextBox Info_TB;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox DBUser_TB;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox MostrarDBPassword_CB;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox DBPassword_TB;
		private System.Windows.Forms.CheckBox MostrarPassword_CB;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox Path_TB;
		private System.Windows.Forms.Button BasePath_BT;
		private System.Windows.Forms.FolderBrowserDialog Script_FdB;
        private System.Windows.Forms.GroupBox Modulos_GB;
        private System.Windows.Forms.CheckedListBox Modulos_CLB;
		private System.Windows.Forms.BindingSource Datos_Modulos;
		private System.Windows.Forms.TabControl TipoScript_TC;
		private System.Windows.Forms.TabPage Install_TP;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox NSchemas_TB;
		private System.Windows.Forms.CheckBox CrearUsuario_CkB;
		private System.Windows.Forms.CheckBox DBScript_CkB;
		private System.Windows.Forms.CheckBox CrearDB_CkB;
		private System.Windows.Forms.TabPage Update_TP;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox UpdateSchemaFin_TB;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox UpdateSchemaIni_TB;
		private System.Windows.Forms.OpenFileDialog Script_FiB;
		private System.Windows.Forms.TextBox DBTemplate_TB;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.CheckBox UserScript_CkB;
		private System.Windows.Forms.GroupBox DB_GB;
		private System.Windows.Forms.GroupBox User_GB;
		private System.Windows.Forms.TabPage Schema_TP;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox SchemaSchemaFin_TB;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox SchemaSchemaIni_TB;
		private System.Windows.Forms.CheckBox DeleteSchema_CkB;
		private System.Windows.Forms.ComboBox Host_CB;
		private System.Windows.Forms.TabPage Module_TP;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox ModuleSchemaFin_TB;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TextBox ModuleSchemaIni_TB;
		private System.Windows.Forms.ComboBox DBUpdate_CB;
		private System.Windows.Forms.ComboBox DBInstall_CB;
		private System.Windows.Forms.ComboBox DBModule_CB;
		private System.Windows.Forms.ComboBox DBSchema_CB;
		private System.Windows.Forms.ComboBox Module_CB;
		private System.Windows.Forms.TextBox ModuleVersion_TB;
		private System.Windows.Forms.Label ModuleVersion_LB;
		private System.Windows.Forms.Label ModuleName_LB;
		private System.Windows.Forms.TextBox BasePath_TB;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Button ScriptPath_BT;
		private System.Windows.Forms.Button CheckVersions_BT;
	}
}

