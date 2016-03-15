using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim.Db
{
    internal class Player : TableRow
    {
        public int Score { get; set; }
        public int Funds { get; set; }
        public int Buffs { get; set; }
    }
}
