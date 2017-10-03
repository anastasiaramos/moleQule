using System;
using System.Collections.Generic;
using System.Text;

namespace moleQule.Config
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                switch (args[0])
                {
                    case "-a":
                        if (args.Length != 3)
                        {   
                            Console.Error.WriteLine("Usage:");
                            Console.Error.WriteLine("molQonfig -a \"source\" \"assembly\"");
                            return;
                        }
                        break;

                    case "-m":
                        if (args.Length != 2)
                        {
                            Console.Error.WriteLine("Usage:");
                            Console.Error.WriteLine("molQonfig -m \"source\"");
                            return;
                        }
                        break;

					case "-e":
						if (args.Length != 3)
						{
							Console.Error.WriteLine("Usage:");
							Console.Error.WriteLine("molQonfig -e \"source\" \"assembly\"");
							return;
						}
						break;
                }

                nHConfig.CreateNHFiles(args[0], args[1], args[1], (args.Length == 3) ? args[2] : string.Empty);
            }
            catch (Exception ex)
            {
                Console.Error.Write(ex.Message);
            }
        }

    }
}
