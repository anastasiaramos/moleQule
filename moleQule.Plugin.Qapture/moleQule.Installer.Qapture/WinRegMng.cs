using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Win32;

namespace moleQule.Installer.Qapture
{
    /// <summary>
    /// Clase base controladora principal. 
    /// El controlador de su aplicación debe heredar de esta clase
    /// </summary>
    public class WinRegMng
	{
		#region Enums

		protected enum WinRegSystem { Win32, Win64 }
		protected enum WinRegKey { AllowElevatedTrustAppsInBrowser, AllowInstallOfElevatedTrustApps, AllowLaunchOfElevatedTrustApps }
		protected enum WinRegMode { Write, ReadOnly }

		#endregion

		#region Application

		public static string DoRun()
		{
			string debug = string.Empty;
			string debug_line = string.Empty;

			try
			{
				WinRegSystem wregsystem = (Environment.Is64BitOperatingSystem) ? WinRegSystem.Win64 : WinRegSystem.Win32;

                if (RegKeyGetValue(wregsystem, WinRegKey.AllowElevatedTrustAppsInBrowser) != string.Empty)
                {
                    RegKeySetValue(wregsystem, WinRegKey.AllowElevatedTrustAppsInBrowser, 1);
                    Console.WriteLine(string.Format("Registry Key {0}... updated", WinRegKey.AllowElevatedTrustAppsInBrowser.ToString()));
                }
                else
                {
                    RegKeySetValue(wregsystem, WinRegKey.AllowElevatedTrustAppsInBrowser, 1);
                    Console.WriteLine(string.Format("Registry Key {0}... created", WinRegKey.AllowElevatedTrustAppsInBrowser.ToString()));
                }

				if (RegKeyGetValue(wregsystem, WinRegKey.AllowInstallOfElevatedTrustApps) != string.Empty)
				{
					RegKeySetValue(wregsystem, WinRegKey.AllowInstallOfElevatedTrustApps, 1);
					Console.WriteLine(string.Format("Registry Key {0}... updated", WinRegKey.AllowInstallOfElevatedTrustApps.ToString()));
				} 
                else 
                {
					RegKeySetValue(wregsystem, WinRegKey.AllowInstallOfElevatedTrustApps, 1);
					Console.WriteLine(string.Format("Registry Key {0}... created", WinRegKey.AllowInstallOfElevatedTrustApps.ToString()));
                }

                if (RegKeyGetValue(wregsystem, WinRegKey.AllowLaunchOfElevatedTrustApps) != string.Empty)
                {
                    RegKeySetValue(wregsystem, WinRegKey.AllowLaunchOfElevatedTrustApps, 1);
                    Console.WriteLine(string.Format("Registry Key {0}... updated", WinRegKey.AllowLaunchOfElevatedTrustApps.ToString()));
                }
                else
                {
                    RegKeySetValue(wregsystem, WinRegKey.AllowLaunchOfElevatedTrustApps, 1);
                    Console.WriteLine(string.Format("Registry Key {0}... created", WinRegKey.AllowLaunchOfElevatedTrustApps.ToString()));
                }
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return string.Empty;
		}

		#endregion

		#region Business Methods

		protected static string GetAppKeyBaseName() { return "SOFTWARE\\"; }

		protected static RegistryKey RegGetAppKey(WinRegSystem wRegSystem, WinRegMode mode = WinRegMode.ReadOnly)
        {
            try
            {
				string entryName = GetAppKeyBaseName();

				switch (wRegSystem)
				{
					case WinRegSystem.Win32: entryName += Properties.Settings.Default.SILVERLIGHT_REGISTER_PATH_32; break;
					case WinRegSystem.Win64: entryName += Properties.Settings.Default.SILVERLIGHT_REGISTER_PATH_64; break;
				}

				RegistryKey rk = null;

                // Create a reference to a valid key.  In order for this code to
                // work, the indicated key must have been created previously.
                // The key name is not case-sensitive.
				if (WinRegMode.ReadOnly == mode)
					rk = Registry.LocalMachine.OpenSubKey(entryName, false);
				else
					rk = Registry.LocalMachine.CreateSubKey(entryName);

                if (rk == null)
                {
                    throw new Exception(string.Format(Properties.Resources.REG32_ENTRY_NOT_FOUND, entryName));
                }

                return rk;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		protected static string RegKeyGetValue(WinRegSystem wRegSystem, WinRegKey wRegKey)
        {
            try
            {
				RegistryKey rk = RegGetAppKey(wRegSystem);

				if (rk.GetValue(wRegKey.ToString()) != null)
					return rk.GetValue(wRegKey.ToString(), string.Empty).ToString();
				else
					return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		protected static void RegKeySetValue(WinRegSystem wRegSystem, WinRegKey wRegKey, object value, RegistryValueKind valueKind = RegistryValueKind.DWord)
		{
			try
			{
				RegistryKey rk = RegGetAppKey(wRegSystem, WinRegMode.Write);

				rk.SetValue(wRegKey.ToString(), value, valueKind);
                rk.Close();

			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


        #endregion

	}
}
