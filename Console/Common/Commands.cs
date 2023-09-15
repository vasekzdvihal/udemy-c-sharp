namespace Console.Common
{
    public enum CommandsEnum
    {
        Help,
        Version
    }

    public static class CommandsExtensions
    {
        public static string GetCommand(this CommandsEnum commandsEnum)
        {
            switch (commandsEnum)
            {
                case CommandsEnum.Help:
                    return "--help";
                case CommandsEnum.Version:
                    return "--version";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
