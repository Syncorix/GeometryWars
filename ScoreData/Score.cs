using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryWars.ScoreData
{
    public class Score
    {
        public string currentScore;
        public Vector2 scorePosition;
        public Rectangle scoreRect { get { return new Rectangle((int)scorePosition.X, (int)scorePosition.Y, _texture.Width, _texture.Height); } }
        public SpriteFont _font;
        private Texture2D _texture;


        public Score(string score, Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            currentScore = score;
            scorePosition = new Vector2(1425, 25);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, scoreRect, Color.White);

            float x = (scoreRect.X + (scoreRect.Width / 2)) - (_font.MeasureString(currentScore).X / 2);
            float y = (scoreRect.Y + (scoreRect.Height / 2)) - (_font.MeasureString(currentScore).Y / 2);

            spriteBatch.DrawString(_font, currentScore, new Vector2(x, y), Color.White);
        }
    }
}
