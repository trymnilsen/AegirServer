using AegirServer.Config;
using AegirServer.Runtime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.HTTP
{
    /// <summary>
    /// Baseclass for all HTTP API controllers
    /// </summary>
    public abstract class HTTPController
    {
        /// <summary>
        /// Configuration of this server
        /// </summary>
        public BaseConfiguration Configuration { get; private set; }
        /// <summary>
        /// The context of this server, containing the workspace and more
        /// </summary>
        public ServerContext ServerContext { get; private set; }
        /// <summary>
        /// The response object for this request
        /// </summary>
        protected HttpListenerResponse Response { get; private set; }
        /// <summary>
        /// The Request object for this request
        /// </summary>
        protected HttpListenerRequest Request { get; private set; }
        /// <summary>
        /// Called when the request is a GET with params
        /// </summary>
        /// <param name="args">the arguments supplied (split by slashes)</param>
        public virtual void GetAction(string[] args)
        {
            this.SetResponseCode(HttpStatusCode.NotImplemented);
        }
        /// <summary>
        /// Called when the request is an Index Action I.E GET without any parameters
        /// </summary>
        public virtual void IndexAction()
        {
            this.SetResponseCode(HttpStatusCode.NotImplemented);
        }
        /// <summary>
        /// Called when the request is a put action
        /// </summary>
        public virtual void PutAction()
        {
            this.SetResponseCode(HttpStatusCode.NotImplemented);
        }
        /// <summary>
        /// Called when the request is a put action
        /// </summary>
        public virtual void PostAction()
        {
            this.SetResponseCode(HttpStatusCode.NotImplemented);
        }
        /// <summary>
        /// Called when the request is a delete action
        /// </summary>
        public virtual void DeleteAction()
        {
            this.SetResponseCode(HttpStatusCode.NotImplemented);
        }
        /// <summary>
        /// Set the http listener context for this controller, sets request and response
        /// </summary>
        /// <param name="context"></param>
        public void SetRequest(HttpListenerContext context)
        {
            this.Response = context.Response;
            this.Request = context.Request;
        }
        /// <summary>
        /// Set the Server context of this controller
        /// </summary>
        /// <param name="context"></param>
        public void SetServerContext(ServerContext context)
        {
            this.ServerContext = context;
        }
        /// <summary>
        /// Set the configuration of this controller
        /// </summary>
        /// <param name="config"></param>
        public void SetConfiguration(BaseConfiguration config)
        {
            this.Configuration = config;
        }
        /// <summary>
        /// Set a Response code to be returned
        /// </summary>
        /// <param name="status"></param>
        protected void SetResponseCode(HttpStatusCode status)
        {
            this.Response.StatusCode = (int)status;
        }
        protected void SetSuccessfulContent(object data, HttpStatusCode code)
        {
            string outData = JsonConvert.SerializeObject(data);
            this.SetOutput(outData);
            this.SetResponseCode(code);
        }
        protected void SetOutput(string data)
        {
            byte[] buf = Encoding.UTF8.GetBytes(data);
            this.Response.ContentLength64 = buf.Length;
            this.Response.OutputStream.Write(buf, 0, buf.Length);
        }
        protected string GetTextData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (System.IO.Stream body = request.InputStream) // here we have data
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
