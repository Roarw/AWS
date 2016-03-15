using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace AWS
{
    class Button : Component, ILoadContent, IMouseDetection
    {
        public Button(GameObject gameObject) : base(gameObject)
        {

        }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void MousePressed()
        {
            System.Diagnostics.Debug.WriteLine("MouseDown");
        }

        public void MouseEnter()
        {
            System.Diagnostics.Debug.WriteLine("MouseEnter");
        }

        public void MouseExit()
        {
            System.Diagnostics.Debug.WriteLine("MouseExit");
        }

        public void MouseReleased()
        {
            System.Diagnostics.Debug.WriteLine("MouseUp");
        }
    }
}
