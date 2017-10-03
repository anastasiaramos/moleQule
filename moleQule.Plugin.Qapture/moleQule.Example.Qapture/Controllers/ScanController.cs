using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using moleQule.Example.Qapture.Models;

namespace moleQule.Example.Qapture.Controllers
{
	public class ScanController : Controller
    {
		#region Factory Methods

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		#endregion
		
		#region Actions

		//
		// GET: /Scan/Edit
		public ActionResult UploadExample()
		{
			ViewBag.ActiveItem = "UPLOAD";

			DocumentViewModel model = new DocumentViewModel();
			model.Oid = 1;
			model.Name = "NoImage.png";

			return View("ScanExample", model);
		}

		//
		// GET: /Scan/EditBasic
		public ActionResult BasicExample()
		{
			ViewBag.ActiveItem = "BASIC";

			DocumentViewModel model = new DocumentViewModel();
			model.Oid = 1;
			model.Name = "NoImage.png";

			return View("ScanExampleBasic", model);
		}

		[HttpPost]
		public ActionResult UploadExample(DocumentViewModel model)
		{
			return View("ScanExample", model);
		} 

		#endregion

		#region Ajax

		[HttpPost]
        public JsonResult Upload(HttpPostedFileBase file)
		{
            DocumentViewModel model = new DocumentViewModel();

            if (file == null)
            {
                file = Request.Files[0];
            }

			model.Name = "ScannedImage.jpg";
            DoUploadFile(model, file);

			ViewData["action"] = "Update";
			ViewData["controller"] = "Scan";

			return Json(new { success = true, html = RenderRazorViewToString("_ImagePartial", model) }, JsonRequestBehavior.AllowGet);
		}

		#endregion

		#region Helpers
		
		public static void DoUploadFile(DocumentViewModel model, HttpPostedFileBase file)
		{
			if ((file != null) && (file.ContentLength > 0))
			{
				byte[] fileData = new byte[file.ContentLength];
				file.InputStream.Read(fileData, 0, file.ContentLength);

				if (!Directory.Exists(Path.GetDirectoryName(model.Path)))
					Directory.CreateDirectory(Path.GetDirectoryName(model.Path));

				file.SaveAs(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, model.Path));
			}
		}

		#endregion

		#region Resource Views

		public string RenderRazorViewToString(string viewName)
		{
			using (var sw = new StringWriter())
			{
				var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
				var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
				viewResult.View.Render(viewContext, sw);
				viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
				return sw.GetStringBuilder().ToString();
			}
		}
		public string RenderRazorViewToString(string viewName, object model)
		{
			ViewData.Model = model;
			return RenderRazorViewToString(viewName);
		}

		#endregion
    }
}