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

        //Decoration variables.
        private Color color = Color.White;
        private int yTopOffset = 0;
        private int yBottomOffset = 0;
        private Rectangle bounds = new Rectangle();
        private Vector2 imageOffset = Vector2.Zero;
        private bool checkBounds = false;

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

        //^
        public SpriteRender(string spriteName, float depth, Rectangle bounds, Vector2 imageOffset) : this(spriteName, depth, bounds)
        {
            this.imageOffset = imageOffset;
        }

        //^
        public SpriteRender(string spriteName, float depth, Rectangle bounds, Vector2 imageOffset, Color color) : this(spriteName, depth, bounds, imageOffset)
        {
            this.color = color;
        }

        public void LoadContent(ContentManager content)
        {
            this.sprite = content.Load<Texture2D>(spriteName);
            this.Rectangle = new Rectangle(0, 0 + yTopOffset, sprite.Width, sprite.Height + yBottomOffset);
            this.transform = GameObject.GetComponent<Transform>();
        }

        public void SetSprite(Texture2D sprite)
        {
            this.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            this.color = color;
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
                Vector2 pos = new Vector2(transform.Position.X + imageOffset.X, transform.Position.Y + y1 + imageOffset.Y);

                spriteBatch.Draw(sprite, pos, rect, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
            }
            else
            {
                spriteBatch.Draw(sprite, transform.Position, Rectangle, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
            }
        }
    }
}
