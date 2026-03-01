using System;

class BreathingActivity : Activity
{
    public BreathingActivity()
    {
        _name = "Breathing Activity";
        _description = "This activity helps you relax by breathing slowly.";
    }

    public void Run()
    {
        StartActivity();

        int timePassed = 0;

        while (timePassed < _duration)
        {
            Console.Write("Breathe in... ");
            PauseWithCountdown(4);

            Console.Write("Breathe out... ");
            PauseWithCountdown(4);

            timePassed = timePassed + 8;
        }

        EndActivity();
    }
}