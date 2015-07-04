using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace AegirMessages
{
    public abstract class Postbox
    {
        protected Dispatcher dispatcher;

        public Postbox()
        {

        }
        public Postbox(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }
        public abstract void PostMessage(Message message);
    }
}
