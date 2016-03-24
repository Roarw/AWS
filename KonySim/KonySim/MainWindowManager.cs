using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    class MainWindowManager : Component, ILoadContent
    {
        private ContentManager content;

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

            GameObject backBtn = UIBuilders.CreateImageWOffset("Sprites/BackButton", new Vector2(305, 95), 1f, 0, 0, 0, 0);
            GameWorld.Instance.AddObject(backBtn);

            //Children list.
            GameObject weaponListGO = new GameObject();
            UIList weaponList = new UIList(new Vector2(5, 95), 610, 0.1f);
            weaponListGO.AddComponent(weaponList);
            GameWorld.Instance.AddObject(weaponListGO);

            using (var con = new Db.Connection())
            {
                //Find all weapons attacked to the active player
                var storedWeapons = con.GetAllRows<Db.StoredWeapon>();
                foreach (var s in storedWeapons)
                {
                    if (s.PlayerID == GameWorld.Instance.State.Player.ID)
                    {
                        var w = con.GetRow<Db.Weapon>(s.WeaponID);
                        var weaponCardGo = CreateWeaponCard(w, weaponList);
                        weaponList.AddItem(weaponCardGo, GameWorld.Instance.Content);
                        GameWorld.Instance.AddObject(weaponCardGo);
                    }
                }
            }

            /*Add weapons to the weaponlist like you did with children cards.*/

            go.OnDeleted += (sender, e) =>
            {
                backBtn.Delete();
                weaponListGO.Delete();
            };
        }

        private GameObject CreateWeaponCard(Db.Weapon weapon, UIList weaponList)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(Vector2.Zero));
            go.AddComponent(new TextRenderer(weapon.Name, Color.Black, 0.3f));
            go.AddComponent(new SpriteRender("ChildSprites/ramme", 0.2f, weaponList.Bounds));
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

        public void GotoShop(Db.WeaponShop weaponShop)
        {
        }

        public void GotoDrugstore()
        {
        }

        public void GotoWorldmap(List<Db.Mission> missions)
        {
            //GameObject backBtn = UIBuilders.CreateImageWOffset("Sprites/BackButton", new Vector2(0, 95), 1f, 0, 0, 0, 0);
            //GameWorld.Instance.AddObject(backBtn);
            //currentObjects.Add(backBtn);

            //GameWorld.Instance.CreateGo(Vector2.Zero);
            //GameWorld.Instance.CreateGo(new Vector2(100, 400));
            //var map = new GameObject();
            //map.AddComponent(new Transform(new Vector2(0, 90)));
            //map.AddComponent(new SpriteRender("Sprites/map", 0));

            //var mis2 = new GameObject();
            //mis2.AddComponent(new Transform(new Vector2(630, 200)));
            //mis2.AddComponent(new SpriteRender("Sprites/huse", 1));
            //mis2.AddComponent(new MouseDetector());
            //mis2.AddComponent(new WorldMap(mis2));
            //mis2.AddComponent(new MissionComp(5, 5, 5, 5, 5, 5));

            //var mis1 = new GameObject();
            //mis1.AddComponent(new Transform(new Vector2(610, 400)));
            //mis1.AddComponent(new SpriteRender("Sprites/huse", 1));
            //mis1.AddComponent(new MouseDetector());
            //mis1.AddComponent(new WorldMap(mis1));
            //mis1.AddComponent(new MissionComp(5, 5, 5, 5, 5, 5));
            //var Weapon = new GameObject();
            //Weapon.AddComponent(new Transform(new Vector2(150, 500)));
            //Weapon.AddComponent(new SpriteRender("sprites/weaponshop", 1));

            //GameWorld.Instance.AddObject(map);
            //GameWorld.Instance.AddObject(mis1);
            //GameWorld.Instance.AddObject(mis2);
            //GameWorld.Instance.AddObject(Weapon);

            //currentObjects.Add(map);
            //currentObjects.Add(mis1);
            //currentObjects.Add(mis2);
            //currentObjects.Add(Weapon);
        }
    }
}
