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
	public static class SortableHelper
	{
        public static MvcHtmlString ListSortable(this System.Web.Mvc.HtmlHelper helper, string columnName, ListSortDirection direction, string selectedValue, Dictionary<String, String> propertiesOrder)
		{
            String directionstring = String.Empty;
            String classicon = String.Empty;

            if (columnName == selectedValue)
            {
                if (direction == ListSortDirection.Descending)
                {
                    directionstring = "ASC";
                    classicon = "icon-chevron-up";
                }
                else
                {
                    directionstring = "DESC";
                    classicon = "icon-chevron-down";
                }
            }
            else
            {
                directionstring = "ASC";
            }

			StringBuilder b = new StringBuilder();
            b.Append(string.Format("<a href=\"#\" onClick=\"tableOrdering('{0}','{1}')\">{2}", columnName, directionstring, propertiesOrder[columnName] ));

            if (columnName == selectedValue)
            {
                b.Append(string.Format("<i class=\"{0} pull-right hidden-print\"></i>", classicon));
			}

            b.Append("</a>");

			return MvcHtmlString.Create(b.ToString());
		}        
	}
}