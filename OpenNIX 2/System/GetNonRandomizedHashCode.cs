using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNIX
{
    public static class Hashing
    {
        public static unsafe int GetNonRandomizedHashCode(string aString)
        {

            // the code is the same as the one used in .net except for the explicit == 2 and == 1 cases
            // we need this since a new object can start directly behind the string in memory, so the standard
            // implementation would read the allocated size of the next object and use it for the hash
            var asSpan = aString.AsSpan();
            if (asSpan.Length == 0)
            {
                unchecked
                {
                    return (int)(352654597u + 352654597u * 1566083941);
                }
            }

            fixed (char* ptr = &asSpan[0])
            {
                uint num = 352654597u;
                uint num2 = num;
                uint* ptr2 = (uint*)ptr;
                int num3 = aString.Length;
                while (num3 >= 4)
                {
                    num3 -= 4;
                    num = (global::System.Numerics.BitOperations.RotateLeft(num, 5) + num) ^ *ptr2;
                    num2 = (global::System.Numerics.BitOperations.RotateLeft(num2, 5) + num2) ^ ptr2[1];
                    ptr2 += 2;
                }
                if (num3 == 3)
                {
                    num2 = (global::System.Numerics.BitOperations.RotateLeft(num2, 5) + num2) ^ *ptr2;
                    num2 = (global::System.Numerics.BitOperations.RotateLeft(num2, 5) + num2) ^ ((char*)ptr2)[2];
                }
                else if (num3 == 2)
                {
                    num2 = (global::System.Numerics.BitOperations.RotateLeft(num2, 5) + num2) ^ *ptr2;
                }
                else if (num3 == 1)
                {
                    num2 = (global::System.Numerics.BitOperations.RotateLeft(num2, 5) + num2) ^ *(char*)ptr2;
                }

                unchecked
                {
                    return (int)(num + num2 * 1566083941);
                }
            }
        }
    }
}
