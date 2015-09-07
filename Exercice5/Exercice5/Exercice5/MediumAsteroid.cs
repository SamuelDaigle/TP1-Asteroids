using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    class MediumAsteroid : Asteroid
    {
        public override void HasCollided(ICollidable _other)
        {
            if (_other.GetType() == typeof(MediumAsteroid))
            {
                TimeSpan spawningDelay = new TimeSpan(0, 0, 3);
                if (DateTime.Now - Birth >= spawningDelay)
                {
                    drawn = false;
                }
            }
            else if (_other.GetType() == typeof(Bullet))
            {
                drawn = false;
            }
        }
    }
}
