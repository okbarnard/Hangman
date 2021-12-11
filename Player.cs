using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class Player
    {
        public string name;
        private int wins, losses; //remove
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
        public int Losses
        {
            get
            {
                return losses;
            }
            set
            {
                losses = value;
            }

        }
        public Player CreatePlayer()
        {
            string playerOneName;
            Console.Write("Enter the name of player 1: ");
            playerOneName = Console.ReadLine();
            Player player = new Player(playerOneName);
            return player;

        }

        public Player(string playerName)
        {
            name = playerName;
            Wins = wins;
            Losses = losses;

        }

        public void AddPoint(bool result)
        {
            if (result)
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
