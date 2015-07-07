using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace AegirServer.Websocket
{
    public abstract class WebsocketService : WebSocketBehavior
    {
        public abstract string ServiceName { get; }
        public void SendToAll(ISerializeableFrame frame)
        {
            string content = frame.Serialize();
            Debug.WriteLine("Sending :" + content);
            if(this.Sessions != null)
            {
                this.Sessions.Broadcast(content);
            }
        }
    }
}
