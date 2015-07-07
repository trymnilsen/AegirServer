using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Websocket
{
    public interface ISerializeableFrame
    {
        string ServiceName { get; }
        string Serialize();
        object Deserialize(string data);
    }
}
