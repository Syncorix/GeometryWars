using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryWars.Enemy
{
    public class SpawnPatterns
    {
        public Random random = new Random();

        public Square SquareRandomPattern(Texture2D squareTexture)
        {
            Square newEnemy = new Square(squareTexture);
            int spawnSide = random.Next(1, 5);

            if (spawnSide == 1)//top
            {
                int spawnPosition = random.Next(1600);
                newEnemy.enemyPosition = new Vector2(spawnPosition, -100);
            }
            else if (spawnSide == 2)//bottom
            {
                int spawnPosition = random.Next(1600);
                newEnemy.enemyPosition = new Vector2(spawnPosition, 1000);
            }
            else if (spawnSide == 3)//left
            {
                int spawnPosition = random.Next(900);
                newEnemy.enemyPosition = new Vector2(-100, spawnPosition);
            }
            else if (spawnSide == 4)//right
            {
                int spawnPosition = random.Next(900);
                newEnemy.enemyPosition = new Vector2(1700, spawnPosition);
            }
            newEnemy.isVisible = true;
            return newEnemy;
        }

        public List<Square> SquareLinePattern(Texture2D squareTexture)
        {
            List<Square> squareEnemies = new List<Square>();
            int spawnSide = random.Next(1, 5);

            if (spawnSide == 1)//top
            {
                for (int spawnPosition = 0; spawnPosition <= 1600; spawnPosition += 100)
                {
                    Square newEnemy = new Square(squareTexture);
                    newEnemy.enemyPosition = new Vector2(spawnPosition, -100);
                    newEnemy.isVisible = true;
                    squareEnemies.Add(newEnemy);
                }
            }
            else if (spawnSide == 2)//bottom
            {
                for (int spawnPosition = 0; spawnPosition <= 1600; spawnPosition += 100)
                {
                    Square newEnemy = new Square(squareTexture);
                    newEnemy.enemyPosition = new Vector2(spawnPosition, 1000);
                    newEnemy.isVisible = true;
                    squareEnemies.Add(newEnemy);
                }
            }
            else if (spawnSide == 3)//left
            {
                for (int spawnPosition = 0; spawnPosition <= 900; spawnPosition += 100)
                {
                    Square newEnemy = new Square(squareTexture);
                    newEnemy.enemyPosition = new Vector2(-100, spawnPosition);
                    newEnemy.isVisible = true;
                    squareEnemies.Add(newEnemy);
                }
            }
            else if (spawnSide == 4)//right
            {
                for (int spawnPosition = 0; spawnPosition <= 900; spawnPosition += 100)
                {
                    Square newEnemy = new Square(squareTexture);
                    newEnemy.enemyPosition = new Vector2(1700, spawnPosition);
                    newEnemy.isVisible = true;
                    squareEnemies.Add(newEnemy);
                }
            }
            return squareEnemies;
        }

        public List<Square> SquareBoxPattern(Texture2D squareTexture)
        {
            List<Square> squareEnemies = new List<Square>();

            for (int spawnPosition = 0; spawnPosition <= 1600; spawnPosition += 100)
            {
                Square newEnemy = new Square(squareTexture);
                newEnemy.enemyPosition = new Vector2(spawnPosition, -100);
                newEnemy.isVisible = true;
                squareEnemies.Add(newEnemy);
            }

            for (int spawnPosition = 0; spawnPosition <= 1600; spawnPosition += 100)
            {
                Square newEnemy = new Square(squareTexture);
                newEnemy.enemyPosition = new Vector2(spawnPosition, 1000);
                newEnemy.isVisible = true;
                squareEnemies.Add(newEnemy);
            }

            for (int spawnPosition = 0; spawnPosition <= 900; spawnPosition += 100)
            {
                Square newEnemy = new Square(squareTexture);
                newEnemy.enemyPosition = new Vector2(-100, spawnPosition);
                newEnemy.isVisible = true;
                squareEnemies.Add(newEnemy);
            }

            for (int spawnPosition = 0; spawnPosition <= 900; spawnPosition += 100)
            {
                Square newEnemy = new Square(squareTexture);
                newEnemy.enemyPosition = new Vector2(1700, spawnPosition);
                newEnemy.isVisible = true;
                squareEnemies.Add(newEnemy);
            }
            return squareEnemies;
        }

        public Circle CircleRandomPattern(Texture2D circleTexture)
        {
            Circle newEnemy = new Circle(circleTexture);
            int spawnSide = random.Next(1, 5);

            if (spawnSide == 1)//top
            {
                int spawnPosition = random.Next(1600);
                newEnemy.enemyPosition = new Vector2(spawnPosition, -100);
            }
            else if (spawnSide == 2)//bottom
            {
                int spawnPosition = random.Next(1600);
                newEnemy.enemyPosition = new Vector2(spawnPosition, 1000);
            }
            else if (spawnSide == 3)//left
            {
                int spawnPosition = random.Next(900);
                newEnemy.enemyPosition = new Vector2(-100, spawnPosition);
            }
            else if (spawnSide == 4)//right
            {
                int spawnPosition = random.Next(900);
                newEnemy.enemyPosition = new Vector2(1700, spawnPosition);
            }
            newEnemy.isVisible = true;
            return newEnemy;
        }
    }
}
