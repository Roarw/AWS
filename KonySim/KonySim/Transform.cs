using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace KonySim
{
    internal class Transform : Component
    {
        public Vector2 Position { get; set; }

        public Transform(GameObject gameObject, Vector2 position) : base(gameObject)
        {
            this.Position = position;
        }
    }
}
