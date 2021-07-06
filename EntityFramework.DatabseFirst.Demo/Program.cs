namespace EntityFramework.DatabseFirst.Demo
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var context = new DemoEFEntities2();

            var post = new Post()
            {
                Body = "Body",
                DatePublished = System.DateTime.Now,
                Title = "Title",
                PostID = 1
            };

            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}