using AegirServer.Project;
using AegirDataTypes.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace AegirServer.HTTP.Controller
{
    public class ProjectController : HTTPController
    {
        private Dictionary<string, Func<string, SimulationProject[]>> identifiers;

        public ProjectController()
        {
            this.identifiers = new Dictionary<string, Func<string, SimulationProject[]>>();
            this.identifiers.Add("projectname",this.GetByName);
            this.identifiers.Add("last_",this.GetByName);
        }
        public override void IndexAction()
        {
            var projects = ServerContext.Workspace.Projects;
            this.SetSuccessfulContent(projects, HttpStatusCode.OK);
        }
        public override void GetAction(string[] args)
        {
            string idToGet = args[0];
            SimulationProject[] projects = null;
            //We only allow one query parameter for now
            if (Request.QueryString.Count > 0)
            {
                if(this.identifiers.ContainsKey(Request.QueryString.Keys[0]))
                {
                    string[] paramValues = Request.QueryString.GetValues(0);
                    if(paramValues.Length>0)
                    {
                        idToGet = paramValues[0];
                    }
                    projects = this.identifiers[Request.QueryString.Keys[0]](idToGet);
                }
                else
                {
                    throw new HTTPException(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                projects = GetByGuid(args[0]);
            }
            this.SetSuccessfulContent(projects,HttpStatusCode.OK);
        }
        public override void PostAction()
        {
            string postData = GetTextData(Request);
            ProjectData projectData = JsonConvert.DeserializeObject<ProjectData>(postData);
            Workspace workspace = ServerContext.Workspace;
            workspace.CreateProject(projectData);
        }
        private SimulationProject[] GetByName(string name)
        {

            var projects = ServerContext.Workspace.Projects.Where(x => x.Name.ToLower() == name.ToLower()).ToArray();
            if (projects.Length < 1)
            {
                throw new HTTPException(HttpStatusCode.NotFound);
            }
            return projects;
        }
        //private SimulationProject[] GetByLastModified(string lastNum)
        //{
            
        //}
        private SimulationProject[] GetByVessel(string guid)
        {
            var projects = ServerContext.Workspace.Projects.Where(x => x.Vessel.Id.ToString() == guid).ToArray();
            if (projects.Length < 1)
            {
                throw new HTTPException(HttpStatusCode.NotFound);
            }
            return projects;
        }
        private SimulationProject[] GetByGuid(string guid)
        {
            //No identifier given assume guid
            var projects = ServerContext.Workspace.Projects.Where(x => x.GUID.ToString() == guid).ToArray();
            if (projects.Length < 1)
            {
                throw new HTTPException(HttpStatusCode.NotFound);
            }
            return projects;
        }


    }
}
