using AegirDataTypes.Vessel;
using AegirDataTypes.Workspace;
using AegirMessages.Project;
using AegirServer.Config;
using AegirServer.Persistence;
using AegirServer.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Project
{
    public class Workspace
    {
        private BaseConfiguration configuration;
        private WorkspaceFile workspace_data;

        public SimulationProject CurrentProject { get; private set; }

        public Workspace(BaseConfiguration config)
        {

            //Create new project
            this.configuration = config;
            workspace_data = WorkspaceFile.LoadWorkspace(config.WorkspaceFileName);
            LoadProjects();
        }

        private void Projects_Changed(object sender, NotifyCollectionChangedEventArgs e)
        {
            
        }

        public void LoadProjects()
        {
            //foreach(string projectPath in workspace_data.ProjectPaths)
            //{
            //    SimulationProject project = new SimulationProject();
            //    project.Load(projectPath);
            //}
        }
        /// <summary>
        /// Creates a new project and sets it as the current one, also updates the recent projects list
        /// </summary>
        public void CreateProject(ProjectData details)
        {
            var vessel = VesselConfigurationService.Vessels[0];

            using (var context = new PersistanceContext())
            {
                context.Projects.Add(details);
                context.SaveChanges();
            }

        }
        
    }
}
