using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Exercice5
{
    /// <summary>
    /// Class that defines a state for the game.
    /// PlayState will display the game.
    /// </summary>
    public class PlayState : IGameState
    {
        protected int level;
        protected Scene scene;
        protected ContentManager content;
        protected InputHandler input;
        private TimeSpan objectSpawningDelay = new TimeSpan(0, 0, 15);
        private DateTime timeLastObjectSpawned = DateTime.Now;
        private bool exit = false;
        private bool paused = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayState"/> class.
        /// </summary>
        /// <param name="_level">The _level.</param>
        public PlayState(int _level)
        {
            level = _level;
        }

        /// <summary>
        /// Loads the content and Initialize the Player's instance.
        /// @see Initialize
        /// @see AddScoreObserver
        /// @see StoreBullet
        /// </summary>
        /// <param name="_content">The _content.</param>
        public void LoadContent(ContentManager _content)
        {
            content = _content;
            AsteroidFactory.SetContent(_content);
            EnemyFactory.SetContent(_content);
            input = AsteroidGame.input;

            // Create scene
            LevelLoader levelLoader = new LevelLoader(level);
            scene = levelLoader.GetScene();
            UIContainer uiContainer = new UIContainer();
            uiContainer.LoadContent(content);
            scene.Initialize(uiContainer, content);

            // Player
            Player.GetInstance().Initialize(content, new Sprite(content.Load<Texture2D>("Graphics\\ship"), 0.6f), new Vector2(500, 300));
            for (int i = 0; i < Player.MAX_NB_BULLETS; i++)
            {
                Bullet bullet = new Bullet();
                bullet.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\fork"), 0.4f), Player.GetInstance().Position);
                bullet.AddVelocity(5f);
                bullet.AddScoreObserver(Player.GetInstance());
                Player.GetInstance().StoreBullet(bullet);
            }
            scene.AddDrawableObject(Player.GetInstance());
        }

        /// <summary>
        /// Updates this instance.
        /// @see Initialize
        /// @see AddBonusObserver
        /// @see AddDrawableObject
        /// </summary>
        public void Update()
        {
            if (!paused)
            {
                scene.Update(AsteroidGame.screenBox);

                if (DateTime.Now - timeLastObjectSpawned >= objectSpawningDelay)
                {
                    timeLastObjectSpawned = DateTime.Now;
                    Bonus bonus = null;
                    Vector2 position = new Vector2(RandomGenerator.GetRandomFloat(0, AsteroidGame.screenBox.Max.X), RandomGenerator.GetRandomFloat(170, AsteroidGame.screenBox.Max.Y));
                    switch (RandomGenerator.GetRandomInt(0, 4))
                    {
                        case 0:
                            bonus = new Bonus(Bonus.Type.SHRINK);
                            bonus.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\hatChef"), 0.3f), position);
                            bonus.AddBonusObserver(Player.GetInstance());
                            break;
                        case 1:
                            bonus = new Bonus(Bonus.Type.BIGGER_BULLETS);
                            bonus.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\hatBiggerBullet"), 0.3f), position);
                            bonus.AddBonusObserver(Player.GetInstance());
                            break;
                        case 2:
                            bonus = new Bonus(Bonus.Type.STOP_TIME);
                            bonus.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\hat_time"), 0.3f), position);
                            foreach(Asteroid asteroid in scene.GetAllAsteroids())
                            {
                                bonus.AddBonusObserver(asteroid);
                            }
                            break;
                        case 3:
                            bonus = new Bonus(Bonus.Type.ASTEROID_EXPLODE);
                            bonus.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\hatExplosion"), 0.3f), position);
                            foreach (Asteroid asteroid in scene.GetAllAsteroids())
                            {
                                bonus.AddBonusObserver(asteroid);
                            }
                            break;
                        case 4:
                            bonus = new Bonus(Bonus.Type.SCORE_TWICE);
                            bonus.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\hat2X"), 0.3f), new Vector2(700, 500));
                            bonus.AddBonusObserver(Player.GetInstance());
                            break;
                    }
                    scene.AddDrawableObject(bonus);
                    scene.AddDrawableObject(EnemyFactory.createEnemy(RandomGenerator.GetRandomInt(1, 3)));
                }

                if (scene.onlyHasPlayer())
                {
                    AsteroidGame.gameState = new PlayState(level + 1);
                    AsteroidGame.gameState.LoadContent(content);
                }
            }
        }

        /// <summary>
        /// Handles the input.
        /// @see HandleGamePadInput
        /// @see HandleKeyboardInput
        /// </summary>
        public void HandleInput()
        {
            if (!paused)
            {
                if (input.IsGamePadOneConnected())
                {
                    HandleGamePadInput();
                }
                else
                {
                    HandleKeyboardInput();
                }
            }
            if (input.IsInputPressed(Keys.P))
            {
                if (paused)
                {
                    paused = false;
                }
                else
                {
                    paused = true;
                }
            }
        }

        /// <summary>
        /// Handles the keyboard input.
        /// </summary>
        private void HandleKeyboardInput()
        {
            if (input.IsInputPressed(Keys.Escape))
                exit = true;

            if (input.IsInputDown(Keys.W))
            {
                Player.GetInstance().AddVelocity(0.3f);
            }
            if (input.IsInputDown(Keys.S))
            {
                Player.GetInstance().AddVelocity(-0.3f);
            }
            if (input.IsInputDown(Keys.D))
            {
                Player.GetInstance().Rotate(0.1f);
            }
            if (input.IsInputDown(Keys.A))
            {
                Player.GetInstance().Rotate(-0.1f);
            }
            if (input.IsInputPressed(Keys.Space))
            {
                Bullet bullet = Player.GetInstance().Shoot();
                if (bullet != null)
                    scene.AddDrawableObject(bullet);
            }
        }

        /// <summary>
        /// Handles the game pad input.
        /// </summary>
        private void HandleGamePadInput()
        {
            if (input.IsInputPressed(Buttons.Back))
                exit = true;

            Player.GetInstance().AddVelocity(input.GetGamePadJoystick().Left.Y);
            Player.GetInstance().Rotate(input.GetGamePadJoystick().Left.X / 10);

            if (input.IsInputPressed(Buttons.A))
            {
                scene.AddDrawableObject(Player.GetInstance().Shoot());
            }
        }

        /// <summary>
        /// Draws the specified _sprite batch.
        /// </summary>
        /// <param name="_spriteBatch">The _sprite batch.</param>
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(content.Load<Texture2D>("Graphics\\background"), Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            scene.Draw(_spriteBatch);
        }

        // *************************************************************** //

        /// <summary>
        /// Determines whether this instance has exited.
        /// </summary>
        /// <returns></returns>
        public bool HasExited()
        {
            return exit;
        }
    }
}
