using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    internal class Map : Component, ILoadContent
    {
        private List<Db.Mission> missions = new List<Db.Mission>();

        public Map()
        {
            using (Db.Connection con = new Db.Connection())
            {
                missions.AddRange(con.GetAllRows<Db.Mission>());
            }
        }

        public void LoadContent(ContentManager content)
        {
            //Adding shop.
            GameObject shopGo = new GameObject();
            shopGo.AddComponent(new Transform(new Vector2(700, 150)));
            shopGo.AddComponent(new SpriteRender("Sprites/shopIcon", 0.245f));
            shopGo.AddComponent(new MouseDetector());
            Button shopBtn = new Button();
            shopGo.AddComponent(shopBtn);
            shopBtn.OnClick += (sender, e) =>
            {
                GameWorld.Instance.MWManager.GotoShop();
                GameObject.Delete();
            };
            GameWorld.Instance.AddObject(shopGo);

            GameObject.OnDeleted += (sender, e) =>
            {
                shopGo.Delete();
            };

            //Adding missions.
            double i = 1;

            foreach (Db.Mission mis in missions)
            {
                var misGo = new GameObject();

                double factor = 20;
                double x = 400 + Math.Sin(i * 0.8) * (i * Math.Pow(factor, 1 + 0.5 / i));
                double y = 400 + Math.Cos(i * 0.8) * (i * Math.Pow(factor, 1 + 0.5 / i));

                misGo.AddComponent(new Transform(new Vector2((int)x, (int)y)));
                misGo.AddComponent(new SpriteRender("Sprites/huse", 0.245f));
                misGo.AddComponent(new MouseDetector());
                Button btn = new Button();
                misGo.AddComponent(btn);
                btn.OnClick += (sender, e) =>
                {
                    GameWorld.Instance.MWManager.GotoMission(mis);
                    GameObject.Delete();
                };
                GameWorld.Instance.AddObject(misGo);

                GameObject.OnDeleted += (sender, e) =>
                {
                    misGo.Delete();
                };

                i++;
            }
        }
    }
}