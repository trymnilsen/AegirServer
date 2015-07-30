using AegirServer.IO.Mount;
using AegirServer.Module;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.HTTP.Controller
{
    public class MountPointController : HTTPController
    {
        public override void IndexAction()
        {
            MountPoint[] mountPoints = this.Configuration.MountPoints;
            this.SetSuccessfulContent(mountPoints);
        }
        public override void GetAction(string[] args)
        {
            string idToGet = args[0];
            MountPoint[] mountPoint = Configuration.MountPoints.Where(x => x.Id.ToString() == idToGet).ToArray();
            if(mountPoint.Length < 1)
            {
                throw new HTTPException(HttpStatusCode.NotFound);
            }
            this.SetSuccessfulContent(mountPoint);
        }
    }
}
