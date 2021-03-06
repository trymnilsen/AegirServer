﻿using AegirServer.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Config
{
    public class ConfigFile
    {
        public string FileSource { get; private set; }
        /// <summary>
        /// Create a new ConfigFile from the given text file
        /// </summary>
        /// <param name="filePath"></param>
        public ConfigFile(string filePath)
        {
            this.FileSource = filePath;
        }
        public BaseConfiguration Load()
        {
            if(File.Exists(this.FileSource))
            {
                string fileContent = FileReader.ReadAllWithoutControlCharacters(this.FileSource);
                //TODO: for now escape
                fileContent = fileContent.Replace(@"\", @"\\");
                BaseConfiguration config = JsonConvert.DeserializeObject<BaseConfiguration>(fileContent);
                return config;
                //Fooobar
            }
            return null;
        }
    }
}
