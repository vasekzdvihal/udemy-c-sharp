using System;
using System.Collections.Generic;

namespace ChainOfResponsibility.Exercise
{
    public abstract class Creature
    {
        protected Game game;
        protected int baseAttack;
        protected int baseDefense;

        protected Creature(Game game, int baseAttack, int baseDefense)
        {
            this.game = game;
            this.baseAttack = baseAttack;
            this.baseDefense = baseDefense;
        }
        
        public virtual int Attack { get; set; }
        public virtual int Defense { get; set; }
        public abstract void Query(object source, StatQuery sq);

        public override string ToString()
        {
            return $"{nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
        }
    }

    public class Goblin : Creature
    {
        public override void Query(object source, StatQuery sq)
        {
            if (ReferenceEquals(source, this))
            {
                switch (sq.Statistic)
                {
                    case Statistic.Attack:
                        sq.Result += baseAttack;
                        break;
                    case Statistic.Defense:
                        sq.Result += baseDefense;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                if (sq.Statistic == Statistic.Defense)
                {
                    sq.Result++;
                }
            }
        }

        public override int Defense
        {
            get
            {
                var q = new StatQuery {Statistic = Statistic.Defense};
                foreach(var c in game.Creatures)
                    c.Query(this, q);
                return q.Result;
            }
        }

        public override int Attack
        {
            get
            {
                var q = new StatQuery {Statistic = Statistic.Attack};
                foreach(var c in game.Creatures)
                    c.Query(this, q);
                return q.Result;
            }
        }

        public Goblin(Game game) : base(game, 1, 1)
        {
            
        }

        protected Goblin(Game game, int baseAttack, int baseDefense) 
            : base(game, baseAttack, baseDefense)
        {
            
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(Defense)}: {Defense}, {nameof(Attack)}: {Attack}";
        }
    }

    public class GoblinKing : Goblin
    {
        public GoblinKing(Game game) : base(game, 3,3)
        { }

        public override void Query(object source, StatQuery sq)
        {
            if (!ReferenceEquals(source, this) && sq.Statistic == Statistic.Attack)
            {
                sq.Result++; // every goblin gets +1 attack
            }
            else base.Query(source, sq);
        }
    }

    public enum Statistic
    {
        Attack,
        Defense
    }
    
    public class StatQuery
    {
        public Statistic Statistic;
        public int Result;
    }
    
    public class Game
    {
        public IList<Creature> Creatures = new List<Creature>();
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            
            var goblin = new Goblin(game);
            game.Creatures.Add(goblin);
            Console.WriteLine(goblin);

            var goblinKing = new GoblinKing(game);
            game.Creatures.Add(goblinKing);
            Console.WriteLine(goblinKing);
        }
    }
}