using AegirSimulation.Scene;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirDataTypes.Workspace
{
    public class ProjectData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string VesselId { get; set; }
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public Scenegraph Scene { get; set; }
        public ProjectData()
        {

        }
        public ProjectData(string name, string description)
        {
            ProjectName = name;
            Description = description;
            CreatedDate = DateTime.Now;
            LastModifiedDate = DateTime.Now;
        }
    
    }
}
