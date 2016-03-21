using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim.Db
{
    static class Generator
    {
        static string[] names = new string[15] { "Luim", "aap", "swart", "poef", "speeksel", "lelike", "adebowale", "ayodele", "dubaku", "oog", "boemelaar", "jabari", "imamu", "john", "mugabe" };
        static Random random = new Random();

        static string GetRandomName
        {
            get { return names[random.Next(names.Length)]; }
        }

        public static void NewChildForDatabase(int exp)
        {
            Soldier soldier = new Soldier();
            soldier.Name = GetRandomName;
            soldier.Health = random.Next(70, 100);
            soldier.Exp = exp;
            soldier.PortraitIndex = 0;
            soldier.PortraitColor = GetRandomInt();
            soldier.PlayerID = 0;
            soldier.WeaponID = null;



            Math.Log(100, 200);
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
    }
}
