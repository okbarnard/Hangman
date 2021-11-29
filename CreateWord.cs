using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Word
    {
        public string CreateWord { get; set; }
        public string Hint { get; set; }
        public Word(string word, string hint)
        {
            CreateWord = word;
            Hint = hint;
        }

        public static Word GenerateWord(int difficulty)
        {
            //generating this list everytime doesn't seem like the best
            Word selection;
            Random rnd = new Random();
            int num = rnd.Next(0, 9);

            //change from array to a class & list for hints for each word and customization in two players
            List<Word> easyWordList = new List<Word>()
            {
                new Word("dessert","You eat this when you're done eating." ),
                new Word("peace", "What the world seems unable to achieve" ),
                new Word("tennis", "Serena will defeat you." ),
                new Word("strike", "3 of these and you're out."),
                new Word("distress", "A negative overwhelming feeling."),
                new Word("telephone", "So call me, maybe."),
                new Word("sanitize", "Clean up, clean up, everybody clean up."),
                new Word("outside", "Get off of the computer and go here instead."),
                new Word("aioli", "Underrated condiment."),
                new Word("deodorant", "Goodness, did you put any on today?"),
            };

            List<Word> mediumWordList = new List<Word>()
            {
                new Word("computer", $"Speak binary to me." ),
                new Word("stretch", "\"I'll take 'Things you're less likely to do as you get older' for 500, Alex.\"" ),
                new Word("sunglasses", "Corey Hart wears thse at night." ),
                new Word("national", "Add a \"The\" in the beginning and you've got a great band." ),
                new Word("basketball", "Kobe." ),
                new Word("equip", "To put something on." ),
                new Word("pajama", "It's 2020, there's a pandemic, and you haven't changed out of these for a year." ),
                new Word("avenue", "Where I used to sit and talk to you, we were both 16 and it felt so right..." ),
                new Word("dictionary", "I don't know, man, look it up." ),
                new Word("kitchen", "Maybe if you check the fridge again, you'll find the answer." )
            };

            List<Word> hardWordList = new List<Word>()
            {
                new Word("beekeeper", "Harvester of honey, savior of the world." ),
                new Word("awkward", "What Wayne wishes Daryl wasn't so much of." ),
                new Word("interview", "Talk one-one, usually in an official manner." ),
                new Word("pneumonia", "A respiratory infection." ),
                new Word("onyx", "Both a color and a pokemon, though varying slightly in spelling." ),
                new Word("disavow", "To deny responsibility of, to refuse to acknowledge or accept." ),
                new Word("kiosk", "Don't look the people who work at these in the eye and you'll get away just fine." ),
                new Word("jackpot", "Things you hit before you no longer work." ),
                new Word("galaxy", "\"Milkyway\" was the better choice of the candybar name, for sure." ),
                new Word("vitamin", "Take them. " )
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
    }
}
