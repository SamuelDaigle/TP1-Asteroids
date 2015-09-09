using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Object2D defines the base of every object of the game.
    /// Almost every object of the game inherits Object2D.
    /// </summary>
    public class Object2D : IDrawable, ICollidable, IBonusObserver
    {
        protected List<IBonusObserver> bonusObservers;
        protected BoundingSphere collisionSphere;
        protected Vector2 position;
        protected Sprite sprite;
        protected bool drawn = false;

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        /// <summary>
        /// Gets the rotation.
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
        }

        /// <summary>
        /// Gets the sprite image.
        /// </summary>
        /// <value>
        /// The sprite image.
        /// </value>
        public Sprite SpriteImage
        {
            get
            {
                return sprite;
            }
        }

        /// <summary>
        /// Adds the bonus observer.
        /// </summary>
        /// <param name="_observers">The _observers.</param>
        public void AddBonusObserver(IBonusObserver _observers)
        {
            bonusObservers.Add(_observers);
        }

        /// <summary>
        /// Initializes the specified _sprite.
        /// </summary>
        /// <param name="_sprite">The _sprite.</param>
        /// <param name="_position">The _position.</param>
        public virtual void Initialize(Sprite _sprite, Vector2 _position)
        {
            sprite = _sprite;
            position = _position;
            drawn = true;
            collisionSphere = new BoundingSphere(new Vector3(_position, 0), _sprite.GetDimension().X / 2);
            bonusObservers = new List<IBonusObserver>();
        }

        /// <summary>
        /// Draws the specified renderer.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        public virtual void Draw(SpriteBatch renderer)
        {
            if (IsDrawn())
                sprite.Draw(renderer, position);
        }

        /// <summary>
        /// Gets the dimension.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetDimension()
        {
            return sprite.GetDimension();
        }

        /// <summary>
        /// Rotates the specified angle.
        /// </summary>
        /// <param name="angle">The angle.</param>
        public void Rotate(float angle)
        {
            sprite.Rotate(angle);
        }

        /// <summary>
        /// Determines whether this instance is drawn.
        /// </summary>
        /// <returns></returns>
        public bool IsDrawn()
        {
            return drawn;
        }

        /// <summary>
        /// Gets the collision.
        /// </summary>
        /// <returns></returns>
        public virtual BoundingSphere GetCollision()
        {
            if (!drawn)
            {
                return new BoundingSphere(new Vector3(-100, -100, -1), 1);
            }
            return collisionSphere;
        }

        /// <summary>
        /// Determines whether the specified _other has collided.
        /// </summary>
        /// <param name="_other">The _other.</param>
        public virtual void HasCollided(ICollidable _other)
        {
            if (_other.GetType() != typeof(Bonus))
            {
                drawn = false;
                position.X = 0;
                position.Y = 0;
            }
        }

        /// <summary>
        /// Adds the bonus.
        /// </summary>
        /// <param name="_type">The _type.</param>
        public virtual void AddBonus(Bonus.Type _type) {}
    }
}
