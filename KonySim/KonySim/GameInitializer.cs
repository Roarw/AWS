using System;
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
        //De kunne gemmes i properties og bruges når Start() er kaldt.

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
                if (con.GetAllRows<Db.Player>().Count > 0) return; //Stop, there is already stuff in the database

                //Wipe all rows in case the tables are already populated
                con.DeleteAllRows<Db.Mission>();
                con.DeleteAllRows<Db.Player>();
                con.DeleteAllRows<Db.ShopWeapon>();
                con.DeleteAllRows<Db.Soldier>();
                con.DeleteAllRows<Db.StoredWeapon>();
                con.DeleteAllRows<Db.Weapon>();
                con.DeleteAllRows<Db.WeaponShop>();

                //Player setup
                var playerId = con.InsertRow(new Db.Player { Score = 0, Funds = 100, Buffs = 10 });

                //Initial soliders
                for (int i = 0; i < 5; i++)
                {
                    var s = Generator.NewChildForDB(0);
                    con.InsertRow(s);
                }

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

                    con.InsertRow(m);
                }

                //Weapon shop setup
                int shopId = con.InsertRow(new Db.WeaponShop { });

                for (int i = 0; i < 10; i++)
                {
                    var w = Generator.NewWeaponForDB(0);
                    int weaponId = con.InsertRow(w);

                    //con.InsertRow(new Db.ShopWeapon { ShopID = shopId, WeaponID = weaponId, Price = dmg });
                    con.InsertRow(new Db.StoredWeapon { PlayerID = playerId, WeaponID = weaponId });
                }
            }
        }
    }
}