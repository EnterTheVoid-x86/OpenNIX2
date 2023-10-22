using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenNIX;

namespace OpenNIX
{
    public static partial class Commands
    {
        public static void Hash(string[] args)
        {
            if (args.Length > 2)
            {
                if (args.Length < 2)
                {
                    Console.WriteLine("Argument underflow.", SVGAIIColor.Red);
                    return;
                }
                else if (args.Length > 2)
                {
                    Console.WriteLine("Argument overflow.", SVGAIIColor.Red);
                    return;
                }
            }

            Console.WriteLine(Hashing.GetNonRandomizedHashCode(args[0]).ToString(), SVGAIIColor.Gray);
        }
    }
}
