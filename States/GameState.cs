using GeometryWars.Collision;
using GeometryWars.Controls;
using GeometryWars.Enemy;
using GeometryWars.ScoreData;
using GeometryWars.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace GeometryWars.States
{
    public class GameState : State
    {
        //declaring variables
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private Texture2D backgroundTexture;
        private Rectangle backgroundRect;

        public PlayerMain player = new PlayerMain();
        public EnemyMain enemy = new EnemyMain();
        public Collision1 collision = new Collision1();

        private Texture2D heartTexture;
        public Vector2 heartPosition;
        private Texture2D shieldBarTexture;
        private Texture2D shieldIconTexture;
        public Vector2 shieldPosition;

        public Highscore highscore = new Highscore();
        public Score newScore;
        private Texture2D scoreTexture;
        public int score = 0;

        private KeyboardToText keyboardToText = new KeyboardToText();
        private Textbox textbox;
        private Texture2D textboxTexture;
        private float keyDelay = 0;
        //private bool keyPressed = false;

        private List<Component> components;
        private List<Component> lostComponents;
        private List<Component> highscoreComponents;

        private bool pauseGame = false;
        private bool lostGame = false;
        private bool newHighscoreCheck = false;

        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {//loading content
            _spriteBatch = new SpriteBatch(graphicsDevice);
            content.RootDirectory = "Content";
            backgroundTexture = content.Load<Texture2D>("images/Background");
            backgroundRect = new Rectangle(0, 0, backgroundTexture.Width, backgroundTexture.Height);

            player.LoadContent(content);
            enemy.LoadContent(content);

            textboxTexture = _content.Load<Texture2D>("images/Textbox");
            heartTexture = _content.Load<Texture2D>("images/Heart");
            scoreTexture = _content.Load<Texture2D>("images/Score");
            SpriteFont scoreFont = _content.Load<SpriteFont>("Fonts/ScoreFont");

            shieldBarTexture = _content.Load<Texture2D>("images/ShieldBarOutline");
            shieldIconTexture = _content.Load<Texture2D>("images/ShieldBar");

            Texture2D buttonTexture = _content.Load<Texture2D>("Controls/Button");
            SpriteFont buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            //creating buttons for the pause menu and the end game menu
            Button newGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 300),
                text = "New Game",
            };
            newGameButton.Click += NewGameButton_Click;

            Button resumeGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 300),
                text = "Resume Game",
            };
            resumeGameButton.Click += ResumeButton_Click;

            Button mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 400),
                text = "Main Menu",
            };
            mainMenuButton.Click += MainMenuButton_Click;

            Button exitGameButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 500),
                text = "Exit Game",
            };
            exitGameButton.Click += ExitGameButton_Click;

            Button saveScoreButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 400),
                text = "Save Highscore",
            };
            saveScoreButton.Click += SaveScoreButton_Click;

            Button skipScoreButton = new Button(buttonTexture, buttonFont)
            {
                position = new Vector2(700, 500),
                text = "Skip",
            };
            skipScoreButton.Click += SkipScoreButton_Click;
            //putting buttons into component lists for each menu
            components = new List<Component>()
            {
                resumeGameButton,
                mainMenuButton,
                exitGameButton,
            };

            lostComponents = new List<Component>()
            {
                newGameButton,
                mainMenuButton,
                exitGameButton,
            };

            highscoreComponents = new List<Component>()
            {
                saveScoreButton,
                skipScoreButton
            };

            newScore = new Score(score.ToString(), scoreTexture, scoreFont);
            //creating the textbox
            textbox = new Textbox(textboxTexture, scoreFont)
            {
                inputPosition = new Vector2(675, 300)
            };
        }



        public override void Update(GameTime gameTime)
        {
            KeyboardState key = Keyboard.GetState();
            keyDelay += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //pauses game if escape is pressed
            if (key.IsKeyDown(Keys.Escape) && lostGame == false)
            {
                pauseGame = true;
            }
            
            //check contains all the action scene code which runs if the game has not ended or paused
            if(pauseGame == false && lostGame == false)
            {
                //updates
                player.Update(gameTime);
                enemy.Update(gameTime, player);
                //checks for collision and updates score
                score += collision.CollisionCheck(player, enemy);
                newScore.currentScore = score.ToString();
                //code to check player lives and end game if lives are 0
                if (collision.PlayerHitCheck(player, enemy))
                {
                    float volume = .5f;
                    float pitch = 0f;
                    float pan = 0f;
                    player.lifeLost.Play(volume, pitch, pan);
                    player.playerLives--;
                    enemy.squareEnemies.Clear();
                    enemy.circleEnemies.Clear();
                    if (player.playerLives == 0)
                    {
                        lostGame = true;
                    }
                }
            }
            else
            {//pause game menu
                foreach(Button component in components)
                {
                    component.Update(gameTime);
                }
            }
            if(lostGame == true)
            {//checks score to see if its a high score
                newHighscoreCheck = highscore.HighscoreCheck(score);
                if (newHighscoreCheck == true)
                {//manages keyboard input to write into the textbox
                    //this isnt the proper way but it still works
                    int characterLimit = textbox.text.Length;
                    if((key.GetHashCode() >= 65 || key.GetHashCode() <= 90) && keyDelay > 150)
                    {
                        string result = keyboardToText.KeyboardKeyPress(key);

                        if (result == "Back")
                        {
                            if(textbox.text.Length != 0)
                            {
                                textbox.text = textbox.text.Remove(textbox.text.Length - 1);
                            }
                        }
                        else
                        {
                            if(characterLimit <= 12)
                            {
                                textbox.text += result;
                            }
                        }
                        keyDelay = 0;
                    }
                    //loads buttons to save or skip score
                    foreach (Button component in highscoreComponents)
                    {
                        component.Update(gameTime);
                    }
                }
                else
                {
                    //loads buttons that allow player to start new game or go back to menu
                    foreach (Button component in lostComponents)
                    {
                        component.Update(gameTime);
                    }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTexture, backgroundRect, Color.White);
            spriteBatch.End();

            if (lostGame != true)
            {
                player.Draw(_spriteBatch);
                enemy.Draw(_spriteBatch);
                spriteBatch.Begin();
                for (int i = 0; i < player.playerLives; i++)
                {
                    heartPosition = new Vector2(50 + (60 * i), 800);
                    spriteBatch.Draw(heartTexture, heartPosition, Color.White);
                }
                    spriteBatch.Draw(shieldBarTexture, new Vector2(45, 745), Color.White);
                for(int i = 0; i < player.shieldPower; i++)
                {
                    shieldPosition = new Vector2(50 + (20 * i), 750);
                    spriteBatch.Draw(shieldIconTexture, shieldPosition, Color.White);
                }
                newScore.Draw(spriteBatch);
                spriteBatch.End();
            }

            if(pauseGame == true)
            {
                spriteBatch.Begin();
                foreach (Button component in components)
                {
                    component.Draw(gameTime, spriteBatch);
                }
                spriteBatch.End();
            }

            if(lostGame == true)
            {
                if(newHighscoreCheck == true)
                {
                    if (highscore.HighscoreCheck(score) == true)
                    {
                        textbox.Draw(_spriteBatch);
                        spriteBatch.Begin();
                        foreach (Button component in highscoreComponents)
                        {
                            component.Draw(gameTime, spriteBatch);
                        }
                        spriteBatch.End();
                    }
                }
                else
                {
                    spriteBatch.Begin();
                    foreach (Button component in lostComponents)
                    {
                        component.Draw(gameTime, spriteBatch);
                    }
                    spriteBatch.End();
                }

            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            pauseGame = false;
        }

        private void MainMenuButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }

        private void SaveScoreButton_Click(object sender, EventArgs e)
        {
            highscore.SaveHighscore(textbox.text, score);
            score = 0;
            newHighscoreCheck = false;
        }        

        private void SkipScoreButton_Click(object sender, EventArgs e)
        {
            score = 0;
            newHighscoreCheck = false;
        }
    }
}