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

            foreach (Enemy enemy in drawableObjects.OfType<Enemy>())
            {
                bulletsToAdd.Add(enemy.chooseToAttack(drawableObjects));
            }
            foreach (Bullet bullet in bulletsToAdd)
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
                        asteroidCollision(collidableObject, other);
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

        private void asteroidCollision(ICollidable collidableObject, ICollidable _other)
        {
            if (collidableObject.GetType() == typeof(LargeAsteroid) || _other.GetType() == typeof(LargeAsteroid))
            {
                try
                {
                    createNewAsteroids((LargeAsteroid)collidableObject, _other);

                }
                catch
                {
                    createNewAsteroids((LargeAsteroid)_other, collidableObject);
                }
            }
            else if (collidableObject.GetType() == typeof(MediumAsteroid) || _other.GetType() == typeof(MediumAsteroid))
            {
                try
                {
                    createNewAsteroids((MediumAsteroid)collidableObject, _other);
                }
                catch
                {
                    createNewAsteroids((MediumAsteroid)_other, collidableObject);
                }
            }
            else if (collidableObject.GetType() == typeof(SmallAsteroid) || _other.GetType() == typeof(SmallAsteroid))
            {
                try
                {
                    createNewAsteroids((SmallAsteroid)collidableObject, _other);
                }
                catch
                {
                    createNewAsteroids((SmallAsteroid)_other, collidableObject);
                }
            }
        }

        private void createNewAsteroids(Asteroid asteroid, ICollidable other)
        {
            if (other.GetType() == typeof(Bullet))
            {
                float[] rotationsValue = CalculateNewRotations(asteroid, (Bullet)other);
                if (asteroid.GetType() == typeof(LargeAsteroid))
                {
                    asteroidsToCreate.Push(AsteroidFactory.createNewAsteroid(2, asteroid.Position + asteroid.Position / 10, rotationsValue[0] + asteroid.Rotation));
                    asteroidsToCreate.Push(AsteroidFactory.createNewAsteroid(2, asteroid.Position - asteroid.Position / 10, rotationsValue[1] + asteroid.Rotation));
                }
                else if (asteroid.GetType() == typeof(MediumAsteroid))
                {
                    asteroidsToCreate.Push(AsteroidFactory.createNewAsteroid(3, asteroid.Position, rotationsValue[0] + asteroid.Rotation));
                    asteroidsToCreate.Push(AsteroidFactory.createNewAsteroid(3, asteroid.Position, rotationsValue[1] + asteroid.Rotation));
                }
            }
            else if (other.GetType() == asteroid.GetType())
            {
                TimeSpan delayBeforeStacking = new TimeSpan(0, 0, 0, 3);
                if (DateTime.Now - asteroid.Birth >= delayBeforeStacking && DateTime.Now - ((Asteroid)other).Birth >= delayBeforeStacking)
                {
                    if (asteroid.GetType() == typeof(MediumAsteroid))
                    {
                        asteroidsToCreate.Push(AsteroidFactory.createNewAsteroid(1, asteroid.Position, RandomGenerator.GetRandomFloat(0, 6.283)));
                    }
                    else if (asteroid.GetType() == typeof(SmallAsteroid))
                    {
                        asteroidsToCreate.Push(AsteroidFactory.createNewAsteroid(2, asteroid.Position, RandomGenerator.GetRandomFloat(0, 6.283)));
                    }
                }
            }
        }

        private float[] CalculateNewRotations(Asteroid asteroid, Bullet bullet)
        {
            float[] returnedValues = new float[2] { 0, 0 };
            float PI = 3.1415f;

            float bulletRotation = bullet.Rotation % (2 * PI);
            float asteroidRotation = asteroid.Rotation % (2 * PI);

            if ((bulletRotation >= asteroidRotation - PI / 4) && (bulletRotation < asteroidRotation + PI / 4))
            {
                //Bullet comes from behind
                returnedValues[0] = PI / 4;
                returnedValues[1] = -PI / 4;
            }
            else if ((bulletRotation >= asteroidRotation - 5 * PI / 4) && (bulletRotation < asteroidRotation + 3 * PI / 4))
            {
                //Bullet comes from the front
                returnedValues[0] = PI / 2;
                returnedValues[1] = -PI / 2;
            }
            else if ((bulletRotation >= asteroidRotation + PI / 4) && (bulletRotation < asteroidRotation + 3 * PI / 4))
            {
                //Bullet comes from the right
                returnedValues[0] = -PI / 4;
                returnedValues[1] = 0;
            }
            else
            {
                //Bullet comes from the left
                returnedValues[0] = PI / 4;
                returnedValues[1] = 0;
            }
            return returnedValues;
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
