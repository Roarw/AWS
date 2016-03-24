using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim.Db
{
    internal class Weapon : TableRow
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public int PortraitIndex { get; set; }
    }
}
