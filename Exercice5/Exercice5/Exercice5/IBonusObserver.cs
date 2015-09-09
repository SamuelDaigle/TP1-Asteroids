using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    /// <summary>
    /// Interface that enables another class to have bonuses.
    /// </summary>
    public interface IBonusObserver
    {
        /// <summary>
        /// Adds the bonus.
        /// </summary>
        /// <param name="_type">The _type.</param>
        void AddBonus(Bonus.Type _type);
    }
}
