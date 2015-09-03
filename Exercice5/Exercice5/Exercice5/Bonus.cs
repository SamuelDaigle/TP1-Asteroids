using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    public class Bonus : Object2D
    {
        public enum Type { SHRINK, BIGGER_BULLETS, STOP_TIME, EXPLODE_WAVE, ASTEROID_EXPLODE, MORE_ENEMIES}

        private Type type;

        public Bonus(Type _type)
        {
            type = _type;
        }

        public override void HasCollided(ICollidable _other)
        {
            foreach (IBonusObserver observer in observers)
            {
                if (observer.Equals(_other))
                    observer.AddBonus(type);
            }
            drawn = false;
        }

    }
}
