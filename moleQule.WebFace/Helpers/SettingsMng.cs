using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

using moleQule.Library;
using moleQule.WebFace.Properties;

namespace moleQule.WebFace
{
	public class SettingsMng
	{
		#region Application Settings

		const int ENVIRONMENT = 10;
		const int REPORT_ADMIN_EMAIL = 12;
		const int REPORT_SUPPORT_EMAIL = 13;
		const int REPORT_DEBUG_EMAIL = 14;
		const int BASE_PROTOCOL = 15;
		const int BASE_DOMAIN = 16;

		protected Dictionary<int, object> _temp_settings = new Dictionary<int, object>();

		#endregion

		#region Factory Methods

		protected static SettingsMng _main;

		public static SettingsMng Instance { get { return (_main != null) ? _main : new SettingsMng(); } }

		public SettingsMng()
		{
			_main = this;

			_temp_settings.Add(ENVIRONMENT, EEnvironment.Release);
			_temp_settings.Add(BASE_DOMAIN, string.Empty);
			_temp_settings.Add(BASE_PROTOCOL, string.Empty);

			_temp_settings.Add(REPORT_ADMIN_EMAIL, Settings.Default.REPORT_ADMIN_EMAIL);
			_temp_settings.Add(REPORT_SUPPORT_EMAIL, Settings.Default.REPORT_SUPPORT_EMAIL);
			_temp_settings.Add(REPORT_DEBUG_EMAIL, Settings.Default.REPORT_DEBUG_EMAIL);
		}

		public void SetBaseDomain(string value) { _temp_settings[BASE_DOMAIN] = value; }
		public void SetBaseProtocol(string value) { _temp_settings[BASE_PROTOCOL] = value; }
		public void SetEnvironment(EEnvironment value) { _temp_settings[ENVIRONMENT] = value; }

		public void SetAdminEmail(string value) { _temp_settings[REPORT_ADMIN_EMAIL] = value; }
		public void SetDebugEmail(string value) { _temp_settings[REPORT_DEBUG_EMAIL] = value; }
		public void SetSupportEmail(string value) { _temp_settings[REPORT_SUPPORT_EMAIL] = value; }

		#endregion

		#region Business Methods

		public string GetBaseUrl() { return (string)_temp_settings[BASE_PROTOCOL] + (string)_temp_settings[BASE_DOMAIN]; }
		public string GetApiBaseUrl() 
		{
			switch (GetEnvironment())
			{
				case EEnvironment.Release:
					string url = ((string)_temp_settings[BASE_DOMAIN]);
					if (url.Contains("www.")) url = url.Substring(url.IndexOf("www.") + 4);
					return (string)_temp_settings[BASE_PROTOCOL] + (string)Settings.Default.API_BASE_SUBDOMAIN + "." + url;
			
				default:
					return (string)_temp_settings[BASE_PROTOCOL] + (string)Settings.Default.API_BASE_SUBDOMAIN + "." + (string)_temp_settings[BASE_DOMAIN];
			}
		}
		public EEnvironment GetEnvironment() { return (EEnvironment)_temp_settings[ENVIRONMENT]; }

		public string GetAdminEmail()
		{
			switch (GetEnvironment())
			{
				case EEnvironment.Release:
				case EEnvironment.Staging:
					return (string)_temp_settings[REPORT_ADMIN_EMAIL];

				default:
					return (string)_temp_settings[REPORT_DEBUG_EMAIL];
			}
		}
		public string GetDebugEmail() { return (string)_temp_settings[REPORT_DEBUG_EMAIL]; }
		public string GetSupportEmail() 
		{
			switch (GetEnvironment())
			{ 
				case EEnvironment.Release:
				case EEnvironment.Staging:
					return (string)_temp_settings[REPORT_SUPPORT_EMAIL];

				default:
					return (string)_temp_settings[REPORT_DEBUG_EMAIL];
			}
		}

		#endregion
	}
}
