using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public abstract class Enemy : Object2D, IMovable
    {
        private Vector2 velocity;
        private Queue<Bullet> bullets;
        private DateTime lastShot = DateTime.Now;
        private TimeSpan shootingDelay = new TimeSpan(0, 0, 3);
        private readonly int MAX_NB_BULLETS = 3;

        public void Initialize(Sprite _sprite, Vector2 _position, Sprite _bulletSprite)
        {
            bullets = new Queue<Bullet>();
            base.Initialize(_sprite, _position);
            AddVelocity(4f);
            for (int i = 0; i < MAX_NB_BULLETS; i++)
            {
                Bullet bullet = new Bullet();
                bullet.Initialize(_bulletSprite, position);
                bullet.AddVelocity(5f);
                bullets.Enqueue(bullet);
            }
        }

        // IMovable
        public virtual void Update(BoundingBox screen)
        {

            position.X += (int)(velocity.X);
            position.Y += (int)(velocity.Y);

            FixMaximumVelocity(Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y)));

            collisionSphere.Radius = GetDimension().X / 2;
            collisionSphere.Center.X = position.X;
            collisionSphere.Center.Y = position.Y;

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

        protected abstract void chooseDirection();

        public abstract Bullet chooseToAttack(List<Object2D> movableObjects);

        public Bullet Shoot(float _rotation)
        {
            Bullet thrownBullet = null;

            if (DateTime.Now - lastShot >= shootingDelay)
            {
                lastShot = DateTime.Now;
                if (drawn)
                {
                    thrownBullet = bullets.Dequeue();
                    thrownBullet.Reset();
                    thrownBullet.Shooter = this;
                    thrownBullet.Position = position;
                    thrownBullet.Rotation = _rotation;
                    thrownBullet.AddVelocity(11f);
                    thrownBullet.StartTimer();
                    bullets.Enqueue(thrownBullet);
                }
            }
            return thrownBullet;
        }

        private void FixMaximumVelocity(double _speed)
        {
            if (_speed > 2)
            {
                float ratio = (float)(2 / _speed);
                velocity.X *= ratio;
                velocity.Y *= ratio;
            }
        }
    }
}
