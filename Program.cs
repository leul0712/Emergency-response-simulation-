using System.Collections.Generic;
using System;

namespace EmergencyResponseSimulation
{
    abstract class EmergencyUnit
    {
        public string Name { get; set; }
        public int Speed { get; set; }

        public EmergencyUnit(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }

        public abstract bool CanHandle(string incidentType);
        public abstract void RespondToIncident(Incident incident);
    }

    class Police : EmergencyUnit
    {
        public Police(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Crime";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is responding to a crime at {incident.Location}.");
        }
    }

    class Firefighter : EmergencyUnit
    {
        public Firefighter(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Fire";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is extinguishing a fire at {incident.Location}.");
        }
    }

    // Ambulance class
    class Ambulance : EmergencyUnit
    {
        public Ambulance(string name, int speed) : base(name, speed) { }

        public override bool CanHandle(string incidentType)
        {
            return incidentType == "Medical";
        }

        public override void RespondToIncident(Incident incident)
        {
            Console.WriteLine($"{Name} is treating patients at {incident.Location}.");
        }
    }

    class Incident
    {
        public string Type { get; set; }
        public string Location { get; set; }
        public string Difficulty { get; set; } 

        public Incident(string type, string location, string difficulty)
        {
            Type = type;
            Location = location;
            Difficulty = difficulty;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Random rng = new Random();
            int totalScore = 0;

            List<EmergencyUnit> emergencyUnits = new List<EmergencyUnit>
            {
                new Police("Police Unit 1", 80),
                new Firefighter("Firefighter Unit 1", 70),
                new Ambulance("Ambulance Unit 1", 90)
            };

            string[] incidentTypes = { "Fire", "Crime", "Medical" };
            string[] locations = { "City Hall", "Downtown", "Suburbs", "Park", "Mall" };
            string[] difficultyLevels = { "Easy", "Medium", "Difficult" };

            for (int turn = 1; turn <= 5; turn++)
            {
                Console.WriteLine($"\n--- Turn {turn} ---");

                string incidentType = incidentTypes[rng.Next(incidentTypes.Length)];
                string incidentLocation = locations[rng.Next(locations.Length)];
                string incidentDifficulty = difficultyLevels[rng.Next(difficultyLevels.Length)];

                Incident newIncident = new Incident(incidentType, incidentLocation, incidentDifficulty);

                Console.WriteLine($"Incident: {newIncident.Type} at {newIncident.Location} [Difficulty: {newIncident.Difficulty}]");

                bool incidentHandled = false;

                foreach (var unit in emergencyUnits)
                {
                    if (unit.CanHandle(newIncident.Type))
                    {
                        unit.RespondToIncident(newIncident);

                        Console.WriteLine("+10 points");
                        totalScore += 10;
                        incidentHandled = true;
                        break;
                    }
                }

                if (!incidentHandled)
                {
                    Console.WriteLine("No unit available to handle this incident.");
                    Console.WriteLine("-5 points");
                    totalScore -= 5;
                }

                Console.WriteLine($"Current Score: {totalScore}");
            }

            Console.WriteLine($"\nFinal Score: {totalScore}");
        }
    }
}
