using AegirMessages;
using AegirMessages.Simulation;
using AegirMessenger;
using AegirServer.Websocket.Frames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Websocket.FrameMappers
{
    public class SimulationCompleteMapper : IFrameMapper
    {
        public ISerializeableFrame CreateFrameFromMessage(Message message)
        {
            var simulationMessage = message as SimulationFrameComplete;
            if(message != null)
            {
                return new SimulationWebsocketFrame(simulationMessage.data);
            }
            return null;
        }

        public Message CreateMessageFromFrame(ISerializeableFrame frame)
        {
            throw new NotImplementedException();
        }
    }
}
