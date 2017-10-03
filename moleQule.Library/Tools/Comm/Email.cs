using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Resources;
using System.Web;
using System.Net.Mail;

namespace moleQule.Library
{
	public struct MailParams
	{
		public string To { get; set; }
		public string Cc { get; set; }
		public string Bcc { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string AttachmentPath { get; set; }
	}

    public static class EMailSender
	{
		public static void MailTo(MailParams mail)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Uri.UriSchemeMailto + ":" + HttpUtility.UrlEncode(mail.To));

			stringBuilder.Append("?subject=" + HttpUtility.UrlEncode(mail.Subject));

			if (!string.IsNullOrEmpty(mail.Body))
			{
				stringBuilder.Append("&body=" + HttpUtility.UrlEncode(mail.Body));
			}

			if (!string.IsNullOrEmpty(mail.Cc))
			{
				stringBuilder.Append("&CC=" + HttpUtility.UrlEncode(mail.Cc));
			}

			if (!string.IsNullOrEmpty(mail.Bcc))
			{
				stringBuilder.Append("&BCC=" + HttpUtility.UrlEncode(mail.Bcc));
			}

			if (!string.IsNullOrEmpty(mail.AttachmentPath))
			{
				stringBuilder.Append("&Attach=" + HttpUtility.UrlEncode(mail.AttachmentPath));
			}

			Process process = new Process();
			process.StartInfo = new ProcessStartInfo(stringBuilder.ToString());
			process.Start();
		}

		/// <summary>
		/// Formats the mailto argument. Converts <![CDATA['%', '&', ' ', '?', '\t', '\n']]> to their 
		/// hexadecimal representation.
		/// </summary>
		/// <param name="argument">The argument.</param>
		/// <returns>The formatted argument.</returns>
		public static string FormatMailToArgument(string argument)
		{
			return argument.
				Replace("%", "%25").
				Replace("&", "%26").
				Replace(":", "%3A").
				Replace("\t", "%0D").
				Replace(Environment.NewLine, "%0A").
				Replace("?", "%3F").
				Replace("\"","%22").
				Replace(" ", "%20").
				Replace(".", "").
				Replace("ñ", "");
		}
    }

	public class EMailClient
	{
		SmtpClient _smtp_client;
		
		public SmtpClient SmtpCliente { get { return _smtp_client; } }

        protected static EMailClient _main;

        public static EMailClient Instance { get { return (_main != null) ? _main : new EMailClient(); } }
        
		public EMailClient()
		{
			// Singleton
            _main = this;

			LoadSMTPConfig();
		}

		public void LoadSMTPConfig()
		{
			if (_smtp_client == null) _smtp_client = new SmtpClient();

			if (SettingsMng.Instance.GetSMTPHost() != string.Empty) _smtp_client.Host = SettingsMng.Instance.GetSMTPHost();

			if (SettingsMng.Instance.GetSMTPPort() != 0) _smtp_client.Port = SettingsMng.Instance.GetSMTPPort();
			
			_smtp_client.UseDefaultCredentials = false;

			if ((SettingsMng.Instance.GetSMTPHost() != string.Empty) && (SettingsMng.Instance.GetSMTPPwd() != string.Empty))
				_smtp_client.Credentials = new System.Net.NetworkCredential(SettingsMng.Instance.GetSMTPUser(), SettingsMng.Instance.GetSMTPPwd());

			_smtp_client.EnableSsl = SettingsMng.Instance.GetSMTPEnableSSL();
			
			_smtp_client.DeliveryMethod = SmtpDeliveryMethod.Network;
		}
	}

	public class EMailSchemaClient
	{
		SmtpClient _smtp_client;
		
		public SmtpClient SmtpCliente { get { return _smtp_client; } }

        protected static EMailSchemaClient _main;

        public static EMailSchemaClient Instance { get { return (_main != null) ? _main : new EMailSchemaClient(); } }

        public EMailSchemaClient()
		{
			// Singleton
            _main = this;

			LoadSMTPConfig();
		}

		public void LoadSMTPConfig()
		{
			if (_smtp_client == null) _smtp_client = new SmtpClient();

			if (SettingsMng.Instance.GetSchemaSMTPHost() != string.Empty) _smtp_client.Host = SettingsMng.Instance.GetSchemaSMTPHost();

			if (SettingsMng.Instance.GetSchemaSMTPPort() != 0) _smtp_client.Port = SettingsMng.Instance.GetSchemaSMTPPort();
			
			_smtp_client.UseDefaultCredentials = false;

			if ((SettingsMng.Instance.GetSchemaSMTPHost() != string.Empty) && (SettingsMng.Instance.GetSchemaSMTPPwd() != string.Empty))
				_smtp_client.Credentials = new System.Net.NetworkCredential(SettingsMng.Instance.GetSchemaSMTPUser(), SettingsMng.Instance.GetSchemaSMTPPwd());

			_smtp_client.EnableSsl = SettingsMng.Instance.GetSchemaSMTPEnableSSL();
			
			_smtp_client.DeliveryMethod = SmtpDeliveryMethod.Network;
		}
	}
}
