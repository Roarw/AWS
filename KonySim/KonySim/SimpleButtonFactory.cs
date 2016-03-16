using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    class SimpleButtonFactory : ButtonFactory
    {
        public SimpleButtonFactory()
        {
        }

        public void MousePressed()
        {
            System.Diagnostics.Debug.WriteLine("MouseDown.");
        }

        public void MouseEnter()
        {
            System.Diagnostics.Debug.WriteLine("MouseEnter.");
        }

        public void MouseExit()
        {
            System.Diagnostics.Debug.WriteLine("MouseExit.");
        }

        public void MouseReleased()
        {
            System.Diagnostics.Debug.WriteLine("MouseUp.");
        }
    }
}
