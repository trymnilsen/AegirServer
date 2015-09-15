using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace AegirServer.Websocket
{
    public class WebsocketTestBench : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            base.OnOpen();
        }
        protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
        {
            base.OnMessage(e);
            this.Send("PINGBACK " + e.Data);
        }
    }
}
