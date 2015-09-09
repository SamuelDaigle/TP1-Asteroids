using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Class that defines an enemy as being a special enemy.
    /// </summary>
    class SpecialEnemy : Enemy
    {
        private int frameCount = 0;
        private const float DIRECTION_VALUE = 0.05f;

        /// <summary>
        /// Updates the specified screen.
        /// @see ChooseDirection
        /// </summary>
        /// <param name="screen">The screen.</param>
        public override void Update(BoundingBox screen)
        {
            chooseDirection();
            base.Update(screen);
        }

        /// <summary>
        /// Determines whether the specified _other has collided.
        /// </summary>
        /// <param name="_other">The _other.</param>
        public override void HasCollided(ICollidable _other)
        {
            if (_other.GetType() == typeof(Bullet))
            {
                if (((Bullet)_other).Shooter != this)
                {
                    drawn = false;
                }
            }
        }

        /// <summary>
        /// Chooses the direction.
        /// </summary>
        protected override void chooseDirection()
        {
            if (frameCount == 2)
            {
                frameCount = 0;
                Rotate(DIRECTION_VALUE);
            }
            frameCount++;
        }

        /// <summary>
        /// Chooses to attack.
        /// @see Shoot
        /// </summary>
        /// <param name="movableObjects">The movable objects.</param>
        /// <returns></returns>
        public override Bullet chooseToAttack(List<Object2D> movableObjects)
        {
            double closerDistance = 1000;
            Vector2 closerPosition = Vector2.Zero;

            foreach (Object2D movableObject in movableObjects)
            {
                if (movableObject != this)
                {
                    double x = movableObject.Position.X - Position.X;
                    double y = movableObject.Position.Y - Position.Y;
                    double distance = Math.Sqrt(y * y + x * x);

                    if (distance < closerDistance)
                    {
                        closerDistance = distance;
                        closerPosition = movableObject.Position;
                    }
                }
            }
        return Shoot(Rotation);
        }
    }
}
