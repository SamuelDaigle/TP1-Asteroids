using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    public class SmallSpaceship : Enemy
    {
        void HasCollided(ICollidable _other)
        {
            drawn = false;
        }
    }
}
