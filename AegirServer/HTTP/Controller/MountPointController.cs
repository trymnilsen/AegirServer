using AegirServer.IO.Mount;
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
        public override HttpStatusCode GetAction(string[] args)
        {
            MountPoint[] mountPoints = this.Configuration.MountPoints;
            string outData = JsonConvert.SerializeObject(mountPoints);
            this.SetOutput(outData);
            return HttpStatusCode.OK;
        }
    }
}
