using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace KonySim
{
    class FightCalculator
    {
        Random r = new Random();

        public static void MissionFight(Db.Connection con, List<Db.Soldier> soldiers, Db.Mission mission)
        {
            float soldiersPower = 0;
            float missionPower = mission.CivilianCount * mission.DefenseMultiplier * (1/99) +
                mission.AnimalCount * (mission.DefenseMultiplier + r.Next(4)) * 2;
            
            foreach (Db.Soldier s in soldiers)
            {
                float weaponPower = 0;

                foreach (Db.Weapon w in con.FindRowsWhere<Db.Weapon>("Weapon.ID", s.ID))
                {
                    weaponPower += w.Damage;
                }

                soldiersPower += (0 + 1 + (s.Lvl / 5)) * weaponPower;
            }

            if (soldiersPower >= missionPower)
            {
                /*Calculate win*/
            }
            else
            {
                /*Calculate loss*/
            }
        }
    }
}
