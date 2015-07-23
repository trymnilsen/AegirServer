using AegirMessages;
using AegirMessenger;
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
        public BaseConfiguration Configuration { get; private set; }
        public Dispatcher Dispatcher { get; private set; }
        /// <summary>
        /// Retrieve the messenger assigned to this module
        /// </summary>
        public Messenger Messenger { get; set; }
        /// <summary>
        /// Messages this module has received based on its subscriptions with the messenger
        /// </summary>
        public Postbox Messages { get; protected set; }

        public abstract void Startup();
        public abstract void Run();
        public abstract void Stop();
        /// <summary>
        /// Initializes this Module from the work thread
        /// </summary>
        public void Init()
        {
            //Get a dispatcher for this thread
            Dispatcher = Dispatcher.CurrentDispatcher;
            //init
            this.Startup();
            //Run it
            this.Run();
        }
        protected void NotifyModuleStopped()
        {
            FinishedStopHandler stopEvent = OnFinishedStopping;
            if(stopEvent != null)
            {
                stopEvent(this, new EventArgs());
            }
        }
        public virtual void SetConfiguration(BaseConfiguration config)
        {
            this.Configuration = config;
        }

        public delegate void FinishedStopHandler(object sender, EventArgs e);
        public event FinishedStopHandler OnFinishedStopping;
    }
}
