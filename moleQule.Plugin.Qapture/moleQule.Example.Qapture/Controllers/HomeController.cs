using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infralution.Localization;

namespace moleQule.Example.Qapture.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index(String locale, String requestUrl)
		{
			//Locale -------------------------------------------------------------------------------
			//Load the culture info from the parameter
			CultureManager.ApplicationUICulture = locale != null 
													? CultureInfo.CreateSpecificCulture(locale)
													: CultureInfo.CreateSpecificCulture(CultureInfo.InstalledUICulture.Name);

			//Save the culture into the cookie
			HttpCookie _cookie = new HttpCookie("CULTURE", CultureManager.ApplicationUICulture.Name);
			_cookie.Expires = DateTime.Now.AddYears(1);
			Response.SetCookie(_cookie);

			return Redirect(requestUrl);
		}

		public ActionResult About()
		{
			return View();
		}

		public ActionResult Contact()
		{
			return View();
		}

		public ActionResult Tutorials()
		{
			return View();
		}
	}
}
