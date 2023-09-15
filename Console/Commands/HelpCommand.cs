using Console.Common;
namespace Console.Commands
{
    public class HelpCommand : ICommand
    {
        public int Execute(string[] args)
        {
            System.Console.WriteLine("Usage: Console.exe <command> [options]");
            System.Console.WriteLine();
            System.Console.WriteLine("Commands:");
            System.Console.WriteLine($"  {CommandsEnum.Help.GetCommand()}        Display help.");
            System.Console.WriteLine($"  {CommandsEnum.Version.GetCommand()}     Display version.");

            return (int)ReturnCodesEnum.Success;
        }
    }
}
