using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.IO
{
    public enum EFileAvailability
    {
        AVAILABLE,
        ACCESSERROR,
        NOTFOUND,
        GENERICERROR,
        IOERROR,
        NOTSAVED
    }
}
