using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class Sprite
    {
        protected Texture2D image;
        protected float rotation;
        protected float scale;
        
        public float Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
            }
        }

        public Sprite(Texture2D _image, float _scale = 1.0f, float _rotation = 0.0f)
        {
            image = _image;
            scale = _scale;
            rotation = _rotation;
        }

        public void Rotate(float degrees)
        {
            rotation += degrees;
        }

        public void Draw(SpriteBatch renderer, Vector2 position)
        {
            renderer.Draw(image, new Vector2(position.X, position.Y), null, Color.White, rotation, new Vector2(image.Width/2, image.Height/2), scale, SpriteEffects.None, 0);
        }

        public Vector2 GetDimension()
        {
            return new Vector2(image.Width * scale, image.Height * scale);
        }
    }
}
