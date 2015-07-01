using AegirServer.CLI;
using AegirServer.Config;
using AegirServer.HTTP;
using AegirServer.Websocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer
{
    public class AppStarter
    {
        private Options options;
        public AppStarter(Options options)
        {
            this.options = options;
        }
        public void Start()
        {
            if (options.Verbose) Console.WriteLine("Filename: {0}", options.InputFile);
            //Load configuration
            ConfigFile configurationFile = new ConfigFile("foobar.txt");
            BaseConfiguration config = configurationFile.Load();
            if(config!=null)
            {
                Console.WriteLine("Name :" + config.Name);
            }
        }
        private void StartSubsystems()
        {
            //Create our environment
            var CliEnv          = new CLIEnvironment();
            var HTTPEnv         = new HTTPEnvironment();
            var WebsocketEnv    = new WebsocketEnvironment();


        }
    }
}
