using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class Asteroid : ExplodableObject, IMovable
    {
        private Vector2 velocity;
        private Size size = Size.LARGE;

        public enum Size { SMALL, MEDIUM, LARGE };

        public void Initialize(Sprite _sprite, Vector2 _position, Size _size)
        {
            base.Initialize(_sprite, _position);
            size = _size;

            if (_size == Size.SMALL)
            {
                _sprite.Scale = 0.3f;
            }
            else if (_size == Size.MEDIUM)
            {
                _sprite.Scale = 0.6f;
            }
        }

        // IMovable
        public void Update(BoundingBox screen)
        {
            position.X += (int)(velocity.X);
            position.Y += (int)(velocity.Y);

            collisionSphere.Radius = GetDimension().X / 2;
            collisionSphere.Center.X = position.X + collisionSphere.Radius;
            collisionSphere.Center.Y = position.Y + collisionSphere.Radius;

            StayInBounds(screen);
        }

        private void StayInBounds(BoundingBox screen)
        {

            if (!collisionSphere.Intersects(screen))
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

        public void AddVelocity(float _speed)
        {
            velocity += (new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)) * _speed);
        }

        public override void HasCollided(ICollidable _other)
        {
            Explode();
        }

        public override void Explode()
        {
            drawn = false;
            position.X = 0;
            position.Y = 0;
        }
    }

}
