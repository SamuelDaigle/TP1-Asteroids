using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public abstract class Asteroid : Object2D, IMovable
    {
        private Vector2 velocity;
        private DateTime birth;
        private TimeSpan spawningDelay = new TimeSpan(0, 0, 0, 1, 500);
        protected List<IObjectAdderObserver> objectObservers;
        protected bool isMergeable = false;

        public DateTime Birth
        {
            get
            {
                return birth;
            }
        }

        public void Initialize(Sprite _sprite, Vector2 _position)
        {
            objectObservers = new List<IObjectAdderObserver>();
            birth = DateTime.Now;
            base.Initialize(_sprite, _position);
        }

        public void AddObserver(IObjectAdderObserver _observer)
        {
            objectObservers.Add(_observer);
        }

        // IMovable
        public void Update(BoundingBox screen)
        {
            position.X += (int)(velocity.X);
            position.Y += (int)(velocity.Y);

            collisionSphere.Radius = GetDimension().X / 2;
            collisionSphere.Center.X = position.X;
            collisionSphere.Center.Y = position.Y;

            if(DateTime.Now - birth >= spawningDelay)
            {
                isMergeable = true;
            }

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

        protected float[] CalculateNewRotations(Asteroid asteroid, Bullet bullet)
        {
            float[] returnedValues = new float[2] { 0, 0 };
            float PI = 3.1415f;

            float bulletRotation = bullet.Rotation % (2 * PI);
            float asteroidRotation = asteroid.Rotation % (2 * PI);

            if ((bulletRotation >= asteroidRotation - PI / 4) && (bulletRotation < asteroidRotation + PI / 4))
            {
                //Bullet comes from behind
                returnedValues[0] = PI / 4;
                returnedValues[1] = -PI / 4;
            }
            else if ((bulletRotation >= asteroidRotation - 5 * PI / 4) && (bulletRotation < asteroidRotation + 3 * PI / 4))
            {
                //Bullet comes from the front
                returnedValues[0] = PI / 2;
                returnedValues[1] = -PI / 2;
            }
            else if ((bulletRotation >= asteroidRotation + PI / 4) && (bulletRotation < asteroidRotation + 3 * PI / 4))
            {
                //Bullet comes from the right
                returnedValues[0] = -PI / 4;
                returnedValues[1] = 0;
            }
            else
            {
                //Bullet comes from the left
                returnedValues[0] = PI / 4;
                returnedValues[1] = 0;
            }
            return returnedValues;
        }

        public override void AddBonus(Bonus.Type _type)
        {
            base.AddBonus(_type);
            if (_type == Bonus.Type.STOP_TIME)
            {
                velocity = Vector2.Zero;
            }
            else if (_type == Bonus.Type.ASTEROID_EXPLODE)
            {
                Bullet fakeBullet = new Bullet();
                fakeBullet.Initialize(sprite, position);
                HasCollided(fakeBullet);
            }
        }

        public void Draw(GameTime gameTime)
        {
            
        }
    }
}
