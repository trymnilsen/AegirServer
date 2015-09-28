using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegirMessenger;
using AegirMessages.Simulation;
using AegirServer.Websocket.Frames;
using AegirServer.Websocket.Frames.Simulation;

namespace AegirServer.Websocket.Mapper.Simulation
{
    public class SimulationFrameCompleteMapper : IFrameMessageMapper
    {
        public Type MessageType
        {
            get
            {
                return typeof(SimulationFrameComplete);
            }
        }

        public MessageFrame CreateFrameFromMessage(Message message)
        {
            SimulationFrameComplete simMessage = message as SimulationFrameComplete;
            return new SimulationFrame(simMessage.data);
        }

        public Message CreateMessageFromFrame(MessageFrame frame)
        {
            throw new NotImplementedException();
        }
    }
}
