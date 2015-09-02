using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class Object2D : IDrawable, ICollidable
    {
        protected BoundingSphere collidingSphere;
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

        public virtual void Initialize(Sprite _sprite, Vector2 _position)
        {
            sprite = _sprite;
            position = _position;
            drawn = true;
            collidingSphere = new BoundingSphere(new Vector3(_position,0), _sprite.GetDimension().X / 2);
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

        public Vector2 GetPosition()
        {
            return position;
        }

        public bool IsDrawn()
        {
            return drawn;
        }
        public BoundingSphere GetCollision()
        {
            return collidingSphere;
        }

        public virtual void Terminate() 
        {
            drawn = false;
        }
     

    }
}
