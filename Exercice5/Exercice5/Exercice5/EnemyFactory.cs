using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Exercice5
{
    public static class EnemyFactory
    {
        private static ContentManager content;

        public static void SetContent(ContentManager _content)
        {
            content = _content;
        }

        public static Enemy createEnemy(int _enemyType, Vector2 _position)
        {
            Enemy enemy = new SmallSpaceship();
            enemy.Initialize(new Sprite(content.Load<Texture2D>("Graphics\\asteroid"), 1f), _position);
            return enemy;
        }
    }
}
