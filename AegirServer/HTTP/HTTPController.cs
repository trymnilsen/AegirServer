using AegirServer.Config;
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
        public BaseConfiguration Configuration { get; private set; }
        protected HttpListenerResponse Response { get; private set; }
        protected HttpListenerRequest Request { get; private set; }

        public virtual HttpStatusCode GetAction(string[] args)
        {
            return HttpStatusCode.NotImplemented;
        }
        public virtual HttpStatusCode IndexAction()
        {
            return HttpStatusCode.NotImplemented;
        }
        public virtual HttpStatusCode PutAction()
        {
            return HttpStatusCode.NotImplemented;
        }
        public virtual HttpStatusCode PostAction()
        {
            return HttpStatusCode.NotImplemented;
        }
        public virtual HttpStatusCode DeleteAction()
        {
            return HttpStatusCode.NotImplemented;
        }
        public void SetContext(HttpListenerContext context)
        {
            this.Response = context.Response;
            this.Request = context.Request;
        }
        public void SetConfiguration(BaseConfiguration config)
        {
            this.Configuration = config;
        }
        protected void SetOutput(string data)
        {
            byte[] buf = Encoding.UTF8.GetBytes(data);
            this.Response.ContentLength64 = buf.Length;
            this.Response.OutputStream.Write(buf, 0, buf.Length);
        }
    }
}
