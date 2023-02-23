using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryWars.Controls
{
    class Button : Component
    {
        public event EventHandler Click;
        public bool clicked { get; private set; }
        public Vector2 position { get; set; }
        public Color fontColour { get; set; }
        public string text { get; set; }
        public Rectangle buttonRect { get { return new Rectangle((int)position.X, (int)position.Y, _texture.Width, _texture.Height); } }

        private Texture2D _texture;
        private MouseState _currentMouse;
        private MouseState _previousMouse;
        private SpriteFont _font;
        private bool _isHovering;

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            fontColour = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;
            if(_isHovering == true)
            {
                colour = Color.Gray;
            }

            spriteBatch.Draw(_texture, buttonRect, colour);

            float x = (buttonRect.X + (buttonRect.Width / 2)) - (_font.MeasureString(text).X / 2);
            float y = (buttonRect.Y + (buttonRect.Height / 2)) - (_font.MeasureString(text).Y / 2);

            spriteBatch.DrawString(_font, text, new Vector2(x, y), fontColour);
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            Rectangle mouseRect = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            _isHovering = false;
            if (mouseRect.Intersects(buttonRect))
            {
                _isHovering = true;
                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}