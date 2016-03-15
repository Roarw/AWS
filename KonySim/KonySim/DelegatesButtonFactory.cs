using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWS
{
    class DelegatesButtonFactory : ButtonFactory
    {
        GameObject gameObject;

        Action<GameObject> mouseEnter;
        Action<GameObject> mouseExit;
        Action<GameObject> mousePressed;
        Action<GameObject> mouseReleased;

        public DelegatesButtonFactory
            (GameObject gameObject, Action<GameObject> mouseEnter, Action<GameObject> mouseExit,
            Action<GameObject> mousePressed, Action<GameObject> mouseReleased)
        {
            this.gameObject = gameObject;

            this.mouseEnter = mouseEnter;
            this.mouseExit = mouseExit;
            this.mousePressed = mousePressed;
            this.mouseReleased = mouseReleased;
        }

        public void MouseEnter()
        {
            mouseEnter.Invoke(gameObject);
        }

        public void MouseExit()
        {
            mouseExit.Invoke(gameObject);
        }

        public void MousePressed()
        {
            mousePressed.Invoke(gameObject);
        }

        public void MouseReleased()
        {
            mouseReleased.Invoke(gameObject);
        }
    }
}
