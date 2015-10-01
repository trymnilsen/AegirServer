using AegirMessages.Simulation;
using AegirMessenger;
using AegirServer.Websocket.Mapper;
using AegirServer.Websocket.Mapper.Simulation;
using Fleck;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Websocket
{
    public class WebSocketRouter
    {
        private ConcurrentDictionary<Guid, IWebSocketConnection> webSocketConnections;
        private ImmutableList<WebSocketSubscription> clientSubscriptions;
        private IDictionary<Type, IFrameMessageMapper> messageToFrame;
        private IDictionary<Type, IFrameMessageMapper> frameToMessage;
        private object subscriptionsLock = new object();
        private bool canAddMappers = true;
        private Messenger messenger;
        private PushPostbox postbox;

        public WebSocketRouter(Messenger messenger)
        {
            clientSubscriptions = ImmutableList<WebSocketSubscription>.Empty;
            messageToFrame = new Dictionary<Type, IFrameMessageMapper>();
            frameToMessage = new Dictionary<Type, IFrameMessageMapper>();

            this.messenger = messenger;
            this.postbox = new PushPostbox();
            this.SetMessageFrameMappings();
            this.postbox.OnMessage += BroadcastInternalFrame;

        }

        public void SetMessageFrameMappings()
        {
            var mappers = new Dictionary<Type, IFrameMessageMapper>();
            //Add New mappers here
            AddMapper(typeof(SimulationFrameComplete), 
                      new SimulationFrameCompleteMapper());

            LockMappers();
        }
         
        public void AddClient(IWebSocketConnection connection)
        {
            webSocketConnections.TryAdd(connection.ConnectionInfo.Id, connection);
        }
        public void RegisterClientSubscription<T>(object target, Guid connectionId) 
            where T: MessageFrame
        {
            lock(subscriptionsLock)
            {
                Type messageFrameType = typeof(T);
                if(clientSubscriptions.Any(x => x.ConnectionId == connectionId 
                                   && x.MessageFrameType == messageFrameType))
                {
                    WebSocketSubscription newSub = 
                        new WebSocketSubscription(connectionId, target, messageFrameType);

                    clientSubscriptions = clientSubscriptions.Add(newSub);
                }
            }
        }
        public void RemoveSubscription(MessageFrame frame, Guid connectionId)
        {
            lock(subscriptionsLock)
            {
                var subToRemove = clientSubscriptions.FirstOrDefault(
                    x => x.ConnectionId == connectionId
                    && x.MessageFrameType == frame.GetType());

                if(subToRemove!=null)
                {
                    clientSubscriptions = clientSubscriptions.Remove(subToRemove);
                }
            }   
        }
        public void BroadcastInternalFrame(Message message)
        {
            if(message==null)
            {
                throw new ArgumentNullException("message");
            }
            if (messageToFrame.ContainsKey(message.GetType()))
            {
                IFrameMessageMapper mapper = messageToFrame[message.GetType()];
                MessageFrame frame = mapper.CreateFrameFromMessage(message);
                BroadcastFrame(frame);
            }
        }
        public void BroadcastFrame(MessageFrame frame)
        {
            var broadcastTargets = clientSubscriptions.Where(x =>
            {
                //If the frame does not have a target, its a "global" message
                //IE all connections should receive it
                if(!frame.HasTarget) { return true; }
                bool subIsValid = frame.Target == x.Target 
                         && x.MessageFrameType == frame.GetType();

                return subIsValid;
            });
            //Mark any unavailable clients for removal
            List<Guid> connectionsToRemove = new List<Guid>();
            //Send to each
            foreach(WebSocketSubscription sub in broadcastTargets)
            {
                IWebSocketConnection client = webSocketConnections[sub.ConnectionId];
                if(client.IsAvailable)
                {
                    //Wrap the frame
                    WebsocketFrameWrapper wrapper = WrapFrame(frame);
                    string serializedContent = JsonConvert.SerializeObject(wrapper);
                    client.Send(serializedContent);
                }
                else
                {
                    connectionsToRemove.Add(sub.ConnectionId);
                }
            }
            //Remove unavailable
            foreach(Guid idToRemove in connectionsToRemove)
            {
                //We need this for some goram reason
                IWebSocketConnection ignored;
                webSocketConnections.TryRemove(idToRemove, out ignored);
            }
        }
        /// <summary>
        /// Adds a mapper
        /// </summary>
        /// <param name="internalMessageType"></param>
        /// <param name="messageFrameType"></param>
        /// <param name="mapper"></param>
        private void AddMapper(Type messageFrameType,
                               IFrameMessageMapper mapper)
        {
            if(!canAddMappers)
            {
                throw new InvalidOperationException("Cannot add mappers after mappers has been locked");
            }
            messageToFrame.Add(mapper.MessageType, mapper);
            frameToMessage.Add(messageFrameType, mapper);
        }
        private void LockMappers()
        {
            canAddMappers = false;
            messageToFrame = messageToFrame.ToImmutableDictionary();
            frameToMessage = frameToMessage.ToImmutableDictionary();

            foreach(Type messageType in messageToFrame.Keys)
            {
                this.messenger.Register(messageType, postbox);
            }
        }
        private WebsocketFrameWrapper WrapFrame(MessageFrame frame)
        {
            string name = frame.FrameId;
            WebsocketFrameWrapper wrapped = new WebsocketFrameWrapper(name, frame);
            return wrapped;
        }

        private class WebSocketSubscription
        {
            public Guid ConnectionId { get; private set; }
            public object Target { get; private set; }
            public Type MessageFrameType { get; private set; }

            public WebSocketSubscription(Guid connectionId, 
                                         object target, 
                                         Type frametype)
            {
                this.ConnectionId = connectionId;
                this.Target = target;
                this.MessageFrameType = frametype;
            }
        }
    }
}
