using Console.Commands;
using Console.Common;

namespace Console
{
    public class CommandFactory
    {
        private readonly Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>
        {
            { CommandsEnum.Help.GetCommand(), new HelpCommand() },
            { CommandsEnum.Version.GetCommand(), new VersionCommand() }
        };

        public int ExecuteCommand(string[] args)
        {
            if (args.Length == 0)
            {
                System.Console.WriteLine($"No command specified. Write  {CommandsEnum.Help.GetCommand()} for help.");
                return (int)ReturnCodesEnum.UnknownCommand;
            }

            var commandName = args[0];
            if (commands.TryGetValue(commandName, out var command))
            {
                return command.Execute(args.Skip(1).ToArray());
            }

            System.Console.WriteLine($"Unknown command: {commandName}");
            return (int)ReturnCodesEnum.UnknownCommand;
        }
    }
}
