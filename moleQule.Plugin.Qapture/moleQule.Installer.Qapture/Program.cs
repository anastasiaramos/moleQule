using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace moleQule.Installer.Qapture
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				WinRegMng.DoRun();
				CertificateMng.DoRun();
				Console.WriteLine("Qapture Image Plugin... installed");
				Thread.Sleep(10000);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
