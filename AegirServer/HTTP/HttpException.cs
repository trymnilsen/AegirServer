using System;
using System.Net;
using System.Runtime.Serialization;

namespace AegirServer.HTTP
{
    public class HTTPException : Exception
    {
        public HttpStatusCode Status { get; private set; }
        public HTTPException(HttpStatusCode status)
        {
            this.Status = status;
        }
        public HTTPException(HttpStatusCode status, string message) :  base(message)
        {
            this.Status = status;
        }
        public HTTPException(HttpStatusCode status, string message, Exception innerException) : base(message, innerException)
        {
            this.Status = status;
        }
    }
}