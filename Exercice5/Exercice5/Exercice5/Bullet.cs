using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Exercice5
{
    /// <summary>
    /// Bullets are the primary attack of the player and
    /// of some enemies. A bullet knows who has shot it
    /// so it won't collide with his shooter.
    /// </summary>
    public class Bullet : Object2D, IMovable
    {
        private List<IScoreObserver> scoreObservers;
        private Vector2 velocity;
        private DateTime birth;
        private TimeSpan lifeTime;
        private Object2D shooter;


        /// <summary>
        /// Gets or sets the shooter.
        /// </summary>
        /// <value>
        /// The shooter.
        /// </value>
        public Object2D Shooter
        {
            get
            {
                return shooter;
            }
            set 
            {
                shooter = value;
            }
        }
        /// <summary>
        /// Sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector2 Position
        {
            set
            {
                position = value;
            }
        }

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        public float Rotation
        {
            get
            {
                return sprite.Rotation;
            }
            set
            {
                sprite.Rotation = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Bullet"/> class.
        /// </summary>
        public Bullet()
        {
            scoreObservers = new List<IScoreObserver>();
            lifeTime = new TimeSpan(0, 0, 1);
        }

        /// <summary>
        /// Adds the score observer.
        /// </summary>
        /// <param name="_observer">The _observer.</param>
        public void AddScoreObserver(IScoreObserver _observer)
        {
            scoreObservers.Add(_observer);
        }

        // IMovable
        /// <summary>
        /// Updates the specified screen.
        /// </summary>
        /// <param name="screen">The screen.</param>
        public void Update(BoundingBox screen)
        {
            if (DateTime.Now - birth >= lifeTime)
            {
                drawn = false;
            }

            position.X += (int)(velocity.X);
            position.Y += (int)(velocity.Y);

            collisionSphere.Radius = GetDimension().X / 2;
            collisionSphere.Center.X = position.X + GetDimension().X / 2;
            collisionSphere.Center.Y = position.Y + GetDimension().Y / 2;

            StayInBounds(screen);
        }

        /// <summary>
        /// Forces the bullets to stay inside the screen's bounds.
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

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset()
        {
            velocity.X = 0;
            velocity.Y = 0;
            drawn = true;
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
        /// Determines whether the specified _other has collided.
        /// </summary>
        /// <param name="_other">The _other.</param>
        public override void HasCollided(ICollidable _other)
        {
            if (_other != shooter && _other.GetType() != typeof(Bullet))
            {
                drawn = false;
            }

            CheckScore(_other);
        }

        /// <summary>
        /// Checks the score.
        /// @see NotifyScoreObserver
        /// </summary>
        /// <param name="_other">The _other.</param>
        private void CheckScore(ICollidable _other)
        {
            if (_other.GetType() == typeof(Bonus))
            {
                NotifyScoreObserver(100);
            }
            if (_other.GetType() == typeof(LargeAsteroid))
            {
                NotifyScoreObserver(500);
            }
            if (_other.GetType() == typeof(MediumAsteroid))
            {
                NotifyScoreObserver(350);
            }
            if (_other.GetType() == typeof(SmallAsteroid))
            {
                NotifyScoreObserver(200);
            }
            if (_other.GetType() == typeof(SpecialEnemy))
            {
                NotifyScoreObserver(1500);
            }
            if (_other.GetType() == typeof(SmallEnemy))
            {
                NotifyScoreObserver(750);
            }
            if (_other.GetType() == typeof(LargeEnemy))
            {
                NotifyScoreObserver(1000);
            }
        }

        /// <summary>
        /// Notifies the score observer.
        /// </summary>
        /// <param name="_score">The _score.</param>
        private void NotifyScoreObserver(int _score)
        {
            foreach (IScoreObserver observer in scoreObservers)
            {
                observer.AddScore(_score);
            }
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void StartTimer()
        {
            birth = DateTime.Now;
        }
    }
}
