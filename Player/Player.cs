using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryWars.Player
{
    public class Player
    {
        public Texture2D tex;
        public Vector2 playerPosition;
        public Rectangle playerRect;
        public float rotation;
        public float scale = .75f;
        public Vector2 origin;

        public Player(Texture2D playerTex)
        {
            tex = playerTex;
            playerPosition = new Vector2(772, 422);
            origin = new Vector2(200, 200);
            playerRect = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 75, 75);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, playerPosition, playerRect, Color.White, rotation, origin, scale,
                SpriteEffects.None, 0.1f);
        }
    }
}
