using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcomeMessage();

        string UserName = PromptuserName();
        int userNumber = Promptusernumber();

        int squaredNumber = squareNumber(userNumber);

        int birthYear;
        PromptUserBirthYear(out birthYear);

        DisplayResult(UserName, squaredNumber, birthYear);
    }
    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the program");
    }
    static string PromptuserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();

        return name;
    }

    static int Promptusernumber()
    {
        Console.Write("Please enter your favorite number:");
        int number = int.Parse(Console.ReadLine());

        return number;
    }

    static void PromptUserBirthYear(out int birthYear)
    {
        Console.Write($"Please enter the year you were born:");
        birthYear = int.Parse(Console.ReadLine());
    }
    static int squareNumber(int number)
    {
        int square = number * number;
        return square;
    }
    static void DisplayResult(string name, int square, int birthYear)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
        Console.WriteLine($"{name}, you will turn {2026 - birthYear} years old this year.  ");

    }
}
