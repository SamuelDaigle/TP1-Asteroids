using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Interface that enables other classes to become drawable on the game screen.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Draws the specified renderer.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        void Draw(SpriteBatch renderer);

        /// <summary>
        /// Gets the dimension.
        /// </summary>
        /// <returns></returns>
        Vector2 GetDimension();

        /// <summary>
        /// Determines whether this instance is drawn.
        /// </summary>
        /// <returns></returns>
        bool IsDrawn();

        /// <summary>
        /// Rotates the specified angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        void Rotate(float angle);
    }
}
