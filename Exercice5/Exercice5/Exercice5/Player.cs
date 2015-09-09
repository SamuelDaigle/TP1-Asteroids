using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exercice5
{
    /// <summary>
    /// Singleton class. Player can only be initialized once.
    /// </summary>
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
        private int scoreRatio;
        private bool hasShrunk;

        /// <summary>
        /// Gets the score.
        /// </summary>
        /// <value>
        /// The score.
        /// </value>
        public int Score
        {
            get
            {
                return score;
            }
        }

        /// <summary>
        /// Gets the life.
        /// </summary>
        /// <value>
        /// The life.
        /// </value>
        public int Life
        {
            get
            {
                return lifeCount;
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static Player GetInstance()
        {
            if (instance == null)
            {
                instance = new Player();
            }

            return instance;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Player"/> class from being created.
        /// </summary>
        private Player()
        {
            bullets = new Queue<Bullet>();
            score = 0;
            lifeCount = 3;
            scoreRatio = 1;
            invulnerabilityStart = DateTime.MinValue;
            isInvulnerable = false;
        }

        /// <summary>
        /// Stores the bullet.
        /// </summary>
        /// <param name="_bullet">The _bullet.</param>
        public void StoreBullet(Bullet _bullet)
        {
            bullets.Enqueue(_bullet);
        }

        /// <summary>
        /// Initializes the specified _content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        /// <param name="_sprite">The _sprite.</param>
        /// <param name="_position">The _position.</param>
        public void Initialize(ContentManager _content, Sprite _sprite, Vector2 _position)
        {
            base.Initialize(_sprite, _position);
            content = _content;
        }

        /// <summary>
        /// Updates the specified screen.
        /// @see GetDimension
        /// </summary>
        /// <param name="screen">The screen.</param>
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

        /// <summary>
        /// Fixes the maximum velocity.
        /// </summary>
        /// <param name="_speed">The _speed.</param>
        private void FixMaximumVelocity(double _speed)
        {
            if (_speed > MAX_VELOCITY)
            {
                float ratio = (float)(MAX_VELOCITY / _speed);
                velocity.X *= ratio;
                velocity.Y *= ratio;
            }
        }

        /// <summary>
        /// Forces the player to stay inside the bounds of the screen.
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
                    position.X -= screenWidth;
                if (position.X <= 0)
                    position.X += screenWidth + sprite.GetDimension().X;
            }
        }

        // IMovable
        /// <summary>
        /// Adds the velocity.
        /// </summary>
        /// <param name="_speed">The _speed.</param>
        public void AddVelocity(float _speed)
        {
            if (_speed < 0)
                _speed /= 2;
            velocity += (new Vector2((float)Math.Cos(sprite.Rotation), (float)Math.Sin(sprite.Rotation)) * _speed);
        }

        /// <summary>
        /// Determines whether the specified _other has collided.
        /// </summary>
        /// <param name="_other">The _other.</param>
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

        /// <summary>
        /// Gets the speed.
        /// </summary>
        /// <returns></returns>
        private double GetSpeed()
        {
            return Math.Sqrt((double)(velocity.X * velocity.X + velocity.Y * velocity.Y));
        }

        /// <summary>
        /// Shoots this instance.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Adds the bonus.
        /// </summary>
        /// <param name="_type">The _type.</param>
        public override void AddBonus(Bonus.Type _type)
        {
            if (_type == Bonus.Type.SHRINK && !hasShrunk)
            {
                hasShrunk = true;
                sprite.Scale /= 2;
            }
            else if (_type == Bonus.Type.BIGGER_BULLETS)
            {
                for (int i = 0; i < MAX_NB_BULLETS; i++)
                {
                    Bullet bullet = bullets.Dequeue();
                    bullet.SpriteImage.Scale = 0.1f;
                    bullets.Enqueue(bullet);
                }
            }
            else if (_type == Bonus.Type.SCORE_TWICE)
            {
                scoreRatio = 2;
            }
        }

        /// <summary>
        /// Adds the score.
        /// </summary>
        /// <param name="_score">The _score.</param>
        public void AddScore(int _score)
        {
            score += _score * scoreRatio;
            if (score >= (lifeCount * 10000))
            {
                lifeCount++;
                if (lifeCount > 3)
                    lifeCount = 3;
            }
        }

        /// <summary>
        /// Draws the specified renderer.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
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

        /// <summary>
        /// Checks if player's dead.
        /// @see ResetAttributes
        /// </summary>
        private void CheckIfDead()
        {
            if (lifeCount == 0)
            {
                AsteroidGame.gameState = new SaveScoreMenu(score.ToString());
                AsteroidGame.gameState.LoadContent(content);
                ResetAttributes();
            }
        }

        /// <summary>
        /// Resets the attributes.
        /// </summary>
        private void ResetAttributes()
        {
            velocity = Vector2.Zero;
            score = 0;
            lifeCount = 3;
            scoreRatio = 1;
            hasShrunk = false;
        }
    }

}
