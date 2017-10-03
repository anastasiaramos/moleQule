using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace moleQule.Library
{
    public static class MyLogger
    {
		#region Structs

		public struct TError
		{
			public EErrorType ErrorType;
			public EErrorLevel ErrorLevel;
			public string ErrorDesc;
			public EComponentStatus Status;			

			public string ItemName;
			public EComponentType ItemType;
			public EComponentStatus ItemStatus;
			public string ItemSerial;
			public string ItemIP;
			public DateTime LastUpdate;
			public int ErrorCount;
		}

		#endregion

		private static string GetLogHome()
		{
			try
			{
				return Path.Combine(AppContext.StartUpPath,System.Configuration.ConfigurationManager.AppSettings["LogHome"]);
			}
			catch 
			{
				return Path.Combine(AppContext.StartUpPath, "log");
			}
		}

		private static string GetLogFilename(string prefix = null)
        {
			prefix = prefix ?? System.Configuration.ConfigurationManager.AppSettings["LogPrefix"];
			return string.Format("{0}\\{1}_{2}.log", GetLogHome(), prefix, DateTime.Now.ToString("yyyyMMdd"));
        }

        public static bool IsLogEnabled()
        {
            string s = System.Configuration.ConfigurationManager.AppSettings["LogEnabled"];
            bool result = false;
            bool.TryParse(s, out result);
            return result;
        }

		public static void LogStart() 
		{
			string home = GetLogHome();

			if (!Directory.Exists(home)) Directory.CreateDirectory(home);

			if (!IsLogEnabled()) return;

			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.AppendFormat("\r\n{0} \r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFFF") + " " + String.Format(Resources.Messages.LOG_STARTED, string.Empty));
			sb.AppendLine();
			string fn = GetLogFilename();
			System.IO.File.AppendAllText(fn, sb.ToString());
		}
		public static void LogStart(string msg)
		{
			LogStart(msg, string.Empty);
		}
		public static void LogStart(string msg, string label)
		{
			LogStart();
			LogText(msg, label);
		}

		public static void LogException(Exception ex, string label, string prefix = null)
		{
			LogText(iQExceptionHandler.GetAllMessages(ex, true), label);
		}
		public static void LogException(Exception ex, string label, TError error, string prefix = null)
		{
			LogText(iQExceptionHandler.GetAllMessages(ex, true), label, error, prefix);
		}

		public static void LogText(string msg, string label, TError error, string prefix = null)
		{
			LogText(msg, label, prefix);

			/*switch (error.ErrorLevel)
			{ 
				case EErrorLevel.CRITICAL:
					SendMail(error);
					break;					
			}*/
		}
		public static void LogText(string msg, string label = null, string prefix = null)
		{
			try
			{
				if (!IsLogEnabled()) return;

				msg = String.Format("{0}: {1}", label ?? string.Empty, msg);

				System.Text.StringBuilder sb = new System.Text.StringBuilder();
				sb.AppendFormat("{0} ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.FFFF"));
				sb.Append(msg);
				sb.AppendLine();
				string fn = GetLogFilename(prefix);
				System.IO.File.AppendAllText(fn, sb.ToString());
			}
			catch { }
		}

		public static string GetDetails(DataRow row)
		{
			StringBuilder sb = new StringBuilder();
			System.IO.StringWriter sw = new System.IO.StringWriter(sb);
			row.Table.WriteXml(sw);
			string data = sb.ToString();
			return string.Format("Row Data: {0}", data);
		}

		/*public static bool SendMail(TError error)
		{
			string itemData =
				"<body><table>" +
				"<tr><td>DATE:</td><td>" + DateTime.Now + "</td></tr>" +
				"<tr><td>INCIDENCE STATUS:</td><td>" + EnumText<EItemStatus>.GetLabel(error.Status) + "</td></tr>" +
				"<tr><td>ERROR LEVEL:</td><td>" + EnumText<EErrorLevel>.GetLabel(error.ErrorLevel) + "</td></tr>" +
				"<tr><td>ITEM NAME:</td><td>" + error.ItemName + "</td></tr>" +
				"<tr><td>ITEM TYPE:</td><td>" + EnumText<EErrorLevel>.GetLabel(error.ItemType) + "</td></tr>" +
				"<tr><td>ITEM SERIAL:</td><td>" + error.ItemSerial + "</td></tr>" +
				"<tr><td>ITEM IP:</td><td>" + error.ItemIP + "</td></tr>" +
				"<tr><td>ITEM STATUS:</td><td>" + EnumText<EItemStatus>.GetLabel(error.ItemStatus) + "</td></tr>" +
				"<tr><td>ERROR DESCRIPTION:</td><td>" + error.ErrorDesc.Replace(Environment.NewLine, "<br/>") + "</td></tr>" +
				"<tr><td>LAST UPDATE:</td><td>" + error.LastUpdate.ToString() + "</td></tr>" +
				"<tr><td>ERRORS COUNT:</td><td>" + error.ErrorCount + "</td></tr>" +
				"</table></body>";

			MailMessage mail = new System.Net.Mail.MailMessage();

			switch (error.ErrorType)
			{
				case EErrorType.ADMIN:

					foreach (string receiver in SettingsMng.GetReportAdminEmail().Split(';'))
					{
						if (receiver == string.Empty) continue;
						mail.To.Add(new MailAddress(receiver, string.Empty));
					}

					foreach (string receiver in SettingsMng.GetReportSupportEmail().Split(';'))
					{
						if (receiver == string.Empty) continue;
						mail.To.Add(new MailAddress(receiver, string.Empty));
					}

					break;

				case EErrorType.SUPPORT:

					foreach (string receiver in SettingsMng.GetReportSupportEmail().Split(';'))
					{
						if (receiver == string.Empty) continue;
						mail.To.Add(new MailAddress(receiver, string.Empty));
					}

					break;

				default:

					foreach (string receiver in SettingsMng.GetReportSupportEmail().Split(';'))
					{
						if (receiver == string.Empty) continue;
						mail.To.Add(new MailAddress(receiver, string.Empty));
					}

					break;
			}

			mail.From = new MailAddress(Tools.SettingsMng.GetSMTPSender(), "ARGOS STATUS MONITOR");
			mail.Subject = String.Format(Properties.Settings.Default.MAIL_SUBJECT, EnumText<EErrorLevel>.GetLabel(error.ErrorLevel));
			mail.IsBodyHtml = true;
			mail.Body = String.Format(Resources.Messages.REPORT_EMAIL_BODY, itemData);

			try
			{
				EMailClient.Instance.SmtpCliente.Send(mail);
				return true;
			}
			catch (Exception ex)
			{
				moleQule.Tools.MyLogger.LogException(ex, "MyLogger::SendMail");
				moleQule.Tools.MyLogger.LogText(itemData, "MyLogger::SendMail");
				return false;
			}
		}*/
    }
}