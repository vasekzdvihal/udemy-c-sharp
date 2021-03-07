using System;

namespace DemoLibrary.Utilities
{
    public interface IDataAccess
    {
        void LoadData();
        void SaveData(string dataName);
    }

    public class DataAccess : IDataAccess
    {
        public void LoadData()
        {
            Console.WriteLine("Loading data");
        }

        public void SaveData(string dataName)
        {
            Console.WriteLine($"Saving {dataName}");
        }
    }
}