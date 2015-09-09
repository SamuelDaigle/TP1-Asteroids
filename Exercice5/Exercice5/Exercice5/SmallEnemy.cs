using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Class that defines an enmey as being a small enemy.
    /// </summary>
    public class SmallEnemy : Enemy
    {
        private int frameCount = 0;
        private float direction;
        private const float DIRECTION_VALUE = 0.05f;

        /// <summary>
        /// Updates the specified screen.
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
                direction = DIRECTION_VALUE;
                if (RandomGenerator.GetRandomInt(0, 2) >= 1)
                {
                    direction = -DIRECTION_VALUE;
                }
                if (Rotation > 3.1415 / 2)
                {
                    direction = -DIRECTION_VALUE;
                }
                else if (Rotation < -3.1415 / 2)
                {
                    direction = DIRECTION_VALUE;
                }
                Rotate(direction);
                AddVelocity(1f);
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

            double playerDistance = 1000;
            Vector2 playerPosition = Vector2.Zero;

            foreach (Object2D movableObject in movableObjects)
            {
                if (movableObject != this)
                {
                    double x = movableObject.Position.X - Position.X;
                    double y = movableObject.Position.Y - Position.Y;
                    double distance = Math.Sqrt(y * y + x * x);

                    if (movableObject.GetType() == typeof(Player))
                    {
                        playerDistance = distance;
                        playerPosition = movableObject.Position;
                    }
                    if (distance < closerDistance)
                    {
                        closerDistance = distance;
                        closerPosition = movableObject.Position;
                    }
                }
            }

            if (playerDistance < 300)
            {
                return Shoot((float)Math.Asin(playerPosition.Y / playerDistance));
            }
            else if (closerDistance < 300)
            {
                return Shoot((float)Math.Asin(closerPosition.Y / closerDistance));
            }
            return null;
        }
    }
}
