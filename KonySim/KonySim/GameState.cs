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
            }
        }

        public void AddSoldiers(List<Db.Soldier> sList)
        {
            foreach (Db.Soldier s in sList)
            {
                soldiers.Add(s);
            }
            
            GameWorld.Instance.UI.UpdateList();
        }
    }
}
