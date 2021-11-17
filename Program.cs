using System;
using System.Collections.Generic;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman!");
            //difficulty selection
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

            //select word according to difficulty
            string correctWord = WordDifficulty(difficulty);

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

                //Guess prompt, intake, and store; could add an option for them to correct the whole word here.
                do
                {
                    Console.Write("Guess a letter: ");
                    input = Console.ReadLine();

                    input = input.Trim().ToLower();

                    if (input == "exit")
                    {
                        Console.WriteLine("Closing Program.");
                        break;
                    }

                    if (input.Length > 1 && input != "exit" && input != correctWord)
                    {
                        Console.WriteLine("You have either guessed the incorrect word or typed more than one character -- Please try again.");
                    }
                }
                while (input.Length > 1 && input != correctWord);

                if (input == correctWord || input == "exit")
                {
                    break;
                }

                //if guess already exists... Don't add, don't add strike. Display message.
                guess = Convert.ToChar(input);
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
                    Console.Clear(); //adjust so it won't clear on last try
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
        }

        private static string WordDifficulty(int difficulty)
        {
            string word = "";
            Random rnd = new Random();
            int num = rnd.Next(0, 9);

            //change from array to a class & list for hints for each word and customization in two players
            string[] easyWordList =
            {
                "dessert",
                "peace", //replace...?
                "tennis",
                "strike",
                "distress",
                "telephone",
                "kitchen",
                "outside",
                "aioli",
                "deodorant"
            };

            string[] mediumWordList =
            {
                "computer",
                "stretch",
                "sunglasses",
                "kitchen",
                "national",
                "basketball",
                "equip",
                "pajama",
                "avenue",
                "deodorant"
            };

            string[] hardWordList =
            {
                "beekeeper",
                "awkward",
                "interview",
                "pneumonia",
                "onyx",
                "disavow",
                "kiosk",
                "jackpot",
                "galaxy",
                "vitamins"
            };

            if (difficulty == 1)
            {
                word = easyWordList[num];
            }
            else if (difficulty == 2)
            {
                word = mediumWordList[num];
            }
            else
            {
                word = hardWordList[num];
            }

            return word;
        }
        private static void Display(char[] displayArr)
        {
            foreach (var letter in displayArr)
            {
                Console.Write("{0 }", letter);
            }

            Console.WriteLine("\n");
        }
    }
}

/*
 * multiplayer
 *specify whose turn it is
 *ask for how many rounds (provide options --3, 5, or 7, where last of each round when the score is even is "tiebreaker!!!" )
 *option to provide the word (with a character limit, maybe 20) supercalifragilisticexpialidocious, no numbers or special characters
 *random generator for what hint to choose to display; 3 hints max.
 *two players will need the difficulty choices individually?
 *check for numbers in custom words
 *
 *general notes
 *add hints later on? -- add option to add hints if providing the word. asking for a hint also adds a limb to the hangman. add a warning when they only have 2 limbs left and they can't ask for a hint on the last... leg (limb)
 *add catch if user enters a number
 *if letter was previously entered, don't log it, no strike, message to user it was previously guessed.
 */