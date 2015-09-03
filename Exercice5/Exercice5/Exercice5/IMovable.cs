using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Exercice5
{
    public interface IMovable
    {
        void AddVelocity(float _speed);

        void Update(BoundingBox screen);
    }
}
