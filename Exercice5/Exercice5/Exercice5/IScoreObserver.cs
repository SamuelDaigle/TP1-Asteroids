using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    public interface IScoreObserver
    {
        void AddScore(int _score);
    }
}
