using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GeometryWars.Player;
using GeometryWars.Enemy;
using GeometryWars.Collision;
using GeometryWars.States;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace GeometryWars
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        public SpriteFont _spriteFont;

        private State _currentState;
        private State _nextState;

        public PlayerMain player = new PlayerMain();
        public EnemyMain enemy = new EnemyMain();
        //public Collision1 collision = new Collision1();

        public SoundEffectInstance songStart;
        public SoundEffectInstance songLoop;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);
            player.LoadContent(Content);
            enemy.LoadContent(Content);

            songStart = Content.Load<SoundEffect>("audio/GameAudioStart").CreateInstance();
            songLoop = Content.Load<SoundEffect>("audio/GameAudioLoop").CreateInstance();

            songStart.Volume = .1f;
            songLoop.Volume = .1f;
            songLoop.IsLooped = true;
            songStart.Play();

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (songStart.State.ToString() != "Playing")
            {
                if(songLoop.State.ToString() != "Playing")
                {
                    songLoop.Play();
                }
            }

            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Exit();

            if(_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }
            // TODO: Add your update logic here
            _currentState.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}
