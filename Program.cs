using System;
using System.Collections.Generic;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman!");



            int players;
            string playerOneName;
            //Console.WriteLine("1 or 2 players? Please select:\n1 - 1 player\n2 - player");
            ////try parse, if false, do while, if number > 2, reselect, etc.
            //players = Convert.ToInt32(Console.ReadLine());

            //difficulty selection
            //turn into enumeration?
            int difficulty = SelectDifficulty();    

            Console.Write("Enter the name of player 1: ");
            playerOneName = Console.ReadLine();
            Player player1 = new Player(playerOneName, difficulty);
            Console.WriteLine("Hi {0}! You have selected level {1} difficulty.", player1.name, player1.difficulty);

            //select word according to difficulty
            Word wordAndHint = Word.GenerateWord(difficulty);
            string correctWord = wordAndHint.CreateWord;
            string hint = wordAndHint.Hint;

            HangmanGame(player1, correctWord, hint);
            
        }

        public static void HangmanGame(Player player, string correctWord, string hint)
        {
            //set default values as '_' in placeholderDisplay to show user
            char[] placeholderDisplay = new char[correctWord.Length];
            for (int i = 0; i < correctWord.Length; i++)
            {
                placeholderDisplay[i] = '_';
            }

            //guess & strike declaration
            List<char> guesses = new List<char>();
            int strikes = 0;
            string input;
            char guess;

            Console.WriteLine("\n6 strikes and the hanging commences!");
            do
            {
                Console.WriteLine("Need a hint? Type \"hint\".");
                Console.WriteLine("To quit, type \"exit\".\n");
                //hangman display
                switch (strikes)
                {
                    case 0:
                        Console.WriteLine("-----\n|   |  \n|    \n|\n|\n|\n=======");
                        break;
                    case 1: //head
                        Console.WriteLine("-----\n|   |  \n|   O\n|\n|\n|\n=======");
                        break;
                    case 2: //body
                        Console.WriteLine("-----\n|   |  \n|   O\n|   |\n|\n|\n=======");
                        break;
                    case 3: //left leg
                        Console.WriteLine("-----\n|   |  \n|   O\n|   |\n|  /\n|\n=======");
                        break;
                    case 4: //right leg
                        Console.WriteLine("-----\n|   |  \n|   O\n|   |\n|  / \\\n|\n=======\nCareful! Two more guesses!");
                        break;
                    case 5: //left arm
                        Console.WriteLine("-----\n|   |  \n|   O\n| --|\n|  / \\\n|\n=======\nOne more guess!");
                        break;
                    case 6: //right arm
                        Console.WriteLine("-----\n|   |  \n|   O\n| --|--\n|  / \\\n|\n=======");
                        break;
                }

                //display guesses (correct & incorrect)
                Console.Write("Previous Guesses: ");
                Display(guesses.ToArray());
                Display(placeholderDisplay);

                bool intIsTrue;
                //Guess prompt, intake, and store; could add an option for them to correct the whole word here.
                do
                {
                    intIsTrue = false;
                    Console.Write("Guess a letter: ");
                    int intInput;
                    input = Console.ReadLine();

                    if (string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine("Guess cannot be blank!");
                    }


                    if (int.TryParse(input, out intInput))
                    {
                        Console.WriteLine("Please enter a letter or guess the word.");
                        intIsTrue = true;
                    }

                    input = input.Trim().ToLower();

                    if (input == "exit")
                    {
                        Console.WriteLine("Closing Program.");
                        break;
                    }

                    if (input == "hint")
                    {
                        Console.WriteLine(hint);
                    }

                    if ((input.Length > 1 && input != "exit" && input != "hint" && input != correctWord))
                    {
                        Console.WriteLine("You have either guessed the incorrect word or typed more than one character -- Please try again.");
                    }
                }
                while ((input.Length > 1 && input != correctWord) || (string.IsNullOrEmpty(input)) || intIsTrue);

                if (input == correctWord || input == "exit")
                {
                    break;
                }

                //if guess already exists... Don't add, don't add strike. Display message.
                guess = Convert.ToChar(input);

                if (guesses.Contains(guess))
                {
                    Console.Clear();
                    Console.WriteLine("You've already guessed this.");
                }
                else
                {
                    guesses.Add(guess);

                    //if guess is correct, update placeholderDisplay
                    for (int i = 0; i < correctWord.Length; i++)
                    {
                        if (guess == correctWord[i])
                        {
                            placeholderDisplay[i] = guess;
                        }
                    }

                    //if guess is incorrect, increase strikes
                    if (!(correctWord.Contains(guess)))
                    {
                        strikes++;
                    }


                    //is this a dupe of input? Is this necessary anymore?
                    //check if guessed word matches correct word
                    string guessedWord = new string(placeholderDisplay);
                    if (guessedWord == correctWord)
                    {
                        break;
                    }

                    if (strikes < 6)
                    {
                        Console.Clear();
                    }
                }
            }
            while (strikes != 6);

            //gameover message (win or lose)
            Console.WriteLine("The correct word was {0}!", correctWord);
            if (strikes != 6 && input != "exit")
            {
                Console.WriteLine("Congratulations! You're free!");
            }
            else
            {
                Console.WriteLine("You've been hung! ): ): ");
            }

            //display wins and losses
        }
        private static void Display(char[] displayArr)
        {
            foreach (var letter in displayArr)
            {
                Console.Write("{0 }", letter);
            }

            Console.WriteLine("\n");
        }

        public static int SelectDifficulty()
        {
            int difficulty = 0;
            do
            {
                Console.WriteLine("Please select your difficulty level: \n1 - Easy\n2 - Medium\n3 - Hard");
                difficulty = Convert.ToInt32(Console.ReadLine());
                if (difficulty < 1 || difficulty > 3)
                {
                    Console.WriteLine("Please enter a number of 1 - 3 to select your difficulty.");
                }
            }
            while (difficulty < 1 || difficulty > 3);
            return difficulty;
        }

    }
}

/*
 * multiplayer
 *specify whose turn it is
 *ask for how many rounds (provide options --3, 5, or 7, where last of each round when the score is even is "tiebreaker!!!" )
 *option to provide the word (with a character limit, maybe 20), no numbers or special characters
 *check for numbers in custom words
 *tie breaker rounds are provided a random word. If a tie breaker round exists...
 *
 * general notes
 *add a diff type of hint--where it displays a letter (this one will add a limb to the hangman.
 *add catch for letter or exit at beginning of program?
 */