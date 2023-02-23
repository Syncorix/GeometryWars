using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeometryWars.ScoreData
{
    public class Highscore
    {
        public List<string> ReadScores()
        {
            List<string> highscores = new List<string>();
            StreamReader reader = new StreamReader("Highscores.txt");//declare reader
            using (StreamReader file = File.OpenText("Highscores.txt"))
            {
                string entries;
                while ((entries = file.ReadLine()) != null)
                {
                    string record;

                    while (!reader.EndOfStream)//for each line
                    {
                        record = reader.ReadLine();
                        highscores.Add(record);
                    }
                }
            }
            reader.Dispose();

            return highscores;
        }

        public bool HighscoreCheck(int score)
        {
            List<string> highscores = ReadScores();
            string[] record = highscores[9].Split(',');
            if (score > Convert.ToInt32(record[1]))
            {
                return true;
            }
            return false;
        }

        public void SaveHighscore(string name, int score)
        {
            string newSave = name + "," + score.ToString();
            List<string> oldHighscores = ReadScores();
            string newHighscores = "";
            int addCheck = 0;
            int tenCount = 10;

            for(int i = 0; i < tenCount; i++)
            {
                string[] record = oldHighscores[i].Split(',');
                if (score >= Convert.ToInt32(record[1]) && addCheck == 0)
                {
                    newHighscores += newSave + "\n";
                    addCheck++;
                    tenCount--;
                    i--;
                }
                else
                {
                    newHighscores += oldHighscores[i] + "\n";
                }
            }
            File.WriteAllText("Highscores.txt", newHighscores);
        }
    }
}
