using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    class LargeAsteroid : Asteroid
    {
        public override void HasCollided(ICollidable _other)
        {
            if (_other.GetType() == typeof(Bullet))
            {
                drawn = false;
                float[] rotationsValue = CalculateNewRotations(this, (Bullet)_other);
                foreach (IObjectAdderObserver observer in objectObservers)
                {
                    observer.AddAsteroid(AsteroidFactory.createNewAsteroid(2, position, rotationsValue[0] + Rotation));
                    observer.AddAsteroid(AsteroidFactory.createNewAsteroid(2, position, rotationsValue[1] + Rotation));
                }
            }
        }
    }
}
