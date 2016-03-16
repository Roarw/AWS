using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim.Db
{
    internal class StoredWeapon : TableRow
    {
        public int PlayerID { get; set; }
        public int WeaponID { get; set; }
    }
}
