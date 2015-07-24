﻿using AegirServer.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Project
{
    public class Workspace
    {
        private BaseConfiguration configuration;
        private WorkspaceFile workspace_data;

        public SimulationProject[] RecentProjects { get; private set; }
        public SimulationProject CurrentProject { get; private set; }

        public Workspace(BaseConfiguration config)
        {
            this.configuration = config;
            workspace_data = WorkspaceFile.LoadWorkspace(config.WorkspaceFileName);
        }
        public void LoadProjects()
        {
            foreach(string projectPath in workspace_data.RecentProjectsPaths)
            {

            }
        }
    }
}
