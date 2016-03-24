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
        private List<GameObject> currentObjects;

        public MainWindowManager()
        {
            frameList = new List<GameObject>();
            currentObjects = new List<GameObject>();

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
            var go = new GameObject();
            go.AddComponent(new MissionScreen(mission));
            GameWorld.Instance.AddObject(go);

            currentObjects.Add(go);
        }

        public void GotoShop(Db.WeaponShop weaponShop)
        {

        }

        public void GotoDrugstore()
        {

        }

        public void GotoWorldmap(List<Db.Mission> missions)
        {
            GameWorld.Instance.CreateGo(Vector2.Zero);
            GameWorld.Instance.CreateGo(new Vector2(100, 400));
            var map = new GameObject();
            map.AddComponent(new Transform(new Vector2(0, 90)));
            map.AddComponent(new SpriteRender("Sprites/map", 0.1f));
            GameWorld.Instance.AddObject(map);

            var mis2 = new GameObject();
            mis2.AddComponent(new Transform(new Vector2(630, 200)));
            mis2.AddComponent(new SpriteRender("Sprites/huse", 1));
            mis2.AddComponent(new MouseDetector());
            mis2.AddComponent(new WorldMap(mis2));
            mis2.AddComponent(new MissionComp(5, 5, 5, 5, 5, 5));
            GameWorld.Instance.AddObject(mis2);

            var mis1 = new GameObject();
            mis1.AddComponent(new Transform(new Vector2(610, 400)));
            mis1.AddComponent(new SpriteRender("Sprites/huse", 1));
            mis1.AddComponent(new MouseDetector());
            mis1.AddComponent(new WorldMap(mis1));
            mis1.AddComponent(new MissionComp(5, 5, 5, 5, 5, 5));
            GameWorld.Instance.AddObject(mis1);
            var Weapon = new GameObject();
            Weapon.AddComponent(new Transform(new Vector2(150, 500)));
            Weapon.AddComponent(new SpriteRender("sprites/weaponshop", 1));
            GameWorld.Instance.AddObject(Weapon);

            //currentObjects is used to remove objects again.
            currentObjects.Add(map);
            currentObjects.Add(mis1);
            currentObjects.Add(mis2);
            currentObjects.Add(Weapon);
        }
    }
}
