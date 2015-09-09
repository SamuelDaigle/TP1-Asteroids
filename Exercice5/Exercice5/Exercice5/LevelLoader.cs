using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Exercice5
{
    /// <summary>
    /// Class that creates new asteroids according to the level.
    /// </summary>
    public class LevelLoader
    {
        private int level;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelLoader"/> class.
        /// </summary>
        /// <param name="_level">The _level.</param>
        public LevelLoader(int _level)
        {
            level = _level;
        }

        /// <summary>
        /// Gets the scene.
        /// @see AddObserver
        /// </summary>
        /// <returns></returns>
        public Scene GetScene()
        {
            Scene scene = new Scene();

            for (int i = 0; i < 3 + level; i++)
            {
                Asteroid asteroid = AsteroidFactory.createNewAsteroid(1, Vector2.Zero, RandomGenerator.GetRandomFloat(0f, 6.28f));
                asteroid.AddObserver(scene);
                scene.AddDrawableObject(asteroid);
            }
            return scene;
        }
    }
}
