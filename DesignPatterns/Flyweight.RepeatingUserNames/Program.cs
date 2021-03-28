using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using JetBrains.dotMemoryUnit;
using NUnit.Framework;

namespace Flyweight.RepeatingUserNames
{
    public class User
    {
        private string fullName;

        public User(string fullName)
        {
            this.fullName = fullName;
        }
    }

    public class User2
    {
        private static List<string> strings = new List<string>();
        private int[] names;
        
        public User2(string fullName)
        {
            int getOrAdd(string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }
        
        public string FullName => string.Join(' ', names.Select(i => strings[i]));
    }
    
    [TestFixture]
    class Program
    {
        // static void Main(string[] args)
        // {
        //     Console.WriteLine("Hello World!");
        // }

        [Test]
        public void TestUser() // 8353986
        {
            
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User>();
            
            foreach(var firstname in firstNames) 
                foreach(var lastname in lastNames)
                    users.Add(new User($"{firstname} {lastname}"));

            ForceGC();

            
            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });
        }        
        
        [Test]
        public void TestUser2() // 8610856
        {
            
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());

            var users = new List<User2>();
            
            foreach(var firstname in firstNames) 
                foreach(var lastname in lastNames)
                    users.Add(new User2($"{firstname} {lastname}"));

            ForceGC();

            
            dotMemory.Check(memory =>
            {
                Console.WriteLine(memory.SizeInBytes);
            });
        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private string RandomString()
        {
            Random rand = new Random();
            return new string(
                Enumerable.Range(0, 10)
                    .Select(i => (char) ('a' + rand.Next(26)))
                    .ToArray());
        }
    }
}