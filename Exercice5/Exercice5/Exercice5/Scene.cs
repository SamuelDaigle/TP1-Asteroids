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
        private List<Object2D> sprites = new List<Object2D>();
        private ContentManager content;

        public Scene(ContentManager _content)
        {
            content = _content;
        }

        public void AddDrawableObject(Object2D sprite)
        {
            sprites.Add(sprite);
        }

        public void UpdateAll(BoundingBox screen)
        {
            foreach (IMovable sprite in sprites.OfType<IMovable>())
            {
                sprite.UpdateMovement();
                sprite.StayInBounds(screen);
            }

            CheckCollision();
        }

        private void CheckCollision()
        {
            foreach (ICollidable collidableObject in sprites.OfType<ICollidable>())
            {
                foreach (ICollidable other in sprites.OfType<ICollidable>())
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
            foreach (IDrawable drawable in sprites.OfType<IDrawable>())
            {
                drawable.Draw(renderer);
            }
        }
    }
}
