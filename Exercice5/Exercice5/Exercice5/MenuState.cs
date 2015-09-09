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
    /// MenuState will display the possible choices
    /// to be made before starting a game.
    /// </summary>
    public class MenuState : IGameState
    {
        protected ContentManager content;
        protected InputHandler input;
        private bool exit = false;
        private readonly int NB_OPTION = 3;
        private int selectedOption = 0;
        private string[] optionText;


        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        public void LoadContent(ContentManager _content)
        {
            content = _content;
            optionText = new string[NB_OPTION];
            optionText[0] = "Play";
            optionText[1] = "Option";
            optionText[2] = "Exit";
            input = AsteroidGame.input;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            
        }

        /// <summary>
        /// Handles the input.
        /// @see HandleGamePadInput
        /// @see HandleKeyboardInput
        /// </summary>
        public void HandleInput()
        {
            if (input.IsGamePadOneConnected())
            {
                HandleGamePadInput();
            }
            else
            {
                HandleKeyboardInput();
            }
            
            if (selectedOption < 0)
            {
                selectedOption = NB_OPTION - 1;
            }
            if (selectedOption >= NB_OPTION)
            {
                selectedOption = 0;
            }
        }

        /// <summary>
        /// Handles the keyboard input.
        /// </summary>
        private void HandleKeyboardInput()
        {
            if (input.IsInputPressed(Keys.Escape))
                exit = true;

            if (input.IsInputPressed(Keys.W))
            {
                selectedOption--;
                
            }
            if (input.IsInputPressed(Keys.S))
            {
                selectedOption++;
            }

            if (input.IsInputPressed(Keys.Space))
            {
                selectOption();
            }
        }

        /// <summary>
        /// Handles the game pad input.
        /// </summary>
        private void HandleGamePadInput()
        {
            if (input.IsInputPressed(Buttons.Back))
                exit = true;

            if (input.IsThumbStickDown(InputHandler.GamePadThumbSticksSide.LEFT, -0.5f))
            {
                selectedOption++;
            }
            if (input.IsThumbStickUp(InputHandler.GamePadThumbSticksSide.LEFT, 0.5f))
            {
                selectedOption--;
            }
            if (input.IsInputPressed(Buttons.A))
            {
                selectOption();
            }
        }

        /// <summary>
        /// Draws the specified _sprite batch.
        /// </summary>
        /// <param name="_spriteBatch">The _sprite batch.</param>
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(content.Load<Texture2D>("Graphics\\background"), Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            Color textColor;

            for (int i = 0; i < NB_OPTION; i++)
            {
                textColor = Color.White;
                if (selectedOption == i)
                    textColor = Color.Blue;
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), optionText[i], new Vector2(500, 100 * i), textColor);
            }
        }

        /// <summary>
        /// Determines whether this instance has exited.
        /// </summary>
        /// <returns></returns>
        public bool HasExited()
        {
            return exit;
        }

        /// <summary>
        /// Selects the option.
        /// </summary>
        private void selectOption()
        {
            if (selectedOption == 0)
            {
                AsteroidGame.gameState = new PlayState(1);
                AsteroidGame.gameState.LoadContent(content);
            }

            if (selectedOption == 1)
            {
                AsteroidGame.gameState = new LeaderboardState();
                AsteroidGame.gameState.LoadContent(content);
            }

            if (selectedOption == 2)
            {
                exit = true;
            }
        }
    }
}
