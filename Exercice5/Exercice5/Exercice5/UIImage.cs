using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Class that defines a UIElement as being an image.
    /// </summary>
    public class UIImage : IUIElement
    {
        private Sprite sprite;
        private Vector2 position;
        private bool drawn = true;

        /// <summary>
        /// Initializes the specified _sprite.
        /// </summary>
        /// <param name="_sprite">The _sprite.</param>
        /// <param name="_position">The _position.</param>
        public void Initialize(Sprite _sprite, Vector2 _position)
        {
            sprite = _sprite;
            position = _position;
        }

        /// <summary>
        /// Draws the specified renderer.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        public void Draw(SpriteBatch renderer)
        {
            sprite.Draw(renderer, position);
        }

        /// <summary>
        /// Gets the dimension.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetDimension()
        {
            return sprite.GetDimension();
        }

        /// <summary>
        /// Determines whether this instance is drawn.
        /// </summary>
        /// <returns></returns>
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
            sprite.Rotate(angle);
        }
    }
}
