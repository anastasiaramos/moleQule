using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices.Automation;

namespace moleQule.Tools.Qapture
{
	public class ScanMng
	{
		public static byte[] Scan(string message, string xr, int maxWidth, int maxHeight, bool debug)
		{
			vXr(xr);

			string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";

			try
			{
				using (dynamic CommonDialog = AutomationFactory.CreateObject("WIA.CommonDialog"))
				{
					//dynamic device = CommonDialog.ShowSelectDevice(0, true, true);
					dynamic imageFile = CommonDialog.ShowAcquireImage(1, 1, 131072, wiaFormatJPEG, false, true, false);

					if (imageFile != null)
					{
#if DEMO
						dynamic imageThumb;

						/*using (dynamic ImageProcess = AutomationFactory.CreateObject("Wia.ImageProcess"))
						{
							ImageProcess.Filters.Add(ImageProcess.FilterInfos("Scale").FilterID);
							ImageProcess.Filters[ImageProcess.Filters.Count].Properties("MaximumWidth").Value = imageFile.Width / 2;
							ImageProcess.Filters[ImageProcess.Filters.Count].Properties("MaximumHeight").Value = imageFile.Height / 2;

							imageThumb = ImageProcess.Apply(imageFile);
						}*/

						using (dynamic ImageProcess = AutomationFactory.CreateObject("Wia.ImageProcess"))
						{
							dynamic left = new Random().Next(3, 6);
							dynamic top = new Random().Next(3, 6);

							ImageProcess.Filters.Add(ImageProcess.FilterInfos("Crop").FilterID);
							ImageProcess.Filters[ImageProcess.Filters.Count].Properties("Left").Value = imageFile.Width / left;
							ImageProcess.Filters[ImageProcess.Filters.Count].Properties("Top").Value = imageFile.Height / top;
							ImageProcess.Filters[ImageProcess.Filters.Count].Properties("Right").Value = imageFile.Width / left;
							ImageProcess.Filters[ImageProcess.Filters.Count].Properties("Bottom").Value = imageFile.Height / top;

							imageThumb = ImageProcess.Apply(imageFile);
						}

						using (dynamic ImageProcess = AutomationFactory.CreateObject("Wia.ImageProcess"))
						{
							dynamic left = new Random().Next(1, imageFile.Width - imageThumb.Width - 1);
							dynamic top = new Random().Next(1, imageFile.Height - imageThumb.Height - 1);

							ImageProcess.Filters.Add(ImageProcess.FilterInfos("Stamp").FilterID);
							ImageProcess.Filters[ImageProcess.Filters.Count].Properties("ImageFile").Value = imageThumb;
							ImageProcess.Filters[ImageProcess.Filters.Count].Properties("Left").Value = left;
							ImageProcess.Filters[ImageProcess.Filters.Count].Properties("Top").Value = top;

							imageFile = ImageProcess.Apply(imageFile);
						}
#endif
						if (maxWidth > 0 || maxHeight > 0)
						{
							using (dynamic ImageProcess = AutomationFactory.CreateObject("Wia.ImageProcess"))
							{
								ImageProcess.Filters.Add(ImageProcess.FilterInfos("Scale").FilterID);
								ImageProcess.Filters[ImageProcess.Filters.Count].Properties("MaximumWidth").Value = maxWidth > 0 ? maxWidth : imageFile.Width;
								ImageProcess.Filters[ImageProcess.Filters.Count].Properties("MaximumHeight").Value = maxHeight > 0 ? maxHeight : imageFile.Height;

								imageFile = ImageProcess.Apply(imageFile);
							}
						}

						if (imageFile.FormatID != wiaFormatJPEG)
						{
							using (dynamic ImageProcess = AutomationFactory.CreateObject("Wia.ImageProcess"))
							{
								ImageProcess.Filters.Add(ImageProcess.FilterInfos("Convert").FilterID);
								ImageProcess.Filters[ImageProcess.Filters.Count].Properties("FormatID").Value = wiaFormatJPEG;
								ImageProcess.Filters[ImageProcess.Filters.Count].Properties("Quality").Value = 70;

								if (maxWidth > 0 || maxHeight > 0)
								{ 
								
								}

								imageFile = ImageProcess.Apply(imageFile);
							}
						}

						return imageFile.FileData.BinaryData();
					}
				}

				return null;
			}
			catch (Exception ex)
			{
				string errormsg = moleQule.Tools.Qapture.Resources.Errors.DEVICE_NOT_FOUND;

				if (debug)
				{
					errormsg += Environment.NewLine + ex.Message;
					Exception inner = ex.InnerException;
					while (inner != null)
					{
						errormsg += Environment.NewLine + inner.Message;
						inner = inner.InnerException;
					}
				}

				throw new Exception(errormsg, ex);
			}
		}

		private static void vXr(string xr)
		{
			string currentdomain = string.Empty;

			try
			{
				Dictionary<string, string> pairs = new Dictionary<string, string>() 
				{
					{ "localhost", "Ygcc560952xsHqevneGmXp0NF8aOUp5C" },
					{ "how-to-use-scanner-from-browser-webapp.com", "1zAK772b9v193A1ul6D3861U8Q4U961C" },
					{ "alquilermotosporhora.com", "1zAK772b9v193A1ul6D3861U8Q4U961C" }
				};

				currentdomain = GetMainDomain(System.Windows.Application.Current.Host.Source.Host);

				KeyValuePair<string, string> pair = pairs.FirstOrDefault(x => currentdomain == x.Key);

				string key = MD5Core.GetHashString(xr + "_" + currentdomain);
				string vkey = MD5Core.GetHashString(pair.Value + "_" + pair.Key);

				if (key != vkey) throw new Exception(string.Format(moleQule.Tools.Qapture.Resources.Errors.LICENSE, currentdomain));
			}
			catch
			{
				throw new Exception(string.Format(moleQule.Tools.Qapture.Resources.Errors.LICENSE, currentdomain));
			}
		}

		private static string GetMainDomain(string domain)
		{
			string host = domain;
			string[] parts = host.Split('.');

			if ( parts.Length >= 2)
				host = parts[parts.Length - 2] + "." + parts[parts.Length - 1];

			return host;
		}
	}
}
