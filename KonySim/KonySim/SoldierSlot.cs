using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    internal class SoldierSlot : Component, ILoadContent
    {
        private TextRenderer text;
        private MissionScreen mission;
        private int slotId;

        private SpriteRender background;

        public SoldierSlot(MissionScreen mission, int slotId)
        {
            this.mission = mission;
            this.slotId = slotId;

            mission.SoldierSet += Mission_SoldierSet;
        }

        public void LoadContent(ContentManager content)
        {
            text = GameObject.GetComponent<TextRenderer>();
            var trans = GameObject.GetComponent<Transform>();
            var bgObj = new GameObject();
            bgObj.AddComponent(new Transform(new Microsoft.Xna.Framework.Vector2(trans.Position.X, trans.Position.Y)));
            background = new SpriteRender("ChildSprites/SoldierBackground", 0.01f);
            bgObj.AddComponent(background);
            GameObject.World.AddObject(bgObj);
        }

        private void Mission_SoldierSet(object sender, SoldierSetArgs e)
        {
            if (e.Slot == slotId)
            {
                if (e.Soldier != null)
                {
                    text.Text = e.Soldier.Name;
                    var texture = GameWorld.Instance.Content.Load<Texture2D>("ChildSprites/Soldier" + e.Soldier.PortraitIndex);
                    GameObject.GetComponent<SpriteRender>().SetSprite(texture);

                    int value = e.Soldier.PortraitColor;

                    Stack<int> stack = new Stack<int>();

                    for (; value > 0; value /= 1000)
                    {
                        stack.Push(value % 1000);
                    }

                    var array = stack.ToArray();
                    var col = new Color(array[0], array[1], array[2]);

                    background.SetColor(col);
                }
                else
                {
                    text.Text = "";
                }
            }
        }

        public void SetSoldier(Db.Soldier soldier)
        {
            mission.SetSoldier(slotId, soldier);
        }
    }
}
