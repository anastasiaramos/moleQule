using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

using moleQule.Library;

namespace moleQule.WebFace.Helpers
{
	public class PostCaller
	{
		public static Encoding encoding = Encoding.UTF8;

		public static HttpWebResponse PostForm(string postUrl, string userAgent, Dictionary<string, object> postParameters)
		{
			HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;

			if (request == null)
				throw new NullReferenceException(string.Format("Request to {0} is not a http request", postUrl));

			byte[] formData = GetPostDataByte(postParameters);

			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.AllowAutoRedirect = false;
			request.UserAgent = userAgent;		
			request.ContentLength = formData.Length;
			request.Timeout = 45000;
			//request.Credentials = System.Net.CredentialCache.DefaultCredentials;

			StreamReader str_reader = new StreamReader(new MemoryStream(formData));
			string msg = String.Format("PostCaller::Post POST URL: {0}", postUrl) + System.Environment.NewLine +
						String.Format("PostCaller::Post FORM DATA: {0}", str_reader.ReadToEnd());
			molLogger.LogText(msg);
			str_reader.Close();

			using (Stream requestStream = request.GetRequestStream())
			{
				requestStream.Write(formData, 0, formData.Length);
				requestStream.Close();
			}

			return request.GetResponse() as HttpWebResponse;
		}

		public static byte[] GetPostDataByte(Dictionary<string, object> postParameters)
		{
			StringBuilder str_builder = new StringBuilder();
			foreach (KeyValuePair<string, object> param in postParameters)
				str_builder.Append(String.Format("{0}={1}&", param.Key,System.Web.HttpUtility.UrlEncode((string)param.Value)));

			str_builder.Length = str_builder.Length-1;

			// Dump the data into a byte[]
			byte[] postParams = Encoding.UTF8.GetBytes(str_builder.ToString());

			return postParams;
		}

		public static StringBuilder GetPostDataString(Dictionary<string, object> postParameters)
		{
			StringBuilder str_builder = new StringBuilder();
			foreach (KeyValuePair<string, object> param in postParameters)
				str_builder.Append(String.Format("&{0}={1}", param.Key, System.Web.HttpUtility.UrlEncode((string)param.Value)));

			// Dump the data into a string
			return str_builder;
		}
	}
}
