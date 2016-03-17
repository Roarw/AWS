using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace KonySim
{
    internal class Transform : Component, ILoadContent
    {
        private Vector2 position;

        public Vector2 Position { get { return position; } set { position = value; } }

        public Transform(GameObject gameObject, Vector2 position) : base(gameObject)
        {
            this.position = position;
        }

        public void LoadContent(ContentManager content)
        {
        }
    }
}
