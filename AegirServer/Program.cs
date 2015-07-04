using AegirServer.CLI;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get flags from passed command args
            Options options = new Options();

            if (Parser.Default.ParseArguments(args, options))
            {
                AppStarter appStarter = new AppStarter(options);
                appStarter.Start();
            }

            if(System.Diagnostics.Debugger.IsAttached)
            { 
                Console.ReadKey(true);
            }
        }
    }
}
