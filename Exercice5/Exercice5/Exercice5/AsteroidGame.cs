using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Exercice5
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class AsteroidGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Scene scene;
        BoundingBox screenBox = new BoundingBox();
        GamePadState oldGamePadState;

        public AsteroidGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            InitGraphicsMode(1024, 768, false);
            base.Initialize();
        }

        private bool InitGraphicsMode(int width, int height, bool fullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (fullScreen == false)
            {
                if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    //if ((dm.Width == width) && (dm.Height == height))
                    //{
                    // The mode is supported, so set the buffer formats, apply changes and return
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    return true;
                    //}
                }
            }
            return false;
        }


        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            scene = new Scene();

            AsteroidFactory.SetContent(Content);
            EnemyFactory.SetContent(Content);

            //Initialize Screen Border collisions
            screenBox.Min.X = 0;
            screenBox.Min.Y = 0;
            screenBox.Max.X = graphics.GraphicsDevice.Viewport.Width;
            screenBox.Max.Y = graphics.GraphicsDevice.Viewport.Height;

            // Player
            Player.GetInstance().Initialize(new Sprite(Content.Load<Texture2D>("Graphics\\ship"), 0.3f), new Vector2(500, 350), new Sprite(Content.Load<Texture2D>("Graphics\\ship"), 0.05f));

            // Asteroid
            scene.AddDrawableObject(AsteroidFactory.createNewAsteroid(1, new Vector2(150,150)));

            //Enemies
            scene.AddDrawableObject(EnemyFactory.createEnemy(1, new Vector2(0, 0)));

            // Bonus
            Bonus shrinkBonus = new Bonus(Bonus.Type.BIGGER_BULLETS);
            shrinkBonus.Initialize(new Sprite(Content.Load<Texture2D>("Graphics\\ship"), 0.3f), new Vector2(700, 500));
            shrinkBonus.AddObserver(Player.GetInstance());


            // Add all previous objects to scene.
            scene.AddDrawableObject(Player.GetInstance());
            scene.AddDrawableObject(shrinkBonus);
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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

            HandleInput();

            scene.Update(screenBox);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(Content.Load<Texture2D>("Graphics\\background"), Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            scene.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void HandleInput()
        {
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
            {
                HandleGamePadInput();
            }
            else
            {
                HandleKeyboardInput();
            }
        }

        private void HandleKeyboardInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape))
                this.Exit();

            if (keyboardState.IsKeyDown(Keys.W))
            {
                Player.GetInstance().AddVelocity(0.3f);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                Player.GetInstance().AddVelocity(-0.3f);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                Player.GetInstance().Rotate(0.1f);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                Player.GetInstance().Rotate(-0.1f);
            }
            if (keyboardState.IsKeyDown(Keys.Space))
            {
                Bullet bullet = Player.GetInstance().Shoot();
                if (bullet != null)
                    scene.AddDrawableObject(bullet);
            }
        }

        private void HandleGamePadInput()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            GamePadState padOneState = GamePad.GetState(PlayerIndex.One);
            Player.GetInstance().AddVelocity(padOneState.ThumbSticks.Left.Y);
            Player.GetInstance().Rotate(padOneState.ThumbSticks.Left.X / 10);

            if (padOneState.IsButtonDown(Buttons.A) && oldGamePadState.IsButtonUp(Buttons.A))
            {
                scene.AddDrawableObject(Player.GetInstance().Shoot());
            }
            oldGamePadState = padOneState;
        }
    }
}
