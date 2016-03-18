using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace KonySim
{
    internal class DragAndDropArgs : EventArgs
    {
        public Vector2 DropPosition { get; }

        public DragAndDropArgs(Vector2 dropPosition)
        {
            DropPosition = dropPosition;
        }
    }

    internal class DragAndDropAlt : Component, ILoadContent, IUpdate, IMouseDetection
    {
        private Transform transform;
        private Vector2 offset;

        private bool done = false;

        public event EventHandler<DragAndDropArgs> Released;

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
                GameObject.World.RemoveObject(GameObject);
                if (Released != null)
                {
                    Released(this, new DragAndDropArgs(new Vector2(mouseX, mouseY)));
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
        }

        public void MouseReleased()
        {
        }
    }
}
