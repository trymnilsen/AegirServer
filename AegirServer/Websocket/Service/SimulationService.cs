using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace AegirServer.Websocket.Service
{
    public class SimulationService : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Debug.WriteLine("Simulation Frame received: "+e.Data);
            Send("Hello");
            base.OnMessage(e);
        }
        
    }
}
