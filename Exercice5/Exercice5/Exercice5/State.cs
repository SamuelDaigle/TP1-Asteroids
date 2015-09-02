using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public abstract class State
    {
        private Sprite sprite;

        public State(Sprite _sprite)
        {
            sprite = _sprite;
        }

        public void Draw(SpriteBatch renderer, Vector2 position)
        {
            sprite.Draw(renderer, position);
        }

        public Sprite GetSprite()
        {
            return sprite;
        }

        public abstract void Update();
    }
}
