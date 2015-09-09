using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    /// <summary>
    /// Static class that generates a random number
    /// </summary>
    public static class RandomGenerator
    {
        private static Random random = new Random();

        /// <summary>
        /// Returns a random float.
        /// @see NextDouble
        /// </summary>
        /// <param name="_min">The _min.</param>
        /// <param name="_max">The _max.</param>
        /// <returns></returns>
        public static float GetRandomFloat(double _min, double _max)
        {
            return (float)(random.NextDouble() * (_max - _min) + _min);
        }

        /// <summary>
        /// Returns a random int.
        /// @see Next
        /// </summary>
        /// <param name="_min">The _min.</param>
        /// <param name="_max">The _max.</param>
        /// <returns></returns>
        public static int GetRandomInt(int _min, int _max)
        {
            return random.Next(_min, _max);
        }
    }
}
