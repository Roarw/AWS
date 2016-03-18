using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    interface ButtonFactory
    {
        void MousePressed();
        void MouseEnter();
        void MouseExit();
        void MouseReleased();
    }
}
