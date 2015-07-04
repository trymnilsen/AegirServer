using AegirMessages;
using AegirServer.Config;
using System;
using System.Collections.Generic;
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
        public ModuleHost(AbstractModule mod, Messenger messenger, BaseConfiguration config)
        {
            this.HostedModule = mod;
            this.ExitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            mod.SetMessenger(messenger);
            mod.SetConfiguration(config);
            mod.OnFinishedStopping += env_OnFinishedStopping;
        }

        void env_OnFinishedStopping(object sender, EventArgs e)
        {
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
