using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        List<Goal> goals = new List<Goal>();
        int score = 0;
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nScore: " + score);
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Quit");

            Console.Write("Select option: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("1. Simple Goal");
                Console.WriteLine("2. Eternal Goal");
                Console.WriteLine("3. Checklist Goal");

                string type = Console.ReadLine();

                Console.Write("Goal Name: ");
                string name = Console.ReadLine();

                Console.Write("Description: ");
                string description = Console.ReadLine();

                Console.Write("Points: ");
                int points = int.Parse(Console.ReadLine());

                if (type == "1")
                {
                    goals.Add(new SimpleGoal(name, description, points));
                }
                else if (type == "2")
                {
                    goals.Add(new EternalGoal(name, description, points));
                }
                else if (type == "3")
                {
                    Console.Write("Times to complete: ");
                    int target = int.Parse(Console.ReadLine());

                    Console.Write("Bonus points: ");
                    int bonus = int.Parse(Console.ReadLine());

                    goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                }
            }

            else if (choice == "2")
            {
                for (int i = 0; i < goals.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {goals[i].GetStatus()}");
                }
            }

            else if (choice == "3")
            {
                Console.Write("Which goal did you complete? ");
                int index = int.Parse(Console.ReadLine()) - 1;

                score += goals[index].RecordEvent();
            }

            else if (choice == "4")
            {
                Console.Write("Filename: ");
                string file = Console.ReadLine();

                using (StreamWriter output = new StreamWriter(file))
                {
                    output.WriteLine(score);

                    foreach (Goal g in goals)
                    {
                        output.WriteLine(g.GetSaveString());
                    }
                }
            }

            else if (choice == "5")
            {
                Console.Write("Filename: ");
                string file = Console.ReadLine();

                string[] lines = File.ReadAllLines(file);

                goals.Clear();
                score = int.Parse(lines[0]);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split("|");

                    if (parts[0] == "Simple")
                    {
                        goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4])));
                    }

                    if (parts[0] == "Eternal")
                    {
                        goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                    }

                    if (parts[0] == "Checklist")
                    {
                        goals.Add(new ChecklistGoal(
                            parts[1],
                            parts[2],
                            int.Parse(parts[3]),
                            int.Parse(parts[4]),
                            int.Parse(parts[5]),
                            int.Parse(parts[6])
                        ));
                    }
                }
            }

            else if (choice == "6")
            {
                running = false;
            }
        }
    }
}

class Goal
{
    protected string _name;
    protected string _description;
    protected int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public virtual int RecordEvent()
    {
        return _points;
    }

    public virtual string GetStatus()
    {
        return _name;
    }

    public virtual string GetSaveString()
    {
        return "";
    }
}

class SimpleGoal : Goal
{
    private bool _complete;

    public SimpleGoal(string name, string description, int points, bool complete = false)
        : base(name, description, points)
    {
        _complete = complete;
    }

    public override int RecordEvent()
    {
        if (!_complete)
        {
            _complete = true;
            return _points;
        }
        return 0;
    }

    public override string GetStatus()
    {
        string box = _complete ? "[X]" : "[ ]";
        return $"{box} {_name} ({_description})";
    }

    public override string GetSaveString()
    {
        return $"Simple|{_name}|{_description}|{_points}|{_complete}";
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        return _points;
    }

    public override string GetStatus()
    {
        return $"[∞] {_name} ({_description})";
    }

    public override string GetSaveString()
    {
        return $"Eternal|{_name}|{_description}|{_points}";
    }
}

class ChecklistGoal : Goal
{
    private int _count;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string name, string description, int points, int target, int bonus, int count = 0)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _count = count;
    }

    public override int RecordEvent()
    {
        _count++;

        if (_count == _target)
        {
            return _points + _bonus;
        }

        return _points;
    }

    public override string GetStatus()
    {
        string box = _count >= _target ? "[X]" : "[ ]";
        return $"{box} {_name} ({_description}) -- Completed {_count}/{_target}";
    }

    public override string GetSaveString()
    {
        return $"Checklist|{_name}|{_description}|{_points}|{_target}|{_bonus}|{_count}";
    }
}