using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Exercice5
{
    public class Scene
    {
        private List<Object2D> drawableObjects = new List<Object2D>();

        public void AddDrawableObject(Object2D drawableObject)
        {
            drawableObjects.Add(drawableObject);
        }

        public void UpdateAll(BoundingBox screen)
        {
            foreach (IMovable sprite in drawableObjects.OfType<IMovable>())
            {
                sprite.UpdateMovement();
                sprite.StayInBounds(screen);
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
                            collidableObject.HasCollided();
                            other.HasCollided();
                        }
                    }
                }
            }
        }

        public void RenderAll(SpriteBatch renderer)
        {
            foreach (IDrawable drawable in drawableObjects.OfType<IDrawable>())
            {
                if(drawable.IsDrawn())
                    drawable.Draw(renderer);
            }
        }
    }
}
