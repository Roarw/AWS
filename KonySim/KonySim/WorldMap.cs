using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KonySim
{

    class WorldMap :Component , IMouseDetection
    {
        List<GameObject> byer = new List<GameObject>();
       

       

        public WorldMap(GameObject go)
        {

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

            this.GameObject.GetComponent<MissionComp>().ShowMission();

        }

        public void MouseReleased()
        {
            

        }

 
    }
}
