using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Collections;

namespace moleQule.Library
{
    public enum ESMSGatewayResult { OK = 0, ERROR = 1 }

	public struct SMSParams
	{
		public string Phone { get; set; }
		public string Text { get; set; }
	}

    public interface ISMSGatewayResult
    {
		ESMSGatewayResult Status { get; set; }
        string Message { get; set; }
    }
	
    public interface ISMSGateway
	{
        SMSGatewaySettings Settings { get; set; }

		ISMSGatewayResult Result { get; set; }

		ESMSGatewayResult Send(SMSParams source);
	}
}
