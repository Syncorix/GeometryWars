using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryWars.ScoreData
{
    public class Textbox
    {
        public string text = "";
        public Vector2 inputPosition;
        public Rectangle inputRect { get { return new Rectangle((int)inputPosition.X, (int)inputPosition.Y, _texture.Width, _texture.Height); } }
        public SpriteFont _font;
        private Texture2D _texture;


        public Textbox( Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            inputPosition = new Vector2(700, 25);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, inputRect, Color.White);

            float x = (inputRect.X + (inputRect.Width / 2)) - (_font.MeasureString(text).X / 2);
            float y = (inputRect.Y + (inputRect.Height / 2)) - (_font.MeasureString(text).Y / 2);

            spriteBatch.DrawString(_font, text, new Vector2(x, y), Color.White);
            spriteBatch.End();
        }
    }
}
