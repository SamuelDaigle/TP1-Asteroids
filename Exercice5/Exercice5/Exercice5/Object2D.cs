using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class Object2D : IDrawable
    {
        protected Vector2 position;
        protected Sprite sprite;
        protected bool drawn = false;

        public void Initialize(Sprite _sprite, Vector2 _position)
        {
            sprite = _sprite;
            position = _position;
            drawn = true;
        }

        public void Draw(SpriteBatch renderer)
        {
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

     

    }
}
