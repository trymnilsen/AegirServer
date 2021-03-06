﻿using AegirMessages;
using AegirMessenger;
using AegirServer.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AegirServer.Runtime
{
    public class ModuleHost
    {
        private Task workerThread;

        public EventWaitHandle ExitHandle { get; private set; }
        public AbstractModule HostedModule { get; private set; }
        /// <summary>
        /// Create a new host for this module
        /// </summary>
        /// <param name="mod">The module to host</param>
        public ModuleHost(AbstractModule mod, ServerContext context, Messenger messenger, BaseConfiguration config)
        {
            this.HostedModule = mod;
            this.ExitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            mod.Messenger = messenger;
            mod.SetConfiguration(config);
            mod.SetContext(context);
            mod.OnFinishedStopping += env_OnFinishedStopping;
        }

        void env_OnFinishedStopping(object sender, EventArgs e)
        {
            Debug.WriteLine("Module Stopped: " + HostedModule.ToString());
            this.ExitHandle.Set();
        }
        public void Start()
        {
            Task worker = new Task(this.HostedModule.Init, 
                                   TaskCreationOptions.LongRunning);
            worker.Start();
        }
        public void Stop()
        {
            HostedModule.Stop();
        }
    }
}
