using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    class Shop : Component, ILoadContent, IUpdate, IDraw
    {
        private ContentManager content;

        private List<GameObject> shopObjects;
        private SpriteFont shopFont;
        private NextClicker weaponList;
        
        

        public Shop()
        {
            shopObjects = new List<GameObject>();

            shopObjects.Add(CreateImage("Sprites/Shop.png", new Vector2(-100, 55)));

            //Buy button
            var btnBuy = new GameObject();
            btnBuy.AddComponent(new SpriteRender("Sprites/btnBuy", 0));
            btnBuy.AddComponent(new MouseDetector());
            var btn = new Button();
            btn.OnClick += (sender, e) => { btnBuy.Delete(); };
            btnBuy.AddComponent(btn);
            btnBuy.AddComponent(new Transform(new Vector2(710, 655)));
            GameWorld.Instance.AddObject(btnBuy);
        }

        public void LoadContent(ContentManager content)
        {
            //Loads Content on every gameobject in the shopObjects list
            foreach (GameObject go in shopObjects)
            {
                go.LoadContent(content);
            }

            //setting ContentManager
            this.content = content;
            
            shopFont = content.Load<SpriteFont>("Fonts/shopFont");

            //Weapon list
            weaponList = new NextClicker(new Vector2(265, 600), 430);
            weaponList.LoadContent(content);
        }

        public void Update(float deltaTime)
        {
            foreach (GameObject go in shopObjects)
            {
                go.Update(deltaTime);
            }

            weaponList.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in shopObjects)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.DrawString(shopFont, "Placeholder", new Vector2(710, 580), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(shopFont, "Damage: ", new Vector2(710, 605), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(shopFont, "Price: ", new Vector2(710, 625), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            weaponList.Draw(spriteBatch);
        }

        private GameObject CreateImage(string sprite, Vector2 position)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(position));
            go.AddComponent(new SpriteRender(sprite, 0, 0, 0));
            return go;
        }

        
    }
}
