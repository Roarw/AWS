using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    internal class TextRenderer : Component, ILoadContent, IDraw
    {
        private string text;
        private float depth;
        private SpriteFont font;

        private Transform transform;

        public TextRenderer(GameObject gameObject, string text, float depth) : base()
        {
            this.text = text;
            this.depth = depth;
        }

        public void LoadContent(ContentManager content)
        {
            this.transform = GameObject.GetComponent<Transform>();
            font = content.Load<SpriteFont>("Default");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, transform.Position, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, depth);
        }
    }
}
