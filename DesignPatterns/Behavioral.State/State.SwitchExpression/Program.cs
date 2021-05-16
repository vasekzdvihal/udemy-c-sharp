using System;

namespace State.SwitchExpression
{
    enum Chest
    {
        Open, Closed, Locked
    }

    enum Action
    {
        Open, Close
    }
    
    class Program
    {
        static Chest Manipulate(Chest chest, Action action, bool haveKey)
            => (chest, action, haveKey)
                switch
                {
                    (Chest.Locked, Action.Open, true) => Chest.Open,
                    (Chest.Closed, Action.Open, _) => Chest.Open,
                    (Chest.Open, Action.Close, true) => Chest.Locked,
                    (Chest.Open, Action.Close, false) => Chest.Closed,

                    _ => chest
                };

        static void Main(string[] args)
        {
            var chest = Chest.Locked;
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Open, true);
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is {chest}");

            chest = Manipulate(chest, Action.Close, false);
            Console.WriteLine($"Chest is {chest}");
        }
    }
}