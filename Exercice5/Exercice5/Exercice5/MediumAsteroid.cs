using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Class that defines an asteroid as being a medium asteroid.
    /// </summary>
    class MediumAsteroid : Asteroid
    {
        /// <summary>
        /// Determines whether the specified _other has collided.
        /// @see AddAsteroid
        /// @see CalculateNewRotations
        /// </summary>
        /// <param name="_other">The _other.</param>
        public override void HasCollided(ICollidable _other)
        {
            if (_other.GetType() == typeof(MediumAsteroid))
            {
                if (isMergeable)
                {
                    drawn = false;
                    MediumAsteroid other = (MediumAsteroid)_other;
                    other.isMergeable = false;
                    other.drawn = false;
                    foreach (IObjectAdderObserver observer in objectObservers)
                    {
                        observer.AddAsteroid(AsteroidFactory.createNewAsteroid(1, position, RandomGenerator.GetRandomFloat(0, 6.283)));
                    }
                }
            }
            else if (_other.GetType() == typeof(Bullet))
            {
                drawn = false;
                float[] rotationsValue = CalculateNewRotations(this, (Bullet)_other);
                foreach (IObjectAdderObserver observer in objectObservers)
                {
                    observer.AddAsteroid(AsteroidFactory.createNewAsteroid(3, position, rotationsValue[0] + Rotation));
                    observer.AddAsteroid(AsteroidFactory.createNewAsteroid(3, position, rotationsValue[1] + Rotation));
                }
            }
        }
    }
}
