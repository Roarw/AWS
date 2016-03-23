﻿using KonySim.Db;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    class Generator
    {
        static string[] childNames = new string[] { "Luim", "Aap", "Swart", "Poef", "Speeksel", "Lelike", "Adebowale", "Ayodele", "Dubaku", "Oog", "Boemelaar", "Jabari", "Imamu", "John", "Mugabe" };
        static string[] weaponNames = new string[] { "Gun" };
        static Random random = new Random();

        public static Soldier NewChildForDB(int exp)
        {
            Soldier soldier = new Soldier();
            soldier.Name = childNames[random.Next(childNames.Length)];
            soldier.Health = random.Next(70, 100);
            soldier.Exp = exp;
            soldier.PortraitIndex = GetRandomImageInt();
            soldier.PortraitColor = GetRandomInt();
            soldier.PlayerID = GameWorld.Instance.State.Player.ID;
            soldier.WeaponID = null;

            return soldier;
        }

        public static Weapon NewWeaponForDB(int dmg)
        {
            Weapon weapon = new Weapon();
            weapon.Name = weaponNames[random.Next(weaponNames.Length)];
            weapon.Damage = dmg;

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

        private static int GetRandomInt()
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

        private static int GetRandomImageInt()
        {
            return random.Next(1, 14 /*This value is the amount of child pictures + 1*/);
        }
    }
}
