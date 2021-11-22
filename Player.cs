using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Player
    {
        public string name;
        public int difficulty;
        private int wins, losses;

        public Player(string playerName, int playerDifficulty)
        {
            name = playerName;
            difficulty = playerDifficulty;

        }
        public void AddPoint()
        {
            //if win, add to win
            //else add to loss
        }
    }   
}
