using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace KonySim
{
    internal class MissionScreen : Component, ILoadContent
    {
        private Db.Mission mission;

        public MissionScreen(Db.Mission mission)
        {
            this.mission = mission;
        }

        public void LoadContent(ContentManager content)
        {
            var slot = new GameObject();
            slot.AddComponent(new Transform(new Vector2(10, 400)));
            slot.AddComponent(new SpriteRender("Sprites/SoldierSlot", 0.1f));
            slot.AddComponent(new SoldierSlot());
            slot.AddComponent(new TextRenderer("", Color.Red, 0.1f));
            GameObject.World.AddObject(slot);

            Text("Animals " + mission.AnimalCount, 10, 150);
            Text("Civilians " + mission.CivilianCount, 10, 200);
            Text("Children " + mission.ChildCount, 10, 250);
        }

        private void Text(string t, float x, float y)
        {
            var f = new GameObject();
            f.AddComponent(new Transform(new Vector2(x, y)));
            f.AddComponent(new TextRenderer(t, Color.Black, 0.1f));
            GameObject.World.AddObject(f);
        }
    }
}
