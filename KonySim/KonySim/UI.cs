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

            childrenList.AddItem(ChildCard("Sprites/ChildSub", new Vector2(0, 0), bounds), content);
            childrenList.AddItem(ChildCard("Sprites/ChildSub", new Vector2(0, 0), bounds), content);
            childrenList.AddItem(ChildCard("Sprites/ChildSub", new Vector2(0, 0), bounds), content);
            childrenList.AddItem(ChildCard("Sprites/ChildSub", new Vector2(0, 0), bounds), content);
            childrenList.AddItem(ChildCard("Sprites/ChildSub", new Vector2(0, 0), bounds), content);
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
        private GameObject ChildCard(string sprite, Vector2 position, Rectangle rect)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(position));
            go.AddComponent(new SpriteRender(sprite, 0.1f, rect));
            return go;
        }
    }
}
