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

        public UI()
        {
            permanentObjects = new List<GameObject>();
            permanentObjects.Add(CreateIcon("Sprites/iconCrack.png", new Vector2(0, 0)));
            permanentObjects.Add(CreateIcon("Sprites/iconSyringe.png", new Vector2(200, 0)));
            permanentObjects.Add(CreateIcon("Sprites/iconWarrior.png", new Vector2(400, 0)));

        }

        public void LoadContent(ContentManager content)
        {
            foreach (GameObject go in permanentObjects)
            {
                go.LoadContent(content);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in permanentObjects)
            {
                go.Draw(spriteBatch);
            }
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
