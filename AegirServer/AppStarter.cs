using AegirServer.CLI;
using AegirServer.Config;
using AegirServer.HTTP;
using AegirServer.Runtime;
using AegirServer.Websocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AegirServer
{
    public class AppStarter
    {
        private BaseConfiguration config;
        private Options options;

        private List<ModuleHost> modHosts;

        public AppStarter(Options options)
        {
            this.options = options;
            this.modHosts = new List<ModuleHost>();
        }
        public void Start()
        {
            if (options.Verbose) Console.WriteLine("Filename: {0}", options.InputFile);
            //Load configuration
            ConfigFile configurationFile = new ConfigFile("config.json");
            BaseConfiguration config = configurationFile.Load();
            this.config = config;
            StartSubsystem(new HTTPModule());
           // Console.WriteLine("Press any key to close");
            Console.ReadKey();
            this.StopModules();
        }
        private void StartSubsystem(Module mod)
        {
            Console.WriteLine("Starting: " + mod.ToString());
            ModuleHost host = new ModuleHost(mod, this.config);
            modHosts.Add(host);
            host.Start();
        }
        private void StopModules()
        {
            //Get all wait handles
            WaitHandle[] waitHandles = new WaitHandle[modHosts.Count];
            for (int i = 0; i < modHosts.Count; i++)
            {
                Console.WriteLine("Stopping Module - " + modHosts[i].HostedModule.ToString());
                waitHandles[i] = modHosts[i].ExitHandle;
                modHosts[i].Stop();
            }
            WaitHandle.WaitAll(waitHandles);
            Console.WriteLine("All Modules Stopped");
        }
    }
}
