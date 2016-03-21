using System;
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
        private List<GameObject> objectsToAdd = new List<GameObject>();

        private float deltaTime;

        public static float WidthMulti { get { return widthMulti; } }
        public static float HeightMulti { get { return heightMulti; } }

        private GameState state;
        public GameState State { get { return state; } }

        public GameWorld()
        {
            //Initialize game and create GameState object
            new GameInitializer(this, new Random()).Start();
            state = new GameState();

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        public void AddObject(GameObject go)
        {
            if (!objectsToAdd.Contains(go))
                objectsToAdd.Add(go);
        }

        public void RemoveObject(GameObject go)
        {
            if (!objectsToRemove.Contains(go))
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

            //CreateGo(Vector2.Zero);
            //CreateGo(new Vector2(100, 400));

            var go = new GameObject(this);
            go.AddComponent(new Transform(Vector2.Zero));
            go.AddComponent(new SpriteRender("Sprites/GO", 0));
            var dnd = new DragAndDropAlt(new Vector2(20, 20));
            //dnd.Released += (sender, e) => { Exit(); };
            go.AddComponent(dnd);
            objectsToAdd.Add(go);

            var uiGo = new GameObject(this);
            uiGo.AddComponent(new UI());
            objectsToAdd.Add(uiGo);

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

            // TODO: use this.Content to load your game content here
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

            var tempAdd = new List<GameObject>(objectsToAdd);
            objectsToAdd.Clear();

            foreach (var go in tempAdd)
            {
                objects.Add(go);
                go.LoadContent(Content);
            }

            var tempRemove = new List<GameObject>(objectsToRemove);
            objectsToRemove.Clear();

            foreach (var go in tempRemove)
            {
                objects.Remove(go);
            }

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

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
