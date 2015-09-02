using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class Player : Object2D, IMovable
    {
        private static Player instance = null;
        private Vector2 velocity;
        private BoundingSphere collisionBox;
        private State deadState;
        private Queue<Bullet> bullets;
        private readonly float MAX_VELOCITY = 7f;

        public static Player GetInstance()
        {
            if (instance == null)
            {
                instance = new Player();
            }

            return instance;
        }

        private Player()
        {
            bullets = new Queue<Bullet>();
        }

        public void Initialize(Sprite _sprite, Vector2 _position, Sprite _bulletSprite)
        {
            base.Initialize(_sprite, _position);
            for(int i = 0; i < 15; i++)
            {
                Bullet bullet = new Bullet();
                bullet.Initialize(_bulletSprite, position);
                bullet.AddVelocity(5f);
                bullets.Enqueue(bullet);
            }
        }

        //*************************//

        

        public void UpdateMovement()  
        {
            double currentSpeed = GetSpeed();

            FixMaximumVelocity(currentSpeed);

            position.X += (int)(velocity.X);
            position.Y += (int)(velocity.Y);

            collisionBox.Radius = GetDimension().X / 2;
            collisionBox.Center.X = position.X + GetDimension().X / 2;
            collisionBox.Center.Y = position.Y + GetDimension().Y / 2;
        }

        private void FixMaximumVelocity(double _speed)
        {
            if (_speed > MAX_VELOCITY)
            {
                float ratio = (float)(MAX_VELOCITY / _speed);
                velocity.X *= ratio;
                velocity.Y *= ratio;
            }
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

        public void SetDeadState(State _deadState)
        {
            deadState = _deadState;
        }

        // IMovable
        public void AddVelocity(float _speed)
        {
            if (_speed < 0)
                velocity /= 2;
            velocity += (new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)) * _speed);
        }

        public override void Terminate()
        {
            drawn = false;
        }

        private double GetSpeed()
        {
            return Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));
        }

        public Bullet Shoot()
        {
            
                Bullet thrownBullet = bullets.Dequeue();
                thrownBullet.Reset();
                thrownBullet.Position = position;
                thrownBullet.Rotation = Rotation;
                thrownBullet.AddVelocity(11f);
                thrownBullet.StartTimer();
                bullets.Enqueue(thrownBullet);
            
            return thrownBullet;
        }
    }

}
