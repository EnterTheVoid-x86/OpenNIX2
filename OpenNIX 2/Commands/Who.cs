﻿using OpenNIX_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNIX
{
    public static partial class Commands
    {
        public static void Who()
        {
            Console.WriteLine($"who: logged users: {Kernel.Username}", SVGAIIColor.Gray);
        }
    }
}
