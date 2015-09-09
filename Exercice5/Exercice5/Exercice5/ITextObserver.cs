using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exercice5
{
    /// <summary>
    /// Interface that enables other classes to display text
    /// </summary>
    public interface ITextObserver
    {
        /// <summary>
        /// Sets the text.
        /// </summary>
        /// <param name="_text">The _text.</param>
        void SetText(string _text);
    }
}
