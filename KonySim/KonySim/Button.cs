using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    internal class Button : Component, IMouseDetection
    {
        public event EventHandler OnClick;

        public void MouseEnter()
        {
        }

        public void MouseExit()
        {
        }

        public void MousePressed()
        {
            if (OnClick != null)
                OnClick(this, EventArgs.Empty);
        }

        public void MouseReleased()
        {
        }
    }
}
