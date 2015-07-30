using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.HTTP
{
    public class HTTPUtil
    {
        public static bool IsCodeError(int status)
        {
            return status >= 400;
        }
    }
}
