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
            HangmanWord wordAndHint = GenerateWord(difficulty);
            string correctWord = wordAndHint.Word;
            string hint = wordAndHint.Hint;

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

                    Console.WriteLine("Need a hint? Type \"hint\".");
                    Console.WriteLine("To quit, type \"exit\".");


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
        }

        public static HangmanWord GenerateWord(int difficulty)
        {
            HangmanWord selection;
            Random rnd = new Random();
            int num = rnd.Next(0, 9);

            //change from array to a class & list for hints for each word and customization in two players
            List<HangmanWord> easyWordList = new List<HangmanWord>()
            {
                new HangmanWord {Word = "dessert", Hint = "You eat this when you're done eating." },
                new HangmanWord {Word = "peace", Hint = "What the world seems unable to achieve" },
                new HangmanWord {Word = "tennis", Hint = "Serena will defeat you." },
                new HangmanWord {Word = "strike", Hint = "3 of these and you're out." },
                new HangmanWord {Word = "distress", Hint = "A negative overwhelming feeling." },
                new HangmanWord {Word = "telephone", Hint = "So call me, maybe." },
                new HangmanWord {Word = "sanitize", Hint = "Clean up, clean up, everybody clean up." },
                new HangmanWord {Word = "outside", Hint = "Get off of the computer and go here instead." },
                new HangmanWord {Word = "aioli", Hint = "Underrated condiment." },
                new HangmanWord {Word = "deodorant", Hint = "Goodness, did you put any on today?" },
            };

            List<HangmanWord> mediumWordList = new List<HangmanWord>()
            {
                new HangmanWord {Word = "computer", Hint = $"Speak binary to me." },
                new HangmanWord {Word = "stretch", Hint = "\"I'll take 'Things you're less likely to do as you get older' for 500, Alex.\"" },
                new HangmanWord {Word = "sunglasses", Hint = "Corey Hart wears thse at night." },
                new HangmanWord {Word = "national", Hint = "Add a \"The\" in the beginning and you've got a great band." },
                new HangmanWord {Word = "basketball", Hint = "Kobe." },
                new HangmanWord {Word = "equip", Hint = "To put something on." },
                new HangmanWord {Word = "pajama", Hint = "It's 2020, there's a pandemic, and you haven't changed out of these for a year." },
                new HangmanWord {Word = "avenue", Hint = "Where I used to sit and talk to you, we were both 16 and it felt so right..." },
                new HangmanWord {Word = "dictionary", Hint = "I don't know, man, look it up." },
                new HangmanWord {Word = "kitchen", Hint = "Maybe if you check the fridge again, you'll find the answer." },
            };

            List<HangmanWord> hardWordList = new List<HangmanWord>()
            {
                new HangmanWord {Word = "beekeeper", Hint = "Harvester of honey, savior of the world." },
                new HangmanWord {Word = "awkward", Hint = "What Wayne wishes Daryl wasn't so much of." },
                new HangmanWord {Word = "interview", Hint = "Talk one-one, usually in an official manner." },
                new HangmanWord {Word = "pneumonia", Hint = "A respiratory infection." },
                new HangmanWord {Word = "onyx", Hint = "Both a color and a pokemon, though varying slightly in spelling." },
                new HangmanWord {Word = "disavow", Hint = "To deny responsibility of, to refuse to acknowledge or accept." },
                new HangmanWord {Word = "kiosk", Hint = "Don't look the people who work at these in the eye and you'll get away just fine." },
                new HangmanWord {Word = "jackpot", Hint = "Things you hit before you no longer work." },
                new HangmanWord {Word = "galaxy", Hint = "\"Milkyway\" was the better choice of the candybar name, for sure." },
                new HangmanWord {Word = "vitamin", Hint = "Take them. " },
            };

            if (difficulty == 1)
            {
                selection = easyWordList[num];
            }
            else if (difficulty == 2)
            {
                selection = mediumWordList[num];
            }
            else
            {
                selection = hardWordList[num];
            }

            return selection;
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

    class HangmanWord
    {
        public string Word { get; set; }
        public string Hint { get; set; }
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
 * add a diff type of hint--where it displays a letter (this one will add a limb to the hangman.
 */