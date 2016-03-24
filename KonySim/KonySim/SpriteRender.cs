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
        private int xRightOffset = 0;
        private int xLeftOffset = 0;
        private Rectangle bounds = new Rectangle();
        private Vector2 imageOffset = Vector2.Zero;
        private bool checkBounds = false;

        public Texture2D Sprite { get; set; }
        private Transform transform;

        public Rectangle Rectangle { get; set; }

        public SpriteRender(string spriteName, float depth) : base()
        {
            this.spriteName = spriteName;
            this.depth = depth;
        }

        //^
        public SpriteRender(string spriteName, float depth, Color color) : this(spriteName, depth)
        {
            this.color = color;
        }

        public SpriteRender(string spriteName, float depth, int yTopOffset, int yBottomOffset) : base()
        {
            this.spriteName = spriteName;
            this.depth = depth;
            this.yTopOffset = yTopOffset;
            this.yBottomOffset = yBottomOffset;
        }

        //^
        public SpriteRender(string spriteName, float depth, int yTopOffset, int yBottomOffset, int xRightOffset, int xLeftOffset) : this(spriteName, depth, yTopOffset, yBottomOffset)
        {
            this.xRightOffset = xRightOffset;
            this.xLeftOffset = xLeftOffset;
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
            this.Sprite = content.Load<Texture2D>(spriteName);
            this.Rectangle = new Rectangle(0 + xRightOffset, 0 + yTopOffset, Sprite.Width + xLeftOffset, Sprite.Height + yBottomOffset);
            this.transform = GameObject.GetComponent<Transform>();
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
                if (transform.Position.Y + Sprite.Height > bounds.Bottom)
                {
                    y2 = bounds.Bottom - (transform.Position.Y + Sprite.Height);
                }

                Rectangle rect = new Rectangle(0, 0 + (int)y1, Sprite.Width, Sprite.Height + (int)y2);
                Vector2 pos = new Vector2(transform.Position.X + imageOffset.X, transform.Position.Y + y1 + imageOffset.Y);

                spriteBatch.Draw(Sprite, pos, rect, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
            }
            else
            {
                spriteBatch.Draw(Sprite, transform.Position, Rectangle, color, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
            }
        }
    }
}
