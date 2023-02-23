using GeometryWars.Collision;
using GeometryWars.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace GeometryWars.Player
{
    public class PlayerMain
    {
        public Texture2D tex;
        public Vector2 playerPosition;
        public Rectangle playerRect;
        public float rotation;
        public float scale = .75f;
        public Vector2 origin;
        public int playerLives = 3;
        public SoundEffect lifeLost;
        public bool shield = false;
        public Vector2 playerOffset = new Vector2(75 / 2, 75 / 2);

        public Texture2D bulletTexture;
        public List<Bullet> bullets = new List<Bullet>();
        public Rectangle bulletRect;
        public Song bulletSound;
        private float timer;
        private int lifeCounter = 3;
        private int bulletPattern = 0;
        private float aliveTimer;

        public Texture2D shieldTex;
        public int shieldPower = 5;
        public double shieldTimer = 0;


        public PlayerMain()
        {
            //bullets = new List<Bullet>();
            tex = null;
            playerPosition = new Vector2(772, 422);
            rotation = 0f;
            origin = new Vector2(75 / 2, 75 / 2);

            //playerRect = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, 75, 75);
            playerRect = new Rectangle(0, 0, 75, 75);
        }

        public void LoadContent(ContentManager content)
        {
            shieldTex = content.Load<Texture2D>("images/Shield");
            bulletTexture = content.Load<Texture2D>("images/Bullet");
            tex = content.Load<Texture2D>("images/Spaceship2");
            //bulletSound = content.Load<Song>("audio/BulletAudio");
            lifeLost = content.Load<SoundEffect>("audio/LifeLost");
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            KeyboardState key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.Space))
            {
                Shoot(gameTime);
            }
            if (key.IsKeyDown(Keys.E))
            {
                shield = true;
            }
            if (key.IsKeyDown(Keys.W))
            {
                if(playerPosition.Y > 35)
                {
                    playerPosition.Y -= 5;
                }
            }
            if (key.IsKeyDown(Keys.A))
            {
                if(playerPosition.X > 35)
                {
                    playerPosition.X -= 5;
                }
            }
            if (key.IsKeyDown(Keys.S))
            {
                if(playerPosition.Y < 865)
                {
                    playerPosition.Y += 5;
                }
            }
            if (key.IsKeyDown(Keys.D))
            {
                if(playerPosition.X < 1565)
                {
                    playerPosition.X += 5;
                }
            }

            Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
            Vector2 dPos = playerPosition - mousePosition;


            Shield(gameTime);


            UpdateBullets();
            rotation = (float)Math.Atan2(dPos.Y, dPos.X) - 1.5708f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, playerPosition, playerRect, Color.White, rotation, origin, scale,
                SpriteEffects.None, 0.1f);
            if(shield == true)
            {
                spriteBatch.Draw(shieldTex, playerPosition - playerOffset, Color.White);
            }

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public void Shoot(GameTime gameTime)
        {
            aliveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(playerLives != lifeCounter)
            {
                lifeCounter = playerLives;
                bulletPattern = 0;
                aliveTimer = 0;
            }
            else if(aliveTimer > 30)
            {
                bulletPattern = 1;
            }

            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer >= 90)
            {
                MouseState mouseState = Mouse.GetState();
                Vector2 mousePosition = new Vector2(mouseState.X, mouseState.Y);
                Vector2 dPos = playerPosition - mousePosition;

                Bullet newBullet = new Bullet(bulletTexture);
                dPos.Normalize();
                newBullet.velocity = -dPos * 8;
                newBullet.bulletPosition = new Vector2(playerPosition.X - 5, playerPosition.Y - 5) - dPos *20;
                newBullet.isVisible = true;
                bullets.Add(newBullet);
                //MediaPlayer.Play(bulletSound);

                if (bulletPattern == 1)
                {
                    Bullet newBullet2 = new Bullet(bulletTexture);
                    dPos = Vector2.Transform(dPos, Matrix.CreateRotationZ((float)0.5));
                    dPos.Normalize();
                    newBullet2.velocity = -dPos * 8;
                    newBullet2.bulletPosition = new Vector2(playerPosition.X - 5, playerPosition.Y - 5) - dPos * 20;
                    newBullet2.isVisible = true;
                    bullets.Add(newBullet2);

                    Bullet newBullet3 = new Bullet(bulletTexture);
                    dPos = Vector2.Transform(dPos, Matrix.CreateRotationZ((float)-1));
                    dPos.Normalize();
                    newBullet3.velocity = -dPos * 8;
                    newBullet3.bulletPosition = new Vector2(playerPosition.X - 5, playerPosition.Y - 5) - dPos * 20;
                    newBullet3.isVisible = true;
                    bullets.Add(newBullet3);
                }

                timer = 0;
            }
        }

        public void UpdateBullets()
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.bulletRect = new Rectangle((int)bullet.bulletPosition.X, (int)bullet.bulletPosition.Y, bullet.bulletTexture.Width, bullet.bulletTexture.Height);
                bullet.bulletPosition += bullet.velocity;
                if (bullet.bulletPosition.X < -100 || bullet.bulletPosition.X > 1700 || bullet.bulletPosition.Y < -100 || bullet.bulletPosition.Y > 1000)
                {
                    bullet.isVisible = false;
                }
            }
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].isVisible == false)
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Shield(GameTime gameTime)
        {
            shieldTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(shieldTimer >= 1 && shield == true)
            {
                shieldPower--;
                shieldTimer = 0;
            }
            if (shieldPower <= 0 && shield == true)
            {
                shield = false;
            }

            if (shield == false && shieldPower < 5)
            {
                if(shieldTimer >= 2)
                {
                    shieldPower++;
                    shieldTimer = 0;
                }
            }
        }
    }
}
