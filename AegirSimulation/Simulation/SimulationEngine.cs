using AegirDataTypes.Simulation;
using AegirSimulation.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirSimulation.Simulation
{
    public class SimulationEngine
    {
        public Scenegraph Scene { get; private set; }
        public SimulationStep latestDataSet { get; private set; }

        public SimulationEngine(Scenegraph scene)
        {
            this.Scene = scene;
            latestDataSet = new SimulationStep();
        }
        public void StepSimulation(long deltaTime)
        {

        }
    }
}
