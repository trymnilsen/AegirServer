using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegirMessenger;

namespace AegirMessages.Simulation
{
    public class SimulationFrameComplete : Message
    {
        public SimulationFrameComplete()
            :base("SimulationFrameComplete", EChannel.SIMULATION)
        {

        }
    }
}
