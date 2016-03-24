using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    class Map : Component, ILoadContent, IDraw, IUpdate
    {
     
        private List<Db.Mission> missions = new List<Db.Mission>();

        public Map(GameObject go)
        {
            using (Db.Connection con = new Db.Connection())
            {
                missions.AddRange(con.GetAllRows<Db.Mission>());
            }
           
        }
     
        public void LoadContent(ContentManager content)
        {
            var i = 0;
            foreach(Db.Mission mis in missions)
            {
                var mis1 = new GameObject();
                mis1.AddComponent(new Transform(new Vector2(i * 100, 400)));
                mis1.AddComponent(new SpriteRender("Sprites/huse", 1));
                mis1.AddComponent(new MouseDetector());
                Button btn = new Button();
                mis1.AddComponent(btn);
                i++;
                GameWorld.Instance.AddObject(mis1);
                btn.OnClick += (sender, e) =>
                 {
                     GameObject go = new GameObject();
                     go.AddComponent(new MissionScreen(mis));
                     GameWorld.Instance.AddObject(go);
                 };
            }
            
        }
       
        public void Update(float deltaTime)
        {
           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

      
    }
}
