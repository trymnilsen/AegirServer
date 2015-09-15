using AegirLogging;
using AegirMessages;
using AegirMessages.Simulation;
using AegirMessenger;
using AegirServer.Config;
using AegirServer.Runtime;
using Fleck;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AegirServer.Websocket
{
    class WebsocketModule : AbstractModule
    {
        private WebSocketServer wsServer;
        private WebsocketFrameResolver frameResolver;
        private IWebSocketConnection webSocketConnection;

        public override void Run()
        {
            Console.WriteLine("Starting WS on port 8888");
            wsServer.Start(socket =>
            {
                webSocketConnection = socket;
                socket.OnMessage = Message =>
                {
                    socket.Send(Message + " ECHOED");
                };
            });
        }

        public override void SetConfiguration(BaseConfiguration config)
        {
            //Nothing yet
        }

        public override void Stop()
        {
            wsServer.Dispose();
            NotifyModuleStopped();
        }

        public override void Startup()
        {
            Console.WriteLine("Creating WS on port 8888");
            wsServer = new WebSocketServer("ws://0.0.0.0:8888");

            //Set up frame resolver
            this.SetUpFrameResolver();   
            //Set up postbox
            this.SetUpPostbox();
            this.SetupWebscoketTest();
        }
        private void SetupWebscoketTest()
        {
            var wssv = new WebSocketSharp.Server.WebSocketServer("ws://localhost:8800");
            wssv.AddWebSocketService<WebsocketTestBench>("/wstest");
            wssv.Start();
        }
        private void SetUpFrameResolver()
        {
            frameResolver = new WebsocketFrameResolver();
        }
        private void SetUpPostbox()
        {
            PushPostbox postbox = new PushPostbox(this.Dispatcher);
            this.Messenger.Register<SimulationFrameComplete>(postbox);

            postbox.OnMessage += postbox_OnMessage;
        }

        private void SendToService(string frame)
        {
            if(webSocketConnection!=null && this.webSocketConnection.IsAvailable)
            {
                webSocketConnection.Send(frame);
            }
        }

        void postbox_OnMessage(Message message)
        {
            if(message.GetType() == typeof(SimulationFrameComplete))
            {
                string[] logMessages = new string[] {
                    "Summoning Cat Lord",
                    "Counting bits",
                    "Hiding Chutulu",
                    "640K ought to be enough for anybody",
                    "the architects are still drafting",
                    "the bits are breeding",
                    "we're building the buildings as fast as we can",
                    "would you prefer chicken, steak, or tofu?",
                    "pay no attention to the man behind the curtain",
                    "and enjoy the elevator music",
                    "while the little elves draw your map",
                    "a few bits tried to escape, but we caught them",
                    "and dream of faster computers",
                    "would you like fries with that?",
                    "checking the gravitational constant in your locale",
                    "go ahead -- hold your breath",
                    "at least you're not on hold",
                    "hum something loud while others stare",
                    "you're not in Kansas any more",
                    "the server is powered by a lemon and two electrodes",
                    "we love you just the way you are",
                    "while a larger software vendor in Seattle takes over the world",
                    "we're testing your patience",
                    "as if you had any other choice",
                    "take a moment to sign up for our lovely prizes",
                    "don't think of purple hippos",
                    "follow the white rabbit",
                    "why don't you order a sandwich?",
                    "while the satellite moves into position",
                    "the bits are flowing slowly today",
                    "dig on the 'X' for buried treasure... ARRR!",
                    "it's still faster than you could draw it",
                    "Programming the flux capacitor"
                };
                Random ran = new Random();
                int randomInt = ran.Next(logMessages.Length-1);
                string funnyMsg = logMessages[randomInt];

                message.Channel = EChannel.LOGGING;

                LogLine log = new LogLine(funnyMsg);

                WebsocketFrameWrapper wrapper = new WebsocketFrameWrapper(message.Channel.ToString(), log);
                string content = JsonConvert.SerializeObject(wrapper);
                SendToService(content);
            }
        }
    }
}
