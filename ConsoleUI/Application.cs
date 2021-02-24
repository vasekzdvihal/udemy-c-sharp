using DemoLibrary;

namespace ConsoleUI
{
    public interface IApplication
    {
        void Run();
    }

    public class Application : IApplication
    {
        IBusinessLogic _bussinesLogic;

        public Application(IBusinessLogic businessLogic)
        {
            _bussinesLogic = businessLogic;
        }

        public void Run()
        {
            _bussinesLogic.ProcessData();
        }
    }
}