using AegirMessages.Simulation;
using AegirMessenger;
using AegirServer.Websocket.Mapper;
using Fleck;
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
        private ImmutableDictionary<Type, IFrameMessageMapper> messageToFrame;
        private ImmutableDictionary<IFrameMessageMapper, Type> frameToMessage;
        private object subscriptionsLock = new object();

        public WebSocketRouter()
        {
            clientSubscriptions = ImmutableList<WebSocketSubscription>.Empty;
            this.SetMessageFrameMappings();
        }
        public void SetMessageFrameMappings()
        {
            var mappers = new Dictionary<Type, IFrameMessageMapper>();
            //Add New mappers here
            mappers.Add(typeof(SimulationFrameComplete), new SimulationFrameCompleteMapper());
            //Don't change this, it sets the actually mapper dictionaries
            messageToFrame = mappers.ToImmutableDictionary();
            frameToMessage = mappers.ToImmutableDictionary(kp => kp.Value, kp => kp.Key);
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
                    client.Send(frame.Serialize());
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
