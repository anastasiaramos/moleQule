using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Management;
using System.DirectoryServices;
using System.Security.AccessControl;
using System.Security.Principal;

using moleQule.Library;

namespace UserCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    // Maquina Cliente
                    Config.CreateUser(PrincipalBase.UserName, PrincipalBase.Password);
                }
                else
                {
                    //Maquina Servidor
                    Config.CreateUser(PrincipalBase.UserName, PrincipalBase.Password);
                    Config.ShareFolder(args[0], PrincipalBase.UserName, PrincipalBase.GetServerName());
                    Config.LocalFolderPermissions(args[0], PrincipalBase.UserName, Environment.MachineName);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Empty;

                msg = ex.Message;
                msg += ex.InnerException != null ? ex.InnerException.Message : string.Empty;

                Console.WriteLine(msg);

                TimeSpan before = DateTime.Now.TimeOfDay;
                TimeSpan now;
                int seconds = 0;

                do
                {
                    now = DateTime.Now.TimeOfDay;
                    seconds = ((TimeSpan)(now - before)).Seconds;
                }
                while (seconds < 2);
            }
        }
    }
}
