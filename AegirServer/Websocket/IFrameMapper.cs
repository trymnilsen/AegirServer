using AegirMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Websocket
{
    public interface IFrameMapper
    {
        ISerializeableFrame CreateFrameFromMessage(Message message);
        Message CreateMessageFromFrame(ISerializeableFrame frame);
    }
}
