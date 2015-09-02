using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class Asteroid : Object2D, IMovable, ICollidable
    {
        private Vector2 velocity;
        private BoundingSphere collisionBox;

        public void UpdateMovement()
        {
            position.X += (int)(velocity.X);
            position.Y += (int)(velocity.Y);

            collisionBox.Radius = GetDimension().X / 2;
            collisionBox.Center.X = position.X + GetDimension().X / 2;
            collisionBox.Center.Y = position.Y + GetDimension().Y / 2;
        }

        public void StayInBounds(BoundingBox screen)
        {
            if (!collisionBox.Intersects(screen))
            {
                float screenHeight = screen.Max.Y - screen.Min.Y;
                float screenWidth = screen.Max.X - screen.Min.X;

                if (position.Y >= screenHeight)
                    position.Y -= screenHeight;
                if (position.Y <= 0)
                    position.Y += screenHeight + sprite.GetDimension().Y;

                if (position.X >= screenWidth)
                    position.X -= screenWidth;
                if (position.X <= 0)
                    position.X += screenWidth + sprite.GetDimension().X;
            }
        }

        private void Die()
        {
        }

        // IMovable
        public void AddVelocity(float _speed)
        {
            velocity += (new Vector2((float)Math.Cos(sprite.GetRotation()), (float)Math.Sin(sprite.GetRotation())) * _speed);
        }

        // ICollidable
        public BoundingSphere GetCollision()
        {
            return collisionBox;
        }

        public void HasCollided()
        {
            Die();
        }
    }

}
