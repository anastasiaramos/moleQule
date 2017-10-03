using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.WebFace;
using moleQule.WebFace.Models;

namespace moleQule.WebFace.Helpers
{
	public class ViewMng
    {
        #region Factory Methods

        protected static ViewMng _main;

		public static ViewMng Instance { get { return (_main != null) ? _main : new ViewMng(); } }

        public ViewMng()
		{
			_main = this;
		}

		#endregion       
    }
}
