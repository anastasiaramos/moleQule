using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Transactions;

using Npgsql;

namespace DBCreator
{
    public partial class MainForm : Form
	{
		#region Enums

		protected enum EOperation { Installation = 0, Update = 1 }

		#endregion

		#region Attributes & Properties

		NpgsqlConnection _conn;
        NpgsqlTransaction _trans;

		public string Info
		{
			get 
			{ 
				return Info_TB.Text; 
			}
			
			set 
			{
				Info_TB.AppendText(value);
				Refresh();
			}
		}

		private string TemplateConnectionString
		{
			get
			{
				return "Server=" + Host_CB.Text +
						";Port=" + Port_TB.Text +
						";User Id=" + User_TB.Text +
						";Password=" + Password_TB.Text +
						";Database=" + DBTemplate_TB.Text +
						";Encoding=UNICODE;";
			}
		}

		private string InstallConnectionString
		{
			get
			{
				if (DBScript_CkB.Checked)
				{
					return "Server=" + Host_CB.Text +
							";Port=" + Port_TB.Text +
							";User Id=" + User_TB.Text +
							";Password=" + Password_TB.Text +
							";Database=" + GetDBNameFromQuery(Path_TB.Text + "\\00_DB.sql") +
							";Encoding=UNICODE;";
				}
				else
				{
					return "Server=" + Host_CB.Text +
							";Port=" + Port_TB.Text +
							";User Id=" + User_TB.Text +
							";Password=" + Password_TB.Text +
							";Database=" + DBInstall_CB.Text +
							";Encoding=UNICODE;";
				}
			}
		}

		private string ModuleConnectionString
		{
			get
			{
				return "Server=" + Host_CB.Text +
						";Port=" + Port_TB.Text +
						";User Id=" + User_TB.Text +
						";Password=" + Password_TB.Text +
						";Database=" + DBModule_CB.Text +
						";Encoding=UNICODE;";
			}
		}

		private string NewSchemaConnnectionString
		{
			get
			{
				return "Server=" + Host_CB.Text +
						";Port=" + Port_TB.Text +
						";User Id=" + User_TB.Text +
						";Password=" + Password_TB.Text +
						";Database=" + DBSchema_CB.Text +
						";Encoding=UNICODE;";
			}
		}

		private string UpdateConnectionString
		{
			get
			{
				return "Server=" + Host_CB.Text +
						";Port=" + Port_TB.Text +
						";User Id=" + User_TB.Text +
						";Password=" + Password_TB.Text +
						";Database=" + DBUpdate_CB.Text +
						";Encoding=UNICODE;";
			}
		}

		#endregion

		#region Factory Methods

		public MainForm()
		{
			InitializeComponent();
			InfoForm info = new InfoForm();

			BasePath_TB.Text = Properties.Settings.Default.ROOT_PATH;
            Path_TB.Text = Properties.Settings.Default.ROOT_PATH;

			InitModules();
		}

		private void InitModules()
		{ 
			Modulos_CLB.SetItemChecked(0, true);
			Modulos_CLB.SetItemChecked(1, true);
			Modulos_CLB.SetItemChecked(2, true);
		}
		
		#endregion

		#region Business Methods

		private bool ValidateValues()
		{
			if (DBInstall_CB.Text == string.Empty)
			{
				MessageBox.Show("No se ha especificado el nombre de la base de datos");
				return false;
			}

			return true;
		}

		private bool ExecuteSQLFromFile(string filename)
		{
			return ExecuteSQLFromFile(filename, true);
		}
		private bool ExecuteSQLFromFile(string filename, bool transaction)
		{
			return ExecuteSQLFromFile(filename, string.Empty, transaction);
		}
		private bool ExecuteSQLFromFile(string filename, string schema)
		{
			return ExecuteSQLFromFile(filename, schema, true);
		}
		private bool ExecuteSQLFromFile(string filename, string schema, bool transaction)
		{
			if (!File.Exists(filename))
			{
				Info = "Error." + Environment.NewLine +
						"No se ha encontrado el fichero " + filename + ".  ";
				return false;
			}

			string[] _lines = File.ReadAllLines(filename);
			string lines = string.Empty;

			if (schema != string.Empty) lines = "SET search_path TO '" + schema + "';";

			for (int i = 0; i < _lines.Length; i++)
			{
				if (!_lines[i].Trim().StartsWith("--"))
					lines += _lines[i];
			}

			try
			{
				if (transaction) _trans = _conn.BeginTransaction();
				NpgsqlCommand command = new NpgsqlCommand(lines, _conn);
				command.ExecuteNonQuery();
				if (transaction) _trans.Commit();
			}
			catch (Exception ex)
			{
				Info = Environment.NewLine + Environment.NewLine +
						ex.Message + Environment.NewLine + Environment.NewLine +
						"File: " + filename + Environment.NewLine + Environment.NewLine +
						"Query: " + lines;
				if (transaction) _trans.Rollback();
				return false;
			}

			return true;
		}

		private bool ExecuteSQL(string query, bool transaction)
		{
			try
			{
				if (transaction) _trans = _conn.BeginTransaction();
				NpgsqlCommand command = new NpgsqlCommand(query, _conn);
				command.ExecuteNonQuery();
				if (transaction) _trans.Commit();
			}
			catch (Exception ex)
			{
				Info = Environment.NewLine +
						ex.Message + Environment.NewLine + Environment.NewLine +
						"Query: " + query;
				if (transaction) _trans.Rollback();
				return false;
			}

			return true;

		}
		private bool ExecuteSQL(string query)
		{
			return ExecuteSQL(query, true);
		}

		private bool CreateUser()
		{
			string query = 
			"	CREATE GROUP \"MOLEQULE_ADMINISTRATOR\";" +
			"	CREATE USER " + DBUser_TB.Text +
			"		PASSWORD '" + DBPassword_TB.Text + "'" +
			"	CREATEDB CREATEUSER; " +
			"	CREATE GROUP \"" + DBInstall_CB.Text.ToUpper() + "_ADMINISTRATOR\";" +
			"	ALTER GROUP \"" + DBInstall_CB.Text.ToUpper() + "_ADMINISTRATOR\"" +
			"		ADD USER " + DBUser_TB.Text + ";" +
			"	CREATE USER \"" + DBInstall_CB.Text + "_Admin\"" +
			"		ENCRYPTED PASSWORD 'md556cbe7ab63d0e1c056f7a153e5811b7b'" +
			"		SUPERUSER" +
			"		IN ROLE \"MOLEQULE_ADMINISTRATOR\";";

			return ExecuteSQL(query, false);
		}

		private bool CreateDB()
		{
			string query = "CREATE DATABASE \"" + DBInstall_CB.Text + "\"" +
							"WITH OWNER = \"" + DBUser_TB.Text + "\" " +
							"ENCODING = 'UTF8' " +
							"TABLESPACE = pg_default;";

			return ExecuteSQL(query, false);
		}

		private bool CreateSchema(string schema)
		{
			if (!ExecuteSQL("DROP SCHEMA IF EXISTS \"" + schema + "\" CASCADE")) return false;
			if (!ExecuteSQL("CREATE SCHEMA \"" + schema + "\" AUTHORIZATION " + DBUser_TB.Text + ";")) return false;
			if (!ExecuteSQL("GRANT ALL ON SCHEMA \"" + schema + "\" TO \"MOLEQULE_ADMINISTRATOR\";")) return false;

			return true;
		}

		private bool InsertCompany(string schema) 
		{ 
			int nschema = Convert.ToInt32(schema);
			if (!ExecuteSQL("INSERT INTO \"COMMON\".\"CMCompany\" (\"OID\", \"SERIAL\", \"CODIGO\", \"NOMBRE\", \"TIPO_ID\") VALUES (" + nschema + "," + nschema + ", '" + nschema.ToString("00") + "', 'Cambie este nombre', 0)")) return false;
			if (!ExecuteSQL("INSERT INTO \"COMMON\".\"SchemaUser\" (\"OID_USER\", \"OID_SCHEMA\") (SELECT U.\"OID\", E.\"OID\" FROM \"COMMON\".\"User\" AS U, \"COMMON\".\"CMCompany\" AS E WHERE U.\"NAME\" IN ('moladmin', 'molservice', 'Admin') AND E.\"OID\" = " + nschema + ")")) return false;

			return true;
		}

		private bool DeleteDB()
		{
			string query = "DROP DATABASE \"" + DBInstall_CB.Text.ToUpper() + "\";";

			return ExecuteSQL(query, false);
		}

		private bool DeleteUser()
		{
			string query = string.Empty;

			query = "DROP USER " + DBUser_TB.Text + ";" +
					"DROP GROUP " + DBInstall_CB.Text.ToUpper() + "_ADMINISTRADOR;";

			return ExecuteSQL(query, false);
		}

		private string GetDBNameFromQuery(string path)
		{
			StreamReader update_file = File.OpenText(path);
			string db_name = string.Empty;
			string line = string.Empty;

			while (!update_file.EndOfStream)
			{
				line = update_file.ReadLine();

				if (line.ToUpper().Contains("CREATE DATABASE"))
				{
					int ini_pos = line.IndexOf("\"");
					db_name = line.Substring(ini_pos, line.LastIndexOf("\"") - ini_pos); 
					update_file.Close();
					return db_name;
				}
			}

			update_file.Close();
			return db_name;
		}

		private string GetCommonQuery(string path)
		{
			StreamReader update_file = File.OpenText(path);
			string query = string.Empty;
			string line = string.Empty;

			while (!update_file.EndOfStream)
			{
				line = update_file.ReadLine();

				if (line.ToUpper() == "SET SEARCH_PATH = \"0001\";")
				{
					update_file.Close();
					return query;
				}
				else
					query += line + Environment.NewLine;
			}

			update_file.Close();
			return query;
		}

		private string GetSchemaQuery(string path)
		{
			StreamReader update_file = File.OpenText(path);
			string query = string.Empty;
			string line = string.Empty;

			//Nos situamos en la primera linea del script del schema 0001
			while (!update_file.EndOfStream)
			{
				line = update_file.ReadLine();

				if (line.ToUpper() == "SET SEARCH_PATH = \"0001\";")
					break;
			}
			
			while (!update_file.EndOfStream)
			{
				line = update_file.ReadLine();
				query += line + Environment.NewLine;
			}

			update_file.Close();

			return query;
		}

		private string GetModuleDBVersion(string module)
		{
			string query = @"
				SELECT V.*
				FROM ""COMMON"".""Variable"" AS V
				WHERE V.""NAME"" = '" + GetVariableName(module) + "';";
			try
			{
				_conn = new NpgsqlConnection(UpdateConnectionString);
				_conn.Open();

				NpgsqlCommand command = new NpgsqlCommand(query, _conn);
				NpgsqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
					return Convert.ToString(reader["VALUE"]);
				else
					return "0.0.0.0";
			}
			catch (Exception ex)
			{
				Info = Environment.NewLine +
						ex.Message + Environment.NewLine + Environment.NewLine +
						"Query: " + query;
				return "0.0.0.0";
			}
			finally
			{
				_conn.Close();
			}
		}

		private List<string> GetModules()
		{
			List<string> modules = new List<string>();

			foreach (object item in Modulos_CLB.CheckedItems)
			{
				modules.Add(item as string);
			}

			return modules;
		}

		private string[] GetModulePath(string module, EOperation operation)
		{
			string module_dir = string.Empty;
			string finaldir = operation == EOperation.Installation ? "Install" : "Update";

			switch (module)
			{
                case "moleQule": return new string[] { Properties.Settings.Default.ROOT_PATH, "moleQule\\moleQule.Library\\SQL\\Updates" };
				case "Hipatia": module_dir = module; break;
				case "Common": module_dir = "moleQule\\moleQule." + module; break;
				default: module_dir = "moleQule." + module; break;
			}

			return new string[] {	Properties.Settings.Default.ROOT_PATH,
											module_dir, 
											"code", 
											"Library",
											"SQL",
											finaldir};
		}

		private string GetVariableName(string module)
		{
			switch (module)
			{
				default: return module.ToUpper() + "_DB_VERSION";
			}
		}

		private void SetModulePath(EOperation operation)
		{
			string path = Path.Combine(GetModulePath(Module_CB.Text, operation));

			if (Directory.Exists(path))
			{
				Script_FiB.InitialDirectory = path;
				Path_TB.Text = path;
			}
		}

		private int CheckVersion(string module)
		{ 
			Version dbversion = new Version(GetModuleDBVersion(module));
			Version moduleversion = GetModuleVersion(module);

			if (moduleversion.CompareTo(new Version("0.0.0.0")) == 0)
				if (dbversion.CompareTo(new Version("0.0.0.0")) == 0)
					return 2;
				else
					return 0;
			else
				return dbversion.CompareTo(moduleversion);
		}

		private Version GetModuleVersion(string module)
		{
			Version version;
			Version maxversion = new Version("0.0.0.0");

			string path = Path.Combine(GetModulePath(module, EOperation.Update));

			if (!Directory.Exists(path)) return maxversion;

			foreach (string file in Directory.GetFiles(path))
			{
				version = new Version(Path.GetFileNameWithoutExtension(file).Substring(("Update ").Length));
				if (version.CompareTo(maxversion) > 0)
					maxversion = version;
			}

			return maxversion;
		}

		private void SetModuleVersion()
		{
			ModuleVersion_TB.Text = GetModuleDBVersion(Module_CB.Text);

			switch (CheckVersion(Module_CB.Text))
			{
				case -1: ModuleVersion_TB.BackColor = Color.Red; break;
				case 0: ModuleVersion_TB.BackColor = Color.LightGreen; break;
				case 1: ModuleVersion_TB.BackColor = Color.OrangeRed; break;
				case 2: ModuleVersion_TB.BackColor = Color.LightGray; break;
			}
		}

		#endregion

		#region Actions

		private void CheckVersionsAction()
		{
			string message = "MODULE VERSIONS SUMMARY FOR " + DBUpdate_CB.Text + Environment.NewLine + Environment.NewLine;

			foreach (string module in Module_CB.Items)
			{
				message += "\t" + module + " is ";

				switch (CheckVersion(module))
				{
					case -1: message += "DOWNGRADED. NEEDS VERSION " + GetModuleVersion(module).ToString(); break;
					case 0: message += "UPDATED"; break;
					case 1: message += "UPGRADED. INSTALLED VERSION " + GetModuleDBVersion(module); break;
					case 2: message += "NOT INSTALLED"; break;
				}

				message += System.Environment.NewLine;
			}

			Info_TB.Text = message;
		}

		private void InstallDBAction()
		{
			Info_TB.Text = string.Empty;

			int n_schemas = 10;

			try
			{
				n_schemas = Convert.ToInt32(NSchemas_TB.Text);
			}
			catch
			{
				n_schemas = 10;
			}

			if (!ValidateValues()) return;

			Aceptar_Button.Enabled = false;

			List<string> modules = GetModules();

			_conn = null;

			Info = "Conectando con el servidor de base de datos...  ";

			string path = Path_TB.Text + "\\";

			try
			{
				_conn = new NpgsqlConnection(TemplateConnectionString);
				_conn.Open();

				Info = "Finalizado";

				// Creamos el usuario desde un script o con los datos de entrada
				if (CrearUsuario_CkB.Checked)
				{
					Info = Environment.NewLine + "Creando el usuario " + DBInstall_CB.Text + "...  ";

					if (UserScript_CkB.Checked)
					{
						if (!ExecuteSQLFromFile(path + "00_Users.sql", false)) return;
					}
					else
					{
						CreateUser();
					}

					Info = "Finalizado";
				}

				if (CrearDB_CkB.Checked)
				{
					if (DBScript_CkB.Checked)
					{
						Info = Environment.NewLine + "Borrando la base de datos existente...  ";
						if (!ExecuteSQLFromFile(path + "00_Drop_All.sql", false)) return;
						Info = "Finalizado";

						Info = Environment.NewLine + "Creando la base de datos " + DBInstall_CB.Text + "...  ";
						if (!ExecuteSQLFromFile(path + "00_DB.sql", false)) return;
					}
					else
					{
						Info = Environment.NewLine + "Borrando la base de datos existente...  ";
						DeleteDB();
						Info = "Finalizado";

						Info = Environment.NewLine + "Creando la base de datos " + DBInstall_CB.Text + "...  ";
						CreateDB();
					}

					Info = "Finalizado";
				}				

				_conn.Close();

				Info = Environment.NewLine + Environment.NewLine + "Conectando con la base de datos " + DBInstall_CB.Text + "...  ";

				_conn = new NpgsqlConnection(InstallConnectionString);
				_conn.Open();

				string schema = "COMMON";

				Info = "Finalizado" + Environment.NewLine + "Creando esquemas...  ";

				Info = Environment.NewLine + "    Esquema " + schema + "...  ";

				if (!CreateSchema(schema)) return;
				if (!ExecuteSQL("SET search_path TO '" + schema + "';")) return;

				if (!ExecuteSQLFromFile(path + "moleQule\\01_Common.sql")) return;
				if (!ExecuteSQLFromFile(path + "Common\\01_Common.sql")) return;
				if (!ExecuteSQLFromFile(path + "01_Common.sql")) return;

				foreach (string module in modules)
				{
					Info = Environment.NewLine + "        Módulo " + module + "...  ";

					if (!ExecuteSQLFromFile(path + module + "\\01_Common.sql")) return;

					Info = "Finalizado";
				}

				for (int i = 1; i <= n_schemas; i++)
				{
					schema = i.ToString("0000");

					// SCHEMA
					Info = Environment.NewLine + "    Esquema " + schema + "...  ";

					if (!CreateSchema(schema)) return;

					if (!ExecuteSQL("SET search_path TO '" + schema + "';")) return;
					if (!ExecuteSQLFromFile(path + "molequle\\02_Schema.sql")) return;
					if (!ExecuteSQLFromFile(path + "Common\\02_Schema.sql")) return;
					if (!ExecuteSQLFromFile(path + "02_Schema.sql")) return;

					foreach (string module in modules)
					{
						Info = Environment.NewLine + "        Módulo " + module + "...  ";

						if (!ExecuteSQLFromFile(path + module + "\\02_Schema.sql")) return;

						Info = "Finalizado";
					}
				}

				Info = Environment.NewLine + "Insertando datos...  ";

				schema = "COMMON";

				// COMMON DATA
				Info = Environment.NewLine + "    Esquema " + schema + "...  ";

				if (!ExecuteSQL("SET search_path TO '" + schema + "';")) return;
				if (!ExecuteSQLFromFile(path + "moleQule\\03_Common_Data.sql")) return;
				if (!ExecuteSQLFromFile(path + "Common\\03_Common_Data.sql")) return;
				if (!ExecuteSQLFromFile(path + "03_Common_Data.sql")) return;

				Info = "Finalizado";

				foreach (string module in modules)
				{
					Info = Environment.NewLine + "        Módulo " + module + "...  ";
					if (!ExecuteSQLFromFile(path + module + "\\03_Common_Data.sql", schema)) return;
					Info = "Finalizado";
				}

				for (int i = 1; i <= n_schemas; i++)
				{
					schema = i.ToString("0000");

					// SCHEMA DATA
					Info = Environment.NewLine + "    Esquema " + schema + "...  ";
									
					if (!ExecuteSQL("SET search_path TO '" + schema + "';")) return;
					if (!ExecuteSQLFromFile(path + "moleQule\\04_Schema_Data.sql")) return;
					if (!ExecuteSQLFromFile(path + "Common\\04_Schema_Data.sql")) return;
					if (!ExecuteSQLFromFile(path + "04_Schema_Data.sql")) return;

					foreach (string module in modules)
					{
						Info = Environment.NewLine + "        Módulo " + module + "...  ";
						if (!ExecuteSQLFromFile(path + module + "\\04_Schema_Data.sql", schema)) return;
						Info = "Finalizado";
					}
				}

				Info = Environment.NewLine + Environment.NewLine +
						"Generación de base de datos " + DBInstall_CB.Text + " finalizada.";
			}
			catch (Exception ex)
			{
				Info = Environment.NewLine + ex.Message;

				Info = Environment.NewLine + Environment.NewLine +
						"Ha ocurrido un error durante la creación de la base de datos. " +
						"Es necesario la instalación de la misma para el correcto funcionamiento " +
						"de la aplicación.";
			}
			finally
			{
				_conn.Close();
				Aceptar_Button.Enabled = true;
			}
		}

		private void InstallModuleAction()
		{ 
			Info_TB.Text = string.Empty;

			int schema_ini = 1;
			int schema_fin = 2;

			try
			{
				schema_ini = Convert.ToInt32(ModuleSchemaIni_TB.Text);
				schema_fin = Convert.ToInt32(ModuleSchemaFin_TB.Text);
			}
			catch
			{
				schema_ini = 1;
				schema_fin = 10;
			}

			if (!ValidateValues()) return;

			_conn = null;

			Info = "Conectando con el servidor de base de datos...  ";

			string path = Path_TB.Text;

			try
			{
				_conn = new NpgsqlConnection(ModuleConnectionString);
				_conn.Open();

				string module = Module_CB.Text;

				string schema = "COMMON";

				Info = Environment.NewLine + "        Módulo " + module + "...  ";

				if (!ExecuteSQLFromFile(Path.Combine(path, "01_Common.sql"))) return;
				if (!ExecuteSQLFromFile(Path.Combine(path, "03_Common_Data.sql"), schema)) return;

				for (int i = schema_ini; i <= schema_fin; i++)
				{
					schema = i.ToString("0000");

					// SCHEMA DATA
					Info = Environment.NewLine + "    Esquema " + schema + "...  ";

					if (!ExecuteSQL("SET search_path TO '" + schema + "';")) return;

					if (!ExecuteSQLFromFile(Path.Combine(path, "02_Schema.sql"))) return;
					if (!ExecuteSQLFromFile(Path.Combine(path, "04_Schema_Data.sql"), schema)) return;

					Info = "Finalizado";
				}

				Info = Environment.NewLine + Environment.NewLine + "Instalación de módulos " + DBUpdate_CB.Text + " finalizada.";

				_conn.Close();

				Aceptar_Button.Enabled = false;
			}
			catch (Exception ex)
			{
				Info = Environment.NewLine + Environment.NewLine + "Ha ocurrido un error durante la instalación de módulos. " +
						Environment.NewLine + Environment.NewLine + ex.Message;

				_conn.Close();
			}
			finally
			{
				Aceptar_Button.Enabled = true;
			}
		}
		
		private void NewSchemaDBAction()
		{
			Info_TB.Text = string.Empty;

			int schema_ini = 1;
			int schema_fin = 2;

			try
			{
				schema_ini = Convert.ToInt32(SchemaSchemaIni_TB.Text);
				schema_fin = Convert.ToInt32(SchemaSchemaFin_TB.Text);
			}
			catch
			{
				schema_ini = 1;
				schema_fin = 10;
			}

			if (DeleteSchema_CkB.Checked)
			{
				if (MessageBox.Show("¿Confirma la eliminación de los esquemas " + schema_ini.ToString() + " al " + schema_fin.ToString() + " de " + Host_CB.Text + "::" + DBSchema_CB.Text + "?",
									"Aviso",
									MessageBoxButtons.YesNo,
									MessageBoxIcon.Question) == DialogResult.No)
					return;
			}


			if (!ValidateValues()) return;

			Aceptar_Button.Enabled = false;

			List<string> modules = new List<string>();

			foreach (object item in Modulos_CLB.CheckedItems)
			{
				modules.Add(item as string);
			}

			_conn = null;

			Info = "Conectando con el servidor de base de datos...  ";

			string path = Path_TB.Text + "\\";

			try
			{
				Info = Environment.NewLine + Environment.NewLine + "Conectando con la base de datos " + DBSchema_CB.Text + "...  ";

				_conn = new NpgsqlConnection(NewSchemaConnnectionString);
				_conn.Open();

				Info = "Finalizado";

				string schema = string.Empty;

				for (int i = schema_ini; i <= schema_fin; i++)
				{
					schema = i.ToString("0000");

					Info = Environment.NewLine + "    Esquema " + schema + "...  ";

					if (DeleteSchema_CkB.Checked)
					{
						Info = Environment.NewLine + "        Borrando esquema...  ";
						if (!ExecuteSQL("DROP SCHEMA IF EXISTS \"" + schema + "\" CASCADE;")) return;
						if (!ExecuteSQL("DELETE FROM \"COMMON\".\"CMCompany\" WHERE \"OID\" = " + Convert.ToInt32(schema))) return;
						if (!ExecuteSQL("DELETE FROM \"COMMON\".\"SchemaUser\" WHERE \"OID_SCHEMA\" = " + Convert.ToInt32(schema))) return;
						Info = "Finalizado";
					}

					Info = Environment.NewLine + "        Creando esquema...  ";
					if (!CreateSchema(schema)) return;
					if (!ExecuteSQL("SET search_path TO '" + schema + "';")) return;
					Info = "Finalizado";

					// SCHEMA

					// moleQule, módulo Common y aplicación
					if (!ExecuteSQLFromFile(path + "molequle\\02_Schema.sql")) return;
					if (!ExecuteSQLFromFile(path + "Common\\02_Schema.sql")) return;
					if (!ExecuteSQLFromFile(path + "02_Schema.sql")) return;

					// módulos
					foreach (string module in modules)
					{
						Info = Environment.NewLine + "        Módulo " + module + "...  ";

						if (!ExecuteSQLFromFile(path + module + "\\02_Schema.sql")) return;

						Info = "Finalizado";
					}
				}

				Info = Environment.NewLine + "Insertando datos...  ";

				for (int i = schema_ini; i <= schema_fin; i++)
				{
					schema = i.ToString("0000");

					Info = Environment.NewLine + "    Esquema " + schema + "...  ";

					// SCHEMA DATA

					if (!ExecuteSQL("SET search_path TO '" + schema + "';")) return;

					InsertCompany(schema);

					// moleQule, módulo Common y aplicación
					if (!ExecuteSQLFromFile(path + "moleQule\\04_Schema_Data.sql")) return;
					if (!ExecuteSQLFromFile(path + "Common\\04_Schema_Data.sql")) return;
					if (!ExecuteSQLFromFile(path + "04_Schema_Data.sql")) return;

					// módulos
					foreach (string module in modules)
					{
						Info = Environment.NewLine + "        Módulo " + module + "...  ";
						if (!ExecuteSQLFromFile(path + module + "\\04_Schema_Data.sql", schema)) return;
						Info = "Finalizado";
					}
				}

				Info = Environment.NewLine + Environment.NewLine +
						"Generación de schemas de base de datos " + DBInstall_CB.Text + " finalizada.";
			}
			catch (Exception ex)
			{
				Info = Environment.NewLine + ex.Message;

				Info = Environment.NewLine + Environment.NewLine +
						"Ha ocurrido un error durante la creación de schemas de la base de datos.";
			}
			finally
			{
				_conn.Close();
				Aceptar_Button.Enabled = true;
			}
		}

		private void UpdateDBAction()
		{
			Info_TB.Text = string.Empty;

			int schema_ini = 1;
			int schema_fin = 2;

			try
			{
				schema_ini = Convert.ToInt32(UpdateSchemaIni_TB.Text);
				schema_fin = Convert.ToInt32(UpdateSchemaFin_TB.Text);
			}
			catch
			{
				schema_ini = 1;
				schema_fin = 10;
			}

			if (!ValidateValues()) return;

			_conn = null;

			Info = "Conectando con el servidor de base de datos...  ";

			string path = Path_TB.Text;

			try
			{
				_conn = new NpgsqlConnection(UpdateConnectionString);
				_conn.Open();

				//COMMON SCHEMA
				string schema_sql = GetCommonQuery(path);
				string schema = "COMMON";

				Info = Environment.NewLine + "    Esquema " + schema + "...  ";

				ExecuteSQL(schema_sql);

				//DETAIL SCHEMAS
				schema_sql = GetSchemaQuery(path);

				for (int i = schema_ini; i <= schema_fin; i++)
				{
					schema = i.ToString("0000");

					Info = Environment.NewLine + "    Esquema " + schema + "...  ";

					if (!ExecuteSQL("SET SEARCH_PATH = \"" + schema + "\";")) return;

					// SCHEMA
					ExecuteSQL(schema_sql);
				}

				Info = Environment.NewLine + Environment.NewLine + "Actualización de base de datos " + DBUpdate_CB.Text + " finalizada.";

				_conn.Close();

				Aceptar_Button.Enabled = false;
			}
			catch (Exception ex)
			{
				Info = Environment.NewLine + Environment.NewLine + "Ha ocurrido un error durante la actualización de la base de datos. " +
						Environment.NewLine + Environment.NewLine + ex.Message;

				_conn.Close();
			}
			finally
			{
				Aceptar_Button.Enabled = true;
			}

		}
		
		#endregion

		#region Buttons

		private void Aceptar_Button_Click(object sender, EventArgs e)
		{
			if (TipoScript_TC.SelectedTab == Install_TP)
			{
				InstallDBAction();
			}
			else if (TipoScript_TC.SelectedTab == Module_TP)
			{
				InstallModuleAction();
			}
			else if (TipoScript_TC.SelectedTab == Schema_TP)
			{
				NewSchemaDBAction();
			}
			else if (TipoScript_TC.SelectedTab == Update_TP)
			{
				UpdateDBAction();
			}
		}

		private void CheckVersions_BT_Click(object sender, EventArgs e)
		{
			CheckVersionsAction();
		}

		private void Ver_Button_Click(object sender, EventArgs e)
		{
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.StartInfo.FileName = Path_TB.Text;
			proc.Start();
		}

		private void BasePath_BT_Click(object sender, EventArgs e)
		{
			if (Script_FdB.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				BasePath_TB.Text = Script_FdB.SelectedPath;
				Properties.Settings.Default.ROOT_PATH = BasePath_TB.Text;
				Properties.Settings.Default.Save();

				SetModulePath(EOperation.Update);
			}
		}

		private void MostrarPassword_CB_CheckedChanged(object sender, EventArgs e)
		{
			Password_TB.UseSystemPasswordChar = !MostrarPassword_CB.Checked;
		}

		private void MostrarDBPassword_CB_CheckedChanged(object sender, EventArgs e)
		{
			DBPassword_TB.UseSystemPasswordChar = !MostrarDBPassword_CB.Checked;
		}

        private void Modulos_CkB_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string value = Modulos_CLB.Items[e.Index] as string;

           /* switch (value)
            {
                case Hipatia:
                    if (e.CurrentValue == CheckState.Unchecked)
                        
            }*/
        }

		private void ScriptPath_BT_Click(object sender, EventArgs e)
		{
			if ((TipoScript_TC.SelectedTab == Install_TP) ||
				(TipoScript_TC.SelectedTab == Schema_TP))
			{
				string base_path = Directory.Exists(Path_TB.Text) 
					? Path_TB.Text 
					: Directory.GetParent(Path_TB.Text).ToString();

				if (Directory.Exists(base_path))
					Script_FdB.SelectedPath = base_path;

				if (Script_FdB.ShowDialog() == DialogResult.OK)
					Path_TB.Text = Script_FdB.SelectedPath;
			}
			else if (TipoScript_TC.SelectedTab == Update_TP)
			{
				string base_path = Path.Combine(GetModulePath(Module_CB.Text, EOperation.Update));

				if (Directory.Exists(Path.Combine(GetModulePath(Module_CB.Text, EOperation.Update))))
					Script_FiB.InitialDirectory = Path_TB.Text;

				if (Script_FiB.ShowDialog() == DialogResult.OK)
					Path_TB.Text = Script_FiB.FileName;
			}
			else if (TipoScript_TC.SelectedTab == Module_TP)
			{
				string base_path = Path.Combine(GetModulePath(Module_CB.Text, EOperation.Installation));

				if (Directory.Exists(Path.Combine(GetModulePath(Module_CB.Text, EOperation.Installation))))
					Script_FdB.SelectedPath = Path_TB.Text;

				if (Script_FdB.ShowDialog() == DialogResult.OK)
					Path_TB.Text = Script_FdB.SelectedPath;
			}
		}

		#endregion

		#region Events

		private void Module_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetModulePath((TipoScript_TC.SelectedTab == Update_TP) ? EOperation.Update: EOperation.Installation);
			SetModuleVersion();
		}

		private void TipoScript_TC_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (TipoScript_TC.SelectedTab == Install_TP)
			{
				Modulos_CLB.Visible = true;
				Module_CB.Visible = false;
				ModuleVersion_TB.Visible = false;
				ModuleName_LB.Visible = false;
				ModuleVersion_LB.Visible = false;
			}
			else if (TipoScript_TC.SelectedTab == Schema_TP)
			{
				Modulos_CLB.Visible = true;
				Module_CB.Visible = false;
			}
			else if (TipoScript_TC.SelectedTab == Module_TP)
			{
				Modulos_CLB.Visible = false;
				Module_CB.Visible = true;
				ModuleVersion_TB.Visible = true;
				ModuleName_LB.Visible = true;
				ModuleVersion_LB.Visible = true;

				SetModulePath(EOperation.Installation);

				ModuleVersion_TB.Text = GetModuleDBVersion(Module_CB.Text);
			}
			else if (TipoScript_TC.SelectedTab == Update_TP)
			{
				Modulos_CLB.Visible = false;
				Module_CB.Visible = true;
				ModuleVersion_TB.Visible = true;
				ModuleName_LB.Visible = true;
				ModuleVersion_LB.Visible = true;

				SetModulePath(EOperation.Update);
				SetModuleVersion();
				CheckVersionsAction();
			}
		}

		private void CrearUsuario_CkB_CheckedChanged(object sender, EventArgs e)
		{
			UserScript_CkB.Enabled = CrearUsuario_CkB.Enabled;
			DBUser_TB.Enabled = CrearUsuario_CkB.Checked && !UserScript_CkB.Checked;
			DBPassword_TB.Enabled = CrearUsuario_CkB.Checked && !UserScript_CkB.Checked;
		}

		private void UserScript_CkB_CheckedChanged(object sender, EventArgs e)
		{
			DBUser_TB.Enabled = CrearUsuario_CkB.Checked && !UserScript_CkB.Checked;
			DBPassword_TB.Enabled = CrearUsuario_CkB.Checked && !UserScript_CkB.Checked;
		}

		private void CrearDB_CkB_CheckedChanged(object sender, EventArgs e)
		{
			DBScript_CkB.Enabled = CrearDB_CkB.Enabled;
			DBInstall_CB.Enabled = CrearDB_CkB.Enabled && !DBScript_CkB.Checked;
			DBTemplate_TB.Enabled = CrearDB_CkB.Enabled && !DBScript_CkB.Checked;
		}

		private void DBScript_CkB_CheckedChanged(object sender, EventArgs e)
		{
			DBInstall_CB.Enabled = CrearDB_CkB.Enabled && !DBScript_CkB.Checked;
			DBTemplate_TB.Enabled = CrearDB_CkB.Enabled && !DBScript_CkB.Checked;
		}

		private void DBModule_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetModuleVersion();
		}
		
		private void DBUpdate_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetModuleVersion();
			CheckVersionsAction();
		}

		private void Host_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetModuleVersion();
		}

		#endregion
	}
}