namespace Console.Commands
{
    public interface ICommand
    {
        int Execute(string[] args);
    }
}
