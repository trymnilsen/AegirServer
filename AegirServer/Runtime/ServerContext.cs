using AegirServer.Config;
using AegirServer.Project;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Runtime
{
    public class ServerContext
    {
        private BaseConfiguration config;

        public Workspace Workspace { get; private set; }

        public ServerContext(BaseConfiguration config)
        {
            this.config = config;
            this.Workspace = new Workspace(config);
        }
    }
}
