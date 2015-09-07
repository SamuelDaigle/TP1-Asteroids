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
        List<UIElement> uiElements;

        public UIContainer()
        {
            uiElements = new List<UIElement>();
        }

        public void AddElement(UIElement _element)
        {
            uiElements.Add(_element);
        }

        public void Draw(SpriteBatch renderer)
        {
            foreach (UIElement ui in uiElements)
            {
                ui.Draw(renderer);
            }
        }
    }
}
