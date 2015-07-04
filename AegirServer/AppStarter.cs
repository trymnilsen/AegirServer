using AegirMessages;
using AegirServer.CLI;
using AegirServer.Config;
using AegirServer.Module;
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
    /// <summary>
    /// The Appstarter class is used to bootstrap the application
    /// as well as loading and starting all the modules
    /// </summary>
    public class AppStarter
    {
        private BaseConfiguration config;
        private Options options;
        private Messenger messenger;

        private List<ModuleHost> modHosts;
        /// <summary>
        /// Creates a new Appstart instance
        /// </summary>
        /// <param name="options"></param>
        public AppStarter(Options options)
        {
            this.options = options;
            this.modHosts = new List<ModuleHost>();
            this.messenger = new Messenger();
        }
        /// <summary>
        /// Starts the application and modules
        /// </summary>
        public void Start()
        {
            if (options.Verbose) Console.WriteLine("Filename: {0}", options.InputFile);
            //Load configuration
            ConfigFile configurationFile = new ConfigFile("config.json");
            BaseConfiguration config = configurationFile.Load();
            this.config = config;
            //Set up ctrl + c handling
            Console.CancelKeyPress += Console_CancelKeyPress;
            //Start Subsystems
            StartSubsystem(new HTTPModule());
            //StartSubsystem(new SimulationModule());


            Console.WriteLine("Press any key to close");
            Console.ReadKey();
            this.StopModules();
        }

        void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            this.StopModules();
        }

        private void StartSubsystem(AbstractModule mod)
        {
            Console.WriteLine("Starting: " + mod.ToString());
            ModuleHost host = new ModuleHost(mod, messenger, this.config);
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
