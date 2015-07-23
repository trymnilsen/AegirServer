using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.IO.Mount
{
    public class MountPoint
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            this.Id = Guid.NewGuid();
        }
    }
}
