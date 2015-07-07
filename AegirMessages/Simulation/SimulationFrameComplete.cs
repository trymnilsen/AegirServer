using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegirMessenger;
using AegirDataTypes.Simulation;

namespace AegirMessages.Simulation
{
    public class SimulationFrameComplete : Message
    {
        public SimulationStep data { get; private set; }
        public SimulationFrameComplete(SimulationStep data)
            :base("SimulationFrameComplete", EChannel.SIMULATION)
        {
            this.data = data;
        }
    }
}
