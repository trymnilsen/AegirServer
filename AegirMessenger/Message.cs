using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirMessenger
{
    public abstract class Message
    {
        public string Name { get; private set; }
        public EChannel Channel { get; set; }
        public Message(string name, EChannel channel)
        {
            this.Name = name;
            this.Channel = channel;
        }
    }
}
