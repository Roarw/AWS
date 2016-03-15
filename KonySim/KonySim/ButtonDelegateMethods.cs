using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWS
{
    static class ButtonDelegateMethods
    {
        public static void MousePressed(GameObject gameObject)
        {
            System.Diagnostics.Debug.WriteLine("MouseDown.");
        }

        public static void MouseEnter(GameObject gameObject)
        {
            System.Diagnostics.Debug.WriteLine("MouseEnter.");
        }

        public static void MouseExit(GameObject gameObject)
        {
            System.Diagnostics.Debug.WriteLine("MouseExit.");
        }

        public static void MouseReleased(GameObject gameObject)
        {
            System.Diagnostics.Debug.WriteLine("MouseUp.");
        }
    }
}
