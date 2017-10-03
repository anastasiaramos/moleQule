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
using moleQule.Plugin.Qapture.Resources;
using moleQule.Tools.Qapture;

namespace moleQule.Plugin.Qapture
{
	public partial class MainPage : UserControl
	{
		public MainPage()
		{
			InitializeComponent();

			HtmlPage.RegisterScriptableObject("Qapture", this);

			if (!Application.Current.HasElevatedPermissions)
				MessageBox.Show(Errors.NO_PRIVILEGES);
		}

		[ScriptableMember]
		public Boolean Start(string message, string xr)
		{
			return Start(message, xr, 0, 0, false);
		}

		[ScriptableMember]
		public Boolean Start(string message, string xr, bool debug)
		{
			return Start(message, xr, 0, 0, debug);
		}

		[ScriptableMember]
		public Boolean Start(string message, string xr, int maxWidth, int maxHeight, bool debug)
		{
			try
			{
				busyIndicator.IsBusy = true;
				busyIndicator.BusyContent = message;

				byte[] imageBytes = ScanMng.Scan(message, xr, maxWidth, maxHeight, debug);

				if (imageBytes != null)
				{
#if DEMO
					MessageBox.Show(moleQule.Plugin.Qapture.Resources.Errors.DEMO_VERSION);
#endif
					HtmlDocument doc = HtmlPage.Document;
					HtmlElement image = doc.GetElementById("scanner-screenshot");
					image.SetAttribute("src", "data:image/jpeg;base64," + Convert.ToBase64String(imageBytes));
					return true;
				}

				return false;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				return false;
			}
			finally
			{
				busyIndicator.IsBusy = false;
			}
		}
	}
}
