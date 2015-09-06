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

            }
            else if (_other.GetType() == typeof(Bullet))
            {
                drawn = false;
            }
        }
    }
}
