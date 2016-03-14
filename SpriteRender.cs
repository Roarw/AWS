using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace AWS
{
    class SpriteRender : Component, ILoadContent, IDraw
    {
        string spriteName;
        float depth;

        Texture2D sprite;
        Transform transform;

        public Texture2D Sprite { get { return sprite; } }

        public Rectangle Rectangle { get; set; }

        public SpriteRender(GameObject gameObject, string spriteName, float depth) : base(gameObject)
        {
            this.spriteName = spriteName;
            this.depth = depth;
        }

        public void LoadContent(ContentManager content)
        {
            this.sprite = content.Load<Texture2D>(spriteName);
            this.Rectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
            this.transform = (Transform)gameObject.GetComponent("Transform");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, transform.Position, Rectangle, Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, depth);
        }
    }
}
