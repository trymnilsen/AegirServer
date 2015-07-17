using AegirMessages;
using AegirServer.Config;
using AegirServer.HTTP;
using AegirServer.HTTP.Controller;
using AegirServer.Runtime;
using AegirValidate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AegirServer.Module
{
    public class HTTPModule : AbstractModule
    {
        public const string NO_ADDRESS = "NOPATH";
        private Dictionary<String, Type> controllers;
        private HttpListener connection = new HttpListener();
        private string RootAddress = NO_ADDRESS; //Loaded from config
        private string ResponseTest = "Hello There";

        public HTTPModule()
        {
            this.connection = new HttpListener();
            this.controllers = new Dictionary<String, Type>();
        }

        public override void SetConfiguration(BaseConfiguration config)
        {
            this.RootAddress = config.HttpEndpoint;
        }
        public override void Startup()
        {
            validateSettings();
            Console.WriteLine("Starting HTTP on " + RootAddress);
            connection.Prefixes.Add(this.RootAddress);
        }
        public override void Stop()
        {
            connection.Stop();
        }
        public override void Run()
        {
            connection.Start();
            StartListeningForRequests();
        }

        /// <summary>
        /// Todo move into own class/task
        /// </summary>
        /// <returns></returns>
        private void StartListeningForRequests()
        {
            try
            {
                //TODO rewrite with async await
                while(connection.IsListening)
                {
                    HttpListenerContext context = connection.GetContext();
                    ThreadPool.QueueUserWorkItem((c) =>
                    {
                        var ctx = c as HttpListenerContext;
                        try
                        {
                            string rstr = "";
                            HttpStatusCode status = HttpStatusCode.OK;
                            try
                            {
                                rstr = this.DispatchRequest(ctx);
                            }
                            catch(HTTPException hex)
                            {

                            }
                            catch(Exception e)
                            {

                            }
                            byte[] buf = Encoding.UTF8.GetBytes(rstr);
                            ctx.Response.ContentLength64 = buf.Length;
                            ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                        }
                        finally
                        {
                            // always close the stream
                            ctx.Response.OutputStream.Close();
                        }
                    }, context);
                }
            }
            catch(HttpListenerException hlex)
            {
                //We get an exception because the GetContext call is considered an 
                //ongoing IO operation until it returns, so when we stop it from our
                //other thread, the listener is quite flabbergasted.. and returns 995
                //operation aborted.. If this happens we want to ignore it
                if(hlex.ErrorCode != 995)
                {
                    Console.WriteLine("HttpListener Exception:" + hlex.ToString());
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in HTTP Module \n" + e.ToString());
            }
            //We have stopped
            connection.Close();
            base.NotifyModuleStopped();
        }
        private string DispatchRequest(HttpListenerContext ctx)
        {
            var request = ctx.Request;
            string controllerName = request.RawUrl.Substring(1, request.RawUrl.Length - 1);//Remove first slash
            string[] args = controllerName.Split('/');
            //Find controller
            if(!controllers.ContainsKey(args[0]))
            {
                throw new HTTPException(HttpStatusCode.NotFound);
            }
            HTTPController targetController = Activator.CreateInstance(controllers[args[0]]) as HTTPController;
            //Dispatch method
            switch(request.HttpMethod)
            {
                case "GET":
                    targetController.GetAction(ctx, args.Skip(1).ToArray());
                    break;
                default:
                    break;
            }
            return this.ResponseTest + DateTime.Now.ToLongTimeString();
        }
        private void RegisterController<T>(string routeName) where T : HTTPController
        {
            this.controllers.Add(routeName, typeof(T));
        }
        private void validateSettings()
        {
            if(!HttpValidate.ValidateHTTPAddress(this.RootAddress))
            {
                throw new InvalidOperationException("HttpEndpoint not valid, use absolute non-tls paths, was"+this.RootAddress);
            }
            if(RootAddress == NO_ADDRESS)
            {
                throw new InvalidOperationException("HTTP Modeule had no address to use , found "+this.RootAddress);
            }
        }

    }
}
