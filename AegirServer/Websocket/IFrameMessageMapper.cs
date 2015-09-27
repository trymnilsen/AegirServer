using AegirMessages;
using AegirMessenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Websocket
{
    public interface IFrameMessageMapper
    {
        Type MessageType { get; }
        MessageFrame CreateFrameFromMessage(Message message);
        Message CreateMessageFromFrame(MessageFrame frame);
    }
}
