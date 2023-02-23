using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using GeometryWars.Player;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using GeometryWars.Collision;

namespace GeometryWars.Enemy
{
    public class EnemyMain
    {
        public PlayerMain player = new PlayerMain();
        public SpawnPatterns spawnPatterns = new SpawnPatterns();

        public Texture2D squareTexture;
        public Vector2 squarePosition;
        public Rectangle squareRect;
        //public bool isVisible;
        public List<Square> squareEnemies = new List<Square>();

        public Texture2D circleTexture;
        public Vector2 circlePosition;
        public Rectangle circleRect;
        //public bool isVisible;
        public List<Circle> circleEnemies = new List<Circle>();

        public SoundEffect explosion;
        public Texture2D explosionSheet;
        public List<Explosion> explosions = new List<Explosion>();


        public double gameLength = 0;
        public double circleTimer;
        public double squareTimer;
        public double frequencyTimer;
        public double squareFrequency = 1;

        int threshold;
        byte previousAnimationIndex;
        byte currentAnimationIndex;



        public EnemyMain()
        {

        }

        public void LoadContent(ContentManager content)
        {
            squareTexture = content.Load<Texture2D>("images/Square");
            circleTexture = content.Load<Texture2D>("images/Circle");
            explosion = content.Load<SoundEffect>("audio/Explosion");
            explosionSheet = content.Load<Texture2D>("images/ExplosionSheet");
        }

        public void Update(GameTime gameTime, PlayerMain player)
        {
            gameLength += gameTime.ElapsedGameTime.TotalSeconds;
            SpawnFrequency(gameTime);
            SpawnEnemy(gameTime);
            MoveEnemies(player);
            Explosion(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Square enemy in squareEnemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach(Circle enemy in circleEnemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (Explosion explosion in explosions)
            {
                explosion.Draw(spriteBatch);
            }
        }

        public void SpawnEnemy(GameTime gameTime)
        {
            circleTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            squareTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (squareTimer >= (1000 * squareFrequency))
            {
                Random random = new Random();

                int spawnType = random.Next(1, 101);
                if (spawnType >= 1 && spawnType <= 3 && gameLength > 30)
                {
                    foreach (Square enemy in spawnPatterns.SquareBoxPattern(squareTexture))
                    {
                        squareEnemies.Add(enemy);
                    }
                }
                else if(spawnType >= 4 && spawnType <= 14)
                {
                    foreach(Square enemy in spawnPatterns.SquareLinePattern(squareTexture))
                    {
                        squareEnemies.Add(enemy);
                    }
                }
                else
                {
                    squareEnemies.Add(spawnPatterns.SquareRandomPattern(squareTexture));
                }
                squareTimer = 0;
            }

            if(circleTimer >= (5000 * squareFrequency) && gameLength > 30)
            {
                circleEnemies.Add(spawnPatterns.CircleRandomPattern(circleTexture));
                circleTimer = 0;
            }
        }

        public void MoveEnemies(PlayerMain player)
        {
            foreach (Square square in squareEnemies)
            {   
                Vector2 offset = new Vector2(25/2, 25/2);
                Vector2 dPos = square.enemyPosition - player.playerPosition + offset;
                dPos.Normalize();

                square.enemyRect = new Rectangle((int)square.enemyPosition.X, (int)square.enemyPosition.Y, square.enemyTexture.Width, square.enemyTexture.Height);
                square.enemyPosition += -dPos * 4;
            }

            foreach (Circle circle in circleEnemies)
            {
                Vector2 offset = new Vector2(25 / 2, 25 / 2);
                Vector2 dPos = circle.enemyPosition - player.playerPosition + offset;
                dPos.Normalize();

                circle.enemyRect = new Rectangle((int)circle.enemyPosition.X, (int)circle.enemyPosition.Y, circle.enemyTexture.Width, circle.enemyTexture.Height);
                circle.enemyPosition += -dPos * 4;
                foreach (Bullet bullet in player.bullets)
                {
                    if (Vector2.Distance(circle.enemyPosition, bullet.bulletPosition) < 50)
                    {
                        dPos = circle.enemyPosition - bullet.bulletPosition;
                        dPos.Normalize();
                        circle.enemyPosition += dPos * 6;
                    }
                }
            }


            for (int i = 0; i < squareEnemies.Count; i++)
            {
                if (squareEnemies[i].isVisible == false)
                {
                    Explosion newExplosion = new Explosion(explosionSheet, squareEnemies[i].enemyPosition);
                    explosions.Add(newExplosion);
                    explosion.Play();
                    squareEnemies.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < circleEnemies.Count; i++)
            {
                if (circleEnemies[i].isVisible == false)
                {
                    Explosion newExplosion = new Explosion(explosionSheet, squareEnemies[i].enemyPosition);
                    explosions.Add(newExplosion);
                    explosion.Play();
                    circleEnemies.RemoveAt(i);
                    i--;
                }
            }
        }

        public void SpawnFrequency(GameTime gameTime)
        {
            frequencyTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if(squareFrequency > 0.5)
            {
                if (frequencyTimer >= 30)
                {
                    squareFrequency = squareFrequency * 0.75;
                    frequencyTimer = 0;
                }
            }
        }

        public void Explosion(GameTime gameTime)
        {
            for (int i = 0; i < explosions.Count; i++)
            {
                explosions[i].timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (explosions[i].timer > 50)
                {
                    explosions[i].timer = 0;
                    if (explosions[i].currentAnimationIndex == 6)
                    {
                        explosions.RemoveAt(i);
                        i--;
                        //if (explosion.previousAnimationIndex == 0)
                        //{
                        //    explosion.currentAnimationIndex = 2;
                        //}
                        //else
                        //{
                        //    explosion.currentAnimationIndex = 0;
                        //}

                        //explosion.previousAnimationIndex = currentAnimationIndex;
                    }
                    else
                    {
                        explosions[i].currentAnimationIndex++;
                    }
                }
            }
        }
        public void Flocking()
        {
            foreach(Square square in squareEnemies)
            {

            }
        }
    }
}