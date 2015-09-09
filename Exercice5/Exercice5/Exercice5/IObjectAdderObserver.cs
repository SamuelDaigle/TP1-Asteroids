using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    /// <summary>
    /// Interface that enables other classes to create asteroids.
    /// </summary>
    public interface IObjectAdderObserver
    {
        /// <summary>
        /// Adds the asteroid.
        /// </summary>
        /// <param name="_object">The _object.</param>
        void AddAsteroid(Asteroid _object);
    }
}
