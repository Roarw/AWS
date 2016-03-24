using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    class Map : Component, ILoadContent
    {
        private List<Db.Mission> missions;

        public Map()
        {
            missions = new List<Db.Mission>();

            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());
            missions.Add(new Db.Mission());

            using (Db.Connection con = new Db.Connection())
            {
                missions.AddRange(con.GetAllRows<Db.Mission>());
            }
        }
     
        public void LoadContent(ContentManager content)
        {
            double i = 1;

            foreach(Db.Mission mis in missions)
            {
                var misGo = new GameObject();
                
                double factor = 20;
                double x = 400 + Math.Sin(i * 0.8) * (i * Math.Pow(factor, 1 + 0.5 / i));
                double y = 400 + Math.Cos(i * 0.8) * (i * Math.Pow(factor, 1 + 0.5 / i));

                misGo.AddComponent(new Transform(new Vector2((int)x, (int)y)));
                misGo.AddComponent(new SpriteRender("Sprites/huse", 1));
                misGo.AddComponent(new MouseDetector());
                Button btn = new Button();
                misGo.AddComponent(btn);
                btn.OnClick += (sender, e) =>
                {
                    GameWorld.Instance.MWManager.GotoMission(mis);
                    GameObject.Delete();
                };

                GameObject.OnDeleted += (sender, e) =>
                {
                    misGo.Delete();
                };

                GameWorld.Instance.AddObject(misGo);

                i++;
            }
        }
    }
}
