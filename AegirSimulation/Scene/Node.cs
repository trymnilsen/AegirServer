using AegirSimulation.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirSimulation.Scene
{
    public class Node
    {
        public Node Parent { get; private set; }
        public List<Node> Children { get; private set; }
        public Transformation Transformation { get; set; }
        public List<Component> Components { get; private set; }
    }
}
