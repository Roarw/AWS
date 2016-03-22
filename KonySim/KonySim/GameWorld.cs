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

        MainMenu main;

        private float widthMulti;
        private float heightMulti;

        public int MouseX { get { return Mouse.GetState().Position.X / (int)WidthMulti; } }
        public int MouseY { get { return Mouse.GetState().Position.Y / (int)HeightMulti; } }

        private List<GameObject> objects = new List<GameObject>();
        private List<GameObject> objectsToRemove = new List<GameObject>();
        private List<GameObject> objectsToAdd = new List<GameObject>();

        private float deltaTime;


        public float WidthMulti { get { return widthMulti; } }
        public float HeightMulti { get { return heightMulti; } }

        private GameState state;
        public GameState State { get { return state; } }

        private static GameWorld instance;
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
        }


        private GameWorld()
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
            //Creating the generator.
            main = new MainMenu();
            //Setting graphics.
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Window.Position = new Point(-10, 0);
            graphics.ApplyChanges();

            widthMulti = (float)Window.ClientBounds.Width / (float)graphics.PreferredBackBufferWidth;
            heightMulti = (float)Window.ClientBounds.Height / (float)graphics.PreferredBackBufferHeight;

            this.IsMouseVisible = true;


            CreateGo(Vector2.Zero);
            CreateGo(new Vector2(100, 400));
            var mis2 = new GameObject(this);
            mis2.AddComponent(new Transform(new Vector2(100, 100)));
            mis2.AddComponent(new SpriteRender("Sprites/huse", 0));
            mis2.AddComponent(new MouseDetector());
            mis2.AddComponent(new WorldMap(mis2));
            mis2.AddComponent(new MissionComp(5,5,5,5,5,5));
            objects.Add(mis2);

            var mis1 = new GameObject(this);
            mis1.AddComponent(new Transform(new Vector2(300, 200)));
            mis1.AddComponent(new SpriteRender("Sprites/huse", 0));
            mis1.AddComponent(new MouseDetector());
            mis1.AddComponent(new WorldMap(mis1));
            mis1.AddComponent(new MissionComp(5, 5, 5, 5, 5, 5));
            objects.Add(mis1);
            
            


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

            /*var fufugo = new GameObject(this);
            fufugo.AddComponent(new SpriteRender("Sprites/GO", 0));
            fufugo.AddComponent(new MouseDetector());
            var but = new Button();
            but.OnClick += (sender, e) => { RemoveObject(fufugo); };
            fufugo.AddComponent(but);
            fufugo.AddComponent(new Transform(new Vector2(50, 100)));
            AddObject(fufugo);*/

            var mission = new GameObject(this);
            mission.AddComponent(new MissionScreen(new Db.Mission { AnimalCount = 5, ChildCount = 10, CivilianCount = 20, DefenseMultiplier = 1, XpReward = 21312, FundsReward = 555555 }));
            AddObject(mission);



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

            main.LoadContent(Content);
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

            main.Update(deltaTime);
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

            main.Draw(spriteBatch);

            foreach (GameObject go in objects)
            {
                go.Draw(spriteBatch);
            }




            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
