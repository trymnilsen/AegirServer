using AegirMessenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Websocket
{
    public class WebsocketFrameResolver
    {
        private Dictionary<Type, IFrameMapper> frameMappers = new Dictionary<Type, IFrameMapper>();

        public void AddMapper<T> (IFrameMapper mapper) where T : Message
        {
            if(!frameMappers.ContainsKey(typeof(T)))
            {
                frameMappers.Add(typeof(T), mapper);
            }
        }
        public ISerializeableFrame GetFrame(Message message)
        {
            if(frameMappers.ContainsKey(message.GetType()))
            {
                return frameMappers[message.GetType()].CreateFrameFromMessage(message);
            }
            return null;
        }
    }
}
