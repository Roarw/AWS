﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KonySim
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    internal class GameWorld : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private static float widthMulti;
        private static float heightMulti;

        public static int MouseX { get { return Mouse.GetState().Position.X / (int)WidthMulti; } }
        public static int MouseY { get { return Mouse.GetState().Position.Y / (int)HeightMulti; } }

        private List<GameObject> objects = new List<GameObject>();
        private List<GameObject> objectsToRemove = new List<GameObject>();
        private UI ui;
        private float deltaTime;
<<<<<<< HEAD
    
=======
>>>>>>> f91dbba6503420906ac27e0855536635b569c69a

        public static float WidthMulti { get { return widthMulti; } }
        public static float HeightMulti { get { return heightMulti; } }

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public void RemoveObject(GameObject go)
        {
            objectsToRemove.Add(go);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            
            // TODO: Add your initialization logic here
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Window.Position = new Point(-10, 0);
            graphics.ApplyChanges();

            widthMulti = (float)Window.ClientBounds.Width / (float)graphics.PreferredBackBufferWidth;
            heightMulti = (float)Window.ClientBounds.Height / (float)graphics.PreferredBackBufferHeight;

            this.IsMouseVisible = true;

            CreateGo(Vector2.Zero);
            CreateGo(new Vector2(100, 400));
            var test = new GameObject(this);
            test.AddComponent(new Transform(new Vector2(100, 100)));
            test.AddComponent(new SpriteRender("sprites/iconWarrior", 0));
            test.AddComponent(new MouseDetector());
            test.AddComponent(new WorldMap(test));
            test.AddComponent(new MissionComp(5,5,5,5,5,5));
            objects.Add(test);
            
            

            var go = new GameObject(this);
            go.AddComponent(new Transform(Vector2.Zero));
            go.AddComponent(new SpriteRender("Sprites/GO", 0));
            var dnd = new DragAndDropAlt(new Vector2(20, 20));
            //dnd.Released += (sender, e) => { Exit(); };
            go.AddComponent(dnd);
            objects.Add(go);
            
            ui = new UI();
<<<<<<< HEAD
           
=======
>>>>>>> f91dbba6503420906ac27e0855536635b569c69a

            base.Initialize();
        }

        private void CreateGo(Vector2 position)
        {
            GameObject go = new GameObject(this);
            go.AddComponent(new Transform(position));
            go.AddComponent(new SpriteRender("Sprites/GO.png", 0));
            go.AddComponent(new MouseDetector());
            go.AddComponent(new DragAndDrop());
            objects.Add(go);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
<<<<<<< HEAD
            
=======

>>>>>>> f91dbba6503420906ac27e0855536635b569c69a
            // TODO: use this.Content to load your game content here
            foreach (GameObject go in objects)
            {
                go.LoadContent(Content);
            }

            ui.LoadContent(Content);

            new GameInitializer(this, new Random()).Start();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (GameObject go in objects)
            {
                go.Update(deltaTime);
            }

            foreach (var go in objectsToRemove)
            {
                objects.Remove(go);
            }
            objectsToRemove.Clear();

            ui.Update(deltaTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Peru);
           
            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, null);

            foreach (GameObject go in objects)
            {
                go.Draw(spriteBatch);
            }
            ui.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
