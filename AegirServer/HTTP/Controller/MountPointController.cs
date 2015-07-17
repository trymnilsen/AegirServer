using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.HTTP.Controller
{
    public class MountPointController : HTTPController
    {
        public override HttpStatusCode GetAction(HttpListenerContext context, string[] args)
        {
            byte[] buf = Encoding.UTF8.GetBytes("foobar");
            context.Response.ContentLength64 = buf.Length;
            context.Response.OutputStream.Write(buf, 0, buf.Length);
            return HttpStatusCode.OK;
        }
    }
}
