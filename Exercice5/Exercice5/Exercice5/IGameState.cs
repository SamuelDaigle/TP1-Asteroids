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
    /// Interface that enables the game to hold multiple state.
    /// </summary>
    public interface IGameState
    {
        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        void LoadContent(ContentManager _content);

        /// <summary>
        /// Updates this instance.
        /// </summary>
        void Update();

        /// <summary>
        /// Handles the input.
        /// </summary>
        void HandleInput();

        /// <summary>
        /// Draws the specified _sprite batch.
        /// </summary>
        /// <param name="_spriteBatch">The _sprite batch.</param>
        void Draw(SpriteBatch _spriteBatch);

        /// <summary>
        /// Determines whether this instance has exited.
        /// </summary>
        /// <returns></returns>
        bool HasExited();
    }
}
