using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KonySim.Db;
using Microsoft.Xna.Framework;

namespace KonySim
{
    internal class Generator
    {
        private static string[] childNames = new string[] { "Luim", "Aap", "Swart", "Poef", "Speeksel", "Lelike", "Adebowale", "Ayodele", "Dubaku", "Oog", "Boemelaar", "Jabari", "Imamu", "John", "Mugabe" };
        private static string[] weaponNames = new string[] { "Gun" };
        private static Random random = new Random();

        public static Soldier NewChildForDB(int exp, int playerId)
        {
            Soldier soldier = new Soldier();
            soldier.Name = childNames[random.Next(childNames.Length)];
            soldier.Health = random.Next(70, 100);
            soldier.Exp = exp;
            soldier.PortraitIndex = random.Next(1, 14 /*This value is the amount of child pictures + 1*/);
            soldier.PortraitColor = RandomImageInt();
            soldier.PlayerID = playerId;

            return soldier;
        }

        public static Weapon NewWeaponForDB(int missionsCompleted)
        {
            Weapon weapon = new Weapon();
            weapon.Name = weaponNames[random.Next(weaponNames.Length)];
            weapon.Damage = 10 + random.Next(missionsCompleted, (int)((float)missionsCompleted * 1.5f));
            weapon.PortraitIndex = random.Next(1, 6 /*This value is the amount of child pictures + 1*/);

            return weapon;
        }

        public static Mission NewMissionForDB(int difficulty, bool poachingMission)
        {
            Mission mission = new Mission();
            if (poachingMission)
            {
                mission.AnimalCount = random.Next(difficulty + 1, difficulty * 2 + 1);
                mission.CivilianCount = 0;
            }
            else
            {
                mission.CivilianCount = random.Next(difficulty * 200 + 1, difficulty * 400 + 1);
                mission.AnimalCount = 0;
            }
            mission.ChildCount = random.Next(1, 5);
            mission.Completed = false;
            mission.DefenseMultiplier = random.Next((difficulty / 2) + 1, difficulty + 1);

            return mission;
        }

        private static int RandomImageInt()
        {
            HashSet<int> tempSet = new HashSet<int>();

            for (int i = 0; i < 3; i++)
            {
                int r = random.Next(100, 255);

                while (tempSet.Contains(r))
                {
                    r = random.Next(100, 255);
                }

                tempSet.Add(r);
            }

            string returnInt = "";

            foreach (int i in tempSet)
            {
                returnInt += i + "";
            }

            return Int32.Parse(returnInt);
        }
    }
}