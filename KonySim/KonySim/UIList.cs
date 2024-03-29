﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace KonySim
{
    /// <summary>
    /// Idea: Make a dictionary with a bool, that checks if an object is bound or not.
    /// </summary>

    internal class UIList : Component, ILoadContent, IUpdate, IDraw
    {
        private List<GameObject> frameList;
        private Dictionary<GameObject, List<Component>> itemInformation;

        private Vector2 position;
        private int height;
        private float depth;
        private int elementHeight;
        private int maxHeight;
        private float scrollerPosition;

        private Rectangle listArea;

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle((int)position.X + 5, (int)position.Y + 50,
                    300/*300 is the width of a ChildCard.*/ - 5, height - 50);
            }
        }

        //Initializing stuff.
        public UIList(Vector2 position, int height, float depth)
        {
            frameList = new List<GameObject>();
            itemInformation = new Dictionary<GameObject, List<Component>>();

            this.position = position;
            this.height = height;
            this.depth = depth;
            this.elementHeight = 0;
            this.scrollerPosition = 0;

            listArea = new Rectangle((int)position.X + 5, (int)position.Y + 50, 290, height - 100);

            //Creating the frame and buttons for the list.
            frameList.Add(UIBuilders.CreateImageWOffset("Sprites/listFrame", new Vector2(position.X, position.Y), depth, 0, height - 5));
            frameList.Add(UIBuilders.CreateImageWOffset("Sprites/listFrame", new Vector2(position.X + 295, position.Y), depth, 0, height - 5));
            frameList.Add(UIBuilders.CreateImageWOffset("Sprites/listBackground", new Vector2(position.X + 5, position.Y + 50), depth, 0, height - 105));

            frameList.Add(CreateScroller("Sprites/goUp", new Vector2(position.X + 5, position.Y), 5));
            frameList.Add(CreateScroller("Sprites/goDown", new Vector2(position.X + 5, position.Y + height - 50), -5));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (GameObject go in frameList)
            {
                go.LoadContent(content);
            }

            foreach (var g in itemInformation.Keys)
            {
                g.LoadContent(content);
            }
        }

        public void Update(float deltaTime)
        {
            foreach (GameObject go in frameList)
            {
                go.Update(deltaTime);
            }

            foreach (var g in itemInformation.Keys)
            {
                g.Update(deltaTime);
            }

            //Placing objects accordingly in the UIList.
            for (int i = 0; i < itemInformation.Count; i++)
            {
                (itemInformation[itemInformation.Keys.ElementAt(i)][0] as Transform).Position = new Vector2(position.X + 5,
                elementHeight + (position.Y + 55) + scrollerPosition);

                elementHeight += ((itemInformation[itemInformation.Keys.ElementAt(i)][1] as SpriteRender).Rectangle.Height + 10);
            }
            maxHeight = elementHeight;
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

        //Adding an item to the UIList.
        public void UpdateItems(List<GameObject> items)
        {
            itemInformation.Clear();

            foreach(GameObject go in items)
            {
                if (go.GetComponent<Transform>() == null || go.GetComponent<SpriteRender>() == null)
                {
                    System.Diagnostics.Debug.WriteLine("Failed to add item to UIList, because Transform or SpriteRender is null.");
                }
                else
                {
                    go.LoadContent(GameWorld.Instance.Content);
                    itemInformation.Add(go, new List<Component>()
                {
                    go.GetComponent<Transform>(), go.GetComponent<SpriteRender>()
                });
                }
            }
        }

        //TouchingUIList and InsideUIList, checks if something is within the bounds of the UIList.
        private bool TouchingUIList(GameObject gameObject)
        {
            float tby = (itemInformation[gameObject][0] as Transform).Position.Y + (itemInformation[gameObject][1] as SpriteRender).Rectangle.Height;
            float tty = (itemInformation[gameObject][0] as Transform).Position.Y;

            return (tby < listArea.Y + listArea.Height && tby > listArea.Y ||
                    tty < listArea.Y + listArea.Height && tty > listArea.Y);
        }

        private bool InsideUIList(GameObject gameObject)
        {
            float tby = (itemInformation[gameObject][0] as Transform).Position.Y + (itemInformation[gameObject][1] as SpriteRender).Rectangle.Height;
            float tty = (itemInformation[gameObject][0] as Transform).Position.Y;

            return (tby < listArea.Y + listArea.Height && tty > listArea.Y);
        }

        //Scrolling up and down the list.
        public void SetScrollerPosition(float value)
        {
            if (value < 0 && scrollerPosition > -(maxHeight - height + 100))
            {
                scrollerPosition += value;

                if (scrollerPosition <= -(maxHeight - height + 100))
                {
                    scrollerPosition = -(maxHeight - height + 100);
                }
            }
            else if (value > 0 && scrollerPosition < 0)
            {
                scrollerPosition += value;

                if (scrollerPosition >= 0)
                {
                    scrollerPosition = 0;
                }
            }
        }

        //Creating a scroller button.
        private GameObject CreateScroller(string sprite, Vector2 position, int factor)
        {
            GameObject go = new GameObject();
            go.AddComponent(new Transform(position));
            go.AddComponent(new SpriteRender(sprite, depth + 0.5f));
            go.AddComponent(new MouseDetector());
            go.AddComponent(new ListScroller(this, factor));
            return go;
        }
    }
}
