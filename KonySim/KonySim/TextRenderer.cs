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
        private Color color;
        private float depth;
        private SpriteFont font;

        private Transform transform;

        public string Text { get { return text; } set { text = value; } }

        public TextRenderer(string text, Color color, float depth) : base()
        {
            this.text = text;
            this.color = color;
            this.depth = depth;
        }

        public void LoadContent(ContentManager content)
        {
            this.transform = GameObject.GetComponent<Transform>();
            font = content.Load<SpriteFont>("Fonts/iconFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, transform.Position, color, 0, Vector2.Zero, 1, SpriteEffects.None, depth);
        }
    }
}
