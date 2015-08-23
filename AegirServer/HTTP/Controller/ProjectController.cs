using AegirServer.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.HTTP.Controller
{
    public class ProjectController : HTTPController
    {
        private List<string> identifiers;

        public ProjectController()
        {
            this.identifiers = new List<string>();
            this.identifiers.Add("vesselguid");
            this.identifiers.Add("vesselname");
        }
        public override void IndexAction()
        {
            var projects = ServerContext.Workspace.Projects;
            this.SetSuccessfulContent(projects);
        }
        public override void GetAction(string[] args)
        {
            string idToGet = args[0];
            SimulationProject[] projects = null;
            if (this.identifiers.Contains(idToGet) && args.Length>1)
            {
                idToGet = args[1];
                if(args[0] == "vessel")
                {
                    projects = GetByVessel(args[1]);
                }
                else
                {
                    projects = GetByGuid(args[1]);
                }
            }
            else
            {
                projects = GetByGuid(args[0]);
            }
            this.SetSuccessfulContent(projects);
        }
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
        public override void PostAction()
        {
            base.PostAction();
        }
    }
}
