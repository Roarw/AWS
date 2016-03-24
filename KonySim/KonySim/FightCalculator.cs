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

        public static void MissionFight(Db.Connection con, Db.Soldier[] soldiers, Db.Weapon[] weapons, Db.Mission mission)
        {
            float soldiersPower = 0;
            float missionPower = (float)(mission.CivilianCount * (mission.DefenseMultiplier + r.Next(4))) * (1/99) +
                mission.AnimalCount * (mission.DefenseMultiplier + r.Next(4)) * 2;
            
            for (int i = 0; i < soldiers.Length; i++)
            {
                float weaponPower = 0;

                if (weapons[i] != null)
                {
                    weaponPower += weapons[i].Damage;
                }

                if (soldiers[i] != null)
                {
                    soldiersPower += (0/*replace 0 with drug bool*/ + 1 + ((float)soldiers[i].Lvl / 5)) * weaponPower;
                }
            }


            ///*
            /// Win or loss of battle is administrated below.
            /// */

            //Winning battle.
            if (soldiersPower >= missionPower)
            {
                //Child stuff.
                float totalExp = mission.DefenseMultiplier * mission.CivilianCount + mission.DefenseMultiplier * mission.AnimalCount * 200;

                foreach (Db.Soldier s in soldiers)
                {
                    s.Exp += (int)((float)totalExp / (float)soldiers.Length);
                    s.Lvl = (int)(Math.Pow((double)s.Exp, 0.8) / 100);
                    s.Health -= r.Next(10, 21);

                    if (s.Health <= 0)
                    {
                        /*Child dies.*/
                    }
                }

                //Mission stuff.
                GameWorld.Instance.State.Player.Funds += (int)((float)(mission.CivilianCount + mission.AnimalCount * 200) * ((float)r.Next(5, 16) / 100));
                GameWorld.Instance.State.Player.Score += mission.CivilianCount + mission.AnimalCount * 200;

                //Getting new children.
                for (int i = 0; i < mission.ChildCount; i++)
                {
                    GameWorld.Instance.State.Soldiers.Add(Generator.NewChildForDB(0 /*A higher value possible?*/));
                }

                mission.CivilianCount = 0;
                mission.AnimalCount = 0;
                mission.Completed = true;
            }

            //Losing battle.
            else
            {
                float powerDifference = 1 / (1 + ((missionPower - soldiersPower) / soldiersPower));

                //Child stuff.
                float totalExp = (float)(mission.DefenseMultiplier * mission.CivilianCount) * powerDifference + (float)(mission.DefenseMultiplier * mission.AnimalCount * 200) * powerDifference;

                foreach (Db.Soldier s in soldiers)
                {
                    if (s != null)
                    {
                        s.Exp += (int)((float)totalExp / (float)soldiers.Length);
                        s.Lvl = (int)(Math.Pow((double)s.Exp, 0.8) / 100);
                        s.Health -= (int)Math.Pow((double)r.Next(25, 201), Math.Log(100, 200));

                        if (s.Health <= 0)
                        {
                            /*Child dies.*/
                        }
                    }
                }

                //Mission stuff.
                GameWorld.Instance.State.Player.Funds += (int)((float)(mission.CivilianCount + mission.AnimalCount * 200) * ((float)r.Next(5, 16) / 100) * powerDifference);
                GameWorld.Instance.State.Player.Score += (int)((float)(mission.CivilianCount + mission.AnimalCount * 200) * powerDifference);

                System.Diagnostics.Debug.WriteLine(GameWorld.Instance.State.Player.Funds);
                System.Diagnostics.Debug.WriteLine(GameWorld.Instance.State.Player.Score);

                mission.CivilianCount = mission.CivilianCount - (int)((float)mission.CivilianCount * powerDifference);
                mission.AnimalCount = mission.AnimalCount - (int)((float)mission.AnimalCount * powerDifference);
            }


            ///*
            /// Saving the data to database. We are assuming the soldiers in the fight were contained in GameState.Soldiers.
            /// */

            GameWorld.Instance.State.Save();
            con.UpdateRow(mission);
        }
    }
}
