using System;
using System.Threading;

class ReflectionActivity : Activity
{
    public ReflectionActivity()
    {
        _name = "Reflection Activity";
        _description = "This activity helps you reflect on times you showed strength.";
    }

    public void Run()
    {
        StartActivity();

        string[] prompts = new string[3];
        prompts[0] = "Think of a time you did something hard.";
        prompts[1] = "Think of a time you helped someone.";
        prompts[2] = "Think of a time you overcame fear.";

        Random random = new Random();
        int index = random.Next(prompts.Length);

        Console.WriteLine();
        Console.WriteLine(prompts[index]);
        Console.WriteLine();
        Console.WriteLine("Reflect on this...");
        Thread.Sleep(_duration * 1000);

        EndActivity();
    }
}