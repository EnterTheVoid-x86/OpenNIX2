using System;
using System.IO;

namespace OpenNIX
{
    public static partial class Commands
    {
        public static void AddUser(string input, string[] args)
        {
            if (!input.Contains('"') && args.Length < 2)
            {
                Console.WriteLine("Argument underflow.", SVGAIIColor.Red);
                return;
            }
            else if (!input.Contains('"') && args.Length > 2)
            {
                Console.WriteLine("Argument overflow.", SVGAIIColor.Red);
                return;
            }

            string oldShadow = File.ReadAllText(@"0:\etc\shadow");

            Console.Write($"Enter new password for {args[1]}: ", SVGAIIColor.Gray);
            var newPassword = Console.ReadLine(true);

            File.WriteAllText($@"0:\etc\shadow", $"{oldShadow}/{args[1]}:{Hashing.GetNonRandomizedHashCode(newPassword).ToString()}");

            Directory.CreateDirectory($@"0:\home\{args[1]}\");

            Console.WriteLine("Added new user.", SVGAIIColor.Gray);
        }
    }
}
