using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace moleQule.Library
{
	public static class SMSGatewayFactory
	{
		public static ISMSGateway GetGateway(SMSGatewaySettings settings)
		{
			switch ((ESMSGateway)settings.GatewayCode)
			{
				case ESMSGateway.Esendex: return new Esendex(settings);
				default: return null;
			}			
		}
	}
}
