using AegirDataTypes.Simulation;
using AegirMessages.Simulation;
using AegirServer.Runtime;
using AegirSimulation.Scene;
using AegirSimulation.Simulation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AegirServer.Module
{
    public class SimulationModule : AbstractModule
    {
        public Scenegraph Scene { get; private set; }
        public SimulationEngine Simulation { get; private set; }

        private Stopwatch stopwatch;
        private bool isRunning;
        private int frameTime;

        public SimulationModule()
        {
            stopwatch = new Stopwatch();
            isRunning = false;
        }

        public override void Startup()
        {
            this.Scene = new Scenegraph();
            this.Simulation = new SimulationEngine(Scene);
        }
        public override void Run()
        {
            stopwatch.Start();
            long nextTime = 0;
            long lastUpdate = 0;
            long deltaTime = 0;
            frameTime = 33;
            isRunning = true;
            while(isRunning)
            {
                long elapsedTime = stopwatch.ElapsedMilliseconds;
                //Is it time of another update?
                if(elapsedTime >= nextTime)
                {
                    //When will our next update be?
                    nextTime += frameTime;

                    deltaTime = (elapsedTime - lastUpdate);
                    Update(deltaTime);
                    lastUpdate = stopwatch.ElapsedMilliseconds;
                }
                else
                {
                    int sleepTime = (int)(nextTime - elapsedTime);
                    //Sanity Check
                    if(sleepTime>0)
                    {
                        try
                        {
                            Thread.Sleep(sleepTime);
                        }
                        catch(ThreadInterruptedException ie)
                        {
                            //Error here
                        }
                    }

                }
            }
            this.NotifyModuleStopped();
        }

        public override void SetConfiguration(Config.BaseConfiguration config)
        {
            Debug.WriteLine("TODO add config to Simulation");
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        private void Update(long deltaTime)
        {
            Debug.WriteLine("Updating");
            Simulation.StepSimulation(deltaTime);
            //The simulation is complete
            this.Messenger.Publish(new SimulationFrameComplete(Simulation.latestDataSet));
        }
    }
}
