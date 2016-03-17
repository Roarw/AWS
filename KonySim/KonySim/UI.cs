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
        ContentManager content;

        List<GameObject> uiObjects;
        SpriteFont iconFont;
        UIList childrenList;

        //Initializing stuff.
        public UI()
        {
            uiObjects = new List<GameObject>();

            uiObjects.Add(CreateImage("Sprites/btnBack", new Vector2(0, 0)));
            uiObjects.Add(CreateImage("Sprites/iconSyringe", new Vector2(200, 20)));
            uiObjects.Add(CreateImage("Sprites/iconWarrior", new Vector2(500, 20)));
            uiObjects.Add(CreateImage("Sprites/iconCrack", new Vector2(800, 20)));
        }
        
        public void LoadContent(ContentManager content)
        {
            //Setting ContentManager.
            this.content = content;

            foreach (GameObject go in uiObjects)
            {
                go.LoadContent(content);
            }

            iconFont = content.Load<SpriteFont>("Fonts/IconFont");

            //Children list.
            childrenList = new UIList(new Vector2(1300, 0), 800);
            childrenList.LoadContent(content);

            childrenList.AddItem(CreateImage("Sprites/ChildSub", new Vector2(0, 0)), content);
            childrenList.AddItem(CreateImage("Sprites/ChildSub", new Vector2(0, 0)), content);
            childrenList.AddItem(CreateImage("Sprites/ChildSub", new Vector2(0, 0)), content);
            childrenList.AddItem(CreateImage("Sprites/iconSyringe", new Vector2(0, 0)), content);
            childrenList.AddItem(CreateImage("Sprites/ChildSub", new Vector2(0, 0)), content);
        }
        
        public void Update(float deltaTime)
        {
            foreach (GameObject go in uiObjects)
            {
                go.Update(deltaTime);
            }

            childrenList.Update(deltaTime);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in uiObjects)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.DrawString(iconFont, "100", new Vector2(320, 20), Color.White);
            spriteBatch.DrawString(iconFont, "100", new Vector2(620, 20), Color.White);
            spriteBatch.DrawString(iconFont, "10000", new Vector2(920, 20), Color.White);

            childrenList.Draw(spriteBatch);
        }
        
        //Creating an image.
        private GameObject CreateImage(string sprite, Vector2 position)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(go, position));
            go.AddComponent(new SpriteRender(go, sprite, 0));
            return go;
        }
    }
}
