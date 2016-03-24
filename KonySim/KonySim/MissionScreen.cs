using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace KonySim
{
    internal class SoldierSetArgs : EventArgs
    {
        public int Slot { get; }
        public Db.Soldier Soldier { get; }

        public SoldierSetArgs(int slot, Db.Soldier soldier)
        {
            Slot = slot;
            Soldier = soldier;
        }
    }

    internal class WeaponSetArgs : EventArgs
    {
        public int Slot { get; }
        public Db.Weapon Weapon { get; }

        public WeaponSetArgs(int slot, Db.Weapon weapon)
        {
            Slot = slot;
            Weapon = weapon;
        }
    }

    internal class MissionScreen : Component, ILoadContent
    {
        private Db.Mission mission;

        private const int slotCount = 3;

        private Db.Soldier[] soldiers = new Db.Soldier[slotCount];
        private Db.Weapon[] weapons = new Db.Weapon[slotCount];

        public event EventHandler<SoldierSetArgs> SoldierSet;
        public event EventHandler<WeaponSetArgs> WeaponSet;

        public MissionScreen(Db.Mission mission)
        {
            this.mission = mission;
        }

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < slotCount; i++)
            {
                var slot = new GameObject();
                slot.AddComponent(new Transform(new Vector2(400 + i * 128, 400)));
                slot.AddComponent(new SpriteRender("Sprites/SoldierSlot", 0.1f));
                slot.AddComponent(new SoldierSlot(this, i));
                slot.AddComponent(new TextRenderer("", Color.Red, 0.4f));
                slot.AddComponent(new MouseDetector());
                var button = new Button();
                int i2 = i;
                button.OnClick += (sender, e) =>
                {
                    SetSoldier(i2, null);
                    SetWeapon(i2, null);
                };
                slot.AddComponent(button);
                GameWorld.Instance.AddObject(slot);
            }

            Text("Animals " + mission.AnimalCount, 400, 150);
            Text("Civilians " + mission.CivilianCount, 400, 200);
            Text("Children " + mission.ChildCount, 400, 250);
        }

        public void SetSoldier(int slot, Db.Soldier s)
        {
            if (s != null)
            {
                for (int i = 0; i < slotCount; i++)
                {
                    if (soldiers[i] == s)
                    {
                        return;
                    }
                }
            }

            soldiers[slot] = s;

            if (SoldierSet != null)
                SoldierSet(this, new SoldierSetArgs(slot, s));
        }

        public void SetWeapon(int slot, Db.Weapon w)
        {
            if (w != null)
            {
                for (int i = 0; i < slotCount; i++)
                {
                    if (weapons[i] == w)
                    {
                        return;
                    }
                }

                if (soldiers[slot] == null)
                {
                    return;
                }
            }

            weapons[slot] = w;

            if (WeaponSet != null)
                WeaponSet(this, new WeaponSetArgs(slot, w));
        }

        private void Text(string t, float x, float y)
        {
            var f = new GameObject();
            f.AddComponent(new Transform(new Vector2(x, y)));
            f.AddComponent(new TextRenderer(t, Color.Black, 0.1f));
            GameWorld.Instance.AddObject(f);
        }
    }
}
