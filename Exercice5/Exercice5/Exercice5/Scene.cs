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

            CheckCollision();
        }

        private void CheckCollision()
        {
            foreach (ICollidable collidableObject in drawableObjects.OfType<ICollidable>())
            {
                foreach (ICollidable other in drawableObjects.OfType<ICollidable>())
                {
                    if (!collidableObject.Equals(other))
                    {
                        if (collidableObject.GetCollision().Intersects(other.GetCollision()))
                        {
                            collidableObject.Terminate();
                            other.Terminate();
                        }
                    }
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
