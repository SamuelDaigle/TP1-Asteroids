using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    public class Scene : IObjectAdderObserver
    {
        private ContentManager content;
        private List<Object2D> drawableObjects = new List<Object2D>();
        private UIContainer uiContainer;

        public ContentManager Content
        {
            get
            {
                return content;
            }
        }

        public void Initialize(UIContainer _uiContainer, ContentManager _content)
        {
            uiContainer = _uiContainer;
            content = _content;
        }

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

        public IEnumerable<Asteroid> GetAllAsteroids()
        {
            return drawableObjects.OfType<Asteroid>();
        }

        public void Update(BoundingBox screen)
        {
            uiContainer.Update();

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
                        collidableObject.HasCollided(other);
                        other.HasCollided(collidableObject);
                    }
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
            uiContainer.Draw(renderer);
        }

        public bool onlyHasPlayer()
        {
            return (drawableObjects.Count == 1 && drawableObjects.First<Object2D>().GetType() == typeof(Player));
        }

        public void AddAsteroid(Asteroid _object)
        {
            _object.AddObserver(this);
            AddDrawableObject(_object);
        }
    }
}
