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
            CLIOptions options = new CLIOptions();

            if (Parser.Default.ParseArguments(args, options))
            {
                CLIAppStart appStarter = new CLIAppStart(options);
                appStarter.Start();
            }
        }
    }
}
