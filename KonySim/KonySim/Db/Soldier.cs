using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim.Db
{
    internal class Soldier : TableRow
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Exp { get; set; }
        public int Lvl { get; set; }
        public int PortraitIndex { get; set; }
        public int PortraitColor { get; set; }
        public int PlayerID { get; set; }
    }
}
