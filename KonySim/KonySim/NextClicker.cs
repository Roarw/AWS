using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KonySim
{
    class NextClicker : ILoadContent, IUpdate, IDraw
    {
        private List<GameObject> frameList;
        private List<GameObject> items;

        private Vector2 position;
        private Vector2 itemPosition;
        private int width;
        private int currentItem;

        public NextClicker(Vector2 position, int width)
        {
            frameList = new List<GameObject>();
            items = new List<GameObject>();

            this.position = position;
            this.width = width;

            currentItem = 0;
            itemPosition = new Vector2(position.X + 50, position.Y);

            frameList.Add(CreateClicker("Sprites/leftArrow", new Vector2(position.X, position.Y), false));
            frameList.Add(CreateClicker("Sprites/rightArrow", new Vector2(position.X + width - 35, position.Y), true));

            items.Add(CreateWeapon("Sprites/WeaponAK47", new Vector2(320, 595)));
            items.Add(CreateWeapon("Sprites/WeaponBizon", new Vector2(320, 595)));
            items.Add(CreateWeapon("Sprites/WeaponDP12", new Vector2(320, 595)));
            items.Add(CreateWeapon("Sprites/WeaponMAC10", new Vector2(320, 595)));
            items.Add(CreateWeapon("Sprites/WeaponRPG7", new Vector2(320, 595)));


        }

        public void LoadContent(ContentManager content)
        {
            foreach (GameObject go in frameList)
            {
                go.LoadContent(content);
            }

            foreach (GameObject go in items)
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

            if (items.Count > 0)
            {
                items[currentItem].Update(deltaTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in frameList)
            {
                go.Draw(spriteBatch);
            }

            if (items.Count > 0)
            {
                items[currentItem].Draw(spriteBatch);
            }
        }

        public void SetCurrentItem(int add)
        {
            if (items.Count > 0 && currentItem + add >= 0 && currentItem + add < items.Count)
            {
                items[currentItem + add].GetComponent<Transform>().Position = itemPosition;
                currentItem += add;
            }
        }

        private GameObject CreateClicker(string sprite, Vector2 position, bool rightButton)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(position));
            go.AddComponent(new SpriteRender(sprite, 0.5f));
            go.AddComponent(new MouseDetector());
            go.AddComponent(new NextButton(this, rightButton));
            return go;
        }

        private GameObject CreateWeapon(string sprite, Vector2 position)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(position));
            go.AddComponent(new SpriteRender(sprite, 1f));
            return go;
        }

        private GameObject Weapon(Db.Weapon wpn)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(position));
            go.AddComponent(new SpriteRender("Sprites/Weapon" + wpn.ID, 0.5f));
            go.AddComponent(new TextRenderer(wpn.Name, Color.White, 1f));
            go.AddComponent(new TextRenderer(wpn.Damage.ToString(), Color.White, 1f));
            return go;
        }

    }
}
