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
using System.Windows.Forms;

namespace moleQule.Library
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

		public static void ChangeUserAgent(EBrowser browser)
		{
			string userAgent = string.Empty;

			switch (browser)
			{
				case EBrowser.Chrome: userAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/ (KHTML, like Gecko) Chrome/"; break;
				case EBrowser.Firefox12: userAgent = "Mozilla/5.0 (Windows NT 6.1; rv:12.0) Gecko/ Firefox/)"; break;
				case EBrowser.IE8: userAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)"; break;
				case EBrowser.IE9: userAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)"; break;
				case EBrowser.IE10: userAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)"; break;
			}

			UrlMkSetSessionOption(URLMON_OPTION_USERAGENT, userAgent, userAgent.Length, 0);
		}

		public static string GetUserAgent(EBrowser browser)
		{
			string userAgent = string.Empty;

			switch (browser)
			{
				case EBrowser.Chrome: return "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/ (KHTML, like Gecko) Chrome/";
				case EBrowser.Firefox12: return "Mozilla/5.0 (Windows NT 6.1; rv:12.0) Gecko/ Firefox/)";
				case EBrowser.IE8: return "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)";
				case EBrowser.IE9: return "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
				case EBrowser.IE10: return "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
				default: return "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/ (KHTML, like Gecko) Chrome/";
			}
		}

		#endregion

		#region Business Methods

		public static string BuildURL(string url) { return BuildURL(SettingsMng.GetBaseDomain(), url); }
		public static string BuildURL(string host, string url) { return host + url; }

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

		public static byte[] WritePicToByteArray(System.Drawing.Bitmap image)
		{
			byte[] data = null;
			if (image != null)
			{
				MemoryStream ms = new MemoryStream();
				image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				data = ms.GetBuffer();
			}

			return data;
		}

		#endregion

		#region POST

		public delegate void DlgPackPOSTData(Dictionary<string, object> postParameters, Dictionary<string, object> postParametersLog);
		public delegate void DlgPackGETData(Dictionary<string, object> postParameters);
		public delegate void DlgShowMsg(string msg);

		public void POSTToSite(Control control, string url, Delegate packer)
		{
            TProxyResult result = new TProxyResult
            {
				Status = EComponentStatus.WORKING,
                Message = string.Empty,
				Response = null,
				ExceptionStatus = WebExceptionStatus.Success,
            };

			try
			{
				// Generate post objects
				Dictionary<string, object> postParameters = new Dictionary<string, object>();
				Dictionary<string, object> postParametersLog = new Dictionary<string, object>();

				control.Invoke(packer, new object[2] { postParameters, postParametersLog });

				string postURL = url;
				string userAgent = Properties.Settings.Default.UserAgent;

				// Parameters are written to log file in JSON
				string postParameteresJson = JsonConvert.SerializeObject(postParametersLog);

				if (!Properties.Settings.Default.IsDebug)
					MyLogger.LogText("NEW POST. " + postParameteresJson, "Proxy::POSTToSite");
				else
					MyLogger.LogText("NEW DEBUG POST. " + postParameteresJson, "Proxy::POSTToSite");

				try
				{
					if (Settings.Default.IsDebug)
					{
						MyLogger.LogText(postURL);
						foreach (KeyValuePair<string, object> item in postParameters)
							MyLogger.LogText(item.Key + ": " + item.Value);
					}

					MyLogger.LogText("BEGIN DATA POST TO " + postURL, "Proxy::POSTToSite");
					result.Response = MultipartFormDataPost(postURL, userAgent, postParameters);
					MyLogger.LogText("END DATA POST", "Proxy::POSTToSite");
				}
				catch (System.Net.WebException ex)
				{
					result.Status = EComponentStatus.ERROR;
					result.Message = String.Format(Resources.Messages.SERVER_CONNECTION_ERROR, ex.Message);
					result.Response = ex.Response as HttpWebResponse;
					result.ExceptionStatus = ex.Status;

					MyLogger.LogText("ERROR IN POST: " + ex.Message + System.Environment.NewLine +
									"POST URL: " + postURL
									,"Proxy::POSTToSite");

					InsertResult(url, result);

					return;
				}

				if (result.Response.StatusCode == HttpStatusCode.OK)
				{
					MyLogger.LogText("HTTP STATUS OK: " + result.Message, "Proxy::POSTToSite");

					result.Status = EComponentStatus.ERROR;

					// Process response
					StreamReader responseReader = new StreamReader(result.Response.GetResponseStream());
					string fullResponse = responseReader.ReadToEnd();
					result.Response.Close();

					MyLogger.LogText("FULL RESPONSE: " + fullResponse, "Proxy::POSTToSite");

					try
					{
						MyLogger.LogText("JSON DESERIALIZE BEGIN", "Proxy::POSTToSite");

						Newtonsoft.Json.JsonSerializer ser = new Newtonsoft.Json.JsonSerializer();
						Newtonsoft.Json.JsonReader r = new Newtonsoft.Json.JsonTextReader(new StringReader(fullResponse));
						Newtonsoft.Json.Linq.JObject res = ser.Deserialize(r) as Newtonsoft.Json.Linq.JObject;
						StringBuilder sb = new StringBuilder();
						sb.AppendLine("Server response");
						foreach (KeyValuePair<string, JToken> kvp in res)
						{
							sb.AppendLine(string.Format("{0} - {1}", kvp.Key, kvp.Value != null ? (kvp.Value.HasValues ? kvp.Value.First.ToString() : kvp.Value.ToString()) : ""));
						}
						
                        result.Message = sb.ToString();

						MyLogger.LogText("JSON DESERIALIZE END. RESULT: " + result.Message, "Proxy::POSTToSite");
						InsertResult(url, result);
						return;
					}
					catch
					{
						MyLogger.LogText("JSON DESERIALIZE ERROR", "Proxy::POSTToSite");
                        result.Message = fullResponse;
						InsertResult(url, result);
						return;
					}
				}
				else
				{
					result.Status = EComponentStatus.OK;
					MyLogger.LogText("NO RESPONSE ERROR. USER REGISTERED. REDIRECTION.", "Proxy::POSTToSite");
					InsertResult(url, result);
					return;
				}
			}
			catch (Exception ex)
			{
				MyLogger.LogText("IN POSTToSite: " + ex.Message, "Proxy::POSTToSite");
				result.Status = EComponentStatus.ERROR;
				result.Message = ex.Message;
				InsertResult(url, result);
			}
		}
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

				string userAgent = Properties.Settings.Default.UserAgent;

				string postURL = url;

				try
				{
					if (Settings.Default.IsDebug)
					{
						MyLogger.LogText(postURL);
						MyLogger.LogText("POST Parameters");
						foreach (KeyValuePair<string, object> item in postParameters)
							MyLogger.LogText(item.Key + ": " + item.Value);
					}

					MyLogger.LogText("BEGIN DATA POST TO " + postURL, "Proxy::POSTToSite");
					result.Response = MultipartFormDataPost(postURL, userAgent, postParameters);
					MyLogger.LogText("END DATA POST", "Proxy::POSTToSite");
				}
				catch (System.Net.WebException ex)
				{
					result.Status = EComponentStatus.ERROR;
					result.Message = Resources.Messages.SERVER_CONNECTION_ERROR;
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

			if (Properties.Settings.Default.IsDebug)
			{
				MyLogger.LogText("HttpWebResponse::PostForm POST URL: " + postUrl);
				MyLogger.LogText("HttpWebResponse::PostForm FORM DATA: " + request.GetRequestStream().ToString());
			}

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

		#region GET

		public void GETToSite(Control control, WebBrowser webBrowser, string url) { GETToSite(control, webBrowser, url, null); }
		public void GETToSite(Control control, WebBrowser webBrowser, string url, Delegate packer)
		{
            TProxyResult result = new TProxyResult
            {
				Status = EComponentStatus.WORKING,
                Message = string.Empty,
            };

			try
			{
				// Generate post objects
				Dictionary<string, object> postParameters = new Dictionary<string, object>();
				Dictionary<string, object> postParametersLog = new Dictionary<string, object>();

				if (packer != null) control.Invoke(packer, postParameters);

				// Parameters are written to log file in JSON
				string postParameteresJson = JsonConvert.SerializeObject(postParametersLog);

				//string postUrl = HttpUtility.UrlEncode(url + GetPHPFormatParams(postParameters));
				string getUrl = url + GetPHPFormatParams(postParameters);

				if (!Properties.Settings.Default.IsDebug)
					MyLogger.LogText("NEW GET TO: " + getUrl, "Proxy:: GETToSite");
				else
					MyLogger.LogText("NEW DEBUG GET TO : " + getUrl, "Proxy:: GETToSite");

				try
				{
					if (Settings.Default.IsDebug) MyLogger.LogText(getUrl);

					MyLogger.LogText("BEGIN DATA GET", "Proxy:: GETToSite");
                    webBrowser.Navigate(getUrl,null,null, "User-Agent: " + GetUserAgent(EBrowser.Chrome));
					MyLogger.LogText("END DATA GET", "Proxy:: GETToSite");
				}
				catch (Exception ex)
				{
					result.Status = EComponentStatus.ERROR;
                    result.Message = String.Format(Resources.Messages.SERVER_CONNECTION_ERROR, ex.Message);

					MyLogger.LogText("ERROR IN GET: " + result.Message, "Proxy:: GETToSite");

					return;
				}

				result.Status = EComponentStatus.OK;
			}
			catch (Exception ex)
			{
				MyLogger.LogText("IN GETToSite" + ex.Message, "Proxy:: GETToSite");

				result.Status = EComponentStatus.ERROR;
                result.Message = ex.Message;
			}
			finally
			{
                InsertResult(GetResultKey(url), result);
			}
		}

		#endregion
	}
}
