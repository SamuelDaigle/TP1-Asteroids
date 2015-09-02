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
        protected State state;

        public void Initialize(State _state, Vector2 _position)
        {
            state = _state;
            position = _position;
        }

        public void Draw(SpriteBatch renderer)
        {
            state.Draw(renderer, position);
        }

        public Vector2 GetDimension()
        {
            return state.GetSprite().GetDimension();
        }

        public void Rotate(float angle)
        {
            state.GetSprite().Rotate(angle);
        }

        public Vector2 GetPosition()
        {
            return position;
        }    
     

    }
}
