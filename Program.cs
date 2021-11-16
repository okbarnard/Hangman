using System;

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

            //variable for correct word and guessed word
            Random rnd = new Random();
            int num = rnd.Next(0, 9);
            string correctWord = wordList[num];
            char[] guessSoFar = new char[correctWord.Length];
            for (int i = 0; i < correctWord.Length; i++)
            {
                guessSoFar[i] = '_';
            }
            //guesses
            int strikes = 0;
            char guess;

            Console.WriteLine("6 strikes and the hanging commences!");
            do
            {
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
                //add guess letters here?



                Console.WriteLine("\n");
                foreach (var letter in guessSoFar)
                {
                    Console.Write(letter);
                }
                Console.WriteLine("\n");
                Console.Write("Guess a letter: ");
                guess = Convert.ToChar(Console.ReadLine());

                //word length display
                for (int i = 0; i < correctWord.Length; i++)
                {
                    if (guess == correctWord[i])
                    {
                        guessSoFar[i] = guess;
                    }
                }

                if (!(correctWord.Contains(guess)))
                {
                    strikes++;
                }

                string guessedWord = new string(guessSoFar);
                if (guessedWord == correctWord)
                {
                    break;
                }
                Console.Clear();
            }
            while (strikes != 6);

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
// Add