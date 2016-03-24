using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KonySim
{
    class MainWindowManager : Component, ILoadContent, IUpdate, IDraw
    {
        private List<GameObject> frameList;

        public MainWindowManager()
        {
            frameList = new List<GameObject>();

            frameList.Add(UIBuilders.CreateImageWOffset("Sprites/listFrame", new Vector2(0, 80), 0, 0, 635, 0, 0));
            frameList.Add(UIBuilders.CreateImageWOffset("Sprites/listFrame", new Vector2(5, 80), 0, 0, 10, 0, 970));
            frameList.Add(UIBuilders.CreateImageWOffset("Sprites/listFrame", new Vector2(5, 705), 0, 0, 10, 0, 970));
            frameList.Add(UIBuilders.CreateImageWOffset("Sprites/BackGround", new Vector2(5, 95), 0, 0, 0, 0, 0));
            frameList.Add(UIBuilders.CreateImageWOffset("Sprites/BackButton", new Vector2(5, 95), 1f, 0, 0, 0, 0));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (GameObject go in frameList)
            {
                go.LoadContent(content);
            }
        }

        public void Update(float deltaTime)
        {
            foreach (GameObject go in frameList)
            {
                go.Update(deltaTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in frameList)
            {
                go.Draw(spriteBatch);
            }
        }

        public void GotoMission(Db.Mission mission)
        {

        }

        public void GotoShop(Db.WeaponShop weaponShop)
        {

        }

        public void GotoDrugstore()
        {

        }

        public void GotoWorldmap(List<Db.Mission> missions)
        {

        }
    }
}
