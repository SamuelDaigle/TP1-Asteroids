using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class Scene
    {
        private List<Object2D> drawableObjects = new List<Object2D>();
        Stack<Asteroid> asteroidsToCreate = new Stack<Asteroid>();

        public void AddDrawableObject(Object2D drawableObject)
        {
            bool found = false;
            foreach (Object2D drawable in drawableObjects)
            {
                if (drawable.Equals(drawableObject))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
                drawableObjects.Add(drawableObject);
        }

        public void Update(BoundingBox screen)
        {
            foreach (IMovable sprite in drawableObjects.OfType<IMovable>())
            {
                sprite.Update(screen);
            }

            List<Bullet> bulletsToAdd = new List<Bullet>();

            foreach(Enemy enemy in drawableObjects.OfType<Enemy>())
            {
                bulletsToAdd.Add(enemy.chooseToAttack(drawableObjects));
            }
            foreach(Bullet bullet in bulletsToAdd)
            {
                if (bullet != null)
                {
                    AddDrawableObject(bullet);

                }
            }

            CheckIfDeleted();
            CheckCollision();


        }

        private void CheckCollision()
        {
            ICollidable collidableObject;
            ICollidable other;
            for (int i = 0; i < drawableObjects.Count; i++)
            {
                collidableObject = drawableObjects.ElementAt(i);
                for (int j = i + 1; j < drawableObjects.Count; j++)
                {
                    other = drawableObjects.ElementAt(j);
                    if (collidableObject.GetCollision().Intersects(other.GetCollision()))
                    {
                        if (collidableObject.GetType() == typeof(LargeAsteroid) || other.GetType() == typeof(LargeAsteroid))
                        {
                            try
                            {
                                createNewAsteroids((Asteroid)collidableObject, other);

                            }
                            catch
                            {
                                createNewAsteroids((Asteroid)other, collidableObject);
                            }
                        }
                        else if (collidableObject.GetType() == typeof(MediumAsteroid) || other.GetType() == typeof(MediumAsteroid))
                        {
                            try
                            {
                                createNewAsteroids((Asteroid)collidableObject, other);

                            }
                            catch
                            {
                                createNewAsteroids((Asteroid)other, collidableObject);
                            }
                        }
                        collidableObject.HasCollided(other);
                        other.HasCollided(collidableObject);
                    }
                }
            }
            foreach (Asteroid asteroid in asteroidsToCreate)
            {
                this.AddDrawableObject(asteroid);
            }
            asteroidsToCreate.Clear();
        }

        private void createNewAsteroids(Asteroid asteroid, ICollidable other)
        {
            if (other.GetType() == typeof(Bullet))
            {
                if (asteroid.GetType() == typeof(LargeAsteroid))
                {
                    asteroidsToCreate.Push(AsteroidFactory.createNewAsteroid(2, asteroid.Position));
                    asteroidsToCreate.Push(AsteroidFactory.createNewAsteroid(2, asteroid.Position));
                }
                else if (asteroid.GetType() == typeof(MediumAsteroid))
                {
                    asteroidsToCreate.Push(AsteroidFactory.createNewAsteroid(3, asteroid.Position));
                    asteroidsToCreate.Push(AsteroidFactory.createNewAsteroid(3, asteroid.Position));
                }
            }
        }

        private void CheckIfDeleted()
        {
            for (int i = 0; i < drawableObjects.Count; i++)
            {
                if (!drawableObjects.ElementAt(i).IsDrawn())
                {
                    drawableObjects.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch renderer)
        {
            foreach (IDrawable drawable in drawableObjects.OfType<IDrawable>())
            {
                drawable.Draw(renderer);
            }

        }
    }
}
