using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryWars.Enemy
{
    public class Square
    {
        public Texture2D enemyTexture;
        public Vector2 enemyPosition;
        public Vector2 velocity;
        public Rectangle enemyRect;
        public float scale = .5f;
        public bool isVisible;

        public Square(Texture2D enemyTex)
        {
            enemyTexture = enemyTex;
            enemyRect = new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, enemyTexture.Width, enemyTexture.Height);
            isVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(enemyTexture, enemyPosition, Color.White);
            spriteBatch.End();
        }
    }
}
