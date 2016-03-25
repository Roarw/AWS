using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    class Shop : Component, ILoadContent, IDraw
    {
        private SpriteFont shopFont;

        public Shop()
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            //setting ContentManager
            shopFont = content.Load<SpriteFont>("Fonts/shopFont");

            //Buy button
            var btnGo = new GameObject();
            btnGo.AddComponent(new Transform(new Vector2(710, 655)));
            btnGo.AddComponent(new SpriteRender("Sprites/btnBuy", 0.2f));
            btnGo.AddComponent(new MouseDetector());
            var btn = new Button();
            btn.OnClick += (sender, e) => 
            {
                System.Diagnostics.Debug.WriteLine("buy");
            };
            btnGo.AddComponent(btn);
            GameWorld.Instance.AddObject(btnGo);

            //Loads Content on every gameobject in the shopObjects list
            GameObject shopGo = UIBuilders.CreateImage("Sprites/Shop.png", new Vector2(-100, 55));
            GameWorld.Instance.AddObject(shopGo);
            
            //Weapon list
            GameObject clickerGo = new GameObject();
            NextClicker weaponList = new NextClicker(new Vector2(265, 600), 430);
            clickerGo.AddComponent(weaponList);
            GameWorld.Instance.AddObject(clickerGo);

            GameObject.OnDeleted += (sender, e) =>
            {
                btnGo.Delete();
                shopGo.Delete();
                clickerGo.Delete();
            };
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(shopFont, "Placeholder", new Vector2(710, 580), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(shopFont, "Damage: ", new Vector2(710, 605), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(shopFont, "Price: ", new Vector2(710, 625), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }
    }
}
