﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegirServer.Messages
{
    public class Postbox
    {
        private EMessengerBufferMode bufferMode;

        public Postbox(EMessengerBufferMode bufferMode)
        {
            this.bufferMode = bufferMode;
        }
    }
}