using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyMath;

namespace OpenNIX
{
    public partial class Commands
    {
        public static void Calculator(string[] args)
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

            try
            {
                Console.WriteLine(SyntaxParser.Evaluate(args[0]));
            }
            catch (Exception e)
            {
                Console.WriteLine("calc: error: " + e.Message, SVGAIIColor.Red);
            }
        }
    }
}
