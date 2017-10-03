using System;
using System.Collections.Generic;
using System.Text;

using moleQule.Library.Application;

namespace moleQule.BaQup
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AppController.SetDBPassword();
                Principal.initnHManager();
				AppController.AutoBackup(true);
                Console.WriteLine("Copia de seguridad realizada con EXITO");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
