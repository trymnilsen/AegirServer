using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirDataTypes.Vessel
{
    public class VesselConfiguration
    {
        public double Width { get; private set; }
        public double Height { get; private set; }
        public string Name { get; private set; }
        public Guid Id { get; private set; }
        public VesselConfiguration(string name, double width, double height)
        {
            this.Width = width;
            this.Height = height;
            this.Name = name;
            this.Id = Guid.NewGuid();
        }
    }
}
