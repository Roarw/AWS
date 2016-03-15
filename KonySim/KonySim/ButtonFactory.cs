using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWS
{
    interface ButtonFactory
    {
        void MousePressed();
        void MouseEnter();
        void MouseExit();
        void MouseReleased();
    }
}
