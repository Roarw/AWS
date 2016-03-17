using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    internal class NameGenerator
    {
        private string[] names = new string[15] { "Luim", "aap", "swart", "poef", "speeksel", "lelike", "adebowale", "ayodele", "dubaku", "oog", "boemelaar", "jabari", "imamu", "john", "mugabe" };

        private Random random = new Random();
        public string GetRandomName
        {
            get { return names[random.Next(names.Length)]; }
        }
    }
}
