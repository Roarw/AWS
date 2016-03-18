using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    internal class SpriteRender : Component, ILoadContent, IDraw
    {
        private string spriteName;
        private float depth;
        private int yTopOffset;
        private int yBottomOffset;
        private Rectangle bounds;
        private bool checkBounds;

        private Texture2D sprite;
        private Transform transform;

        public Rectangle Rectangle { get; set; }

        public SpriteRender(string spriteName, float depth) : base()
        {
            this.spriteName = spriteName;
            this.depth = depth;
        }

        public SpriteRender(string spriteName, float depth, int yTopOffset, int yBottomOffset) : base()
        {
            this.spriteName = spriteName;
            this.depth = depth;
            this.yTopOffset = yTopOffset;
            this.yBottomOffset = yBottomOffset;
        }

        public SpriteRender(string spriteName, float depth, Rectangle bounds) : base()
        {
            this.spriteName = spriteName;
            this.depth = depth;
            this.bounds = bounds;
            checkBounds = true;
        }

        public void LoadContent(ContentManager content)
        {
            this.sprite = content.Load<Texture2D>(spriteName);
            this.Rectangle = new Rectangle(0, 0 + yTopOffset, sprite.Width, sprite.Height + yBottomOffset);
            this.transform = GameObject.GetComponent<Transform>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (checkBounds)
            {
                float y1 = 0;
                float y2 = 0;

                if (transform.Position.Y < bounds.Top)
                {
                    y1 = bounds.Top - transform.Position.Y;
                    y2 = -(bounds.Top - transform.Position.Y);
                }
                if (transform.Position.Y + sprite.Height > bounds.Bottom)
                {
                    y2 = bounds.Bottom - (transform.Position.Y + sprite.Height);
                }

                Rectangle rect = new Rectangle(0, 0 + (int)y1, sprite.Width, sprite.Height + (int)y2);
                Vector2 pos = new Vector2(transform.Position.X, transform.Position.Y + y1);

                spriteBatch.Draw(sprite, pos, rect, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
            }
            else
            {
                spriteBatch.Draw(sprite, transform.Position, Rectangle, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
            }
        }
    }
}
