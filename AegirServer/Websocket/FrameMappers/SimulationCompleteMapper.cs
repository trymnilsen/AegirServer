using AegirMessages;
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
            throw new NotImplementedException();
        }

        public AegirMessages.Message CreateMessageFromFrame(ISerializeableFrame frame)
        {
            throw new NotImplementedException();
        }
    }
}
