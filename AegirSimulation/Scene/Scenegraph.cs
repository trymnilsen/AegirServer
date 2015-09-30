using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirSimulation.Scene
{
    public class Scenegraph
    {
        public List<Node> RootNodes { get; set; }
        public Scenegraph()
        {
            RootNodes = new List<Node>();
        }
        public void RunComponents()
        {
            
        }
        public void AddNode(Node node)
        {
            RootNodes.Add(node);
        }
    }
}
