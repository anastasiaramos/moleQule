using System;
using System.Collections.Generic;
using System.Text;
using System.DirectoryServices;
using System.Management;
using System.Security.Principal;
using System.IO;
using System.Security.AccessControl;

namespace UserCreator
{
    static class Config
    {
        /// <summary>
        /// Crea un usuario <paramref name="user_name"/> con permisos de administrador
        /// y con password <paramref name="password"/>
        /// </summary>
        /// <param name="user_name">Nombre de usuario</param>
        /// <param name="password">Contraseña</param>
        public static void CreateUser(string user_name, string password)
        {
            DirectoryEntry AD = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer");
            bool success = false;

            DirectoryEntry admin_group = null;
            // Se busca aquel grupo que sea Administradores o Administrators.
            foreach (DirectoryEntry ds in AD.Children)
            {
                if (ds.SchemaClassName.Equals("Group"))
                    if (ds.Name.ToLower().StartsWith("admin"))
                    {
                        admin_group = ds;
                    }
            }

            if (admin_group == null)
                throw new Exception("Error al crear el usuario");

            try
            {
                // Si salta una excepcion es que no existe el usuario.
                AD.Children.Find(user_name, "user");
            }
            catch
            {
                success = true;
            }

            if (success)
            {
                DirectoryEntry NewUser = AD.Children.Add(user_name, "user");
                NewUser.Invoke("SetPassword", new object[] { password });
                NewUser.Invoke("Put", new object[] { "Description", "Admin User for IQ Apps" });
                NewUser.CommitChanges();

                admin_group.Invoke("Add", new object[] { NewUser.Path.ToString() });
            }
        }


        /// <summary>
        /// Comparte el directio <paramref name="app_path"/>. Este directorio debe haber sido creado
        /// anteriormente. Los permisos de red se generan con control total unicamente para el usuario
        /// <paramref name="user_name"/>. El nombre del recurso compartido sera <paramref name="shared_name"/>
        /// </summary>
        /// <param name="app_path">Ruta al directorio a compartir</param>
        /// <param name="user_name">Usuario al cual se asignan los permisos</param>
        /// <param name="shared_name">Nombre del recurso en red</param>
        public static void ShareFolder(string app_path, string user_name, string shared_name)
        {
            // Create a ManagementPath and scope object that will control the creating of the share
            ManagementPath path = new ManagementPath();
            path.Server = Environment.MachineName; // Change this to be the machine name of the local computer
            path.NamespacePath = @"root\CIMV2";
            ManagementScope scope = new ManagementScope(path);

            NTAccount ntAccount = new NTAccount(Environment.MachineName, user_name);
            SecurityIdentifier sid =
            (SecurityIdentifier)ntAccount.Translate(typeof(SecurityIdentifier));
            byte[] sidArray = new byte[sid.BinaryLength];
            sid.GetBinaryForm(sidArray, 0);

            //ManagementObject to represent the group to be added as a trustee
            ManagementObject newGroup = new ManagementClass(scope,
            new ManagementPath("Win32_Trustee"), null).CreateInstance();
            newGroup["Domain"] = Environment.MachineName;
            newGroup["Name"] = user_name;
            newGroup["SID"] = sidArray;

            // ManagementObject to represent the new group to add to the acle of the share
            ManagementObject aceGroup = new ManagementClass(scope,
            new ManagementPath("Win32_ACE"), null).CreateInstance();
            aceGroup["AccessMask"] = 2032127;
            aceGroup["AceFlags"] = 3;
            aceGroup["AceType"] = 0;
            aceGroup["Trustee"] = newGroup;

            // Management class to modify the ACL
            ManagementObject secDescriptor = new ManagementClass(scope,
            new ManagementPath("Win32_SecurityDescriptor"),
            null).CreateInstance();
            secDescriptor["ControlFlags"] = 4;
            secDescriptor["DACL"] = new ManagementObject[] { aceGroup };

            // create a directory
            if (!Directory.Exists(app_path))
                throw new Exception("La ruta " + app_path + " no existe.");

            // Create a ManagementClass object
            ManagementClass managementClass = new ManagementClass("Win32_Share");
            // Create ManagementBaseObjects for in and out parameters
            ManagementBaseObject inParams = managementClass.GetMethodParameters("Create");
            ManagementBaseObject outParams;
            // Set the input parameters
            inParams["Description"] = "IQ Shared Folders";
            inParams["Name"] = shared_name;
            inParams["Path"] = app_path;
            inParams["Type"] = 0x0; // Disk Drive
            inParams["Access"] = secDescriptor;
            // Invoke the method on the ManagementClass object
            outParams = managementClass.InvokeMethod("Create", inParams, null);
            // Check to see if the method invocation was successful
            if ((uint)(outParams.Properties["ReturnValue"].Value) != 0)
            {
                throw new Exception("Imposible compartir el directorio.");
            }
        }


        /// <summary>
        /// Asigna los permisos locales (no confundir con los permisos de red) de la carpeta
        /// <paramref name="app_path"/>. El usuario es <paramref name="domain_name"/>\<paramref name="user_name"/>
        /// y se le asigna control total sobre la carpeta.
        /// </summary>
        /// <param name="app_path">Ruta al directorio</param>
        /// <param name="user_name">Nombre de usuario</param>
        /// <param name="domain_name">Nombre de dominio</param>
        public static void LocalFolderPermissions(string app_path, string user_name, string domain_name)
        {
            DirectoryInfo dinfo = new DirectoryInfo(app_path);
            DirectorySecurity dsec = dinfo.GetAccessControl();

            const System.Security.AccessControl.InheritanceFlags inhFlags =
            System.Security.AccessControl.InheritanceFlags.ContainerInherit |
            System.Security.AccessControl.InheritanceFlags.ObjectInherit;

            System.Security.AccessControl.FileSystemAccessRule AccessRule =
            new System.Security.AccessControl.FileSystemAccessRule
            (domain_name + @"\" + user_name, System.Security.AccessControl.FileSystemRights.FullControl, inhFlags,
             System.Security.AccessControl.PropagationFlags.None,
             System.Security.AccessControl.AccessControlType.Allow);

            dsec.AddAccessRule(AccessRule);

            dinfo.SetAccessControl(dsec);
        }
    }
}
