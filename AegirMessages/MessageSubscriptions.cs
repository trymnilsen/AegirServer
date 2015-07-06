using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirMessages
{
    public class MessageSubscriptions
    {
        private ConcurrentBag<Postbox> postboxes;

        public MessageSubscriptions()
        {
            postboxes = new ConcurrentBag<Postbox>();
        }
        public void AddPostbox(Postbox box)
        {
            postboxes.Add(box);
        }
        public void SendToPostboxes(Message message)
        {
            foreach(Postbox box in postboxes)
            {
                box.PostMessage(message);
            }
        }
    }
}
