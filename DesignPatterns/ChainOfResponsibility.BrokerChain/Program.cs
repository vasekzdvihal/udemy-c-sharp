using System;

namespace ChainOfResponsibility.BrokerChain
{
    public class Game
    {
        public event EventHandler<Query> Queries;

        public void PerformQuery(object sender, Query q)
        {
            Queries?.Invoke(sender, q);
        }
    }

    public class Query
    {
        public string CreatureName;

        public enum Argument
        {
            Attack,
            Defense
        }

        public Argument WhatToQuery;
        public int Value;

        public Query(string creatureName, Argument whatToQuery, int value)
        {
            CreatureName = creatureName;
            WhatToQuery = whatToQuery;
            Value = value;
        }
    }

    public class Creature
    {
        private Game game;
        public string Name;
        private int attack, defense;

        public Creature(Game game, string name, int attack, int defense)
        {
            this.game = game;
            Name = name;
            this.attack = attack;
            this.defense = defense;
        }

        public int Attact
        {
            get
            {
                var q = new Query(Name, Query.Argument.Attack, attack);
                game.PerformQuery(this, q); // q.Value
                return q.Value;
            }
        }

        public int Defense
        {
            get
            {
                var q = new Query(Name, Query.Argument.Defense, defense);
                game.PerformQuery(this, q); // q.Value
                return q.Value;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Attact)}: {Attact}, {nameof(Defense)}: {Defense}";
        }
    }

    public abstract class CreatureModifier : IDisposable
    {
        protected Game game;
        protected Creature creature;

        protected CreatureModifier(Game game, Creature creature)
        {
            this.game = game;
            this.creature = creature;
            game.Queries += Handle;
        }

        protected abstract void Handle(object sender, Query q);

        public void Dispose()
        {
            game.Queries -= Handle;
        }
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Game game, Creature creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name && q.WhatToQuery == Query.Argument.Attack)
            {
                q.Value *= 2;
            } 
        }
    }

    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Game game, Creature creature) : base(game, creature)
        {
        }

        protected override void Handle(object sender, Query q)
        {
            if (q.CreatureName == creature.Name && q.WhatToQuery == Query.Argument.Defense)
            {
                q.Value *= 2;
            } 
        }
    }

    class Program
    { 
        static void Main(string[] args)
        {
            var game = new Game();
            var goblin = new Creature(game, "Strong Goblin", 3, 3);
            Console.WriteLine(goblin);

            using (new DoubleAttackModifier(game, goblin))
            {
                Console.WriteLine(goblin);
                using (new IncreaseDefenseModifier(game, goblin))
                {
                    Console.WriteLine(goblin);
                }
            }

            Console.WriteLine(goblin);
        }
    }
}