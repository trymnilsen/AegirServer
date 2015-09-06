using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirDataTypes.Workspace
{
    public class ProjectData
    {
        public ProjectData()
        {

        }
        public ProjectData(string name, string vessel)
        {
            this.ProjectName = name;
            this.VesselId = vessel;
        }
        public string ProjectName { get; set; }
        public string VesselId { get; set; }
    }
}
