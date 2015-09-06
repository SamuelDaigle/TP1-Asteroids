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
        protected Scene scene;
        protected ContentManager content;
        protected InputHandler input;
        private bool exit = false;
        private bool paused = false;

        public void LoadContent(ContentManager _content)
        {
            content = _content;
            AsteroidFactory.SetContent(_content);
            scene = new Scene();
            input = AsteroidGame.input;

            // Player
            Player.GetInstance().Initialize(new Sprite(content.Load<Texture2D>("Graphics\\ship"), 0.3f), new Vector2(500, 300), new Sprite(content.Load<Texture2D>("Graphics\\ship"), 0.05f));

            // Asteroid
            Asteroid asteroid = AsteroidFactory.createNewAsteroid(1, Vector2.Zero);

            // Bonus
            Bonus shrinkBonus = new Bonus(Bonus.Type.BIGGER_BULLETS);
            shrinkBonus.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\ship"), 0.3f), new Vector2(700, 500));
            shrinkBonus.AddObserver(Player.GetInstance());

            // Add all previous objects to scene.
            scene.AddDrawableObject(Player.GetInstance());
            scene.AddDrawableObject(asteroid);
            scene.AddDrawableObject(shrinkBonus);
        }

        public void Update()
        {
            scene.Update(AsteroidGame.screenBox);
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
