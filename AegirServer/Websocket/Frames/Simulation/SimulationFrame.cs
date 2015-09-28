using AegirDataTypes.Simulation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Websocket.Frames.Simulation
{
    public class SimulationFrame : MessageFrame
    {

        private SimulationStep data;

        public override string FrameId
        {
            get
            {
                return "SimulationFrame";
            }
        }

        public SimulationFrame(SimulationStep simulationData) 
        {
            this.data = simulationData;
        }

        public override string Serialize()
        {
            return JsonConvert.SerializeObject(this.data);
        }

        public override object Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<SimulationStep>(data);
        }
    }
}
