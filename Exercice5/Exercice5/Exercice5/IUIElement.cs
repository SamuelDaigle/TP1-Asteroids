using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Interface that enables other classes to have drawable UIElement
    /// </summary>
    public interface IUIElement : IDrawable
    {
        /// <summary>
        /// Draws the specified renderer.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        void Draw(SpriteBatch renderer);

        /// <summary>
        /// Determines whether this instance is drawn.
        /// </summary>
        /// <returns></returns>
        bool IsDrawn();
    }
}
