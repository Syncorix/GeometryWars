using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryWars.Controls
{
    public class KeyboardToText
    {
        public string KeyboardKeyPress(KeyboardState key)
        {
            if (key.IsKeyDown(Keys.Back))
            {
                return "Back";
            }
            else if (key.IsKeyDown(Keys.A))
            {
                return "A";
            }
            else if (key.IsKeyDown(Keys.B))
            {
                return "B";
            }
            else if (key.IsKeyDown(Keys.C))
            {
                return "C";
            }
            else if (key.IsKeyDown(Keys.D))
            {
                return "D";
            }
            else if (key.IsKeyDown(Keys.E))
            {
                return "E";
            }
            else if (key.IsKeyDown(Keys.F))
            {
                return "F";
            }
            else if (key.IsKeyDown(Keys.G))
            {
                return "G";
            }
            else if (key.IsKeyDown(Keys.H))
            {
                return "H";
            }
            else if (key.IsKeyDown(Keys.I))
            {
                return "I";
            }
            else if (key.IsKeyDown(Keys.J))
            {
                return "J";
            }
            else if (key.IsKeyDown(Keys.K))
            {
                return "K";
            }
            else if (key.IsKeyDown(Keys.L))
            {
                return "L";
            }
            else if (key.IsKeyDown(Keys.M))
            {
                return "M";
            }
            else if (key.IsKeyDown(Keys.N))
            {
                return "N";
            }
            else if (key.IsKeyDown(Keys.O))
            {
                return "O";
            }
            else if (key.IsKeyDown(Keys.P))
            {
                return "P";
            }
            else if (key.IsKeyDown(Keys.Q))
            {
                return "Q";
            }
            else if (key.IsKeyDown(Keys.R))
            {
                return "R";
            }
            else if (key.IsKeyDown(Keys.S))
            {
                return "S";
            }
            else if (key.IsKeyDown(Keys.T))
            {
                return "T";
            }
            else if (key.IsKeyDown(Keys.U))
            {
                return "U";
            }
            else if (key.IsKeyDown(Keys.V))
            {
                return "V";
            }
            else if (key.IsKeyDown(Keys.W))
            {
                return "W";
            }
            else if (key.IsKeyDown(Keys.X))
            {
                return "X";
            }
            else if (key.IsKeyDown(Keys.Y))
            {
                return "Y";
            }
            else if (key.IsKeyDown(Keys.Z))
            {
                return "Z";
            }
            return "";
        }
    }
}
