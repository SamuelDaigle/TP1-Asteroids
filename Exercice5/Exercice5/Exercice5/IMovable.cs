using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Exercice5
{
    /// <summary>
    /// Interface that enables other classes to be movable.
    /// </summary>
    public interface IMovable
    {
        /// <summary>
        /// Adds the velocity.
        /// </summary>
        /// <param name="_speed">The _speed.</param>
        void AddVelocity(float _speed);

        /// <summary>
        /// Updates the specified screen.
        /// </summary>
        /// <param name="screen">The screen.</param>
        void Update(BoundingBox screen);
    }
}
