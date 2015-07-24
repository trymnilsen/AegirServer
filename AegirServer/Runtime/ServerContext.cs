using AegirServer.Config;
using AegirServer.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Runtime
{
    public class ServerContext
    {
        private BaseConfiguration config;

        public SimulationProject[] RecentProjects { get; private set; }
        public SimulationProject CurrentProject { get; private set; }

        public ServerContext(BaseConfiguration config)
        {
            this.config = config;
        }
    }
}
