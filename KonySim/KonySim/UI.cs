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

        public UI()
        {
            permanentObjects = new List<GameObject>();
            permanentObjects.Add(CreateIcon("Sprites/btnBack", new Vector2(0, 0)));
            permanentObjects.Add(CreateIcon("Sprites/iconCrack", new Vector2(200, 20)));
            permanentObjects.Add(CreateIcon("Sprites/iconSyringe", new Vector2(500, 20)));
            permanentObjects.Add(CreateIcon("Sprites/iconWarrior", new Vector2(800, 20)));

        }

        public void LoadContent(ContentManager content)
        {
            foreach (GameObject go in permanentObjects)
            {
                go.LoadContent(content);
            }

            iconFont = content.Load<SpriteFont>("Fonts/IconFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in permanentObjects)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.DrawString(iconFont, "11", new Vector2(320, 20), Color.White);
            spriteBatch.DrawString(iconFont, "11", new Vector2(620, 20), Color.White);
            spriteBatch.DrawString(iconFont, "11", new Vector2(920, 20), Color.White);
        }

        public void Update(float deltaTime)
        {
            foreach (GameObject go in permanentObjects)
            {
                go.Update(deltaTime);
            }
        }

        private GameObject CreateIcon(string sprite, Vector2 position)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(go, position));
            go.AddComponent(new SpriteRender(go, sprite, 0));
            return go;
        }
    }
}
