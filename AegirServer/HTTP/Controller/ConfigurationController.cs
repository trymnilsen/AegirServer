using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.HTTP.Controller
{
    public class ConfigurationController : HTTPController
    {
        public override void IndexAction()
        {
            this.SetSuccessfulContent(this.Configuration,System.Net.HttpStatusCode.OK);
        }
    }
}
