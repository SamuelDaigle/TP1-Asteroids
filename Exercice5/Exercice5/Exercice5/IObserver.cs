using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    public interface IBonusObserver
    {
        void AddBonus(Bonus.Type _type);
    }
}
