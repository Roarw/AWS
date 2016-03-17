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

        public GameState()
        {
            using (Db.Connection con = new Db.Connection())
            {
                player = con.GetAllRows<Db.Player>().First();
            }
        }
    }
}
