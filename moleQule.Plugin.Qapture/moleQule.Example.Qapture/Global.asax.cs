using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using Infralution.Localization;

namespace moleQule.Example.Qapture
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
			var cookie = Request.Cookies["CULTURE"];
			string cultureName = (cookie != null) ? cookie.Value : "es-ES";

			CultureManager.ApplicationUICulture = cultureName != null
											? CultureInfo.CreateSpecificCulture(cultureName)
											: CultureInfo.CreateSpecificCulture(CultureInfo.InstalledUICulture.Name);

		}
	}
}