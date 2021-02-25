using System;
using System.Collections.Generic;
using System.Linq;
using Generics.List.Models;

namespace Generics.List.WithoutGenerics
{
    public class OriginalTextFileProcessor
    {
        public static List<Person> LoadPeople(string filePath)
        {
            List<Person> output = new List<Person>();
            Person p;
            var lines = System.IO.File.ReadAllLines(filePath).ToList();
            
            // Remove the header now
            lines.RemoveAt(0);

            foreach (var line in lines)
            {
                var vals = line.Split(',');
                p = new Person();

                p.FirstName = vals[0];
                p.IsAlive = bool.Parse(vals[1]);
                p.LastName = vals[2];
                
                output.Add(p);
            }

            return output;
        }
        
        public static List<LogEntry> LoadLogs(string filePath)
        {
            List<LogEntry> output = new List<LogEntry>();
            LogEntry logEntry;
            var lines = System.IO.File.ReadAllLines(filePath).ToList();
            
            // Remove the header now
            lines.RemoveAt(0);

            foreach (var line in lines)
            {
                var vals = line.Split(',');
                logEntry = new LogEntry();

                logEntry.ErrorCode = Int32.Parse(vals[0]);
                logEntry.Message = vals[1];
                logEntry.TimeOfEvent = DateTime.Parse(vals[2]);
                
                output.Add(logEntry);
            }

            return output;
        }
        
        public static void SavePeople(List<Person> people, string filePath)
        {
            List<string> lines = new List<string>();
            
            //Add a header row
            lines.Add("FirstName,IsAlive,LastName");

            foreach (var p in people)
            {
                lines.Add($"{p.FirstName}, {p.IsAlive}, {p.LastName}");
            }
            
            System.IO.File.WriteAllLines(filePath, lines);
        }
        
        public static void SaveLogs(List<LogEntry> people, string filePath)
        {
            List<string> lines = new List<string>();
            
            //Add a header row
            lines.Add("ErrorCode,Message,TimeOfEvent");

            foreach (var p in people)
            {
                lines.Add($"{p.ErrorCode}, {p.Message}, {p.TimeOfEvent}");
            }
            
            System.IO.File.WriteAllLines(filePath, lines);
        }
    }
}