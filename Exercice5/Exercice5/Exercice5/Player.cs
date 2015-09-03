﻿using System;
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
            for (int i = 0; i < 15; i++)
            {
                Bullet bullet = new Bullet();
                bullet.Initialize(_bulletSprite, position);
                bullet.AddVelocity(5f);
                bullets.Enqueue(bullet);
            }
        }

        //*************************//



        public void Update(BoundingBox screen)
        {
            double currentSpeed = GetSpeed();

            FixMaximumVelocity(currentSpeed);

            position.X += (int)(velocity.X);
            position.Y += (int)(velocity.Y);

            collisionSphere.Radius = GetDimension().X / 2;
            collisionSphere.Center.X = position.X + GetDimension().X / 2;
            collisionSphere.Center.Y = position.Y + GetDimension().Y / 2;

            StayInBounds(screen);
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
            position.X = 0;
            position.Y = 0;
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
