using System.Web;
using System.Web.Mvc;

namespace moleQule.Example.Qapture
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}