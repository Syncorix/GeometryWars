using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GeometryWars.Player
{
    public class Bullet
    {
        public Texture2D bulletTexture;
        public Vector2 bulletPosition;
        public Vector2 velocity;
        public Rectangle bulletRect;
        public bool isVisible;
        public float scale = .5f;
        public float rotate = 0f;

        public Bullet(Texture2D bulletTex)
        {
            bulletTexture = bulletTex;
            scale = .5f;
            bulletRect = new Rectangle((int)bulletPosition.X, (int)bulletPosition.Y, bulletTexture.Width, bulletTexture.Height);
            isVisible = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bulletTexture, bulletPosition, Color.White);
        }
    }
}
