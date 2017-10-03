using System;
using System.Security.Principal;
using System.Globalization;
using System.Web;

using Csla;
using moleQule.Library.CslaEx; 

namespace moleQule.Library
{
	internal static class molAppContext
    {
        #region Global Objects

		public static ISchemaInfo ActiveSchema { get { return Principal.ActiveSchema; } set { Principal.ActiveSchema = value; } }
        public static string CommonSchema { get { return "COMMON"; } }
		public static IPrincipalEx Principal { get { return ApplicationContextEx.User; } set { ApplicationContextEx.User = value; } }
		public static string StarUpPath { get { return AppDomain.CurrentDomain.BaseDirectory; } }
		public static User User 
		{ 
			get 
			{
				try
				{
                    return (User)(Principal.Identity);
				}
				catch
				{
					return null;
				}
			} 
		}

		public static CultureInfo GetCultureInfo(string locale)
		{
			CultureInfo culture;

			switch (locale)
			{
				case "--":
				case "system":
				case "default":
					{
						culture = CultureInfo.CreateSpecificCulture(CultureInfo.InstalledUICulture.Name);
					}
					break;

				default:
					{
						CultureInfo c_info = CultureInfo.GetCultureInfo(locale);
						switch (c_info.TwoLetterISOLanguageName)
						{
							case "es":
								culture = CultureInfo.CreateSpecificCulture("es-ES");
								culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
								culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
								culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
								break;

							case "en":
								culture = CultureInfo.CreateSpecificCulture("en-US");
								culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
								culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
								culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
								break;

							default:
								//culture = c_info;
								culture = CultureInfo.CreateSpecificCulture("en-US");
								culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
								culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
								culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
								break;
						}
					}
					break;
			}

			//Formato de Fecha Español para que no fallen las consultas
			//culture.DateTimeFormat = CultureInfo.CreateSpecificCulture("es-ES").DateTimeFormat;
			culture.NumberFormat.NumberDecimalSeparator = ".";
			culture.NumberFormat.NumberGroupSeparator = ",";
			culture.NumberFormat.CurrencyDecimalSeparator = ".";
			culture.NumberFormat.CurrencyGroupSeparator = ",";
			culture.NumberFormat.CurrencyPositivePattern = 3;
            culture.NumberFormat.CurrencyNegativePattern = 5;

			return culture;
		}

        #endregion
    }

	internal static class molHttpContext
	{
		#region Global Objects

		public static ISchemaInfo ActiveSchema { get { return Principal.ActiveSchema; } set { Principal.ActiveSchema = value; } }
		public static string CommonSchema { get { return "COMMON"; } }
		public static IPrincipalEx Principal 
		{ 
			get
			{
				try
				{
					if (HttpContext.Current.User is IPrincipalEx)
						return (IPrincipalEx)(HttpContext.Current.User);
					else
						return null;
				}
				catch
				{
					return null;
				}
			}

			set { HttpContext.Current.User = (IPrincipal)value; } 
		}
		public static string StarUpPath { get { return HttpRuntime.AppDomainAppPath; } }
		public static User User
		{
			get
			{
				try
				{
					return (User)(Principal.Identity);
				}
				catch
				{
					return null;
				}
			}
		}

		public static CultureInfo GetCultureInfo(string locale)
		{
			CultureInfo culture;

			switch (locale)
			{
				case "--":
				case "system":
				case "default":
					{
						culture = CultureInfo.CreateSpecificCulture(CultureInfo.InstalledUICulture.Name);
					}
					break;

				default:
					{
						switch (CultureInfo.GetCultureInfo(locale).TwoLetterISOLanguageName)
						{
							case "es":
								culture =  CultureInfo.CreateSpecificCulture("es-ES");
								culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
								culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
								culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
								break;

							case "en":
								culture = CultureInfo.CreateSpecificCulture("en-US");
								culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
								culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
								culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
								break;

							//English by default
							default:
								culture = CultureInfo.CreateSpecificCulture("en-US");
                                culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
								culture.DateTimeFormat.LongTimePattern = "HH:mm:ss";
								culture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
								break;
						}
					}
					break;
			}

            culture.NumberFormat.CurrencySymbol = "€";
			culture.NumberFormat.NumberDecimalSeparator = ".";
			culture.NumberFormat.NumberGroupSeparator = ",";
			culture.NumberFormat.CurrencyDecimalSeparator = ".";
			culture.NumberFormat.CurrencyGroupSeparator = ",";
			culture.NumberFormat.CurrencyPositivePattern = 3;
			culture.NumberFormat.CurrencyNegativePattern = 3;

			return culture;
		}

		#endregion
	}

	public static class AppContext
	{
		#region Global Objects

		public static ISchemaInfo ActiveSchema 
		{
			get
			{
				try
				{
					return (SettingsMng.Instance.GetApplicationType() == EAppType.Web)
						? molHttpContext.ActiveSchema
						: molAppContext.ActiveSchema;
				}
				catch
				{
					return null;
				}
			}
			set
			{
				if (SettingsMng.Instance.GetApplicationType() == EAppType.Web)
					molHttpContext.ActiveSchema = value;
				else
					molAppContext.ActiveSchema = value;
			} 
		}		
		public static string CommonSchema 
		{
			get
			{
				return (SettingsMng.Instance.GetApplicationType() == EAppType.Web)
					? molHttpContext.CommonSchema
					: molAppContext.CommonSchema;
			}
		}
		public static IPrincipalEx Principal
		{
			get
			{
				return (SettingsMng.Instance.GetApplicationType() == EAppType.Web)
					? molHttpContext.Principal
					: molAppContext.Principal;
			}
			set
			{
				if (SettingsMng.Instance.GetApplicationType() == EAppType.Web)
					molHttpContext.Principal = value;
				else
					molAppContext.Principal = value;
			}
		}
		public static string StartUpPath
		{
			get
			{
				return (SettingsMng.Instance.GetApplicationType() == EAppType.Web)
					? molHttpContext.StarUpPath
					: molAppContext.StarUpPath;
			}
		}
		public static User User
		{
			get
			{
				try
				{
					return (SettingsMng.Instance.GetApplicationType() == EAppType.Web)
						? molHttpContext.User
						: molAppContext.User;
				}
				catch
				{
					return null;
				}
			}
		}

		public static CultureInfo GetCultureInfo(string locale)
		{
			return (SettingsMng.Instance.GetApplicationType() == EAppType.Desktop)
					? molAppContext.GetCultureInfo(locale)
					: molHttpContext.GetCultureInfo(locale);
		}

		#endregion
	}
}
