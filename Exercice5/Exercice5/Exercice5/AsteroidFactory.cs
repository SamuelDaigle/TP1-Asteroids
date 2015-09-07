using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exercice5
{
    public static class AsteroidFactory
    {
        private static ContentManager content;

        public static void SetContent(ContentManager _content)
        {
            content = _content;
        }

        public static Asteroid createNewAsteroid(int size, Vector2 _position)
        {
            Asteroid asteroid;
            float scale = 1f;

            switch (size)
            {
                case 1:
                    asteroid = new LargeAsteroid();
                    break;
                case 2:
                    asteroid = new MediumAsteroid();
                    scale = 0.6f;
                    break;
                case 3:
                    asteroid = new SmallAsteroid();
                    scale = 0.3f;
                    break;
                default:
                    asteroid = null;
                    break;
            }
            if (asteroid != null)
            {
                asteroid.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\asteroid"), scale), _position);
                asteroid.Rotate(RandomGenerator.GetRandomFloat(0, 3.1415));
                asteroid.AddVelocity(4f);
            }
            return asteroid;
        }

       
    }
}
