using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim.Db
{
    internal class ShopWeapon : TableRow
    {
        public int ShopID { get; set; }
        public int WeaponID { get; set; }
        public int Price { get; set; }
    }
}
