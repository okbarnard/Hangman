using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Game
    {
        public static void HangmanGame(Player player, int difficulty)
        {
            GenerateWordWithHint(difficulty, out string correctWord, out string hint);
            char[] placeholderDisplay = GeneratePlaceholder(correctWord);

            //guess & strike declaration
            List<char> guesses = new List<char>(); //was this always a list?
            int strikes = 0;
            string input;
            char guess;

            Console.WriteLine("\n6 strikes and the hanging commences!");

            do
            {
                Console.Clear();
                DisplayHeader(player.name);
                //hangman display
                HangmanDisplay(strikes);
                //display guesses
                DisplayGuess(guesses, placeholderDisplay);


                bool inputIsInvalid; //empty or an int
                //Guess prompt, intake, and store; could add an option for them to correct the whole word here.
                do
                {
                    inputIsInvalid = false;

                    Console.Write("Guess a letter: ");
                    input = Console.ReadLine();

                    int number;
                    bool inputIsInt = (int.TryParse(input, out number));
                    bool inputIsEmpty = string.IsNullOrEmpty(input);

                    if (inputIsInt || inputIsEmpty)
                    {
                        inputIsInvalid = true;
                        DisplayMessage(inputIsInvalid);
                    }

                    input = input.Trim().ToLower();

                    if (!inputIsInvalid && input.Length > 1 && input != correctWord)
                    {
                        //separate function?
                        if (input == "exit")
                        {
                            Console.WriteLine("Closing program.");
                            break;
                        }
                        else if (input == "hint")
                        {
                            Console.WriteLine(hint);
                        }
                        else
                        {
                            Console.WriteLine("You have either guessed the incorrect word or typed more than one character -- Please try again.");
                        }
                    }
                }
                while ((input.Length > 1 && input != correctWord) || inputIsInvalid) ;

                if (input == correctWord || input == "exit")
                {
                    break;
                }

                //if guess already exists... Don't add, don't add strike. Display message.
                guess = Convert.ToChar(input);
                bool previouslyGuessed = guesses.Contains(guess);

                if (previouslyGuessed)
                {
                    Console.Clear();
                    Console.WriteLine("You've already guessed this.");
                }
                else
                {
                    guesses.Add(guess);

                    //if guess is correct, update placeholderDisplay
                    UpdatePlaceholderWithGuess(correctWord, guess, placeholderDisplay);

                    //if guess is incorrect, increase strikes
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
                }
            }
            while (strikes != 6);

            //gameover message (win or lose)
            Console.Write("The correct word was {0}! ", correctWord);
            RoundEndMessage(strikes, input, player);

            Console.WriteLine("{0} Wins: {1}\t Losses: {2}", player.name, player.Wins, player.Losses);
        }

        public static Word GenerateWordWithHint(int difficulty, out string correctWord, out string hint)
        {
            //select word according to difficulty
            Word wordAndHint = Word.GetWord(difficulty);
            correctWord = wordAndHint.CreateWord;
            hint = wordAndHint.Hint;
            return wordAndHint;
        }
        public static void DisplayHeader(string name)
        {
            Console.WriteLine("{0}'S TURN\n" +
                    "\nNeed a hint? Type \"hint\"." +
                    "To quit, type \"exit\".\n\n", name.ToUpper());
        }
        private static void Display(char[] displayArr) //can probably refactor this and generate placeholder...
        {
            foreach (var letter in displayArr)
            {
                Console.Write("{0 }", letter);
            }

            Console.WriteLine("\n");
        }
        public static void DisplayMessage(bool isEmptyOrANumber)
        {
            if (isEmptyOrANumber)
            {
                Console.WriteLine("Please enter a valid input (no numbers; cannot be blank).");
            }
        }

        public static void DisplayGuess(List<char> guesses, char[] placeholderDisplay )
        {
            //display guesses (correct & incorrect)
            Console.Write("Previous Guesses: ");
            Display(guesses.ToArray());
            Display(placeholderDisplay);
        }

        private static void HangmanDisplay(int strikes)
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
                    Console.WriteLine("-----\n|   |  \n|   O\n|   |\n|  / \\\n|\n=======\nCareful! Two more guesses!");
                    break;
                case 5: //left arm
                    Console.WriteLine("-----\n|   |  \n|   O\n| --|\n|  / \\\n|\n=======\nOne more guess!");
                    break;
                case 6: //right arm
                    Console.WriteLine("-----\n|   |  \n|   O\n| --|--\n|  / \\\n|\n=======");
                    break;
            }
        }

        private static char[] GeneratePlaceholder(string correctWord)
        {
            char[] placeholder = new char[correctWord.Length];
            //set default values as '_' in placeholderDisplay to show user
            for (int i = 0; i < correctWord.Length; i++)
            {
                placeholder[i] = '_';
            }
            return placeholder;
        }

        //could probably use this in generateplaceholder()
        private static void UpdatePlaceholderWithGuess(string correctWord, char guess, char[] placeholderDisplay)
        {
            for (int i = 0; i < correctWord.Length; i++)
            {
                if (guess == correctWord[i])
                {
                    placeholderDisplay[i] = guess;
                }
            }
        }
        public static void RoundEndMessage(int strikes, string input, Player player)
        {
            if (strikes != 6 && input != "exit")
            {
                Console.WriteLine("Congratulations! You're free!");
                bool win = true;
                player.AddPoint(win);
            }
            else
            {
                Console.WriteLine("You've been hung! ): ");
                bool win = false;
                player.AddPoint(win);
            }
        }


        //public static void CheckIfExit()
        //{

        //}
    }
}
