using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class Object2D : IDrawable, ICollidable, IBonusObserver
    {
        protected List<IBonusObserver> bonusObservers;
        protected BoundingSphere collisionSphere;
        protected Vector2 position;
        protected Sprite sprite;
        protected bool drawn = false;

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public float Rotation
        {
            get
            {
                return sprite.Rotation;
            }
        }

        public Sprite SpriteImage
        {
            get
            {
                return sprite;
            }
        }

        public void AddBonusObserver(IBonusObserver _observers)
        {
            bonusObservers.Add(_observers);
        }

        public virtual void Initialize(Sprite _sprite, Vector2 _position)
        {
            sprite = _sprite;
            position = _position;
            drawn = true;
            collisionSphere = new BoundingSphere(new Vector3(_position, 0), _sprite.GetDimension().X / 2);
            bonusObservers = new List<IBonusObserver>();
        }

        public virtual void Draw(SpriteBatch renderer)
        {
            if (IsDrawn())
                sprite.Draw(renderer, position);
        }

        public Vector2 GetDimension()
        {
            return sprite.GetDimension();
        }

        public void Rotate(float angle)
        {
            sprite.Rotate(angle);
        }

        public bool IsDrawn()
        {
            return drawn;
        }

        public virtual BoundingSphere GetCollision()
        {
            if (!drawn)
            {
                return new BoundingSphere(new Vector3(-100, -100, -1), 1);
            }
            return collisionSphere;
        }

        public virtual void HasCollided(ICollidable _other)
        {
            if (_other.GetType() != typeof(Bonus))
            {
                drawn = false;
                position.X = 0;
                position.Y = 0;
            }
        }

        public virtual void AddBonus(Bonus.Type _type)
        {
            if (_type == Bonus.Type.SHRINK)
            {
                sprite.Scale /= 2;
            }
        }
    }
}
