using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim.Db
{
    internal class Mission : TableRow
    {
        public bool Completed { get; set; }
        public int AnimalCount { get; set; }
        public int CivilianCount { get; set; }
        public int ChildCount { get; set; }
        public int DefenseMultiplier { get; set; }
        public int XpReward { get; set; }
        public int FundsReward { get; set; }
    }
}
