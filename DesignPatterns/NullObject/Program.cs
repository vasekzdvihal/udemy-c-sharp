using System;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Autofac;

namespace NullObject
{
    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    public class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            Console.WriteLine(msg);
        }

        public void Warn(string msg)
        {
            Console.WriteLine("WARN: " + msg);
        }
    } 
    
    public class BankAccount
    {
        private ILog log;
        private int balance;

        public BankAccount(ILog log)
        {
            this.log = log;
        }

        public void Deposit(int amount)
        {
            balance += amount;
            log.Info($"Deposited {amount}. Balance is now {balance}");
        }
    }
    
    public class NullLog : ILog
    {
        public void Info(string msg){ }
        public void Warn(string msg){ }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var log = new ConsoleLog();
            var ba = new BankAccount(log);
            ba.Deposit(100);

            var cb = new ContainerBuilder();
            cb.RegisterType<BankAccount>();
            cb.RegisterType<NullLog>().As<ILog>();
            using (var c = cb.Build())
            {
                var ba2 = c.Resolve<BankAccount>();
                ba2.Deposit(200);
            }

        }
    }
}