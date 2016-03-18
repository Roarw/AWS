using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace KonySim
{
    class Interactive : Component, ILoadContent, IMouseDetection, IUpdate
    {
        ButtonFactory factory;

        public Interactive(GameObject gameObject, ButtonFactory factory) : base(gameObject)
        {
            this.factory = factory;
        }

        public void LoadContent(ContentManager content)
        {
            if (factory is ILoadContent)
            {
                (factory as ILoadContent).LoadContent(content);
            }
        }

        public void MousePressed()
        {
            factory.MousePressed();
        }

        public void MouseEnter()
        {
            factory.MouseEnter();
        }

        public void MouseExit()
        {
            factory.MouseExit();
        }

        public void MouseReleased()
        {
            factory.MouseReleased();
        }

        public void Update(float deltaTime)
        {
            if (factory is IUpdate)
            {
                (factory as IUpdate).Update(deltaTime);
            }
        }
    }
}
