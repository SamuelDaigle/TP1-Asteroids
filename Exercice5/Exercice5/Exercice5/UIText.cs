using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Class that defines a UIELement as being a text field.
    /// </summary>
    public class UIText : IUIElement, ITextObserver
    {
        private SpriteFont spriteFont;
        private string text;
        private Vector2 position;
        private bool drawn = true;

        /// <summary>
        /// Initializes the specified _sprite font.
        /// </summary>
        /// <param name="_spriteFont">The _sprite font.</param>
        /// <param name="_text">The _text.</param>
        /// <param name="_position">The _position.</param>
        public void Initialize(SpriteFont _spriteFont, string _text, Vector2 _position)
        {
            spriteFont = _spriteFont;
            text = _text;
            position = _position;
        }

        /// <summary>
        /// Draws the specified renderer.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        public void Draw(SpriteBatch renderer)
        {
            renderer.DrawString(spriteFont, text, position, Color.White);
        }

        /// <summary>
        /// Gets the dimension.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetDimension()
        {
            return Vector2.Zero;
        }

        public bool IsDrawn()
        {
            return drawn;
        }

        /// <summary>
        /// Rotates the specified angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        public void Rotate(float angle)
        {
            
        }

        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="_text">The _text.</param>
        public void SetText(string _text)
        {
            text = _text;
        }
    }
}
