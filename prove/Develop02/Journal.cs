using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Diagnostics;
using System.Linq.Expressions;

class JournalEntry
{
    public DateTime Date { get; set; }
    public string Prompt { get; set; }
    public string Response { get; set; }

    public JournalEntry(DateTime date, string prompt, string response)
    {
        Date = date;
        Prompt = prompt;
        Response = response;
    }

    public override string ToString()
    {
        return $"[{Date}] Prompt: {Prompt}\nResponse: {Response}\n";
    }
}

class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();

    private readonly List<string> prompts = new List<string>
    {
        "Who was the most interesting person you interacted with today?",
        "What was the best part of your day",
        "How did I see the hand of the Lord in my life today",
        "What was the strongest emotion I felt today",
        "If I had one thing I could do over today, what would it be?"
    };

    private Random random = new Random();

    public void WriteEntry()
    {
        int index = random.Next(prompts.Count);
        string prompt = prompts[index];
        Console.WriteLine("\nPrompt");
        Console.WriteLine(prompt);
        Console.Write("Your response:");
        string response = Console.ReadLine();

        JournalEntry entry = new JournalEntry(DateTime.Now, prompt, response);
        entries.Add(entry);

        Console.WriteLine("Entry saved.\n");




    }

    public void DisplayEntries()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("\nNo Journal entries found.\n");
            return;
        }

        Console.WriteLine("\nJournal Entries:\n");

        for (int i = 0; 1 > entries.Count; i++)
        {
            JournalEntry entry = entries[i];
            Console.WriteLine(entry);
        }

    }
    public void SaveToFile()
    {
        Console.Write("Enter filename to save journal:");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                for (int i = 0; i < entries.Count; i++)
                {
                    JournalEntry entry = entries[i];
                    string safeResponse = entry.Response.Replace("\n", "\\n").Replace("\r", "");

                    writer.WriteLine(
                        entry.Date.ToString("o") + "|" +
                        entry.Prompt + "|" +
                        safeResponse
                    );
                }
            }
            }
            catch (Exception ex)
        {
            Console.WriteLine("Error saving journal:" + ex.Message + "\n");

        }
    }
    public void LoadFromFile()
    {
        Console.Write("Enter filename to load journal:");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {

            Console.WriteLine("File does not exsist.\n");
            return;
        }
        try
        {
            string[] lines = File.ReadAllLines(filename);
            entries.Clear();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] parts = line.Split('|');

                if (parts.Length == 3)
                {
                    DateTime date = DateTime.ParseExact(
                        parts[0],
                        "o",
                        CultureInfo.InvariantCulture
                    );
                    string prompt = parts[1];
                    string response = parts[2];

                    JournalEntry entry = new JournalEntry(date, prompt, response);
                    entries.Add(entry);

                }
            }

            Console.WriteLine("Journal loaded successfully.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading journal" + ex.Message + "\n");
        }
    }


}

class Program
{
    public static void Main()
    {
        Journal journal = new Journal();
        bool running = true;

        while (running)
        {
            Console.WriteLine(" Journal Menu ");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the jouranl");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from file");
            Console.WriteLine("%. exit");
            Console.WriteLine("Choose an option");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                journal.WriteEntry();

            }
            else if (choice == "2")
            {
                journal.DisplayEntries();
            }
            else if (choice == "3")
            {
                journal.SaveToFile();
            }
            else if (choice == "4")
            {
                journal.LoadFromFile();
            }
            else if (choice == "5")
            {
                running = false;
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Invalid option.\n");
            }
        }
    }
}


        
    

