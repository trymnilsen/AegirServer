using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirMessages
{

    public class Messenger
    {
        private Dictionary<Message, List<Postbox>> subscriptions; 
        /// <summary>
        /// Creates new messenger for sending and receving messages
        /// </summary>
        public Messenger()
        {

        }
        public void SendMessage()
        {

        }
        public void UnsubscribePostbox(Postbox postbox)
        {

        }
        public void Register(Postbox postbox, Message message)
        {

        }
        public void Publish(Message message)
        {

        }
    }
}
