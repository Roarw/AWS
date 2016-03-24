using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KonySim
{
    internal class MainMenu : IUpdate, ILoadContent, IDraw
    {
        private enum GameState
        { mainMenu, enterName, inGame }

        private GameState gameState;

        private List<GUIElement> main = new List<GUIElement>();
        private List<GUIElement> enterName = new List<GUIElement>();

        private Keys[] lastPressedKeys = new Keys[5];

        private string myName = string.Empty;

        private SpriteFont sf;

        public MainMenu()
        {
            enterName.Add(new GUIElement("Sprites/done", 0.9f));
            enterName.Add(new GUIElement("Sprites/name", 0.5f));

            main.Add(new GUIElement("Sprites/nameBtn", 0.5f));
            main.Add(new GUIElement("Sprites/menu", 0.2f));
            main.Add(new GUIElement("Sprites/play", 0.5f));
        }

        public void LoadContent(ContentManager content)
        {
            sf = content.Load<SpriteFont>("Fonts/iconFont");

            foreach (GUIElement element in main)
            {
                element.LoadContent(content);
                element.CenterElement(1280, 720);
                element.clickEvent += OnClick;
            }
            main.Find(x => x.SpriteName == "Sprites/play").MoveElement(0, -100);

            foreach (GUIElement element in enterName)
            {
                element.LoadContent(content);
                element.CenterElement(1280, 720);
                element.clickEvent += OnClick;
            }
            enterName.Find(x => x.SpriteName == "Sprites/done").MoveElement(0, 50);
        }

        public void Update(float deltaTime)
        {
            switch (gameState)
            {
                case GameState.mainMenu:
                    foreach (GUIElement element in main)
                    {
                        element.Update(deltaTime);
                    }
                    break;
                case GameState.enterName:
                    foreach (GUIElement element in enterName)
                    {
                        element.Update(deltaTime);
                    }
                    GetKeys();
                    break;
                case GameState.inGame:
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (gameState)
            {
                case GameState.mainMenu:
                    foreach (GUIElement element in main)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                case GameState.enterName:
                    foreach (GUIElement element in enterName)
                    {
                        element.Draw(spriteBatch);
                    }
                    spriteBatch.DrawString(sf, myName, new Vector2(305, 300), Color.Black);
                    break;
                case GameState.inGame:
                    break;
                default:
                    break;
            }
        }

        public void OnClick(string element)
        {
            if (element == "Sprites/play")
            {
                //Play the game
                gameState = GameState.inGame;
            }

            if (element == "Sprites/nameBtn")
            {
                gameState = GameState.enterName;
            }

            if (element == "Sprites/done")
            {
                gameState = GameState.mainMenu;
            }
        }

        public void GetKeys()
        {
            KeyboardState kbState = Keyboard.GetState();

            Keys[] pressedKeys = kbState.GetPressedKeys();

            foreach (Keys key in lastPressedKeys)
            {
                if (!pressedKeys.Contains(key))
                {
                    //key is no longer pressed
                    OnKeyUp(key);
                }
            }

            foreach (Keys key in pressedKeys)
            {
                if (!lastPressedKeys.Contains(key))
                {
                    OnKeyDown(key);
                }
            }
            lastPressedKeys = pressedKeys;
        }

        public void OnKeyUp(Keys key)
        {
        }

        public void OnKeyDown(Keys key)
        {
            if (key == Keys.Back && myName.Length > 0)
            {
                myName = myName.Remove(myName.Length - 1);
            }
            else
            {
                myName += key.ToString();
            }
        }
    }
}
