using System;
using System.Collections.Generic;
using System.Text;
using GeometryWars.Player;
using GeometryWars.Enemy;
using GeometryWars.States;
using Microsoft.Xna.Framework;

namespace GeometryWars.Collision
{
    public class Collision1
    {
        public int CollisionCheck(PlayerMain player, EnemyMain enemy)
        {
            //checks if a bullet intersects an enemy and passes back the total score gained
            int score = 0;
            foreach (Square square in enemy.squareEnemies)
            {
                for (int i = 0; i < player.bullets.Count; i++)
                {
                    if (square.enemyRect.Intersects(player.bullets[i].bulletRect))
                    {
                        
                        square.isVisible = false;
                        player.bullets[i].isVisible = false;
                        score += 10;
                    }
                }
            }
            foreach (Circle circle in enemy.circleEnemies)
            {
                for (int i = 0; i < player.bullets.Count; i++)
                {
                    if (circle.enemyRect.Intersects(player.bullets[i].bulletRect))
                    {
                        circle.isVisible = false;
                        player.bullets[i].isVisible = false;
                        score += 50;
                    }
                }
            }
            return score;
        }

        public bool PlayerHitCheck(PlayerMain player, EnemyMain enemy)
        {
            //checks if an enemy hits the player
            foreach(Square square in enemy.squareEnemies)
            {
                if(Vector2.Distance(square.enemyPosition, player.playerPosition) < 40)
                {
                    square.isVisible = false;
                    if(player.shield == true)
                    {
                        return false;
                    }
                    return true;
                }
            }

            foreach (Circle circle in enemy.circleEnemies)
            {
                if (Vector2.Distance(circle.enemyPosition, player.playerPosition) < 40)
                {
                    circle.isVisible = false;
                    if (player.shield == true)
                    {
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
