﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.CLI
{
    public class CLIAppStart
    {
        private CLIOptions options;
        public CLIAppStart(CLIOptions options)
        {
            this.options = options;
        }
        public void Start()
        {
            if (options.Verbose) Console.WriteLine("Filename: {0}", options.InputFile);
        }
    }
}
