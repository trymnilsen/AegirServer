using AegirSimulation.Scene.Presets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirSimulation.Scene
{
    public class ScenegraphFactory
    {
        public static Scenegraph CreateDefault()
        {
            Scenegraph graph = new Scenegraph();
            VesselNode node = new VesselNode();

            graph.AddNode(node);
            //A Normal Scenegraph contains
            return graph;

        }
    }
}
