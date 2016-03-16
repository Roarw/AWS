using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWS
{
    class NameGenereter
    {
        private string[] names = new string[15] {"Luim","aap","swart","poef","speeksel","lelike","adebowale","ayodele","dubaku","oog","boemelaar","jabari","imamu","john","mugabe" };
        Random random = new Random();
        public void GetRandomName
        {
            get { return names[random.Next(strings.Length)]; }
        }
        
    }
    
}
