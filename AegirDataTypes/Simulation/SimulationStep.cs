using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirDataTypes.Simulation
{
    public class SimulationStep
    {
        public float BoatSize { get; set; }
        public string Name { get; set; }
        public SimulationStep()
        {
            BoatSize = 14.57f;
            Name = "Foobar of the seas";
        }
    }
}
