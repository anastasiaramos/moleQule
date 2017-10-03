using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using moleQule.Library.CslaEx; 

using moleQule.Library;
using moleQule.Library.Common;

namespace moleQule.WebFace.HtmlHelpers
{
	public static class InputHelper
	{
        public static MvcHtmlString InputControl(this System.Web.Mvc.HtmlHelper helper, Type entityType, string name, string propertyName, object value)
		{
            System.Reflection.PropertyInfo prop = entityType.GetProperty(propertyName);      

            StringBuilder b = new StringBuilder();

			b.Append(@"<div id=""Parameters"" class=""input-append"">");

			if (prop.PropertyType.Equals(typeof(System.DateTime)))
			{
				b.Append(string.Format(@"
		                <input id=""{0}"" name=""{0}"" type=""text"" class=""input-medium date datepicker"" value=""{1}""/>
		                <!--<span class=""add-on glyphicons calendar""><i></i></span>-->",
					name, DateTime.Today)
				);
			}
			else if (prop.PropertyType.Equals(typeof(System.Boolean)))
			{
				b.Append(string.Format("<select name=\"{0}\" id=\"{0}\">", name));

				List<Boolean> options = new List<Boolean> {
					true,
                    false
                };

				string selected = string.Empty;

				foreach (Boolean item in options)
				{
					selected = (item == (Boolean)value) ? "selected=\"selected\"" : string.Empty;
					b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", item, selected, item));
				}

				b.Append("</select>");
			}
			else
			{
				b.Append(string.Format(@"
					<input type=""text"" id=""{0}"" name=""{0}"" value=""{1}"" placeholder=""{2}"" />",
					name, value, Resources.Labels.SEARCH)
				);
			}            

			b.Append(@"
					<button type=""submit"" class=""add-on glyphicons search"" name=""searchByParameterAction"" value=""searchByParameterAction""><i></i></button>
					<button type=""submit"" class=""add-on glyphicons remove"" data-toggle=""tooltip"" data-original-title=""" + Resources.Messages.RESET_FILTER + @""" data-placement=""top"" name=""resetFilterAction"" value=""resetFilterAction"" id=""resetFilterAction""><i></i></button>
				</div>"
			);

			return MvcHtmlString.Create(b.ToString());
		}        
	}
}