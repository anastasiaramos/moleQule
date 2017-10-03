using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace moleQule.Library
{
     public static class Documents
    {
        #region Business Methods

        public static string GetRootPath()
        {
            return AppControllerBase.Reg32GetServerPath();
        }        

        #endregion

        #region General Documents Business Methods

        /// <summary>
        /// Función que copia un documento desde <paramref name="sourcePath"/>
        /// a <paramref name="destinationPath"/>. No sobreescribe los archivos si existen.
        /// </summary>
        /// <param name="sourcePath">Ruta del fichero origen</param>
        /// <param name="destinationPath">Ruta del fichero destino</param>
        private static string SaveDoc(string sourcePath, string destinationPath)
        {
            
            try
            {
                System.IO.File.Copy(sourcePath, destinationPath, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }

            return destinationPath;
        }

        /// <summary>
        /// Función que copia un documento desde <paramref name="sourcePath"/>
        /// a <paramref name="destinationPath"/>
        /// </summary>
        /// <param name="sourcePath">Ruta del fichero origen</param>
        /// <param name="destinationPath">Ruta del fichero destino</param>
        private static string SaveDoc(string sourcePath, string destinationPath, bool overwrite)
        {
            
            try
            {
                System.IO.File.Copy(sourcePath, destinationPath, overwrite);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }

            return destinationPath;
        }

        /// <summary>
        /// Función que borra una foto de la carpeta correspondiente
        /// </summary>
        /// <param name="path">Ruta del fichero de imagen</param>
        private static void DeleteDoc(string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }

        /// <summary>
        /// Función que muestra el documento
        /// </summary>
        /// <param name="path">Ruta del fichero</param>
        private static void ShowDoc(string path)
        {
            //WindowsIdentity id = new WindowsIdentity(
        }

        #endregion

        #region Application Documents Business Methods

        /// <summary>
        /// Almacena un documento en la carpeta del servidor
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        public static void Save(string sourcePath, string destinationPath, string fileName)
        {
            string ruta = GetRootPath() + @"\" + destinationPath;

            if (!Directory.Exists(ruta))
                Directory.CreateDirectory(ruta);

            ruta += @"\";
            ruta += fileName;

            SaveDoc(sourcePath, ruta);
        }

        /// <summary>
        /// Función que muestra un documento obtenido a partir de la carpeta de instalación de la aplicación
        /// </summary>
        /// <param name="path">Ruta del fichero</param>
        public static void Show(string fileName, string sourcePath)
        {
            string ruta = GetRootPath() + @"\" + sourcePath + @"\" + fileName;
            ShowDoc(ruta);
        }

        /// <summary>
        /// Función que borra un documento obtenido a partir de la carpeta de instalación de la aplicación
        /// </summary>
        /// <param name="path">Ruta del fichero</param>
        public static void Delete(string fileName, string sourcePath)
        {
            string ruta = GetRootPath() + @"\" + sourcePath + @"\" + fileName;
            DeleteDoc(ruta);
        }

        /// <summary>
        /// Función que renombra un documento con otro nombre dentro de la misma carpeta
        /// </summary>
        /// <param name="path">Ruta del fichero de imagen</param>
        public static void Rename(string sourceFileName, string destinationFileName, string Path)
        {
            string source = GetRootPath() + @"\" + Path + @"\" + sourceFileName;
            string dest = GetRootPath() + @"\" + Path + @"\" + destinationFileName;
            SaveDoc(source, dest);
            DeleteDoc(source);
        }

        #endregion
    }
}
