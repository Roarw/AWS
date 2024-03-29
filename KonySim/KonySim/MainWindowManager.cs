﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    internal class MainWindowManager : Component, ILoadContent
    {
        private ContentManager content;

        GameObject map;

        public void LoadContent(ContentManager content)
        {
            this.content = content;

            GameObject leftFrame = UIBuilders.CreateImageWOffset("Sprites/listFrame", new Vector2(0, 80), 0, 0, 635, 0, 0);
            GameObject topFrame = UIBuilders.CreateImageWOffset("Sprites/listFrame", new Vector2(5, 80), 0, 0, 10, 0, 970);
            GameObject bottomFrame = UIBuilders.CreateImageWOffset("Sprites/listFrame", new Vector2(5, 705), 0, 0, 10, 0, 970);
            GameObject background = UIBuilders.CreateImageWOffset("Sprites/BackGround", new Vector2(5, 95), 0, 0, 0, 0, 0);

            GameObject.OnDeleted += (sender, e) =>
            {
                leftFrame.Delete();
                topFrame.Delete();
                bottomFrame.Delete();
                background.Delete();
            };

            GameWorld.Instance.AddObject(leftFrame);
            GameWorld.Instance.AddObject(topFrame);
            GameWorld.Instance.AddObject(bottomFrame);
            GameWorld.Instance.AddObject(background);
        }

        public void GotoMission(Db.Mission mission)
        {
            var go = new GameObject();
            go.AddComponent(new MissionScreen(mission));
            GameWorld.Instance.AddObject(go);

            GameObject backBtnGo = UIBuilders.CreateImageWOffset("Sprites/BackButton", new Vector2(305, 95), 1f, 0, 0, 0, 0);
            backBtnGo.AddComponent(new MouseDetector());
            Button backBtn = new Button();
            backBtnGo.AddComponent(backBtn);
            backBtn.OnClick += (sender, e) =>
            {
                GotoWorldmap();
                go.Delete();
            };
            GameWorld.Instance.AddObject(backBtnGo);

            //Children list.
            GameObject weaponListGO = new GameObject();
            UIList weaponList = new UIList(new Vector2(5, 95), 610, 0.1f);
            weaponListGO.AddComponent(weaponList);
            GameWorld.Instance.AddObject(weaponListGO);

            using (var con = new Db.Connection())
            {
                //Find all weapons attacked to the active player
                var storedWeapons = con.GetAllRows<Db.StoredWeapon>();

                List<GameObject> weapons = new List<GameObject>();
                foreach (var s in storedWeapons)
                {
                    if (s.PlayerID == GameWorld.Instance.State.Player.ID)
                    {
                        var w = con.GetRow<Db.Weapon>(s.WeaponID);
                        var weaponCardGo = CreateWeaponCard(w, weaponList);
                        weapons.Add(weaponCardGo);
                    }
                }
                weaponList.UpdateItems(weapons);
            }

            /*Add weapons to the weaponlist like you did with children cards.*/

            GameObject.OnDeleted += (sender, e) =>
            {
                go.Delete();
            };

            go.OnDeleted += (sender, e) =>
            {
                backBtnGo.Delete();
                weaponListGO.Delete();
            };
        }

        private GameObject CreateWeaponCard(Db.Weapon weapon, UIList weaponList)
        {
            string picName = "WeaponSprites/Weapon" + weapon.PortraitIndex;

            GameObject go = new GameObject();
            go.AddComponent(new Transform(Vector2.Zero));
            go.AddComponent(new TextRenderer(weapon.Name + " (" + weapon.Damage + " DMG)", Color.Black, 0.4f, new Vector2(15, 50), "Fonts/smallIconFont"));
            go.AddComponent(new SpriteRender("WeaponSprites/wepRamme", 0.3f, weaponList.Bounds));
            go.AddComponent(new SpriteRender(picName, 0.4f, weaponList.Bounds, new Vector2(15, 9)));
            go.AddComponent(new MouseDetector());
            var btn = new Button();
            btn.OnClick += (sender, e) =>
            {
                //When the weapon card is clicked, it spawns a drag'n'drop object that disappears when the mouse is released.
                var go2 = new GameObject();
                go2.AddComponent(new Transform(Vector2.Zero));
                go2.AddComponent(new TextRenderer(weapon.Name, Color.Black, 1f));
                var dnd = new DragAndDropAlt(new Vector2(20, 20));
                dnd.Released += (sender2, e2) =>
                {
                    //When the drag'n'drop object disappears, check if it was dropped on a soldier slot
                    foreach (var obj in GameWorld.Instance.Objects)
                    {
                        SpriteRender md = obj.GetComponent<SpriteRender>();
                        SoldierSlot slot = obj.GetComponent<SoldierSlot>();
                        Transform trans = obj.GetComponent<Transform>();
                        if (md != null && slot != null && trans != null)
                        {
                            var rect = new Rectangle(md.Rectangle.Location, md.Rectangle.Size);
                            rect.Offset(trans.Position);
                            if (rect.Contains(e2.DropPosition))
                            {
                                slot.SetWeapon(weapon);
                            }
                        }
                    }
                };
                go2.AddComponent(dnd);
                GameWorld.Instance.AddObject(go2);
            };
            go.AddComponent(btn);
            return go;
        }

        public void GotoShop()
        {
            GameObject go = new GameObject();
            go.AddComponent(new Shop());
            GameWorld.Instance.AddObject(go);

            GameObject backBtnGo = UIBuilders.CreateImageWOffset("Sprites/BackButton", new Vector2(5, 95), 1f, 0, 0, 0, 0);
            backBtnGo.AddComponent(new MouseDetector());
            Button backBtn = new Button();
            backBtnGo.AddComponent(backBtn);
            backBtn.OnClick += (sender, e) =>
            {
                GotoWorldmap();
                go.Delete();
            };
            GameWorld.Instance.AddObject(backBtnGo);

            GameObject.OnDeleted += (sender, e) =>
            {
                go.Delete();
            };
            go.OnDeleted += (sender, e) =>
            {
                backBtnGo.Delete();
            };
        }

        public void GotoWorldmap()
        {
            map = new GameObject();
            map.AddComponent(new Transform(new Vector2(0, 90)));
            map.AddComponent(new SpriteRender("Sprites/map", 0.1f));
            map.AddComponent(new Map());
            GameWorld.Instance.AddObject(map);

            GameObject.OnDeleted += (sender, e) =>
            {
                map.Delete();
            };
        }
    }
}