﻿using System;
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
        private string fontPath;
        private SpriteFont font;

        private Transform transform;

        public string Text { get { return text; } set { text = value; } }

        public TextRenderer(string text, Color color, float depth, string fontPath = "Fonts/iconFont") : base()
        {
            this.text = text;
            this.color = color;
            this.depth = depth;
            this.fontPath = fontPath;
        }

        public void LoadContent(ContentManager content)
        {
            transform = GameObject.GetComponent<Transform>();
            font = content.Load<SpriteFont>(fontPath);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, transform.Position, color, 0, Vector2.Zero, 1, SpriteEffects.None, depth);
        }
    }
}
