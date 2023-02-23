using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using GeometryWars.Controls;
using System.Text;

namespace GeometryWars.States
{
    public class HelpState : State
    {
        private Texture2D backgroundTexture;
        private Rectangle backgroundRect;
        private List<Component> components;
        private SpriteFont font;


        public HelpState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            backgroundTexture = content.Load<Texture2D>("images/Background");
            backgroundRect = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
            font = _content.Load<SpriteFont>("Fonts/ScoreFont");

            Texture2D buttonTexture = _content.Load<Texture2D>("Controls/Button");
            SpriteFont buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            Button backButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 700),
                text = "Back",
            };
            backButton.Click += BackButton_Click;

            components = new List<Component>()
            {
                backButton,
            };
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);

            Vector2 position = new Vector2(450, 250);
            spriteBatch.DrawString(font, "The goal of this game is to get the highest score possible.", position, Color.White);

            position = new Vector2(700, 300);
            spriteBatch.DrawString(font, "W - Move up.\nS - Move down.\nA - Move left.\nD - Move right.\nE - Activate shield.\nSpace - Shoot.\nEsc - Pause Game.", position, Color.White);


            foreach (Button component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(Button component in components)
            {
                component.Update(gameTime);
            }
        }
    }
}
