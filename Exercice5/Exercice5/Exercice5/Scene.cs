using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Exercice5
{
    /// <summary>
    /// Class that contains every object to be
    /// displayed on the screen. Also checks
    /// the collisions of the different objects.
    /// </summary>
    public class Scene : IObjectAdderObserver
    {
        private ContentManager content;
        private List<Object2D> drawableObjects = new List<Object2D>();
        private UIContainer uiContainer;

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public ContentManager Content
        {
            get
            {
                return content;
            }
        }

        /// <summary>
        /// Initializes the specified _ui container.
        /// </summary>
        /// <param name="_uiContainer">The _ui container.</param>
        /// <param name="_content">The _content.</param>
        public void Initialize(UIContainer _uiContainer, ContentManager _content)
        {
            uiContainer = _uiContainer;
            content = _content;
        }

        /// <summary>
        /// Adds the drawable object.
        /// </summary>
        /// <param name="drawableObject">The drawable object.</param>
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

        /// <summary>
        /// Gets all asteroids.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Asteroid> GetAllAsteroids()
        {
            return drawableObjects.OfType<Asteroid>();
        }

        /// <summary>
        /// Updates the specified screen.
        /// @see CheckIfDeleted
        /// @see CheckCollision
        /// </summary>
        /// <param name="screen">The screen.</param>
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

        /// <summary>
        /// Checks the collision.
        /// </summary>
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

        /// <summary>
        /// Checks if deleted.
        /// </summary>
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

        /// <summary>
        /// Draws the specified renderer.
        /// </summary>
        /// <param name="renderer">The renderer.</param>
        public void Draw(SpriteBatch renderer)
        {
            foreach (IDrawable drawable in drawableObjects.OfType<IDrawable>())
            {
                drawable.Draw(renderer);
            }
            uiContainer.Draw(renderer);
        }

        /// <summary>
        /// Onlies the has player.
        /// </summary>
        /// <returns></returns>
        public bool onlyHasPlayer()
        {
            return (drawableObjects.Count == 1 && drawableObjects.First<Object2D>().GetType() == typeof(Player));
        }

        /// <summary>
        /// Adds the asteroid.
        /// </summary>
        /// <param name="_object">The _object.</param>
        public void AddAsteroid(Asteroid _object)
        {
            _object.AddObserver(this);
            AddDrawableObject(_object);
        }
    }
}
