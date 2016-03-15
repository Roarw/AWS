using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWS
{
    interface IMouseDetection
    {
        void MousePressed();
        void MouseReleased();
        void MouseEnter();
        void MouseExit();
    }
}
