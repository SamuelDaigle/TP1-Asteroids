using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Class that defines what image will be displayed
    /// to the screen.
    /// </summary>
    public class Sprite
    {
        protected Texture2D image;
        protected float rotation;
        protected float scale;

        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
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

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>
        /// The scale.
        /// </value>
        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="_image">The _image.</param>
        /// <param name="_scale">The _scale.</param>
        /// <param name="_rotation">The _rotation.</param>
        public Sprite(Texture2D _image, float _scale = 1.0f, float _rotation = 0.0f)
        {
            image = _image;
            scale = _scale;
            rotation = _rotation;
        }

        /// <summary>
        /// Rotates the specified degrees.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        public void Rotate(float degrees)
        {
            rotation += degrees;
        }

        /// <summary>
        /// Draws the specified renderer.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        /// <param name="position">The position.</param>
        public void Draw(SpriteBatch renderer, Vector2 position)
        {
            renderer.Draw(image, new Vector2(position.X, position.Y), null, Color.White, rotation, new Vector2(image.Width/2, image.Height/2), scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Gets the dimension.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetDimension()
        {
            return new Vector2(image.Width * scale, image.Height * scale);
        }
    }
}
