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
        List<GameObject> byer = new List<GameObject>();
       

        public void gameobject()
        {
        }

        public void MouseEnter()
        {
            
        }

        public void MouseExit()
        {
            
        }

        public void MousePressed()
        {
            //go.GetComponent<MissionComp>().ShowMission();
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
            
           

        }

        public void Update(float deltaTime)
        {
           
        }
    }
}
