using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class SmallAsteroid : Asteroid
    {
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
