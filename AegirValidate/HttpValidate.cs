using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirValidate
{
    public class HttpValidate
    {
        public static bool ValidateHTTPAddress(string address)
        {
            Uri uriResult;
            return Uri.TryCreate(address, UriKind.Absolute, out uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp);
        }
    }
}
