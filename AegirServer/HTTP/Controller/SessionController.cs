using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.HTTP.Controller
{
    /// <summary>
    /// Session controller simply creates a random token that makes it easy to 
    /// identify requests from the same session across both websocket and rest requests without requiring any input from the user
    /// We do not want a consistent id for a given browser as this would not make it able to run to projects in different tabs
    /// </summary>
    public class SessionController : HTTPController
    {
        public override void PostAction()
        {
            Guid sessionId = Guid.NewGuid();
            this.SetSuccessfulContent(sessionId.ToString(), System.Net.HttpStatusCode.Created);
        }
    }
}
