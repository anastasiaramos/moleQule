using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Elmah;

using moleQule.Library;

namespace moleQule.WebFace.Helpers
{
	public class molLogger
	{
		/// <summary>
		/// Log error to Elmah
		/// </summary>
		public static void LogError(string contextualMessage) { LogError(null, contextualMessage); }
		public static void LogError(Exception ex, string contextualMessage = null) { LogError(ex, contextualMessage, iQExceptionCode.NO_CODE); }
		public static void LogError(Exception ex, string contextualMessage, iQExceptionCode exCode = iQExceptionCode.NO_CODE)
		{
			try
			{
				// log error to Elmah
				if (contextualMessage != null)
				{
					// log exception with contextual information that's visible when 
					// clicking on the error in the Elmah log
					var annotatedException = new Exception(contextualMessage, iQExceptionHandler.ConvertToiQException(ex, exCode));
					ErrorSignal.FromCurrentContext().Raise(annotatedException, HttpContext.Current);
				}
				else
				{
					ErrorSignal.FromCurrentContext().Raise(iQExceptionHandler.ConvertToiQException(ex, exCode), HttpContext.Current);
				}

				// send errors to ErrorWS (my own legacy service)
				// using (ErrorWSSoapClient client = new ErrorWSSoapClient())
				// {
				//    client.LogErrors(...);
				// }
			}
			catch (Exception)
			{
				// uh oh! just keep going
			}
		}

		public static void LogText(string contextualMessage)
		{
			try
			{
				// log exception with contextual information that's visible when 
				// clicking on the error in the Elmah log
				var annotatedException = new iQException(contextualMessage, iQExceptionCode.INFO);
				ErrorSignal.FromCurrentContext().Raise(annotatedException, HttpContext.Current);
			}
			catch (Exception)
			{
				// uh oh! just keep going
			}
		}
		
		public static string PrintSession(HttpSessionStateBase session)
		{
			string sessionContent = "SESSION ID: " + session.SessionID;
			for (int i = 0; i < session.Count; i++)
			{
				var crntSession = session.Keys[i];
				sessionContent += Environment.NewLine + string.Concat(crntSession, "=", session[crntSession]);
			}

			return sessionContent;
		}
		public static string PrintSession(HttpSessionState session)
		{
			return PrintSession(new HttpSessionStateWrapper(session));
		}
	}
}
