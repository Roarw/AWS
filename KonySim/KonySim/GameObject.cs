using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace AWS
{
    class GameObject : Component, ILoadContent, IUpdate, IDraw
    {
        List<Component> components;

        public GameObject()
        {
            components = new List<Component>();
        }

        public Component GetComponent(string componentName)
        {
            return components.Find(x => x.GetType().Name == componentName);
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Component component in components)
            {
                if (component is ILoadContent)
                {
                    (component as ILoadContent).LoadContent(content);
                }
            }
        }

        public void Update(float deltaTime)
        {
            foreach (Component component in components)
            {
                if (component is IUpdate)
                {
                    (component as IUpdate).Update(deltaTime);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in components)
            {
                if (component is IDraw)
                {
                    (component as IDraw).Draw(spriteBatch);
                }
            }
        }
    }
}
