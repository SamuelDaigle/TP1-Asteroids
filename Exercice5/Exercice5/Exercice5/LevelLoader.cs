using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Exercice5
{
    public class LevelLoader
    {
        private int level;

        public LevelLoader(int _level)
        {
            level = _level;
        }

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
