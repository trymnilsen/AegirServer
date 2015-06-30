using AegirServer.CLI.Flags;
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
            FlagSet flags = new FlagSet();
            flags.ParseFlags(args);

            AegirServer server = new AegirServer(flags);
            server.Start();
        }
    }
}
