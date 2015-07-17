using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.HTTP
{
    public abstract class HTTPController
    {
        public virtual HttpStatusCode GetAction(HttpListenerContext context, string[] args)
        {
            return HttpStatusCode.NotImplemented;
        }
        public virtual HttpStatusCode IndexAction(HttpListenerContext context)
        {
            return HttpStatusCode.NotImplemented;
        }
        public virtual HttpStatusCode PutAction(HttpListenerContext context)
        {
            return HttpStatusCode.NotImplemented;
        }
        public virtual HttpStatusCode PostAction(HttpListenerContext context)
        {
            return HttpStatusCode.NotImplemented;
        }
        public virtual HttpStatusCode DeleteAction(HttpListenerContext context)
        {
            return HttpStatusCode.NotImplemented;
        }
    }
}
