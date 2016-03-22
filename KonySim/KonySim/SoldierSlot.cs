using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace KonySim
{
    internal class SoldierSlot : Component, ILoadContent
    {
        private TextRenderer text;

        private Db.Soldier content;
        public Db.Soldier Content
        {
            get { return content; }
            set { content = value; text.Text = value.Name; }
        }

        public void LoadContent(ContentManager content)
        {
            text = GameObject.GetComponent<TextRenderer>();
        }
    }
}
