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
        int yTopOffset;
        int yBottomOffset;

        private Texture2D sprite;
        private Transform transform;

        public Texture2D Sprite { get { return sprite; } }

        public Rectangle Rectangle { get; set; }

        public SpriteRender(GameObject gameObject, string spriteName, float depth, int yTopOffset, int yBottomOffset) : base(gameObject)
        {
            this.spriteName = spriteName;
            this.depth = depth;
            this.yTopOffset = yTopOffset;
            this.yBottomOffset = yBottomOffset;
        }

        public void LoadContent(ContentManager content)
        {
            this.sprite = content.Load<Texture2D>(spriteName);
            this.Rectangle = new Rectangle(0, 0 + yTopOffset, sprite.Width, sprite.Height + yBottomOffset);
            this.transform = (Transform)gameObject.GetComponent("Transform");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, transform.Position, Rectangle, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
        }

        public void SetYOffset(int yTopOffset, int yBottomOffset)
        {
            this.yTopOffset = yTopOffset;
            this.yBottomOffset = yBottomOffset;
            this.Rectangle = new Rectangle(0, 0 + yTopOffset, sprite.Width, sprite.Height + yBottomOffset);
        }
    }
}
