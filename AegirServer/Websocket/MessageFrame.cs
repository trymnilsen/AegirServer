using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Websocket
{
    public abstract class MessageFrame
    {
        /// <summary>
        /// Does this Frame have a target
        /// </summary>
        public bool HasTarget {
            get {
                return false;
            }
        }
        public object Target { get; set; }
        public abstract string FrameId { get; }

        public abstract string Serialize();
        public abstract object Deserialize(string data);
    }
}
