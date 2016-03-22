using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace KonySim
{
    class FightCalculator
    {
        static Random r = new Random();

        public static void MissionFight(Db.Connection con, List<Db.Soldier> soldiers, Db.Mission mission)
        {
            float soldiersPower = 0;
            float missionPower = mission.CivilianCount * (mission.DefenseMultiplier + r.Next(4)) * (1/99) +
                mission.AnimalCount * (mission.DefenseMultiplier + r.Next(4)) * 2;
            
            foreach (Db.Soldier s in soldiers)
            {
                float weaponPower = 0;

                foreach (Db.Weapon w in con.FindRowsWhere<Db.Weapon>("Weapon.ID", s.ID))
                {
                    weaponPower += w.Damage;
                }

                soldiersPower += (0/*replace 0 with drug bool*/ + 1 + (s.Lvl / 5)) * weaponPower;
            }

            if (soldiersPower >= missionPower)
            {
                //Child stuff.
                float totalExp = mission.DefenseMultiplier * mission.CivilianCount + mission.DefenseMultiplier * mission.AnimalCount * 200;

                foreach (Db.Soldier s in soldiers)
                {
                    s.Exp += (int)totalExp / soldiers.Count;
                    s.Lvl = (int)(Math.Pow((double)s.Exp, 0.8) / 100);
                    s.Health -= r.Next(10, 21);

                    if (s.Health <= 0)
                    {
                        /*Child dies.*/
                    }
                }

                //Mission stuff.
                GameWorld.Instance.State.Player.Funds = 0;

                mission.CivilianCount = 0;
                mission.AnimalCount = 0;
                mission.Completed = true;
            }
            else
            {
                /*Calculate loss*/
            }
        }
    }
}
