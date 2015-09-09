using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exercice5
{
    /// <summary>
    /// EnemyFactory is a static factory that creates enemies.
    /// </summary>
    public static class EnemyFactory
    {
        private static ContentManager content;

        /// <summary>
        /// Sets the content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        public static void SetContent(ContentManager _content)
        {
            content = _content;
        }

        /// <summary>
        /// Creates the enemy.
        /// @see Initialize
        /// </summary>
        /// <param name="_enemyType">Type of the _enemy.</param>
        /// <param name="_position">The _position.</param>
        /// <returns></returns>
        public static Enemy createEnemy(int _enemyType)
        {
            Enemy enemy = null;
            switch (_enemyType)
            {
                case 1:
                    enemy = new SmallEnemy();
                    enemy.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\smallEnemy"), 0.5f), new Vector2(0,200), new Sprite(content.Load<Texture2D>("Graphics\\ship"), 0.2f));
                    break;
                case 2:
                    enemy = new LargeEnemy();
                    enemy.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\largeEnemy"), 0.5f), new Vector2(0, 200), new Sprite(content.Load<Texture2D>("Graphics\\ship"), 0.2f));
                    break;
                case 3:
                    enemy = new SpecialEnemy();
                    enemy.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\specialEnemy"), 0.5f), new Vector2(0, 200), new Sprite(content.Load<Texture2D>("Graphics\\ship"), 0.2f));
                    break;
            }
            return enemy;
        }
    }
}
