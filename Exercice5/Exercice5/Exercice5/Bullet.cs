﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Exercice5
{
    public class Bullet : Object2D, IMovable
    {
        private Vector2 velocity;
        private BoundingSphere collisionSphere;
        private DateTime birth;
        private TimeSpan lifeTime;

        public Vector2 Position
        {
            set
            {
                position = value;
            }
        }

        public float Rotation
        {
            set
            {
                sprite.Rotation = value;
            }
        }

        public Bullet()
        {
            lifeTime = new TimeSpan(0,0,1);
        }

        // IMovable
        public void UpdateMovement()
        {
            if(DateTime.Now - birth >= lifeTime)
            {
                drawn = false;
            }

            position.X += (int)(velocity.X);
            position.Y += (int)(velocity.Y);

            collisionSphere.Radius = GetDimension().X / 2;
            collisionSphere.Center.X = position.X + GetDimension().X / 2;
            collisionSphere.Center.Y = position.Y + GetDimension().Y / 2;
        }

        public void StayInBounds(BoundingBox screen)
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

        public void Reset()
        {
            velocity.X = 0;
            velocity.Y = 0;
            drawn = true;
        }

        public void AddVelocity(float _speed)
        {
            velocity += (new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)) * _speed);
        }

        public override void Terminate()
        {
            drawn = false;
        }

        public void StartTimer()
        {
            birth = DateTime.Now;
        }
    }
}