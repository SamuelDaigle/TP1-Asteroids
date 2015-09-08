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
    public class LeaderboardState : IGameState
    {
        protected ContentManager content;
        protected InputHandler input;
        private bool exit = false;
        private List<Score> scores;


        public void LoadContent(ContentManager _content)
        {
            content = _content;
            scores = new List<Score>();
            input = AsteroidGame.input;
            XMLScoreReader reader = new XMLScoreReader();
            reader.Load("Scores.xml");
            scores = reader.GetScores();
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
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < scores.Count; i++)
            {
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), scores[i].name, new Vector2(300, 100 * i), Color.White);
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), scores[i].score.ToString(), new Vector2(500, 100 * i), Color.White);
            }
        }

        public bool HasExited()
        {
            return exit;
        }
    }
}
