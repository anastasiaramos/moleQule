using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace moleQule.Library
{
	public class SMSGatewaySettings : ConfigurationSection
	{
		[ConfigurationProperty("GatewayName", DefaultValue = "Esendex", IsRequired = true)]
		public string GatewayName
		{
			get { return SettingsMng.Instance.GetSMSGatewayName(); }
			set { SettingsMng.Instance.SetSMSGatewayName(value); }
		}

		[ConfigurationProperty("GatewayCode", DefaultValue = "1", IsRequired = true)]
		public long GatewayCode
		{
			get { return SettingsMng.Instance.GetSMSGatewayCode(); }
			set { SettingsMng.Instance.SetSMSGatewayCode(value); }
		}

		[ConfigurationProperty("User", DefaultValue = "", IsRequired = true)]
		public string User
		{
			get { return SettingsMng.Instance.GetSMSGatewayUser(); }
			set { SettingsMng.Instance.SetSMSGatewayUser(value); }
		}

		[ConfigurationProperty("Password", DefaultValue = "", IsRequired = true)]
		public string Password
		{
			get { return SettingsMng.Instance.GetSMSGatewayPwd(); }
			set { SettingsMng.Instance.SetSMSGatewayPwd(value); }
		}

		[ConfigurationProperty("AccountReference", DefaultValue = "", IsRequired = true)]
		public string AccountReference
		{
			get { return SettingsMng.Instance.GetSMSGatewayAccount(); }
			set { SettingsMng.Instance.SetSMSGatewayAccount(value); }
		}
	}

	/*public class SMSGatewaySettings : ConfigurationSection
	{
		[ConfigurationProperty("GatewayName", DefaultValue = "Esendex", IsRequired = true)]
		public string GatewayName
		{
			get
			{
				return (string)this["GatewayName"];
			}

			set
			{
				this["GatewayName"] = value;
			}
		}

		[ConfigurationProperty("GatewayCode", DefaultValue = "1", IsRequired = true)]
		public long GatewayCode
		{
			get
			{
				return (long)this["GatewayCode"];
			}

			set
			{
				this["GatewayCode"] = value;
			}
		}

		[ConfigurationProperty("User", DefaultValue = "", IsRequired = true)]
        public string User
		{
			get
			{
				return (string)this["User"];
			}

			set
			{
				this["User"] = value;
			}
		}

        [ConfigurationProperty("Password", DefaultValue = "", IsRequired = true)]
        public string Password
        {
            get
            {
                return (string)this["Password"];
            }

            set
            {
                this["Password"] = value;
            }
        }

        [ConfigurationProperty("AccountReference", DefaultValue = "", IsRequired = true)]
        public string AccountReference
        {
            get
            {
                return (string)this["AccountReference"];
            }

            set
            {
                this["AccountReference"] = value;
            }
        }
	}*/
}
