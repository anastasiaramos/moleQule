using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

using moleQule.Library;

namespace moleQule.WebFace.Models
{
	[Serializable()]
	public class MenuItemViewModel
	{
		public string Action { get; set; }
		public string Controller { get; set; }
        public long Oid { get; set; }
		public bool Disabled { get; set; }
		public string Label { get; set; }
		public string Name { get; set; }
		public molAction Operation { get; set; }
		public object RouteValues { get; set; }

		public MenuItemListViewModel SubMenu { get; set; }
	}

	/// <summary>
	/// ViewModel List
	/// </summary>
	[Serializable()]
	public class MenuItemListViewModel : List<MenuItemViewModel>
	{
		public MenuItemViewModel GetItem(molAction action)
		{
			return this.FirstOrDefault(x => x.Operation == action);
		}
	}
}