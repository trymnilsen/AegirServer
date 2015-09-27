using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AegirMessenger
{
    public abstract class Postbox
    {
        protected Dispatcher dispatcher;

        public bool IsOpen { get; set; }

        public Postbox()
        {
            IsOpen = true;
        }
        public Postbox(Dispatcher dispatcher)
        {
            IsOpen = true;
            this.dispatcher = dispatcher;
        }
        public abstract void PostMessage(Message message);
    }
}
