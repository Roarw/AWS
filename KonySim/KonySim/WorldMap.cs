using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KonySim
{
    class WorldMap : IMouseDetection, IDraw, ILoadContent, IUpdate
    {
        new List<GameObject> byer = new List<GameObject>();
        GameObject go = new GameObject();
       

        public void gameobject()
        {
            go.AddComponent(new Transform(new Vector2(10, 10)));
            byer.Add(go);
        }

        public void MouseEnter()
        {
            
        }

        public void MouseExit()
        {
            
        }

        public void MousePressed()
        {
            go.GetComponent<MissionComp>().ShowMission();
        }

        public void MouseReleased()
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in byer)
            {
                go.GetComponent<SpriteRender>().Draw(spriteBatch);
            }
        }

        public void LoadContent(ContentManager content)
        {
            go.LoadContent(content);
            go.AddComponent(new MissionComp(1, 1, 1, 1, 1, 1));
            go.AddComponent(new MouseDetector());
            go.AddComponent(new Transform(new Vector2(10, 10)));
            go.AddComponent(new SpriteRender("Sprites/iconWarrior", 5));
            byer.Add(go);

        }

        public void Update(float deltaTime)
        {
           
        }
    }
}
