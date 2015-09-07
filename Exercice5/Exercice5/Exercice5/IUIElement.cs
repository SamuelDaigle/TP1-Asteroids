using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public interface IUIElement : IDrawable
    {
        void Draw(SpriteBatch renderer);

        bool IsDrawn();
    }
}
