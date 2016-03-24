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
        private TextRenderer weaponText;
        private MissionScreen mission;
        private int slotId;
        private Texture2D startTexture;

        private SpriteRender background;

        public SoldierSlot(MissionScreen mission, int slotId)
        {
            this.mission = mission;
            this.slotId = slotId;

            mission.SoldierSet += Mission_SoldierSet;
            mission.WeaponSet += Mission_WeaponSet;
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

            var weaponTextGo = new GameObject();
            weaponTextGo.AddComponent(new Transform(new Vector2(trans.Position.X, trans.Position.Y + 110)));
            weaponText = new TextRenderer("No weapon", Color.Black, 0.6f, "Fonts/smallIconFont");
            weaponTextGo.AddComponent(weaponText);
            GameWorld.Instance.AddObject(weaponTextGo);

            GameObject.OnDeleted += (sender, e) =>
            {
                bgObj.Delete();
                textGo.Delete();
                weaponTextGo.Delete();
            };

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

                    var col = Utils.IntToColor(e.Soldier.PortraitColor);
                    background.SetColor(col);
                }
                else
                {
                    text.Text = "";

                    GameObject.GetComponent<SpriteRender>().Sprite = startTexture;
                }
            }
        }

        private void Mission_WeaponSet(object sender, WeaponSetArgs e)
        {
            if (e.Slot == slotId)
            {
                if (e.Weapon != null)
                {
                    weaponText.Text = e.Weapon.Name;
                }
                else
                {
                    weaponText.Text = "No weapon";
                }
            }
        }

        public void SetSoldier(Db.Soldier soldier)
        {
            mission.SetSoldier(slotId, soldier);
        }

        public void SetWeapon(Db.Weapon weapon)
        {
            mission.SetWeapon(slotId, weapon);
        }
    }
}
