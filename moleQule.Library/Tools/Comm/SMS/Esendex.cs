using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Resources;
using System.Web;
using System.Net.Mail;

namespace moleQule.Library
{
	public class EsendexResult : ISMSGatewayResult
	{
		public ESMSGatewayResult Status { get; set; }
		public string Message { get; set; }
	}

	public class Esendex : ISMSGateway
	{
		#region Attributes

		protected ISMSGatewayResult _result = new EsendexResult();

		#endregion

		#region Properties

        public SMSGatewaySettings Settings { get; set; }
        public ISMSGatewayResult Result { get { return _result; } set { _result = value; } }

		#endregion

        #region Factory Methods

        public Esendex(SMSGatewaySettings settings)
		{
			Settings = settings;
		}

        #endregion
        
		#region Business Methods

		public ESMSGatewayResult Send(SMSParams source)
		{
			Dictionary<object, object> requestParams = new Dictionary<object, object>()
			{
                { "Phone", source.Phone },
				{ "Text",  source.Text },
			};

			Dictionary<object, object> response = EsendexProxy.Send(requestParams);

			Result.Status = ((int)response["RESULT"] == (int)ESMSGatewayResult.OK)
											? ESMSGatewayResult.OK
											: ESMSGatewayResult.ERROR;

			Result.Message = (Result.Status == ESMSGatewayResult.OK)
								? Resources.Messages.SMS_SENT_OK
								: String.Format(Resources.Messages.SMS_SENT_ERROR, (string)response["ERRORMESSAGE"]);

			return Result.Status;
		}

		#endregion
	}
}
