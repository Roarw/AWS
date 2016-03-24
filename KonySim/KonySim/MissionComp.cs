using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    class MissionComp : Component , IDraw
    {
        Db.Mission mission;
        
        public MissionComp(int animalCount,int CivilianCount,int ChildCount,int DefenseMultiplier, int XpReward, int FundsReward)
        {
            mission = new Db.Mission();
            mission.AnimalCount = animalCount;
            mission.CivilianCount = CivilianCount;
            mission.ChildCount = ChildCount;
            mission.DefenseMultiplier = DefenseMultiplier;
          
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void ShowMission()
        {

        }
    }
}
