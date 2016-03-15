using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KonySim
{
    abstract class Component
    {
        protected GameObject gameObject;

        public GameObject GameObject { get { return gameObject; } }

        public Component()
        {

        }

        public Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
