using AegirSimulation.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirSimulation.Command
{
    public abstract class SimulationCommand
    {
        public virtual void PreComponentCalcuation(Scenegraph scene)
        {
            //nothing
        }
        public virtual void PostComponentCalculation(Scenegraph scene)
        {

        }
    }
}
