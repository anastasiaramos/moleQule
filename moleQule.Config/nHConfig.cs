using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace moleQule.Config
{
    public class nHConfig
    {
        public const string ASM_DIR_NAME = "Asm";
        public const string MODULES_DIR_NAME = "Modules";
        public const string CONFIG_FILE_NAME = "hibernate_nh";
        public const string CONFIG_FILE_EXT = ".cfg.xml";

        public static bool CreateNHFiles(string type, string source, string destination, string conf_name)
        {
            if (source.Substring(source.Length - 1) != "\\")
                source += "\\";

            string sDir;

            switch (type)
            {
                case "-a":

                    sDir = source + ASM_DIR_NAME + "\\" + conf_name;
                    CreateNHMainFiles(sDir, sDir);

                    break;

				/*case "-e":

					sDir = source + ASM_DIR_NAME;
					CreateNHEnvironmentFiles(sDir, sDir);

					break;*/

                case "-m":

                    sDir = source + ASM_DIR_NAME;
                    CreateNHModuleFiles(sDir, sDir);

                    break;
            }

            return true;
        }

        public static bool CreateNHMainFiles(string source, string destination)
        {
            try
            {
                if (source.Substring(source.Length - 1) == "\"")
                    source = source.Substring(0, source.Length - 1);

                if (source.Substring(source.Length - 1) != "\\")
                    source += "\\";

                if (destination.Substring(destination.Length - 1) == "\"")
                    destination = destination.Substring(0, destination.Length - 1);

                if (destination.Substring(destination.Length - 1) != "\\")
                    destination += "\\";

                if (!Directory.Exists(source))
                {
                    Console.WriteLine("ERROR: No se ha encontrado el directorio origen para generación de ficheros de configuración " + source + ".");
                    return false;
                }

                if (!File.Exists(source + CONFIG_FILE_NAME + "0001" + CONFIG_FILE_EXT))
                {
                    Console.WriteLine("La carpeta seleccionada no contiene un modelo de fichero de configuración válido.");
                }

                string line = null;
                string newName = null;
                string[] lines = File.ReadAllLines(source + CONFIG_FILE_NAME + "0001" + CONFIG_FILE_EXT);
                int pos = 0;
                StreamWriter newFile = null;

                for (int numFile = 2; numFile <= 10; numFile++)
                {
                    // Fichero de configuración general

                    newName = destination + CONFIG_FILE_NAME + numFile.ToString("0000") + CONFIG_FILE_EXT;
                    newFile = File.CreateText(newName);
                    pos = 0;

                    while (pos < lines.Length)
                    {
                        line = lines[pos++];
                        newFile.WriteLine(line.Replace("0001", numFile.ToString("0000")));
                    }

                    if (newFile != null) newFile.Close();
                    Console.WriteLine("Fichero " + newName + " generado con éxito.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Generación de ficheros de configuración principales finalizada con EXITO");
            return true;
        }

        public static bool CreateNHModuleFiles(string source, string destination)
        {
            try
            {
                if (source.Substring(source.Length - 1) == "\"")
                    source = source.Substring(0, source.Length - 1);
                
                if (source.Substring(source.Length - 1) != "\\")
                    source += "\\";

                if (destination.Substring(destination.Length - 1) == "\"")
                    destination = destination.Substring(0, destination.Length - 1);

                if (destination.Substring(destination.Length - 1) != "\\")
                    destination += "\\";

                if (!Directory.Exists(source))
                {
                    Console.WriteLine("ERROR: No se ha encontrado el directorio origen para generación de ficheros de configuración " + source + ".");
                    return false;
                }

                string line = null;
                string newName = null;
                int pos = 0;
                DirectoryInfo newDir = null;
                StreamWriter newFile = null;

                for (int numFile = 2; numFile <= 10; numFile++)
                {
                    // Carpetas de ficheros de configuración de objetos

                    string[] fileLines = null;
                    string[] fileEntries = Directory.GetFiles(source + "nh0001\\");

                    if (fileEntries.Length == 0) continue;

                    newName = destination + "nh" + numFile.ToString("0000");
                    if (!Directory.Exists(newName))
                        newDir = Directory.CreateDirectory(newName);

                    // Recorremos todos los ficheros
                    foreach (string fileName in fileEntries)
                    {
                        // Lineas del fichero
                        fileLines = File.ReadAllLines(fileName);
                        newName = fileName.Replace("nh0001", "nh" + numFile.ToString("0000"));
                        newFile = File.CreateText(newName);
                        pos = 0;

                        while (pos < fileLines.Length)
                        {
                            line = fileLines[pos++];
                            newFile.WriteLine(line.Replace("0001", numFile.ToString("0000")));
                        }

                        if (newFile != null) newFile.Close();
                        Console.WriteLine("Fichero " + newName + " generado con éxito.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Generación de ficheros de configuración de módulos finalizada con EXITO");
            return true;
        }
    }
}
