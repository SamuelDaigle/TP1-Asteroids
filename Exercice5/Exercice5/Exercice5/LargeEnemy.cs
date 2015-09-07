using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    class LargeEnemy : Enemy
    {
        private int frameCount = 0;
        private float direction;
        private const float DIRECTION_VALUE = 0.05f;

        public override void Update(BoundingBox screen)
        {
            chooseDirection();
            base.Update(screen);
        }

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

            if (closerDistance < 400)
            {
                return Shoot((float)Math.Asin(closerPosition.Y / closerDistance) + RandomGenerator.GetRandomFloat(-1f,1f));
            }
            return null;
        }
    }
}
