using AegirMessenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirMessages.Simulation
{
    public class NewSimulationCommand : Message
    {
        public object Command { get; private set; }
        public NewSimulationCommand(object command)
            : base("NewSimulationCommand", EChannel.SIMULATION)
        {
            this.Command = command;
        }
    }
}
