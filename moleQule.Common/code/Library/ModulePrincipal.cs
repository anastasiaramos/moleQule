using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Reflection;

using moleQule.Library;
using moleQule.Library.Common.Properties;

namespace moleQule.Library.Common
{
    [Serializable()]
    public class ModulePrincipal
    {
        #region Application Settings

		public static void SaveSettings() { Settings.Default.Save(); }

		public static void UpgradeSettings()
		{
			Assembly ensamblado = System.Reflection.Assembly.GetExecutingAssembly();
			Version ver = ensamblado.GetName().Version;

			if (Properties.Settings.Default.MODULE_VERSION != ver.ToString())
			{
				Properties.Settings.Default.Upgrade();
				Properties.Settings.Default.MODULE_VERSION = ver.ToString();
			}
		}

		public static string GetDBVersion() { return Settings.Default.DB_VERSION; }

        #endregion

		#region Schema Settings

		public static int GetNDigitosCuentasContablesSetting()
		{
			return Convert.ToInt32(SettingsMng.Instance.SchemaSettings.GetValue(Settings.Default.SETTING_NAME_N_DIGITOS_CUENTAS_CONTABLES));
		}
		public static void SetNDigitosCuentasContablesSetting(long value)
		{
			SettingsMng.Instance.SchemaSettings.SetValue(Settings.Default.SETTING_NAME_N_DIGITOS_CUENTAS_CONTABLES, value.ToString());
		}

		#endregion

		#region Security.User Settings

		public static DateTime GetActiveYear()
		{
			return Convert.ToDateTime(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_ACTIVE_YEAR));			
		}
		public static void SetActiveYear(DateTime value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_ACTIVE_YEAR, value.ToShortDateString());
		}

		public static bool GetUseActiveYear()
		{
			return Convert.ToBoolean(SettingsMng.Instance.UserSettings.GetValue(Settings.Default.SETTING_NAME_USE_ACTIVE_YEAR));
		}
		public static void SetUseActiveYear(bool value)
		{
			SettingsMng.Instance.UserSettings.SetValue(Settings.Default.SETTING_NAME_USE_ACTIVE_YEAR, value.ToString());
        }

		#endregion
	}
}