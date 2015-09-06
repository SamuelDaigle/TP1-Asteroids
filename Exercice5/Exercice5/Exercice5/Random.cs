using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    public static class RandomGenerator
    {
        public static float GetRandomFloat(double _min, double _max)
        {
            Random random = new Random();
            return (float)(random.NextDouble() * (_max - _min) + _min);
        }

        public static int GetRandomInt(int _min, int _max)
        {
            Random random = new Random();
            return random.Next(_min, _max);
        }
    }
}
