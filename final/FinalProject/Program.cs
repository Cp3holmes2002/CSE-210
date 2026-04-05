using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace FinalProject
{
    // ========================= PATIENT =========================
    public class Patient
    {
        public string Name { get; private set; }
        public double BloodGlucose { get; set; }

        public Patient(string name, double initialGlucose)
        {
            Name = name;
            BloodGlucose = initialGlucose;
        }
    }

    // ========================= INSULIN =========================
    public abstract class InsulinDelivery
    {
        public abstract double DeliverDose(double glucoseLevel);
    }

    public class BasalDelivery : InsulinDelivery
    {
        private double _rate;

        public BasalDelivery(double rate)
        {
            _rate = rate;
        }

        public override double DeliverDose(double glucoseLevel)
        {
            return _rate / 12.0;
        }
    }

    public class ClosedLoopController
    {
        private double _target;
        private double _sensitivity;

        public ClosedLoopController(double target, double sensitivity)
        {
            _target = target;
            _sensitivity = sensitivity;
        }

        public double CalculateAutoCorrection(double glucose)
        {
            if (glucose > _target)
            {
                return (glucose - _target) / _sensitivity;
            }
            return 0;
        }
    }

    // ========================= MEAL =========================
    public class Meal
    {
        public double Carbs { get; private set; }
        private int _cycles;

        public Meal(double carbs, int cycles)
        {
            Carbs = carbs;
            _cycles = cycles;
        }

        public double Absorb()
        {
            if (_cycles <= 0) return 0;

            double rise = (Carbs * 3) / 12.0;
            _cycles--;
            return rise;
        }

        public bool Done()
        {
            return _cycles <= 0;
        }
    }

    // ========================= PUMP =========================
    public class InsulinPump
    {
        private Patient _patient;
        private BasalDelivery _basal;
        private ClosedLoopController _controller;
        private List<Meal> _meals;
        private List<string> _log;

        public InsulinPump(Patient patient, BasalDelivery basal, ClosedLoopController controller)
        {
            _patient = patient;
            _basal = basal;
            _controller = controller;
            _meals = new List<Meal>();
            _log = new List<string>();

            _log.Add("Time,Glucose");
        }

        public void EatMeal(double carbs)
        {
            Console.WriteLine("Meal: " + carbs + "g carbs");
            _meals.Add(new Meal(carbs, 12));
        }

        public void RunCycle(int minute)
        {
            double insulin = 0;

            insulin += _basal.DeliverDose(_patient.BloodGlucose);
            insulin += _controller.CalculateAutoCorrection(_patient.BloodGlucose);

            ApplyInsulin(insulin);

            double mealImpact = 0;
            foreach (Meal m in _meals)
            {
                mealImpact += m.Absorb();
            }

            _patient.BloodGlucose += mealImpact;
            _meals.RemoveAll(delegate(Meal m) { return m.Done(); });

            _patient.BloodGlucose += 1;

            Console.WriteLine("Time: " + minute + " min | Glucose: " + _patient.BloodGlucose.ToString("F1"));

            _log.Add(minute + "," + _patient.BloodGlucose.ToString("F1"));
        }

        private void ApplyInsulin(double dose)
        {
            _patient.BloodGlucose -= dose * 40;
        }

        public void ExportCSV(string path)
        {
            File.WriteAllLines(path, _log);
            Console.WriteLine("\nCSV exported to: " + path);
        }
    }

    // ========================= PROGRAM =========================
    class Program
    {
        static void Main(string[] args)
        {
            Patient patient = new Patient("Connor", 120);

            BasalDelivery basal = new BasalDelivery(1.2);
            ClosedLoopController controller = new ClosedLoopController(100, 40);

            InsulinPump pump = new InsulinPump(patient, basal, controller);

            Console.WriteLine("=== Closed Loop Insulin Pump Simulation ===\n");

            for (int t = 0; t <= 180; t += 5)
            {
                if (t == 10)
                {
                    pump.EatMeal(60);
                }

                if (t == 90)
                {
                    pump.EatMeal(50);
                }

                pump.RunCycle(t);
                Thread.Sleep(100);
            }

            pump.ExportCSV("glucose_data.csv");

            Console.WriteLine("\nOpen the CSV in Excel to see the glucose graph.");
        }
    }
}
