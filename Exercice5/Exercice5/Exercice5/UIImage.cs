using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class UIImage : IUIElement
    {
        private Sprite sprite;
        private Vector2 position;
        private bool drawn = true;

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public void Initialize(Sprite _sprite, Vector2 _position)
        {
            sprite = _sprite;
            position = _position;
        }

        public void Draw(SpriteBatch renderer)
        {
            sprite.Draw(renderer, position);
        }

        public Vector2 GetDimension()
        {
            return sprite.GetDimension();
        }

        public bool IsDrawn()
        {
            return drawn;
        }

        public void Rotate(float angle)
        {
            sprite.Rotate(angle);
        }
    }
}
