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
        static GameWorld gameWorld;

        static string[] names = new string[] { "Luim", "Aap", "Swart", "Poef", "Speeksel", "Lelike", "Adebowale", "Ayodele", "Dubaku", "Oog", "Boemelaar", "Jabari", "Imamu", "John", "Mugabe" };
        static Random random = new Random();
        

        public Generator(GameWorld gameWorld)
        {
            Generator.gameWorld = gameWorld;
        }

        public static Soldier NewChildForDB(int exp)
        {
            Soldier soldier = new Soldier();
            soldier.Name = GetRandomName();
            soldier.Health = random.Next(70, 100);
            soldier.Exp = exp;
            soldier.PortraitIndex = GetRandomImageInt();
            soldier.PortraitColor = GetRandomInt();
            soldier.PlayerID = gameWorld.State.Player.ID;
            soldier.WeaponID = null;

            return soldier;
        }

        static string GetRandomName()
        {
            return names[random.Next(names.Length)];
        }

        private static int GetRandomInt()
        {
            HashSet<int> tempSet = new HashSet<int>();

            for (int i = 0; i < 3; i++)
            {
                int r = random.Next(0, 255);

                while (tempSet.Contains(r))
                {
                    r = random.Next(0, 255);
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
            return random.Next(1, 14 /*This value is the highest child picture number +  1*/);
        }
    }
}