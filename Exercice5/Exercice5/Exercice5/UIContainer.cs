using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exercice5
{
    public class UIContainer
    {
        private List<IUIElement> uiElements;
        private UIText scoreText;
        private UIText scoreLabel;
        private UIText lifeText;
        private UIText lifeLabel;

        public UIContainer()
        {
            uiElements = new List<IUIElement>();
        }

        public void LoadContent(ContentManager content)
        {
            // Add UI elements.
            UIImage scoreBackground = new UIImage();
            scoreBackground.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\hud"), 1f), new Vector2(400, 0));
            AddElement(scoreBackground);

            scoreText = new UIText();
            scoreText.Initialize(content.Load<SpriteFont>("Font\\MainFont"), "0", new Vector2(200, 0));
            AddElement(scoreText);
            scoreLabel = new UIText();
            scoreLabel.Initialize(content.Load<SpriteFont>("Font\\MainFont"), "Score", new Vector2(50, 0));
            AddElement(scoreLabel);

            lifeText = new UIText();
            lifeText.Initialize(content.Load<SpriteFont>("Font\\MainFont"), "3", new Vector2(550, 0));
            AddElement(lifeText);
            lifeLabel = new UIText();
            lifeLabel.Initialize(content.Load<SpriteFont>("Font\\MainFont"), "Life", new Vector2(400, 0));
            AddElement(lifeLabel);
        }

        public void AddElement(IUIElement _element)
        {
            uiElements.Add(_element);
        }

        public void Update()
        {
            scoreText.SetText(Player.GetInstance().Score.ToString());
            lifeText.SetText(Player.GetInstance().Life.ToString());
        }

        public void Draw(SpriteBatch renderer)
        {
            foreach (IUIElement ui in uiElements)
            {
                ui.Draw(renderer);
            }
        }
    }
}
