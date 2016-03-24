using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    internal class UI : Component, ILoadContent, IUpdate, IDraw
    {
        private ContentManager content;

        private List<GameObject> uiObjects;
        private SpriteFont iconFont;
        private UIList childrenList;
        private GameObject childrenListGO;

        private int statStart = 50;
        private int statDist = 220;

        //Initializing stuff.
        public UI()
        {
            uiObjects = new List<GameObject>();

            uiObjects.Add(UIBuilders.CreateImage("Sprites/Icon_Warrior", new Vector2(statStart + statDist * 0, 20)));
            uiObjects.Add(UIBuilders.CreateImage("Sprites/Icon_Syringe", new Vector2(statStart + statDist * 1, 20)));
            uiObjects.Add(UIBuilders.CreateImage("Sprites/Icon_Crack", new Vector2(statStart + statDist * 2, 20)));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (var o in uiObjects)
            {
                GameWorld.Instance.AddObject(o);
            }

            //Setting ContentManager.
            this.content = content;

            iconFont = content.Load<SpriteFont>("Fonts/IconFont");

            //Children list.
            childrenListGO = new GameObject();
            childrenList = new UIList(new Vector2(980, 0), 720, 0);
            childrenListGO.AddComponent(childrenList);
            childrenListGO.LoadContent(content);

            Rectangle bounds = childrenList.Bounds;

            foreach (var soldier in GameWorld.Instance.State.Soldiers)
            {
                childrenList.AddItem(CreateChildCard(soldier), content);
            }
        }

        public void Update(float deltaTime)
        {
            childrenListGO.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var player = GameWorld.Instance.State.Player;
            spriteBatch.DrawString(iconFont, player.Buffs.ToString(), new Vector2(60 + statStart, 20), Color.White);
            spriteBatch.DrawString(iconFont, "100", new Vector2(60 + statStart + statDist * 1, 20), Color.White);
            spriteBatch.DrawString(iconFont, player.Funds.ToString(), new Vector2(60 + statStart + statDist * 2, 20), Color.White);

            childrenListGO.Draw(spriteBatch);
        }

        //Creating a child card.
        private GameObject CreateChildCard(Db.Soldier soldier)
        {
            string picName = "ChildSprites/Soldier" + soldier.PortraitIndex;

            GameObject go = new GameObject();
            go.AddComponent(new Transform(Vector2.Zero));
            go.AddComponent(new SpriteRender("ChildSprites/ramme", 0.1f, childrenList.Bounds));
            go.AddComponent(new SpriteRender("ChildSprites/SoldierBackground", 0.2f, childrenList.Bounds, new Vector2(8, 9), Utils.IntToColor(soldier.PortraitColor)));
            go.AddComponent(new TextRenderer(soldier.Name, Color.Black, 0.3f, new Vector2(120, 20), "Fonts/smallIconFont"));
            go.AddComponent(new TextRenderer("Level " + soldier.Lvl, Color.Black, 0.3f, new Vector2(120, 40), "Fonts/smallIconFont"));
            go.AddComponent(new TextRenderer("Exp " + soldier.Exp, Color.Black, 0.3f, new Vector2(120, 60), "Fonts/smallIconFont"));
            go.AddComponent(new TextRenderer("Health " + soldier.Health, Color.Black, 0.3f, new Vector2(120, 80), "Fonts/smallIconFont"));
            go.AddComponent(new SpriteRender(picName, 0.3f, childrenList.Bounds, new Vector2(8, 9)));
            go.AddComponent(new MouseDetector());
            var btn = new Button();
            btn.OnClick += (sender, e) =>
            {
                //When the child card is clicked, it spawns a drag'n'drop object that disappears when the mouse is released.
                var go2 = new GameObject();
                go2.AddComponent(new Transform(Vector2.Zero));
                go2.AddComponent(new TextRenderer(soldier.Name, Color.Black, 1f));
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
                                slot.SetSoldier(soldier);
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
    }
}
