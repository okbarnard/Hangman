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
        private int wins, losses;
        public int Wins
        {
            get
            {
                return wins;
            }
            set
            {
                wins = value;
            }

        }
        public int Losses { get
            {
                return losses;
            }
            set
            {
                losses = value;
            }

        }

        public Player(string playerName)
        {
            name = playerName;
            Wins = wins;
            Losses = losses;

        }
        public void AddPoint(bool result)
        {
            if (result == true)
            {
                wins++;
            }
            else
            {
                losses++;
            }
        }
    }   
}
