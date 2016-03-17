using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KonySim
{
    class UI: ILoadContent, IUpdate, IDraw
    {
        List<GameObject> permanentObjects;
        SpriteFont iconFont;

        List<GameObject> childrenList;
        List<Transform> childrensTransform;
        float scrollerPosition;

        //Initializing stuff.
        public UI()
        {
            permanentObjects = new List<GameObject>();

            permanentObjects.Add(CreateIcon("Sprites/btnBack", new Vector2(0, 0)));
            permanentObjects.Add(CreateIcon("Sprites/iconSyringe", new Vector2(200, 20)));
            permanentObjects.Add(CreateIcon("Sprites/iconWarrior", new Vector2(500, 20)));
            permanentObjects.Add(CreateIcon("Sprites/iconCrack", new Vector2(800, 20)));

            childrenList = new List<GameObject>();
            childrensTransform = new List<Transform>();

            childrenList.Add(CreateIcon("Sprites/ChildSub", new Vector2(0, 0)));
            childrenList.Add(CreateIcon("Sprites/ChildSub", new Vector2(0, 0)));
            childrenList.Add(CreateIcon("Sprites/ChildSub", new Vector2(0, 0)));
            childrenList.Add(CreateIcon("Sprites/ChildSub", new Vector2(0, 0)));

            permanentObjects.Add(CreateIcon("Sprites/goUp", new Vector2(1305, 0)));
            permanentObjects.Add(CreateIcon("Sprites/goDown", new Vector2(1305, 950)));
        }

        //Loading content.
        public void LoadContent(ContentManager content)
        {
            foreach (GameObject go in permanentObjects)
            {
                go.LoadContent(content);
            }

            iconFont = content.Load<SpriteFont>("Fonts/IconFont");

            foreach (GameObject go in childrenList)
            {
                go.LoadContent(content);
                childrensTransform.Add((Transform)go.GetComponent("Transform"));
            }
        }

        //Updating.
        public void Update(float deltaTime)
        {
            foreach (GameObject go in permanentObjects)
            {
                go.Update(deltaTime);
            }

            for (int i = 0; i < childrenList.Count; i++)
            {
                childrensTransform[i].Position = new Vector2(1305, i * 200 + 60 + scrollerPosition);
            }
        }

        //Drawing.
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in permanentObjects)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.DrawString(iconFont, "100", new Vector2(320, 20), Color.White);
            spriteBatch.DrawString(iconFont, "100", new Vector2(620, 20), Color.White);
            spriteBatch.DrawString(iconFont, "10000", new Vector2(920, 20), Color.White);

            foreach (GameObject go in childrenList)
            {
                go.Draw(spriteBatch);
            }
        }
        
        //Creating a simple icon.
        private GameObject CreateIcon(string sprite, Vector2 position)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(go, position));
            go.AddComponent(new SpriteRender(go, sprite, 0));
            return go;
        }
    }
}
