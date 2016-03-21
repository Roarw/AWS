using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    class WorldMap : Component, IDraw, ILoadContent, IUpdate
    {
        private Texture2D sprite;

        public WorldMap(Texture2D sprite)
        { 
            this.sprite = sprite;
        }

        void IDraw.Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite);
        }

        void ILoadContent.LoadContent(ContentManager content)
        {
            
        }

        void IUpdate.Update(float deltaTime)
        {
            
        }
    }
}
