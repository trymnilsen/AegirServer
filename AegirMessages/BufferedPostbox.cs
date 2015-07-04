using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AegirMessages
{
    public class BufferedPostbox : Postbox
    {
        private ConcurrentQueue<Message> bufferedMessages;

        /// <summary>
        /// Creates a new postbox buffering messages until they are fetched
        /// </summary>
        public BufferedPostbox()
        {
            this.bufferedMessages = new ConcurrentQueue<Message>();
        }
        public override void PostMessage(Message message)
        {
            this.bufferedMessages.Enqueue(message);
        }
        public bool TryDeque(out Message message)
        {
            return this.bufferedMessages.TryDequeue(out message);
        }
        public bool IsEmpty()
        {
            return bufferedMessages.IsEmpty;
        }
    }
}
