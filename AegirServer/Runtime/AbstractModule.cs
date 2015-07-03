using AegirServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AegirServer.Runtime
{

    public abstract class AbstractModule
    {
        public delegate void FinishedStopHandler(object sender, EventArgs e);
        public event FinishedStopHandler OnFinishedStopping;

        public abstract void Startup();
        public abstract void SetConfiguration(BaseConfiguration config);
        public abstract void Stop();

        protected void NotifyFinishedStopping()
        {
            FinishedStopHandler stopEvent = OnFinishedStopping;
            if(stopEvent != null)
            {
                stopEvent(this, new EventArgs());
            }
        }
    }
}
