using AegirMessages;
using AegirServer.Config;
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

        private HttpListener connection = new HttpListener();
        private string RootAddress = NO_ADDRESS; //Loaded from config
        private string ResponseTest = "Hello There";

        public HTTPModule()
        {
            this.connection = new HttpListener();
        }

        public override void SetConfiguration(BaseConfiguration config)
        {
            this.RootAddress = config.HttpEndpoint;
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
                            string rstr = this.HandleRequest(ctx.Request);
                            byte[] buf = Encoding.UTF8.GetBytes(rstr);
                            ctx.Response.ContentLength64 = buf.Length;
                            ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                        }
                        catch { } // suppress any exceptions
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
        private string HandleRequest(HttpListenerRequest request)
        {
            return this.ResponseTest + DateTime.Now.ToLongTimeString();
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

        public override void Startup()
        {
            validateSettings();
            Console.WriteLine("Starting HTTP on " + RootAddress);
            connection.Prefixes.Add(this.RootAddress);
        }
    }
}
