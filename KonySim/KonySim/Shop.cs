using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    class Shop : ILoadContent, IUpdate, IDraw
    {
        ContentManager content;

        List<GameObject> shopObjects;
        SpriteFont shopFont;
        

        public Shop()
        {
            shopObjects = new List<GameObject>();

            shopObjects.Add(CreateImage("Sprites/Shop.png", new Vector2(-100, 55)));

            
            
        }

        public void LoadContent(ContentManager content)
        {
            this.content = content;

            foreach (GameObject go in shopObjects)
            {
                go.LoadContent(content);
            }

            shopFont = content.Load<SpriteFont>("Fonts/shopFont");
        }

        public void Update(float deltaTime)
        {
            foreach (GameObject go in shopObjects)
            {
                go.Update(deltaTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            

            foreach (GameObject go in shopObjects)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.DrawString(shopFont, "Placeholder", new Vector2(720, 585), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(shopFont, "Damage: ", new Vector2(720, 615), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(shopFont, "Price: ", new Vector2(720, 640), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            
        }

        private GameObject CreateImage(string sprite, Vector2 position)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(go, position));
            go.AddComponent(new SpriteRender(go, sprite, 0, 0, 0));
            return go;
        }

        
    }
}
