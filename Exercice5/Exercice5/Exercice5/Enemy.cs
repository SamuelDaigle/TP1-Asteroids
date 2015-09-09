using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Enemy is an abstract class that represent one
    /// of the three possible types of enemies (small, large or special).
    /// </summary>
    public abstract class Enemy : Object2D, IMovable
    {
        private Vector2 velocity;
        private Queue<Bullet> bullets;
        private DateTime lastShot = DateTime.Now;
        private TimeSpan shootingDelay = new TimeSpan(0, 0, 0, 1, 500);
        private readonly int MAX_NB_BULLETS = 3;

        /// <summary>
        /// Initializes the specified instance of an enemy.
        /// </summary>
        /// <param name="_sprite">The _sprite.</param>
        /// <param name="_position">The _position.</param>
        /// <param name="_bulletSprite">The _bullet sprite.</param>
        public void Initialize(Sprite _sprite, Vector2 _position, Sprite _bulletSprite)
        {
            bullets = new Queue<Bullet>();
            base.Initialize(_sprite, _position);
            AddVelocity(1f);
            for (int i = 0; i < MAX_NB_BULLETS; i++)
            {
                Bullet bullet = new Bullet();
                bullet.Initialize(_bulletSprite, position);
                bullet.AddVelocity(5f);
                bullets.Enqueue(bullet);
            }
        }

        // IMovable
        /// <summary>
        /// Updates the specified instance according to the screen.
        /// </summary>
        /// <param name="screen">The screen.</param>
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

        /// <summary>
        /// Deletes the enemy if it goes out of screen's width and forces the enemy
        /// to stay inside the screen's height.
        /// </summary>
        /// <param name="screen">The screen.</param>
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
                    drawn = false;
                if (position.X <= 0)
                    drawn = false;
            }
        }

        /// <summary>
        /// Adds the velocity.
        /// </summary>
        /// <param name="_speed">The _speed.</param>
        public void AddVelocity(float _speed)
        {
            velocity += (new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)) * _speed);
        }

        /// <summary>
        /// Chooses the direction.
        /// </summary>
        protected abstract void chooseDirection();

        /// <summary>
        /// Chooses to attack.
        /// </summary>
        /// <param name="movableObjects">The movable objects.</param>
        /// <returns></returns>
        public abstract Bullet chooseToAttack(List<Object2D> movableObjects);

        /// <summary>
        /// Shoots the specified _rotation.
        /// @see Reset
        /// @see StartTimer
        /// </summary>
        /// <param name="_rotation">The _rotation.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Fixes the maximum velocity.
        /// </summary>
        /// <param name="_speed">The _speed.</param>
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
