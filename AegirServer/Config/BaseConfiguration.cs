using AegirServer.IO.Mount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Config
{
    public class BaseConfiguration
    {
        public string Name { get; set; }
        public string HttpEndpoint { get; set; }
        public MountPoint[] MountPoints { get; set; }

        public BaseConfiguration()
        {
            this.Name = "Foobar";
            this.HttpEndpoint = "http://localhost/";
        }
    }
}
