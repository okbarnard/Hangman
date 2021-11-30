using System;
using System.Collections.Generic;

//use numbers to exit game?
//invalid selection. Please try again

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman!");

            int numberOfPlayers = GetNumberOfPlayers();

            if (numberOfPlayers == 1)
            {
                Player player1 = CreatePlayer();
                int difficulty = SelectDifficulty();

                Console.WriteLine("Hi {0}! You have selected level {1} difficulty.", player1.name, difficulty); //change to difficulty name

                Console.Clear();
                Game.HangmanGame(player1, difficulty);
            }

            if (numberOfPlayers == 2)
            {
                Player player1 = CreatePlayer();
                Player player2 = CreatePlayer();

                Console.WriteLine("Hello {0} (player 1) and {1} (player 2)!", player1.name, player2.name);

                //select rounds function
                int numberOfRounds = GetNumberOfRounds();

                int difficulty = SelectDifficulty();
                int currentRound = 0;
                while (currentRound != numberOfRounds)
                {

                    DisplayPlayerTurn(player1.name);
                    Game.HangmanGame(player1, difficulty);

                    DisplayPlayerTurn(player2.name);
                    Game.HangmanGame(player2, difficulty);
                    Console.ReadLine();
                    currentRound++;
                }

                //display winner
                DetermineWinner(player1, player2);

            }          
        }

        //public static void PrepareGame(int numberOfPlayers)
        //{
        //    //create players
        //    //select difficulty
        //    //get number of rounds?
        //}
        private static void DisplayPlayerTurn(string name)
        {
            Console.WriteLine("{0}'s turn. Press any key to continue.", name);
            Console.ReadLine();
        }

        private static int GetNumberOfPlayers()
        {
            int players;
            do
            {
                Console.WriteLine("How many players? Please select:\n1 - 1 player\n2 - 2 players");
                ////try parse, if false, do while, if number > 2, reselect, etc.
                players = Convert.ToInt32(Console.ReadLine());

                if (players < 1 || players > 2)
                {
                    Console.WriteLine("You will need to select at least one player and two at the most!");
                }
            }
            while (players < 1 || players > 2);
            return players;
        }

        private static int SelectDifficulty()
        {
            int difficulty = 0;
            do
            {
                Console.WriteLine("Please select your difficulty level: \n1 - Easy\n2 - Medium\n3 - Hard");
                difficulty = Convert.ToInt32(Console.ReadLine());
                if (difficulty < 1 || difficulty > 3)
                {
                    Console.WriteLine("Please enter a number of 1 - 3 to select the difficulty."); //"the difficulty, insinutating it chooses it for both players in 2 player rounds
                }
            }
            while (difficulty < 1 || difficulty > 3);
            return difficulty;
        }

        public static Player CreatePlayer()
        {
            string playerName;
            Console.Write("Enter player name: ");
            playerName = Console.ReadLine();
            Player player = new Player(playerName);
            return player;

        }

        public static int GetNumberOfRounds()
        {
            Console.WriteLine("How many rounds do you want to play? 1, 2, 3");
            int rounds = Convert.ToInt32(Console.ReadLine());

            return rounds;
        }

        private static void DetermineWinner(Player player1, Player player2)
        {
            if (player1.Wins > player2.Wins)
            {
                Console.WriteLine("{0} wins!", player1.name);
            }
            else if (player1.Wins > player2.Wins)
            {
                Console.WriteLine("{0} wins!", player2.name);
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }
    }
}

/*
 * multiplayer
 *specify whose turn it is
 *ask for how many rounds (provide options --3, 5, or 7, where last of each round when the score is even is "tiebreaker!!!" )

 *Can probably simplify 1/2 player... pass int to hangman to decide how many times to run?
 */