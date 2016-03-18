using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    internal class GameObject : ILoadContent, IUpdate, IDraw, IMouseDetection
    {
        private List<Component> components;
        public GameWorld World { get; }

        public GameObject() : this(null)
        {
        }

        public GameObject(GameWorld world)
        {
            components = new List<Component>();
            World = world;
        }

        public T GetComponent<T>() where T : Component
        {
            return components.OfType<T>().FirstOrDefault();
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
            component.GameObject = this;
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

        public void MousePressed()
        {
            foreach (Component component in components)
            {
                if (component is IMouseDetection)
                {
                    (component as IMouseDetection).MousePressed();
                }
            }
        }

        public void MouseReleased()
        {
            foreach (Component component in components)
            {
                if (component is IMouseDetection)
                {
                    (component as IMouseDetection).MouseReleased();
                }
            }
        }

        public void MouseEnter()
        {
            foreach (Component component in components)
            {
                if (component is IMouseDetection)
                {
                    (component as IMouseDetection).MouseEnter();
                }
            }
        }

        public void MouseExit()
        {
            foreach (Component component in components)
            {
                if (component is IMouseDetection)
                {
                    (component as IMouseDetection).MouseExit();
                }
            };
        }
    }
}
