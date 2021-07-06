using System;
using System.Data.Entity;

namespace EntityFramework.Demo.CodeFirst
{
    public class Post
    {
        public int Id { get; set; }
        public DateTime DatePublished { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public class BlogDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }
    
    internal class Program
    {
        
        
        public static void Main(string[] args)
        {
        }
    }
}