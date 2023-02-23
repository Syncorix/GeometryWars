using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using GeometryWars.Controls;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace GeometryWars.States
{
    public class MenuState : State
    {
        private Texture2D backgroundTexture;
        private Rectangle backgroundRect;
        private List<Component> components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            backgroundTexture = content.Load<Texture2D>("images/MainBackground");
            backgroundRect = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);
            Texture2D buttonTexture = _content.Load<Texture2D>("Controls/Button");
            SpriteFont buttonFont = _content.Load<SpriteFont>("Fonts/Font");


            Button startGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 300),
                text = "Start Game",
            };
            startGameButton.Click += StartButton_Click;

            Button highscoreButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 375),
                text = "Highscores",
            };
            highscoreButton.Click += HighscoreButton_Click;

            Button aboutButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 450),
                text = "About",
            };
            aboutButton.Click += AboutButton_Click;

            Button helpButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 525),
                text = "Help",
            };
            helpButton.Click += HelpButton_Click;

            Button exitGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 600),
                text = "Exit Game",
            };
            exitGameButton.Click += ExitGameButton_Click;

            components = new List<Component>()
            {
                startGameButton,
                highscoreButton,
                aboutButton,
                helpButton,
                exitGameButton,
            };
        }



        private void StartButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }
        private void HighscoreButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new HighscoreState(_game, _graphicsDevice, _content));
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new AboutState(_game, _graphicsDevice, _content));
        }

        private void HelpButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new HelpState(_game, _graphicsDevice, _content));

        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);
            //spriteBatch.DrawString()
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
