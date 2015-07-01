﻿using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.CLI
{
    public class CLIOptions
    {
        [Option('r', "read", 
            DefaultValue = "foobar",
            HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option('v', "verbose", 
            DefaultValue = true,
            HelpText = "Prints all messages to standard output.")]
        public bool Verbose { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
