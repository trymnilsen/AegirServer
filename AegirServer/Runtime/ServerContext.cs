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
        private Workspace workspace;

        public ServerContext(BaseConfiguration config)
        {
            this.config = config;
            this.workspace = new Workspace(config);
            
        }
    }
}
