using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    class UIList: ILoadContent, IUpdate, IDraw
    {
        List<GameObject> frameList;
        
        Dictionary<GameObject, List<Component>> itemInformation;
        
        Vector2 position;
        int height;
        int elementHeight;
        float scrollerPosition;

        private Rectangle listArea;

        //Initializing stuff.
        public UIList(Vector2 position, int height)
        {
            frameList = new List<GameObject>();
            itemInformation = new Dictionary<GameObject, List<Component>>();

            this.position = position;
            this.height = height;
            this.elementHeight = 0;
            this.scrollerPosition = 0;

            listArea = new Rectangle((int)position.X + 5, (int)position.Y + 50, 290, height - 100);

            //Creating the frame and buttons for the list.
            frameList.Add(CreateImageOffset("Sprites/listFrame", new Vector2(position.X, position.Y), 0, height - 5));
            frameList.Add(CreateImageOffset("Sprites/listFrame", new Vector2(position.X + 295, position.Y), 0, height - 5));
            frameList.Add(CreateImageOffset("Sprites/listBackground", new Vector2(position.X + 5, position.Y + 50), 0, height - 105));

            frameList.Add(CreateImageOffset("Sprites/goUp", new Vector2(position.X + 5, position.Y), 0, 0));
            frameList.Add(CreateImageOffset("Sprites/goDown", new Vector2(position.X + 5, height - 50), 0, 0));
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

            //Placing objects accordingly in the UIList.
            for (int i = 0; i < itemInformation.Count; i++)
            {
                (itemInformation[itemInformation.Keys.ElementAt(i)][0] as Transform).Position = new Vector2(position.X + 5,
                elementHeight + (position.Y + 60) + scrollerPosition);

                elementHeight += ((itemInformation[itemInformation.Keys.ElementAt(i)][1] as SpriteRender).Rectangle.Height + 10);
            }
            elementHeight = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject go in frameList)
            {
                go.Draw(spriteBatch);
            }
            foreach (var item in itemInformation.Keys)
            {
                if (TouchingUIList(item))
                {
                    item.Draw(spriteBatch);
                }
            }
        }

        //Creating an image with offset.
        private GameObject CreateImageOffset(string sprite, Vector2 position, int yTopOffset, int yBottomOffset)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(go, position));
            SpriteRender spr = new SpriteRender(go, sprite, 0);
            spr.SetYOffset(yTopOffset, yBottomOffset);
            go.AddComponent(spr);
            return go;
        }

        //Adding an item to the UIList.
        public void AddItem(GameObject gameObject, ContentManager content)
        {
            if (gameObject.GetComponent("Transform") == null || gameObject.GetComponent("SpriteRender") == null)
            {
                System.Diagnostics.Debug.WriteLine("Failed to add item to UIList, because Transform or SpriteRender is null.");
            }
            else
            {
                gameObject.LoadContent(content);
                itemInformation.Add(gameObject, new List<Component>()
                {
                    (Transform)gameObject.GetComponent("Transform"), (SpriteRender)gameObject.GetComponent("SpriteRender")
                });
            }
        }

        //TouchingUIList and InsideUIList, checks if something is within the bounds of the UIList.
        private bool TouchingUIList(GameObject gameObject)
        {
            return ((itemInformation[gameObject][0] as Transform).Position.Y + 
                    (itemInformation[gameObject][1] as SpriteRender).Rectangle.Height < listArea.Y + listArea.Height &&
                    (itemInformation[gameObject][0] as Transform).Position.Y +
                    (itemInformation[gameObject][1] as SpriteRender).Rectangle.Height > listArea.Y ||
                    (itemInformation[gameObject][0] as Transform).Position.Y < listArea.Y + listArea.Height &&
                    (itemInformation[gameObject][0] as Transform).Position.Y > listArea.Y);
        }

        private bool InsideUIList(GameObject gameObject)
        {
            return ((itemInformation[gameObject][0] as Transform).Position.Y +
                    (itemInformation[gameObject][1] as SpriteRender).Rectangle.Height < listArea.Y + listArea.Height &&
                    (itemInformation[gameObject][0] as Transform).Position.Y > listArea.Y);
        }
    }
}
