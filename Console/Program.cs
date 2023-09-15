namespace Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var commandFactory = new CommandFactory();
            var exitCode = commandFactory.ExecuteCommand(args);
            System.Environment.Exit(exitCode);
        }
    }
}

