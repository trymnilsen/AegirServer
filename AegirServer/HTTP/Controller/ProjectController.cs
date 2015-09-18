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
using AegirServer.Persistence;
using AegirDataTypes.Vessel;

namespace AegirServer.HTTP.Controller
{
    public class ProjectController : HTTPController
    {
        private Dictionary<string, Func<string, ProjectData>> identifiers;

        public ProjectController()
        {
            this.identifiers = new Dictionary<string, Func<string, ProjectData>>();
            this.identifiers.Add("projectname",this.GetByName);
        }
        public override void IndexAction()
        {
            using (var context = new PersistanceContext())
            {
                this.SetSuccessfulContent(context.Projects, HttpStatusCode.OK);
            }
        }
        public override void GetAction(string[] args)
        {
            string idToGet = args[0];
            ProjectData project = null;
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
                    project = this.identifiers[Request.QueryString.Keys[0]](idToGet);
                }
                else
                {
                    throw new HTTPException(HttpStatusCode.BadRequest);
                }
            }
            else
            {
                project = GetById(args[0]);
            }
            this.SetSuccessfulContent(project,HttpStatusCode.OK);
        }
        public override void PostAction()
        {
            string postData = GetTextData(Request);
            ProjectData projectData = JsonConvert.DeserializeObject<ProjectData>(postData);
            //Check if a project with this name already exists
            using (var context = new PersistanceContext())
            {
                ProjectData data = context.Projects.FirstOrDefault(x=> x.ProjectName == projectData.ProjectName);
                if (data != null)
                {
                    this.Response.AppendHeader("location", "/project/" + data.Id);
                    throw new HTTPException(HttpStatusCode.SeeOther);
                }
            }
            Workspace workspace = ServerContext.Workspace;
            workspace.CreateProject(projectData);

            //Just for testing
            this.SetSuccessfulContent(projectData, HttpStatusCode.Created);
        }

        private ProjectData GetByName(string name)
        {
            using (var context = new PersistanceContext())
            {
                var project = context.Projects.FirstOrDefault(x => x.ProjectName == name);
                if (project == null)
                {
                    throw new HTTPException(HttpStatusCode.NotFound);
                }
                return project;
            }
        }
        private ProjectData GetById(string id)
        {
            using (var context = new PersistanceContext())
            {
                int projId = int.Parse(id);
                var project = context.Projects.FirstOrDefault(x => x.Id == projId);
                if (project == null)
                {
                    throw new HTTPException(HttpStatusCode.NotFound);
                }
                return project;
            }
        }
        //private SimulationProject[] GetByLastModified(string lastNum)
        //{
            
        //}
        //private ProjectData GetByVessel(string guid)
        //{
        //    var projects = ServerContext.Workspace.Projects.Where(x => x.Vessel.Id.ToString() == guid).ToArray();
        //    if (projects.Length < 1)
        //    {
        //        throw new HTTPException(HttpStatusCode.NotFound);
        //    }
        //    return projects;
        //}

    }
}
