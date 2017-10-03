using Microsoft.CSharp; 
using System.Net; 
using System.IO; 
using System.Threading; 
using System.Configuration; 
using System; 

namespace moleQule.Library
{
	public class FtpClient
	{
		#region Attributes & Properties

		public const int TIMEOUT = 20000; 

		public class FtpException : Exception
		{
			public FtpException(string message) : base(message) { }
			public FtpException(string message, Exception innerException) : base(message, innerException) { }
		}

		public string _host, _user, _pass, _path, _port;

		public string Parameters
		{
			get
			{
				string msg = string.Empty;

				msg = "Servidor: " + _host + Environment.NewLine +
						"Puerto: " + _port + Environment.NewLine +
						"Usuario: " + _user + Environment.NewLine +
						"Carpeta: " + _path + Environment.NewLine;
#if DEBUG
				msg += "Password: " + _pass;
#endif
				return msg;
			}
		}

		#endregion

		#region Factory Methods
		
		public FtpClient(string host, string user, string pass, string path) 
		{ 
			this._host = host; 
			this._user = user; 
			this._pass = pass;
			this._path = System.Web.HttpUtility.UrlEncode(path);
		}

		private Uri GetUri(string path)
		{
			return new Uri("ftp://" + _host + "/" + UrlEncode(_path + "/" + path));
		}
		private string GetFTPPath(string path)
		{
			return "ftp://" + _host + "/" + UrlEncode(_path + "/" + path);
		}

		private string UrlEncode(string path) { return System.Web.HttpUtility.UrlEncode(path.Replace("\\", "/")); }
		
		#endregion

		#region Business Methods

		public string DeleteFile(string file) 
		{ 
			// Creamos una petición FTP con la dirección del fichero a eliminar 
			FtpWebRequest peticionFTP = (FtpWebRequest)WebRequest.Create(GetUri(file)); 

			// Fijamos el usuario y la contraseña de la petición 
			peticionFTP.Credentials = new NetworkCredential (_user,_pass); 
			
			// Seleccionamos el comando que vamos a utilizar: Eliminar un fichero 
			peticionFTP.Method = WebRequestMethods.Ftp.DeleteFile;
			peticionFTP.Timeout = TIMEOUT;
			peticionFTP.UsePassive = false; 
			peticionFTP.KeepAlive = false; 
			
			try 
			{ 
				FtpWebResponse respuestaFTP = (FtpWebResponse)peticionFTP.GetResponse(); 
				respuestaFTP.Close(); 

				return string.Empty; 
			} 
			catch (Exception ex) 
			{ 
				return ex.Message; 
			} 
		} 

		public Boolean ExistsFile(string path) 
		{ 
			//Creamos una peticion FTP con la dirección del objeto que queremos saber si existe 
			FtpWebRequest peticionFTP = (FtpWebRequest)WebRequest.Create(GetUri(path)); 
			
			//Fijamos el usuario y la contraseña de la petición 
			peticionFTP.Credentials = new NetworkCredential(_user, _pass); 

			//Para saber si el objeto existe, solicitamos la fecha de creación del mismo 
			peticionFTP.Method = WebRequestMethods.Ftp.GetDateTimestamp; 
			peticionFTP.Timeout = TIMEOUT;
			peticionFTP.UsePassive = false; 
			peticionFTP.KeepAlive = false; 
			
			try 
			{ 
				//si el objeto existe, se devolvera true 
				FtpWebResponse respuestaFTP = (FtpWebResponse)peticionFTP.GetResponse();
				respuestaFTP.Close();
				return true; 
			} 
			catch
			{ 
				//si el objeto no existe, se producirá un error y al entrar por el Catch 
				//se devolvera falso 
				return false; 
			} 
		}

		public Boolean ExistsFile(string dir, string file)
		{
			FtpWebRequest dirFtp = (FtpWebRequest)FtpWebRequest.Create(GetUri(dir));

			// Los datos del usuario (credenciales) 
			dirFtp.Credentials = new NetworkCredential(_user, _pass);

			// También usando la enumeración de WebRequestMethods.Ftp 
			dirFtp.Timeout = TIMEOUT;
			dirFtp.KeepAlive = false;
			dirFtp.Method = WebRequestMethods.Ftp.ListDirectory;

			// Obtener el resultado del comando 
			FtpWebResponse respuestaFTP = (FtpWebResponse)dirFtp.GetResponse();
			StreamReader reader = new StreamReader(respuestaFTP.GetResponseStream());

			// Leer el stream 
			string res = "";
			Boolean respuesta = false;

			while (!reader.EndOfStream)
			{
				res = reader.ReadLine();

				if (res == file)
				{
					respuesta = true;
				}
			}

            reader.Close();
			respuestaFTP.Close();
			return respuesta;
		}

		public Boolean ExistsDir(string dir)
		{
			string uri = GetFTPPath(dir);

			FtpWebRequest dirFtp = (FtpWebRequest)FtpWebRequest.Create(GetFTPPath(dir));

			// Los datos del usuario (credenciales) 
			dirFtp.Credentials = new NetworkCredential(_user, _pass);

			//Operacion y parametros
			dirFtp.Method = WebRequestMethods.Ftp.ListDirectory;
			dirFtp.Timeout = TIMEOUT;
			dirFtp.KeepAlive = false;

			try
			{
				//si el objeto existe, se devolvera true 
				FtpWebResponse respuestaFTP = (FtpWebResponse)dirFtp.GetResponse();
				respuestaFTP.Close();
				return true;
			}
			catch
			{
				//si el objeto no existe, se producirá un error y al entrar por el Catch 
				//se devolvera falso 
				return false;
			}
		}

		public void MakeDir(String dir) 
		{ 
			FtpWebRequest peticionFTP;

			// Creamos una peticion FTP con la dirección del directorio que queremos crear 
			peticionFTP = (FtpWebRequest)WebRequest.Create(GetUri(dir)); 
			peticionFTP.KeepAlive = true; 

			// Fijamos el usuario y la contraseña de la petición 
			peticionFTP.Credentials = new NetworkCredential(_user, _pass); 

			// Seleccionamos el comando que vamos a utilizar: Crear un directorio 
			peticionFTP.Method = WebRequestMethods.Ftp.MakeDirectory; 
			peticionFTP.Timeout = TIMEOUT;
			peticionFTP.KeepAlive = false;

			try 
			{ 
				FtpWebResponse respuesta = (FtpWebResponse)peticionFTP.GetResponse(); 
				respuesta.Close(); 
			} 
			catch (Exception ex)
			{ 
				// Si se produce algún fallo, se devolverá el mensaje del error 
				throw ex; 
			} 
		}

		public void DownloadDir(string dirD) 
		{ 
			FtpWebRequest dirFtp = ((FtpWebRequest)FtpWebRequest.Create(_host)); 

			// Los datos del usuario (credenciales) 
			dirFtp.Credentials = new NetworkCredential(_user, _pass); 

			// El comando a ejecutar 
			dirFtp.Method = "LIST"; 

			// También usando la enumeración de WebRequestMethods.Ftp 
			dirFtp.Method = WebRequestMethods.Ftp.ListDirectory; 
			dirFtp.Timeout = TIMEOUT;
			dirFtp.KeepAlive = false;

			// Obtener el resultado del comando 
			FtpWebResponse respuestaFTP = (FtpWebResponse)dirFtp.GetResponse();
			StreamReader reader = null;

			try
			{
				reader = new StreamReader(respuestaFTP.GetResponseStream());

				// Leer el stream 
				string res = "";
				string nuevo_dir = "";

				while (!reader.EndOfStream)
				{
					res = reader.ReadLine();

					if (res.Contains("."))
					{
						DownloadFileFromDir(_host, dirD, res);
					}
					else
					{
						nuevo_dir = dirD + res;
						Directory.CreateDirectory(nuevo_dir);

						listarDirRecursivo(_host + res + "/", nuevo_dir + "\\", _user, _pass);
					}
				}
			}
			finally
			{
				if (reader != null) reader.Close();
				respuestaFTP.Close();
			}
		} 

		public void DownloadFile(string dirO, string dirD, string file) 
		{ 
			using (WebClient wc = new WebClient()) 
			{ 
				wc.Proxy = null;

				// Authenticate, then download a file to the FTP server. 
				// The same approach also works for HTTP and HTTPS. 

				string remoteFile = GetFTPPath(dirO + "/" + file);
				wc.Credentials = new NetworkCredential(_user, _pass);
				wc.DownloadFile(remoteFile, dirD + "\\" + System.Web.HttpUtility.UrlEncode(file));
				wc.Dispose();
			} 
		}
		public void DownloadFile(string dirO, string dirD, string remoteFile, string localFile)
		{
			using (WebClient wc = new WebClient())
			{
				wc.Proxy = null;

				// Authenticate, then download a file to the FTP server. 
				// The same approach also works for HTTP and HTTPS. 

				remoteFile = GetFTPPath(dirO + "/" + remoteFile);
				wc.Credentials = new NetworkCredential(_user, _pass);
				wc.DownloadFile(remoteFile, dirD + "\\" + System.Web.HttpUtility.UrlEncode(localFile));
				wc.Dispose();
			}
		}

		public void UploadFile(string rutaOrigen, string rutaDestino)
		{
			using (WebClient wc = new WebClient())
			{
				wc.Proxy = null;

				// Authenticate, then download a file to the FTP server. 
				// The same approach also works for HTTP and HTTPS. 

				string username = _user;
				string password = _pass;
				wc.Credentials = new NetworkCredential(username, password);
				wc.UploadFile(GetFTPPath(rutaDestino), rutaOrigen);
			}
		}

		public String UploadFile(String fichero, String destino, String dir)
		{
			dir = dir.Replace("\\", "/");
			destino = destino.Replace("\\", "/");

			FileInfo infoFichero = new FileInfo(fichero);

			// Si no existe el directorio, lo creamos 
			if (ExistsFile(dir)) MakeDir(System.Web.HttpUtility.UrlEncode(dir));

			// Creamos una peticion FTP con la dirección del fichero que vamos a subir 
			FtpWebRequest peticionFTP = (FtpWebRequest)FtpWebRequest.Create(GetUri(destino));

			// Fijamos el usuario y la contraseña de la petición 
			peticionFTP.Credentials = new NetworkCredential(_user, _pass);

			// Seleccionamos el comando que vamos a utilizar: Subir un fichero 
			peticionFTP.Method = WebRequestMethods.Ftp.UploadFile;
			peticionFTP.KeepAlive = false;
			peticionFTP.UsePassive = false;
			peticionFTP.UseBinary = true;

			// Informamos al servidor sobre el tamaño del fichero que vamos a subir 
			try
			{
				peticionFTP.ContentLength = infoFichero.Length;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}

			// Fijamos un buffer de 2KB 
			int longitudBuffer;
			longitudBuffer = 2048;
			Byte[] lector = new Byte[2048]; // esti estaba Byte (2048); 
			int num;

			// Abrimos el fichero para subirlo 
			FileStream fs;
			fs = infoFichero.OpenRead();
			Stream escritor = null;
			try
			{
				escritor = peticionFTP.GetRequestStream();
				// Leemos 2 KB del fichero en cada iteración 
				num = fs.Read(lector, 0, longitudBuffer);
				// num=fs.Read (lector[],0,longitudBuffer); 
				while (num != 0)
				{
					// Escribimos el contenido del flujo de lectura en el 
					// flujo de escritura del comando FTP 
					escritor.Write(lector, 0, num);
					num = fs.Read(lector, 0, longitudBuffer);
				}

				escritor.Close();
				fs.Close();
				//Si todo ha ido bien, se devolverá String.Empty 
				return String.Empty;
			}
			catch (WebException ex)
			{
				if (escritor == null) escritor.Close();
				fs.Close();
				// Si se produce algún fallo, se devolverá el mensaje del error 
				return ex.Message;
			}
		} 

		#endregion

		#region Puajj

		private void listarDirRecursivo(string dirO, string dirD, string user, string pass)
		{
			FtpWebRequest dirFtp = ((FtpWebRequest)FtpWebRequest.Create(dirO));

			// Los datos del usuario (credenciales) 
			NetworkCredential cr = new NetworkCredential(user, pass);
			dirFtp.Credentials = cr;
			// El comando a ejecutar 
			dirFtp.Method = "LIST";

			// También usando la enumeración de WebRequestMethods.Ftp 
			dirFtp.Method = WebRequestMethods.Ftp.ListDirectory;

			// Obtener el resultado del comando 
			StreamReader reader = new StreamReader(dirFtp.GetResponse().GetResponseStream());

			// Leer el stream 
			string res = "";//reader.ReadLine(); 
			//Response.Write(res); 

			string nuevo_dir = "";

			while (!reader.EndOfStream)
			{
				res = reader.ReadLine();

				if (res.Contains("."))
				{
					DownloadFileFromDir(dirO, dirD, res);
				}
				else
				{
					nuevo_dir = dirD + res;
					Directory.CreateDirectory(nuevo_dir);
					listarDirRecursivo(dirO + res + "/", nuevo_dir + "\\", user, pass);
				}
			}
			reader.Close();

			// Cerrar el stream abierto. 
		}

		private void DownloadFileFromDir(string dirO, string dirD, string archivo)
		{
			using (WebClient wc = new WebClient())
			{
				wc.Proxy = null;
				wc.BaseAddress = System.Web.HttpUtility.UrlEncode(dirO);

				// Authenticate, then download a file to the FTP server. 
				// The same approach also works for HTTP and HTTPS. 

				string username = _user;
				string password = _pass;
				wc.Credentials = new NetworkCredential(username, password);
				wc.DownloadFile(System.Web.HttpUtility.UrlEncode(archivo), dirD + archivo);
			}
		} 

		#endregion
	} 
}