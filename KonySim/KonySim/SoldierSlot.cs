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
        private Texture2D startTexture;

        private SpriteRender background;

        public SoldierSlot(MissionScreen mission, int slotId)
        {
            this.mission = mission;
            this.slotId = slotId;

            mission.SoldierSet += Mission_SoldierSet;
        }

        public void LoadContent(ContentManager content)
        {
            var trans = GameObject.GetComponent<Transform>();
            var bgObj = new GameObject();
            bgObj.AddComponent(new Transform(new Vector2(trans.Position.X, trans.Position.Y)));
            background = new SpriteRender("ChildSprites/SoldierBackground", 0.01f);
            bgObj.AddComponent(background);
            GameWorld.Instance.AddObject(bgObj);

            var textGo = new GameObject();
            textGo.AddComponent(new Transform(new Vector2(trans.Position.X, trans.Position.Y - 32)));
            text = new TextRenderer("", Color.Black, 0.6f, "Fonts/smallIconFont");
            textGo.AddComponent(text);
            GameWorld.Instance.AddObject(textGo);

            startTexture = GameObject.GetComponent<SpriteRender>().Sprite;
        }

        private void Mission_SoldierSet(object sender, SoldierSetArgs e)
        {
            if (e.Slot == slotId)
            {
                if (e.Soldier != null)
                {
                    text.Text = e.Soldier.Name;
                    var texture = GameWorld.Instance.Content.Load<Texture2D>("ChildSprites/Soldier" + e.Soldier.PortraitIndex);
                    GameObject.GetComponent<SpriteRender>().Sprite = texture;

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

                    GameObject.GetComponent<SpriteRender>().Sprite = startTexture;
                }
            }
        }

        public void SetSoldier(Db.Soldier soldier)
        {
            mission.SetSoldier(slotId, soldier);
        }
    }
}
