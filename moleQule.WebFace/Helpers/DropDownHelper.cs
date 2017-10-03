using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html; 

using moleQule.Library;
using moleQule.Library.CslaEx;
using moleQule.Library.Common;

namespace moleQule.WebFace.HtmlHelpers
{
	public static class DropDownHelper
	{
		public static MvcHtmlString CountryDropDown(this System.Web.Mvc.HtmlHelper helper, string name, string optionLabel, object selectedValue)
		{
			List<Country> countries = Country.Load();

			StringBuilder b = new StringBuilder();
			b.Append(string.Format("<select name=\"{0}\" id=\"{0}\" onchange=\"CountryChangeOperation();\">", name));
			
			if (!string.IsNullOrEmpty(optionLabel))
				b.Append(string.Format("<option value=\"\">{0}</option>", optionLabel));

			string selected = string.Empty;

			foreach (Country item in countries)
			{
				selected = (item.Iso2 == selectedValue as string) ? "selected=\"selected\"" : string.Empty;

				b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", item.Iso2, selected, item.Name));
			}
			b.Append("</select>");

			return MvcHtmlString.Create(b.ToString());
		}

        public static MvcHtmlString EnumDropDownListFor<TModel, TProperty, TEnum>(
                    this HtmlHelper<TModel> htmlHelper,
                    Expression<Func<TModel, TProperty>> expression,
                    TEnum selectedValue)
        {
            IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum))
                                        .Cast<TEnum>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem()
                                                {
                                                    Text = value.ToString(),
                                                    Value = value.GetHashCode().ToString(),
                                                    Selected = (value.Equals(selectedValue))
                                                };
            return SelectExtensions.DropDownListFor(htmlHelper, expression, items);
        }

		public static MvcHtmlString MonthDropDown(this System.Web.Mvc.HtmlHelper helper, string name, int selectedValue)
		{
			StringBuilder b = new StringBuilder();
			b.Append(string.Format("<select class=\"pagination input-mini pull-right\" name=\"{0}\" id=\"{0}\">", name));

			string selected = string.Empty;

			DateTime first = DateTime.Today.AddMonths(-DateTime.Today.Month);

			for (int i = 1; i <= 12; i++)
			{
				selected = (i == selectedValue) ? "selected=\"selected\"" : string.Empty;
				b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", i, selected, first.AddMonths(i).ToString("MMM").ToUpper()));
			}

			b.Append("</select>");

			return MvcHtmlString.Create(b.ToString());
		}

        public static MvcHtmlString OperatorsDropDown(this System.Web.Mvc.HtmlHelper helper, Type entityType, string name, string propertyName, object selectedValue)
		{
            System.Reflection.PropertyInfo prop = entityType.GetProperty(propertyName);      

            StringBuilder b = new StringBuilder();
            b.Append(string.Format("<select class=\"input-medium\" name=\"{0}\" id=\"{0}\">", name));

            string selected = string.Empty;

            if (propertyName == "Estado" || propertyName == "Status")
            {
                List<Operation> operations = new List<Operation> {
                                                                Operation.Equal,
                                                                Operation.Distinct
                };

                foreach (Operation item in operations)
                {
                    selected = (item == (Operation)selectedValue) ? "selected=\"selected\"" : string.Empty;
					b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", (long)item, selected, moleQule.Library.CslaEx.EnumText.GetString(item)));
                }
            }
            else if (prop.PropertyType.Equals(typeof(System.DateTime)))
            {
                List<Operation> operations = new List<Operation> {
                                                                Operation.Equal,
                                                                Operation.Distinct,
                                                                Operation.Less,
                                                                Operation.LessOrEqual,
                                                                Operation.Greater,
                                                                Operation.GreaterOrEqual
                };

                foreach (Operation item in operations)
			    {
                    selected = (item == (Operation)selectedValue) ? "selected=\"selected\"" : string.Empty;
					b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", (long)item, selected, moleQule.Library.CslaEx.EnumText.GetString(item)));
                }
            }
            else if ((prop.PropertyType.Equals(typeof(System.Int32))) ||
                    (prop.PropertyType.Equals(typeof(System.Int64))) ||
                    (prop.PropertyType.Equals(typeof(System.Decimal))) ||
                    (prop.PropertyType.Equals(typeof(System.Double))))
            {
                List<Operation> operations = new List<Operation> {
                                                                Operation.Equal,
                                                                Operation.Distinct,
                                                                Operation.Less,
                                                                Operation.LessOrEqual,
                                                                Operation.Greater,
                                                                Operation.GreaterOrEqual
                };

                foreach (Operation item in operations)
                {
                    selected = (item == (Operation)selectedValue) ? "selected=\"selected\"" : string.Empty;
					b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", (long)item, selected, moleQule.Library.CslaEx.EnumText.GetString(item)));
                }
            }
            else if (prop.PropertyType.Equals(typeof(System.Boolean)))
            {

                List<Operation> operations = new List<Operation> { Operation.Equal };

                foreach (Operation item in operations)
                {
                    selected = (item == (Operation)selectedValue) ? "selected=\"selected\"" : string.Empty;
					b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", (long)item, selected, moleQule.Library.CslaEx.EnumText.GetString(item)));
                }
            }
            else
            {
                List<Operation> operations = new List<Operation> {
                                                                Operation.Equal,
                                                                Operation.Distinct,
                                                                Operation.Less,
                                                                Operation.LessOrEqual,
                                                                Operation.Greater,
                                                                Operation.GreaterOrEqual,
                                                                Operation.Contains,
                                                                Operation.StartsWith
                };

                foreach (Operation item in operations)
                {
                    selected = (item == (Operation)selectedValue) ? "selected=\"selected\"" : string.Empty;
					b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", (long)item, selected, moleQule.Library.CslaEx.EnumText.GetString(item)));
                }
            }

            b.Append("</select>");

            return MvcHtmlString.Create(b.ToString());
        }

        public static MvcHtmlString PaginationLimitDropDown(this System.Web.Mvc.HtmlHelper helper, string name, int selectedValue)
        {
            StringBuilder b = new StringBuilder();
            b.Append(string.Format("<select onChange=\"limitPagination(this.options[this.selectedIndex].value)\" class=\"pagination input-mini pull-right\" name=\"{0}\" id=\"{0}\">", name));

            string selected = string.Empty;

            for (int i=5; i <= 30; i+=5)
            {
                selected = (i == selectedValue) ? "selected=\"selected\"" : string.Empty;
                b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", i, selected, i));
            }

            selected = (50 == selectedValue) ? "selected=\"selected\"" : string.Empty;
            b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", 50, selected, 50));

			selected = (99 == selectedValue) ? "selected=\"selected\"" : string.Empty;
			b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", 99, selected, 99));

			b.Append("</select>");

            return MvcHtmlString.Create(b.ToString());
        }

        public static MvcHtmlString StepDropDown(this System.Web.Mvc.HtmlHelper helper, string name, object selectedValue)
        {
            ComboBoxList<EStepGraph> steps = Library.EnumText<EStepGraph>.GetList(false, false, false);

            StringBuilder b = new StringBuilder();
            b.Append(string.Format("<select class=\"input-small\" name=\"{0}\" id=\"{0}\">", name));

            string selected = string.Empty;

            foreach (ComboBoxSource item in steps)
            {
                selected = (item.Oid == Convert.ToInt64(selectedValue)) ? "selected=\"selected\"" : string.Empty;

                b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", item.Oid, selected, item.Texto));
            }
            b.Append("</select>");

            return MvcHtmlString.Create(b.ToString());
        }

		public static MvcHtmlString SMSGatewaysDropDown(this System.Web.Mvc.HtmlHelper helper, string name, object selectedValue)
		{
			ComboBoxList<ESMSGateway> list = Library.EnumText<ESMSGateway>.GetList(true, false);

			StringBuilder b = new StringBuilder();
			b.Append(string.Format("<select name=\"{0}\" id=\"{0}\" class=\"input-small\">", name));

			string selected = string.Empty;

			foreach (ComboBoxSource item in list)
			{
				selected = (item.Oid == Convert.ToInt64(selectedValue)) ? "selected=\"selected\"" : string.Empty;

				b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", item.Oid, selected, item.Texto));
			}
			b.Append("</select>");

			return MvcHtmlString.Create(b.ToString());
		}

		public static MvcHtmlString YearDropDown(this System.Web.Mvc.HtmlHelper helper, string name, int selectedValue)
		{
			StringBuilder b = new StringBuilder();
			b.Append(string.Format("<select class=\"pagination input-mini pull-right\" name=\"{0}\" id=\"{0}\">", name));

			string selected = string.Empty;

			for (int i = DateTime.Today.Year; i >= DateTime.Today.Year - 15; i-=1)
			{
				selected = (i == selectedValue) ? "selected=\"selected\"" : string.Empty;
				b.Append(string.Format("<option value=\"{0}\" {1}>{2}</option>", i, selected, i));
			}

			b.Append("</select>");

			return MvcHtmlString.Create(b.ToString());
		}
	}
}