﻿using AegirServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AegirServer.Runtime
{
    public class EnvironmentHost
    {
        private Task workerThread;
        private EventWaitHandle exitHandle;

        public Environment Environment { get; private set; }
        /// <summary>
        /// Create a new host for this environment
        /// </summary>
        /// <param name="env">The environment to host</param>
        public EnvironmentHost(Environment env, BaseConfiguration config)
        {
            this.Environment = env;
            env.SetConfiguration(config);
        }
        public void Start()
        {
            Task worker = new Task(this.Environment.Startup, 
                                   TaskCreationOptions.LongRunning);
            worker.Start();
        }
        public void Stop()
        {
            Environment.Shutdown();
        }
    }
}
