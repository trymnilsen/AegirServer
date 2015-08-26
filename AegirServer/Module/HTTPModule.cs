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
        /// <summary>
        /// Sets up module from the main thread
        /// </summary>
        public HTTPModule()
        {
            this.connection = new HttpListener();
            this.controllers = new Dictionary<String, Type>();
            //Register Controllers
            this.RegisterController<MountPointController>("mount");
            this.RegisterController<ProjectController>("project");
            this.RegisterController<ConfigurationController>("config");
            this.RegisterController<VesselConfigurationController>("vessel");
        }
        /// <summary>
        /// Set the configuration of this module
        /// </summary>
        /// <param name="config"></param>
        public override void SetConfiguration(BaseConfiguration config)
        {
            this.RootAddress = config.HttpEndpoint;
            base.SetConfiguration(config);
        }
        /// <summary>
        /// Initialize the module on its own thread
        /// </summary>
        public override void Startup()
        {
            validateSettings();
            Console.WriteLine("Starting HTTP on " + RootAddress);
            connection.Prefixes.Add(this.RootAddress);
        }
        /// <summary>
        /// Stop the HTTP module
        /// </summary>
        public override void Stop()
        {
            connection.Stop();
        }
        /// <summary>
        /// Run the HTTP module
        /// </summary>
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
                            SetHeaders(ctx.Response);
                            HttpStatusCode status = HttpStatusCode.OK;
                            try
                            {
                                //Try to dispatch and catch any errors
                                this.DispatchRequest(ctx);
                            }
                            catch(HTTPException hex)
                            {
                                ///A HTTPException can be use to control flow for cases like 404
                                ctx.Response.StatusCode = (int)hex.Status;
                            }
                            catch(Exception e)
                            {
                                //Generic error happended
                                ctx.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            }

                        }
                        finally
                        {
                            //Check if there is any content
                            if(ctx.Response.ContentLength64 == 0 && !HTTPUtil.IsCodeError(ctx.Response.StatusCode))
                            {
                                ctx.Response.StatusCode = (int)HttpStatusCode.NoContent;
                                //For some reason status description is set at the point the 
                                //status is set for the first time, and then not 
                                ctx.Response.StatusDescription = "No Content";
                            }
                            // close the stream
                            ctx.Response.OutputStream.Close();
                        }
                    }, context);
                }
            }
            //Something went wrong
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
        /// <summary>
        /// Dispatches a HTTP request the appropriate controller
        /// </summary>
        /// <param name="ctx">Http Listener request to dispatch</param>
        private void DispatchRequest(HttpListenerContext ctx)
        {
            var request = ctx.Request;
            string controllerName = request.RawUrl.Substring(1, request.RawUrl.Length - 1);//Remove first slash
            string[] args = controllerName.Split('/');
            //normalize arguments
            args = this.NormalizeArgs(args);
            if(args.Length == 0)
            {
                throw new HTTPException(HttpStatusCode.OK);
            }
            //Find controller
            if(!controllers.ContainsKey(args[0]))
            {
                throw new HTTPException(HttpStatusCode.NotFound);
            }
            //Create Controller
            HTTPController targetController = Activator.CreateInstance(controllers[args[0]]) as HTTPController;
            //Set values
            targetController.SetConfiguration(this.Configuration);
            targetController.SetRequest(ctx);
            targetController.SetServerContext(this.Context);
            //Dispatch method
            switch(request.HttpMethod)
            {
                case "GET":
                    //Both Index and Get will get here, if no args = index.. Has args = Get
                    //First arg is controller name, therefor we need to check for more than one arg
                    if(args.Length <= 1) { targetController.IndexAction(); }
                    else { targetController.GetAction(args.Skip(1).ToArray()); }
                    break;
                case "POST":
                    targetController.PostAction();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// Normalizes the arguments (removes empty at the moment)
        /// </summary>
        /// <param name="args">an array of arguments provided</param>
        /// <returns>A normalized/cleaned array of arguments</returns>
        private string[] NormalizeArgs(string[] args)
        {
            List<string> newArgs = new List<string>();
            for(int i = 0; i<args.Length; i++)
            {
                if (args[i] != string.Empty)
                {
                    newArgs.Add(args[i]);
                }
            }
            return newArgs.ToArray();
        }
        /// <summary>
        /// Sets common headers on all requests
        /// </summary>
        /// <param name="response">Http context reponse</param>
        private void SetHeaders(HttpListenerResponse response)
        {
            response.Headers.Add(HttpResponseHeader.ContentType, "application/json");
            response.Headers.Add(HttpResponseHeader.ContentEncoding, "UTF-8");
            response.AppendHeader("Access-Control-Allow-Origin", "*");
            response.AppendHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");

        }
        /// <summary>
        /// Registers a route for a controller
        /// </summary>
        /// <typeparam name="T">Classname of a HTTP control we would like to route to</typeparam>
        /// <param name="routeName">the routename</param>
        private void RegisterController<T>(string routeName) where T : HTTPController
        {
            this.controllers.Add(routeName, typeof(T));
        }
        /// <summary>
        /// Validates configuration settings
        /// </summary>
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
