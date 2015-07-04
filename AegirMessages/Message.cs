using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirMessages
{
    public abstract class Message
    {
        public string Name { get; private set; }
        public Message(string name)
        {
            this.Name = name;
        }
    }
}
