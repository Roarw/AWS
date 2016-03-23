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

        private int statStart = 100;
        private int statDist = 300;

        //Initializing stuff.
        public UI()
        {
            uiObjects = new List<GameObject>();

            uiObjects.Add(CreateImage("Sprites/BackButton", new Vector2(0, 0)));
            uiObjects.Add(CreateImage("Sprites/Icon_Syringe", new Vector2(statStart, 20)));
            uiObjects.Add(CreateImage("Sprites/Icon_Warrior", new Vector2(statStart + statDist, 20)));
            uiObjects.Add(CreateImage("Sprites/Icon_Crack", new Vector2(statStart + statDist * 2, 20)));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (var o in uiObjects)
            {
                GameObject.World.AddObject(o);
            }

            //Setting ContentManager.
            this.content = content;

            iconFont = content.Load<SpriteFont>("Fonts/IconFont");

            //Children list.
            childrenList = new UIList(new Vector2(980, 0), 720);
            childrenList.LoadContent(content);

            Rectangle bounds = childrenList.Bounds;

            foreach (var soldier in GameObject.World.State.Soldiers)
            {
                childrenList.AddItem(ChildCard(soldier), content);
            }

            Db.Soldier ss = Generator.NewChildForDB(0);
            childrenList.AddItem(ChildCard(ss), content);
        }

        public void Update(float deltaTime)
        {
            childrenList.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var player = GameObject.World.State.Player;
            spriteBatch.DrawString(iconFont, player.Buffs.ToString(), new Vector2(60 + statStart, 20), Color.White);
            spriteBatch.DrawString(iconFont, "100", new Vector2(60 + statStart + statDist, 20), Color.White);
            spriteBatch.DrawString(iconFont, player.Funds.ToString(), new Vector2(60 + statStart + statDist * 2, 20), Color.White);

            childrenList.Draw(spriteBatch);
        }

        //Creating an image.
        private GameObject CreateImage(string sprite, Vector2 position)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(position));
            go.AddComponent(new SpriteRender(sprite, 0.1f));
            return go;
        }

        //Creating a child card.
        private GameObject ChildCard(Db.Soldier soldier)
        {
            int[] rgb = IntToByteArray(soldier.PortraitColor);
            Color color = new Color(rgb[0], rgb[1], rgb[2]);

            GameObject go = new GameObject();
            go.AddComponent(new Transform(Vector2.Zero));
            go.AddComponent(new SpriteRender("ChildSprites/ramme", 0.1f, childrenList.Bounds));
            go.AddComponent(new SpriteRender("ChildSprites/SoldierBackground", 0.2f, childrenList.Bounds, new Vector2(8, 9), color));
            go.AddComponent(new SpriteRender("ChildSprites/Soldier" + soldier.PortraitIndex, 0.3f, childrenList.Bounds, new Vector2(8, 9)));
            go.AddComponent(new MouseDetector());
            var btn = new Button();
            btn.OnClick += (sender, e) =>
            {
                var go2 = new GameObject(GameObject.World);
                go2.AddComponent(new Transform(Vector2.Zero));
                //go2.AddComponent(new SpriteRender("Sprites/GO", 0));
                go2.AddComponent(new TextRenderer(soldier.Name, Color.Black, 1f));
                var dnd = new DragAndDropAlt(new Vector2(20, 20));
                dnd.Released += (dropSender, dropE) =>
                {
                    foreach (var obj in GameObject.World.Objects)
                    {
                        SpriteRender md = obj.GetComponent<SpriteRender>();
                        SoldierSlot slot = obj.GetComponent<SoldierSlot>();
                        Transform trans = obj.GetComponent<Transform>();
                        if (md != null && slot != null && trans != null)
                        {
                            var rect = new Rectangle(md.Rectangle.Location, md.Rectangle.Size);
                            rect.Offset(trans.Position);
                            if (rect.Contains(dropE.DropPosition))
                            {
                                slot.Content = soldier;
                            }
                        }
                    }
                };
                go2.AddComponent(dnd);
                GameObject.World.AddObject(go2);
            };
            go.AddComponent(btn);
            return go;
        }

        private static int[] IntToByteArray(int value)
        {
            Stack<int> stack = new Stack<int>();

            for (; value > 0; value /= 1000)
            {
                stack.Push(value % 1000);
            }

            return stack.ToArray();
        }
    }
}
