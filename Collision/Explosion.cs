using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryWars.Collision
{
    public class Explosion
    {
        public Texture2D explosionTexture;
        public Vector2 position;
        public Rectangle[] explosionRects;
        //public byte previousAnimationIndex;
        public byte currentAnimationIndex;
        public float timer;

        public Explosion(Texture2D explosionSheet, Vector2 enemyPosition)
        {
            explosionTexture = explosionSheet;
            position = enemyPosition;

            explosionRects = new Rectangle[7];
            explosionRects[0] = new Rectangle(0, 0, 36, 37);
            explosionRects[1] = new Rectangle(34, 0, 36, 37);
            explosionRects[2] = new Rectangle(68, 0, 36, 37);
            explosionRects[3] = new Rectangle(102, 0, 36, 37);
            explosionRects[4] = new Rectangle(136, 0, 36, 37);
            explosionRects[5] = new Rectangle(170, 0, 36, 37);
            explosionRects[6] = new Rectangle(204, 0, 36, 37);
            // This tells the animation to start on the left-side sprite.
            //previousAnimationIndex = 1;
            currentAnimationIndex = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(explosionTexture, position, explosionRects[currentAnimationIndex], Color.White);
            spriteBatch.End();
        }
    }
}
