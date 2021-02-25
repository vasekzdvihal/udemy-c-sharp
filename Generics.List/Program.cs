using System;
using System.Collections.Generic;
using Generics.List.Models;
using Generics.List.WithoutGenerics;

namespace Generics.List
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            
            DemonstrateTextFileStorage();
            
            Console.WriteLine();
            Console.WriteLine("Press enter to shut down...");
            Console.ReadLine();
        }

        private static void DemonstrateTextFileStorage()
        {
            List<Person> people = new List<Person>();
            List<LogEntry> logs = new List<LogEntry>();

            string peopleFile = @"C:\Temp\people.csv";
            string logFile = @"C:\Temp\log.txt";

            PopulateList(people, logs);

            // People
            OriginalTextFileProcessor.SavePeople(people, peopleFile);
            var newPeople = OriginalTextFileProcessor.LoadPeople(peopleFile);
            foreach (var p in newPeople)
            {
                Console.WriteLine($"{p.FirstName}{p.LastName} (IsAlive = {p.IsAlive})");
            }

            Console.ReadLine();
            
            // Logs
            OriginalTextFileProcessor.SaveLogs(logs, logFile);
            var newLogs = OriginalTextFileProcessor.LoadLogs(logFile);
            foreach (var l in newLogs)
            {
                Console.WriteLine($"{l.ErrorCode}: {l.Message} at {l.TimeOfEvent.ToShortDateString()}");
            }
        }

        private static void PopulateList(List<Person> people, List<LogEntry> logs)
        {
            people.Add(new Person{ FirstName = "Tim", LastName = "Corey" });
            people.Add(new Person{ FirstName = "Sue", LastName = "Storm", IsAlive = false});
            people.Add(new Person{ FirstName = "Greg", LastName = "Olsen"});
            
            logs.Add(new LogEntry { Message = "I blew up", ErrorCode = 9999 });
            logs.Add(new LogEntry { Message = "I'm too awesome", ErrorCode = 1337 });
            logs.Add(new LogEntry { Message = "I was tired", ErrorCode = 2222 });
        }
    }
}