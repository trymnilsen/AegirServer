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
        public SimulationProject[] RecentProjects { get; private set; }
        public SimulationProject CurrentProject { get; private set; }

        public ServerContext(BaseConfiguration config)
        {
            this.config = config;
            this.workspace = loadWorkspace(config.WorkspaceFileName);
        }
        private Workspace loadWorkspace(string workspacename)
        {
            string fileContent = File.ReadAllText(workspacename);
            //TODO: for now escape
            fileContent = fileContent.Replace(@"\", @"\\");
            Workspace workspace = JsonConvert.DeserializeObject<Workspace>(fileContent);
            return workspace;
        }
    }
}
