using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class UIContainer
    {
        List<IUIElement> uiElements;

        public UIContainer()
        {
            uiElements = new List<IUIElement>();
        }

        public void AddElement(IUIElement _element)
        {
            uiElements.Add(_element);
        }

        public void Draw(SpriteBatch renderer)
        {
            foreach (IUIElement ui in uiElements)
            {
                ui.Draw(renderer);
            }
        }
    }
}
