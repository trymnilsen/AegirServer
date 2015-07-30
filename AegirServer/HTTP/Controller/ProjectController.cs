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
        public override void IndexAction()
        {
            var projects = ServerContext.Workspace.Projects;
            this.SetSuccessfulContent(projects);
        }
        public override void GetAction(string[] args)
        {
            string idToGet = args[0];
            SimulationProject[] projects = ServerContext.Workspace.Projects.Where(x => x.GUID.ToString() == idToGet).ToArray();
            if (projects.Length < 1)
            {
                throw new HTTPException(HttpStatusCode.NotFound);
            }
            this.SetSuccessfulContent(projects);
        }
    }
}
