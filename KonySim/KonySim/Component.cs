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
            get
            {
                if (gameObject == null)
                {
                    throw new NullReferenceException("This component has not been attached to a GameObject.");
                }
                else
                {
                    return gameObject;
                }
            }
            set
            {
                if (gameObject == null)
                {
                    gameObject = value;
                }
                else
                {
                    throw new InvalidOperationException("This component already has a GameObject attached.");
                }
            }
        }

        public Component()
        {
        }
    }
}
