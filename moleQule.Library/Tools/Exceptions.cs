using System;

using moleQule.Library.Resources;

namespace moleQule.Library
{
	/// <summary>
	/// Códigos de error de moleQule
	/// </summary>
	public enum iQExceptionCode
	{
		NO_CODE = 100,
		DUPLICATE_KEY = 101,
		LOCKED_ROW = 102,
		SYNTAX_ERROR = 103,
		SERVER_NOT_FOUND = 104,
		USER_NOT_ALLOWED = 105,
		PASSWORD_FAILED = 106,
		DB_VERSION_MISSMATCH = 107,
		INVALID_OBJECT = 108,
		INFO = 109,
		WS_ERROR = 110,
		SESSION_NOT_FOUND = 111,
		LOCKED_USER = 112,
		RESOURCE_NOT_FOUND = 113,
		HTTP_ERROR = 500,
        WARNING = 109,
	}

	/// <summary>
	/// Error codes
	/// </summary>
	public static class iQExCode
	{
		/// <summary>
		/// NH Exception map codes
		/// </summary>
		public const string NOT_DEFINED = "#####";
		public const string NH_DUPLICATE_KEY = "23505";
		//public const string NH_SYNTAX_ERROR = "42601";
		public const string NH_LOCKED_ROW = "57014";
		//public const string NH_UNDEFINED_TABLE = "42P01";
		public const string NH_PASSWORD_FAILED = "28000";
		public const string NH_SESSION_NOT_FOUND = "CS_00001";

		/// <summary>
		/// PostgreSQL map codes
		/// </summary>
		public const string PG_DUPLICATE_KEY = "PG_DUP_NOT_DEFINED";
		public const string PG_PASSWORD_FAILED = "28P01";
		public const string PG_SYNTAX_ERROR = "42601";
		public const string PG_UNDEFINED_COLUMN = "42703";
		public const string PG_UNDEFINED_TABLE = "42P01";
		public const string PG_LOCKED_ROW = "55P03";
	}

	/// <summary>
	/// Manejador de Excepciones
	/// </summary>
	public class iQExceptionHandler
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public iQExceptionHandler() { }

		public static string GetCaller(Exception ex)
		{
			return ex.Data.Contains("CALLER") ? Convert.ToString(ex.Data["CALLER"]) : "NO CALLER";
		}
		public static void SetCaller(Exception ex, string caller)
		{
			if (!ex.Data.Contains("CALLER"))
				ex.Data.Add("CALLER", caller);
		}

		/// <summary>
		/// Busca y devuelve una iQException dentro de una System.Exception
		/// </summary>
		/// <param name="ex">Excepcion</param>
		/// <returns>iQException or null</returns>
		public static iQException GetiQException(Exception ex)
		{
			Exception _ex = ex;

			while (!(_ex is iQException) && (_ex != null)) _ex = _ex.InnerException;

			if (!(_ex is iQException))
				return null;

			return (iQException)_ex;
		}

		public static iQException ConvertToiQException(Exception ex)
		{
			return ConvertToiQException(ex, iQExceptionCode.NO_CODE);
		}
		public static iQException ConvertToiQException(Exception ex, iQExceptionCode exCode)
		{
			if (ex is iQException) return (iQException)ex;
			if (ex == null) return new iQException(string.Empty, exCode);

			iQException iQ_ex = GetiQException(ex);

			if (iQ_ex != null) return iQ_ex;

			iQ_ex = new iQException(ex.Message);
			iQ_ex.Code = exCode;
			iQ_ex.SysMessage = GetAllMessages(ex);

			return iQ_ex;
		}

		/// <summary>
		/// Devuelve todos los mensajes anidados en le excepcion
		/// </summary>
		/// <param name="ex">Excepcion</param>
		public static string GetAllMessages(Exception ex) { return GetAllMessages(ex, false); }
		public static string GetAllMessages(Exception ex, bool getDebug)
		{
			Exception _ex = ex;
			string msg = string.Empty;
			string debug_msg = string.Format(Resources.Errors.DEBUG_MSG, System.Environment.NewLine);

			while (_ex != null)
			{
				if (_ex is iQException)
				{
					msg += _ex.Message + Environment.NewLine + Environment.NewLine;
					debug_msg += (_ex as iQException).SysMessage;
				}
				else if (ex is FtpClient.FtpException)
					msg += _ex.Message + Environment.NewLine + Environment.NewLine;
				else
					debug_msg += _ex.Message + Environment.NewLine;

				_ex = _ex.InnerException;
			}

			debug_msg += Environment.NewLine + ex.StackTrace;

#if (DEBUG)
			if (SettingsMng.Instance.GetApplicationType() == EAppType.Desktop || getDebug)
				return msg + Environment.NewLine + debug_msg;
			else
				return msg;
#else
			if (SettingsMng.Instance.GetApplicationType() == EAppType.Desktop)
				if (msg == string.Empty) msg = Resources.Errors.NO_OPERATION;

			return (getDebug) ? msg + Environment.NewLine + debug_msg : msg;
#endif
		}

		public static Exception GetLastException(Exception ex)
		{
			Exception inner = ex.InnerException;

			while (inner != null)
			{
				if (inner.InnerException != null)
					inner = inner.InnerException;
			}

			return inner;
		}

		public static void TreatException(Exception ex) { TreatException(ex, null); }
		public static void TreatException(Exception ex, object[] parameters)
		{
			MyLogger.LogText(ex.Message + ex.StackTrace, GetCaller(ex));

			if (ex is Npgsql.NpgsqlException)
			{
				if (ex.InnerException != null) TreatException(ex.InnerException);
				else
				{
					MyLogger.LogText(ex.Message, GetCaller(ex));
					ThrowExceptionByCode(ex as Npgsql.NpgsqlException, parameters);
				}
			}
			else if (ex is NHibernate.ADOException)
			{
				//DB Exception
				if ((ex.InnerException != null) && (ex.InnerException is NHibernate.ADOException))
					TreatException(ex.InnerException);
				else
				{
					MyLogger.LogText(ex.Message, GetCaller(ex));
					ThrowExceptionByCode(ex as NHibernate.ADOException, parameters);
				}
			}
			else if ((ex is Csla.DataPortalException) || (ex is Csla.Server.CallMethodException))
			{
				if (ex.InnerException != null) TreatException(ex.InnerException);
				else
				{
					MyLogger.LogText(ex.Message, GetCaller(ex));
					throw new iQException(GetAllMessages(ex));
				}
			}
			else if (ex is System.Net.Sockets.SocketException)
			{
				MyLogger.LogText(ex.Message, GetCaller(ex));
				//Conexion Exception
				throw new iQException(Resources.Errors.SERVER_NOT_FOUND, ex.Message, iQExceptionCode.SERVER_NOT_FOUND);
			}
			else if (ex is iQAuthorizationException)
			{
				//User Authorization
				throw ex;
			}
			else if (ex is Csla.Validation.ValidationException)
			{
				//Object Validation
				throw new iQValidationException(Resources.Errors.GENERIC_VALIDATION_FAIL, (ex as Csla.Validation.ValidationException).Message);
			}
			else if (ex is iQLockException)
			{
				//Code Validation
				throw ex;
			}
			else if (ex is iQDuplicateCodeException)
			{
				//Code Validation
				throw ex;
			}
			else if (ex is iQValidationException)
			{
				//Child Validation
				if (((iQValidationException)ex).Field == string.Empty)
					throw new iQValidationException(string.Empty, (ex as iQValidationException).Message, ((iQValidationException)ex).SysMessage);
				else
					throw ex;
			}
			else if (ex is System.NullReferenceException)
			{
				MyLogger.LogText(ex.Message, GetCaller(ex));
				//Rules checking
				throw new iQException(Resources.Errors.RULES_CHECK + Environment.NewLine + iQExceptionHandler.GetAllMessages(ex));
			}
			else if (ex is System.Web.HttpException)
			{
				//Code Validation
				string msg = string.IsNullOrEmpty(((System.Web.HttpException)ex).GetHtmlErrorMessage()) 
									? string.Empty 
									: ((System.Web.HttpException)ex).GetHtmlErrorMessage();
				
				throw new iQException(msg, 
										iQExceptionHandler.GetAllMessages(ex, true),
										iQExceptionCode.HTTP_ERROR, 
										new object[] { ex });
			}
			else
			{
				MyLogger.LogText(ex.Message, GetCaller(ex));
				throw new iQException(GetAllMessages(ex as Exception));
			}
		}

		/// <summary>
		/// Lanza una excepcion en función del codigo de una exception ADO de nHibernate
		/// </summary>
		/// <param name="ex">Excepción causante</param>
		public static void ThrowExceptionByCode(NHibernate.ADOException ex) { ThrowExceptionByCode(ex, null); }
		public static void ThrowExceptionByCode(NHibernate.ADOException ex, object[] parameters)
		{
			Npgsql.NpgsqlException pgex = null;

			try
			{
				pgex = (Npgsql.NpgsqlException)ex.InnerException;
			}
			catch
			{
				ThrowExceptionByCode(iQExCode.NOT_DEFINED, ex, parameters);
			}

			ThrowExceptionByCode((pgex != null) ? pgex.Code : iQExCode.NOT_DEFINED, ex, parameters);
		}
		public static void ThrowExceptionByCode(Npgsql.NpgsqlException ex, object[] parameters)
		{
			ThrowExceptionByCode(ex.Code, ex, parameters);
		}

		/// <summary>
		/// Lanza una excepcion en función del codigo de error
		/// </summary>
		/// <param name="ex">Excepción causante</param>
		public static void ThrowExceptionByCode(string errorCode, Exception ex, object[] parameters)
		{
			switch (errorCode)
			{
				case iQExCode.NH_PASSWORD_FAILED:
				case iQExCode.PG_PASSWORD_FAILED:
					{
						throw new iQAuthorizationException(String.Format(Errors.LOGIN_FAILED, (AppContext.User != null) ? AppContext.User.Name : string.Empty),
															GetAllMessages(ex),
															iQExceptionCode.PASSWORD_FAILED);
					}

				case iQExCode.PG_LOCKED_ROW:
				case iQExCode.NH_LOCKED_ROW:
					{
						throw new iQLockException(Errors.NO_OPERATION + Environment.NewLine
												  + Errors.LOCKED_ROW,
												  GetAllMessages(ex));
					}

				case iQExCode.PG_DUPLICATE_KEY:
				case iQExCode.NH_DUPLICATE_KEY:
					{
						throw new iQPersistentException(Errors.NO_OPERATION + Environment.NewLine + Environment.NewLine
														+ Errors.DUPLICATE_KEY,
														GetAllMessages(ex),
														iQExceptionCode.DUPLICATE_KEY);
					}

				case iQExCode.PG_SYNTAX_ERROR:
				case iQExCode.PG_UNDEFINED_COLUMN:
				case iQExCode.PG_UNDEFINED_TABLE:
					//case iQExCode.NH_SYNTAX_ERROR:
					{
						string query = (parameters != null) ? (string)parameters[0] : "EMPTY QUERY";

						throw new iQPersistentException(Errors.NO_OPERATION + Environment.NewLine + Environment.NewLine
														+ String.Format(Errors.SYNTAX_ERROR, query),
														GetAllMessages(ex),
														iQExceptionCode.SYNTAX_ERROR);
					}

				default:
					{
						if (ex.InnerException != null) TreatException(ex.InnerException);
						else throw new iQException(ex.Message);

						throw new iQPersistentException(Errors.NO_OPERATION + Environment.NewLine + Environment.NewLine
														+ String.Format(Errors.GENERIC_ADO, errorCode) + Environment.NewLine + Environment.NewLine
														+ GetAllMessages(ex),
														GetAllMessages(ex),
														iQExceptionCode.NO_CODE);
					}
			}

		}

		#region ErrorHandling

		public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);

		public class ErrorEventArgs : EventArgs
		{
			public iQException Exception;

			public ErrorEventArgs(iQException exception)
			{
				this.Exception = exception;
			}
		}

		public static void HandleError(ErrorEventHandler errorHandler, string message, string sysMessage)
		{
			if (errorHandler != null)
				errorHandler(null, new ErrorEventArgs(new iQException(message, sysMessage)));
			else
				throw new iQException(message, sysMessage);
		}
		public static void HandleError(ErrorEventHandler errorHandler, iQException ex)
		{
			if (errorHandler != null)
				errorHandler(null, new ErrorEventArgs(ex));
			else
				throw ex;
		}

		#endregion
	}

	/// <summary>
	/// Manejador de un tipo concreto de excepciones
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class iQExceptionHandler<T> where T : iQException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public iQExceptionHandler() { }

		/// <summary>
		/// Busca y devuelve una iQException dentro de una System.Exception
		/// </summary>
		/// <param name="ex">Excepcion</param>
		/// <returns>iQException or null</returns>
		public static T GetiQException(Exception ex)
		{
			Exception _ex = ex;

			while (!(_ex is T) && (_ex != null)) _ex = _ex.InnerException;

			return (_ex is T) ? (T)_ex : default(T);
		}

		/// <summary>
		/// Devuelve todos los mensajes anidados en le excepcion
		/// </summary>
		/// <param name="ex">Excepcion</param>
		public static string GetAllMessages(Exception ex)
		{
			Exception _ex = ex;
			string msg = string.Empty;

			while (_ex != null)
			{
				msg += _ex.Message + System.Environment.NewLine + System.Environment.NewLine;
				_ex = _ex.InnerException;
			}

			return msg;
		}

		/// <summary>
		/// Lanza una excepcion en función del codigo de una exception ADO de nHibernate
		/// </summary>
		/// <param name="ex">Excepción causante</param>
		public static void ThrowExceptionByCode(Exception ex)
		{

			switch (GetCode(ex))
			{
				case iQExCode.PG_DUPLICATE_KEY:
					{
						throw new iQPersistentException(Errors.NO_OPERATION + System.Environment.NewLine
														+ Errors.DUPLICATE_KEY,
														GetAllMessages(ex),
														iQExceptionCode.DUPLICATE_KEY);
					}

				case iQExCode.PG_LOCKED_ROW:
					{
						throw new iQLockException(Errors.NO_OPERATION + System.Environment.NewLine
												  + Errors.LOCKED_ROW,
												  GetAllMessages(ex));
					}

				default:
					{
						throw new iQPersistentException(Errors.NO_OPERATION + System.Environment.NewLine
														+ Errors.GENERIC_ADO,
														GetAllMessages(ex),
														iQExceptionCode.DUPLICATE_KEY);
					}
			}
		}

		/// <summary>
		/// Devuelve el codigo de error de una System.Exception a partir
		/// del mensaje de la excepcion que viene de la forma "ERROR: CODIGO: mensaje"
		/// </summary>
		/// <param name="ex">Excepción</param>
		/// <returns>Código de error incluido en el ex.Message</returns>
		private static string GetCode(Exception ex)
		{
			int pos = ex.Message.IndexOf(":");
			string aux = ex.Message.Substring(pos + 1);
			return aux.Substring(1, ex.Message.IndexOf(":")).Trim();
		}
	}

	/// <summary>
	/// Excepcion genérica
	/// </summary>
	public class iQException : System.Exception
	{
		/// <summary>
		/// Mensajes anidados
		/// </summary>
		public string InnerMessages { get { return iQExceptionHandler.GetAllMessages(this, true); } }

		/// <summary>
		/// Mensaje del sistema
		/// </summary>
		public string SysMessage { get; set; }

		/// <summary>
		/// Codigo de error
		/// </summary>
		public virtual iQExceptionCode Code { get; set; }

		/// <summary>
		/// Argumentos
		/// </summary>
		public object[] Args { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
        public iQException(Exception innerException)
            : this(innerException, string.Empty, string.Empty, iQExceptionCode.NO_CODE, null) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">Mensaje</param>
        public iQException(Exception innerException, string msg)
            : this(innerException, msg, string.Empty, iQExceptionCode.NO_CODE, null) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="msg">Mensaje</param>
        public iQException(string msg)
            : this(null, msg, string.Empty, iQExceptionCode.NO_CODE, null) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		public iQException(string msg, object[] args)
			: this(null, msg, string.Empty, iQExceptionCode.NO_CODE, args) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="code">Código del mensaje</param>
		public iQException(string msg, iQExceptionCode code)
			: this(null, msg, string.Empty, code, null) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="code">Mensaje del sistema</param>
		public iQException(string msg, string sysMessage)
			: this(null, msg, sysMessage, iQExceptionCode.NO_CODE, null) { }

		public iQException(string msg, string sysMessage, object[] args)
			: this(null, msg, sysMessage, iQExceptionCode.NO_CODE, args) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="code">Mensaje del sistema</param>
		/// <param name="code">Código del mensaje</param>
		public iQException(string msg, string sysMessage, iQExceptionCode code)
			: this(null, msg, sysMessage, code, null) { }

        public iQException(string msg, string sysMessage, iQExceptionCode code, object[] args)
            : this(null, msg, sysMessage, code, args) { }

		public iQException(Exception innerException, string msg, string sysMessage, iQExceptionCode code, object[] args)
            : base(msg, innerException)
		{
			SysMessage = sysMessage;
			Code = code;
			Args = args;
		}
	}

	/// <summary>
	/// Excepcion genérica
	/// </summary>
	public class iQCslaException : iQException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		public iQCslaException(string msg)
			: base(msg, iQExceptionCode.SESSION_NOT_FOUND) { }
	}

	/// <summary>
	/// Exception de error de sesión
	/// </summary>
	public class iQCslaSessionException : iQCslaException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="method">Metodo que provoca la excepcion</param>
		public iQCslaSessionException(Type type, long sessioNumber)
			: base(String.Format(Resources.Errors.SESSION_EXCEPTION, sessioNumber.ToString(), type.Name))
		{
			Code = iQExceptionCode.SESSION_NOT_FOUND;
		}
	}

	/// <summary>
	/// Exception de error de implementacion
	/// </summary>
	public class iQImplementationException : iQException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="method">Metodo que provoca la excepcion</param>
		public iQImplementationException(string method)
			: base(String.Format(Messages.IMP_EXCEPTION, method)) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="sysMessage">Mensaje del sistema</param>
		public iQImplementationException(string msg, string sysMessage)
			: base(msg, sysMessage) { }

	}

	/// <summary>
	/// Excepción informativa
	/// </summary>
	public class iQInfoException : iQException
	{
		public iQInfoException()
			: this(Resources.Errors.INFO_EXCEPTION_MESSAGE) { }

        public iQInfoException(string message, string sysMessage)
            : base(message, sysMessage, iQExceptionCode.INFO) { }

        public iQInfoException(string message, string sysMessage = "", iQExceptionCode code = iQExceptionCode.INFO)
			: base(message, string.Empty, code) { }
	}

	/// <summary>
	/// Excepción de acceso a objeto
	/// </summary>
	public class iQAuthorizationException : iQException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="method">Mensaje</param>
		public iQAuthorizationException()
			: this(Resources.Messages.USER_NOT_ALLOWED) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="method">Mensaje</param>
		public iQAuthorizationException(string msg)
			: this(msg, string.Empty, iQExceptionCode.USER_NOT_ALLOWED) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="method">Mensaje</param>
		public iQAuthorizationException(iQExceptionCode code)
			: this(string.Empty, string.Empty, code) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="sysMessage">Mensaje del sistema</param>
		public iQAuthorizationException(string msg, string sysMessage, iQExceptionCode code)
			: base(msg, sysMessage, code) { }
	}

	/// <summary>
	/// Exception de error de implementacion
	/// </summary>
	public class iQNotAllowedCodeException : iQException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="method">Metodo que provoca la excepcion</param>
		public iQNotAllowedCodeException(string method)
			: base(String.Format(Messages.NOR_ALLOWED_CODE_EXCEPTION, method)) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="sysMessage">Mensaje del sistema</param>
		public iQNotAllowedCodeException(string msg, string sysMessage)
			: base(msg, sysMessage) { }

	}

	/// <summary>
	/// Excepción de consulta sin resultados
	/// </summary>
	public class iQNoResultsException : iQException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="method">Mensaje</param>
		public iQNoResultsException(string msg)
			: base(msg) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="sysMessage">Mensaje del sistema</param>
		public iQNoResultsException(string msg, string sysMessage)
			: base(msg, sysMessage) { }

	}

	/// <summary>
	/// Excepción de violación de reglas de validación
	/// </summary>
	public class iQValidationException : iQException
	{
		public string Field { get; set; }

		public iQValidationException()
			: base(Resources.Errors.GENERIC_VALIDATION_FAIL) { }

		public iQValidationException(string message)
			: this(message, string.Empty, string.Empty) { }

		public iQValidationException(string message, string sys_message)
			: base(message, sys_message) { }

		public iQValidationException(string message, string sys_message, string field)
			: base((message == string.Empty) ? String.Format(Resources.Errors.VALIDATION_FAIL, field) : message, sys_message)
		{
			Field = field;
		}
	}

	/// <summary>
	/// Excepción de violación de codigo unico
	/// </summary>
	public class iQDuplicateCodeException : iQException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="sysMessage">Mensaje del sistema</param>
		public iQDuplicateCodeException(string code)
			: base(string.Format(Resources.Errors.DUPLICATE_CODE, code), string.Empty) { }
	}

	/// <summary>
	/// Excepción provocada en el acceso a la base de datos
	/// </summary>
	public class iQPersistentException : iQException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="method">Mensaje</param>
		public iQPersistentException(string msg)
			: base(msg) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="code">Código del mensaje</param>
		public iQPersistentException(string msg, iQExceptionCode code)
			: base(msg, code) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="sysMessage">Mensaje del sistema</param>
		public iQPersistentException(string msg, string sysMessage)
			: base(msg, sysMessage) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="code">Mensaje del sistema</param>
		/// <param name="code">Código del mensaje</param>
		public iQPersistentException(string msg, string sysMessage, iQExceptionCode code)
			: base(msg, sysMessage, code) { }

	}

	/// <summary>
	/// Excepción al intentar bloquear registros de la base de datos
	/// </summary>
	public class iQLockException : iQException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="method">Mensaje</param>
		public iQLockException(string msg)
			: base(msg) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="sysMessage">Mensaje del sistema</param>
		public iQLockException(string msg, string sysMessage)
			: base(msg, sysMessage)
		{
			Code = iQExceptionCode.LOCKED_ROW;
		}

	}

	/// <summary>
	/// Excepción tratando ficheros Excel
	/// </summary>
	public class iQExcelException : iQException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="method">Mensaje</param>
		public iQExcelException(string msg)
			: base(msg) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="sysMessage">Mensaje del sistema</param>
		public iQExcelException(string msg, string sysMessage)
			: base(msg, sysMessage) { }

	}

	/// <summary>
	/// Excepción de Depuración
	/// </summary>
	public class iQDebugException : iQException
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="method">Mensaje</param>
		public iQDebugException(string msg)
			: base(msg) { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="msg">Mensaje</param>
		/// <param name="sysMessage">Mensaje del sistema</param>
		public iQDebugException(string msg, string sysMessage)
			: base(msg, sysMessage) { }

	}
}