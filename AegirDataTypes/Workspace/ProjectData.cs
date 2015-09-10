﻿using System;
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

        public ProjectData()
        {

        }
        public ProjectData(string name, string vessel)
        {
            this.ProjectName = name;
            this.VesselId = vessel;
        }
    }
}
