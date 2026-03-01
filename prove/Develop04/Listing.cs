using System;
using System.Collections.Generic;

class ListingActivity : Activity
{
    public ListingActivity()
    {
        _name = "Listing Activity";
        _description = "This activity helps you list positive things in your life.";
    }

    public void Run()
    {
        StartActivity();

        Console.WriteLine();
        Console.WriteLine("List as many things as you can. Press Enter after each item.");
        Console.WriteLine("You may begin...");
        Console.WriteLine();

        List<string> items = new List<string>();

        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            items.Add(input);
        }

        Console.WriteLine();
        Console.WriteLine("You listed " + items.Count + " items!");

        EndActivity();
    }
}