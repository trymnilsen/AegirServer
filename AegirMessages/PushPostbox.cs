using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AegirMessages
{
    public class PushPostbox : Postbox
    {
        public PushPostbox(Dispatcher dispatcher)
            :base(dispatcher)
        {

        }
        /// <summary>
        /// Push a message synchronously to the other thread
        /// </summary>
        /// <param name="message">The message to push</param>
        public override void PostMessage(Message message)
        {
            //Using closure to make it easy to send the message
            this.dispatcher.Invoke(() =>
            {
                MessageReceivedHandler messageEvent = OnMessage;
                if (messageEvent != null)
                {
                    messageEvent(message);
                }
            });
        }

        public delegate void MessageReceivedHandler(Message message);
        public event MessageReceivedHandler OnMessage;
    }
}
