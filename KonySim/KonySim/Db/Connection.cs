using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KonySim.Db
{
    internal class Connection : IDisposable
    {
        private SQLiteConnection con;

        public Connection()
        {
            con = new SQLiteConnection("Data Source=data.db");
            con.Open();

            var reader = new SQLiteCommand("SELECT * FROM sqlite_master WHERE type='table'", con).ExecuteReader();
            if (!reader.Read())
            {
                //No tables, create them all
                var str = @"
CREATE TABLE Player(ID integer primary key, score integer not null, funds integer not null, buffs integer not null);
CREATE TABLE Soldier(ID integer primary key, name string not null, health integer not null, exp integer not null, lvl integer not null,
    portraitIndex integer not null, playerID integer not null, weaponID integer,
    FOREIGN KEY (playerID) REFERENCES Player(ID), FOREIGN KEY (weaponID) REFERENCES Weapon(ID));
    CREATE TABLE Weapon (ID integer primary key, name string not null, damage integer not null);
CREATE TABLE WeaponShop (ID integer primary key);
CREATE TABLE Mission (ID integer primary key, completed boolean not null, animalCount integer not null, civilianCount integer not null, childCount integer not null,
    defenseMultiplier integer not null, xpReward integer not null, fundsReward integer not null);
CREATE TABLE StoredWeapon(ID integer primary key, playerID integer, weaponID integer,
    FOREIGN KEY (playerID) REFERENCES Player(ID), FOREIGN KEY (weaponID) REFERENCES Weapon(ID));
CREATE TABLE ShopWeapon(ID integer primary key, shopID integer not null, weaponID integer not null, price integer not null,
    FOREIGN KEY (shopID) REFERENCES Shop(ID), FOREIGN KEY (weaponID) REFERENCES Weapon(ID));";

                new SQLiteCommand(str, con).ExecuteNonQuery();
            }
        }

        public List<T> GetAllRows<T>() where T : TableRow, new()
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM " + typeof(T).Name, con);

            List<T> result = new List<T>();

            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                T item = new T();
                FillPropertiesFromRow(item, reader);

                result.Add(item);
            }

            return result;
        }

        public T GetRow<T>(int ID) where T : TableRow, new()
        {
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM " + typeof(T).Name + " WHERE ID = " + ID, con);

            SQLiteDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var result = new T();
                FillPropertiesFromRow(result, reader);
                return result;
            }
            else
            {
                return null;
            }
        }

        private void FillPropertiesFromRow<T>(T target, SQLiteDataReader reader)
        {
            PropertyInfo[] propertiesToFill = typeof(T).GetProperties();
            foreach (PropertyInfo p in propertiesToFill)
            {
                object value = reader[p.Name];

                if (value != DBNull.Value)
                {
                    value = Convert.ChangeType(value, p.PropertyType);
                    p.SetValue(target, value, null);
                }
            }
        }

        public void Dispose()
        {
            con.Dispose();
        }
    }

    internal class TableRow
    {
        public int ID { get; set; }
    }

    internal class Player : TableRow
    {
        public int Score { get; set; }
        public int Funds { get; set; }
        public int Buffs { get; set; }
    }
}
