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
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using RazorEngine;

using moleQule.Library;
using moleQule.Library.Common;
using moleQule.WebFace;
using moleQule.WebFace.Helpers;
using moleQule.WebFace.Models;

namespace moleQule.WebFace
{
	public class molViewEngineBase : RazorViewEngine
	{
		#region Attributes

		string _phisical_path = string.Empty;
		string _base_path = "Views\\base";

		#endregion

		#region Properties

		public string BasePhysicalPath { get { return _phisical_path; } }
		public string BasePath
		{
			get { return Path.Combine(BasePhysicalPath, _base_path); }
			set { _base_path = value; }
		}

		#endregion

		#region Factory Methods

		public molViewEngineBase()
		{
			ClearLocations();
			InitLocations();
		}
		public molViewEngineBase(string physicalPath, string applicationPath)
			: this()
		{
			_phisical_path = physicalPath;
			_base_path = applicationPath.Replace("~/", string.Empty);
			_base_path = _base_path.Replace("/", "\\");
		}

        public void AddViewLocation(string newLocation)
        {
			List<string> existingPaths = new List<string>(ViewLocationFormats);

			if (existingPaths.Contains(newLocation)) return;

			existingPaths.Add(newLocation);
			ViewLocationFormats = existingPaths.ToArray();
        }

		public void AddMasterViewLocation(string newLocation)
		{
			List<string> existingPaths = new List<string>(MasterLocationFormats);

			if (existingPaths.Contains(newLocation)) return;

			existingPaths.Add(newLocation);
			MasterLocationFormats = existingPaths.ToArray();
		}

        public void AddPartialViewLocation(string newLocation)
        {
			List<string> existingPaths = new List<string>(PartialViewLocationFormats);
			
			if (existingPaths.Contains(newLocation)) return;
			
			existingPaths.Add(newLocation);
			PartialViewLocationFormats = existingPaths.ToArray();
        }

		public void AddAreaViewLocation(string newLocation)
		{
			List<string> existingPaths = new List<string>(AreaViewLocationFormats);

			if (existingPaths.Contains(newLocation)) return;

			existingPaths.Add(newLocation);
			AreaViewLocationFormats = existingPaths.ToArray();
		}

		public void AddAreaMasterViewLocation(string newLocation)
		{
			List<string> existingPaths = new List<string>(AreaMasterLocationFormats);

			if (existingPaths.Contains(newLocation)) return;

			existingPaths.Add(newLocation);
			AreaMasterLocationFormats = existingPaths.ToArray();
		}

		public void AddAreaPartialViewLocation(string newLocation)
		{

			List<string> existingPaths = new List<string>(AreaPartialViewLocationFormats);

			if (existingPaths.Contains(newLocation)) return;

			existingPaths.Add(newLocation);
			AreaPartialViewLocationFormats = existingPaths.ToArray();
		}

		public void ClearLocations()
		{ 
			 ViewLocationFormats = new string[] {};
			 MasterLocationFormats = new string[] {};
			 PartialViewLocationFormats = new string[] {};
			 AreaViewLocationFormats = new string[] {};
			 AreaMasterLocationFormats = new string[] {};
			 AreaPartialViewLocationFormats = new string[] {};
		}

		public virtual void InitBaseLocations()
		{
			//Frontend -----------------------------------------------------

			AddViewLocation("~/Views/{1}/{0}.cshtml");
			AddViewLocation("~/Views/base/{1}/{0}.cshtml");
			AddViewLocation("~/Views/base/Shared/{0}.cshtml");
			AddViewLocation("~/Views/Shared/{0}.cshtml");

			AddMasterViewLocation("~/Views/base/{1}/{0}.cshtml");
			AddMasterViewLocation("~/Views/base/Shared/{0}.cshtml");
			AddMasterViewLocation("~/Views/Shared/{0}.cshtml");

			AddPartialViewLocation("~/Views/base/{1}/{0}.cshtml");
			AddPartialViewLocation("~/Views/base/Shared/{0}.cshtml");
			AddPartialViewLocation("~/Views/Shared/{0}.cshtml");

			//Areas -------------------------------------------------------

			AddAreaViewLocation("~/Areas/{2}/Views/{1}/{0}.cshtml");
			AddAreaViewLocation("~/Areas/{2}/Views/base/{1}/{0}.cshtml");
			AddAreaViewLocation("~/Areas/{2}/Views/base/Shared/{0}.cshtml");
			AddAreaViewLocation("~/Areas/{2}/Views/Shared/{0}.cshtml");
			
			AddAreaMasterViewLocation("~/Areas/{2}/Views/base/{1}/{0}.cshtml");
			AddAreaMasterViewLocation("~/Areas/{2}/Views/base/Shared/{0}.cshtml");
			AddAreaMasterViewLocation("~/Areas/{2}/Views/Shared/{0}.cshtml");

			AddAreaPartialViewLocation("~/Areas/{2}/Views/base/{1}/{0}.cshtml");
			AddAreaPartialViewLocation("~/Areas/{2}/Views/base/Shared/{0}.cshtml");
			AddAreaPartialViewLocation("~/Areas/{2}/Views/Shared/{0}.cshtml");
		}

		public virtual void InitLocations() { InitBaseLocations(); }

		#endregion

		#region Emails

        public string GetMailTemplate(ENotice eNotice, dynamic model, string locale="es")
        {
            string basepath = BasePath + "\\Shared\\Emails\\_" + locale;
			string headertemplate = basepath + "\\_EmailHeader.cshtml";
			string footertemplate = basepath + "\\_EmailFooter.cshtml";
			string bodytemplate = string.Empty;

            switch (eNotice)
            {
				case ENotice.CrewNotice:
					{
						footertemplate = basepath + "\\_EmailFooterCrew.cshtml";
						bodytemplate = basepath + "\\CrewNotice.cshtml";
					}break;

                case ENotice.Error:
                case ENotice.Info:
                    {
						bodytemplate = basepath + "\\Info.cshtml";
                    } break;

                case ENotice.Contact:
                    {
						bodytemplate = basepath + "\\Contact.cshtml";
                    } break;

				case ENotice.NewRegistration:
					{
						bodytemplate = basepath + "\\NewRegistration.cshtml";
					} break;

                case ENotice.SubscriptionActive:
					{
						bodytemplate = basepath + "\\SubscriptionActivation.cshtml";
					} break;

                case ENotice.SubscriptionExpired:
					{
						bodytemplate = basepath + "\\SubscriptionExpiration.cshtml";
					} break;

                case ENotice.SubscriptionFinished:
                    {
						bodytemplate = basepath + "\\SubscriptionFinish.cshtml";
                    } break;

                case ENotice.Monitor:
                    {
						headertemplate = string.Empty;
						bodytemplate = basepath + "\\Monitor.cshtml";
						footertemplate = string.Empty;
                    } break;
            }

			string template = string.Empty;
			string result = string.Empty;
			try
			{
				//Header
				template = headertemplate;
				result = !string.IsNullOrEmpty(template) ? RenderViewToString(template, model) : string.Empty;

				//Body
				template = bodytemplate;
				result += !string.IsNullOrEmpty(template) ? RenderViewToString(template, model) : string.Empty;

				//Footer
				template = footertemplate;
				result  += !string.IsNullOrEmpty(template) ? RenderViewToString(template, model) : string.Empty;
				
				return result;
			}
			catch (Exception ex)
			{
				molLogger.LogError(ex);
				throw new iQException(string.Format(Resources.Errors.VIEW_TEMPLATE_NOT_FOUND, template), iQExceptionCode.RESOURCE_NOT_FOUND);
			}
        }

        protected string RenderViewToString(string viewName, dynamic model)
        {
            var template = File.ReadAllText(viewName);
            var body = Razor.Parse(template, model);
            return body;
        }

        protected string RenderViewToString<T>(string viewName, T model)
        {
            var template = File.ReadAllText(viewName);
            var body = Razor.Parse(template, model);
            return body;
        }

        #endregion

		#region SMS

		public string GetSMSTemplate(ENotice eNotice, dynamic model)
		{
			string header = string.Empty;
			string footer = string.Empty;

			switch (eNotice)
			{
				case ENotice.Error:
				case ENotice.Info:
					{
						string body = System.Web.HttpUtility.HtmlDecode(RenderViewToString(BasePath + "\\SMS\\Info.cshtml", model));
						return header + body + footer;
					}

				case ENotice.Contact:
					{
						string body = System.Web.HttpUtility.HtmlDecode(RenderViewToString(BasePath + "\\SMS\\Info.cshtml", model));
						return header + body + footer;
					}

				case ENotice.SubscriptionActive:
					{
						string body = System.Web.HttpUtility.HtmlDecode(RenderViewToString(BasePath + "\\SMS\\Info.cshtml", model));
						return header + body + footer;
					}

				case ENotice.SubscriptionExpired:
					{
						string body = System.Web.HttpUtility.HtmlDecode(RenderViewToString(BasePath + "\\SMS\\Info.cshtml", model));
						return header + body + footer;
					}

				case ENotice.SubscriptionFinished:
					{
						string body = System.Web.HttpUtility.HtmlDecode(RenderViewToString(BasePath + "\\SMS\\Info.cshtml", model));
						return header + body + footer;
					}

				default:
					{
						string body = System.Web.HttpUtility.HtmlDecode(RenderViewToString(BasePath + "\\SMS\\Info.cshtml", model));
						return header + body + footer;
					}
			}
		}

		#endregion
	}	
}
