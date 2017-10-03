using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;

namespace moleQule.Installer.Qapture
{
    public class CertificateMng
	{
		#region Application

		public static void DoRun()
		{
			string certPath = AppDomain.CurrentDomain.BaseDirectory;
			string certificateFile = "Qapture_TemporaryKey.cer";
			string certificateLocation = certPath + certificateFile;

			InstallCertificate(certificateLocation);
		}

		#endregion

		#region Business Methods

		private static void InstallCertificate(string certificatePath)
		{
			try
			{
				X509Store store;
				X509Certificate2 cert = null;
	
				try
				{
					cert = new X509Certificate2(certificatePath);

                    // Install the certificate to the Trusted Publishers certificate store and 
                    // (if necessary) the Trusted Root Certification Authorities store

                    //Install certificate to Trusted Publishers certificate store
                    // Editores de Confianza
					store = new X509Store(StoreName.TrustedPublisher, StoreLocation.LocalMachine);
					store.Open(OpenFlags.ReadWrite);
					store.Add(cert);
					store.Close();

                    Console.WriteLine("Plugin Certificate in Trusted Publishers store ... installed");

					//Install certificate to Trusted Root Certification Authorities store
                    // Entidades de certificación raíz de confianza
					store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
					store.Open(OpenFlags.ReadWrite);
					store.Add(cert);
					store.Close();

                    Console.WriteLine("Plugin Certificate in Trusted Root Certification Authorities store ... installed");
				}
				catch (Exception ex)
				{
					Console.WriteLine("Failed to load certificate " + certificatePath);
					Console.WriteLine(ex.Message);
				}
			}
			catch (Exception)
			{
				Console.WriteLine("Failed to install {0}.  Check the certificate index entry and verify the certificate file exists.", certificatePath);
			}
		}

		#endregion
	}
}
