using AegirDataTypes.Simulation;
using AegirSimulation.Command;
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

        public void StepSimulation(SimulationTime deltaTime, SimulationCommand[] commands)
        {
            //Run precomponents of all commands
            for (int i = 0; i < commands.Length; i++)
            {
                commands[i].PreComponentCalcuation(this.Scene, deltaTime);
            }
            this.Scene.RunComponents();
            //Run precomponents of all commands
            for (int i = 0; i < commands.Length; i++)
            {
                commands[i].PostComponentCalculation(this.Scene, deltaTime);
            }
        }
    }
}
