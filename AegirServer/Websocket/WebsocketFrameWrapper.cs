using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Websocket
{
    public class WebsocketFrameWrapper
    {
        public string FrameName { get; private set; }
        public object Data { get; private set; }

        public WebsocketFrameWrapper(string frameName, object data)
        {
            this.FrameName = frameName;
            this.Data = data;
        }
    }
}
