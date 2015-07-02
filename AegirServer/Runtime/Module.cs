using AegirServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AegirServer.Runtime
{
    public abstract class Module
    {
        public abstract void Startup();
        public abstract void SetConfiguration(BaseConfiguration config);
        public abstract void Shutdown();
    }
}
