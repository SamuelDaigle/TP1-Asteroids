using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    /// <summary>
    /// Interface that enables a class to generate score.
    /// </summary>
    public interface IScoreObserver
    {
        /// <summary>
        /// Adds the score.
        /// </summary>
        /// <param name="_score">The _score.</param>
        void AddScore(int _score);
    }
}
