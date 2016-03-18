using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    internal class UIListScrollerFactory : ButtonFactory, IUpdate
    {
        UIList uiList;
        int factor;
        bool mouseDown;

        public UIListScrollerFactory(UIList uiList, int factor)
        {
            this.uiList = uiList;
            this.factor = factor;
            mouseDown = false;
        }

        public void Update(float deltaTime)
        {
            if (mouseDown)
            {
                uiList.SetScrollerPosition(factor);
            }
        }

        public void MouseEnter()
        {
            
        }

        public void MouseExit()
        {
            mouseDown = false;
        }

        public void MousePressed()
        {
            mouseDown = true;
        }

        public void MouseReleased()
        {
            mouseDown = false;
        }
    }
}
