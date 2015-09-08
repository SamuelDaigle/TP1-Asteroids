using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class UIText : IUIElement, ITextObserver
    {
        private SpriteFont spriteFont;
        private string text;
        private Vector2 position;
        private bool drawn = true;

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public void Initialize(SpriteFont _spriteFont, string _text, Vector2 _position)
        {
            spriteFont = _spriteFont;
            text = _text;
            position = _position;
        }

        public void Draw(SpriteBatch renderer)
        {
            renderer.DrawString(spriteFont, text, position, Color.White);
        }

        public Vector2 GetDimension()
        {
            return Vector2.Zero;
        }

        public bool IsDrawn()
        {
            return drawn;
        }

        public void Rotate(float angle)
        {
            
        }

        public void SetText(string _text)
        {
            text = _text;
        }
    }
}
