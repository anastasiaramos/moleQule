using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using moleQule.Library;
using moleQule.WebFace;

namespace moleQule.WebFace.Models
{
	/// <summary>
	/// SettingViewModel
	/// </summary>
	[Serializable()]
	public class SchemaSettingViewModel
	{
		#region Attributes

		string _schema_code;

		bool _smtp_enable_ssl = false;
		string _smtp_host;
		string _smtp_mail;
		int _smtp_port = 0;
		string _smtp_pwd;
		string _smtp_user;

		string _admin_email;
		string _admin_contact;
		string _admin_mobile_phone;
		string _clients_email;
		string _clients_contact;
		string _clients_mobile_phone;
		string _support_email;
		string _support_contact;
		string _support_mobile_phone;

		string _sms_gateway_account;
		long _sms_gateway_code = -1;
		string _sms_gateway_name;
		string _sms_gateway_pwd;
		string _sms_gateway_user;

		#endregion

        #region Properties

		public string SchemaCode { get { return _schema_code; } set { _schema_code = value; } }

		//Smtp
		[Display(ResourceType = typeof(Resources.Labels), Name = "SMTP_EMAIL")]
		public string SMTPEmail { get { return _smtp_mail; } set { _smtp_mail = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "SMTP_ENABLE_SSL")]
		public bool SMTPEnableSSL { get { return _smtp_enable_ssl; } set { _smtp_enable_ssl = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "SMTP_HOST")]
		public string SMTPHost { get { return _smtp_host; } set { _smtp_host = value; } }
	
		[Display(ResourceType = typeof(Resources.Labels), Name = "SMTP_PORT")]
		public int SMTPPort { get { return _smtp_port; } set { _smtp_port = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "SMTP_PWD")]
		public string SMTPPwd { get { return _smtp_pwd; } set { _smtp_pwd = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "SMTP_USER")]
		public string SMTPUser { get { return _smtp_user; } set { _smtp_user = value; } }

		// Contacts
		[Display(ResourceType = typeof(Resources.Labels), Name = "EMAIL")]
		public string AdminEmail { get { return _admin_email; } set { _admin_email = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "CONTACT")]
		public string AdminContact { get { return _admin_contact; } set { _admin_contact = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "MOBILE_PHONE")]
		public string AdminMobilePhone { get { return _admin_mobile_phone; } set { _admin_mobile_phone = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "EMAIL")]
		public string ClientsEmail { get { return _clients_email; } set { _clients_email = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "CONTACT")]
		public string ClientsContact { get { return _clients_contact; } set { _clients_contact = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "MOBILE_PHONE")]
		public string ClientsMobilePhone { get { return _clients_mobile_phone; } set { _clients_mobile_phone = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "EMAIL")]
		public string SupportEmail { get { return _support_email; } set { _support_email = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "CONTACT")]
		public string SupportContact { get { return _support_contact; } set { _support_contact = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "MOBILE_PHONE")]
		public string SupportMobilePhone { get { return _support_mobile_phone; } set { _support_mobile_phone = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "SMS_GATEWAY_ACCOUNT")]
		public string SMSGatewayAccount { get { return _sms_gateway_account; } set { _sms_gateway_account = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "SMS_GATEWAY_CODE")]
		public long SMSGatewayCode { get { return (_sms_gateway_code == 0) ? -1 : _sms_gateway_code; } set { _sms_gateway_code = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "SMS_GATEWAY_NAME")]
		public string SMSGatewayName { get { return _sms_gateway_name; } set { _sms_gateway_name = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "SMS_GATEWAY_PWD")]
		public string SMSGatewayPwd { get { return _sms_gateway_pwd; } set { _sms_gateway_pwd = value; } }

		[Display(ResourceType = typeof(Resources.Labels), Name = "SMS_GATEWAY_USER")]
		public string SMSGatewayUser { get { return _sms_gateway_user; } set { _sms_gateway_user = value; } }

		#endregion

		#region Business Objects

		#endregion

		#region Factory Methods

		public SchemaSettingViewModel() { }

		public static SchemaSettingViewModel Get(string schemaCode)
		{
			SchemaSettingViewModel obj = new SchemaSettingViewModel();
			obj.SchemaCode = schemaCode;

			obj.LoadSettings(GetSchemaSettings(schemaCode));

			return obj;
		}
		
		protected static SchemaSettings GetSchemaSettings(string schemaCode)
		{
			string currentschema = AppContext.ActiveSchema.Code;
			AppContext.Principal.ChangeUserSchema(schemaCode, true);

			SchemaSettings settings = Library.SchemaSettings.GetList();
			settings.CloseSession();

			AppContext.Principal.ChangeUserSchema(currentschema, true);

			return settings;
		}

		protected virtual void LoadSettings(SchemaSettings settings)
		{
			SMTPEmail = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_EMAIL);
			try { SMTPEnableSSL = Convert.ToBoolean(settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_ENABLE_SSL)); }
			catch { }
			SMTPHost = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_HOST);
			try { SMTPPort = Convert.ToInt32(settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_PORT)); }
			catch { }
			SMTPPwd = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_PWD);
			SMTPUser = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_USER);

			AdminEmail = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_ADMIN_EMAIL);
			AdminContact = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_ADMIN_CONTACT);
			AdminMobilePhone = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_ADMIN_MOBILE_PHONE);
			ClientsEmail = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_CLIENTS_EMAIL);
			ClientsContact = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_CLIENTS_CONTACT);
			ClientsMobilePhone = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_CLIENTS_MOBILE_PHONE);
			SupportEmail = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SUPPORT_EMAIL);
			SupportContact = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SUPPORT_CONTACT);
			SupportMobilePhone = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SUPPORT_MOBILE_PHONE);				

			SMSGatewayAccount = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_ACCOUNT);
			try { SMSGatewayCode = Convert.ToInt64(settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_CODE)); }
			catch { }
			SMSGatewayName = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_NAME);
			SMSGatewayPwd = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_PWD);
			SMSGatewayUser = settings.GetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_USER);
		}

		protected virtual void SaveSettings(SchemaSettings settings)
		{
			SaveGatewaySettings(settings);
			SaveContactsSettings(settings);
		}

		protected virtual void SaveGatewaySettings(SchemaSettings settings)
		{
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_ENABLE_SSL, SMTPEnableSSL.ToString());
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_EMAIL, SMTPEmail);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_HOST, SMTPHost);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_PORT, SMTPPort.ToString());
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_USER, SMTPUser);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMTP_PWD, SMTPPwd);

			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_ACCOUNT, SMSGatewayAccount);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_CODE, SMSGatewayCode.ToString());
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_NAME, SMSGatewayName);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_PWD, SMSGatewayPwd);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SMS_GATEWAY_USER, SMSGatewayUser);
		}

		protected virtual void SaveContactsSettings(SchemaSettings settings)
		{
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_ADMIN_EMAIL, AdminEmail);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_ADMIN_CONTACT, AdminContact);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_ADMIN_MOBILE_PHONE, AdminMobilePhone);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_CLIENTS_EMAIL, ClientsEmail);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_CLIENTS_CONTACT, ClientsContact);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_CLIENTS_MOBILE_PHONE, ClientsMobilePhone);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SUPPORT_EMAIL, SupportEmail);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SUPPORT_CONTACT, SupportContact);
			settings.SetValue(moleQule.Library.Properties.Settings.Default.SETTING_NAME_SUPPORT_MOBILE_PHONE, SupportMobilePhone);
		}

		public void Save()
		{
			string currentschema = AppContext.ActiveSchema.Code;
			AppContext.Principal.ChangeUserSchema(SchemaCode, true);

			SchemaSettings settings = Library.SchemaSettings.GetList();

			SaveSettings(settings);
			settings.Save();

			AppContext.Principal.ChangeUserSchema(currentschema, true);
		}

		#endregion
	}
}
