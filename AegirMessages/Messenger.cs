using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirMessages
{

    public class Messenger
    {
        private object addLock = new object();
        private object unRegisterLock = new object();
        private object sendMessageLock = new object();
        private Dictionary<Message, MessageSubscriptions> subscriptions; 
        /// <summary>
        /// Creates new messenger for sending and receving messages
        /// </summary>
        public Messenger()
        {
            subscriptions = new Dictionary<Message, MessageSubscriptions>();
        }
        public void UnsubscribePostbox(Postbox postbox)
        {
            postbox.IsOpen = false;
        }
        public void Register(Postbox postbox, Message message)
        {
            lock(addLock)
            {
                if(!subscriptions.ContainsKey(message))
                {
                    subscriptions.Add(message, new MessageSubscriptions());
                }
                subscriptions[message].AddPostbox(postbox);
            }
        }
        public void Publish(Message message)
        {
            lock(sendMessageLock)
            {
                if(!subscriptions.ContainsKey(message))
                {
                    subscriptions[message].SendToPostboxes(message);
                }
            }
        }
    }
}
