using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Web;

using moleQule.Library;

namespace moleQule.WebFace
{
	public class Proxy
	{
		#region Lib

		[DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
		public static extern int UrlMkSetSessionOption(int dwOption, string pBuffer, int dwBufferLength, int dwReserved);

		public const int URLMON_OPTION_USERAGENT = 0x10000001;

		#endregion

		#region Properties

		public static Encoding encoding = Encoding.UTF8;

        public class TProxyResult
        {
            public EComponentStatus Status;
            public string Message;
			public HttpWebResponse Response;
			public WebExceptionStatus ExceptionStatus = WebExceptionStatus.Success;

			public HttpStatusCode ResponseStatusCode
			{
				get
				{
					if (Response != null) return Response.StatusCode;

					switch (ExceptionStatus)
					{
						case WebExceptionStatus.Success:
							return HttpStatusCode.OK;

						default:
							return HttpStatusCode.NotFound;
					}
				}
			}
        }

        public Dictionary<string, TProxyResult> Results = new Dictionary<string, TProxyResult>();

		#endregion

		#region Factory Methods

		private static Proxy _singleton = null;

		public static Proxy Instance { get { return _singleton != null ? _singleton : new Proxy(); } }

		public Proxy()
		{
			_singleton = this;
		}

		#endregion

		#region Business Methods

		public static string BuildURL(string url) { return BuildURL(SettingsMng.Instance.GetBaseUrl(), url); }
		public static string BuildURL(string host, string url) { return host + url; }

		/*public static string GetPublicIP()
		{
			HttpWebRequest request = WebRequest.Create(Properties.Settings.Default.PublicIPWebServicUrl) as HttpWebRequest;

			request.Method = "GET";

			WebResponse web_response = request.GetResponse();

			XmlDocument x_doc = new XmlDocument();
			x_doc.Load(web_response.GetResponseStream());

			XmlNodeList elemList = x_doc.GetElementsByTagName("body");

			string body = (elemList.Count > 0) ? elemList[0].InnerText : "Address:";
			string ip = body.Substring(body.IndexOf("Address:") + ("Address:").Length).Trim();

			return ip;
		}*/

		public string GetResultKey(string url) { return ClassMD5.GetMd5Hash(String.Format(url, string.Empty)); }
        public void RemoveResultKey(string url) { try { Results.Remove(GetResultKey(url)); } catch { } }
		public EComponentStatus Status(string url) { try { return Results[GetResultKey(url)].Status; } catch { return EComponentStatus.UNAVAILABLE; } }
        public string GetResultMessage(string url) { try { return Results[GetResultKey(url)].Message; } catch { return "key not found"; } }

		public TProxyResult GetResult(string url) { try { return Results[GetResultKey(url)]; } catch { return new TProxyResult { Status = EComponentStatus.UNAVAILABLE, Message = string.Empty }; } }
        public void InsertResult(string url, TProxyResult result)
        {
            try
            {
                TProxyResult res = Results[GetResultKey(url)];
                Results.Remove(GetResultKey(url));
            }
            catch { }
            finally
            {
                Results.Add(GetResultKey(url), result);
            }
        }

		public static string GetPublicIP()
		{
			HttpWebRequest request = WebRequest.Create(Properties.Settings.Default.PublicIPWebServicUrl) as HttpWebRequest;

			request.Method = "GET";

			WebResponse web_response = request.GetResponse();

			XmlDocument x_doc = new XmlDocument();
			x_doc.Load(web_response.GetResponseStream());

			XmlNodeList elemList = x_doc.GetElementsByTagName("body");

			string body = (elemList.Count > 0) ? elemList[0].InnerText : "Address:";
			string ip = body.Substring(body.IndexOf("Address:") + ("Address:").Length).Trim();

			return ip;
		}

		public static string GetSalt() { return System.Guid.NewGuid().ToString(); }
		public static string GetToken(string apiKey, string secretKey, string salt)
		{
			string token = ClassMD5.GetMd5Hash(apiKey + salt + secretKey);
			return System.Web.HttpUtility.UrlEncode(token);
		} 

        public static void FillToken(Dictionary<string, object> postParameters)
		{
			postParameters.Add("apiKey", Properties.Settings.Default.API_KEY);
			postParameters.Add("salt", GetSalt());
			postParameters.Add("token", GetToken((string)postParameters["apiKey"], Properties.Settings.Default.SECRET_KEY, (string)postParameters["salt"]));
		}

		public static string GetPHPFormatParams(Dictionary<string, object> parameters)
		{
			string paramsURL = "?";

			foreach (var param in parameters)
			{
				paramsURL += param.Key + "=" + param.Value + "&";
			}

			return paramsURL.Substring(0, paramsURL.Length - 1);
		}

		#endregion

		#region POST

		public delegate void DlgPackPOSTData(Dictionary<string, object> postParameters, Dictionary<string, object> postParametersLog);
		public delegate void DlgPackGETData(Dictionary<string, object> postParameters);
		public delegate void DlgShowMsg(string msg);

		public void POSTToSite(string url, DlgPackPOSTData packer) 
		{
            TProxyResult result = new TProxyResult
            {
				Status = EComponentStatus.WORKING,
                Message = string.Empty,
				ExceptionStatus = WebExceptionStatus.Success,
            };

			try
			{
				// Generate post objects
				Dictionary<string, object> postParameters = new Dictionary<string, object>();
				Dictionary<string, object> postParametersLog = new Dictionary<string, object>();

				packer(postParameters, postParametersLog);

				// Parameters are written to log file in JSON
				string postParameteresJson = JsonConvert.SerializeObject(postParametersLog);

				string userAgent = string.Empty; // Properties.Settings.Default.UserAgent;

				string postURL = url;

				try
				{
					/*if (Settings.Default.IsDebug)
					{
						MyLogger.LogText(postURL);
						MyLogger.LogText("POST Parameters");
						foreach (KeyValuePair<string, object> item in postParameters)
							MyLogger.LogText(item.Key + ": " + item.Value);
					}*/

					MyLogger.LogText("BEGIN DATA POST TO " + postURL, "Proxy::POSTToSite");
					result.Response = MultipartFormDataPost(postURL, userAgent, postParameters);
					MyLogger.LogText("END DATA POST", "Proxy::POSTToSite");
				}
				catch (System.Net.WebException ex)
				{
					result.Status = EComponentStatus.ERROR;
					result.Message = Resources.Errors.SERVER_CONNECTION;
					result.Response = ex.Response as HttpWebResponse;
					result.ExceptionStatus = ex.Status;

					MyLogger.LogText("ERROR IN POST: " + iQExceptionHandler.GetAllMessages(ex) + System.Environment.NewLine +
									"POST URL: " + postURL + System.Environment.NewLine +
									"POST RESPONSE: " + result.Response.ToString(),
									"Proxy::POSTToSite");

					InsertResult(url, result);

					return;
				}

				if (result.Response.StatusCode == HttpStatusCode.OK)
				{
					// Process response
					StreamReader responseReader = new StreamReader(result.Response.GetResponseStream());
					string fullResponse = responseReader.ReadToEnd();
					result.Response.Close();

					try
					{
						Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();
						Newtonsoft.Json.JsonReader r = new Newtonsoft.Json.JsonTextReader(new StringReader(fullResponse));
						Newtonsoft.Json.Linq.JObject res = ser.Deserialize(r) as Newtonsoft.Json.Linq.JObject;
						StringBuilder sb = new StringBuilder();						

						foreach (KeyValuePair<string, JToken> kvp in res)
						{
							switch (kvp.Key)
							{
								case "message":
									{
										result.Status = (kvp.Value.ToString() == "OK") ? EComponentStatus.OK : EComponentStatus.ERROR;
										sb.AppendLine((kvp.Value != null ? (kvp.Value.HasValues ? kvp.Value.First.ToString() : kvp.Value.ToString()) : ""));
									}
									break;

								default:
									sb.AppendLine(kvp.Value != null ? (kvp.Value.HasValues ? kvp.Value.First.ToString() : kvp.Value.ToString()) : "");
									break;
							}
						}
                        result.Message = sb.ToString();
					}
					catch
					{
						MyLogger.LogText("JSON DESERIALIZE ERROR", "Proxy::POSTToSite");
						MyLogger.LogText("FULL RESPONSE: " + fullResponse, "Proxy::POSTToSite");

                        result.Message = fullResponse;
					}
				}
				else
				{
					MyLogger.LogText("HTTP REQUEST STATUS ERROR.", "Proxy::POSTToSite");
					MyLogger.LogText("HTTP URL: " + url, "Proxy::POSTToSite");
				}
			}
			catch (Exception ex)
			{
				MyLogger.LogText("IN POSTToSite: " + ex.Message, "Proxy::POSTToSite");
				result.Status = EComponentStatus.ERROR;
                result.Message = ex.Message;
			}
			finally
			{
                InsertResult(url, result);
			}
		}
		
		public static HttpWebResponse MultipartFormDataPost(string postUrl, string userAgent, Dictionary<string, object> postParameters)
		{
			string formDataBoundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
			//string contentType = "multipart/form-data; boundary=" + formDataBoundary;
			string contentType = "multipart/form-data; charset=UTF-8; boundary=" + formDataBoundary;

			byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

			// Do not activate in production. Very heavy process with image data
			//MyLogger.LogText(ELogLevel.INFO, "FORM DATA " + System.Text.Encoding.UTF8.GetString(formData));

			return PostForm(postUrl, userAgent, contentType, formData);
		}

		private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData)
		{
			HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;
			request.AllowAutoRedirect = false;
			//request.Headers.Add("Accept-Language:" + System.Threading.Thread.CurrentThread.CurrentCulture.Name);
			request.Headers.Add("Accept-Language:" + "en");

			if (request == null)
			{
				throw new NullReferenceException("request is not a http request");
			}

			request.Method = "POST";
			request.ContentType = contentType;
			request.UserAgent = userAgent;
			request.CookieContainer = new CookieContainer();

			request.ContentLength = formData.Length;
			request.Timeout = 300000;
			request.KeepAlive = true;
			request.ReadWriteTimeout = 1000000;
			request.Credentials = System.Net.CredentialCache.DefaultCredentials;

			using (Stream requestStream = request.GetRequestStream())
			{
				requestStream.Write(formData, 0, formData.Length);
				requestStream.Close();
			}

			/*if (Properties.Settings.Default.IsDebug)
			{
				MyLogger.LogText("HttpWebResponse::PostForm POST URL: " + postUrl);
				MyLogger.LogText("HttpWebResponse::PostForm FORM DATA: " + request.GetRequestStream().ToString());
			}*/

			return request.GetResponse() as HttpWebResponse;
		}

		private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
		{
			string realBoundary = "--" + boundary;
			Stream formDataStream = new System.IO.MemoryStream();
			formDataStream.Write(System.Text.Encoding.UTF8.GetBytes(realBoundary), 0, realBoundary.Length);
			foreach (var param in postParameters)
			{
				if (param.Value is byte[])
				{
					/*byte[] fileData = ImageLib.CreateTumbnailImageFromByteArray(param.Value as byte[], Properties.Settings.Default.ImageWidth);

					// Add just the first part of this param, since we will write the file data directly to the Stream
					string header = string.Format("\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{0}.png\"; \r\nContent-Type: application/octet-stream\r\n\r\n", param.Key);
					formDataStream.Write(System.Text.Encoding.UTF8.GetBytes(header), 0, header.Length);
					formDataStream.Write(fileData, 0, fileData.Length);

					formDataStream.Write(encoding.GetBytes("\r\n"), 0, "\r\n".Length);
					formDataStream.Write(System.Text.Encoding.UTF8.GetBytes(realBoundary), 0, realBoundary.Length);*/
				}
				else
				{
					string postData = string.Format("\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n", param.Key, param.Value);
					byte[] encodedData = encoding.GetBytes(postData);
					formDataStream.Write(encodedData, 0, encodedData.Length);
					formDataStream.Write(System.Text.Encoding.UTF8.GetBytes(realBoundary), 0, realBoundary.Length);
				}
			}

			// Dump the Stream into a byte[]
			formDataStream.Position = 0;
			byte[] formData = new byte[formDataStream.Length];
			formDataStream.Read(formData, 0, formData.Length);
			formDataStream.Close();

			return formData;
		}

		#endregion
	}
}
