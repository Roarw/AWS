using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KonySim
{
    internal static class Utils
    {
        public static int[] IntToByteArray(int value)
        {
            Stack<int> stack = new Stack<int>();

            for (; value > 0; value /= 1000)
            {
                stack.Push(value % 1000);
            }

            return stack.ToArray();
        }

        public static Color IntToColor(int value)
        {
            int[] byteArray = IntToByteArray(value);
            return new Color(byteArray[0], byteArray[1], byteArray[2]);
        }
    }
}
