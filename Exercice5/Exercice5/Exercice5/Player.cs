using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exercice5
{
    public class Player : Object2D, IMovable, IScoreObserver
    {
        private static Player instance = null;
        private ContentManager content;
        private Vector2 velocity;
        private Queue<Bullet> bullets;
        private readonly float MAX_VELOCITY = 7f;
        public static readonly int MAX_NB_BULLETS = 15;
        private int score;
        private int lifeCount;
        private DateTime invulnerabilityStart;
        private bool isInvulnerable;

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
            score = 0;
            lifeCount = 1;
            invulnerabilityStart = DateTime.MinValue;
            isInvulnerable = false;
        }

        public void StoreBullet(Bullet _bullet)
        {
            bullets.Enqueue(_bullet);
        }

        public void Initialize(ContentManager _content, Sprite _sprite, Vector2 _position)
        {
            base.Initialize(_sprite, _position);
            content = _content;
        }

        public void Update(BoundingBox screen)
        {
            double currentSpeed = GetSpeed();

            FixMaximumVelocity(currentSpeed);

            position.X += (int)(velocity.X);
            position.Y += (int)(velocity.Y);

            collisionSphere.Radius = GetDimension().X / 2;
            collisionSphere.Center.X = position.X + GetDimension().X / 2;
            collisionSphere.Center.Y = position.Y + GetDimension().Y / 2;

            if (isInvulnerable && invulnerabilityStart + TimeSpan.FromSeconds(5) < DateTime.Now)
            {
                isInvulnerable = false;
            }

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
                _speed /= 2;
            velocity += (new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)) * _speed);
        }

        public override void HasCollided(ICollidable _other)
        {
            if (!isInvulnerable)
            {
                if (_other.GetType() != typeof(Bonus))
                {
                    if (_other.GetType() != typeof(Bullet))
                    {
                        drawn = false;
                    }
                    else if (((Bullet)_other).Shooter != this)
                    {
                        drawn = false;
                    }
                }
                if (drawn == false && lifeCount >= 1)
                {
                    drawn = true;
                    lifeCount--;
                    CheckIfDead();
                    invulnerabilityStart = DateTime.Now;
                    isInvulnerable = true;
                }
            }
        }

        private double GetSpeed()
        {
            return Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));
        }

        public Bullet Shoot()
        {
            Bullet thrownBullet = null;
            if (drawn)
            {
                thrownBullet = bullets.Dequeue();
                thrownBullet.Reset();
                thrownBullet.Shooter = this;
                thrownBullet.Position = position;
                thrownBullet.Rotation = Rotation;
                thrownBullet.AddVelocity(11f);
                thrownBullet.StartTimer();
                bullets.Enqueue(thrownBullet);
            }

            return thrownBullet;
        }

        public override void AddBonus(Bonus.Type _type)
        {
            base.AddBonus(_type);

            if (_type == Bonus.Type.BIGGER_BULLETS)
            {
                for (int i = 0; i < MAX_NB_BULLETS; i++)
                {
                    Bullet bullet = bullets.Dequeue();
                    bullet.SpriteImage.Scale = 0.1f;
                    bullets.Enqueue(bullet);
                }
            }
        }

        public void AddScore(int _score)
        {
            score += _score;
            if (score >= (lifeCount * 100))
            {
                lifeCount++;
            }
        }

        public override void Draw(SpriteBatch renderer)
        {
            if (IsDrawn())
            {
                if (isInvulnerable)
                {
                    TimeSpan difference = DateTime.Now - invulnerabilityStart;
                    if (difference.Milliseconds < 500)
                        sprite.Draw(renderer, position);
                }
                else
                {
                    sprite.Draw(renderer, position);
                }
            }
        }

        private void CheckIfDead()
        {
            if (lifeCount == 0)
            {
                AsteroidGame.gameState = new SaveScoreMenu(score.ToString());
                AsteroidGame.gameState.LoadContent(content);
            }
        }
    }

}
