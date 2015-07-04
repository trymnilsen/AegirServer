using AegirServer.Config;
using AegirServer.Runtime;
using AegirServer.Websocket.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace AegirServer.Websocket
{
    class WebsocketModule : AbstractModule
    {
        private SimulationService simulationSocket;
        private WebSocketServer wsServer;

        public override void Run()
        {
            Console.WriteLine("Starting WS on port 8888");
            wsServer.Start();
        }

        public override void SetConfiguration(BaseConfiguration config)
        {
            //Nothing yet
        }

        public override void Stop()
        {
            wsServer.Stop();
            NotifyModuleStopped();
        }

        public override void Startup()
        {
            simulationSocket = new SimulationService();

            Console.WriteLine("Creating WS on port 8888");
            wsServer = new WebSocketServer("ws://localhost:8888");

            wsServer.AddWebSocketService<SimulationService>("/Simulation");
            
        }
    }
}
