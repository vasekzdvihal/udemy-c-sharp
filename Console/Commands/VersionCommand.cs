using Console.Common;
namespace Console.Commands
{
    public class VersionCommand : ICommand
    {
        public int Execute(string[] args)
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Console.WriteLine(assembly.GetName().Version);
            return (int)ReturnCodesEnum.Success;
        }
    }
}
