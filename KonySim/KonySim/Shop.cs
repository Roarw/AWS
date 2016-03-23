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
        ContentManager content;

        List<GameObject> shopObjects;
        SpriteFont shopFont;

        
        

        public Shop()
        {
            shopObjects = new List<GameObject>();

            shopObjects.Add(CreateImage("Sprites/Shop.png", new Vector2(-100, 55)));

            //Right arrow button
            var btnRight = new GameObject(GameWorld.Instance);
            btnRight.AddComponent(new SpriteRender("Sprites/rightArrow", 0));
            btnRight.AddComponent(new MouseDetector());
            var btn = new Button();
            btn.OnClick += (sender, e) => { GameWorld.Instance.RemoveObject(btnRight); };
            btnRight.AddComponent(btn);
            btnRight.AddComponent(new Transform(new Vector2(660, 600)));
            GameWorld.Instance.AddObject(btnRight);

            //Left arrow button
            var btnLeft = new GameObject(GameWorld.Instance);
            btnLeft.AddComponent(new SpriteRender("Sprites/leftArrow", 0));
            btnLeft.AddComponent(new MouseDetector());
            btn = new Button();
            btn.OnClick += (sender, e) => { GameWorld.Instance.RemoveObject(btnLeft); };
            btnLeft.AddComponent(btn);
            btnLeft.AddComponent(new Transform(new Vector2(265, 600)));
            GameWorld.Instance.AddObject(btnLeft);

            //Buy button
            var btnBuy = new GameObject(GameWorld.Instance);
            btnBuy.AddComponent(new SpriteRender("Sprites/btnBuy", 0));
            btnBuy.AddComponent(new MouseDetector());
            btn = new Button();
            btn.OnClick += (sender, e) => { GameWorld.Instance.RemoveObject(btnBuy); };
            btnBuy.AddComponent(btn);
            btnBuy.AddComponent(new Transform(new Vector2(710, 655)));
            GameWorld.Instance.AddObject(btnBuy);
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

            spriteBatch.DrawString(shopFont, "Placeholder", new Vector2(710, 580), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(shopFont, "Damage: ", new Vector2(710, 605), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(shopFont, "Price: ", new Vector2(710, 625), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            
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
