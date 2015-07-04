using AegirDataTypes.Simulation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Websocket.Frames
{
    public class SimulationWebsocketFrame : ISerializeableFrame
    {

        private SimulationStep data;

        public SimulationWebsocketFrame(SimulationStep simulationData) 
        {
            this.data = simulationData;
        }
        public string Serialize()
        {
            return JsonConvert.SerializeObject(this.data);
        }

        public object Deserialize(string data)
        {
            return JsonConvert.DeserializeObject<SimulationStep>(data);
        }
    }
}
