using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AegirMessenger;

namespace AegirServer.Websocket.Mapper
{
    public class SimulationFrameCompleteMapper : IFrameMessageMapper
    {
        public Type MessageType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public MessageFrame CreateFrameFromMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public Message CreateMessageFromFrame(MessageFrame frame)
        {
            throw new NotImplementedException();
        }
    }
}
