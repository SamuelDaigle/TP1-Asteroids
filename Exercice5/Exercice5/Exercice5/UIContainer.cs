using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exercice5
{
    /// <summary>
    /// Class that contains the different UI
    /// on the screen.
    /// </summary>
    public class UIContainer
    {
        private List<IUIElement> uiElements;
        private UIText scoreText;
        private UIText scoreLabel;
        private UIText lifeText;
        private UIText lifeLabel;

        /// <summary>
        /// Initializes a new instance of the <see cref="UIContainer"/> class.
        /// </summary>
        public UIContainer()
        {
            uiElements = new List<IUIElement>();
        }

        /// <summary>
        /// Loads the content.
        /// @see Initialize
        /// @see AddElement
        /// </summary>
        /// <param name="content">The content.</param>
        public void LoadContent(ContentManager content)
        {
            // Add UI elements.
            UIImage scoreBackground = new UIImage();
            scoreBackground.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\hud"), 1f), new Vector2(300, 0));
            AddElement(scoreBackground);

            scoreText = new UIText();
            scoreText.Initialize(content.Load<SpriteFont>("Font\\MainFont"), "0", new Vector2(150, 0));
            AddElement(scoreText);
            scoreLabel = new UIText();
            scoreLabel.Initialize(content.Load<SpriteFont>("Font\\MainFont"), "Score:", new Vector2(10, 0));
            AddElement(scoreLabel);

            lifeText = new UIText();
            lifeText.Initialize(content.Load<SpriteFont>("Font\\MainFont"), "3", new Vector2(410, 0));
            AddElement(lifeText);
            lifeLabel = new UIText();
            lifeLabel.Initialize(content.Load<SpriteFont>("Font\\MainFont"), "Life:", new Vector2(290, 0));
            AddElement(lifeLabel);
        }

        /// <summary>
        /// Adds the element.
        /// </summary>
        /// <param name="_element">The _element.</param>
        public void AddElement(IUIElement _element)
        {
            uiElements.Add(_element);
        }

        /// <summary>
        /// Updates this instance.
        /// @see SetText
        /// </summary>
        public void Update()
        {
            scoreText.SetText(Player.GetInstance().Score.ToString());
            lifeText.SetText(Player.GetInstance().Life.ToString());
        }

        /// <summary>
        /// Draws the specified renderer.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        public void Draw(SpriteBatch renderer)
        {
            foreach (IUIElement ui in uiElements)
            {
                ui.Draw(renderer);
            }
        }
    }
}
