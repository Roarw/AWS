using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    class NextButton : Component, IUpdate, IMouseDetection
    {
        private NextClicker clicker;
        private bool rightButton;

        private bool mousePressed;
        private bool mouseReleased;

        public NextButton(NextClicker clicker, bool rightButton)
        {
            this.clicker = clicker;
            this.rightButton = rightButton;

            mousePressed = false;
            mouseReleased = false;
        }

        public void Update(float deltaTime)
        {
            if (mousePressed && mouseReleased)
            {
                mousePressed = false;
                mouseReleased = false;

                if (rightButton)
                {
                    clicker.SetCurrentItem(1);
                }
                else
                {
                    clicker.SetCurrentItem(-1);
                }
            }
        }

        public void MouseEnter()
        {
        }

        public void MouseExit()
        {
            
        }

        public void MousePressed()
        {
            mousePressed = true;
        }

        public void MouseReleased()
        {
            mouseReleased = true;
        }
    }
}
