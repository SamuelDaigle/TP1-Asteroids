using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    public static class RandomGenerator
    {
        public static float GetRandom(double _min, double _max)
        {
            Random random = new Random();
            return (float)(random.NextDouble() * (_max - _min) + _min);
        }
    }
}
