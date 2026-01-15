using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<int> numbers = new List<int>();

        int Usernumber = -1;
        while (Usernumber != 0)
        {
            Console.Write("Enter a number (enter 0 to quit): ");

            string Useresponse = Console.ReadLine();
            Usernumber = int.Parse(Useresponse);

            if (Usernumber != 0)
            {
                numbers.Add(Usernumber);
            }



        }
        int sum = 0;
        foreach (int number in numbers)
        {
            sum += number;
        }

        Console.WriteLine($"The sum is; {sum}");

        float average = (float) sum / numbers.Count;
        Console.WriteLine($"The average is: {average} ");

        int max = numbers[0];

        foreach (int number in numbers)
        {
            if (number > max)
            {
                max = number;
            }
        }

        Console.WriteLine($"The max is: {max}");

    }
}