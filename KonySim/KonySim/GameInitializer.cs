﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    internal class GameInitializer
    {
        private GameWorld world;
        private Random random;

        //Denne klasse kunne evt. bruges til at holde på indstillinger, spilleren vælger, før spillet startes (feks. sværhedsgrad).
        //Start() sin egen funktion så dette nemt kan implementeres (og fordi det ser nice ud)

        public GameInitializer(GameWorld world, Random random)
        {
            this.world = world;
            this.random = random;
        }

        public void Start()
        {
            var gi = new GameInitializer(world, random);

            using (Db.Connection con = new Db.Connection())
            {
                //Wipe all rows in case the tables are already populated
                con.DeleteAllRows<Db.Mission>();
                con.DeleteAllRows<Db.Player>();
                con.DeleteAllRows<Db.ShopWeapon>();
                con.DeleteAllRows<Db.Soldier>();
                con.DeleteAllRows<Db.StoredWeapon>();
                con.DeleteAllRows<Db.Weapon>();
                con.DeleteAllRows<Db.WeaponShop>();

                //Player setup
                con.InsertRow(new Db.Player { Score = 0, Funds = 100, Buffs = 10 });

                //Mission setup
                for (int i = 0; i < 10; i++)
                {
                    var m = new Db.Mission
                    {
                        Completed = false,
                        AnimalCount = random.Next(3, 20),
                        CivilianCount = random.Next(1, 100),
                        ChildCount = random.Next(1, 30),
                        DefenseMultiplier = random.Next(1, 4),
                    };
                    m.XpReward = (m.CivilianCount + m.ChildCount + m.AnimalCount) * m.DefenseMultiplier;
                    m.FundsReward = (m.CivilianCount + m.AnimalCount) * m.DefenseMultiplier;

                    con.InsertRow(m);
                }

                //Weapon shop setup
                int shopId = con.InsertRow(new Db.WeaponShop { });

                for (int i = 0; i < 10; i++)
                {
                    string n = "";
                    int dmg = 0;
                    int p = 0;

                    int pick = random.Next(3);
                    switch (pick)
                    {
                        case 0:
                            n = "AK-47";
                            dmg = 5;
                            break;
                        case 1:
                            n = "RPG";
                            dmg = 15;
                            break;
                        case 2:
                            n = "Nerf gun";
                            dmg = 1;
                            break;
                    }

                    var w = new Db.Weapon
                    {
                        Name = n,
                        Damage = dmg
                    };

                    int weaponId = con.InsertRow(w);

                    con.InsertRow(new Db.ShopWeapon { ShopID = shopId, WeaponID = weaponId, Price = dmg });
                }
            }
        }
    }
}