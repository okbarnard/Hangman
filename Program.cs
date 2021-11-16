using System;
using System.Collections.Generic;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman!");
            //Add letters guessed
            string[] wordList = {
                "computer",
                "waterfountain",
                "sunglasses",
                "interview",
                "national",
                "microphone",
                "vitamins",
                "waterbottle",
                "parkinglot",
                "spaceship"
            };

            //variable for correct word, guessed word, and guesses
            Random rnd = new Random();
            int num = rnd.Next(0, 9);
            string correctWord = wordList[num];
            List<char> guesses = new List<char>();
            char[] placeholderDisplay = new char[correctWord.Length];
            
            //setting default values as '_' in placeholderDisplay
            for (int i = 0; i < correctWord.Length; i++)
            {
                placeholderDisplay[i] = '_';
            }

            //guesses
            int strikes = 0;
            char guess;

            Console.WriteLine("6 strikes and the hanging commences!");
            do
            {
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
                        Console.WriteLine("-----\n|   |  \n|   O\n|   |\n|  / \\\n|\n=======");
                        break;
                    case 5: //left arm
                        Console.WriteLine("-----\n|   |  \n|   O\n| --|\n|  / \\\n|\n=======");
                        break;
                    case 6: //right arm
                        Console.WriteLine("-----\n|   |  \n|   O\n| --|--\n|  / \\\n|\n=======");
                        break;
                }

                //display previous guesses
                Console.Write("Previous Guesses: ");
                foreach (var letter in guesses)
                {
                    Console.Write("{0}", letter);
                }

                Console.WriteLine("\n");

                //display placeholders/correct guesses
                foreach (var letter in placeholderDisplay)
                {
                    Console.Write("{0 }", letter);
                }
                Console.WriteLine("\n");

                //Guess prompt, intake, and store
                Console.Write("Guess a letter: ");
                guess = Convert.ToChar(Console.ReadLine());
                guesses.Add(guess);

                //word length display
                for (int i = 0; i < correctWord.Length; i++)
                {
                    if (guess == correctWord[i])
                    {
                        placeholderDisplay[i] = guess;
                    }
                }
                //strike if incorrect guess
                if (!(correctWord.Contains(guess)))
                {
                    strikes++;
                }

                //check if guessed word matches correct word
                string guessedWord = new string(placeholderDisplay);
                if (guessedWord == correctWord)
                {
                    break;
                }
                Console.Clear();
            }
            while (strikes != 6);


            //gameover message (win or lose)
            Console.WriteLine("The correct word was {0}!", correctWord);
            if (strikes != 6)
            {
                Console.WriteLine("Congratulations! You're free!");
            }
            else
            {
                Console.WriteLine("You've been hung! ): ): ");
            }
        }
    }
}

//Single or multiplayer
// *Ask for how many rounds (provide options --3, 5, or 7, where last of each round WHEN the score is even is "Tiebreaker!!!" )
// *Option to provide the word (with a character limit, maybe 20) Supercalifragilisticexpialidocious, no numbers or special characters
// *add hints later on? -- add option to add hints if providing the word. Asking for a hint also adds a limb to the hangman. Add a warning when they only have 2 limbs left and they can't ask for a hint on the last... leg (limb)
// * Random generator for what hint to choose to display; 3 hints max.
// *If single player and no hangman "graphic," displays how many turns they have left (head, body, arm, arm, leg, leg --total of 6 guesses).
// *Difficulty level of words?
// Add letters guessed.