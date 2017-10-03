using System;
using System.IO;
using System.Web.Mvc;

namespace moleQule.Example.Qapture.Models
{
	/// <summary>
	/// DocumentViewModel
	/// </summary>
	[Serializable()]
	public class DocumentViewModel 
	{
		#region Attributes

		#endregion	
	
		#region Properties

		[HiddenInput]
		public long Oid { get; set; }
		
		public virtual string Name { get; set; }

		public virtual string Path { get { return System.IO.Path.Combine("Data", Name); } }
		public virtual string WebPath { get { return Path.Replace("\\", "/"); } }

		#endregion

	}
}
