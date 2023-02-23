using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using GeometryWars.Controls;
using GeometryWars.ScoreData;
using System.Text;

namespace GeometryWars.States
{
    public class HighscoreState : State
    {
        private Texture2D backgroundTexture;
        private Rectangle backgroundRect;

        public Highscore highscore = new Highscore();
        private List<Component> components;
        private List<string> scores;
        private List<string> scoreNames = new List<string>();
        private List<string> scoreList = new List<string>();
        private SpriteFont scoreFont;

        public HighscoreState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            backgroundTexture = content.Load<Texture2D>("images/Background");
            backgroundRect = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);

            Texture2D buttonTexture = _content.Load<Texture2D>("Controls/Button");
            SpriteFont buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            scoreFont = _content.Load<SpriteFont>("Fonts/ScoreFont");

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

            scores = highscore.ReadScores();
            for (int i = 0; i < scores.Count; i++)
            {
                string[] temp = scores[i].Split(',');
                scoreNames.Add((i + 1).ToString() + ". " + temp[0]);
                scoreList.Add(temp[1]);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Update(GameTime gameTime)
        {
            foreach(Button component in components)
            {
                component.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);

            foreach (Button component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();

            spriteBatch.Begin();
            int x = 650;
            int y = 300;
            foreach (string scoreName in scoreNames)
            {
                Vector2 position = new Vector2(x, y);
                if (scoreName.Contains("10"))
                {
                    position.X -= 15;
                }
                spriteBatch.DrawString(scoreFont, scoreName, position, Color.White);
                y += 25;
            }
            x = 925;
            y = 300;
            foreach (string score in scoreList)
            {
                Vector2 position = new Vector2(x, y);
                spriteBatch.DrawString(scoreFont, score, position, Color.White);
                y += 25;
            }
            spriteBatch.End();
        }
    }
}
