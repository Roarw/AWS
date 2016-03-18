using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace KonySim
{
    internal class DragAndDropAlt : Component, ILoadContent, IUpdate, IMouseDetection
    {
        private Transform transform;
        private Vector2 offset;

        private bool done = false;

        public DragAndDropAlt() : this(Vector2.Zero)
        {
        }

        public DragAndDropAlt(Vector2 offset)
        {
            this.offset = offset;
        }

        public void LoadContent(ContentManager content)
        {
            transform = GameObject.GetComponent<Transform>();
        }

        public void Update(float deltaTime)
        {
            if (done) return;

            var mouseX = GameWorld.MouseX;
            var mouseY = GameWorld.MouseY;

            transform.Position = new Vector2(mouseX + offset.X, mouseY + offset.Y);

            if (Mouse.GetState().LeftButton == ButtonState.Released)
            {
                done = true;
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
        }

        public void MouseReleased()
        {
        }
    }
}
