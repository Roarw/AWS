using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    internal abstract class Component
    {
        private GameObject gameObject;

        public GameObject GameObject
        {
            get { return gameObject; }
            set
            {
                if (gameObject == null)
                {
                    gameObject = value;
                }
                else
                {
                    throw new InvalidOperationException("What the fuck man");
                }
            }
        }

        public Component()
        {
        }
    }
}
