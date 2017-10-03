using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;

using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.WebFace.Common.HtmlHelpers
{
	public static class DropDownHelper
	{
		public static MvcHtmlString CurrencyDropDown(this System.Web.Mvc.HtmlHelper helper, string name, object selectedValue)
		{
			ComboBoxList<ECurrency> list = Library.Common.EnumText<ECurrency>.GetList(true, false);

			StringBuilder b = new StringBuilder();
			b.Append(string.Format("<select name=\"{0}\" id=\"{0}\" class=\"input-mini\">", name));

			string selected = string.Empty;

			foreach (ComboBoxSource item in list)
			{
				selected = (item.Oid == Convert.ToInt64(selectedValue)) ? "selected=\"selected\"" : string.Empty;

				b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", item.Oid, selected, item.Texto));
			}
			b.Append("</select>");

			return MvcHtmlString.Create(b.ToString());
		}
	}
}