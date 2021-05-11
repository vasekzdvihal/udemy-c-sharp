using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Autofac;
using ImpromptuInterface;

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
    
    public class Null<TInterface> : DynamicObject where TInterface : class
    {
        public static TInterface Instance => new Null<TInterface>().ActLike<TInterface>();

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Activator.CreateInstance(binder.ReturnType);
            return true;
        }
    }

    
    class Program
    {
        static void Main(string[] args)
        {
            var log = Null<ILog>.Instance;
            var ba = new BankAccount(log);
            ba.Deposit(100);

        }
    }
}