using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Runtime
{
    public class EnvironmentHost
    {
        public IEnvironment Environment { get; private set; }
        /// <summary>
        /// Create a new host for this environment
        /// </summary>
        /// <param name="env">The environment to host</param>
        public EnvironmentHost(IEnvironment env)
        {

        }
    }
}
