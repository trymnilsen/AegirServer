using AegirMessages;
using AegirServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AegirServer.Runtime
{

    public abstract class AbstractModule
    {
        private Dispatcher dispatcher;
        /// <summary>
        /// Retrieve the messenger assigned to this module
        /// </summary>
        public Messenger Messenger { get; private set; }
        /// <summary>
        /// Messages this module has received based on its subscriptions with the messenger
        /// </summary>
        public Postbox Messages { get; private set; }
        public abstract void Startup();
        public abstract void Run();
        public abstract void SetConfiguration(BaseConfiguration config);
        public abstract void Stop();
        /// <summary>
        /// Initializes this Module from the work thread
        /// </summary>
        public void Init()
        {
            //Get a dispatcher for this thread
            dispatcher = Dispatcher.CurrentDispatcher;
            //init
            this.Startup();
            //Run it
            this.Run();
        }
        public void SetMessenger(Messenger messenger)
        {
            this.Messenger = messenger;
        }
        protected void NotifyModuleStopped()
        {
            FinishedStopHandler stopEvent = OnFinishedStopping;
            if(stopEvent != null)
            {
                stopEvent(this, new EventArgs());
            }
        }

        public delegate void FinishedStopHandler(object sender, EventArgs e);
        public event FinishedStopHandler OnFinishedStopping;
    }
}
