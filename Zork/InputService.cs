﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Zork
{
    internal interface InputService
    {
        event EventHandler<string> InputRecieved;
    }
}
