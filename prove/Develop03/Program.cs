using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        
        List<Scripture> scriptures = new List<Scripture>();

        scriptures.Add(
            new Scripture(
                new Reference("Proverbs", 3, 5),
                "Trust in the Lord with all thine heart and lean not unto thine own understanding"
            )
        );

        scriptures.Add(
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world that he gave his only begotten Son that whosoever believeth in him should not perish but have everlasting life"
            )
        );

        scriptures.Add(
            new Scripture(
                new Reference("1 Nephi", 3, 7),
                "I will go and do the things which the Lord hath commanded"
            )
        );

        scriptures.Add(
            new Scripture(
                new Reference("D&C", 90, 24),
                "Search diligently, pray always, and be believing, and all things shall work together for your good, if ye walk uprightly and remember the covenant wherewith ye have covenanted one with another. "
            )
        );

        
        Random random = new Random();
        int index = random.Next(scriptures.Count);
        Scripture scripture = scriptures[index];

        
        while (true)
        {
            Console.Clear();
            scripture.Display();

            Console.WriteLine();
            Console.WriteLine("Press ENTER to hide words or type 'quit' to exit:");

            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(3);

            if (scripture.AllWordsHidden())
            {
                Console.Clear();
                scripture.Display();
                Console.WriteLine("\nAll words are hidden. Program ending.");
                break;
            }
        }
    }
}

