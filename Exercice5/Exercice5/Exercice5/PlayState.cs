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
    public class PlayState : IGameState
    {
        protected int level;
        protected Scene scene;
        protected ContentManager content;
        protected InputHandler input;
        private bool exit = false;
        private bool paused = false;

        public PlayState(int _level)
        {
            level = _level;
        }

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
            Player.GetInstance().Initialize(content, new Sprite(content.Load<Texture2D>("Graphics\\ship"), 0.3f), new Vector2(500, 300));
            for (int i = 0; i < Player.MAX_NB_BULLETS; i++)
            {
                Bullet bullet = new Bullet();
                bullet.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\ship"), 0.05f), Player.GetInstance().Position);
                bullet.AddVelocity(5f);
                bullet.AddScoreObserver(Player.GetInstance());
                Player.GetInstance().StoreBullet(bullet);
            }

            //Enemy
            Enemy enemy = EnemyFactory.createEnemy(1, Vector2.Zero);
            Enemy largeEnemy = EnemyFactory.createEnemy(2, new Vector2(0, 0));
            Enemy specialEnemy = EnemyFactory.createEnemy(3, new Vector2(200, 200));

            // Bonus
            Bonus shrinkBonus = new Bonus(Bonus.Type.ASTEROID_EXPLODE);
            shrinkBonus.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\ship"), 0.3f), new Vector2(700, 500));
            foreach (Asteroid asteroid in scene.GetAllAsteroids())
            {
                shrinkBonus.AddBonusObserver(asteroid);
            }

            // Add all previous objects to scene.
            scene.AddDrawableObject(Player.GetInstance());
            scene.AddDrawableObject(shrinkBonus);
            //scene.AddDrawableObject(enemy);
            //scene.AddDrawableObject(largeEnemy);
            //scene.AddDrawableObject(specialEnemy);
        }

        public void Update()
        {
            if (!paused)
            {
                scene.Update(AsteroidGame.screenBox);

                if (scene.onlyHasPlayer())
                {
                    AsteroidGame.gameState = new PlayState(level + 1);
                    AsteroidGame.gameState.LoadContent(content);
                }
            }
        }

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

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(content.Load<Texture2D>("Graphics\\background"), Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            scene.Draw(_spriteBatch);
        }

        // *************************************************************** //

        public bool HasExited()
        {
            return exit;
        }
    }
}
