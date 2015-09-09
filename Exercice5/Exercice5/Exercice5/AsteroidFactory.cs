using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exercice5
{
    /// <summary>
    /// AsteroidFactory is a static factory that creates asteroids.
    /// </summary>
    public static class AsteroidFactory
    {
        private static ContentManager content;

        /// <summary>
        /// Sets the content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        public static void SetContent(ContentManager _content)
        {
            content = _content;
        }

        /// <summary>
        /// Creates the new asteroid.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="_position">The _position.</param>
        /// <param name="_rotation">The _rotation.</param>
        /// <returns></returns>
        public static Asteroid createNewAsteroid(int size, Vector2 _position, float _rotation)
        {
            Asteroid asteroid;
            float scale = 0.5f;

            switch (size)
            {
                case 1:
                    asteroid = new LargeAsteroid();
                    asteroid.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\largeAsteroid"), scale), _position);
                    break;
                case 2:
                    asteroid = new MediumAsteroid();
                    scale = 0.4f;
                    asteroid.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\mediumAsteroid"), scale), _position);
                    break;
                case 3:
                    asteroid = new SmallAsteroid();
                    scale = 0.3f;
                    asteroid.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\smallAsteroid"), scale), _position);
                    break;
                default:
                    asteroid = null;
                    break;
            }
            if (asteroid != null)
            {
                asteroid.Rotate(_rotation);
                asteroid.AddVelocity(4f);
            }
            return asteroid;
        }  
    }
}
