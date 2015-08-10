using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirLogging
{
    public class LogLine
    {
        public string Content { get; private set; }
        public DateTime Time { get; private set; }
        public LogLine(string content)
        {
            this.Content = content;
            this.Time = DateTime.Now;
        }
    }
}
