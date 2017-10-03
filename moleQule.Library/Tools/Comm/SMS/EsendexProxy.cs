using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Xml;
using System.Text;
using System.Net;

using moleQule.Library;
//using moleQule.Library.App.TrakaIntegration_WS;

using com.esendex.sdk.core;
using com.esendex.sdk.messaging;
using com.esendex.sdk;

namespace moleQule.Library
{
	public class EsendexProxy
	{
		const string LOG_PREFIX = "ESSENDEXPROXY";

		protected System.Web.Script.Serialization.JavaScriptSerializer _ser = new System.Web.Script.Serialization.JavaScriptSerializer();

		protected static Dictionary<string, string> ParseNode(XmlNode[] nodes)
		{
			Dictionary<string, string> dict = new Dictionary<string, string>();

			foreach (XmlNode node in nodes)
			{
				if (node is XmlElement)
				{
					XmlElement el = node as XmlElement;
					string k = null;
					string v = null;
					foreach (XmlNode n in el.ChildNodes)
					{
						if (n.LocalName == "Key")
							k = n.InnerText;
						else if (n.LocalName == "Value")
							v = n.InnerText;
					}

					if (k != null)
					{
						dict[k] = v;
					}
				}
			}
			return dict;
		}

        #region API

		public static Dictionary<object, object> Send(Dictionary<object, object> requestParams) 
		{
            // TODO: get credentials from config settings
            EsendexCredentials credentials = new EsendexCredentials("soporte@iqingenieros.com", "XAvHA7Tdf433");
            
            // TODO: get accountReference EX0111208 from config settings
            SmsMessage message = new SmsMessage((string)requestParams["Phone"], (string)requestParams["Text"], "EX0111208");
            
            MessagingService messagingService = new MessagingService(true, credentials);
            try
            {
                MessagingResult result = messagingService.SendMessage(message);

                /*Console.WriteLine("Sent Message Batch Id: {0}", result.BatchId);

                foreach (ResourceLink messageId in result.MessageIds)
                {
                    Console.WriteLine("Message Uri: {0}", messageId.Uri);
                }*/

                return new Dictionary<object, object>() 
                    {  
				        { "RESULT", ESMSGatewayResult.OK },
				        { "ERROR", string.Empty },
				        { "ERRORMESSAGE", string.Empty }
			        };
            }
            catch (WebException ex)
            {
			    return new Dictionary<object, object>() 
                    {  
				        { "RESULT", ESMSGatewayResult.ERROR },
				        { "ERROR", "ERROR" },
				        { "ERRORMESSAGE", ex.Message }
			        };
            }
		}

        #endregion
    }   
}