using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

using NHibernate;
using NHibernate.Impl;
using NHibernate.Cfg;
using NHibernate.Transform;
using NHibernate.Mapping;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace moleQule.Library.CslaEx
{
	[Serializable()]
	public class nHManager
	{
		#region Business Methods

		private Configuration _cfg = null;
		private Dictionary<string, ISessionFactory> _sFactories = new Dictionary<string,ISessionFactory>();
		private List<ISession> _sessions = new List<ISession>();

		private string _server = null;

		public Configuration Cfg { get { return _cfg; } set { _cfg = value; } }

        public bool UseDirectSQL
        {
            get { return (Cfg != null) ? /*Convert.ToBoolean(Cfg.Properties["use_direct_sql"])*/true : false; }
        }

		public string DefaultUser
		{
			get { return (Cfg != null) ? (Cfg.Properties["default_user"] != null ? Cfg.Properties["default_user"].ToString() : string.Empty) : string.Empty; }
			set
			{
				Cfg.Properties["default_user"] = value;
			}
		}

		public string Host { get { return (Cfg != null) ? GetConnectionParam("Server") : string.Empty; } }
		public string User { get { return (Cfg != null) ? GetConnectionParam("User Id") : string.Empty; } }
		public string Password { get { return (Cfg != null) ? GetConnectionParam("Password") : string.Empty; } }
		public string Database { get { return (Cfg != null) ? GetConnectionParam("Database") : string.Empty; } }

        public Dictionary<string, ISessionFactory> SFactories { get { return _sFactories; } set { _sFactories = value; } }

		public List<ISession> Sessions { get { return _sessions; } }

		public string Server { get { return _server; } set { _server = value; } }

		public string ConnectionString
		{
			get 
            {
                if (Cfg == null) throw new iQException("NHibernate Configuration not loaded");

                return Cfg.GetProperty("connection.connection_string"); 
            }
			set
			{
				Cfg.SetProperty("connection.connection_string", value);
				Cfg.SetProperty("hibernate.connection.connection_string", value);
			}
		}

		public string GetConnectionParam(string param)
		{
			try
			{
				string con = Cfg.GetProperty("connection.connection_string");
				String[] conParams = con.Split(new Char[] { ';' });

				for (int i = 0; i < conParams.Length; i++)
					if (conParams[i].Contains(param))
						return conParams[i].Substring(conParams[i].IndexOf("=") + 1);
			}
			catch { }

            throw new Exception(String.Format(Resources.Messages.NOT_FOUND_CONNECTION_PARAM, param));
        }

		public void SetUser(string user)
		{
			string con = ConnectionString;

			int pass_pos = con.IndexOf("User Id=");
			int pass_length = pass_pos + con.Substring(pass_pos).IndexOf(";");

			string pass = con.Substring(0, pass_pos);
			pass += "User Id=" + user;
			pass += con.Substring(pass_length);

			ConnectionString = pass;
		}

		public void SetServer(string server)
		{
			string con = ConnectionString;

			int pass_pos = con.IndexOf("Server=");
			int pass_length = pass_pos + con.Substring(pass_pos).IndexOf(";");

			string pass = con.Substring(0, pass_pos);
			pass += "Server=" + server;
			pass += con.Substring(pass_length);

			ConnectionString = pass;
		}

		public void SetDBName(string dbName)
		{
			string con = ConnectionString;

			int pass_pos = con.IndexOf("Database=");
			int pass_length = pass_pos + con.Substring(pass_pos).IndexOf(";");

			string pass = con.Substring(0, pass_pos);
			pass += "Database=" + dbName;
			pass += con.Substring(pass_length);

			ConnectionString = pass;
		}

		public void SetDBPassword(string db_pass)
		{
			string con = ConnectionString;

			int pass_pos = con.IndexOf("Password=");
			int pass_length = pass_pos + con.Substring(pass_pos).IndexOf(";");

			string pass = con.Substring(0, pass_pos);
			pass += "Password=" + db_pass;
			pass += con.Substring(pass_length);

			ConnectionString = pass;
		}

		public void ChangeUserSchema(string schema, string dbUser, bool forceChangeSessionFactory = false)
        {
			try
			{
				if (SFactories.ContainsKey(dbUser)
						&& ((NHibernate.Impl.SessionFactoryImpl)SFactories[dbUser]).Settings.DefaultSchemaName != "\"" + schema + "\"")
				{
					Cfg.SetProperty("default_schema", schema);

					foreach (PersistentClass item in Cfg.ClassMappings)
						if (item.Table.Schema != "\"COMMON\"")
						{
							item.Table.Schema = "\"" + schema + "\"";
							((NHibernate.Mapping.SimpleValue)item.Table.IdentifierValue).IdentifierGeneratorProperties["schema"] = item.Table.Schema;
						}

					CreateSessionFactory(dbUser, forceChangeSessionFactory);
					AppControllerBase.AppControler.ActivateEntities();
				}
			}
			catch 
			{
				throw new iQException("Error en ChangeUserSchema");
			}
		}

        /// <summary>
        /// Devuelve el nombre de la tabla asociada a un tipo
        /// </summary>
        /// <param name="type">Tipo del objeto</param>
        /// <returns>Lo devuelve de la forma "schema"."tabla"</returns>
        public string GetSQLTable(Type type, string activeSchema)
        {
            try
            {
                string schema = nHManager.Instance.Cfg.GetClassMapping(type).Table.Schema;
                schema = schema != null ? schema.Replace("`", "") : null;
                string pattern = "{0}.\"{1}\"";
                return String.Format(pattern, schema != null ? schema : activeSchema, nHManager.Instance.Cfg.GetClassMapping(type).Table.Name);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("No se ha podido mapear el objeto {0}. Revise el fichero de configuración de nHibernate", type.Name), ex.InnerException);
            }
        }

        /// <summary>
        /// Devuelve el nombre de la tabla asociada a un tipo
        /// </summary>
        /// <param name="type">Tipo del objeto</param>
        /// <returns>Lo devuelve de la forma "schema"."tabla"</returns>
        public string GetSQLTable(Type type)
        {
			try
			{
				return GetSQLTable(type, (AppContext.ActiveSchema != null) 
											? AppContext.ActiveSchema.Code
											: Cfg.GetProperty("default_schema"));
			}
			catch
			{
				return GetSQLTable(type, Cfg.GetProperty("default_schema"));
			}
        }

        /// <summary>
        /// Devuelve el nombre del campo de la tabla asociado a la propiedad
        /// </summary>
        /// <param name="type">Tipo del objeto del que se quiere obtener la propiedad</param>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetTableField(Type type, string property)
        {
            IEnumerable<Property> cols;

            cols = Cfg.GetClassMapping(type).PropertyIterator;
            
            foreach (Property prop in cols)
            {
                if (prop.Name == property)
                {
                    Column col = (Column)(((IList)(prop.ColumnIterator))[0]);
                    return col.Text; 
                }
            }

           throw new Exception(String.Format(Resources.Messages.COLUMN_NOT_FOUND, property, type.Name));
		}

        public Column GetTableColumn(Type type, string property)
        {
            IEnumerable<Property> cols;

            cols = Cfg.GetClassMapping(type).PropertyIterator;

            foreach (Property prop in cols)
            {
                if (prop.Name == property)
                {
                    return (Column)(((IList)(prop.ColumnIterator))[0]);
                }
            }

            throw new Exception(String.Format(Resources.Messages.COLUMN_NOT_FOUND, property, type.Name));
		}

        public ICollection<Column> GetTableColumns(Type type)
        {
            IEnumerable<Property> props;
            List<Column> cols = new List<Column>();

            props = Cfg.GetClassMapping(type).PropertyIterator;

            foreach (Property prop in props)
            {
               cols.Add((Column)(((IList)(prop.ColumnIterator))[0]));
            }

			return cols;
		}

		/// <summary>
		/// Devuelve el nombre del campo identificador en la tabla
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public string GetTableID(Type type)
		{
			PersistentClass pclass;

			//Obtencion de la información de mapeo
			pclass = Cfg.GetClassMapping(type);

            Column columna = (Column)((IList)(pclass.Identifier.ColumnIterator))[0];
            return (columna!=null) ? columna.Text : string.Empty;
        }

		public Column GetTableIDColumn(Type type)
		{
			PersistentClass pclass;

			//Obtencion de la información de mapeo
			pclass = Cfg.GetClassMapping(type);

            return (Column)((IList)(pclass.Identifier.ColumnIterator))[0];
           
        }
       
		#endregion
		
		#region By Code Interface

		/// <summary>
		/// Devuelve una sesión
		/// </summary>
		/// <param name="type">Tipo del objeto</param>
		/// <returns></returns>
		public ISession GetSession(int sessionCode)
		{
			if (sessionCode < Sessions.Count)
				return Sessions[sessionCode];
			else
				throw new iQCslaSessionException(typeof(int), sessionCode);
		}

		/// <summary>
		/// Devuelve un criterio asociado a la sesion perteneciente a type.
		/// </summary>
		/// <param name="type">Tipo del objeto</param>
		/// <param name="oid">Oid del objeto</param>
		/// <returns></returns>
		public CriteriaEx GetCriteria(Type type, int sessionCode)
		{
			ISession sess = GetSession(sessionCode);
			return (sess == null) ? null : new CriteriaEx(type, (SessionImpl)sess, sessionCode);
		}

		public ITransaction BeginTransaction(int sessionCode)
		{
			try
			{
				ISession sess = GetSession(sessionCode);
				return (sess != null) ? sess.BeginTransaction() : null;
			}
			catch
			{
				return null;
			}
		}

		public void CloseSession(int sessionCode)
		{
            if (Sessions.Count == 0) return;

            //if (Sessions.Count <= sessionCode)
            //{
            //    Exception ex = new Exception(Resources.Messages.CLOSE_SESSION_ERROR + System.Environment.NewLine +
            //                        Resources.Messages.INVALID_SESSION_NUMBER);
            //    iQExceptionHandler.SetCaller(ex, "nHManager::CloseSession");
            //    throw ex;
            //}

            try
            {
                ISession sess = Sessions[sessionCode];
            }
            catch
            {
                Exception ex = new Exception(Resources.Messages.CLOSE_SESSION_ERROR + System.Environment.NewLine +
                                    Resources.Messages.INVALID_SESSION_NUMBER);
                iQExceptionHandler.SetCaller(ex, "nHManager::CloseSession");
                throw ex;
            }

			try
			{
				ITransaction transaction = GetTransaction(sessionCode);

				if (transaction != null)
				{
					if ((!transaction.WasCommitted) && !(transaction.WasRolledBack))
					{
						transaction.Rollback();
					}
				}
			}
			catch { }

			Sessions[sessionCode].Close();

			//Si es la última la eliminamos
			//si no la ponemos a null para reaprovecharla luego
			if (sessionCode == Sessions.Count - 1)
			{
				Sessions.RemoveAt(sessionCode);
				for (int i = Sessions.Count - 1; i >= 0; i--)
				{
					if (Sessions[i] == null)
						Sessions.RemoveAt(i);
					else
						break;
				}
			}
			else
				Sessions[sessionCode] = null;
		}

		/// <summary>
		/// Devuelve la transaccion asociada a una session
		/// </summary>
		/// <param name="sess">Sesión</param>
		/// <returns></returns>
		public ITransaction GetTransaction(int codeSession)
		{
			try
			{
				ISession sess = GetSession(codeSession);
				return (sess != null) ? sess.Transaction : null;
			}
			catch
			{
				return null;
			}
		}

		#endregion

		#region Criteria, Session & Transaction

		/// <summary>
		/// Abre una sesión
		/// </summary>
		/// <returns>Código de session</returns>
		/// 
		public int OpenSession()
		{
			try
			{
				int pos = 0;
				ISession sess = null;

				//Usamos el SessionFactory del usuario o del usuario por defecto si no hay usuario logueado
				if (AppContext.User != null && AppContext.User.IsAuthenticated)
					sess = SFactories[Library.User.MapToDBUsername(AppContext.User.Name)].OpenSession();
				else
					sess = SFactories.First().Value.OpenSession();

				// Buscamos la primera posicion vacía
				foreach (ISession session in Sessions)
				{
					if (session == null)
					{
						Sessions[pos] = sess;
						return pos;
					}
					pos++;
				}

				Sessions.Insert(pos, sess);
				return pos;
			}
			catch 
			{
				throw new iQException("SFactories no contiene sesiones activas");
			}
		}

		/// <summary>
		/// Devuelve la sesión asociada a un objeto
		/// </summary>
		/// <param name="type">Tipo del objeto</param>
		/// <param name="oid">ID del objeto</param>
		/// <returns></returns>
		public ISession GetSession(Type type, long oid)
		{
			int pos = -1;
			foreach (ISession sess in Sessions)
			{
				pos++;
				if (sess.Get(type, oid) != null)
					return sess;
			}

			return null;
		}

		/// <summary>
		/// Devuelve un criterio asociado a la sesion
		/// </summary>
		/// <param name="sess">Sesión desde la que obtener el criterio</param>
		/// <returns></returns>
		public CriteriaEx GetCriteria(ISession sess, Type type)
		{
			return (sess == null) ? null : new CriteriaEx(type, (SessionImpl)sess, 0);
		}
		public CriteriaEx GetCriteria(Type type, long oid)
		{
			ISession sess = GetSession(type, oid);
			return (sess == null) ? null : new CriteriaEx(type, (SessionImpl)sess, 0);
		}

		public ITransaction BeginTransaction(ISession sess)
		{
			return (sess != null) ? sess.BeginTransaction() : null;
		}

		/// <summary>
		/// Devuelve la transaccion asociada a una session
		/// </summary>
		/// <param name="sess">Sesión</param>
		/// <returns></returns>
		public ITransaction GetTransaction(ISession sess)
		{
			return (sess != null) ? sess.Transaction : null;
		}
		public ITransaction GetTransaction(Type type, long oid)
		{
			foreach (ISession sess in Sessions)
			{
				if (sess.Get(type, oid) != null)
					return sess.Transaction;
			}

			return null;
		}

		#endregion

		#region Commands

		public IList HQLSelect(string query)
        {
            string user = AppContext.User != null ? AppContext.User.Name : SettingsMng.GetSuperUser();

            ISession sess = SFactories[Library.User.MapToDBUsername(user)].OpenSession();
			ITransaction trans = sess.BeginTransaction();
			IList results;

			try
			{
				results = sess.CreateQuery(query).List();
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new Exception(ex.Message);
			}
			finally
			{
				sess.Close();
			}

			return results;
		}

		public void HQLExecute(string query, Type type)
        {
            string user = AppContext.User != null ? AppContext.User.Name : SettingsMng.GetSuperUser();

            ISession sess = SFactories[Library.User.MapToDBUsername(user)].OpenSession();
			ITransaction trans = sess.BeginTransaction();

			try
			{
				IQuery nHQ = sess.CreateSQLQuery(query).AddEntity(type);
				nHQ.UniqueResult();
				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new Exception(ex.Message);
			}
			finally
			{
				sess.Close();
			}
		}

		/*public IList SQLNativeSelect(string query, Type type)
		{
			ISession sess = SFactory.OpenSession();
			ITransaction trans = sess.BeginTransaction();
			IList results = null;

			try
			{
				IQuery nHQ = sess.CreateSQLQuery(query)
								.SetResultTransformer(Transformers.AliasToBean(type));

				results = nHQ.List();

				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw new Exception(ex.Message);
			}
			finally
			{
				sess.Close();
			}

			return results;
		}*/

		/// <summary>
		/// Ejecuta una consulta SQL nativa mediante una nueva session
		/// </summary>
		/// <param name="query"></param>
		public IDataReader SQLNativeSelect(string query)
		{
			ISession sess = null;
			ITransaction trans = null;
            //System.Object values;

			try
            {
                string user = AppContext.User != null ? AppContext.User.Name : SettingsMng.GetSuperUser();

                sess = SFactories[Library.User.MapToDBUsername(user)].OpenSession();
				trans = sess.BeginTransaction();
				IDbCommand command = sess.Connection.CreateCommand();

				command.CommandText = query;
				IDataReader list = command.ExecuteReader();

				trans.Commit();

                //IDataRecord rec;

                //rec.GetValues(values);

				return list;
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();

				throw ex;
			}
			finally
			{
				sess.Close();
			}
		}

		/// <summary>
		/// Ejecuta una consulta SQL nativa mediante una session abierta
		/// </summary>
		/// <param name="query"></param>
		/// <param name="sess"></param>
		public IDataReader SQLNativeSelect(string query, ISession sess)
		{
			try
			{
				IDbCommand command = sess.Connection.CreateCommand();

                command.CommandText = query;
                return command.ExecuteReader();
			}
			catch (Exception ex)
			{
				iQExceptionHandler.SetCaller(ex, "nHManager::SQLNativeSelect");
				throw new NHibernate.ADOException(ex.Message, ex);
			}
		}

		/// <summary>
		/// Ejecuta una instruccion SQL nativa mediante una nueva session
		/// </summary>
		/// <param name="query"></param>
		public void SQLNativeExecute(string query)
		{
			ISession sess = null;
			ITransaction trans = null;

			try
            {
                string user = AppContext.User != null ? AppContext.User.Name : SettingsMng.GetSuperUser();

                sess = SFactories[Library.User.MapToDBUsername(user)].OpenSession();
				trans = sess.BeginTransaction();
				IDbCommand command = sess.Connection.CreateCommand();

				command.CommandText = query;
				command.ExecuteNonQuery();

				trans.Commit();
			}
			catch (Exception ex)
			{
				if (trans != null) trans.Rollback();
				throw ex;
			}
			finally
			{
				sess.Close();
			}
		}

		/// <summary>
		/// Ejecuta una instruccion SQL nativa mediante una session abierta
		/// </summary>
		/// <param name="query"></param>
		/// <param name="sess"></param>
		public void SQLNativeExecute(string query, ISession sess)
		{
			try
			{
				IDbCommand command = sess.Connection.CreateCommand();

				command.CommandText = query;
				command.ExecuteNonQuery();
			}
			catch (Exception ex) { throw ex; }
		}

		#endregion

		#region Factory Methods

		public static nHManager _main;

		public static nHManager Instance { get { return (_main != null) ? _main : new nHManager(); } }

		private nHManager()
		{
			_main = this;
		}

		public void Close()
		{
            foreach (KeyValuePair<string, NHibernate.ISessionFactory> item in SFactories)
            {
                item.Value.Close();
                item.Value.Dispose();
            }

            SFactories.Clear();
		}

        public void CloseSessionFactory(string dbUser)
		{
			//No cerramos la primera porque se usa para hacer login de usuarios
			if (SFactories.First().Key == dbUser) return;

            if (SFactories.ContainsKey(dbUser) && SFactories[dbUser] != null)
			{
                SFactories[dbUser].Close();
                SFactories[dbUser].Dispose();
			}
		}

        public void Configure(string nHConfigFile, IEnumerable<Type> mappings)
        {
            Configure(nHConfigFile, mappings, string.Empty, string.Empty);
        }
        public void Configure(string nHConfigFile, IEnumerable<Type> mappings, string dbPwd, string dbName)
        {
			Configure(nHConfigFile, mappings, string.Empty, string.Empty, string.Empty);
		}
        public void Configure(string nHConfigFile, IEnumerable<Type> mappings, string dbPwd, string dbName, string server)
		{
			Configure(nHConfigFile, mappings, string.Empty, string.Empty, string.Empty, dbName);
		}
        public void Configure(string nHConfigFile, IEnumerable<Type> mappings, string dbPwd, string dbUser, string dbName, string server)
		{
			try
            {
                _cfg = new Configuration();
                _cfg.Configure(nHConfigFile);

                var mapper = new ConventionModelMapper();
                mapper.AddMappings(mappings);

                _cfg.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

				SetCredentials(dbUser, dbPwd, dbName, server);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.SetCaller(ex, "nHManager::Configure");
				throw ex;
			}
		}

		/// <summary>
		/// Función que cambia el usuario por defecto que se carga al ejecutar la aplicación
		/// </summary>
		/// <param name="nHConfigFile"></param>
		public void ConfigureDefaultUser(string nHConfigFile)
		{
			string propiedad = "<property name=\"default_user\">";
			string fin_propiedad = "</property>";
			string texto = System.IO.File.ReadAllText(nHConfigFile);

			int pos = texto.IndexOf(propiedad);
			string inicio = texto.Substring(0, pos + propiedad.Length);
			texto = texto.Substring(inicio.Length);
			pos = texto.IndexOf(fin_propiedad);
			string fin = texto.Substring(pos);
			texto = inicio + DefaultUser + fin;

			System.IO.File.WriteAllText(nHConfigFile, texto);
		}

		public void ChangeSessionFactory(string dbUser)
		{
			try
			{
				if (SFactories.ContainsKey(dbUser))
				{
					RemoveSessionFactory(dbUser, true);
					CreateSessionFactory(dbUser);
				}
			}
			catch (Exception ex)
			{
				RemoveSessionFactory(dbUser);
				throw ex;
			}
		}

        public void CreateSessionFactory(string dbUser, bool forceChangeSessionFactory = false)
        {
			try
			{
                if (!SFactories.ContainsKey(dbUser))
                {
                    try
                    {
                        SFactories.Add(dbUser, _cfg.BuildSessionFactory());
                    }
                    catch
                    {
                        List<string> servers = new List<string>();

                        if (SettingsMng.Instance.GetActiveServer() != string.Empty) servers.Add(SettingsMng.Instance.GetActiveServer());
                        if (SettingsMng.Instance.GetLANServer() != string.Empty && !servers.Contains(SettingsMng.Instance.GetLANServer())) servers.Add(SettingsMng.Instance.GetLANServer());
                        if (SettingsMng.Instance.GetWANServer() != string.Empty && !servers.Contains(SettingsMng.Instance.GetWANServer())) servers.Add(SettingsMng.Instance.GetWANServer());
                        servers.Add(nHManager.Instance.Host);

                        for (int i = 0; i <= servers.Count; i++)
                        {
                            string connection = _cfg.GetProperty("hibernate.connection.connection_string");
                            string new_connection = string.Empty;

                            int pos = 0;
                            pos = connection.IndexOf("Server=");
                            pos += 7;
                            new_connection = connection.Substring(0, pos);
                            connection = connection.Substring(pos);
                            new_connection += servers[i];
                            pos = connection.IndexOf(";");
                            new_connection += connection.Substring(pos);

                            _cfg.SetProperty("hibernate.connection.connection_string", new_connection);

                            connection = _cfg.GetProperty("connection.connection_string");
                            pos = connection.IndexOf("Server=");
                            pos += 7;
                            new_connection = connection.Substring(0, pos);
                            connection = connection.Substring(pos);
                            new_connection += servers[i];
                            pos = connection.IndexOf(";");
                            new_connection += connection.Substring(pos);

                            _cfg.SetProperty("connection.connection_string", new_connection);
                            try
                            {
                                SFactories.Add(dbUser, _cfg.BuildSessionFactory());
                                nHManager.Instance.SetServer(servers[i]);
                                return;
                            }
                            catch { continue; }
                        }
                    }
                }
                // Modificamos la SessionFactory solo si se fuerza explicitamente o es aplicacion desktop por si cambia el password, 
                // el host o el dbname. No lo hacemos si es aplicacion web porque si esto ocurre se llama explícitame a ChangeSessionFactory
                // desde la aplicación, de esta forma se hace una única vez y así mejoramos el rendimiento para cada script
                else if ((SettingsMng.Instance.GetApplicationType() == EAppType.Desktop) || (forceChangeSessionFactory))
                {
                    ChangeSessionFactory(dbUser);
                }
			}
			catch (Exception ex)
			{
				RemoveSessionFactory(dbUser);
				throw ex;
			}
        }

		public void RemoveSessionFactory(string dbUser, bool overrideFirst = false)
		{
			if (SFactories.Count == 0) return;

			//No borramos la primera porque se usa para hacer login de usuarios
			if (!overrideFirst && SFactories.First().Key == dbUser) return;

			if (SFactories.ContainsKey(dbUser) && SFactories[dbUser] != null)
			{
				SFactories[dbUser].Close();
				SFactories[dbUser].Dispose();
				SFactories.Remove(dbUser);
			}
		}

		public void ChangeCredentials(string dbUser, string dbPwd, string server)
		{
			try
			{
				if (!String.IsNullOrEmpty(dbUser))
					SetUser(dbUser);

				if (!String.IsNullOrEmpty(dbPwd))
					SetDBPassword(dbPwd);

				if (!String.IsNullOrEmpty(server))
					SetServer(server);

				string key = string.Empty;
				foreach (KeyValuePair<string, string> item in _cfg.Properties)
				{
					key = item.Key.ToString();
					if (key == "hibernate.connection.connection_string")
					{
						int pos = 0;
						_server = item.Value.ToString();
						pos = _server.IndexOf("Server=");
						pos += 7;
						_server = _server.Substring(pos);
						pos = _server.IndexOf(";");
						_server = _server.Substring(0, pos);
					}
				}

				ChangeSessionFactory(dbUser);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}

		public void SetCredentials(string dbUser, string dbPwd) { SetCredentials(dbUser, dbPwd, string.Empty, string.Empty); }
		public void SetCredentials(string dbUser, string dbPwd, string dbName, string server)
		{
			try
			{
				if (!String.IsNullOrEmpty(dbUser))
					SetUser(dbUser);

				if (!String.IsNullOrEmpty(dbPwd))
					SetDBPassword(dbPwd);

				if (!String.IsNullOrEmpty(dbName))
					SetDBName(dbName);

				if (!String.IsNullOrEmpty(server))
					SetServer(server);

				string key = string.Empty;
				foreach (KeyValuePair<string, string> item in _cfg.Properties)
				{
					key = item.Key.ToString();
					if (key == "hibernate.connection.connection_string")
					{
						int pos = 0;
						_server = item.Value.ToString();
						pos = _server.IndexOf("Server=");
						pos += 7;
						_server = _server.Substring(pos);
						pos = _server.IndexOf(";");
						_server = _server.Substring(0, pos);
					}
				}

                CreateSessionFactory(dbUser);
			}
			catch (Exception ex)
			{
				iQExceptionHandler.TreatException(ex);
			}
		}
		
		#endregion

		#region Format Methods

		public string FormatQuery(string query)
		{
			return query;
		}

		#endregion

		#region SQL

		/// <summary>
		/// Construye un LOCK para el esquema dado
		/// </summary>
		/// <param name="schema">Esquema de la base de datos</param>
		/// <returns>Consulta</returns>
		public string LOCK(Type type, string schema)
		{
			string tabla = string.Empty;

			if (schema == null)
				tabla = GetSQLTable(type);
			else
			{
				schema = "\"" + ((schema == "COMMON") ? schema : Convert.ToInt32(schema).ToString("0000")) + "\"";
				tabla = schema + ".\"" + Cfg.GetClassMapping(type).Table.Name + "\"";
			}

			//return String.Empty;

            switch (Cfg.GetProperty("dialect"))
            {
                // En PostgreSQL el LOCK se hace en el SELECT para hacerlo a nivel de registro
                // y no de tabla
                case "NHibernate.Dialect.PostgreSQL82Dialect":
                    return string.Empty;
                
                default:
                    return "LOCK TABLE " + tabla + " IN ROW EXCLUSIVE MODE NOWAIT;";
            }
		}

		/// <summary>
		/// Construye el SELECT de la lista
		/// </summary>
		/// <returns>Consulta SQL</returns>
		/// <remarks>Obtiene el esquema del fichero de configuración de nHibernate</remarks>
		public string SELECT(Type type)
		{
			return SELECT(type, null, true, null, null, null);
		}

		/// <summary>
		/// Construye el SELECT de la lista
		/// </summary>
		/// <param name="type">Tipo del objeto para obtener la tabla</param>
		/// <param name="schema">Esquema de datos</param>
		/// <param name="lock_regs">Bloqueo de registros</param>
		/// <param name="filter_field">Campo de filtrado</param>
		/// <param name="field_value">Valor</param>
		/// <param name="order_field">Campo de ordenación</param>
		/// <returns></returns>
		public string SELECT(Type type,
							 string schema,
							 bool lock_regs,
							 string filter_field,
							 object field_value,
							 string order_field)
		{

			string tabla = string.Empty;
			string columna = string.Empty;

			if (schema == null)
				tabla = GetSQLTable(type);
			else
			{
				schema = "\"" + ((schema == "COMMON") ? schema : Convert.ToInt32(schema).ToString("0000")) + "\"";
				tabla = schema + ".\"" + Cfg.GetClassMapping(type).Table.Name + "\"";
			}

			string query = "SELECT * " +
							" FROM " + tabla;

			if (filter_field != null)
			{
				columna = (filter_field == "Oid") ? "OID" : GetTableField(type, filter_field);
				query += " WHERE \"" + columna + "\" = " + field_value.ToString();
			}

			if (order_field != null)
			{
				columna = (order_field == "Oid") ? "OID" : GetTableField(type, order_field);
				query += " ORDER BY \"" + columna + "\"";
			}

			query += (lock_regs) ? " FOR UPDATE NOWAIT;" : ";";

			return query;
		}

		#endregion
	}
}