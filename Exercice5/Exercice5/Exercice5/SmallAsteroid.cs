using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Class that defines an asteroid as being a small asteroid.
    /// </summary>
    public class SmallAsteroid : Asteroid
    {
        /// <summary>
        /// Determines whether the specified _other has collided.
        /// @see AddAsteroid
        /// </summary>
        /// <param name="_other">The _other.</param>
        public override void HasCollided(ICollidable _other)
        {
            if(_other.GetType() == typeof(SmallAsteroid))
            {
                if (isMergeable)
                {
                    drawn = false;
                    SmallAsteroid other = (SmallAsteroid)_other;
                    other.isMergeable = false;
                    other.drawn = false;
                    foreach (IObjectAdderObserver observer in objectObservers)
                    {
                        observer.AddAsteroid(AsteroidFactory.createNewAsteroid(2, position, RandomGenerator.GetRandomFloat(0, 6.283)));
                    }
                }
            }
            if (_other.GetType() == typeof(Bullet))
            {
                drawn = false;
            }
        }
    }
}
