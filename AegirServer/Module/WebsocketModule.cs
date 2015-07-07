using AegirMessages;
using AegirMessages.Simulation;
using AegirMessenger;
using AegirServer.Config;
using AegirServer.Runtime;
using AegirServer.Websocket.FrameMappers;
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
        private WebsocketFrameResolver frameResolver;

        private Dictionary<string, WebsocketService> services;

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
            services = new Dictionary<string, WebsocketService>();
            Console.WriteLine("Creating WS on port 8888");
            wsServer = new WebSocketServer("ws://localhost:8888");

            wsServer.AddWebSocketService<SimulationService>("/simulation");
            //Set up postbox
            this.SetUpPostbox();
            //Set up frame resolver
            this.SetUpFrameResolver();   
        }
        private void SetUpFrameResolver()
        {
            frameResolver = new WebsocketFrameResolver();
            frameResolver.AddMapper<SimulationFrameComplete>(new SimulationCompleteMapper());
        }
        private void SetUpPostbox()
        {
            PushPostbox postbox = new PushPostbox(this.Dispatcher);
            this.Messenger.Register<SimulationFrameComplete>(postbox);

            postbox.OnMessage += postbox_OnMessage;
        }

        private void SendToService(ISerializeableFrame frame)
        {
            //For testing
            SimulationService simulationService = new SimulationService();
            simulationService.SendToAll(frame);
        }

        void postbox_OnMessage(Message message)
        {
            ISerializeableFrame frame = frameResolver.GetFrame(message);
            //
        }
    }
}
