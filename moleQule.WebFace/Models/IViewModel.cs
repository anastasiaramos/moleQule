using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace moleQule.WebFace.Models
{
	public interface IViewModel
	{
		[HiddenInput]
		long Oid { get; set; }

		[HiddenInput]
		long Status { get; set; }

		string StatusLabel { get; set; }
	}
}
