using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public interface IDrawable
    {
        void Draw(SpriteBatch renderer);

        Vector2 GetPosition();

        Vector2 GetDimension();

        bool IsDrawn();

        void Rotate(float angle);
    }
}
