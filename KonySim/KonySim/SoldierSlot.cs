using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace KonySim
{
    internal class SoldierSlot : Component, ILoadContent
    {
        private TextRenderer text;
        private MissionScreen mission;
        private int slotId;

        public SoldierSlot(MissionScreen mission, int slotId)
        {
            this.mission = mission;
            this.slotId = slotId;

            mission.SoldierSet += Mission_SoldierSet;
        }

        private void Mission_SoldierSet(object sender, SoldierSetArgs e)
        {
            if (e.Slot == slotId)
            {
                if (e.Soldier != null)
                    text.Text = e.Soldier.Name;
                else
                    text.Text = "";
            }
        }

        public void SetSoldier(Db.Soldier soldier)
        {
            mission.SetSoldier(slotId, soldier);
        }

        public void LoadContent(ContentManager content)
        {
            text = GameObject.GetComponent<TextRenderer>();
        }
    }
}
