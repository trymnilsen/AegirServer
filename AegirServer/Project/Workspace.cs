using AegirMessages.Project;
using AegirServer.Config;
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

        public ObservableCollection<SimulationProject> Projects { get; private set; }
        public SimulationProject CurrentProject { get; private set; }

        public Workspace(BaseConfiguration config)
        {
            Projects = new ObservableCollection<SimulationProject>();
            Projects.CollectionChanged += Projects_Changed;
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
            foreach(string projectPath in workspace_data.ProjectPaths)
            {
                SimulationProject project = new SimulationProject();
                project.Load(projectPath);
                Projects.Add(project);
            }
        }
        /// <summary>
        /// Creates a new project and sets it as the current one, also updates the recent projects list
        /// </summary>
        public void CreateProject()
        {
            SimulationProject newProject = new SimulationProject();

        }
        
    }
}
