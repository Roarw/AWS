using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    internal class GameState
    {
        private Db.Player player;
        public Db.Player Player { get { return player; } }

        private List<Db.Soldier> soldiers;
        public List<Db.Soldier> Soldiers { get { return soldiers; } }

        private List<Db.Soldier> unsavedRemovals = new List<Db.Soldier>();

        public GameState()
        {
            using (Db.Connection con = new Db.Connection())
            {
                player = con.GetAllRows<Db.Player>().First();
                soldiers = con.GetAllRows<Db.Soldier>().Where(a => a.PlayerID == player.ID).ToList();
            }
        }

        public void Save()
        {
            using (Db.Connection con = new Db.Connection())
            {
                con.UpdateRow(player);
                foreach (var s in soldiers) con.InsertOrUpdateRow(s);

                foreach (var s in unsavedRemovals)
                {
                    con.DeleteRow<Db.Soldier>(s.ID);
                }
                unsavedRemovals.Clear();
            }
        }

        public void AddSoldiers(List<Db.Soldier> sList)
        {
            foreach (Db.Soldier s in sList)
            {
                soldiers.Add(s);
            }
        }

        public void RemoveSoldiers(Db.Soldier s)
        {
            if (soldiers.Contains(s))
            {
                soldiers.Remove(s);
                unsavedRemovals.Add(s);
            }
        }
    }
}
