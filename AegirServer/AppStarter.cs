﻿using AegirServer.CLI;
using AegirServer.Config;
using AegirServer.HTTP;
using AegirServer.Runtime;
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
        private BaseConfiguration config;
        private Options options;

        private List<EnvironmentHost> envHosts;

        public AppStarter(Options options)
        {
            this.options = options;
            this.envHosts = new List<EnvironmentHost>();
        }
        public void Start()
        {
            if (options.Verbose) Console.WriteLine("Filename: {0}", options.InputFile);
            //Load configuration
            ConfigFile configurationFile = new ConfigFile("config.json");
            BaseConfiguration config = configurationFile.Load();
            this.config = config;
            StartSubsystem(new HTTPEnvironment());
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
        private void StartSubsystem(Environment env)
        {
            EnvironmentHost host = new EnvironmentHost(env, this.config);
            envHosts.Add(host);
            host.Start();
        }
    }
}
