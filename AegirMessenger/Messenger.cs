using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirMessenger
{

    public class Messenger
    {
        private object addLock = new object();
        private object unRegisterLock = new object();
        private object sendMessageLock = new object();
        private Dictionary<Type, MessageSubscriptions> subscriptions; 
        /// <summary>
        /// Creates new messenger for sending and receving messages
        /// </summary>
        public Messenger()
        {
            subscriptions = new Dictionary<Type, MessageSubscriptions>();
        }
        public void UnsubscribePostbox(Postbox postbox)
        {
            postbox.IsOpen = false;
        }
        public void Register<T>(Postbox postbox) where T : Message
        {
            Type MessageType = typeof(T);
            lock(addLock)
            {
                if(!subscriptions.ContainsKey(MessageType))
                {
                    subscriptions.Add(MessageType, new MessageSubscriptions());
                }
                subscriptions[MessageType].AddPostbox(postbox);
            }
        }
        public void Publish(Message message)
        {
            lock(sendMessageLock)
            {
                if (subscriptions.ContainsKey(message.GetType()))
                {
                    Debug.WriteLine("Publishing : " + message.ToString());
                    subscriptions[message.GetType()].SendToPostboxes(message);
                }
            }
        }
    }
}
