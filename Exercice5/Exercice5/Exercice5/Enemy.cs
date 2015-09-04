using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Exercice5
{
    public abstract class Enemy : Object2D, IMovable
    {
        private Vector2 velocity;

        public void AddVelocity(float _speed)
        {
            velocity += (new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)) * _speed);
        }

        public void Update(BoundingBox screen)
        {
            position.X += (int)(velocity.X);
            position.Y += (int)(velocity.Y);

            collisionSphere.Radius = GetDimension().X / 2;
            collisionSphere.Center.X = position.X;
            collisionSphere.Center.Y = position.Y;

            StayInBounds(screen);
        }
    }
}
