using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Exercice5
{
    /// <summary>
    /// Interface that enables other classes to become collidable.
    /// </summary>
    public interface ICollidable
    {
        /// <summary>
        /// Gets the collision.
        /// </summary>
        /// <returns></returns>
        BoundingSphere GetCollision();

        /// <summary>
        /// Determines whether the specified _other has collided.
        /// </summary>
        /// <param name="_other">The _other.</param>
        void HasCollided(ICollidable _other);
    }
}
