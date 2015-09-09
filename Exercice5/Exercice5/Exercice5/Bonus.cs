using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    /// <summary>
    /// Bonus can be collected to get buffs to help complete the game.
    /// Different bonuses are described with an enumeration.
    /// </summary>
    public class Bonus : Object2D
    {
        public enum Type { SHRINK, BIGGER_BULLETS, STOP_TIME, SCORE_TWICE, ASTEROID_EXPLODE }

        private Type type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Bonus"/> class.
        /// </summary>
        /// <param name="_type">The _type.</param>
        public Bonus(Type _type)
        {
            type = _type;
        }

        /// <summary>
        /// Determines whether the specified _other has collided.
        /// </summary>
        /// <param name="_other">The _other.</param>
        public override void HasCollided(ICollidable _other)
        {
            if (_other.GetType() == typeof(Player))
            {
                foreach (IBonusObserver observer in bonusObservers)
                {
                    observer.AddBonus(type);
                }
                drawn = false;
            }
        }
    }
}
