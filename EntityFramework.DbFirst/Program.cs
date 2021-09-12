using System;

namespace EntityFramework.DbFirst
{
    public enum Level : byte
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3,
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var dbContext = new PlutoDbContext();
            var courses = dbContext.GetCourses();
            foreach(var c in courses)
                Console.WriteLine(c.Title);

            var course = new Course { Level = Level.Advanced };
        }
    }
}