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
    public class SaveScoreMenu : IGameState
    {
        protected ContentManager content;
        protected InputHandler input;
        private bool exit = false;
        private string score;
        private string name;

        public SaveScoreMenu(string _score)
        {
            score = _score;
            name = "";
        }

        public void LoadContent(ContentManager _content)
        {
            content = _content;
            input = AsteroidGame.input;
        }

        public void Update()
        {

        }

        public void HandleInput()
        {
            if (input.IsInputPressed(Keys.Escape) || input.IsInputPressed(Buttons.Back))
            {
                AsteroidGame.gameState = new MenuState();
                AsteroidGame.gameState.LoadContent(content);
            }

            if (input.IsInputPressed(Keys.Enter))
            {
                XMLScoreWriter writer = new XMLScoreWriter();
                writer.WriteXML(name, score);
                AsteroidGame.gameState = new MenuState();
                AsteroidGame.gameState.LoadContent(content);
            }

            foreach (Keys key in input.GetPressedKeys())
            {
                if (key == Keys.Back)
                {
                    if (name.Length > 0)
                    {
                        name = name.Substring(0, name.Length - 1);
                    }
                }
                else if (name.Length <= 10)
                {
                    name += key.ToString();
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), name, new Vector2(300, 400), Color.White);
            _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), score, new Vector2(700, 400), Color.White);
        }

        public bool HasExited()
        {
            return exit;
        }
    }
}
